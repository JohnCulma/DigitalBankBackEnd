using CoreWCFService.Repository;
using Domain.Common.Usuario;
using Presentation.DTOs.Response.Usuario;

namespace CoreWCFService
{
    public class Service : IService
    {
        private readonly DataAccess dataAccess;
        private readonly IConfiguration configuration;

        public Service()
        {
            dataAccess = new DataAccess();
        }

        public IEnumerable<UsuarioDto> GetUsuario()
        {                     

            return dataAccess.ObtenerUsuarios();
        }
        
        public UsuarioDto GetUsuarioPorId(string id)
        {
            return dataAccess.GetUsuarioPorId(id);
        }

        public void CreateUsuario(Usuario entity)
        {
            dataAccess.CreateUsuario(entity);
        }

        public void UpdateUsuario(Usuario entity)
        {
            dataAccess.UpdateUsuario(entity);
        }

        public void DeleteUsuario(string id)
        {
            dataAccess.DeleteUsuario(id);
        }
    }

}
