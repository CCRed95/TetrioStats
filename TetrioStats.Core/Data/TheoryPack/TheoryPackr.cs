using System.Collections.Generic;
using System.IO;
using MessagePack;
using MessagePack.Resolvers;

namespace TetrioStats.Core.Data.TheoryPack
{
	internal static class TheoryPackr
	{
		private static readonly MessagePackSerializerOptions DefaultOptions;

		static TheoryPackr()
		{
			// Configuring options, similar to how you would add extensions in msgpackr
			DefaultOptions = MessagePackSerializerOptions.Standard
				.WithSecurity(MessagePackSecurity.UntrustedData)
				.WithResolver(CompositeResolver.Create(
					new TheoryPackResolver1(),
					new TheoryPackResolver2(),
					StandardResolver.Instance));
		}

		// Unpack a single theorypack message
		public static T Unpack<T>(byte[] bytes)
		{
			return MessagePackSerializer.Deserialize<T>(bytes, DefaultOptions);
		}

		// Unpack multiple theorypack messages
		public static IEnumerable<T> UnpackMultiple<T>(byte[] bytes)
		{
			using var stream = new MemoryStream(bytes);

			while (stream.Position < stream.Length)
			{
				yield return MessagePackSerializer.Deserialize<T>(stream, DefaultOptions);
			}
		}

		// Decode a theorypack message (same as unpack in this context)
		public static T Decode<T>(byte[] bytes)
		{
			return Unpack<T>(bytes);
		}

		// Pack a single theorypack message
		public static byte[] Pack<T>(T obj)
		{
			return MessagePackSerializer.Serialize(obj, DefaultOptions);
		}

		// Encode a theorypack message (same as pack in this context)
		public static byte[] Encode<T>(T obj)
		{
			return Pack(obj);
		}
	}
}