using System;
using Ccr.Std.Core.Extensions;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Labs;

namespace TetrioStats.Api.Domain.Converters;

public class UnixTimeStampConverter
    : JsonConverter
{
    public static readonly UnixTimeStampConverter Instance = new();


    public override bool CanConvert(Type type)
    {
        return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
    }

    public override object ReadJson(
        JsonReader reader,
        Type type,
        object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var value = serializer.Deserialize<double>(reader);
        var timeSpan = TimeSpan.FromSeconds(value);

        return timeSpan;
    }

    public override void WriteJson(
        JsonWriter writer,
        object untypedValue,
        JsonSerializer serializer)
    {
        switch (untypedValue)
        {
            case TimeSpan timeSpan:
            {
                serializer.Serialize(writer, timeSpan.TotalSeconds);
                break;
            }
            case null:
            {
                serializer.Serialize(writer, null);
                return;
            }
        }
        throw new("Cannot marshal type TimeSpan");
    }
}

public class UnixTimeStampMillisecondConverter
    : JsonConverter
{
    public static readonly UnixTimeStampMillisecondConverter Instance = new();


    public override bool CanConvert(Type type)
    {
        return type == typeof(TimeSpan) || type == typeof(TimeSpan?);
    }

    public override object ReadJson(
        JsonReader reader,
        Type type,
        object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var value = serializer.Deserialize<double>(reader);
        var timeSpan = TimeSpan.FromMilliseconds(value);

        return timeSpan;
    }

    public override void WriteJson(
        JsonWriter writer,
        object untypedValue,
        JsonSerializer serializer)
    {
        switch (untypedValue)
        {
            case TimeSpan timeSpan:
            {
                serializer.Serialize(writer, timeSpan.TotalMilliseconds);
                break;
            }
            case null:
            {
                serializer.Serialize(writer, null);
                return;
            }
        }
        throw new("Cannot marshal type TimeSpan");
    }
}

public class LeagueFlowMatchResultConverter
    : JsonConverter
{
    public static readonly LeagueFlowMatchResultConverter Singleton = new();


    public override bool CanConvert(Type type)
    {
        return type == typeof(LeagueFlowMatchResult) || type == typeof(LeagueFlowMatchResult?);
    }

    public override object ReadJson(
        JsonReader reader,
        Type type,
        object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var integralValue = serializer.Deserialize<int>(reader);

        return integralValue switch
        {
            0 => LeagueFlowMatchResult.Unknown,
            1 => LeagueFlowMatchResult.Victory,
            2 => LeagueFlowMatchResult.Defeat,
            3 => LeagueFlowMatchResult.VictoryByDisqualification,
            4 => LeagueFlowMatchResult.DefeatByDisqualification,
            5 => LeagueFlowMatchResult.Tie,
            6 => LeagueFlowMatchResult.NoContest,
            7 => LeagueFlowMatchResult.MatchNullified,

            _ => throw new(
                $"Cannot unmarshal type {nameof(LeagueFlowMatchResult).SQuote()}.")
        };
    }

    public override void WriteJson(
        JsonWriter writer,
        object untypedValue,
        JsonSerializer serializer)
    {
        if (untypedValue == null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var value = (LeagueFlowMatchResult)untypedValue;
        var integralValue = (int)value;

        serializer.Serialize(writer, integralValue);

        throw new("Cannot marshal type GameType");
    }
}