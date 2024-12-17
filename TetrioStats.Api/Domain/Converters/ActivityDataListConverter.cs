using System;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.General;

namespace TetrioStats.Api.Domain.Converters;

public class ActivityDataListConverter
    : JsonConverter
{
    public static readonly ActivityDataListConverter Singleton = new();


    public override bool CanConvert(Type type)
    {
        return type == typeof(ActivityDataPointList);
    }

    public override object ReadJson(
        JsonReader reader,
        Type type,
        object existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;

        var list = new ActivityDataPointList();

        var dateTime = DateTime.Now;

        var value = reader.ReadAsInt32();

        while (value != null)
        {
            var integralValue = value.Value;

            list.Add(new(dateTime, integralValue));

            dateTime = dateTime.Subtract(TimeSpan.FromHours(2));
            value = reader.ReadAsInt32();
        }

        return list;
    }

    public override void WriteJson(
        JsonWriter writer,
        object untypedValue,
        JsonSerializer serializer)
    {
        throw new("Cannot marshal type GameType");
    }
}