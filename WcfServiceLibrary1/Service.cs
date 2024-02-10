using Domain.Common.Usuario;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Presentation.DTOs.Response.Usuario;

namespace CoreWCFService
{
    public class Service : IService
    {
        private readonly AppDbContext context;

        public Service(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<UsuarioDto>> Get()
        {
            return await context.AspNetUsers.Select(
                s => new UsuarioDto
                {
                    Id = s.Id,

                }
            ).ToListAsync();
        }

        public async Task<UsuarioDto> GetUserById(string Id)
        {
            return await context.AspNetUsers.Select(
                    s => new UsuarioDto
                    {
                        Id = s.Id

                    }).FirstOrDefaultAsync(s => s.Id == Id);
        }

        public async Task<bool> InsertUser(UsuarioDto User)
        {
            var entity = new Usuario()
            {
                //FirstName = User.FirstName,
                //LastName = User.LastName,
                //Username = User.Username,
                //Password = User.Password,
                //EnrollmentDate = User.EnrollmentDate
            };

            context.AspNetUsers.Add(entity);
            context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUser(UsuarioDto User)
        {
            var entity = context.AspNetUsers.FirstOrDefault(s => s.Id == User.Id);

            //entity.FirstName = User.FirstName;
            //entity.LastName = User.LastName;
            //entity.Username = User.Username;
            //entity.Password = User.Password;
            //entity.EnrollmentDate = User.EnrollmentDate;

            context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUser(string Id)
        {
            var entity = new Usuario()
            {
                Id = Id
            };

            context.AspNetUsers.Attach(entity);
            context.AspNetUsers.Remove(entity);
            context.SaveChangesAsync();

            context.Dispose();

            return true;
         
        }
    }

}
