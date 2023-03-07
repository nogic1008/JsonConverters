namespace Nogic.JsonConverters;

/// <summary>Naming policy for kebab-casing.</summary>
public sealed class JsonKebabCaseNamingPolicy : JsonSeparatorNamingPolicy
{
    /// <summary>
    /// Initializes a new instance of <see cref="JsonKebabCaseNamingPolicy"/>
    /// </summary>
    public JsonKebabCaseNamingPolicy() : base(true, '-') { }
}
