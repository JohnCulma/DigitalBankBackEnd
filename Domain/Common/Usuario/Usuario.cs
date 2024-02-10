using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Usuario
{
    [Table("Usuario", Schema = "Users")]
    public class Usuario
    {
        [Key]
        [StringLength(128)]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string UsuarioNombre { get; set; }
        public string Contrasena { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Sexo { get; set; }        
        public string IdUsuarioCreo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FechaCreo { get; set; }

    }
}
