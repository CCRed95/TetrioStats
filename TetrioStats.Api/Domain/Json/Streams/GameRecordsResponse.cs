using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TetrioStats.Api.Domain.Json.Converters;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class GameRecordsResponse
		: TetrioApiResponseBase<GameRecordsPayload, List<GameRecordInfo>>
	{
		public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None,
			Converters =
			{
				GameTypeConverter.Singleton,
				new IsoDateTimeConverter
				{
					DateTimeStyles = DateTimeStyles.AssumeUniversal
				}
			},
		};

		public string ToJson()
		{
			return JsonConvert.SerializeObject(this, Settings);
		}

		public static GameRecordsResponse FromJson(string json)
		{
			return JsonConvert.DeserializeObject<GameRecordsResponse>(json, Settings);
		}
	}
}