using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ProvaSuficiencia.Dto.Comanda;


[DisplayName("GetComanda")]
public class GetComandaDto
{
    [JsonPropertyName("idUsuario")]
    public int IdUsuario { get; set; }
    [JsonPropertyName("nomeUsuario")]
    public required string NomeUsuario { get; set; }
    [JsonPropertyName("telefoneUsuario")]
    public required string TelefoneUsuario { get; set; }

    [JsonPropertyName("produtos")]
    public required IEnumerable<Produto> Produtos { get; set; }


    [DisplayName("GetComanda.Produto")]
    public class Produto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public required string Nome { get; set; }
        [JsonPropertyName("preco")]
        public decimal Preco { get; set; }
    }
}
