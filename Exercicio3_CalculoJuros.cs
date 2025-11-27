using System;
using System.Globalization;

namespace ExerciciosTarget
{
    public class CalculoJuros
    {
        public decimal ValorOriginal { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataCalculo { get; set; }
        public int DiasAtraso { get; set; }
        public decimal PercentualJurosDia { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class Exercicio3_CalculoJuros
    {
        private const decimal JUROS_DIA = 2.5m; // 2,5% ao dia

        public static void ExecutarExercicio3()
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== CALCULADORA DE JUROS POR ATRASO ===");
                Console.WriteLine($"Taxa de juros: {JUROS_DIA}% ao dia\n");

                try
                {
                    // Solicitar valor original
                    decimal valorOriginal = SolicitarValor();
                    if (valorOriginal <= 0)
                    {
                        Console.WriteLine("Valor deve ser maior que zero!");
                        continue;
                    }

                    // Solicitar data de vencimento
                    DateTime dataVencimento = SolicitarDataVencimento();

                    // Calcular juros
                    var calculo = CalcularJuros(valorOriginal, dataVencimento, DateTime.Today);
                    
                    // Exibir resultado
                    ExibirResultado(calculo);

                    // Perguntar se deseja continuar
                    Console.WriteLine("\nDeseja fazer outro cálculo? (S/N): ");
                    string resposta = Console.ReadLine() ?? ""?.ToUpper();
                    continuar = resposta == "S" || resposta == "SIM";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                    Console.WriteLine("\nPressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine("\nObrigado por usar a Calculadora de Juros!");
        }

        private static decimal SolicitarValor()
        {
            Console.Write("Digite o valor original (R$): ");
            string input = Console.ReadLine() ?? "";

            // Remover símbolos de moeda e espaços
            input = input.Replace("R$", "").Replace("$", "").Trim();

            if (decimal.TryParse(input, NumberStyles.Currency, CultureInfo.GetCultureInfo("pt-BR"), out decimal valor))
            {
                return valor;
            }
            else if (decimal.TryParse(input, out valor))
            {
                return valor;
            }
            else
            {
                throw new ArgumentException("Valor inválido! Use formato: 1000,50 ou 1000.50");
            }
        }

        private static DateTime SolicitarDataVencimento()
        {
            Console.Write("Digite a data de vencimento (dd/MM/yyyy): ");
            string input = Console.ReadLine() ?? "";

            DateTime data;
            if (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out data))
            {
                // Validar se a data não é muito antiga
                ValidarDataVencimento(data);
                return data;
            }
            else if (DateTime.TryParse(input, out data))
            {
                // Validar se a data não é muito antiga
                ValidarDataVencimento(data);
                return data;
            }
            else
            {
                throw new ArgumentException("Data inválida! Use formato: dd/MM/yyyy (ex: 15/01/2024)");
            }
        }

        private static void ValidarDataVencimento(DateTime dataVencimento)
        {
            // Verificar se a data não é muito antiga (mais de 5 anos atrás)
            DateTime dataLimite = DateTime.Today.AddYears(-5);
            
            if (dataVencimento < dataLimite)
            {
                int anosAtraso = DateTime.Today.Year - dataVencimento.Year;
                Console.WriteLine($"\n⚠️  ATENÇÃO: A data de vencimento é de {anosAtraso} anos atrás!");
                Console.WriteLine("Datas muito antigas podem resultar em valores extremamente altos.");
                Console.WriteLine("O sistema suporta cálculos de até 5 anos de atraso.");
                Console.WriteLine("\nDeseja continuar mesmo assim? (S/N): ");
                
                string resposta = Console.ReadLine()?.ToUpper() ?? "";
                if (resposta != "S" && resposta != "SIM")
                {
                    throw new ArgumentException("Operação cancelada pelo usuário.");
                }
            }
        }

        public static CalculoJuros CalcularJuros(decimal valorOriginal, DateTime dataVencimento, DateTime dataCalculo)
        {
            var calculo = new CalculoJuros
            {
                ValorOriginal = valorOriginal,
                DataVencimento = dataVencimento,
                DataCalculo = dataCalculo,
                PercentualJurosDia = JUROS_DIA
            };

            // Calcular dias de atraso
            TimeSpan diferenca = dataCalculo - dataVencimento;
            calculo.DiasAtraso = diferenca.Days;

            if (calculo.DiasAtraso <= 0)
            {
                // Não há atraso
                calculo.DiasAtraso = 0;
                calculo.ValorJuros = 0;
                calculo.ValorTotal = valorOriginal;
            }
            else
            {
                // Verificar se o período é muito longo (mais de 5 anos = ~1825 dias)
                const int DIAS_MAXIMOS = 1825; // 5 anos
                
                if (calculo.DiasAtraso > DIAS_MAXIMOS)
                {
                    throw new ArgumentException($"Período de atraso muito longo ({calculo.DiasAtraso} dias). " +
                        $"O sistema suporta cálculos de até {DIAS_MAXIMOS} dias ({DIAS_MAXIMOS/365} anos). " +
                        "Para períodos maiores, consulte um especialista financeiro.");
                }

                try
                {
                    // Calcular juros compostos: Valor * (1 + taxa)^dias - Valor
                    double taxa = (double)(JUROS_DIA / 100);
                    double fatorDouble = Math.Pow(1 + taxa, calculo.DiasAtraso);
                    
                    // Verificar se o resultado está dentro dos limites do decimal
                    if (fatorDouble > (double)decimal.MaxValue || double.IsInfinity(fatorDouble))
                    {
                        throw new OverflowException("O valor calculado excede os limites suportados pelo sistema.");
                    }
                    
                    decimal fator = (decimal)fatorDouble;
                    calculo.ValorTotal = valorOriginal * fator;
                    calculo.ValorJuros = calculo.ValorTotal - valorOriginal;
                    
                    // Verificação adicional para overflow no resultado final
                    if (calculo.ValorTotal < 0 || calculo.ValorJuros < 0)
                    {
                        throw new OverflowException("O valor calculado resultou em overflow.");
                    }
                }
                catch (OverflowException)
                {
                    throw new ArgumentException($"O período de atraso ({calculo.DiasAtraso} dias) resulta em valores " +
                        "muito altos para serem calculados. Considere usar um período menor ou consulte um especialista financeiro.");
                }
            }

            return calculo;
        }

        private static void ExibirResultado(CalculoJuros calculo)
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("RESULTADO DO CÁLCULO");
            Console.WriteLine(new string('=', 50));

            Console.WriteLine($"Valor Original: {calculo.ValorOriginal:C2}");
            Console.WriteLine($"Data de Vencimento: {calculo.DataVencimento:dd/MM/yyyy}");
            Console.WriteLine($"Data do Cálculo: {calculo.DataCalculo:dd/MM/yyyy}");

            if (calculo.DiasAtraso <= 0)
            {
                Console.WriteLine("\n✅ PAGAMENTO EM DIA!");
                Console.WriteLine("Não há juros a serem cobrados.");
                Console.WriteLine($"Valor a pagar: {calculo.ValorTotal:C2}");
            }
            else
            {
                Console.WriteLine($"\n⚠️  PAGAMENTO EM ATRASO!");
                Console.WriteLine($"Dias de atraso: {calculo.DiasAtraso} dia(s)");
                Console.WriteLine($"Taxa de juros: {calculo.PercentualJurosDia}% ao dia");
                Console.WriteLine($"Juros calculados: {calculo.ValorJuros:C2}");
                Console.WriteLine($"Valor total a pagar: {calculo.ValorTotal:C2}");

                // Mostrar detalhamento
                decimal percentualTotal = ((calculo.ValorTotal - calculo.ValorOriginal) / calculo.ValorOriginal) * 100;
                Console.WriteLine($"Acréscimo total: {percentualTotal:F2}%");
            }

            Console.WriteLine(new string('=', 50));
        }

        // Método auxiliar para testes automatizados
        public static void ExemplosCalculo()
        {
            Console.WriteLine("=== EXEMPLOS DE CÁLCULO ===\n");

            var exemplos = new[]
            {
                new { Valor = 1000m, Vencimento = DateTime.Today.AddDays(-5), Descricao = "5 dias de atraso" },
                new { Valor = 500m, Vencimento = DateTime.Today.AddDays(-10), Descricao = "10 dias de atraso" },
                new { Valor = 2000m, Vencimento = DateTime.Today.AddDays(-30), Descricao = "30 dias de atraso" },
                new { Valor = 1500m, Vencimento = DateTime.Today.AddDays(5), Descricao = "Ainda não venceu" }
            };

            foreach (var exemplo in exemplos)
            {
                Console.WriteLine($"Exemplo: {exemplo.Descricao}");
                var calculo = CalcularJuros(exemplo.Valor, exemplo.Vencimento, DateTime.Today);
                Console.WriteLine($"  Valor: {calculo.ValorOriginal:C2}");
                Console.WriteLine($"  Dias de atraso: {calculo.DiasAtraso}");
                Console.WriteLine($"  Juros: {calculo.ValorJuros:C2}");
                Console.WriteLine($"  Total: {calculo.ValorTotal:C2}");
                Console.WriteLine();
            }
        }
    }
}
