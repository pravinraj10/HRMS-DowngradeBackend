using GEOMASTER.DTO.Login;
using GEOMASTER.Interface.Jwt.GEOMASTER.Interface.Auth;
using GEOMASTER.Interface.Login;

namespace GEOMASTER.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;
        private readonly IJwtService _jwt;

        public LoginService(ILoginRepository repo, IJwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                throw new Exception("Username and Password are required");

            var user = await _repo.GetByUsername(dto.Username);

            if (user == null)
                throw new Exception("Invalid username");

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                throw new Exception("Invalid password");

            // TODO: Fetch role from DB
            string role = "User";

            var token = _jwt.GenerateToken(user.EmployeeId, user.Username, role);

            return new LoginResponseDTO
            {
                EmployeeId = user.EmployeeId,
                Username = user.Username,
                Token = token
            };
        }
    }
}