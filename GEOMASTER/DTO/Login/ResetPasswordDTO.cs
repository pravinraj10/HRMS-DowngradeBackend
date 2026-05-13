namespace GEOMASTER.DTO.Login
{
    public class ResetPasswordDTO
    {
        public string? Token { get; set; }

        public string? Password { get; set; }

        public string? ConfirmPassword { get; set; }
    }
}
