using System;
using System.Security.Cryptography;

namespace VehicleTrackingSystem.Security.Handlers
{
    public class CryptographyHandler : ICryptographyHandler
    {
        public string GeneratePasswordHash(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string passwordHash = Convert.ToBase64String(hashBytes);
            return passwordHash;
        }
        public bool VerifyGeneratedHash(string password, string savedPasswordHash)
        {
            byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf21 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash1 = pbkdf21.GetBytes(20);
            int ok = 1;
            for (int i = 0; i < 20; i++)
                if (hashBytes[i + 16] != hash1[i])
                    ok = 0;
            if (ok == 1) return true;
            else return false;
        }
    }
}
