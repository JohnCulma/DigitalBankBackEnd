using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Trazabilidad
{
    [Table("Trazabilidad", Schema = "Logs")]
    public class Trazabilidad
    {
        [Key]
        [StringLength(128)]
        public string Id { get; set; } = null!;
        public string? Metodo { get; set; }
        public string? Peticion { get; set; }
        public string? Respuesta { get; set; }
        public string IdUsuarioCreo { get; set; } = null!;
        [Column(TypeName = "datetime")]
        public DateTime FechaCreo { get; set; }
        public string? IdUsuarioModifico { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FechaModifico { get; set; }

         
    }
}
