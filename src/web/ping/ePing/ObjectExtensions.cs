using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ePing
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serialize an object to String
        /// </summary>
        /// <typeparam name="T">Any kind</typeparam>
        /// <param name="o">The Object to Serialize</param>
        /// <returns></returns>
        public static string ToJson<T>(this T o)=> JsonConvert.SerializeObject(o);

        /// <summary>
        /// Deserialize a string to this Object representation
        /// </summary>
        /// <typeparam name="T">Any kind</typeparam>
        /// <param name="s">The string to deserialize into T</param>
        /// <returns></returns>
        public static T FromJson<T>(this string s) => JsonConvert.DeserializeObject<T>(s);
        
    }
}
