using GEOMASTER.DTO.Login;

namespace GEOMASTER.Interface.Login
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO dto);
        Task ForgotPassword(ForgotPasswordDTO dto);

        Task ResetPassword(ResetPasswordDTO dto);
    }
}