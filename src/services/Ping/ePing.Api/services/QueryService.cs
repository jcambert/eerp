using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ePing.Api.services
{

    public class QueryService
    {
        const string PATTERN = @"(?<=[\?|\&]*)(?<key>[^\?=\&\#]+)=?(?<value>[^\?=\&\#]*)";
        public Dictionary<string,string> Parse(string querystring)
        {
            var result = new Dictionary<string, string>();
            var matches = Regex.Matches(querystring, PATTERN,RegexOptions.IgnoreCase);
            foreach (Match match in matches)
            {
                result[match.Groups["key"].Value] = match.Groups["value"].Value;
            }
            return result;
        }
    }
}
