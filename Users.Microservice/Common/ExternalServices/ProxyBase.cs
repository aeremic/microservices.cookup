namespace Users.Microservice.Common.ExternalServices;

public class ProxyBase
{
    /// <summary>
    /// Method for sending get request via HttpClient.
    /// </summary>
    /// <param name="requestUrl">Url for sending request.</param>
    /// <returns>Response as a string</returns>
    internal static async Task<string> GetAsync(string requestUrl)
    {
        using var client = new HttpClient();

        var response = await client.GetAsync(requestUrl);

        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Method for sending post request via HttpClient.
    /// </summary>
    /// <param name="requestUrl">Url for sending request.</param>
    /// <param name="content">Content to post</param>
    /// <returns>Response as a string</returns>
    internal static async Task<string> PostAsync(string requestUrl, FormUrlEncodedContent content)
    {
        using var client = new HttpClient();
        
        var response = await client.PostAsync(requestUrl, content);
        
        return await response.Content.ReadAsStringAsync();
    }
}