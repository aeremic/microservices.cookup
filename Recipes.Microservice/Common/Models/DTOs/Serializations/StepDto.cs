using Newtonsoft.Json;

namespace Recipes.Microservice.Common.Models.DTOs.Serializations;

/// <summary>
/// Partial class used for serialization domain model instructions.
/// </summary>
public sealed partial class StepDto
{
    [JsonProperty("Value", NullValueHandling = NullValueHandling.Ignore)]
    public string? Value { get; set; }
}