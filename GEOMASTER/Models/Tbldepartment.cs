using GEOMASTER.Models.GEOMASTER.Models;

namespace GEOMASTER.Models
{
    public class Tbldepartment
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDelete { get; set; } = false;

        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public virtual ICollection<Tbldesignation> Tbldesignations { get; set; } = new List<Tbldesignation>();
        public virtual ICollection<Tblrole> Tblroles { get; set; } = new List<Tblrole>();
        public virtual ICollection<Tblemployee> Tblemployees { get; set; } = new List<Tblemployee>();
    }
}
