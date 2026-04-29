
using BCrypt.Net;
using GEOMASTER.DTO.Signup;

using GEOMASTER.Interface.Signup;
using GEOMASTER.Models;
namespace GEOMASTER.Service
{

    public class SignupService : ISignupService
    {
        private readonly ISignupRepository _repo;

        public SignupService(ISignupRepository repo)
        {
            _repo = repo;
        }

        public async Task<string> RegisterAsync(SignupDTO dto)
        {
            var existingUser = await _repo.GetByEmailAsync(dto.Email);

            if (existingUser != null)
                return "User already exists";
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new Tblsignup
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = hashedPassword,
                Role = dto.Role
            };

            await _repo.AddAsync(user);

            return "Signup successful";
        }
    }
}