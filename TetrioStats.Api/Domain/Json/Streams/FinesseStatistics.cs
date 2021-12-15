using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class FinesseStatistics
	{
		[JsonProperty("combo")]
		public int Combo { get; set; }

		[JsonProperty("faults")]
		public int FinesseFaults { get; set; }

		[JsonProperty("perfectpieces")]
		public int PerfectPieces { get; set; }
	}
}