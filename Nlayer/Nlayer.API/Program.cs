using Microsoft.EntityFrameworkCore;
using Nlayer.Core.Repositories;
using Nlayer.Core.Services;
using Nlayer.Core.UnitOfWorks;
using Nlayer.Data;
using Nlayer.Data.Repositories;
using Nlayer.Data.Repositories.Nlayer.Core.Repositories;
using Nlayer.Data.UnitOfWorks;
using Nlayer.Service.Mapping;
using Nlayer.Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //IUnitwýrk gördüðü yerde unitof work kullanýcak
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>)); //generic olduðu için type of þekinde ekledim
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));//servislerin dependecy tarafý
builder.Services.AddScoped<IProductRepository, ProductRepository>();//Iproductrepository gördüðü yerde product repository kullanýcak
builder.Services.AddScoped<IProductService,ProductService>();//IProductService gördüðü yerde ProductService kullanýcak
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();    
builder.Services.AddAutoMapper(typeof(MapProfile));//mapprofile'nin içinde bulunduðu assembly 

builder.Services.AddDbContext<AppDbContext>(options => //sql yolu appsetting json'dan verildi
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name); //burda migration oluþcaðý yer yani appdbcontext assembly ! bunu null olmacaðýný belirrttim
    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
