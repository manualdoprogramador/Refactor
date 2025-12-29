using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExemploRefatoracao
{
    public class Pedido
    {
        public decimal CalculatePrice(List<ItemDTO> itens, bool temImprecao)
        {
            decimal total = 0;
            string mensagem = "";

            // 1. Lógica de Cálculo (simples)
            foreach (var item in itens)
            {
                decimal precoItem = item.Quantidade * item.PrecoBase;
                if (item.Nome == "PremiumItem")
                {
                    precoItem *= 1.10m; // 10% de markup
                }
                total += precoItem;
            }

            // 2. Lógica de Impressão/Formatação (MISTA)
            if (temImprecao)
            {
                // Formatação para Console
                mensagem += $"--- Detalhes do Pedido ---\n";
                mensagem += $"Total a Pagar: R$ {total:F2}\n";
                mensagem += "--------------------------";
                Console.WriteLine(mensagem);
            }

            return total;
        }

        public decimal CalculatePriceHtml(List<ItemDTO> itens, bool temImprecaoHtml)
        {
            decimal total = 0;
            string mensagem = "";

            // 1. Lógica de Cálculo (simples)
            foreach (var item in itens)
            {
                decimal precoItem = item.Quantidade * item.PrecoBase;
                if (item.Nome == "PremiumItem")
                {
                    precoItem *= 1.10m; // 10% de markup
                }
                total += precoItem;
            }

            // 2. Lógica de Impressão/Formatação (MISTA)
            if (temImprecaoHtml)
            {
                // Formatação para Console
                mensagem += $"--- Detalhes do Pedido ---\n";
                mensagem += $"Total a Pagar: R$ {total:F2}\n";
                mensagem += "--------------------------";
                Console.WriteLine(mensagem);
            }

            return total;
        }
    }

    /* 
     
    Como você está criando uma playlist em C#, é importante mostrar um exemplo prático (mesmo que simplificado) de um design quebrado. Este exemplo pode ser uma versão simplificada do que será trabalhado em detalhes no Módulo 1.
Você deve usar este código para ilustrar a Duplicação de Código e a dificuldade em adicionar uma nova funcionalidade (como a impressão em HTML).
Exemplo 1: Código C# "Antes da Refatoração" (Simulação de Code Smell)
Imagine uma função simples que calcula o preço de um pedido e formata o resultado para impressão em console.

EXEMPLO

Pontos de Discussão com o Exemplo C#
1. A Dor do Código Misto (Shotgun Surgery / Divergent Change):
    ◦ Problema: Se o cliente solicitar uma versão em HTML, somos forçados a duplicar toda a função CalculatePrice e modificar apenas a parte da formatação (Ponto 2).
    ◦ Consequência: Se a lógica de cálculo (Ponto 1) mudar no futuro (ex: alterar o markup para PremiumItem), teremos que atualizar ambas as cópias da função (CalculatePriceConsole e CalculatePriceHtml), o que introduz o risco de inconsistência e é uma ameaça em programas de longa duração.
2. Solução Prevista (Introdução):
    ◦ O Objetivo da Refatoração: Aplicar Split Phase (154) (Separação de Fases) para que a lógica de Cálculo (Ponto 1) fique totalmente separada da lógica de Apresentação/Formatação (Ponto 2).
    ◦ Com a refatoração, a função principal se torna curta e clara, revelando a intenção de apenas dispor a impressão.
Você pode finalizar o vídeo dizendo que, embora esse seja o objetivo, o primeiro passo real é sempre garantir a segurança através de testes (o tema do próximo vídeo: "O Ciclo de Segurança em C#: Testar, Compilar, Cometer").
     
     */
}
