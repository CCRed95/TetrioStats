using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TetrioStats.Api.Domain.Converters;

namespace TetrioStats.Api.Domain.General;

public class ActivityData
{
	[JsonProperty("activity")]
	public ActivityDataPointList ActivityPoints { get; set; }
}

[JsonConverter(typeof(ActivityDataListConverter))]
public class ActivityDataPointList : List<ActivityDataPoint>;

    
public class ActivityDataPoint(DateTime timeStamp, int activePlayers)
{
	public DateTime TimeStamp { get; } = timeStamp;
	
	public int ActivePlayers { get; } = activePlayers;
}


public class ActivityDataResponse : TetrioApiResponse<ActivityData>;