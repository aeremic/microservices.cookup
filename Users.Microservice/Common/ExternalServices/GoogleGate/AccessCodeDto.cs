using Newtonsoft.Json;

namespace Users.Microservice.Common.ExternalServices.GoogleGate;

public sealed class AccessCodeDto
{
    [JsonProperty("access_token", NullValueHandling = NullValueHandling.Ignore)]
    public string? AccessToken { get; set; }

    [JsonProperty("token_type", NullValueHandling = NullValueHandling.Ignore)]
    public string? TokenType { get; set; }

    [JsonProperty("expires_in", NullValueHandling = NullValueHandling.Ignore)]
    public long? ExpiresIn { get; set; }

    [JsonProperty("refresh_token", NullValueHandling = NullValueHandling.Ignore)]
    public string? RefreshToken { get; set; }
}