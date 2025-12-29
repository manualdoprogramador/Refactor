using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploRefatoracao._03
{
    public class Produto
    {
        public string? Nome { get; set; }
        public double Preco { get; set; }
        public int Quantidade { get; set; }
        public string? FormaPagamento { get; set; }
        public double PercentualDesconto { get; set; }
        public bool TemDesconto { get; set; }
    }
}
