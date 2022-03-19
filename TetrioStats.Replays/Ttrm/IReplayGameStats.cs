using System;

namespace TetrioStats.Replays.Ttrm
{
	public interface IReplayGameStats
	{
		double LeftPPS { get; }

		double LeftAPM { get; }

		double LeftVS { get; }

		TimeSpan GameLength { get; }

		double RightPPS { get; }

		double RightAPM { get; }

		double RightVS { get; }
	}
}