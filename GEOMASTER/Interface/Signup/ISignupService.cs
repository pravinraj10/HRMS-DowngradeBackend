using GEOMASTER.DTO.Signup;

namespace GEOMASTER.Interface.Signup
{
    public interface ISignupService
    {
        Task<string> RegisterAsync(SignupDTO dto);
    }
}
