using GEOMASTER.DTO.Login;
using GEOMASTER.Interface.Jwt.GEOMASTER.Interface.Auth;
using GEOMASTER.Interface.Login;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;

namespace GEOMASTER.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;
        private readonly IJwtService _jwt;
        private readonly AppDbContext _context;

        public LoginService(
            ILoginRepository repo,
            IJwtService jwt,
            AppDbContext context
        )
        {
            _repo = repo;
            _jwt = jwt;
            _context = context;
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

            var employee = await _context.Tblemployees
                .FirstOrDefaultAsync(x => x.Id == user.EmployeeId);

            return new LoginResponseDTO
            {
                EmployeeId = user.EmployeeId,
                Username = user.Username,
                Token = token,

                FullName = employee?.FullName,
                Email = employee?.PersonalEmail,
                ProfilePhoto = employee?.ProfilePhoto
            };
        }
    }
}