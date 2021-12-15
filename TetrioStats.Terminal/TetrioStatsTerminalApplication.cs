using Ccr.Terminal.Application;

namespace TetrioStats.Terminal
{
	public class TetrioStatsTerminalApplication
		: TerminalApplication
	{
		public override string ApplicationName => " TetrioStats data terminal ";

		public override string VersionNumber => "3.0.0.0";


		public TetrioStatsTerminalApplication(
			string[] args) : base(args)
		{
		}
	}
}