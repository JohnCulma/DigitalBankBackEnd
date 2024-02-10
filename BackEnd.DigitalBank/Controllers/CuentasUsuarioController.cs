using Domain.Common.Usuario;
using Infraestructure.Helpers;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Presentation.DTOs.Answer;
using Presentation.DTOs.Request;
using Presentation.DTOs.Response;

namespace BackEnd.DigitalBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsApi")]
    public class CuentasUsuarioController : ControllerBase
    {
        #region Private variables

        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ValidacionPassword _validacionPassword = new();
        private readonly DeserializaRespuesta _deserializaRespuesta = new();
        private readonly CrearTrazabilidad _crearTrazabilidad = new();
        private readonly JwtTokenGeneracion _jwtTokenGeneracion = new();
        private readonly VerificarContrasena _verificarContrasena = new();

        //Variables
        private readonly string _procesando = "Procesando";
        private readonly string _incompleto = "Incompleto";
        private readonly string _completado = "Completado";
        private readonly string _metodo = "Completado";

        #endregion

        public CuentasUsuarioController(AppDbContext context,
            IConfiguration configuration)
        {
            this._context = context;
            this._configuration = configuration;
        }
        #region Login
        [HttpPost("Login")]
        public async Task<ActionResult<AutenticacionResponseDto>> Login(CredencialesUsuarioDto credencialesUsuarioDto)
        {
            try
            {

                EncryptarContrasena p = new();
                var a = p.BuildHash(credencialesUsuarioDto.Password, _configuration);

                #region Verify user     

                //Consulta si el usuario existe y si esta activo en el sistema
                Usuario datosUsuario = await _context.Usuario.Where(x => x.UsuarioNombre.Equals(credencialesUsuarioDto.UserName)).FirstOrDefaultAsync();
                //Si el usuario no existe o tiene otro estado que no es activo
                if (datosUsuario == null)
                {
                    //Retorna el tipo de error
                    return BadRequest(new GeneralDto()
                    {
                        title = "Login",
                        status = 400,
                        message = "Usuario no encontrado. !Error¡",
                        otherData = ""
                    });
                }

                #endregion       

                #region Verify password                

                //Se envían parámetros de validación
                bool isValid = await _validacionPassword.ValidacionPasswordUsuario(credencialesUsuarioDto.Password, _configuration);
                //Si no es valida la contraseña 
                if (!isValid)
                {

                    //Retorna el tipo de error
                    return BadRequest(new GeneralDto()
                    {
                        title = "Login",
                        status = 400,
                        message = "Contraseña no valida. !Error¡",
                        otherData = ""
                    });
                }

                #endregion



                #region Transaction Processing
                //Toma los datos si vienen de el body
                string requestBody = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
                //
                string request = await _deserializaRespuesta.DeserializaCuerpo(requestBody);

                await _crearTrazabilidad.Create(datosUsuario.Id, _procesando, _metodo, request, _context);

                #endregion



                #region Verifica si el usuario esta bloqueado

                #region VerifyPassword
                //Instancia de clase

                //Verifica las contraseñas, tanto de base de datos, como la enviada
                bool responsePwd = await _verificarContrasena.VerificarContrasenas(datosUsuario.Contrasena, credencialesUsuarioDto.Password, _configuration);
                //Si no son iguales las contraseñas
                if (!responsePwd)
                {
                    //Retorna el tipo de error
                    return BadRequest(new GeneralDto()
                    {
                        title = "Login",
                        status = 400,
                        message = "Contraseña invalida, !Error¡",
                        otherData = ""
                    });
                }

                #endregion

                #region Generate Token         

                //Genera el token, si el usuario esta activo
                AutenticacionResponseDto responseToken = await _jwtTokenGeneracion.GenerarToken(_configuration, _context, credencialesUsuarioDto);
                //Si el token no es generado
                if (responseToken == null)
                {
                    await _crearTrazabilidad.Create(datosUsuario.Id, _incompleto, _metodo, request, _context);

                    //Retorna el tipo de error
                    return BadRequest(new GeneralDto()
                    {
                        title = "Login",
                        status = 400,
                        message = "oken no generado, !Error¡",
                        otherData = ""
                    });
                }
                else
                {
                    await _crearTrazabilidad.Create(datosUsuario.Id, _completado, _metodo, request, _context);

                    //Retorna el token
                    return Ok(responseToken);
                }
                #endregion

                #endregion
            }
            catch (Exception ex)
            {

                //Retorna el tipo de error
                return BadRequest(new GeneralDto()
                {
                    title = "Login",
                    status = 400,
                    message = "Contacte al administrador",
                    otherData = ""
                });
            }
        }

        #endregion

    }
}
