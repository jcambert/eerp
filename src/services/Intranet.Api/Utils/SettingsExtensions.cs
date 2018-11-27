using Intranet.Api.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intranet.Api
{
    public static class SettingsExtensions
    {
        public static string FindByTypeName(this ApiSettings settings,string s)
        {

            var props=settings.GetType().GetProperties().Where(p => p.Name == s).FirstOrDefault();
            if (props == null) return string.Empty;
            var result=props.GetGetMethod().Invoke(settings, new object[] { });
            return result as string;
        }
    }
}
