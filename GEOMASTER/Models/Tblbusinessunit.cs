using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GEOMASTER.Models
{
    public partial class Tblbusinessunit
    {
        public int Id { get; set; }
        public string? UnitCode { get; set; }
        public string? UnitName { get; set; }
        public bool? IsDelete { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
