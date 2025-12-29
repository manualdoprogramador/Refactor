namespace ExemploRefatoracao._02;

public class Extrato_Refatorado
{
    public string Gerar(Empresa empresa, Dictionary<string, Filme_refatorado> filmes)
    {
        decimal valorTotal = 0;
        string resultado = $"Extrato para {empresa.Nome}\n";
        foreach (var apresentacao in empresa.Apresentacoes)
        {
            var filme = filmes[apresentacao.FilmeId];
            decimal valorEspetaculo = filme.Calular(apresentacao.Assento);
            resultado += $" {filme.Nome}: {Formatar(valorEspetaculo)} ({apresentacao.Assento} assentos)\n";
            valorTotal += valorEspetaculo;
        }
        
        resultado += $"Valor Total é {Formatar(valorTotal)}\n";
        return resultado;
    }
    

    private string Formatar(decimal umNumero)
    {
        return $"{umNumero:C2}";
    }
}