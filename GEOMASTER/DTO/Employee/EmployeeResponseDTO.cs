public class EmployeeResponseDTO
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
    public string? CountryName { get; set; }
    public string? StateName { get; set; }
    public string? CityName { get; set; }
    public int? DepartmentId { get; set; }
    public string? DepartmentName { get; set; }
    public int? DesignationId { get; set; }
    public string? DesignationName { get; set; }
    public DateTime? JoiningDate { get; set; }
    public string? EmployeeCode { get; set; }
    public int? ReportingManagerId { get; set; }
    public string? ReportingManagerName { get; set; }
    public string? Shift { get; set; }
    public string? ProfilePhoto { get; set; } //  string path
    public string? IdProof { get; set; }      //  string path
    public bool IsActive { get; set; }
    public string? CreatedBy { get; set; }
}