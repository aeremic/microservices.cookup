namespace Recipes.Microservice.Common;

public static class Constants
{
    public static class JwtConfigurationSectionKeys
    {
        public static readonly string Jwt = "Jwt";
        public static readonly string SecurityKey = "SecurityKey";
        public static readonly string ValidIssuer = "ValidIssuer";
        public static readonly string ValidAudience = "ValidAudience";
        public static readonly string ExpiryInMinutes = "ExpiryInMinutes";
    }
}