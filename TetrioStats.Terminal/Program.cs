namespace TetrioStats.Terminal
{
	public class Program
	{
		public static string[] Args { get; private set; }


		public static void Main(string[] args)
		{
			Args = args;

			var terminalApplication = new TetrioStatsTerminalApplication(args);
			terminalApplication.Start();
		}
	}
}
