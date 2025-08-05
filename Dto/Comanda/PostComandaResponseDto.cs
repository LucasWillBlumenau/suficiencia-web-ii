using System.ComponentModel;

namespace ProvaSuficiencia.Dto.Comanda;


[DisplayName("PostComandaResponse")]
public class PostComandaResponseDto
{
    public required int Id { get; set; }
    public required int IdUsuario { get; set; }
    public required string NomeUsuario { get; set; }
    public required string TelefoneUsuario { get; set; }
    public required IEnumerable<Produto> Produtos { get; set; }

    [DisplayName("PostComandaResponse.Produto")]
    public class Produto
    {
        public required int Id { get; set; }
        public required string Nome { get; set; }
        public required decimal Preco { get; set; }
    }
}