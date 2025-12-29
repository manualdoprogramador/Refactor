namespace ExemploRefatoracao._02;

public class Filme_refatorado
{
    public string? FilmeId { get; set; }
    public string? Nome { get; set; }
    public string? Tipo { get; set; }

    public decimal Calular(int assentos)
    {
        return Tipo switch
        {
            "acao" => CalcularTipoAcao(assentos),
            "comedia" => CalcularTipoComedia(assentos),
            _ => throw new Exception($"tipo desconhecido: {Tipo}")
        };
    }
    
    private decimal CalcularTipoAcao(int assentos)
    {
        decimal valorEspetaculo = 40 * assentos;
        if (assentos > 30)
        {
            // Quando a audiência ultrapassa 30, cobra-se um valor extra
            valorEspetaculo += (valorEspetaculo * 30) / 100;
        }
        return valorEspetaculo;
    }

    private decimal CalcularTipoComedia(int assentos)
    {
        decimal valorEspetaculo = 30 * assentos;
        if (assentos > 20)
        {
            // Quando a audiência ultrapassa 20, cobra-se um valor extra
            valorEspetaculo += (valorEspetaculo * 20) / 100;
        }
        return valorEspetaculo;
    }
}