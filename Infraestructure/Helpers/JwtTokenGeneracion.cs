using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Persistence.Context;
using Presentation.DTOs.Request;
using Presentation.DTOs.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Helpers
{
    public class JwtTokenGeneracion
    {

        #region Create token first time
        /// <summary>
        /// Método para generar el password
        /// </summary>
        /// <param name="idUser">Id del usuario</param>
        /// <param name="idRol">IdRoles de usuario</param>
        /// <returns></returns>
        public async Task<AutenticacionResponseDto> GenerarToken(IConfiguration _configuration, AppDbContext _context, CredencialesUsuarioDto credencialesUsuarioDto)
        {
            try
            {
                //Claims de usuario
                var user = _context.Usuario.Where(x => x.UsuarioNombre.Equals(credencialesUsuarioDto.UserName)).FirstOrDefault();

                var claims = new List<Claim>()
            {
                new Claim("Id", user.Id != null ? user.Id: ""),

            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["KeyJwt"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var expiration = DateTime.UtcNow.AddDays(5);

                var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                    expires: expiration, signingCredentials: creds);
                return new AutenticacionResponseDto()
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                    Expiration = expiration,
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}

