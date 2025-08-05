using System.ComponentModel;
using System.Text.Json.Serialization;

namespace ProvaSuficiencia.Dto.Comanda;

[DisplayName("GetComandas")]
public class GetComandasDto
{
    [JsonPropertyName("idUsuario")]
    public required int IdUsuario { get; set; }
    [JsonPropertyName("nomeUsuario")]
    public required string NomeUsuario { get; set; }
    [JsonPropertyName("telefoneUsuario")]
    public required string TelefoneUsuario { get; set; }
}
