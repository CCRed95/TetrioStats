using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Ccr.Std.Core.Collections;
using Ccr.Std.Core.Extensions;

namespace TetrioStats.Api.Domain.Rankings
{
	//[DebuggerDisplay("ToString()")]
	public sealed class UserRankGrade
		: ValueEnum<(Grade, GradeModifier, double)>
	{
		public Grade GradeLetter
		{
			get => Value.Item1;
		}

		public GradeModifier GradeModifier
		{
			get => Value.Item2;
		}

		public double RankPercentile
		{
			get => Value.Item3;
		}

		public static readonly UserRankGrade D
			= new UserRankGrade(100.0, Grade.D);

		public static readonly UserRankGrade DPlus
			= new UserRankGrade(97.5, Grade.D, GradeModifier.Plus);

		public static readonly UserRankGrade CMinus
			= new UserRankGrade(95.0, Grade.C, GradeModifier.Minus);

		public static readonly UserRankGrade C
			= new UserRankGrade(90.0, Grade.C);

		public static readonly UserRankGrade CPlus
			= new UserRankGrade(84.0, Grade.C, GradeModifier.Plus);

		public static readonly UserRankGrade BMinus
			= new UserRankGrade(78.0, Grade.B, GradeModifier.Minus);

		public static readonly UserRankGrade B
			= new UserRankGrade(70.0, Grade.B);

		public static readonly UserRankGrade BPlus
			= new UserRankGrade(62.0, Grade.B, GradeModifier.Plus);

		public static readonly UserRankGrade AMinus
			= new UserRankGrade(54.0, Grade.A, GradeModifier.Minus);

		public static readonly UserRankGrade A
			= new UserRankGrade(46.0, Grade.A);

		public static readonly UserRankGrade APlus
			= new UserRankGrade(38.0, Grade.A, GradeModifier.Plus);

		public static readonly UserRankGrade SMinus
			= new UserRankGrade(30.0, Grade.S, GradeModifier.Minus);

		public static readonly UserRankGrade S
			= new UserRankGrade(23.0, Grade.S);
		
		public static readonly UserRankGrade SPlus
			= new UserRankGrade(17.0, Grade.S, GradeModifier.Plus);

		public static readonly UserRankGrade SS
			= new UserRankGrade(11.0, Grade.SS);

		public static readonly UserRankGrade U
			= new UserRankGrade(5.0, Grade.U);

		public static readonly UserRankGrade X
			= new UserRankGrade(1.0, Grade.X);
			

		private UserRankGrade(
			double percentile,
			Grade grade,
			GradeModifier modifier = GradeModifier.None,
			[CallerMemberName] string fieldName = "",
			[CallerLineNumber] int line = 0) : base(
				(grade, modifier, percentile),
				fieldName,
				line)
		{
		}


		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.Append($"{Value.Item1:G}");

			var suffix = Value.Item2 switch
			{
				GradeModifier.Minus => "-",
				GradeModifier.Plus => "+",
				GradeModifier.None => "",
				_ => throw new NotImplementedException()
			};

			sb.Append(suffix);

			return sb.ToString();
		}


		public static UserRankGrade ParseFromJsonFormat(
			string gradeStr)
		{
			if (!TryParseFromJsonFormat(gradeStr, out var userRankGrade))
				throw new FormatException(
					$"Cannot parse type {typeof(UserRankGrade).FormatName().SQuote()} from the string " +
					$"{gradeStr.Quote()}.");

			return userRankGrade;
		}

		public static bool TryParseFromJsonFormat(
			string gradeStr, out UserRankGrade userRankGrade)
		{
			var modifiedRank = gradeStr.ToUpper()
				.Replace("-", "Minus")
				.Replace("+", "Plus");

			var parsedUserRank = ToArray<UserRankGrade>()
				.SingleOrDefault(t => t.Name == modifiedRank);

			if (ReferenceEquals(parsedUserRank, null))
			{
				userRankGrade = null;
				return false;
			}

			userRankGrade = parsedUserRank;
			return true;
		}

		public bool Equals(UserRankGrade other)
		{
			if (ReferenceEquals(other, null))
				return false;

			return GradeLetter == other.GradeLetter
				&& GradeModifier == other.GradeModifier
				&& RankPercentile == other.RankPercentile;
		}

		public override int GetHashCode()
		{
			var grade = (int) GradeLetter * 1;
			var modifier = (int) GradeModifier * 7;
			var percentile = RankPercentile;

			var hash = (grade ^ modifier) * (percentile);
			var finalHash = (int)Math.Floor(hash);

			return finalHash;
		}

		public override bool Equals(object obj)
		{
			return ReferenceEquals(this, obj);
		}

		public static bool operator >(UserRankGrade left, UserRankGrade right)
		{
			return left.LineNumber > right.LineNumber;
		}

		public static bool operator <(UserRankGrade left, UserRankGrade right)
		{
			return left.LineNumber < right.LineNumber;
		}

		public static bool operator ==(UserRankGrade left, UserRankGrade right)
		{
			return left?.Equals(right) ?? ReferenceEquals(right, null);

			//	return left == (ValueEnum) null 
			//		? right == (ValueEnum)null
			//		: ReferenceEquals(left, right);
		}

		public static bool operator !=(UserRankGrade left, UserRankGrade right)
		{
			return left?.Equals(right) ?? !ReferenceEquals(right, null);
		}

		//public override int GetHashCode()
		//{
		//	int num1 = GetType().GetHashCode() * 397;
		//	object valueBase = this.ValueBase;
		//	int num2 = ReferenceEquals() != null ? valueBase.GetHashCode() : 0;
		//	return num1 ^ num2;
		//}

		//var letterGrade = gradeStr.Substring(0, 1);
		//var gradeModifierStr = gradeStr.Substring(1).ToLower();

		//var gradeModifier = gradeModifierStr switch
		//{
		//	"-" => GradeModifier.Minus,
		//	"+" => GradeModifier.Plus,
		//	"" => GradeModifier.None,
		//	_ => throw new NotImplementedException()
		//};

		//sb.Append(suffix);



		// Unranked,
		// D,
		// DPlus,
		// CMinus,
		// C,
		// CPlus,
		// BMinus,
		// B,
		// BPlus,
		// AMinus,
		// A,
		// APlus,
		// SMinus,
		// S,
		// SPlus,
		// SS,
		// U,
		// X
	}
}