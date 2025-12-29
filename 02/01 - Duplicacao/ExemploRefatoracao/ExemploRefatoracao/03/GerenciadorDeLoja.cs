namespace ExemploRefatoracao._03;
public class GerenciadorDeLoja
{
    // Atributos sem encapsulamento
    public string nome;
    public string endereco;
    public string telefone;
    public List<Produto> produtos { get; set; }
    public double totalVendas;
    public int numeroClientes;
    
    public GerenciadorDeLoja()
    {
        produtos = new List<Produto>();              
        numeroClientes = 0;
    }

    // MÉTODO MUITO LONGO com muitas responsabilidades
    public void ProcessarVenda(Produto produto, Pessoa pessoa)
    {                                    
        if (!produtos.Any(x => x.Nome == produto.Nome))
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }
        
        var produtoEstoque = produtos?.FirstOrDefault(x => x.Nome == produto.Nome); 
        if (produtoEstoque.Quantidade < produto.Quantidade)
        {
            Console.WriteLine("Estoque insuficiente!");
            return;
        }        
        double valorTotal = produto.Preco * produto.Quantidade;                
        if (produto.TemDesconto)
        {
            // Número mágico: 100
            valorTotal = valorTotal - (valorTotal * produto.PercentualDesconto / 100);
        }
        
        if (produto.FormaPagamento == "cartao")        
            valorTotal = valorTotal + (valorTotal * 0.03); // 3% de taxa
        
        else if (produto.FormaPagamento == "cheque")        
            valorTotal = valorTotal + (valorTotal * 0.05); // 5% de taxa

        produtoEstoque.Quantidade -= produto.Quantidade;

        // Registrar venda (mais código duplicado)        
        numeroClientes++;
        totalVendas += valorTotal;
        // Imprimir recibo (método muito longo continua...)
        Console.WriteLine("=== RECIBO DE VENDA ===");
        Console.WriteLine("Cliente: " + pessoa.Nome);
        Console.WriteLine("CPF: " + pessoa.Cpf);
        Console.WriteLine("Endereço: " + pessoa.EnderecoCliente);
        Console.WriteLine("Telefone: " + pessoa.TelefoneCliente);
        Console.WriteLine("Email: " + pessoa.Email);
        Console.WriteLine("Produto: " + produto.Nome);
        Console.WriteLine("Quantidade: " + produto.Quantidade);
        Console.WriteLine("Preço unitário: R$ " + produto.Preco);
        Console.WriteLine("Valor total: R$ " + valorTotal);
        Console.WriteLine("Forma de pagamento: " + produto.FormaPagamento);
        Console.WriteLine("======================");
    }

    // CÓDIGO DUPLICADO - mesma lógica de busca do método anterior
    public void VerificarEstoque(string nomeProduto)
    {
             
        var produto = produtos?.FirstOrDefault(x => x.Nome == nomeProduto);
        if(produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            return;
        }
        
        Console.WriteLine("Produto: " + nomeProduto);
        Console.WriteLine("Preço: R$ " + produto.Preco);
        Console.WriteLine("Quantidade em estoque: " + produto.Quantidade);
    }

    // Outro método com CÓDIGO DUPLICADO
    public void AdicionarProduto(string nome, double preco, int quantidade)
    {
        // Verificar se já existe (código duplicado novamente)
        if (produtos.Any(x => x.Nome == nome))
        {
            Console.WriteLine("Produto já existe!");
            return;
        }
        produtos.Add(new Produto { Nome = nome, Preco = preco, Quantidade = quantidade});        
    }
}