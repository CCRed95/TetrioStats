using System;
using System.ComponentModel;
using System.Linq;

namespace TetrioStats.Api.Extensions
{
	public static class EnumExtensions
	{
		public static string GetDescription<TEnum>(
			this TEnum @this)
				where TEnum : Enum
		{
			var fieldInfo = @this.GetType().GetField(@this.ToString());

			var attributes = fieldInfo?.GetCustomAttributes(
				typeof(DescriptionAttribute), false) as DescriptionAttribute[];

			if (attributes != null && attributes.Any())
				return attributes.First().Description;

			return @this.ToString();
		}
	}
}