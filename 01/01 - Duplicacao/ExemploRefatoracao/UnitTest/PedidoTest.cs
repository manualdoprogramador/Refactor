using ExemploRefatoracao;

namespace UnitTest;

public class PedidoTest
{
    [Fact]
    public void Deve_Calcular_Pedido()
    {
        List<ItemDTO> itens = new List<ItemDTO>
        {
            new ItemDTO
            {
                Nome = "Banana",
                PrecoBase = 5,
                Quantidade = 1
            },
            new ItemDTO
            {
                Nome = "Arroz",
                PrecoBase = 20,
                Quantidade = 1
            }
        };
        
        var pedido = new Pedido();
        var total = pedido.Calcular(itens);
        Assert.Equal(25, total);
    }
    
    [Fact]
    public void Deve_Calcular_Pedido_PremiumItem()
    {
        List<ItemDTO> itens = new List<ItemDTO>
        {
            new ItemDTO
            {
                Nome = "Banana",
                PrecoBase = 5,
                Quantidade = 1
            },
            new ItemDTO
            {
                Nome = "PremiumItem",
                PrecoBase = 10,
                Quantidade = 1
            }
        };
        
        var pedido = new Pedido();
        var total = pedido.Calcular(itens);
        Assert.Equal(16, total);
    }
}