using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.EntityFrameworkCore;
using Papa.Facturacion.Business.Implementations;
using Papa.Facturacion.Business.Interfaces;
using Papa.Facturacion.DataAccess.Context;
using Papa.Facturacion.Repositories.Implementations;
using Papa.Facturacion.Repositories.Interfaces;
using Papa.Facturacion.UI.Components;
using Scrutor;

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
//Registrando las inyecciones de dependencia con Scrutor
builder.Services.Scan(p => p
    .FromAssemblies(typeof(IClienteRepository).Assembly, typeof(IClienteService).Assembly)
    .AddClasses(false)
    .UsingRegistrationStrategy(RegistrationStrategy.Skip)
    .AsImplementedInterfaces()
    .WithScopedLifetime()
);
//
builder.Services.AddBlazorBootstrap();
builder.Services.AddSweetAlert2();
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
