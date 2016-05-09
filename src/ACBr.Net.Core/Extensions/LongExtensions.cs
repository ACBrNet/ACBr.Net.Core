namespace ACBr.Net.Core.Extensions
{
	/// <summary>
	/// Class LongExtensions.
	/// </summary>
	public static class LongExtensions
	{
		/// <summary>
		/// Determines whether the specified n is divisble for x.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="n">The n.</param>
		/// <returns><c>true</c> if the specified x is divisble; otherwise, <c>false</c>.</returns>
		public static bool IsDivisible(this long x, int n)
		{
			return (x % n) == 0;
		}

		/// <summary>
		/// Determines whether the specified value is odd.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns><c>true</c> if the specified value is odd; otherwise, <c>false</c>.</returns>
		public static bool IsOdd(this long value)
		{
			return value % 2 != 0;
		}

		/// <summary>
		/// Zeroes the fill.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns>System.String.</returns>
		public static string ZeroFill(this long? value, int length)
		{
			return value.HasValue ? value.Value.ToString().ZeroFill(length) : "".ZeroFill(length);
		}

		/// <summary>
		/// Zeroes the fill.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="length">The length.</param>
		/// <returns>System.String.</returns>
		public static string ZeroFill(this long value, int length)
		{
			return ((int?)value).ZeroFill(length);
		}
	}
}