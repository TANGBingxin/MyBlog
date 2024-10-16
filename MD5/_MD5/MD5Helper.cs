﻿using System.Security.Cryptography;
using System.Text;

namespace MyBlog.Utility._MD5
{
    /// <summary>
    /// 密码加密
    /// </summary>
    public static class MD5Helper
    {
        public static string MD5Encrypt32(string password)
        {
            string pwd = "";
            MD5 md5 = MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < s.Length; i++)
            {
                pwd = pwd + s[i].ToString("X");
            }
            return pwd;
        }
    }
}



