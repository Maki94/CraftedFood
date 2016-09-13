using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public static class HashPassword
    {
        static byte[] GetBytes(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }

        public static string SaltedHashPassword(string password, string salt)
        {
            byte[] saltBytes = GetBytes(salt);
            var passwordAndSaltBytes = Concat(password, saltBytes);
            return ComputeHash(passwordAndSaltBytes);
        }

        public static bool Verify(string salt, string password, string hash)
        {
            var saltBytes = GetBytes(salt);
            var passwordAndSaltBytes = Concat(password, saltBytes);
            var hashAttempt = ComputeHash(passwordAndSaltBytes);
            return hash == hashAttempt;
        }

        static private string ComputeHash(byte[] bytes)
        {
            using (var sha512 = SHA512.Create())
            {
                return Convert.ToBase64String(sha512.ComputeHash(bytes));
            }
        }

        static private byte[] Concat(string password, byte[] saltBytes)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            return passwordBytes.Concat(saltBytes).ToArray();
        }
    }


    public class Login
    {
        public static Employee CheckUsernameAndPassword(string email, string password)
        {
            using (DataClassesDataContext dc = new DataClassesDataContext())
            {
                var pass = HashPassword.SaltedHashPassword(password, email);
                var find = dc.Employees.Where(a => a.Email == email && a.Password == pass);
                if (find.Any())
                {
                    return find.First();
                }
                return null;
            }
        }
    }
}
