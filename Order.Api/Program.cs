using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Order.Api.Middleware;
using Order.Application;
using Order.Infrastructure;
using Order.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);
var conString = builder.Configuration.GetConnectionString("WwiContext");

builder.Services.AddOpenTelemetry().ConfigureResource(resource => resource.AddService("Api"))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter())
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddConsoleExporter());


// Add services to the container.
builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = (context) =>
    {
        if (context.ProblemDetails.Status == StatusCodes.Status401Unauthorized)
        {
            context.ProblemDetails.Title = "Unauthorized Access";
            context.ProblemDetails.Detail = "You are not authorized to access this resource.";
        }
        else if (context.ProblemDetails.Status == StatusCodes.Status404NotFound)
        {
            context.ProblemDetails.Title = "Resource Not Found";
            context.ProblemDetails.Detail = "The resource you are looking for was not found.";
        }
        else
        {
            context.ProblemDetails.Title = "An unexpected error occurred";
            context.ProblemDetails.Detail = "An unexpected error occurred.Please try again later.";
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination"));
});

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddApplication();
builder.Services.AddDbContext(conString);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks().AddDbContextCheck<WideWorldImportersContext>();

var app = builder.Build();
app.UseCors("CorsPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseMiddleware<HttpOnlyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHealthChecks("/health");
app.Run();
