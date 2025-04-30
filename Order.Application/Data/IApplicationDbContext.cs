using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;

namespace Order.Application.Data;

public interface IApplicationDbContext
{
    DbSet<City> Cities { get; }
    DbSet<Country> Countries { get; }
    DbSet<DeliveryMethod> DeliveryMethods { get; }
    DbSet<PaymentMethod> PaymentMethods { get; }
    DbSet<Person> People { get; }
    DbSet<StateProvince> StateProvinces { get; }
    DbSet<SystemParameter> SystemParameters { get; }
    DbSet<TransactionType> TransactionTypes { get; }
}