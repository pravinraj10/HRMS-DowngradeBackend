using System;
using System.Collections.Generic;

namespace GEOMASTER.Models;

public partial class Tblcountry
{
    public int Id { get; set; }

    public string? CountryCode { get; set; }

    public string? CountryName { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDelete { get; set; }

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public virtual ICollection<Tblcity> Tblcities { get; set; } = new List<Tblcity>();

    public virtual ICollection<Tblstate> Tblstates { get; set; } = new List<Tblstate>();
}
