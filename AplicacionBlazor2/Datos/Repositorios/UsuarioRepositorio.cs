using Dapper;
using Datos.Interfaces;
using Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Repositorios
{

    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private string CadenaConexion;

        public UsuarioRepositorio(string cadenaConexion)
        {
            CadenaConexion = cadenaConexion;
        }

        private MySqlConnection Conexion()
        {
            return new MySqlConnection(CadenaConexion);
        }
        public Task<bool> Actualizar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public  Task<bool> Eliminar(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> GetPorCodigo(string codigo)
        {
           Usuario user = new Usuario();
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"SELEC * FROM usuario WHERE Codigo = @Codigo;";
                user = await conexion.QueryFirstAsync<Usuario>(sql, new {codigo});
            }
            catch (Exception ex)
            {
            }
            return user;
        }

        public async  Task<IEnumerable<Usuario>> GetLista()
        {
            IEnumerable<Usuario> lista = new List<Usuario>();
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"SELEC * FROM usuario;";
                lista = await conexion.QueryAsync<Usuario>(sql);
            }
            catch (Exception ex)
            {
            }
            return lista;
        }

        public async  Task<bool> Nuevo(Usuario usuario)
        {
            bool result = false;
            try
            {
                using MySqlConnection conexion = Conexion();
                await conexion.OpenAsync();
                string sql = @"INSERT INTO usuario(Codigo, Nombre, Clave,Rol,EstaActivo
                               VALUES(@Codigo, @Nombre, @Clave, @Rol, @EstaActivo);";
                result = Convert.ToBoolean(await conexion.ExecuteAsync(sql, usuario));
            }
            catch (Exception)
            {
            }
            return result;
        }
    }
}
