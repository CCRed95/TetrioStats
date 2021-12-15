using Newtonsoft.Json;

namespace TetrioStats.Api.Domain.Json.Streams
{
	public static class Serialize
	{
		public static string ToJson(this GameRecordsResponse self)
		{
			return JsonConvert.SerializeObject(self, Converter.Settings);
		}
	}
}