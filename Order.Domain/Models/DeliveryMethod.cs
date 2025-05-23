﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Order.Domain.Models;

public partial class DeliveryMethod
{
    public int DeliveryMethodId { get; set; }

    public string DeliveryMethodName { get; set; }

    public int LastEditedBy { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Person LastEditedByNavigation { get; set; }

    public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}