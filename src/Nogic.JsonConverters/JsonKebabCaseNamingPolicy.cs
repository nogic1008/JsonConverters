namespace Nogic.JsonConverters;

/// <summary>Naming policy for kebab-casing.</summary>
#if NET8_0_OR_GREATER
[Obsolete($"Use built-in {nameof(JsonNamingPolicy)}.{nameof(KebabCaseLower)}.")]
#endif
public sealed class JsonKebabCaseNamingPolicy() : JsonSeparatorNamingPolicy(true, '-');
