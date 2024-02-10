using Isopoh.Cryptography.Argon2;
using Isopoh.Cryptography.SecureArray;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Helpers
{
    public class EncryptarContrasena
    {
        #region Method

        /// <summary>
        /// Método que construye las contraseña encriptada
        /// </summary>
        /// <param name="password">Contraseña</param>
        /// <returns></returns>
        public string BuildHash(string password, IConfiguration _configuration)
        {
            try
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] salt = new byte[Convert.ToInt32(_configuration["Salt"])];

                var Rng = RandomNumberGenerator.GetBytes(salt.Length);

                var config = new Argon2Config
                {
                    Type = Argon2Type.DataIndependentAddressing,
                    Version = Argon2Version.Nineteen,
                    TimeCost = Convert.ToInt32(_configuration["TimeCost"]),
                    MemoryCost = Convert.ToInt32(_configuration["MemoryCost"]),
                    Lanes = 5,
                    Threads = Environment.ProcessorCount,
                    Password = passwordBytes,
                    Salt = salt,
                    HashLength = Convert.ToInt32(_configuration["HashLength"])
                };

                var argon2A = new Argon2(config);
                string hashString;
                using (SecureArray<byte> hashA = argon2A.Hash())
                {
                    hashString = config.EncodeString(hashA.Buffer);
                }

                return hashString;

            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
