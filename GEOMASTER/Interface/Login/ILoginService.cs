using GEOMASTER.DTO.Login;

namespace GEOMASTER.Interface.Login
{
    public interface ILoginService
    {
        Task<LoginResponseDTO> Login(LoginRequestDTO dto);
    }
}