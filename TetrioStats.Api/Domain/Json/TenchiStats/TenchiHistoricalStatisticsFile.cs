using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TetrioStats.Api.Domain.Json.TenchiStats
{
	public class TenchiHistoricalStatisticsFile
	{
		[JsonIgnore]
		private static readonly JsonSerializerSettings _converterSettings = new JsonSerializerSettings
		{
			MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
			DateParseHandling = DateParseHandling.None, 
			Converters =
			{
				new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
			},
		};


		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("users")]
		public List<TenchiUserStatistics> UserStats { get; set; }


		public string ToJson()
		{
			return JsonConvert.SerializeObject(this, _converterSettings);
		}

		public static TenchiHistoricalStatisticsFile FromJson(string json)
		{
			return JsonConvert.DeserializeObject<TenchiHistoricalStatisticsFile>(
				json, _converterSettings);
		}


		public static TenchiUserStatistics FromJsonHighEfficiency(
			string jsonFileName, 
			string userId)
		{
			using var streamReader = File.OpenText(jsonFileName);

			var fileContent = streamReader.ReadToEnd();
			var recordIndex = fileContent.IndexOf(
				userId, StringComparison.CurrentCultureIgnoreCase);

			if (recordIndex < 0)
				return null;

			var innerRecordEndIndex = fileContent.IndexOf(
				"}", recordIndex, StringComparison.CurrentCultureIgnoreCase);

			var recordEndIndex = fileContent.IndexOf(
				"}", innerRecordEndIndex + 1, StringComparison.CurrentCultureIgnoreCase);

			var actualRecordIndex = BackReferenceIndexOf(fileContent, '{', recordIndex);

			var jsonRecordText = fileContent.Substring(
				actualRecordIndex, 
				recordEndIndex - actualRecordIndex + 1);

			return JsonConvert.DeserializeObject<TenchiUserStatistics>(
				jsonRecordText, _converterSettings);
		}

		private static int BackReferenceIndexOf(
			string @this,
			char search,
			int startIndex)
		{
			for (var i = 0; i <= startIndex; i++)
			{
				var c = @this[startIndex - i];

				if (c == search)
				{
					return startIndex - i;
				}
			}
			return -1;
		}
	}
}