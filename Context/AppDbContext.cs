using Microsoft.EntityFrameworkCore;
using ProvaSuficiencia.Domain.Comanda;
using ProvaSuficiencia.Dto.Comanda;

namespace ProvaSuficiencia.Context;


public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public required DbSet<Comanda> Comandas { get; set; }
    public required DbSet<Produto> Produtos { get; set; }
}