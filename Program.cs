using System;

namespace ExerciciosTarget
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.Clear();
                Console.WriteLine("=== EXERCÍCIOS TARGET - SOLUÇÕES ===");
                Console.WriteLine();
                Console.WriteLine("Escolha qual exercício deseja executar:");
                Console.WriteLine();
                Console.WriteLine("1 - Cálculo de Comissões de Vendedores");
                Console.WriteLine("2 - Sistema de Movimentação de Estoque");
                Console.WriteLine("3 - Calculadora de Juros por Atraso");               
                Console.WriteLine("0 - Sair");
                Console.WriteLine();
                Console.Write("Digite sua opção: ");

                string opcao = Console.ReadLine() ?? "";

                try
                {
                    switch (opcao)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("=== EXERCÍCIO 1: CÁLCULO DE COMISSÕES ===\n");
                            Exercicio1_ComissaoVendedores.ExecutarExercicio1();
                            break;

                        case "2":
                            Console.Clear();
                            Console.WriteLine("=== EXERCÍCIO 2: MOVIMENTAÇÃO DE ESTOQUE ===\n");
                            Exercicio2_MovimentacaoEstoque.ExecutarExercicio2();
                            break;

                        case "3":
                            Console.Clear();
                            Console.WriteLine("=== EXERCÍCIO 3: CÁLCULO DE JUROS ===\n");
                            Exercicio3_CalculoJuros.ExecutarExercicio3();
                            break;

                        case "0":
                            continuar = false;
                            Console.WriteLine("\nObrigado por usar os Exercícios Target!");
                            break;

                        default:
                            Console.WriteLine("\nOpção inválida! Pressione qualquer tecla para tentar novamente...");
                            Console.ReadKey();
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nErro ao executar exercício: {ex.Message}");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }

                if (continuar && opcao != "0")
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
                    Console.ReadKey();
                }
            }
        }
    }
}
