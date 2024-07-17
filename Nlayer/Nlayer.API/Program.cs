using Microsoft.EntityFrameworkCore;
using Nlayer.Core.Repositories;
using Nlayer.Core.UnitOfWorks;
using Nlayer.Data;
using Nlayer.Data.Repositories;
using Nlayer.Data.UnitOfWorks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); //IUnitw�rk g�rd��� yerde unitof work kullan�cak
builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>)); //generic oldu�u i�in type of �ekinde ekledim

builder.Services.AddDbContext<AppDbContext>(options => //sql yolu appsetting json'dan verildi
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext))!.GetName().Name); //burda migration olu�ca�� yer yani appdbcontext assembly ! bunu null olmaca��n� belirrttim
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
