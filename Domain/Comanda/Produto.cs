namespace ProvaSuficiencia.Domain.Comanda;


public class Produto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public Comanda? Comanda { get; set; }
}