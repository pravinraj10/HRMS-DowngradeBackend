namespace GEOMASTER.DTO.Employee
{
    public class EmployeeResponseDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string? PersonalEmail { get; set; }
        public string? PersonalPhone { get; set; }
        public string? EmployeeCode { get; set; }
        public string? ProfilePhoto { get; set; }
        public bool IsActive { get; set; }
    }
}
