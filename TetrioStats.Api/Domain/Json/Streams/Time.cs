using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class Time
	{
		[JsonProperty("start")]
		public int Start { get; set; }

		[JsonProperty("zero")]
		public bool Zero { get; set; }

		[JsonProperty("locked")]
		public bool Locked { get; set; }

		[JsonProperty("prev")]
		public int Prev { get; set; }

		[JsonProperty("frameoffset")]
		public int FrameOffset { get; set; }
	}
}