namespace Nogic.JsonConverters.Test;

/// <summary>
/// Test for <see cref="BlankNullableConverterFactory"/> and <see cref="BlankNullableConverter{T}"/>.
/// </summary>
[TestClass]
public sealed class BlankNullableConverterTest
{
    private static readonly JsonSerializerOptions _options = new()
    {
        Converters = { new BlankNullableConverterFactory() }
    };

    /// <summary>
    /// When <paramref name="type"/> is <see cref="Nullable{T}"/>, <see cref="BlankNullableConverterFactory.CanConvert"/> returns <see langword="true"/>.
    /// </summary>
    /// <param name="type">Type to convert</param>
    [TestMethod]
    [DataRow(typeof(int?))]
    [DataRow(typeof(TypeCode?))]
    [DataRow(typeof(DateTimeOffset?))]
    public void When_Type_Is_NullableStruct_CanConvert_Returns_True(Type type)
        => new BlankNullableConverterFactory().CanConvert(type).ShouldBeTrue();

    /// <summary>
    /// When <paramref name="type"/> is not <see cref="Nullable{T}"/>, <see cref="BlankNullableConverterFactory.CanConvert"/> returns <see langword="false"/>.
    /// </summary>
    /// <param name="type">Type to convert</param>
    [TestMethod]
    [DataRow(typeof(int))]
    [DataRow(typeof(string))]
    [DataRow(typeof(TypeCode))]
    [DataRow(typeof(DateTimeOffset))]
    [DataRow(typeof(Array))]
    [DataRow(typeof(Dictionary<,>))]
    public void When_Type_Is_Not_NullableStruct_CanConvert_Returns_False(Type type)
        => new BlankNullableConverterFactory().CanConvert(type).ShouldBeFalse();

    /// <summary>
    /// <see cref="BlankNullableConverter{T}.Write"/> writes expected JSON string.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="value"/></typeparam>
    /// <param name="value">serialize target</param>
    /// <param name="expectedJson">expected JSON string</param>
    [TestMethod]
    public void CanSerializeJson()
    {
        // int
        JsonSerializer.Serialize((int?)null, _options).ShouldBe("\"\"");
        JsonSerializer.Serialize(1, _options).ShouldBe("1");

        // enum
        JsonSerializer.Serialize((TypeCode?)null, _options).ShouldBe("\"\"");
        JsonSerializer.Serialize((TypeCode?)TypeCode.Decimal, _options).ShouldBe("15");

        // DateTimeOffset
        JsonSerializer.Serialize((DateTimeOffset?)null, _options).ShouldBe("\"\"");
        JsonSerializer.Serialize((DateTimeOffset?)new DateTimeOffset(2022, 1, 26, 10, 0, 27, 0, TimeSpan.Zero), _options).ShouldBe("\"2022-01-26T10:00:27+00:00\"");
    }

    /// <summary>
    /// <see cref="BlankNullableConverter{T}.Read"/> returns expected value.
    /// </summary>
    /// <typeparam name="T">Type of <paramref name="expected"/></typeparam>
    /// <param name="json">JSON string for deserialize</param>
    /// <param name="expected">expected value</param>
    [TestMethod]
    public void CanDeserializeJson()
    {
        // int
        JsonSerializer.Deserialize<int?>("null", _options).ShouldBeNull();
        JsonSerializer.Deserialize<int?>("\"\"", _options).ShouldBeNull();
        JsonSerializer.Deserialize<int?>("1", _options).ShouldBe(1);

        // enum
        JsonSerializer.Deserialize<TypeCode?>("null", _options).ShouldBeNull();
        JsonSerializer.Deserialize<TypeCode?>("\"\"", _options).ShouldBeNull();
        JsonSerializer.Deserialize<TypeCode?>("3", _options).ShouldBe(TypeCode.Boolean);

        // DateTimeOffset
        JsonSerializer.Deserialize<DateTimeOffset?>("null", _options).ShouldBeNull();
        JsonSerializer.Deserialize<DateTimeOffset?>("\"\"", _options).ShouldBeNull();
        JsonSerializer.Deserialize<DateTimeOffset?>("\"2022-01-26T10:00:27+00:00\"", _options).ShouldBe(new DateTimeOffset(2022, 1, 26, 10, 0, 27, 0, TimeSpan.Zero));
    }
}
