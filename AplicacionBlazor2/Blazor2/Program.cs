
using Blazor2.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Blazor2.Interfaces;
using Blazor2.Servicios;
using CurrieTechnologies.Razor.SweetAlert2;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
//builder.Services.AddSingleton<WeatherForecastService>();
//var cadenaConexion = new

MySQLConfiguracion cadenaConexion= new MySQLConfiguracion (builder.Configuration.GetConnectionString("MySQL"));
builder.Services.AddSingleton(cadenaConexion);
builder.Services.AddControllers();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
builder.Services.AddScoped<IUsuarioServicio, UsuarioServicio>();
builder.Services.AddSweetAlert2();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())

{
  
    app.UseExceptionHandler("/Error");
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToPage("/_Host");

app.Run();
