using System;

namespace VividClub.Web.Infrastructure
{
    public static class CoreValidator
    {
        public static bool CheckIfStringIsNullOrEmpty(string input) => String.IsNullOrEmpty(input);
    }
}