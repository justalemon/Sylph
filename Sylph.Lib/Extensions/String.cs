namespace Sylph.Extensions
{
    /// <summary>
    /// Extensions to be used on your normal every day string's.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Removes the trailing characters out of a Nintendo DS/DSi Title.
        /// </summary>
        /// <returns>A System.String without trailing whitespaces or \0.</returns>
        public static string SanitizeTitle(this string _string)
        {
            int index = _string.LastIndexOf('\n');
            return _string.Substring(0, index == -1 ? _string.Length : index).Replace("\n", " ").Replace("\0", "");
        }
    }
}
