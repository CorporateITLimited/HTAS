using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace HoldingTaxWebApp.Helpers
{
    public class PasswordHelper
    {

        public static string EncryptPass(string password)
        {
            try
            {
                string EncryptionKey = "29P7I4M3S5S6I81";
                byte[] clearBytes = Encoding.Unicode.GetBytes(password);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            // cs.Close();
                        }
                        password = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return password;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static string DecryptPass(string password)
        {
            try
            {
                string EncryptionKey = "29P7I4M3S5S6I81";
                byte[] cipherBytes = Convert.FromBase64String(password);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                        }
                        password = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return password;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}