using Microsoft.EntityFrameworkCore;
using Order.Domain.Models;

namespace Order.Application.Data
{
    public interface IWideWorldImportersContext
    {
        DbSet<City> Cities { get; }
        DbSet<BuyingGroup> BuyingGroups { get; set; }
        DbSet<ColdRoomTemperature> ColdRoomTemperatures { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<Customer> Customers { get; set; }
        DbSet<CustomerCategory> CustomerCategories { get; set; }
        DbSet<CustomerTransaction> CustomerTransactions { get; set; }
        DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<InvoiceLine> InvoiceLines { get; set; }
        DbSet<Domain.Models.Order> Orders { get; set; }
        DbSet<OrderLine> OrderLines { get; set; }
        DbSet<PackageType> PackageTypes { get; set; }
        DbSet<PaymentMethod> PaymentMethods { get; set; }
        DbSet<Person> People { get; set; }
        DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        DbSet<PurchaseOrderLine> PurchaseOrderLines { get; set; }
        DbSet<SpecialDeal> SpecialDeals { get; set; }
        DbSet<StateProvince> StateProvinces { get; set; }
        DbSet<StockGroup> StockGroups { get; set; }
        DbSet<StockItem> StockItems { get; set; }
        DbSet<StockItemHolding> StockItemHoldings { get; set; }
        DbSet<StockItemStockGroup> StockItemStockGroups { get; set; }
        DbSet<StockItemTransaction> StockItemTransactions { get; set; }
        DbSet<Supplier> Suppliers { get; set; }
        DbSet<SupplierCategory> SupplierCategories { get; set; }
        DbSet<SupplierTransaction> SupplierTransactions { get; set; }
        DbSet<SystemParameter> SystemParameters { get; set; }
        DbSet<TransactionType> TransactionTypes { get; set; }
        DbSet<VehicleTemperature> VehicleTemperatures { get; set; }
    }
}
