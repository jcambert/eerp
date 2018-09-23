using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Ping.Api.services
{
    public interface ISpidLicence
    {
        string Tm { get; }

        string Tmc { get; }
    }

    public class SpidLicence : ISpidLicence
    {
        private string _tm;
        private string _key;
        private string _tmc;
        private readonly ISpidConfiguration _configuration;

        public SpidLicence(ISpidConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Tm
        {
            get
            {
                _tm= DateTime.Now.ToString("yyyyMMddhhMMssfff");
                _key = _tm.Key(_configuration["spid:password"]);
                _tmc = _key.Hash(_tm);
                return _tm;
            }
        }

        public string Tmc =>_tmc;
    }

    internal static class SpidLicenceExtensions
    {
        public static string Key(this string tm,string password)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
                var md5hex = BitConverter.ToString(result);
                md5hex = md5hex.Replace("-", "").ToLower();
                return md5hex;
                //var tmc = GetHash("20180921105708490", md5hex);

                //Console.WriteLine(tmc == "33f0ce7d89042e2d049659a103fbf636c9f8234a");
            }
        }

        public static string Hash(this string key,string tm)
        {
            return GetHash(tm, key);
        }

        private static String GetHash(String text, String key)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            Byte[] textBytes = encoding.GetBytes(text);
            Byte[] keyBytes = encoding.GetBytes(key);

            Byte[] hashBytes;

            using (HMACSHA1 hash = new HMACSHA1(keyBytes))
                hashBytes = hash.ComputeHash(textBytes);

            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

    }
}
