using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ProvaSuficiencia.Dto.Comanda;


[DisplayName("PutComanda")]
public class PutComandaDto
{
    [JsonPropertyName("idUsuario")]
    public int? IdUsuario { get; set; }
    [JsonPropertyName("nomeUsuario")]
    public string? NomeUsuario { get; set; }
    [JsonPropertyName("telefoneUsuario")]
    public string? TelefoneUsuario { get; set; }
    [JsonPropertyName("produtos")]
    public List<Produto>? Produtos { get; set; }

    [DisplayName("PutComanda.Produto")]
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