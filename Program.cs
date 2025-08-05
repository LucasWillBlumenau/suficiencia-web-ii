using System.ComponentModel;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using ProvaSuficiencia.Context;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                            throw new InvalidCredentialException("Não é possível iniciar a aplicação pois a string de conexão padrão não foi informada");
string serverSecret = GenerateServerSecret();

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "FreeTrained",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(serverSecret))
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
    {
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders(); ;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.MapIdentityApi<IdentityUser>();
app.Run();


static string GenerateServerSecret()
{
    byte[] randomBytes = new byte[128];
    using (var random = RandomNumberGenerator.Create())
    {
        random.GetBytes(randomBytes);
    }
    return Convert.ToBase64String(randomBytes);
}