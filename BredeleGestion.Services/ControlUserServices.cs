using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BredeleGestion.Services
{
    public class ControlUserServices
    {
        static public string pwdBdd;
        static public string userConnected;

        public static string EncryptPwd(string clearPwd)
        {
            byte[] pwd = Encoding.UTF8.GetBytes(clearPwd);
            byte[] hash = SHA512.HashData(pwd);

            return Convert.ToBase64String(hash);
        }

        public static bool CheckLogin(string login, List<string[]> rstRequet)
        {
            bool flag = false;
            pwdBdd = "";
            userConnected = "";

            foreach (string[] user in rstRequet)
            {
                if (user.Contains(login))
                {
                    flag = true;
                    userConnected = login;
                    pwdBdd = user[1];
                }
            }
            return flag;
        }

        public static bool CheckPwd(string clearPwd, string encryptPwd)
        {
            string hashPwd = EncryptPwd(clearPwd);

            if (hashPwd == encryptPwd)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
