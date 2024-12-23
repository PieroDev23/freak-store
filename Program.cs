using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using freak_store.Data;
using freak_store.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la cadena de conexión a PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnection")
    ?? throw new InvalidOperationException("Connection string 'PostgreSQLConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

// Configuración de identidad de ASP.NET Core
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
{
    options.SignIn.RequireConfirmedAccount = false; // Desactivar confirmación de cuenta para facilitar el registro
    options.Password.RequireNonAlphanumeric = false; // Ajustes de complejidad de contraseña
})
.AddEntityFrameworkStores<ApplicationDbContext>();

// Agrega servicios de Razor Pages y MVC
builder.Services.AddControllersWithViews();

// Configuración de Swagger solo en desarrollo
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API de Freak Store",
        Version = "v1",
        Description = "Documentación de la API de Freak Store usando Swagger"
    });
});

// Configuración del servicio CheapShark con HttpClient y headers personalizados
builder.Services.AddHttpClient<CheapSharkService>(client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("User-Agent", "Freak Store");
});

var app = builder.Build();

// Aplicar migraciones automáticas en entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.Database.Migrate(); // Ejecuta las migraciones automáticamente
    }

    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Freak Store v1");
        c.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Ruta por defecto para el controlador Home
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Ruta específica para el filtro por categoría en el catálogo
app.MapControllerRoute(
    name: "catalog",
    pattern: "Catalog/{categoryId?}",
    defaults: new { controller = "Catalog", action = "Index" }
);

// Ruta para acceder a las ofertas de CheapShark
app.MapControllerRoute(
    name: "cheapShark",
    pattern: "CheapShark",
    defaults: new { controller = "CheapShark", action = "Index" }
);

app.MapRazorPages();

app.Run();
