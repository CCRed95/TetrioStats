using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class GarbageStatistics
	{
		[JsonProperty("sent")]
		public int TotalSent { get; set; }

		[JsonProperty("received")]
		public int TotalReceived { get; set; }

		[JsonProperty("attack")]
		public int Attack { get; set; }

		[JsonProperty("cleared")]
		public int TotalCleared { get; set; }
	}
}