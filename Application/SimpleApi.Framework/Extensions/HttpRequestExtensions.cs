using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace SimpleApi.Framework
{
    public static class HttpRequestExtensions
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIpAddress(this HttpContext httpContext)
        {
            // todo support new "Forwarded" header (2014) https://en.wikipedia.org/wiki/X-Forwarded-For
            var ip = SplitCsv(httpContext.GetHeaderValueAs("X-Forwarded-For")).FirstOrDefault();

            // RemoteIpAddress is always null in DNX RC1 Update1 (bug).
            if (string.IsNullOrWhiteSpace(ip) && httpContext?.Connection?.RemoteIpAddress != null)
                ip = httpContext.Connection.RemoteIpAddress.ToString();

            if (string.IsNullOrWhiteSpace(ip))
                ip = httpContext.GetHeaderValueAs("REMOTE_ADDR");

            if (string.IsNullOrWhiteSpace(ip))
                ip = "0.0.0.0";

            if (ip.Equals("::1"))
                ip = "127.0.0.1";

            return ip;
        }

        private static List<string> SplitCsv(string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable()
                .Select(s => s.Trim())
                .ToList();
        }

        private static string GetHeaderValueAs(this HttpContext httpContext, string headerName)
        {
            if (httpContext.Request?.Headers?.TryGetValue(headerName, out var values) ?? false)
            {
                var rawValues = values.ToString();

                if (!string.IsNullOrEmpty(rawValues))
                    return values.ToString();
            }
            return string.Empty;
        }
    }
}
