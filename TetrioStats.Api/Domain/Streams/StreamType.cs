using System.ComponentModel;

namespace TetrioStats.Api.Domain.Streams
{
	public enum StreamType
	{
		[Description("any")] Any,
		[Description("40l")] _40Lines,
		[Description("blitz")] Blitz
	}
}