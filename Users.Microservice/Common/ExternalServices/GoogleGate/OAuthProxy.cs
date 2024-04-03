using System.Text;
using Newtonsoft.Json;
using Users.Microservice.Common.ExternalServices.GoogleGate.GateModels;
using Users.Microservice.Common.ExternalServices.HttpProxy;
using Users.Microservice.Common.Interfaces;

namespace Users.Microservice.Common.ExternalServices.GoogleGate;

public class OAuthProxy : ProxyBase, IOAuthProxy
{
    #region Public methods

    public async Task<AccessCodeDto?> ProcessGetAccessCodeAsync(string apiUrl, string endpoint,
        string clientId, string clientSecret, string redirectUri, string authorizationCode)
    {
        AccessCodeDto? result = null;
        var responseString = await PostAsStringAsync(
            FormGetAccessCodeRequestUrl(apiUrl, endpoint),
            new StringContent(
                BuildGetAccessCodePostModel(authorizationCode, clientId, clientSecret, redirectUri,
                    Constants.Authorization.AuthorizationCodeGrantType),
                Encoding.UTF8, Constants.ContentTypes.ApplicationXWwwFormUrlencoded));

        if (!string.IsNullOrEmpty(responseString))
        {
            result = JsonConvert.DeserializeObject<AccessCodeDto>(responseString);
        }

        return result;
    }

    public async Task<UserInfoDto?> ProcessGetUserInfoAsync(string apiUrl, string endpoint, string accessToken)
    {
        UserInfoDto? result = null;

        var responseString = await GetAsync(FormGetAccessCodeRequestUrl(apiUrl, endpoint),
            new List<KeyValuePair<string, string>>
            {
                new(Constants.Authorization.AuthorizationHeader, $"Bearer {accessToken}")
            });

        if (!string.IsNullOrEmpty(responseString))
        {
            result = JsonConvert.DeserializeObject<UserInfoDto>(responseString);
        }

        return result;
    }

    #endregion

    #region Private methods

    private static string FormGetAccessCodeRequestUrl(string apiUrl, string endpoint) => $"{apiUrl}/{endpoint}";

    private static string BuildGetAccessCodePostModel(string authorizationCode, string clientId, string clientSecret,
        string redirectUri, string grantType)
        =>
            $"code={authorizationCode}&client_id={clientId}&client_secret={clientSecret}" +
            $"&redirect_uri={redirectUri}&grant_type={grantType}";

    #endregion
}