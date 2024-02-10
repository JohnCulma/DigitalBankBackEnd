using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Helpers
{
    public class ValidacionPassword
    {
        #region Methods       

        /// <summary>
        /// Método para validar la contraseña
        /// </summary>
        /// <param name="password">Contraseña del usuario</param>
        /// <returns></returns>
        public async Task<bool> ValidacionPasswordUsuario(string password, IConfiguration configuration)
        {
            bool hasMinLength = password.Length >= int.Parse(configuration["PasswordLength"]);
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasLowerCase = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecialChar = password.Any(c => !char.IsLetterOrDigit(c));

            return hasMinLength && hasUpperCase && hasLowerCase && hasDigit && hasSpecialChar;
        }

        #endregion
    }
}

