namespace Undine.Extensions
{
    /// <summary>
    /// Extensions to be used on your normal every day string's.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the trailing characters out of a string.
        /// </summary>
        /// <returns>A System.String without trailing whitespaces or \0.</returns>
        public static string Sanitize(this string _string)
        {
            return _string.Replace("\0", "").Trim();
        }
    }
}
