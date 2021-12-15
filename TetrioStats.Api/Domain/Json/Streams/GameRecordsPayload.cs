using System.Collections.Generic;
using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public class GameRecordsPayload
		: ITetrioResponsePayload<List<GameRecordInfo>>
	{
		[JsonProperty("records")]
		public List<GameRecordInfo> Payload { get; set; }
	}
}