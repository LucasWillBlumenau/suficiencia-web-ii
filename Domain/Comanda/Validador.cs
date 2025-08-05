using ProvaSuficiencia.Domain.Common;

namespace ProvaSuficiencia.Domain.Comanda;


public static class ValidadorComanda
{
    public static ErroValidacao? Validar(Comanda comanda)
    {

        var erro = new ErroValidacao();

        if (comanda.IdUsuario <= 0)
        {
            erro.Erros.Add(new KeyValuePair<string, string>("idUsuario", "Id de usuário é menor ou igual a zero"));
        }

        if (string.IsNullOrEmpty(comanda.NomeUsuario))
        {
            erro.Erros.Add(new KeyValuePair<string, string>("nomeUsuario", "Nome de usuário está vazio"));
        }

        var erroTelefone = ValidarTelefone(comanda.TelefoneUsuario);
        if (erroTelefone is not null)
        {
            erro.Erros.Add(new KeyValuePair<string, string>("telefoneUsuario", erroTelefone));
        }

        var erroProduto = ValidarProdutos(comanda.Produtos);
        if (erroProduto is not null)
        {
            erro.Erros.Add(new KeyValuePair<string, string>("produtos", erroProduto));
        }

        return erro.Erros.Count == 0 ? null : erro;
    }

    private static string? ValidarTelefone(string telefoneUsuario)
    {
        if (string.IsNullOrEmpty(telefoneUsuario))
        {
            return "Telefone do usuário está vazio";
        }

        if (telefoneUsuario.Length != 10 && telefoneUsuario.Length != 11)
        {
            return $"Telefone do usuário possui um número inválido de caracteres. Esperava-se 10 ou 11 e encontrou-se {telefoneUsuario.Length}";
        }

        if (telefoneUsuario.Any(c => c < '0' || c > '9'))
        {
            return $"Telefone do usuário é inválido, pois possui caracteres não numéricos";
        }

        return null;
    }

    private static string? ValidarProdutos(List<Produto> produtos)
    {

        HashSet<int> idsVistos = [];
        foreach (var produto in produtos)
        {
            if (produto.Id <= 0)
            {
                return "Um dos ids dos produtos é menor ou igual a zero";
            }

            if (idsVistos.Contains(produto.Id))
            {
                return $"O id {produto.Id} aparece em mais de um produto";
            }

            if (string.IsNullOrEmpty(produto.Nome))
            {
                return "Um dos produtos possui o nome vazio";
            }

            if (produto.Preco <= 0)
            {
                return "O preco de um dos produtos é menor ou igual a zero";
            }
            idsVistos.Add(produto.Id);
        }
        return null;
    }
}