namespace GEOMASTER.Models
{
    public class TblLogin
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Tblemployee Employee { get; set; }
    }
}