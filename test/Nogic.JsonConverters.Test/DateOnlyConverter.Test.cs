using System.Globalization;

namespace Nogic.JsonConverters.Test;

/// <summary>Unit test of <see cref="DateOnlyConverter"/></summary>
[TestClass]
public sealed class DateOnlyConverterTest
{
    /// <summary>
    /// Create <see cref="JsonSerializerOptions"/> that contains <see cref="DateOnlyConverter"/>.
    /// </summary>
    /// <param name="format">
    /// <inheritdoc cref="DateOnlyConverter(string, IFormatProvider?)" path="/param[@name='serializationFormat']"/>
    /// </param>
    private static JsonSerializerOptions CreateOption(string format)
#if NET7_0_OR_GREATER
#pragma warning disable CS0618
#endif
        => new() { Converters = { new DateOnlyConverter(format, CultureInfo.InvariantCulture) } };
#if NET7_0_OR_GREATER
#pragma warning restore CS0618
#endif

    /// <summary>
    /// <see cref="DateOnlyConverter.Read"/> returns expected <see cref="DateOnly"/> object.
    /// </summary>
    /// <inheritdoc cref="CreateOption" path="/param[@name='format']"/>
    /// <param name="json">JSON string</param>
    /// <inheritdoc cref="DateOnly(int, int, int)" path="/param[@name='year']"/>
    /// <inheritdoc cref="DateOnly(int, int, int)" path="/param[@name='month']"/>
    /// <inheritdoc cref="DateOnly(int, int, int)" path="/param[@name='day']"/>
    [TestMethod]
    [DataRow("yyyy-MM-dd", "\"0001-01-01\"", 1, 1, 1)]
    [DataRow("yy/MM/dd", "\"22/01/01\"", 2022, 1, 1)]
    [DataRow("MM/dd/yyyy g", "\"12/31/9999 A.D.\"", 9999, 12, 31)]
    public void CanDeserializeJson(string format, string json, int year, int month, int day)
        => JsonSerializer.Deserialize<DateOnly>(json, CreateOption(format)).Should().Be(new(year, month, day));

    /// <summary>
    /// <see cref="DateOnlyConverter.Read"/> throws error.
    /// </summary>
    /// <inheritdoc cref="CreateOption" path="/param[@name='format']"/>
    /// <param name="json">JSON string</param>
    [TestMethod]
    [DataRow("yyyy-MM-dd", "\"2020/01/01\"")]
    [DataRow("yyyy/MM/dd", "\"2023/10/10 0:00:00.000+09:00\"")]
    [DataRow("MM/dd/yyyy g", "\"0001-01-01\"")]
    public void CannotDeserializeJson(string format, string json)
    {
        // Arrange - Act
        var action = () => JsonSerializer.Deserialize<DateOnly>(json, CreateOption(format));

        // Assert
        _ = action.Should().Throw<FormatException>();
    }

    /// <summary>
    /// <see cref="DateOnlyConverter.Write"/> writes <paramref name="expected"/>.
    /// </summary>
    /// <inheritdoc cref="CreateOption" path="/param[@name='format']"/>
    /// <inheritdoc cref="DateOnly(int, int, int)" path="/param[@name='year']"/>
    /// <inheritdoc cref="DateOnly(int, int, int)" path="/param[@name='month']"/>
    /// <inheritdoc cref="DateOnly(int, int, int)" path="/param[@name='day']"/>
    /// <param name="expected">Expected JSON string</param>
    [TestMethod]
    [DataRow("yyyy-MM-dd", 1, 1, 1, "\"0001-01-01\"")]
    [DataRow("yy/MM/dd", 2022, 1, 1, "\"22/01/01\"")]
    [DataRow("MM/dd/yyyy g", 9999, 12, 31, "\"12/31/9999 A.D.\"")]
    public void CanSerializeJson(string format, int year, int month, int day, string expected)
        => JsonSerializer.Serialize(new DateOnly(year, month, day), CreateOption(format)).Should().Be(expected);
}
