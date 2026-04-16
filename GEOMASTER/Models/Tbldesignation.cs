namespace GEOMASTER.Models
{
    public partial class Tbldesignation
    {
        public int Id { get; set; }

        public string DesignationName { get; set; } = null!;

        public int DepartmentId { get; set; }

        public string? Description { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public string? CreatedBy { get; set; }

        public string? UpdatedBy { get; set; }

        //  Navigation Property
        public virtual Tbldepartment? Department { get; set; }
        public virtual ICollection<Tblemployee> Tblemployees { get; set; } = new List<Tblemployee>();
    }
}
