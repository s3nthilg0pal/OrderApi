﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Order.Domain.Models;

public partial class StockItemStockGroup
{
    public int StockItemStockGroupId { get; set; }

    public int StockItemId { get; set; }

    public int StockGroupId { get; set; }

    public int LastEditedBy { get; set; }

    public DateTime LastEditedWhen { get; set; }

    public virtual Person LastEditedByNavigation { get; set; }

    public virtual StockGroup StockGroup { get; set; }

    public virtual StockItem StockItem { get; set; }
}