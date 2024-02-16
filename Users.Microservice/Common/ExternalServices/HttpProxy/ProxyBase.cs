namespace Users.Microservice.Common.ExternalServices.HttpProxy;

public class ProxyBase
{
    /// <summary>
    /// Method for sending get request via HttpClient.
    /// </summary>
    /// <param name="requestUrl">Url for sending request.</param>
    /// <param name="headers">Optional headers.</param>
    /// <returns>Response as a string.</returns>
    internal static async Task<string> GetAsync(string requestUrl, List<KeyValuePair<string, string>>? headers = null)
    {
        using var client = new HttpClient();

        if (headers != null && headers.Any())
        {
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var response = await client.GetAsync(requestUrl);

        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// Method for sending post as string request via HttpClient.
    /// </summary>
    /// <param name="requestUrl">Url for sending request.</param>
    /// <param name="content">Content to post as string content</param>
    /// <param name="headers">Optional headers.</param>
    /// <returns>Response as a string</returns>
    internal static async Task<string> PostAsStringAsync(string requestUrl,
        StringContent content, List<KeyValuePair<string, string>>? headers = null)
    {
        using var client = new HttpClient();

        if (headers != null && headers.Any())
        {
            foreach (var header in headers)
            {
                client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
        }

        var response = await client.PostAsync(requestUrl, content);

        return await response.Content.ReadAsStringAsync();
    }
}