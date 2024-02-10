using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.DTOs.Response
{
    public class AutenticacionResponseDto
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
