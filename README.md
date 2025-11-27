# Exercícios Target - Soluções em C#

Este repositório contém as soluções para os 3 exercícios propostos, implementados em C# com .NET 6.

## 📋 Exercícios Implementados

### 1. Sistema de Cálculo de Comissões de Vendedores
### 2. Sistema de Movimentação de Estoque
### 3. Calculadora de Juros por Atraso

---

## 🚀 Como Executar os Projetos

### Pré-requisitos
- .NET 6.0 SDK ou superior
- Visual Studio, Visual Studio Code ou qualquer editor de código
- Terminal/Prompt de Comando

### Instalação e Execução

1. **Clone ou baixe os arquivos do projeto**

2. **Navegue até o diretório do projeto**
```bash
cd caminho/para/o/projeto
```

3. **Restaure as dependências**
```bash
dotnet restore
```

4. **Execute cada exercício individualmente:**

**Exercício 1 - Comissões:**
```bash
dotnet run --project . Exercicio1_ComissaoVendedores.cs
```

**Exercício 2 - Estoque:**
```bash
dotnet run --project . Exercicio2_MovimentacaoEstoque.cs
```

**Exercício 3 - Juros:**
```bash
dotnet run --project . Exercicio3_CalculoJuros.cs
```

---

## 📊 Exercício 1: Sistema de Cálculo de Comissões

### 📝 Descrição
Calcula a comissão de vendedores baseado nas seguintes regras:
- Vendas abaixo de R$ 100,00: **0% de comissão**
- Vendas entre R$ 100,00 e R$ 499,99: **1% de comissão**
- Vendas a partir de R$ 500,00: **5% de comissão**

### 🔧 Funcionalidades
- ✅ Leitura e processamento de dados JSON
- ✅ Cálculo automático de comissões por venda
- ✅ Agrupamento por vendedor
- ✅ Relatório completo com totais
- ✅ Ordenação por maior comissão

### 🧪 Como Testar

**Teste Automático:**
```bash
dotnet run Exercicio1_ComissaoVendedores.cs
```

**Resultados Esperados:**
- João Silva: R$ 6.751,50 em vendas → R$ 313,58 de comissão
- Maria Souza: R$ 11.878,60 em vendas → R$ 581,93 de comissão  
- Carlos Oliveira: R$ 8.127,05 em vendas → R$ 378,35 de comissão
- Ana Lima: R$ 9.762,95 em vendas → R$ 481,15 de comissão

**Teste Manual:**
Modifique os valores no JSON dentro do código para testar diferentes cenários.

---

## 📦 Exercício 2: Sistema de Movimentação de Estoque

### 📝 Descrição
Sistema interativo para controle de estoque com funcionalidades completas de entrada e saída de produtos.

### 🔧 Funcionalidades
- ✅ Visualização do estoque atual
- ✅ Movimentações de entrada e saída
- ✅ ID único para cada movimentação
- ✅ Descrição obrigatória para movimentações
- ✅ Histórico completo de movimentações
- ✅ Consulta detalhada por produto
- ✅ Validação de estoque insuficiente

### 🧪 Como Testar

**1. Execute o programa:**
```bash
dotnet run Exercicio2_MovimentacaoEstoque.cs
```

**2. Teste o Menu Interativo:**

**Opção 1 - Visualizar Estoque:**
- Mostra todos os produtos e quantidades atuais

**Opção 2 - Realizar Movimentação:**
```
Teste de Entrada:
- Código: 101 (Caneta Azul)
- Tipo: 1 (Entrada)
- Quantidade: 50
- Descrição: "Compra de canetas para reposição"
- Resultado: Estoque 150 → 200

Teste de Saída:
- Código: 102 (Caderno Universitário)  
- Tipo: 2 (Saída)
- Quantidade: 25
- Descrição: "Venda para escola municipal"
- Resultado: Estoque 75 → 50

Teste de Validação:
- Código: 105 (Marcador Amarelo)
- Tipo: 2 (Saída)
- Quantidade: 100 (maior que estoque de 90)
- Resultado: Erro "Estoque insuficiente!"
```

**Opção 3 - Histórico:**
- Visualiza todas as movimentações realizadas

**Opção 4 - Consultar Produto:**
- Digite código do produto para ver detalhes e últimas movimentações

### 🔍 Cenários de Teste Recomendados

1. **Teste de Entrada Normal:** Adicionar produtos ao estoque
2. **Teste de Saída Normal:** Remover produtos do estoque  
3. **Teste de Estoque Insuficiente:** Tentar saída maior que disponível
4. **Teste de Produto Inexistente:** Usar código que não existe
5. **Teste de Dados Inválidos:** Quantidade negativa, descrição vazia

---

## 💰 Exercício 3: Calculadora de Juros por Atraso

### 📝 Descrição
Calcula juros compostos de 2,5% ao dia para pagamentos em atraso.

### 🔧 Funcionalidades
- ✅ Entrada flexível de valores (aceita R$ 1.000,50 ou 1000.50)
- ✅ Entrada de data no formato brasileiro (dd/MM/yyyy)
- ✅ Cálculo de juros compostos
- ✅ Simulação para diferentes períodos
- ✅ Detalhamento completo dos cálculos
- ✅ Validação de pagamentos em dia

### 🧪 Como Testar

**1. Execute o programa:**
```bash
dotnet run Exercicio3_CalculoJuros.cs
```

**2. Testes Manuais Interativos:**

**Teste 1 - Pagamento em Atraso:**
```
Valor: R$ 1000,00
Data Vencimento: 01/11/2024
Data Atual: 06/11/2024 (5 dias de atraso)
Resultado Esperado: 
- Juros: R$ 131,41
- Total: R$ 1.131,41
```

**Teste 2 - Pagamento em Dia:**
```
Valor: R$ 500,00  
Data Vencimento: 30/11/2024
Data Atual: 27/11/2024 (ainda não venceu)
Resultado Esperado:
- Juros: R$ 0,00
- Total: R$ 500,00
```

**Teste 3 - Atraso Longo:**
```
Valor: R$ 2000,00
Data Vencimento: 01/10/2024  
Data Atual: 27/11/2024 (57 dias de atraso)
Resultado Esperado:
- Juros muito alto devido ao tempo
- Demonstra o impacto dos juros compostos
```

### 🧮 Fórmula Utilizada
```
Valor Final = Valor Original × (1 + 0,025)^dias_atraso
Juros = Valor Final - Valor Original
```

**3. Teste com Exemplos Pré-definidos:**
Descomente a linha no método Main para executar exemplos automáticos:
```csharp
// ExemplosCalculo(); // Descomente esta linha
```

---

## 🛠️ Estrutura Técnica

### Dependências
- **Newtonsoft.Json**: Para manipulação de dados JSON
- **.NET 6.0**: Framework base

### Padrões Utilizados
- **Separação de Responsabilidades**: Cada exercício em arquivo separado
- **Validação de Entrada**: Tratamento de erros e dados inválidos
- **Interface Amigável**: Menus interativos e mensagens claras
- **Código Limpo**: Métodos bem definidos e documentados

### Arquivos do Projeto
```
📁 Projeto/
├── 📄 Exercicio1_ComissaoVendedores.cs
├── 📄 Exercicio2_MovimentacaoEstoque.cs  
├── 📄 Exercicio3_CalculoJuros.cs
├── 📄 ExerciciosTarget.csproj
└── 📄 README.md
```

---

## 🧪 Testes Automatizados

Para criar testes unitários, você pode usar o framework xUnit:

```bash
dotnet new xunit -n ExerciciosTarget.Tests
dotnet add ExerciciosTarget.Tests/ExerciciosTarget.Tests.csproj reference ExerciciosTarget.csproj
```

### Exemplos de Testes

**Teste do Exercício 1:**
```csharp
[Fact]
public void CalcularComissaoVenda_ValorAbaixo100_RetornaZero()
{
    var resultado = Exercicio1_ComissaoVendedores.CalcularComissaoVenda(50m);
    Assert.Equal(0m, resultado);
}

[Fact]  
public void CalcularComissaoVenda_ValorEntre100e500_Retorna1Porcento()
{
    var resultado = Exercicio1_ComissaoVendedores.CalcularComissaoVenda(300m);
    Assert.Equal(3m, resultado);
}
```

**Teste do Exercício 3:**
```csharp
[Fact]
public void CalcularJuros_SemAtraso_RetornaValorOriginal()
{
    var dataVencimento = DateTime.Today;
    var dataCalculo = DateTime.Today;
    
    var resultado = Exercicio3_CalculoJuros.CalcularJuros(1000m, dataVencimento, dataCalculo);
    
    Assert.Equal(0, resultado.DiasAtraso);
    Assert.Equal(0m, resultado.ValorJuros);
    Assert.Equal(1000m, resultado.ValorTotal);
}
```

---

## 🚨 Solução de Problemas

### Erro: "Newtonsoft.Json não encontrado"
```bash
dotnet add package Newtonsoft.Json
```

### Erro: "Framework não encontrado"
Instale o .NET 6.0 SDK:
- Windows: https://dotnet.microsoft.com/download
- Linux: `sudo apt install dotnet-sdk-6.0`
- macOS: `brew install dotnet`

### Erro de Compilação
```bash
dotnet clean
dotnet restore
dotnet build
```

---

## 📞 Suporte

Se encontrar problemas:

1. **Verifique os pré-requisitos** (.NET 6.0 instalado)
2. **Execute `dotnet --version`** para confirmar a instalação
3. **Verifique se todos os arquivos estão no mesmo diretório**
4. **Execute `dotnet restore`** para baixar dependências

---

## 🎯 Conclusão

Os três exercícios demonstram diferentes aspectos da programação em C#:

- **Exercício 1**: Manipulação de JSON, LINQ, e cálculos matemáticos
- **Exercício 2**: Interface de usuário, validação de dados, e controle de estado
- **Exercício 3**: Cálculos financeiros, formatação de dados, e simulações

Cada solução é independente e pode ser executada separadamente, facilitando testes e manutenção.

**Desenvolvido com ❤️ em C# .NET 6.0**
