
using Dapper;
using Domain.Common.Usuario;
using Microsoft.Data.SqlClient;
using Presentation.DTOs.Response.Usuario;
using System.Data;
using static Dapper.SqlMapper;

namespace CoreWCFService.Repository
{

    public class DataAccess
    {
        private readonly string connectionString;
        private readonly IConfiguration configuration;
        /// <summary>
        /// Acceso a los datos
        /// </summary>
        public DataAccess()
        {
            configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

            connectionString = configuration["ConnectionStrings:database"];
        }

        #region MyRegion
        /// <summary>
        /// Obtiene todos los usuarios
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UsuarioDto> ObtenerUsuarios()
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    return db.Query<UsuarioDto>("dbo.SP_ObtenerUsuarios", commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Obtiene el usuario por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UsuarioDto GetUsuarioPorId(string id)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    DynamicParameters param = new();
                    param.Add("Id", id, DbType.String);
                    return db.QueryFirstOrDefault<UsuarioDto>("dbo.SP_ObtenerUsuarioPorId", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// Crea el usuario
        /// </summary>
        /// <param name="entity"></param>
        public void CreateUsuario(Usuario entity)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("Id", entity.Id, DbType.String);
                param.Add("Nombre", entity.Nombre, DbType.String);
                param.Add("UsuarioNombre", entity.UsuarioNombre, DbType.String);
                param.Add("Contrasena", entity.Contrasena, DbType.String);
                param.Add("FechaNacimiento", entity.FechaNacimiento, DbType.DateTime);
                param.Add("Sexo", entity.Sexo, DbType.String);
                param.Add("IdUsuarioCreo", entity.IdUsuarioCreo, DbType.String);
                param.Add("FechaCreo", entity.FechaCreo, DbType.DateTime);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("dbo.SP_CrearUsuario", param, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Actualiza el usuario
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUsuario(Usuario entity)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("Id", entity.Id, DbType.String);
                param.Add("Nombre", entity.Nombre, DbType.String);
                param.Add("UsuarioNombre", entity.UsuarioNombre, DbType.String);
                param.Add("Contrasena", entity.Contrasena, DbType.String);
                param.Add("FechaNacimiento", entity.FechaNacimiento, DbType.DateTime);
                param.Add("Sexo", entity.Sexo, DbType.String);
                param.Add("IdUsuarioCreo", entity.IdUsuarioCreo, DbType.String);
                param.Add("FechaCreo", entity.FechaCreo, DbType.DateTime);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("dbo.SP_ActualizarUsuario", entity, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// Elimiana el usuario
        /// </summary>
        /// <param name="id"></param>
        public void DeleteUsuario(string id)
        {
            try
            {
                DynamicParameters param = new();
                param.Add("Id", id, DbType.String);

                using (IDbConnection db = new SqlConnection(connectionString))
                {
                    db.Execute("dbo.SP_EliminarUsuario", param, commandType: CommandType.StoredProcedure);
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
