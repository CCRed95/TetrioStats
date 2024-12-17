using MessagePack;
using MessagePack.Formatters;

namespace TetrioStats.Core.Data.TheoryPack
{
	public class TheoryPackResolver2
		: IFormatterResolver
	{
		public IMessagePackFormatter<T> GetFormatter<T>()
		{
			if (typeof(T) == typeof(object))
				return (IMessagePackFormatter<T>)new TheorypackFormatter2();
			
			return null;
		}
	}
}