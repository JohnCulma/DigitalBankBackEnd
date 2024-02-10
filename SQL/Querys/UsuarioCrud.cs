using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL.Querys
{
    [ExcludeFromCodeCoverage]
    public class UsuarioCrud
    {
        public static string GetUsuario => "SELECT * FROM [Users].[Usuario]";
    }
}
