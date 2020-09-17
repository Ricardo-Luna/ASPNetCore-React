using System.Linq;
using System.Threading.Tasks;
using Dominio;
using Microsoft.AspNetCore.Identity;

namespace Persistencia {
    public class DataPrueba {
        public static async Task InsertarData (CursosOnlineContext context, UserManager<Usuario> usuarioManager) {
            if (!usuarioManager.Users.Any ()) {
                var usuario = new Usuario { NombreCompleto = "Ricardo Luna", UserName = "ricardo.luna", Email = "rickylunat@hotmail.com" };
                await usuarioManager.CreateAsync(usuario,"Ri10lu18%");//password Ri10lu18% user ricardo.luna
                
            }
        }
    }
}