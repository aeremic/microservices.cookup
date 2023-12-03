namespace Users.Microservice.Common;

public static class Constants
{
    public enum Role : int
    {
        Administrator = 0,
        Regular
    }

    public static class AuthConfigurationSectionKeys
    {
        public static readonly string AuthenticationGoogle = "Authentication:Google";
        public static readonly string ClientId = "ClientId";
    }
    
    public static class JwtConfigurationSectionKeys
    {
        public static readonly string Jwt = "Jwt";
        public static readonly string SecurityKey = "SecurityKey";
        public static readonly string ValidIssuer = "ValidIssuer";
        public static readonly string ValidAudience = "ValidAudience";
        public static readonly string ExpiryInMinutes = "ExpiryInMinutes";
    }
}