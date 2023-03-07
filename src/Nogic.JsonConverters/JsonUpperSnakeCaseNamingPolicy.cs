namespace Nogic.JsonConverters;

/// <summary>Naming policy for UPPER_SNAKE_CASING.</summary>
public sealed class JsonUpperSnakeCaseNamingPolicy : JsonSeparatorNamingPolicy
{
    /// <summary>
    /// Initializes a new instance of <see cref="JsonUpperSnakeCaseNamingPolicy"/>
    /// </summary>
    public JsonUpperSnakeCaseNamingPolicy() : base(false, '_') { }
}
