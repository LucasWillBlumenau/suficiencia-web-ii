using System.ComponentModel;
using System.Reflection;
using System.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using ProvaSuficiencia.Context;

var builder = WebApplication.CreateBuilder(args);


string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                            throw new InvalidCredentialException("Não é possível iniciar a aplicação pois a string de conexão padrão não foi informada");

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(static c =>
{                     
    c.CustomSchemaIds(static type => type.GetCustomAttributes<DisplayNameAttribute>()?.SingleOrDefault()?.DisplayName ?? type.Name);
});
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{  
    optionsBuilder.UseSqlite(connectionString);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
