using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PotatoReader.Providers.Sites
{
	class ParseHelper
	{
		public static IEnumerable<(string Name, string Value)> ParseGroup(string regExp, string input, string nameGroup, string valueGroup)
		{
			var list = new List<(string Name, string Value)>();
			var reg = new Regex(regExp, RegexOptions.IgnoreCase);
			var matches = reg.Matches(input);

			if (matches.Count == 0)
			{
				throw new Exception("Could not parse");
			}

			foreach (Match match in matches)
			{
				var value = match.Groups[valueGroup].Value.Trim();
				var name = match.Groups[nameGroup].Value.Trim();
				var chapter = (name, value);
				list.Add(chapter);
			}
			return list;
		}

		public static IEnumerable<string> Parse(string regExp, string input, string groupName)
		{
			var reg = new Regex(regExp, RegexOptions.IgnoreCase);
			var matches = reg.Matches(input);

			if (matches.Count == 0)
			{
				throw new Exception("Could not parse");
			}

			var list = (from Match match in matches select match.Groups[groupName].Value.Trim()).ToList();
			var result = list.Distinct().ToList();
			return result;
		}
	}
}
