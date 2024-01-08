using Newtonsoft.Json;

namespace Users.Microservice.Common.ExternalServices.GoogleGate;

public class OAuthProxy : ProxyBase
{
    public static async Task<AccessCodeDto?> ProcessGetAccessCodeAsync(string apiUrl, string endpoint,
        string clientId, string secret, string redirectUri, string authorizationCode)
    {
        AccessCodeDto? result = null;

        var requestUrl = FormGetAccessCodeRequestUrl(apiUrl, endpoint);
        var responseString = await PostAsync(requestUrl, new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("code", authorizationCode),
            new KeyValuePair<string, string>("clientId", clientId),
            new KeyValuePair<string, string>("secret", secret),
            new KeyValuePair<string, string>("redirectUri", redirectUri)
        }));

        if (!string.IsNullOrEmpty(responseString))
        {
            result = JsonConvert.DeserializeObject<AccessCodeDto>(responseString);
        }

        return result;
    }

    private static string FormGetAccessCodeRequestUrl(string apiUrl, string endpoint)
    {
        return $"{apiUrl}/{endpoint}";
    }
}