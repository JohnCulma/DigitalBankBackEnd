using CoreWCF;
using Domain.Common.Usuario;
using Presentation.DTOs.Response.Usuario;
using System;
using System.Runtime.Serialization;

namespace CoreWCFService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Task<List<UsuarioDto>> Get();

        [OperationContract]
        Task<UsuarioDto> GetUserById(string Id);

        [OperationContract]
        Task<bool> InsertUser(UsuarioDto User);

        [OperationContract]
        Task<bool> UpdateUser(UsuarioDto User);

        [OperationContract]
        Task<bool> DeleteUser(string Id);
    }  
}
