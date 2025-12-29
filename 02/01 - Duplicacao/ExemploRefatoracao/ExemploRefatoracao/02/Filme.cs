using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploRefatoracao._02
{
    public class Filme
    {
        public string? FilmeId { get; set; }
        public string? Nome { get; set; }
        public string? Tipo { get; set; }
        
        public decimal CalcularValor(int assentos)
        {
            switch (Tipo)
            {
                case "acao":
                    return CalcularValorAcao(assentos);
                case "comedia":
                    return CalcularValorComedia(assentos);
                default:
                    throw new Exception($"tipo desconhecido: {Tipo}");
            }
        }
        
        private decimal CalcularValorAcao(int assentos)
        {
            decimal valorEspetaculo = 40 * assentos;
            if (assentos > 30)
            {
                // Quando a audiência ultrapassa 30, cobra-se um valor extra
                valorEspetaculo += (valorEspetaculo * 30) / 100;
            }
            return valorEspetaculo;
        }

        private decimal CalcularValorComedia(int assentos)
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
}
