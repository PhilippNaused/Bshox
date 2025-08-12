using System.Text.Json.Serialization;
using Bshox.Attributes;
using Bshox.TestUtils;
using Google.Protobuf.WellKnownTypes;
using MessagePack;
using ProtoBuf;
using ProtoBuf.Meta;

namespace Benchmark.Models;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CA2227 // Collection properties should be read only
#pragma warning disable CA1819 // Properties should not return arrays

[BshoxSerializer(typeof(Forecast))]
internal partial class ForecastSerializer;

[JsonSerializable(typeof(Forecast))]
[JsonSourceGenerationOptions]
internal sealed partial class ForecastJsonContext : JsonSerializerContext;

[GeneratedMessagePackResolver]
internal partial class MyMessagePackResolver;

/// <summary>
/// <see href="https://github.com/open-meteo/open-meteo/blob/82c087f87fbc91d63683fa5c9c6a78b1cf70f31b/openapi.yml"/>
/// </summary>
[MessagePackObject]
[BshoxContract]
[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public sealed record Forecast
{
    [BshoxMember(1), Key(1)]
    public float Latitude { get; set; }

    [BshoxMember(2), Key(2)]
    public float Longitude { get; set; }

    [BshoxMember(3), Key(3)]
    public float Elevation { get; set; }

    [BshoxMember(4), Key(4)]
    public double GenerationTime { get; set; }

    [BshoxMember(5), Key(5)]
    public int UtcOffsetSeconds { get; set; }

    [BshoxMember(6), Key(6)]
    public HourlyResponse Hourly { get; set; }

    [BshoxMember(7), Key(7)]
    public Dictionary<string, string> HourlyUnits { get; set; }

    [BshoxMember(8), Key(8)]
    public CurrentWeather CurrentWeather { get; set; }

    public static Forecast GetRandom(int size = 7 * 24)
    {
        var random = new Random(42);
        return new Forecast
        {
            Latitude = random.NextSingle(-90f, 90f),
            Longitude = random.NextSingle(-180f, 180f),
            Elevation = random.NextSingle(0f, 1000f),
            GenerationTime = random.NextDouble(0d, 10d),
            UtcOffsetSeconds = random.Next(-11, 14) * 3600,
            Hourly = new HourlyResponse
            {
                Time = random.NextArray(size),
                Temperature = random.NextArray(size, -20f, 40f),
                Precipitation = random.NextArray(size, 0f, 10f),
                WeatherCode = Enumerable.Range(0, size).Select(_ => random.NextEnum<WeatherCode>()).ToArray()
            },
            HourlyUnits = new Dictionary<string, string>
            {
                {"Time", "unixtime"},
                {"Temperature", "°C"},
                {"Precipitation", "mm"},
                {"WeatherCode", "wmo code"}
            },
            CurrentWeather = new CurrentWeather
            {
                Time = random.NextDateTime(),
                Temperature = random.NextSingle(-20f, 40f),
                WindSpeed = random.NextSingle(0f, 100f),
                WindDirection = random.NextSingle(0f, 360f),
                WeatherCode = random.NextEnum<WeatherCode>()
            }
        };
    }

    public static Forecast2 GetRandom2(int size = 7 * 24)
    {
        var random = new Random(42);
        return new Forecast2
        {
            Latitude = random.NextSingle(-90f, 90f),
            Longitude = random.NextSingle(-180f, 180f),
            Elevation = random.NextSingle(0f, 1000f),
            GenerationTime = random.NextDouble(0d, 10d),
            UtcOffsetSeconds = random.Next(-11, 14) * 3600,
            Hourly = new HourlyResponse2
            {
                Time = { random.NextArray(size).Select(Timestamp.FromDateTime) },
                Temperature = { random.NextArray(size, -20f, 40f) },
                Precipitation = { random.NextArray(size, 0f, 10f) },
                WeatherCode = { Enumerable.Range(0, size).Select(_ => random.NextEnum<WeatherCode2>()) }
            },
            HourlyUnits =
            {
                {"Time", "unixtime"},
                {"Temperature", "°C"},
                {"Precipitation", "mm"},
                {"WeatherCode", "wmo code"}
            },
            CurrentWeather = new CurrentWeather2
            {
                Time = Timestamp.FromDateTime(random.NextDateTime()),
                Temperature = random.NextSingle(-20f, 40f),
                WindSpeed = random.NextSingle(0f, 100f),
                WindDirection = random.NextSingle(0f, 360f),
                WeatherCode = random.NextEnum<WeatherCode2>()
            }
        };
    }

    public static TypeModel GetProtoModel()
    {
        var model = RuntimeTypeModel.Create();
        model.DefaultCompatibilityLevel = CompatibilityLevel.Level300;
        model.UseImplicitZeroDefaults = false;
        model.AllowPackedEncodingAtRoot = true;
        _ = model.Add(typeof(WeatherCode));
        _ = model.Add(typeof(Forecast));
        model.AutoAddMissingTypes = true;
        return model.Compile();
    }
}

[MessagePackObject]
[BshoxContract]
[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public sealed record HourlyResponse
{
    [BshoxMember(1), Key(1), ProtoMember(1)]
    public DateTime[] Time { get; set; }

    [BshoxMember(2), Key(2), ProtoMember(2, IsPacked = true)]
    public float[] Temperature { get; set; }

    [BshoxMember(3), Key(3), ProtoMember(3, IsPacked = true)]
    public float[] Precipitation { get; set; }

    [BshoxMember(4), Key(4), ProtoMember(4, IsPacked = true)]
    public WeatherCode[] WeatherCode { get; set; }
}

[MessagePackObject]
[BshoxContract]
[ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
public sealed record CurrentWeather
{
    [BshoxMember(1), Key(1)]
    public DateTime Time { get; set; }

    [BshoxMember(2), Key(2)]
    public float Temperature { get; set; }

    [BshoxMember(3), Key(3)]
    public float WindSpeed { get; set; }

    [BshoxMember(4), Key(4)]
    public float WindDirection { get; set; }

    [BshoxMember(5), Key(5)]
    public WeatherCode WeatherCode { get; set; }
}

/*
    https://open-meteo.com/en/docs
    Code	    Description
    0	        Clear sky
    1, 2, 3	    Mainly clear, partly cloudy, and overcast
    45, 48	    Fog and depositing rime fog
    51, 53, 55	Drizzle: Light, moderate, and dense intensity
    56, 57	    Freezing Drizzle: Light and dense intensity
    61, 63, 65	Rain: Slight, moderate and heavy intensity
    66, 67	    Freezing Rain: Light and heavy intensity
    71, 73, 75	Snow fall: Slight, moderate, and heavy intensity
    77	        Snow grains
    80, 81, 82	Rain showers: Slight, moderate, and violent
    85, 86	    Snow showers slight and heavy
    95 	        Thunderstorm: Slight or moderate
    96, 99  	Thunderstorm with slight and heavy hail
 */

public enum WeatherCode
{
    ClearSky = 0,
    MainlyClear = 1,
    PartlyCloudy = 2,
    Overcast = 3,

    Fog = 45,
    DepositRimeFog = 48,

    DrizzleLight = 51,
    DrizzleModerate = 53,
    DrizzleDense = 55,

    FreezingDrizzleLight = 56,
    FreezingDrizzleDense = 57,

    RainSlight = 61,
    RainModerate = 63,
    RainHeavy = 65,

    FreezingRainLight = 66,
    FreezingRainHeavy = 67,

    SnowLight = 71,
    SnowModerate = 73,
    SnowHeavy = 75,

    SnowGrains = 77,

    RainShowersSlight = 80,
    RainShowersModerate = 81,
    RainShowersViolent = 82,

    SnowShowersSlight = 85,
    SnowShowersHeavy = 86,

    Thunderstorm = 95,
    ThunderstormSlightHail = 96,
    ThunderstormHeavyHail = 99
}
