using Domain.Common.Trazabilidad;
using Persistence.Context;

namespace Infraestructure.Helpers
{
    public class CrearTrazabilidad
    {

        private readonly Trazabilidad _trazabilidad  = new();
        public async Task<bool> Create(string IdUser, string procesando, string Metodo, string request, AppDbContext context)
        {
            try
            {
                _trazabilidad.Id = Guid.NewGuid().ToString();
                _trazabilidad.Metodo = procesando;
                _trazabilidad.Peticion = request;
                _trazabilidad.Respuesta = request;
                _trazabilidad.IdUsuarioCreo = IdUser;
                _trazabilidad.FechaCreo = DateTime.Now;
                _trazabilidad.IdUsuarioModifico = null;
                _trazabilidad.FechaModifico = null;

                context.Add(_trazabilidad);
                //Guardado de datos 
                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
