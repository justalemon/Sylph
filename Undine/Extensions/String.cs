namespace Undine.Extensions
{
    public static class StringExtensions
    {
        public static string Sanitize(this string _string)
        {
            return _string.Replace("\0", "").Trim();
        }
    }
}
