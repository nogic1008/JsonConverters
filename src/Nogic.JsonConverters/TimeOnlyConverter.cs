namespace Nogic.JsonConverters;

/// <summary>Json converter for <see cref="TimeOnly"/>.</summary>
/// <param name="serializationFormat">A standard or custom time format string.</param>
/// <param name="provider">An object that supplies culture-specific formatting information.</param>
#if NET7_0_OR_GREATER
[Obsolete("Use internal System.Text.Json.Serialization.Converters.DateOnlyConverter instead.")]
#endif
public class TimeOnlyConverter(string serializationFormat = "HH:mm:ss.fff", IFormatProvider? provider = null) : JsonConverter<TimeOnly>
{
    /// <inheritdoc/>
    public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => TimeOnly.ParseExact(reader.GetString()!, serializationFormat, provider);

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
        => writer.WriteStringValue(value.ToString(serializationFormat, provider));
}
