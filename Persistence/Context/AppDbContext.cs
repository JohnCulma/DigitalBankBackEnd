using Domain.Common.Trazabilidad;
using Domain.Common.Usuario;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Persistence.Context
{
    public class AppDbContext : DbContext
    { 
    
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Usuario> Usuario { get; set; }

        public DbSet<Trazabilidad> Trazabilidad { get; set; }

        
    }
}