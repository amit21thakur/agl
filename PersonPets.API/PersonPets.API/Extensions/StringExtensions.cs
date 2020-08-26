using System.Text;

namespace PersonPets.API.Extensions
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string str)
        {
            var tokens = str.Split(' ');
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < tokens.Length; i++)
            {
                if (i > 0)
                    builder.Append(" ");

                if (tokens[i] == string.Empty)
                    builder.Append(" ");
                else
                {
                    var lower = str.ToLower();
                    builder.AppendFormat("{0}{1}", lower.Substring(0, 1).ToUpper(), lower.Substring(1));
                }
            }
            return builder.ToString();
        }
    }
}
