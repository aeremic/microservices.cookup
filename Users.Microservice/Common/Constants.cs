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
        public const string AuthenticationGoogle = "Authentication:Google";
        public const string GoogleApisBaseUrl = "GoogleApisBaseUrl";
        public const string AccountsBaseUrl = "AccountsBaseUrl";
        public const string TokenEndpoint = "TokenEndpoint";
        public const string UserInfoEndpoint = "UserInfoEndpoint";
        public const string ClientId = "ClientId";
        public const string ClientSecret = "ClientSecret";
        public const string RedirectUri = "RedirectUri";
    }
    
    public static class JwtConfigurationSectionKeys
    {
        public const string Jwt = "Jwt";
        public const string SecurityKey = "SecurityKey";
        public const string ValidIssuer = "ValidIssuer";
        public const string ValidAudience = "ValidAudience";
        public const string ExpiryInMinutes = "ExpiryInMinutes";
    }

    public static class Authorization
    {
        public const string AuthorizationHeader = "Authorization";
        public const string AuthorizationCodeGrantType = "authorization_code";
    }

    public static class ContentTypes
    {
        public const string ApplicationXWwwFormUrlencoded = "application/x-www-form-urlencoded";
    }
}