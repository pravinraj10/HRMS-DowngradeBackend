using System;
using System.Collections.Generic;

namespace GEOMASTER.Models;

    public partial class Tblcity
    {
        public int Id { get; set; }

        public int? CountryId { get; set; }

        public int? StateId { get; set; }

        public string? CityName { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

    public virtual Tblcountry? Country { get; set; }

    public virtual Tblstate? State { get; set; }
}
