using Domain.Common.Usuario;
using Presentation.DTOs.Response.Usuario;

namespace CoreWCFService
{
    [ServiceContract]
    public interface IService 
    {
        [OperationContract]
        IEnumerable<UsuarioDto> GetUsuario();

        [OperationContract]
        UsuarioDto GetUsuarioPorId(string id);

        [OperationContract]
        void CreateUsuario(Usuario entity);

        [OperationContract]
        void UpdateUsuario(Usuario entity);

        [OperationContract]
        void DeleteUsuario(string id);
    }  
}
