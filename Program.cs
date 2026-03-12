using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(Program));

// repository & service DI
builder.Services.AddScoped<ProjetoTestBlue.Repository.IUsuarioRepository, ProjetoTestBlue.Repository.Impl.UsuarioRepository>();
builder.Services.AddScoped<ProjetoTestBlue.Services.IUsuarioService, ProjetoTestBlue.Services.UsuarioService>();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

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
