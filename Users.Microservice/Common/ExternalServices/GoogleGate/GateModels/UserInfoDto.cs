using Newtonsoft.Json;

namespace Users.Microservice.Common.ExternalServices.GoogleGate.GateModels;

public sealed class UserInfoDto
{
    [JsonProperty("sub", NullValueHandling = NullValueHandling.Ignore)]
    public string? Sub { get; set; }

    [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
    public string? Name { get; set; }

    [JsonProperty("given_name", NullValueHandling = NullValueHandling.Ignore)]
    public string? GivenName { get; set; }

    [JsonProperty("picture", NullValueHandling = NullValueHandling.Ignore)]
    public Uri? Picture { get; set; }

    [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
    public string? Email { get; set; }

    [JsonProperty("email_verified", NullValueHandling = NullValueHandling.Ignore)]
    public bool? EmailVerified { get; set; }

    [JsonProperty("locale", NullValueHandling = NullValueHandling.Ignore)]
    public string? Locale { get; set; }
}