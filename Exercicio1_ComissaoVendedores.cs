using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ExerciciosTarget
{
    public class Venda
    {
        public string vendedor { get; set; } = string.Empty;
        public decimal valor { get; set; }
    }

    public class VendasData
    {
        public List<Venda> vendas { get; set; } = new List<Venda>();
    }

    public class ComissaoVendedor
    {
        public string Vendedor { get; set; } = string.Empty;
        public decimal TotalVendas { get; set; }
        public decimal TotalComissao { get; set; }
        public List<decimal> DetalhesComissao { get; set; } = new List<decimal>();
    }

    public class Exercicio1_ComissaoVendedores
    {
        public static void ExecutarExercicio1()
        {
            string jsonVendas = @"{
                ""vendas"": [
                    { ""vendedor"": ""João Silva"", ""valor"": 1200.50 },
                    { ""vendedor"": ""João Silva"", ""valor"": 950.75 },
                    { ""vendedor"": ""João Silva"", ""valor"": 1800.00 },
                    { ""vendedor"": ""João Silva"", ""valor"": 1400.30 },
                    { ""vendedor"": ""João Silva"", ""valor"": 1100.90 },
                    { ""vendedor"": ""João Silva"", ""valor"": 1550.00 },
                    { ""vendedor"": ""João Silva"", ""valor"": 1700.80 },
                    { ""vendedor"": ""João Silva"", ""valor"": 250.30 },
                    { ""vendedor"": ""João Silva"", ""valor"": 480.75 },
                    { ""vendedor"": ""João Silva"", ""valor"": 320.40 },

                    { ""vendedor"": ""Maria Souza"", ""valor"": 2100.40 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 1350.60 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 950.20 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 1600.75 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 1750.00 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 1450.90 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 400.50 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 180.20 },
                    { ""vendedor"": ""Maria Souza"", ""valor"": 90.75 },

                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 800.50 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1200.00 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1950.30 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1750.80 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 1300.60 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 300.40 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 500.00 },
                    { ""vendedor"": ""Carlos Oliveira"", ""valor"": 125.75 },

                    { ""vendedor"": ""Ana Lima"", ""valor"": 1000.00 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 1100.50 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 1250.75 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 1400.20 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 1550.90 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 1650.00 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 75.30 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 420.90 },
                    { ""vendedor"": ""Ana Lima"", ""valor"": 315.40 }
                ]
            }";

            try
            {
                var vendasData = JsonConvert.DeserializeObject<VendasData>(jsonVendas);
                if (vendasData?.vendas != null)
                {
                    var comissoes = CalcularComissoes(vendasData.vendas);
                    ExibirResultados(comissoes);
                }
                else
                {
                    Console.WriteLine("Erro: Dados de vendas não encontrados.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao processar dados: {ex.Message}");
            }
        }

        public static List<ComissaoVendedor> CalcularComissoes(List<Venda> vendas)
        {
            var vendedoresAgrupados = vendas.GroupBy(v => v.vendedor);
            var resultados = new List<ComissaoVendedor>();

            foreach (var grupo in vendedoresAgrupados)
            {
                var comissaoVendedor = new ComissaoVendedor
                {
                    Vendedor = grupo.Key,
                    TotalVendas = grupo.Sum(v => v.valor),
                    DetalhesComissao = new List<decimal>()
                };

                decimal totalComissao = 0;

                foreach (var venda in grupo)
                {
                    decimal comissao = CalcularComissaoVenda(venda.valor);
                    comissaoVendedor.DetalhesComissao.Add(comissao);
                    totalComissao += comissao;
                }

                comissaoVendedor.TotalComissao = totalComissao;
                resultados.Add(comissaoVendedor);
            }

            return resultados.OrderByDescending(c => c.TotalComissao).ToList();
        }

        public static decimal CalcularComissaoVenda(decimal valorVenda)
        {
            if (valorVenda < 100)
            {
                return 0; // Não gera comissão
            }
            else if (valorVenda < 500)
            {
                return valorVenda * 0.01m; // 1% de comissão
            }
            else
            {
                return valorVenda * 0.05m; // 5% de comissão
            }
        }

        public static void ExibirResultados(List<ComissaoVendedor> comissoes)
        {
            Console.WriteLine("=== RELATÓRIO DE COMISSÕES ===\n");

            foreach (var vendedor in comissoes)
            {
                Console.WriteLine($"Vendedor: {vendedor.Vendedor}");
                Console.WriteLine($"Total de Vendas: {vendedor.TotalVendas:C2}");
                Console.WriteLine($"Total de Comissão: {vendedor.TotalComissao:C2}");
                Console.WriteLine($"Percentual sobre vendas: {(vendedor.TotalComissao / vendedor.TotalVendas * 100):F2}%");
                Console.WriteLine(new string('-', 50));
            }

            Console.WriteLine($"\nTOTAL GERAL:");
            Console.WriteLine($"Vendas: {comissoes.Sum(c => c.TotalVendas):C2}");
            Console.WriteLine($"Comissões: {comissoes.Sum(c => c.TotalComissao):C2}");
        }
    }
}
