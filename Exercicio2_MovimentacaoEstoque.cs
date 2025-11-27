using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace ExerciciosTarget
{
    public class Produto
    {
        public int codigoProduto { get; set; }
        public string descricaoProduto { get; set; } = string.Empty;
        public int estoque { get; set; }
    }

    public class EstoqueData
    {
        public List<Produto> estoque { get; set; } = new List<Produto>();
    }

    public class Movimentacao
    {
        public int Id { get; set; }
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; } = string.Empty;
        public string TipoMovimentacao { get; set; } = string.Empty; // "ENTRADA" ou "SAIDA"
        public int Quantidade { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataMovimentacao { get; set; }
        public int EstoqueAnterior { get; set; }
        public int EstoqueFinal { get; set; }
    }

    public class Exercicio2_MovimentacaoEstoque
    {
        private static List<Produto> produtos = new List<Produto>();
        private static List<Movimentacao> movimentacoes = new List<Movimentacao>();
        private static int proximoIdMovimentacao = 1;

        public static void ExecutarExercicio2()
        {
            InicializarDados();
            
            bool continuar = true;
            while (continuar)
            {
                ExibirMenu();
                string opcao = Console.ReadLine() ?? "" ?? "";

                switch (opcao)
                {
                    case "1":
                        ExibirEstoque();
                        break;
                    case "2":
                        RealizarMovimentacao();
                        break;
                    case "3":
                        ExibirHistoricoMovimentacoes();
                        break;
                    case "4":
                        ConsultarProduto();
                        break;
                    case "0":
                        continuar = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida! Tente novamente.");
                        break;
                }

                if (continuar)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void InicializarDados()
        {
            string jsonEstoque = @"{
                ""estoque"": [
                    {
                        ""codigoProduto"": 101,
                        ""descricaoProduto"": ""Caneta Azul"",
                        ""estoque"": 150
                    },
                    {
                        ""codigoProduto"": 102,
                        ""descricaoProduto"": ""Caderno Universitário"",
                        ""estoque"": 75
                    },
                    {
                        ""codigoProduto"": 103,
                        ""descricaoProduto"": ""Borracha Branca"",
                        ""estoque"": 200
                    },
                    {
                        ""codigoProduto"": 104,
                        ""descricaoProduto"": ""Lápis Preto HB"",
                        ""estoque"": 320
                    },
                    {
                        ""codigoProduto"": 105,
                        ""descricaoProduto"": ""Marcador de Texto Amarelo"",
                        ""estoque"": 90
                    }
                ]
            }";

            var estoqueData = JsonConvert.DeserializeObject<EstoqueData>(jsonEstoque);
            if (estoqueData?.estoque != null)
            {
                produtos = estoqueData.estoque;
            }
            movimentacoes = new List<Movimentacao>();

            Console.WriteLine("Sistema de Movimentação de Estoque Inicializado!");
        }

        private static void ExibirMenu()
        {
            Console.Clear();
            Console.WriteLine("=== SISTEMA DE MOVIMENTAÇÃO DE ESTOQUE ===");
            Console.WriteLine("1 - Exibir Estoque Atual");
            Console.WriteLine("2 - Realizar Movimentação");
            Console.WriteLine("3 - Histórico de Movimentações");
            Console.WriteLine("4 - Consultar Produto");
            Console.WriteLine("0 - Sair");
            Console.Write("\nEscolha uma opção: ");
        }

        private static void ExibirEstoque()
        {
            Console.Clear();
            Console.WriteLine("=== ESTOQUE ATUAL ===\n");
            Console.WriteLine($"{"Código",-8} {"Descrição",-30} {"Estoque",-10}");
            Console.WriteLine(new string('-', 50));

            foreach (var produto in produtos.OrderBy(p => p.codigoProduto))
            {
                Console.WriteLine($"{produto.codigoProduto,-8} {produto.descricaoProduto,-30} {produto.estoque,-10}");
            }
        }

        private static void RealizarMovimentacao()
        {
            Console.Clear();
            Console.WriteLine("=== REALIZAR MOVIMENTAÇÃO ===\n");

            // Selecionar produto
            Console.Write("Digite o código do produto: ");
            if (!int.TryParse(Console.ReadLine() ?? "", out int codigoProduto))
            {
                Console.WriteLine("Código inválido!");
                return;
            }

            var produto = produtos.FirstOrDefault(p => p.codigoProduto == codigoProduto);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            Console.WriteLine($"Produto selecionado: {produto.descricaoProduto}");
            Console.WriteLine($"Estoque atual: {produto.estoque}");

            // Tipo de movimentação
            Console.WriteLine("\nTipo de movimentação:");
            Console.WriteLine("1 - Entrada");
            Console.WriteLine("2 - Saída");
            Console.Write("Escolha: ");
            
            string tipoEscolha = Console.ReadLine() ?? "";
            string tipoMovimentacao = tipoEscolha == "1" ? "ENTRADA" : tipoEscolha == "2" ? "SAIDA" : "";
            
            if (string.IsNullOrEmpty(tipoMovimentacao))
            {
                Console.WriteLine("Tipo de movimentação inválido!");
                return;
            }

            // Quantidade
            Console.Write("Digite a quantidade: ");
            if (!int.TryParse(Console.ReadLine() ?? "", out int quantidade) || quantidade <= 0)
            {
                Console.WriteLine("Quantidade inválida!");
                return;
            }

            // Verificar se há estoque suficiente para saída
            if (tipoMovimentacao == "SAIDA" && quantidade > produto.estoque)
            {
                Console.WriteLine($"Estoque insuficiente! Disponível: {produto.estoque}");
                return;
            }

            // Descrição da movimentação
            Console.Write("Digite a descrição da movimentação: ");
            string descricao = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(descricao))
            {
                Console.WriteLine("Descrição é obrigatória!");
                return;
            }

            // Realizar movimentação
            int estoqueAnterior = produto.estoque;
            int novoEstoque = tipoMovimentacao == "ENTRADA" ? 
                produto.estoque + quantidade : 
                produto.estoque - quantidade;

            produto.estoque = novoEstoque;

            // Registrar movimentação
            var movimentacao = new Movimentacao
            {
                Id = proximoIdMovimentacao++,
                CodigoProduto = produto.codigoProduto,
                DescricaoProduto = produto.descricaoProduto,
                TipoMovimentacao = tipoMovimentacao,
                Quantidade = quantidade,
                Descricao = descricao,
                DataMovimentacao = DateTime.Now,
                EstoqueAnterior = estoqueAnterior,
                EstoqueFinal = novoEstoque
            };

            movimentacoes.Add(movimentacao);

            Console.WriteLine("\n=== MOVIMENTAÇÃO REALIZADA COM SUCESSO ===");
            Console.WriteLine($"ID da Movimentação: {movimentacao.Id}");
            Console.WriteLine($"Produto: {produto.descricaoProduto}");
            Console.WriteLine($"Tipo: {tipoMovimentacao}");
            Console.WriteLine($"Quantidade: {quantidade}");
            Console.WriteLine($"Estoque anterior: {estoqueAnterior}");
            Console.WriteLine($"Estoque final: {novoEstoque}");
        }

        private static void ExibirHistoricoMovimentacoes()
        {
            Console.Clear();
            Console.WriteLine("=== HISTÓRICO DE MOVIMENTAÇÕES ===\n");

            if (!movimentacoes.Any())
            {
                Console.WriteLine("Nenhuma movimentação registrada.");
                return;
            }

            foreach (var mov in movimentacoes.OrderByDescending(m => m.DataMovimentacao))
            {
                Console.WriteLine($"ID: {mov.Id} | Data: {mov.DataMovimentacao:dd/MM/yyyy HH:mm}");
                Console.WriteLine($"Produto: [{mov.CodigoProduto}] {mov.DescricaoProduto}");
                Console.WriteLine($"Tipo: {mov.TipoMovimentacao} | Quantidade: {mov.Quantidade}");
                Console.WriteLine($"Estoque: {mov.EstoqueAnterior} → {mov.EstoqueFinal}");
                Console.WriteLine($"Descrição: {mov.Descricao}");
                Console.WriteLine(new string('-', 60));
            }
        }

        private static void ConsultarProduto()
        {
            Console.Clear();
            Console.WriteLine("=== CONSULTAR PRODUTO ===\n");

            Console.Write("Digite o código do produto: ");
            if (!int.TryParse(Console.ReadLine() ?? "", out int codigoProduto))
            {
                Console.WriteLine("Código inválido!");
                return;
            }

            var produto = produtos.FirstOrDefault(p => p.codigoProduto == codigoProduto);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                return;
            }

            Console.WriteLine($"\nProduto: {produto.descricaoProduto}");
            Console.WriteLine($"Código: {produto.codigoProduto}");
            Console.WriteLine($"Estoque atual: {produto.estoque}");

            var movimentacoesProduto = movimentacoes
                .Where(m => m.CodigoProduto == codigoProduto)
                .OrderByDescending(m => m.DataMovimentacao)
                .Take(5);

            if (movimentacoesProduto.Any())
            {
                Console.WriteLine("\nÚltimas 5 movimentações:");
                foreach (var mov in movimentacoesProduto)
                {
                    Console.WriteLine($"  {mov.DataMovimentacao:dd/MM/yyyy} - {mov.TipoMovimentacao} - Qtd: {mov.Quantidade} - {mov.Descricao}");
                }
            }
        }
    }
}
