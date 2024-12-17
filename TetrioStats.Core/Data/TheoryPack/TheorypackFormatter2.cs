using MessagePack;
using MessagePack.Formatters;

namespace TetrioStats.Core.Data.TheoryPack
{
	public class TheorypackFormatter2
		: IMessagePackFormatter<object>
	{
		public void Serialize(
			ref MessagePackWriter writer,
			object value, 
			MessagePackSerializerOptions options)
		{
			writer.Write(value == null ? MessagePackCode.Nil : (byte)2);
		}

		public object Deserialize(
			ref MessagePackReader reader,
			MessagePackSerializerOptions options)
		{
			return reader.TryReadNil()
				? new { success = false }
				: new { success = false, error = reader.ReadRaw() };
		}
	}
}