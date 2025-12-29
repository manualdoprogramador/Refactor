using ExemploRefatoracao._02;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ExemploRefatoracaoTest._02
{
    public class ExtratoTest
    {
        // Dados de Filmes
        Dictionary<string, Filme> filmes = new Dictionary<string, Filme>
        {
            { "01", new Filme { FilmeId = "01", Nome = "007", Tipo = "acao" } },
            { "02", new Filme { FilmeId = "02", Nome = "Debi & Lóide", Tipo = "comedia" } },
            { "03", new Filme { FilmeId = "03", Nome = "Velozes & Furiosos", Tipo = "acao" } }
        };
        
        
        Empresa empresa = new Empresa
        {
            Nome = "Cinema do Bairro",
            Apresentacoes = new List<Apresentacao>
            {
                new Apresentacao { FilmeId = "01", Assento = 55 },
                new Apresentacao { FilmeId = "02", Assento = 35 },
                new Apresentacao { FilmeId = "03", Assento = 40 }
            }
        };


        [Fact] // Exemplo usando xUnit
        public void TestarGeracaoExtrato()
        {
            var extrato = new Extrato();
            string esperado = "Extrato para Cinema do Bairro\n" +
                              " 007: R$ 2.860,00 (55 assentos)\n" +
                              " Debi & Lóide: R$ 1.260,00 (35 assentos)\n" +
                              " Velozes & Furiosos: R$ 2.080,00 (40 assentos)\n" +
                              "Valor Total é R$ 6.200,00\n";

            // Act
            string resultadoAtual = extrato.Gerar(empresa, filmes);

            // Assert
            Assert.Equal(esperado, resultadoAtual);
        }
        
    }

}
