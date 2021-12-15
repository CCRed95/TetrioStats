using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class LineClearsStatistics
	{
		[JsonProperty("singles")]
		public int Singles { get; set; }

		[JsonProperty("doubles")]
		public int Doubles { get; set; }

		[JsonProperty("triples")]
		public int Triples { get; set; }

		[JsonProperty("quads")]
		public int Quads { get; set; }

		[JsonProperty("realtspins")]
		public int RealTSpins { get; set; }

		[JsonProperty("minitspins")]
		public int MiniTSpins { get; set; }

		[JsonProperty("minitspinsingles")]
		public int MiniTSpinSingles { get; set; }

		[JsonProperty("tspinsingles")]
		public int TSpinSingles { get; set; }

		[JsonProperty("minitspindoubles")]
		public int MiniTSpinDoubles { get; set; }

		[JsonProperty("tspindoubles")]
		public int TSpinDoubles { get; set; }

		[JsonProperty("tspintriples")]
		public int TSpinTriples { get; set; }

		[JsonProperty("tspinquads")]
		public int TSpinQuads { get; set; }

		[JsonProperty("allclear")]
		public int AllClears { get; set; }
	}
}