using MessagePack;
using MessagePack.Formatters;

namespace TetrioStats.Core.Data.TheoryPack
{
	public class TheorypackFormatter1
		: IMessagePackFormatter<object>
	{
		public void Serialize(
			ref MessagePackWriter writer,
			object value,
			MessagePackSerializerOptions options)
		{
			writer.Write(
				value == null ? MessagePackCode.Nil : (byte)1);
		}

		public object Deserialize(
			ref MessagePackReader reader,
			MessagePackSerializerOptions options)
		{
			return reader.TryReadNil()
				? new { success = true }
				: new { success = true, data = reader.ReadRaw() };
		}
	}
}