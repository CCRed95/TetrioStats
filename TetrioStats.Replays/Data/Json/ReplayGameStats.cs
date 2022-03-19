using System;
using TetrioStats.Replays.Ttrm;

namespace TetrioStats.Replays.Data.Json
{
	internal class ReplayGameStats 
		: IReplayGameStats
	{
		public double LeftPPS { get; set; }

		public double LeftAPM { get; set; }

		public double LeftVS { get; set; }

		public TimeSpan GameLength { get; set; }

		public double RightPPS { get; set; }

		public double RightAPM { get; set; }

		public double RightVS { get; set; }
	}
}