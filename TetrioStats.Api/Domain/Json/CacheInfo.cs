using System;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Json.Converters;

namespace TetrioStats.Api.Domain.Json
{
	public class CacheInfo
	{
		[JsonProperty("status")]
		public CacheStatus Status { get; set; }

		[JsonConverter(typeof(CustomUnixDateTimeConverter))]
		[JsonProperty("cached_at")]
		public DateTime CachedAt { get; set; }

		[JsonConverter(typeof(CustomUnixDateTimeConverter))]
		[JsonProperty("cached_until")]
		public DateTime CachedUntil { get; set; }
	}
}