using System;
using Ccr.Std.Core.Extensions;
using Newtonsoft.Json;
using TetrioStats.Api.Infrastructure;
using EnumExtensions = TetrioStats.Api.Extensions.EnumExtensions;

namespace TetrioStats.Api.Domain.Converters;

public class UserRankConverter
	: JsonConverter
{
	public static readonly UserRankConverter Singleton = new();


	public override bool CanConvert(Type type)
	{
		return type == typeof(UserRank) || type == typeof(UserRank?);
	}

	public override object ReadJson(
		JsonReader reader,
		Type type,
		object existingValue,
		JsonSerializer serializer)
	{
		if (reader.TokenType == JsonToken.Null)
			return null;
		
		var value = serializer.Deserialize<string>(reader);

		if (string.IsNullOrEmpty(value))
			return null;

		return EnumExtensions.ParseFromDescription<UserRank>(value);
	}

	public override void WriteJson(
		JsonWriter writer,
		object untypedValue,
		JsonSerializer serializer)
	{
		throw new("Cannot marshal type UserRank.");
	}
}