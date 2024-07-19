using Autofac.Extensions.DependencyInjection;
using Autofac;
using Nlayer.Web.Modules;
using Microsoft.EntityFrameworkCore;
using Nlayer.Data;
using System.Reflection;
using Nlayer.Service.Mapping;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMemoryCache();//cache yap�s�n� dahil etmek i�in ekledim
 
builder.Services.AddAutoMapper(typeof(MapProfile));//mapprofile'nin i�inde bulundu�u assembly 

builder.Services.AddDbContext<AppDbContext>(options => //sql yolu appsetting json'dan verildi
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name); //burda migration olu�ca�� yer yani appdbcontext assembly ! bunu null olmaca��n� belirrttim
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); //k�t�phanden gelen servis eklendi
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => //containerbuilder autofacten geliyor burda autofac'ta yapt���m�z uygulamalar� buraya dahil ettik
{
    containerBuilder.RegisterModule(new RepoServiceModule());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
