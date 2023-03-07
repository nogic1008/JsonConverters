namespace Nogic.JsonConverters;

/// <summary>Naming policy for lower_snake_casing.</summary>
public sealed class JsonLowerSnakeCaseNamingPolicy : JsonSeparatorNamingPolicy
{
    /// <summary>
    /// Initializes a new instance of <see cref="JsonLowerSnakeCaseNamingPolicy"/>
    /// </summary>
    public JsonLowerSnakeCaseNamingPolicy() : base(true, '_') { }
}
