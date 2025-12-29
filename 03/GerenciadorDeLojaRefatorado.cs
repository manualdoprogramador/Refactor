using System;
using System.Collections.Generic;

// SOLUÇÃO REFATORADA - Eliminando os Odores de Código

public class Produto
{
    public string Nome { get; set; }
    public double Preco { get; set; }
    public int QuantidadeEstoque { get; set; }

    public Produto(string nome, double preco, int quantidadeEstoque)
    {
        Nome = nome;
        Preco = preco;
        QuantidadeEstoque = quantidadeEstoque;
    }
}

public class Cliente
{
    public string Nome { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }

    public Cliente(string nome, string endereco, string telefone, string email, string cpf)
    {
        Nome = nome;
        Endereco = endereco;
        Telefone = telefone;
        Email = email;
        Cpf = cpf;
    }
}

public class Venda
{
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public Cliente Cliente { get; set; }
    public bool TemDesconto { get; set; }
    public double PercentualDesconto { get; set; }
    public FormaPagamento FormaPagamento { get; set; }

    public Venda(Produto produto, int quantidade, Cliente cliente, bool temDesconto, 
                 double percentualDesconto, FormaPagamento formaPagamento)
    {
        Produto = produto;
        Quantidade = quantidade;
        Cliente = cliente;
        TemDesconto = temDesconto;
        PercentualDesconto = percentualDesconto;
        FormaPagamento = formaPagamento;
    }
}

public enum FormaPagamento
{
    Dinheiro,
    Cartao,
    Cheque
}

// Classe para constantes - elimina números mágicos
public static class ConfiguracaoTaxas
{
    public const double TAXA_CARTAO = 0.03; // 3%
    public const double TAXA_CHEQUE = 0.05; // 5%
    public const double PERCENTUAL_DIVISOR = 100.0;
}

public class ResultadoOperacao
{
    public bool Sucesso { get; set; }
    public string MensagemErro { get; set; }
    public object Dados { get; set; }

    public static ResultadoOperacao ComSucesso(object dados = null)
    {
        return new ResultadoOperacao { Sucesso = true, Dados = dados };
    }

    public static ResultadoOperacao ComErro(string mensagem)
    {
        return new ResultadoOperacao { Sucesso = false, MensagemErro = mensagem };
    }
}

public class GerenciadorDeLojaRefatorado
{
    private List<Produto> _produtos;
    private double _totalVendas;
    private int _numeroClientes;

    public double TotalVendas => _totalVendas;
    public int NumeroClientes => _numeroClientes;

    public GerenciadorDeLojaRefatorado()
    {
        _produtos = new List<Produto>();
        _totalVendas = 0;
        _numeroClientes = 0;
    }

    public void AdicionarProduto(string nome, double preco, int quantidade)
    {
        _produtos.Add(new Produto(nome, preco, quantidade));
    }

    // MÉTODO REFATORADO: ProcessarVenda
    // Aplicou refatorações: Extract Method, Replace Parameter with Method Object, 
    // Eliminate Magic Numbers, Single Responsibility
    public ResultadoOperacao ProcessarVenda(Venda venda)
    {
        // 1. Buscar produto (método extraído)
        var produto = BuscarProduto(venda.Produto.Nome);
        if (produto == null)
        {
            return ResultadoOperacao.ComErro("Produto não encontrado!");
        }

        // 2. Verificar estoque (método extraído)
        if (!TemEstoqueSuficiente(produto, venda.Quantidade))
        {
            return ResultadoOperacao.ComErro("Estoque insuficiente!");
        }

        // 3. Calcular valor total (método extraído)
        var valorTotal = CalcularValorTotalDaVenda(produto, venda);

        // 4. Atualizar dados (métodos extraídos)
        AtualizarEstoque(produto, venda.Quantidade);
        RegistrarVenda(valorTotal);

        // 5. Gerar recibo (método extraído)
        ImprimirRecibo(venda, produto, valorTotal);

        return ResultadoOperacao.ComSucesso(new { ValorTotal = valorTotal });
    }

    // MÉTODO REFATORADO: VerificarEstoque  
    // Aplicou refatorações: Extract Method, Replace Return Code with Exception Pattern
    public ResultadoOperacao VerificarEstoque(string nomeProduto)
    {
        // Usa o mesmo método extraído para buscar produto (elimina duplicação)
        var produto = BuscarProduto(nomeProduto);
        if (produto == null)
        {
            return ResultadoOperacao.ComErro("Produto não encontrado!");
        }

        // Método extraído para exibir informações do estoque
        ExibirInformacoesDoEstoque(produto);
        
        return ResultadoOperacao.ComSucesso(new 
        { 
            Nome = produto.Nome, 
            Preco = produto.Preco, 
            Estoque = produto.QuantidadeEstoque 
        });
    }

    // MÉTODOS EXTRAÍDOS - Elimina código duplicado e melhora legibilidade

    private Produto BuscarProduto(string nomeProduto)
    {
        foreach (var produto in _produtos)
        {
            if (produto.Nome.Equals(nomeProduto, StringComparison.OrdinalIgnoreCase))
            {
                return produto;
            }
        }
        return null;
    }

    private bool TemEstoqueSuficiente(Produto produto, int quantidadeDesejada)
    {
        return produto.QuantidadeEstoque >= quantidadeDesejada;
    }

    private double CalcularValorTotalDaVenda(Produto produto, Venda venda)
    {
        var valorBase = produto.Preco * venda.Quantidade;
        var valorComDesconto = AplicarDesconto(valorBase, venda);
        var valorFinal = AplicarTaxaPagamento(valorComDesconto, venda.FormaPagamento);
        
        return valorFinal;
    }

    private double AplicarDesconto(double valorBase, Venda venda)
    {
        if (!venda.TemDesconto)
        {
            return valorBase;
        }

        var desconto = valorBase * (venda.PercentualDesconto / ConfiguracaoTaxas.PERCENTUAL_DIVISOR);
        return valorBase - desconto;
    }

    private double AplicarTaxaPagamento(double valor, FormaPagamento formaPagamento)
    {
        return formaPagamento switch
        {
            FormaPagamento.Cartao => valor + (valor * ConfiguracaoTaxas.TAXA_CARTAO),
            FormaPagamento.Cheque => valor + (valor * ConfiguracaoTaxas.TAXA_CHEQUE),
            FormaPagamento.Dinheiro => valor,
            _ => valor
        };
    }

    private void AtualizarEstoque(Produto produto, int quantidadeVendida)
    {
        produto.QuantidadeEstoque -= quantidadeVendida;
    }

    private void RegistrarVenda(double valorVenda)
    {
        _totalVendas += valorVenda;
        _numeroClientes++;
    }

    private void ImprimirRecibo(Venda venda, Produto produto, double valorTotal)
    {
        Console.WriteLine("=== RECIBO DE VENDA ===");
        Console.WriteLine($"Cliente: {venda.Cliente.Nome}");
        Console.WriteLine($"CPF: {venda.Cliente.Cpf}");
        Console.WriteLine($"Endereço: {venda.Cliente.Endereco}");
        Console.WriteLine($"Telefone: {venda.Cliente.Telefone}");
        Console.WriteLine($"Email: {venda.Cliente.Email}");
        Console.WriteLine($"Produto: {produto.Nome}");
        Console.WriteLine($"Quantidade: {venda.Quantidade}");
        Console.WriteLine($"Preço unitário: R$ {produto.Preco:F2}");
        Console.WriteLine($"Valor total: R$ {valorTotal:F2}");
        Console.WriteLine($"Forma de pagamento: {venda.FormaPagamento}");
        Console.WriteLine("======================");
    }

    private void ExibirInformacoesDoEstoque(Produto produto)
    {
        Console.WriteLine($"Produto: {produto.Nome}");
        Console.WriteLine($"Preço: R$ {produto.Preco:F2}");
        Console.WriteLine($"Quantidade em estoque: {produto.QuantidadeEstoque}");
    }
}

// Exemplo de uso da versão refatorada
public class ProgramaRefatorado
{
    public static void Main()
    {
        var loja = new GerenciadorDeLojaRefatorado();

        // Adicionar produtos
        loja.AdicionarProduto("Notebook", 2500.00, 10);
        loja.AdicionarProduto("Mouse", 50.00, 100);
        loja.AdicionarProduto("Teclado", 150.00, 50);

        // Criar cliente
        var cliente = new Cliente("Maria Silva", "Rua A, 456", "(11) 88888-8888", 
                                 "maria@email.com", "123.456.789-00");

        // Criar produto para venda
        var produtoParaVenda = new Produto("Notebook", 2500.00, 1);

        // Criar venda
        var venda = new Venda(produtoParaVenda, 1, cliente, true, 5.0, FormaPagamento.Cartao);

        // Processar venda (método refatorado)
        var resultadoVenda = loja.ProcessarVenda(venda);
        
        if (resultadoVenda.Sucesso)
        {
            Console.WriteLine("Venda processada com sucesso!");
        }
        else
        {
            Console.WriteLine($"Erro na venda: {resultadoVenda.MensagemErro}");
        }

        // Verificar estoque (método refatorado)
        var resultadoEstoque = loja.VerificarEstoque("Mouse");
        
        if (!resultadoEstoque.Sucesso)
        {
            Console.WriteLine($"Erro: {resultadoEstoque.MensagemErro}");
        }
    }
}