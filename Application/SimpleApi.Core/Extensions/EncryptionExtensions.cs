using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace SimpleApi.Core
{
    public static class EncryptionExtensions
    {
        public static string GetHash<T>(this string str, bool toUpper = false) where T : HashAlgorithm
        {
            return GetHash<T>(Encoding.UTF8.GetBytes(str), toUpper);
        }

        /// <summary>
        /// 获取哈希值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="bytes"></param>
        /// <param name="toUpper"></param>
        /// <returns></returns>
        public static string GetHash<T>(this byte[] bytes, bool toUpper = false) where T : HashAlgorithm
        {
            StringBuilder sb = new StringBuilder();

            MethodInfo create = typeof(T).GetMethod("Create", new Type[] { });
            using (T crypt = (T)create.Invoke(null, null))
            {
                byte[] hashBytes = crypt.ComputeHash(bytes);
                foreach (byte bt in hashBytes)
                {
                    sb.Append(bt.ToString("x2"));
                }
            }
            if (toUpper)
            {
                return sb.ToString().ToUpper();
            }
            return sb.ToString();
        }
    }
}
