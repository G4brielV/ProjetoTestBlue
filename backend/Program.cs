using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Data;
using System.Text;
using ProjetoTestBlue.Services;
using ProjetoTestBlue.Services.Impl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuração do JWT
var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false; 
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
// Swagger configuration with JWT support
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http, // Alterado de ApiKey para Http
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Cole apenas o seu token JWT abaixo (não precisa de escrever 'Bearer')"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// AutoMapper configuration
builder.Services.AddAutoMapper(typeof(Program));

// repository & service DI
builder.Services.AddScoped<ProjetoTestBlue.Repository.IUsuarioRepository, ProjetoTestBlue.Repository.Impl.UsuarioRepository>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddScoped<ProjetoTestBlue.Repository.ICardRepository, ProjetoTestBlue.Repository.Impl.CardRepository>();

builder.Services.AddScoped<ProjetoTestBlue.Repository.ITodoListRepository, ProjetoTestBlue.Repository.Impl.TodoListRepository>();

builder.Services.AddScoped<ICardService, CardService>();

builder.Services.AddScoped<ITodoListService, TodoListService>();

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// CORS 
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyHeader()
              .AllowAnyMethod();
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
app.UseAuthorization();

app.UseCors("FrontEndPolicy");
app.MapControllers();

// Aplicar Migrations automaticamente ao subir com docker
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    
    for (int i = 0; i < 5; i++)
    {
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
            logger.LogInformation("Banco de dados e tabelas criados com sucesso.");
            break; 
        }
        catch (Exception ex)
        {
            logger.LogWarning($"Tentativa {i + 1}: Banco ainda não disponível. Aguardando...");
            Thread.Sleep(5000); 
            if (i == 4) logger.LogError(ex, "Não foi possível conectar ao banco após várias tentativas.");
        }
    }
}

app.Run();
