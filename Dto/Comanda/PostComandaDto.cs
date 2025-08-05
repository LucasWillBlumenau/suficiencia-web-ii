using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ProvaSuficiencia.Dto.Comanda;


[DisplayName("PostComanda")]
public class PostComandaDto
{
    [JsonPropertyName("idUsuario")]
    public required int IdUsuario { get; set; }
    [JsonPropertyName("nomeUsuario")]
    public required string NomeUsuario { get; set; }
    [JsonPropertyName("telefoneUsuario")]
    public required string TelefoneUsuario { get; set; }
    [JsonPropertyName("produtos")]
    public required List<Produto> Produtos { get; set; }

    [DisplayName("PostComanda.Produto")]
    public class Produto
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }
        [JsonPropertyName("nome")]
        public required string Nome { get; set; }
        [JsonPropertyName("preco")]
        public required decimal Preco { get; set; }
    }
}

