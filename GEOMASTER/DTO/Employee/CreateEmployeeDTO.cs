namespace GEOMASTER.DTO.Employee
{
    public class CreateEmployeeDTO
    {
        public string FullName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? PersonalEmail { get; set; }
        public string? PersonalPhone { get; set; }
        public string? EmergencyContact { get; set; }
        public string? Address { get; set; }
        public int? DepartmentId { get; set; }
        public int? DesignationId { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? EmployeeCode { get; set; }
        public int? ReportingManagerId { get; set; }
        public string? Shift { get; set; }
        public string? CreatedBy { get; set; }
        //  FILES
        public IFormFile? ProfilePhoto { get; set; }
        public IFormFile? IdProof { get; set; }

        //pasword
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
