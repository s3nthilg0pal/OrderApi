namespace Order.Application.Sales.Orders;

public record OrderDetail(string CustomerName, string[] OrderLineDescription, int[] InvoiceNumber, DateOnly ExpectedDeliveryDate, DateOnly OrderDate);