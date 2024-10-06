using MiBotica.SolPedido.Entidades.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiBotica.SolPedido.AccesoDatos.Core;
using MiBotica.SolPedido.Entidades.Base;

namespace MiBotica.SolPedido.LogicaNegocio.Core
{
    public class UsuarioLN : BaseLN
    {
        public List<Usuario> ListaUsuarios()
        {

            try
            {
                return new UsuarioDA().ListaUsuarios(); // error?
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertarUsuario(Usuario usuario)
        {
            try
            {
                new UsuarioDA().InsertarUsuario(usuario);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Usuario ObtenerUsuario(int id)
        {
            return new UsuarioDA().ObtenerUsuario(id);
        }

        public void ActualizarUsuario(Usuario usuario)
        {
            new UsuarioDA().ActualizarUsuario(usuario);
        }

        public void EliminarUsuario(int id)
        {
            new UsuarioDA().EliminarUsuario(id);
        }

        public Usuario BuscarUsuario(Usuario Usuario)
        {
            try
            {
                return new UsuarioDA().BuscarUsuario(Usuario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                throw;
            }
        }

    }
}
