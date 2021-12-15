using System.Drawing;
using Ccr.Colorization.Mappings;
using Ccr.Std.Core.Extensions;
using Ccr.Terminal;
using Ccr.Terminal.Extensions;
using static Ccr.Terminal.ExtendedConsole;

namespace TetrioStats.Terminal.Extensions
{
	public static class ExtendedConsoleExtensions
	{
		public static string CenterAlign(
			this string @this,
			int width)
		{
			var contentLength = @this.Length;
			var remainingLength = width - contentLength;
			var leftPadding = (int)(remainingLength / 2d).Floor();
			var formattedStr = (" ".Repeat(leftPadding) + @this).PadRight(width);

			return formattedStr;
		}

		public static bool PromptYesNo(
			this IXConsoleSession @this,
			string prompt,
			bool onNewLine = false)
		{
			while (true)
			{
				XConsole.Write(prompt, Swatch.Cyan)
					.Write(" ( y / n )", Swatch.Teal)
					.Write(": ", Color.Azure);

				if (onNewLine)
					XConsole.WriteLine();

				var input = XConsole.ReadLine()
					.Trim()
					.ToLower();

				switch (input)
				{
					case "y":
					case "yes":
						return true;
					case "n":
					case "no":
						return false;
				}
				XConsole.WriteLine("Invalid input. Must be 'y' or 'n'.", Swatch.Red);
			}
		}

		public static int PromptForInt(
			this IXConsoleSession @this,
			string prompt,
			bool onNewLine = false)
		{
			while (true)
			{
				XConsole.Write(prompt, Swatch.Cyan)
					.Write(": ", Color.Azure);

				if (onNewLine)
					XConsole.WriteLine();

				var input = XConsole.ReadLine()
					.Trim()
					.ToLower();

				if (int.TryParse(input.Trim(), out var value))
					return value;
				
				XConsole.WriteLine(
					"Invalid input. Must be a parseable integer.", Swatch.Red);
			}
		}

		public static int? PromptForIntOrSkip(
			this IXConsoleSession @this,
			string prompt,
			out bool wasSkipped,
			bool onNewLine = false)
		{
			while (true)
			{
				XConsole.Write(prompt, Swatch.Cyan)
					.Write("(or 's' to skip)", Swatch.Orange)
					.Write(": ", Color.Azure);

				if (onNewLine)
					XConsole.WriteLine();

				var input = XConsole.ReadLine()
					.Trim()
					.ToLower();

				if (input.Trim().ToLower() == "s"
					|| input.Trim().ToLower() == "skip")
				{
					wasSkipped = true;
					return null;
				}

				if (int.TryParse(input.Trim(), out var value))
				{
					wasSkipped = false;
					return value;
				}

				XConsole.WriteLine(
					"Invalid input. Must be a parseable integer, or 's' to skip.", Swatch.Red);
			}
		}
	}
}