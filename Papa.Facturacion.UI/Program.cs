using Microsoft.EntityFrameworkCore;
using Papa.Facturacion.Business.Implementations;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess.Context;
using Papa.Facturacion.Repositories.Implementations;
using Papa.Facturacion.Repositories.Interfaces;
using Papa.Facturacion.UI.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//DbContext
builder.Services.AddDbContext<PapaFacturacionContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("cnFacturacion"));
});

//Inyección de dependencia
builder.Services.AddScoped<ICatalogoRepository, CatalogoRepository>();
builder.Services.AddScoped<ICatalogoDetalleRepository, CatalogoDetalleRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IComprobanteRepository, ComprobanteRepository>();
builder.Services.AddScoped<IComprobanteDetalleRepository, ComprobanteDetalleRepository>();
//
builder.Services.AddScoped<IClienteService, ClienteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
