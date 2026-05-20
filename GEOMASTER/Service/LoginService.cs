using GEOMASTER.DTO.Login;
using GEOMASTER.Interface.Email;
using GEOMASTER.Interface.Jwt.GEOMASTER.Interface.Auth;
using GEOMASTER.Interface.Login;
using GEOMASTER.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace GEOMASTER.Service
{
    public class LoginService : ILoginService
    {
        private readonly ILoginRepository _repo;
        private readonly IJwtService _jwt;
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public LoginService(
            ILoginRepository repo,
            IJwtService jwt,
            AppDbContext context,
            IEmailService emailService
        )
        {
            _repo = repo;
            _jwt = jwt;
            _context = context;
            _emailService = emailService;
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

            var employee = await _context.Tblemployees
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Id == user.EmployeeId);

            string role = employee?.Role?.RoleName ?? "User";
            string permissions = employee?.Role?.SideMenu ?? "[]";

            var token = _jwt.GenerateToken(user.EmployeeId, user.Username, role, permissions);

            return new LoginResponseDTO
            {
                EmployeeId = user.EmployeeId,
                Username = user.Username,
                Token = token,

                FullName = employee?.FullName,
                Email = employee?.PersonalEmail,
                ProfilePhoto = employee?.ProfilePhoto,

                RoleName = role,
                SideMenu = permissions
            };
        }
        public async Task ForgotPassword(ForgotPasswordDTO dto)
        {
            var login = await _repo.GetByEmail(dto.Email);

            if (login == null)
                throw new Exception("Email not found");

            var token = Guid.NewGuid().ToString();

            login.PasswordResetToken = token;

            login.PasswordResetTokenExpiry =
                DateTime.UtcNow.AddMinutes(30);

            await _repo.Update(login);

            var resetLink =
                $"http://localhost:3000/login?page=resetPassword&token={token}";

            var body = $@"
               <h3>Password Reset</h3>
               <p>Click below link to reset password:</p>
               <a href='{resetLink}'>Reset Password</a>
            ";

            await _emailService.SendEmailAsync(
                dto.Email,
                "Reset Password",
                body
            );
        }
        public async Task ResetPassword(ResetPasswordDTO dto)
        {
            if (dto.Password != dto.ConfirmPassword)
                throw new Exception("Passwords do not match");

            ValidatePasswordStrength(dto.Password);

            var login =
                await _repo.GetByResetToken(dto.Token);

            if (login == null)
                throw new Exception("Invalid token");

            if (login.PasswordResetTokenExpiry < DateTime.UtcNow)
                throw new Exception("Token expired");

            login.PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(dto.Password);

            login.PasswordResetToken = null;

            login.PasswordResetTokenExpiry = null;

            await _repo.Update(login);
        }
        private void ValidatePasswordStrength(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (password.Length < 8)
                throw new Exception(
                    "Password must be at least 8 characters long");

            if (!password.Any(char.IsUpper))
                throw new Exception(
                    "Password must contain at least one uppercase letter");

            if (!password.Any(char.IsLower))
                throw new Exception(
                    "Password must contain at least one lowercase letter");
            if (!password.Any(char.IsDigit))
                throw new Exception(
                    "Password must contain at least one number");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new Exception(
                    "Password must contain at least one special character");
        }
    }
}