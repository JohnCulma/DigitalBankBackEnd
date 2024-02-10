using Isopoh.Cryptography.Argon2;
using Microsoft.Extensions.Configuration;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Helpers
{
    public class VerificarContrasena
    {

        #region Methods

        /// <summary>
        /// Método para verificar la contraseña enviada
        /// </summary>
        /// <param name="hash">Contraseña que viene de base de datos</param>
        /// <param name="password">Password enviada</param>
        /// <param name="configuration">Configuracion</param>
        /// <returns></returns>
        public async Task<bool> VerificarContrasenas(string hash, string password, IConfiguration configuration)
        { 
            try
            {
                //Datos necesarios para validación de contraseña - vienen de la configuración de appsetting
                string Version = configuration["VersionArgon2"];
                string MemoryCost = "m=" + configuration["MemoryCost"];
                string timeCost = ",t=" + configuration["TimeCost"];
                string hashComplete = ",p=" + hash;

                var hashString =  Version + MemoryCost + timeCost + hashComplete;

                //retorno de valores tipo bool
                if (Argon2.Verify(hashString, password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
