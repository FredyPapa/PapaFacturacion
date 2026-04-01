using Blazored.Toast;
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
builder.Services.AddScoped<ICatalogoService, CatalogoService>();
builder.Services.AddScoped<ICatalogoDetalleService, CatalogoDetalleService>();
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IComprobanteService, ComprobanteService>();
builder.Services.AddScoped<IComprobanteDetalleService, ComprobanteDetalleService>();
//
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();
//
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
