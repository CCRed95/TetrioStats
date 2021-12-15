using System.ComponentModel;

namespace TetrioStats.Api.Domain.Streams
{
	public enum StreamContext
	{
		[Description("global")] Global,
		[Description("userbest")] UserBest,
		[Description("userrecent")] UserRecent,
	}
}