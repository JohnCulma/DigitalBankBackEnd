using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs.Request
{
    public class CredencialesUsuarioDto
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string UserName { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Password { get; set; } = null!;

    }
}
