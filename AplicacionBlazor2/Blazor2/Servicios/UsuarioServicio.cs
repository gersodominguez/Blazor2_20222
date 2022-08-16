using Blazor2.Data;
using Blazor2.Interfaces;
using Datos.Interfaces;
using Datos.Repositorios;
using Modelos;

namespace Blazor2.Servicios
{

        public class UsuarioServicio : IUsuarioServicio
    {
        private readonly MySQLConfiguracion _Configuracion;
        private IUsuarioRepositorio usuarioRepositorio;

        public UsuarioServicio(MySQLConfiguracion configuracion)
        {
            _Configuracion = configuracion;
            usuarioRepositorio = new UsuarioRepositorio(configuracion.CadenaConexion);
        }

        public async Task<bool> Actualizar(Usuario usuario)
        {
           return await  usuarioRepositorio.Actualizar(usuario);
        }

        public async  Task<bool> Eliminar(Usuario usuario)
        {
            return await  usuarioRepositorio.Eliminar(usuario);
        }

        public async Task<IEnumerable<Usuario>> GetLista()
        {
            return await usuarioRepositorio.GetLista();
        }

        public async  Task<Usuario> GetPorCodigo(string codigo)
        {
            return await  usuarioRepositorio.GetPorCodigo(codigo);
        }

        public async Task<bool> Nuevo(Usuario usuario)
        {
            return await usuarioRepositorio.Nuevo(usuario);
        }
    }
}

//public async Task<bool> nuevo(Usuario usuario)
//{
//    return await usuarioRepositorio.nuevo(usuario);//
//}