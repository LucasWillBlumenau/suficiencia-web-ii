using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProvaSuficiencia.Domain.Comanda;

namespace ProvaSuficiencia.Context;


public class AppDbContext(DbContextOptions options) : IdentityDbContext<IdentityUser>(options)
{
    public required DbSet<Comanda> Comandas { get; set; }
    public required DbSet<Produto> Produtos { get; set; }
}