
namespace GEOMASTER.Models
{
    public class Tblemployee
    {
        public int Id { get; set; }

        public string? FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public string? PersonalEmail { get; set; }
        public string? OfficeEmail { get; set; }
        public string? PersonalPhone { get; set; }
        public string? EmergencyContact { get; set; }
        public string? Address { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CityId { get; set; }
        public virtual Tblcountry? Country { get; set; }
        public virtual Tblstate? State { get; set; }
        public virtual Tblcity? City { get; set; }

        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }

        public virtual Tbldepartment? Department { get; set; }
        public virtual Tbldesignation? Designation { get; set; }

        public DateTime? JoiningDate { get; set; }
        public string? EmployeeCode { get; set; }

        // FK to tblroles.id
        public int? ReportingManagerId { get; set; }

        // Navigation property
        public virtual Tblemployee? ReportingManager { get; set; }

        public virtual ICollection<Tblemployee>
    Subordinates
        { get; set; }
    = new List<Tblemployee>();

        // Proper Role Mapping
        public int? RoleId { get; set; }
        public virtual Tblrole? Role { get; set; }

        public string? Shift { get; set; }

        public string? ProfilePhoto { get; set; }
        public string? IdProof { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
    }
}           