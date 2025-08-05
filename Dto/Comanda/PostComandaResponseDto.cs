using System.ComponentModel;

namespace ProvaSuficiencia.Dto.Comanda;


[DisplayName("PostComandaResponse")]
public class PostComandaResponseDto
{
    public int Id { get; set; }
    public int IdUsuario { get; set; }
    public string NomeUsuario { get; set; } = "";
    public string TelefoneUsuario { get; set; } = "";
    public IEnumerable<Produto> Produtos { get; set; } = [];

    [DisplayName("PostComandaResponse.Produto")]
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = "";
        public decimal Preco { get; set; }
    }
}