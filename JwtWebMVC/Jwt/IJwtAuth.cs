namespace JwtWebMVC.Jwt
{
    public interface IJwtAuth
    {
        string Authentication(string username, string password);
    }
}
