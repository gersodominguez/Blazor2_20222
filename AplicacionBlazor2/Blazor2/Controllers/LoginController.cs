using Blazor2.Data;
using Datos.Interfaces;
using Datos.Repositorios;
using Microsoft.AspNetCore.Mvc;
using Modelos;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Blazor2.Controllers
{
    public class LoginController : Controller 
    {
        private readonly MySQLConfiguracion _Configuracion;
        private ILoginRepositorio _loginRepositorio;
        private IUsuarioRepositorio _usuarioRepositorio;
        public LoginController(MySQLConfiguracion configuracion)
        {
            _Configuracion = configuracion;
            _loginRepositorio = new LoginRepositorio(configuracion.CadenaConexion);
            _usuarioRepositorio = new UsuarioRepositorio(configuracion.CadenaConexion);
        }
        [HttpPost("/account/login")]
        public async Task<IActionResult>Login(Login login)
        {
            string rol = string.Empty;
            try
            {
                bool usuarioValido = await _loginRepositorio.ValidarUsuario(login);
                if(usuarioValido)
                {
                    Usuario usu = await _usuarioRepositorio.GetPorCodigo(login.Codigo);
                    if(usu.EstaActivo)
                    {
                        rol = usu.Rol;

                        //añadimos los claums del usuario y su rol

                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name,usu.Codigo),
                            new Claim(ClaimTypes.Role, rol)
                        };

                        //crear el claim principal
                        ClaimsIdentity clainsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(clainsIdentity);

                            //generador la cookies

                         await    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,claimsPrincipal,
                                                    new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTime.UtcNow.AddHours(2)});
                    }
                    else
                    {
                        return LocalRedirect("/login/El usuario esta inactivo");
                    }
                }
                else
                {
                    return LocalRedirect("/login/Datos del usuario invalidos");
                }
            }
            catch (Exception)
            {

            }
            return LocalRedirect("/");
        }

        [HttpGet("/account/logout")]
        public async Task<IActionResult>Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/login");
        }
    }
}
