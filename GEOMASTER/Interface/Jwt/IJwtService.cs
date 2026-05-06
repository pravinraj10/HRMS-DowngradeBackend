namespace GEOMASTER.Interface.Jwt
{
    namespace GEOMASTER.Interface.Auth
    {
        public interface IJwtService
        {
            string GenerateToken(int userId, string username, string role);
        }
    }
}
