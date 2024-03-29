﻿namespace Socials.Client.Client.Helpers
{
    public static class StringExtensions
    {
        public static string FirstLetterToUpperCase(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }
    }
}
