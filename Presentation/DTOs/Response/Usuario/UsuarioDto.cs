using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs.Response.Usuario
{
    public class UsuarioDto
    {
        public string? Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string? Sexo { get; set; }
    }
}
