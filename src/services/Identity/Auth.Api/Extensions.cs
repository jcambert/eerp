using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Auth.Api
{
    internal static class Extensions
    {
        public static string BuildUri(this string root, NameValueCollection query)
        {
            if (query == null) return root;
            var collection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var key in query.Cast<string>().Where(key => !string.IsNullOrEmpty(query[key])))
            {
                collection[key] = query[key];
            }

            if (root.Contains("?"))
            {
                if (root.EndsWith("&"))
                {
                    root = root + collection.ToString();
                }
                else
                {
                    root = root + "&" + collection.ToString();
                }
            }
            else
            {
                root = root + "?" + collection.ToString();
            }

            return root;
        }

        public static string FormatMe(this string me, params string[] parameters)
        {
            return string.Format(me, parameters);
        }
    }
}
