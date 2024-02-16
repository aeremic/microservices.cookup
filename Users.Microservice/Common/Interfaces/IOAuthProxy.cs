using Users.Microservice.Common.ExternalServices.GoogleGate.GateModels;

namespace Users.Microservice.Common.Interfaces;

public interface IOAuthProxy
{
    public Task<AccessCodeDto?> ProcessGetAccessCodeAsync(string apiUrl, string endpoint,
        string clientId, string clientSecret, string redirectUri, string authorizationCode);

    public Task<UserInfoDto?> ProcessGetUserInfoAsync(string apiUrl, string endpoint, string accessToken);
}