using ExemploRefatoracao._03;

namespace ExemploRefatoracaoTest._03;

public class TestesGerenciadorDeLoja
{
    private GerenciadorDeLoja CriarLojaComProdutos()
    {
        var loja = new GerenciadorDeLoja();
        loja.nome = "Loja Teste";
        loja.endereco = "Rua Teste, 123";
        loja.telefone = "(11) 99999-9999";
        
        // Adicionar produtos para os testes
        loja.AdicionarProduto("Notebook", 2500.00, 5);
        loja.AdicionarProduto("Mouse", 50.00, 10);
        loja.AdicionarProduto("Teclado", 150.00, 0); // Produto sem estoque
        
        return loja;
    }

    #region Testes para ProcessarVenda

    [Fact]
    public void ProcessarVenda_ComProdutoExistenteEEstoqueSuficiente_DeveProcessarVendaComSucesso()
    {
        // Arrange (Preparar)
        var loja = CriarLojaComProdutos();
        var vendaInicialAntes = loja.totalVendas;
        var clientesAntes = loja.numeroClientes;
        var estoqueAntes = loja.produtos.FirstOrDefault().Quantidade; // Notebook tem índice 0

        // Capturar a saída do console para verificar se o recibo foi impresso
        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act (Executar)
        var produto = new Produto()
        {
            Nome = "Notebook",
            Preco = 2500.00,
            Quantidade = 1,
            FormaPagamento = "dinheiro",
            PercentualDesconto = 0,
            TemDesconto = false
        };
        var pessoa = new Pessoa()
        {
            Nome = "João Silva",
            EnderecoCliente = "Rua A, 456",
            TelefoneCliente = "(11) 88888-8888",
            Email = "joao@email.com"
        };
        loja.ProcessarVenda(produto, pessoa);

        // Assert (Verificar)
        Assert.True(loja.totalVendas > vendaInicialAntes, "Total de vendas deve ter aumentado");
        Assert.Equal(clientesAntes + 1, loja.numeroClientes);
        Assert.Equal(estoqueAntes - 1, loja.produtos.FirstOrDefault().Quantidade);
        
        var output = consoleOutput.ToString();
        Assert.Contains("RECIBO DE VENDA", output);
        Assert.Contains("João Silva", output);
        Assert.Contains("Notebook", output);
    }

    //[Fact]
    //public void ProcessarVenda_ComDesconto_DeveAplicarDescontoCorretamente()
    //{
    //    // Arrange
    //    var loja = CriarLojaComProdutos();
    //    var vendaInicialAntes = loja.totalVendas;

    //    using var consoleOutput = new StringWriter();
    //    Console.SetOut(consoleOutput);

    //    // Act - Aplicar 10% de desconto no Mouse (R$ 50,00)
    //    loja.ProcessarVenda("Mouse", 1, "Maria Santos", 
    //                       "Rua B, 789", "(11) 77777-7777", 
    //                       "maria@email.com", "987.654.321-00", 
    //                       true, 10.0, "dinheiro");

    //    // Assert
    //    // Preço original: R$ 50,00
    //    // Com 10% de desconto: R$ 45,00
    //    var valorEsperado = vendaInicialAntes + 45.00;
    //    Assert.Equal(valorEsperado, loja.totalVendas);
    //}

    //[Fact]
    //public void ProcessarVenda_ComPagamentoCartao_DeveAplicarTaxaDeCartao()
    //{
    //    // Arrange
    //    var loja = CriarLojaComProdutos();
    //    var vendaInicialAntes = loja.totalVendas;

    //    using var consoleOutput = new StringWriter();
    //    Console.SetOut(consoleOutput);

    //    // Act - Pagamento com cartão (taxa de 3%)
    //    loja.ProcessarVenda("Mouse", 1, "Pedro Lima", 
    //                       "Rua C, 321", "(11) 66666-6666", 
    //                       "pedro@email.com", "456.789.123-00", 
    //                       false, 0, "cartao");

    //    // Assert
    //    // Preço original: R$ 50,00
    //    // Com taxa de cartão (3%): R$ 50,00 + (50,00 * 0,03) = R$ 51,50
    //    var valorEsperado = vendaInicialAntes + 51.50;
    //    Assert.Equal(valorEsperado, loja.totalVendas);
    //}

    //[Fact]
    //public void ProcessarVenda_ComPagamentoCheque_DeveAplicarTaxaDeCheque()
    //{
    //    // Arrange
    //    var loja = CriarLojaComProdutos();
    //    var vendaInicialAntes = loja.totalVendas;

    //    using var consoleOutput = new StringWriter();
    //    Console.SetOut(consoleOutput);

    //    // Act - Pagamento com cheque (taxa de 5%)
    //    loja.ProcessarVenda("Mouse", 1, "Ana Costa", 
    //                       "Rua D, 654", "(11) 55555-5555", 
    //                       "ana@email.com", "789.123.456-00", 
    //                       false, 0, "cheque");

    //    // Assert
    //    // Preço original: R$ 50,00
    //    // Com taxa de cheque (5%): R$ 50,00 + (50,00 * 0,05) = R$ 52,50
    //    var valorEsperado = vendaInicialAntes + 52.50;
    //    Assert.Equal(valorEsperado, loja.totalVendas);
    //}

    //[Fact]
    //public void ProcessarVenda_ComProdutoInexistente_DeveExibirMensagemDeErro()
    //{
    //    // Arrange
    //    var loja = CriarLojaComProdutos();
    //    var vendaInicialAntes = loja.totalVendas;
    //    var clientesAntes = loja.numeroClientes;

    //    using var consoleOutput = new StringWriter();
    //    Console.SetOut(consoleOutput);

    //    // Act
    //    loja.ProcessarVenda("ProdutoInexistente", 1, "Cliente Teste", 
    //                       "Endereço", "Telefone", "Email", "CPF", 
    //                       false, 0, "dinheiro");

    //    // Assert
    //    Assert.Equal(vendaInicialAntes, loja.totalVendas); // Vendas não devem ter mudado
    //    Assert.Equal(clientesAntes, loja.numeroClientes); // Clientes não devem ter mudado
        
    //    var output = consoleOutput.ToString();
    //    Assert.Contains("Produto não encontrado!", output);
    //}

    //[Fact]
    //public void ProcessarVenda_ComEstoqueInsuficiente_DeveExibirMensagemDeErro()
    //{
    //    // Arrange
    //    var loja = CriarLojaComProdutos();
    //    var vendaInicialAntes = loja.totalVendas;
    //    var clientesAntes = loja.numeroClientes;

    //    using var consoleOutput = new StringWriter();
    //    Console.SetOut(consoleOutput);

    //    // Act - Tentar vender Teclado (que tem estoque 0)
    //    loja.ProcessarVenda("Teclado", 1, "Cliente Teste", 
    //                       "Endereço", "Telefone", "Email", "CPF", 
    //                       false, 0, "dinheiro");

    //    // Assert
    //    Assert.Equal(vendaInicialAntes, loja.totalVendas); // Vendas não devem ter mudado
    //    Assert.Equal(clientesAntes, loja.numeroClientes); // Clientes não devem ter mudado
        
    //    var output = consoleOutput.ToString();
    //    Assert.Contains("Estoque insuficiente!", output);
    //}

    //[Fact]
    //public void ProcessarVenda_ComQuantidadeMaiorQueEstoque_DeveExibirMensagemDeErro()
    //{
    //    // Arrange
    //    var loja = CriarLojaComProdutos();
    //    var vendaInicialAntes = loja.totalVendas;

    //    using var consoleOutput = new StringWriter();
    //    Console.SetOut(consoleOutput);

    //    // Act - Tentar vender 20 Mouses (só tem 10 no estoque)
    //    loja.ProcessarVenda("Mouse", 20, "Cliente Teste", 
    //                       "Endereço", "Telefone", "Email", "CPF", 
    //                       false, 0, "dinheiro");

    //    // Assert
    //    Assert.Equal(vendaInicialAntes, loja.totalVendas);
        
    //    var output = consoleOutput.ToString();
    //    Assert.Contains("Estoque insuficiente!", output);
    //}

    #endregion

    #region Testes para VerificarEstoque

    [Fact]
    public void VerificarEstoque_ComProdutoExistente_DeveExibirInformacoesDoProduto()
    {
        // Arrange
        var loja = CriarLojaComProdutos();

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        loja.VerificarEstoque("Notebook");

        // Assert
        var output = consoleOutput.ToString();
        Assert.Contains("Produto: Notebook", output);
        Assert.Contains("Preço: R$ 2500", output);
        Assert.Contains("Quantidade em estoque: 5", output);
    }

    [Fact]
    public void VerificarEstoque_ComProdutoInexistente_DeveExibirMensagemDeErro()
    {
        // Arrange
        var loja = CriarLojaComProdutos();

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        loja.VerificarEstoque("ProdutoQueNaoExiste");

        // Assert
        var output = consoleOutput.ToString();
        Assert.Contains("Produto não encontrado!", output);
        Assert.DoesNotContain("Preço:", output);
        Assert.DoesNotContain("Quantidade em estoque:", output);
    }

    [Fact]
    public void VerificarEstoque_ComProdutoSemEstoque_DeveExibirEstoqueZero()
    {
        // Arrange
        var loja = CriarLojaComProdutos();

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        loja.VerificarEstoque("Teclado"); // Teclado tem quantidade 0

        // Assert
        var output = consoleOutput.ToString();
        Assert.Contains("Produto: Teclado", output);
        Assert.Contains("Preço: R$ 150", output);
        Assert.Contains("Quantidade em estoque: 0", output);
    }

    [Theory]
    [InlineData("Notebook")]
    [InlineData("Mouse")]
    [InlineData("Teclado")]
    public void VerificarEstoque_ComDiferentesProdutosValidos_DeveExibirInformacoes(string nomeProduto)
    {
        // Arrange
        var loja = CriarLojaComProdutos();

        using var consoleOutput = new StringWriter();
        Console.SetOut(consoleOutput);

        // Act
        loja.VerificarEstoque(nomeProduto);

        // Assert
        var output = consoleOutput.ToString();
        Assert.Contains($"Produto: {nomeProduto}", output);
        Assert.Contains("Preço: R$", output);
        Assert.Contains("Quantidade em estoque:", output);
    }

    #endregion

    #region Testes que Demonstram os Problemas do Código Original

    [Fact]
    public void TesteQueMonstraProblemaDeTestabilidade_MetodoMuitoComplexo()
    {
        // Este teste demonstra como é difícil testar um método que faz muitas coisas
        // O método ProcessarVenda deveria ser quebrado em métodos menores:
        // - BuscarProduto()
        // - VerificarEstoque()
        // - CalcularValorComDesconto()
        // - AplicarTaxaPagamento()
        // - AtualizarEstoque()
        // - RegistrarVenda()
        // - ImprimirRecibo()
        
        var loja = CriarLojaComProdutos();
        
        // É difícil testar apenas uma parte específica do método
        // Por exemplo, testar apenas o cálculo de desconto sem processar toda a venda
        
        Assert.True(true, "Este teste serve para documentar o problema de testabilidade");
    }

    [Fact]
    public void TesteQueMonstraProblemaDeCodigoDuplicado()
    {
        // O código de busca de produto está duplicado em:
        // - ProcessarVenda()
        // - VerificarEstoque()  
        // - AdicionarProduto()
        
        // Isso torna os testes mais difíceis porque qualquer mudança
        // na lógica de busca precisa ser testada em múltiplos lugares
        
        var loja = CriarLojaComProdutos();
        
        Assert.True(true, "Este teste documenta o problema do código duplicado");
    }

    #endregion

    // Método para restaurar o console após os testes
    public void Dispose()
    {
        var standardOutput = new StreamWriter(Console.OpenStandardOutput());
        standardOutput.AutoFlush = true;
        Console.SetOut(standardOutput);
    }
}