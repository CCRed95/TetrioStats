using System;
using System.Collections.Generic;

namespace TetrioStats.Replays.Ttrm
{
	public interface ITtrmFileStatistics
	{
		string ReplayID { get; }

		string ShortID { get; }

		DateTimeOffset TimeStamp { get; }

		IUserData LeftUser { get; }

		IUserData RightUser { get; }

		IReplayGameStats OverallStats { get; }

		IList<IReplayGameStats> Replays { get; }
	}
}
