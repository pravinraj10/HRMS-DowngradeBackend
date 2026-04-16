using System;
using System.Collections.Generic;

namespace GEOMASTER.Models;

    public class Tblstate
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string? StateCode { get; set; }
        public string? StateName { get; set; }

        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }

        // Audit Fields
        public DateTime? CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }

    public virtual Tblcountry? Country { get; set; }

    public virtual ICollection<Tblcity> Tblcities { get; set; } = new List<Tblcity>();
}
