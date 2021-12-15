using System;
using Ccr.Std.Core.Extensions;
using JetBrains.Annotations;
using TetrioStats.Api.Extensions;

namespace TetrioStats.Api.Domain.Streams
{
	public readonly struct StreamID
	{
		public StreamType Type { get; }
		
		public StreamContext Context { get; }
		
		public string Identifier { get; }


		private StreamID(
			StreamType type, 
			StreamContext context,
			[CanBeNull] string identifier = null)
		{
			switch (context)
			{
				case StreamContext.UserBest:
				case StreamContext.UserRecent:
				{
					if (identifier != null)
						break;

					throw new ArgumentNullException(
						$"The {nameof(identifier).SQuote()} argument cannot be 'null' if the 'context' argument " +
						$"is set to either {nameof(StreamContext.UserBest).SQuote()} or " +
						$"{nameof(StreamContext.UserRecent).SQuote()}, because a tetr.io UserID must be specified.");
				}
				case StreamContext.Global when identifier != null:
					throw new ArgumentNullException(
						$"The {nameof(identifier).SQuote()} argument cannot be {identifier.Quote()} if the 'context' " +
						$"argument is set to {nameof(StreamContext.Global).SQuote()} because no tetr.io 'UserID' " +
						$"can be specified on global Stream IDs.");
				//default:
				//	throw new ArgumentOutOfRangeException(nameof(context), context, null);
			}

			Type = type;
			Context = context;
			Identifier = identifier;
		}


		public static StreamID UserBest(
			StreamType type,
			string userID)
		{
			return new StreamID(type, StreamContext.UserBest, userID);
		}

		public static StreamID UserRecent(
			StreamType type,
			string userID)
		{
			return new StreamID(type, StreamContext.UserRecent, userID);
		}

		public static StreamID Global(
			StreamType type)
		{
			return new StreamID(type, StreamContext.Global);
		}


		public override string ToString()
		{
			var str = $"{Type.GetDescription()}_{Context.GetDescription()}";

			if (Context == StreamContext.Global)
				return str;

			str += $"_{Identifier}";
			return str;
		}
	}
}
