using Order.Application.Common;
using Order.Domain.Models;
using Riok.Mapperly.Abstractions;

namespace Order.Api.Mappers;
public class CustomerDto
{
    public string CustomerName { get; set; } = null!;
}