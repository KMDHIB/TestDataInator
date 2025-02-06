using System.Text.RegularExpressions;

namespace TestDataInator.Helpers
{
    public static class StringHelper
    {
        public static string RemoveIllegalCharacters(string input)
        {

            // Step 1: Extract the content between the ```
            string pattern = @"```(.*?)```";
            Match match = Regex.Match(input, pattern, RegexOptions.Singleline);

            if (match.Success)
            {
                string content = match.Groups[1].Value
                                    .Replace("\\n", "")
                                    .Replace("\n", "")
                                    .Replace("\\", "")
                                    .Replace(@"\", "");

                return content;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
