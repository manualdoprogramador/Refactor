using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploRefatoracao
{
    public class Pedido
    {
        public decimal Calcular(List<ItemDTO> itens)
        {
            decimal total = 0;
            foreach (var item in itens)
            {
                decimal precoItem = item.Quantidade * item.PrecoBase;
                if (item.Nome == "PremiumItem")
                {
                    precoItem *= 1.10m; // 10% de markup
                }
                total += precoItem;
            }
            
            return total;
        }

        public void ImprimirPedido(List<ItemDTO> itens, bool temImprecao)
        {
            if (!temImprecao)
                return;
            
            var total = Calcular(itens);
            string mensagem = $"--- Detalhes do Pedido ---\n";
            mensagem += $"Total a Pagar: R$ {total:F2}\n";
            mensagem += "--------------------------";
            Console.WriteLine(mensagem);
        }
        
        public void ImprimirPedidoEmHtml(List<ItemDTO> itens, bool temImprecao)
        {
            if (!temImprecao)
                return;
            
            var total = Calcular(itens);
            string mensagem = $"<html>";
            mensagem += $"--- Detalhes do Pedido ---\n";
            mensagem += $"Total a Pagar: R$ {total:F2}\n";
            mensagem += "--------------------------";
            mensagem += $"</html>";
            Console.WriteLine(mensagem);
        }
    }
}
