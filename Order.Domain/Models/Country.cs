﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Order.Domain.Models;

public partial class Country
{
    public int CountryId { get; set; }

    public string CountryName { get; set; }

    public string FormalName { get; set; }

    public string IsoAlpha3Code { get; set; }

    public int? IsoNumericCode { get; set; }

    public string CountryType { get; set; }

    public long? LatestRecordedPopulation { get; set; }

    public string Continent { get; set; }

    public string Region { get; set; }

    public string Subregion { get; set; }

    public int LastEditedBy { get; set; }

    public virtual Person LastEditedByNavigation { get; set; }

    public virtual ICollection<StateProvince> StateProvinces { get; set; } = new List<StateProvince>();
}