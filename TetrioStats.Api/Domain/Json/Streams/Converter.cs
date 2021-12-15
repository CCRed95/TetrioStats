using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TetrioStats.Api.Domain.Json.Converters;

namespace TetrioStats.Api.Domain.Json.Streams
{
	internal static class Converter
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				GameTypeConverter.Singleton,
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};
	}
}