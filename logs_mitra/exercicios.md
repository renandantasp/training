### 1. Pegue um trecho de código que você tenha familiaridade (ou um trecho qualquer do MITRA). Gere um erro localmente a partir dele, como um uso incorreto ou uma exceção. Mostre como foi a Pilha de chamadas e qual a função que contém o erro.

O erro seguinte é causado quando o MITRA Services não está instalado e configurado. Uma exceção é gerada e a partir dela é gerado o seguinte log: 

```log
2023-07-11 09:37:23.888 MITRA.Services não esta configurado. 
DESKTOP-VQIA967 :Mitra.exe: 7.8.0.198 Gerencial.dll: 7.3.0.151 Estatistica.dll: 7.0.0.0 Opcao.dll: 7.0.0.0
Desde: 11/07/2023 09:35:09 Memória: 383209472 
 System.Exception: MITRA.Services não esta configurado.
   em MITRA.Portal.Main.Modulos.Front.JanelasFront.TelaMonitorLimites(Boolean ehBoleta)
   em MITRA.Portal.Main.Modulos.Front.FrontVM.<MontaRelayCommandBotoes>b__55_6(Object param)    
   em MITRA.Portal.Main.Modulos.Front.JanelasFront.TelaMonitorLimites(Boolean ehBoleta)
   em MITRA.Portal.Main.Modulos.Front.FrontVM.<MontaRelayCommandBotoes>b__55_6(Object param)

```

### 2. Dado o exercício anterior, busque uma documentação (ex: wiki interna, regra de negócio ou documentação do framework) que justifique o erro. Justifique também um uso mais adequado. Exemplo: Convert.ToString Method (System) | Microsoft Learn

O erro anterior é causado numa exceção quando clicamos no botão `Limite(Realizado)` ou `Limite(Intenções)` na aba do Front do MITRA. No caso do botão `Limite Realizado`, um RelayCommand é chamado e dentro dele isso ocorre:

```c#
if (this.permissoesActions.TryGetValue("actLimitesRealizados", out string hint))
{
    CriaJanela(Resources.LimitesRealizados, JanelasFront.TelaMonitorLimites(true));
}
```
Essa condicional chama o método `JanelasFront.TelaMonitorLimites(true)`:

```c#
public MonitorEnquadramento TelaMonitorLimites(bool ehBoleta)
{
    if (string.IsNullOrEmpty(Sessao.ArquivoIni.Ler("MQ", "conexao")))
    {
        throw new Exception("MITRA.Services não esta configurado.");
    }
...
```

O Método `TelaMonitorLimites` irá ler o arquivo ini buscando as configurações referentes a instalação do MITRA.Services, e quando isso não está configurado, é gerada a exceção: `"MITRA.Services não esta configurado."`.


Na Wiki interna é possível encontrar uma página que fornece um passo a passo da [instalação do MITRA Services](http://wiki.lef.intra/index.php/File:Passo_a_Passo_Service.pdf), que irá consertar o erro causado.

### 3. Quais são os pontos positivos e negativos de se ter logs?

As principais vantagens de se ter logs são:
- **Monitoramento do sistema**: Logs podem ser utilizados para monitoramento e para manutenção dos sistemas. Pela monitoração dos logs, desenvolvedores podem detectar bugs, anomalias, trackear o comportamento do sistema (por features como o *trace*) e identificar potenciais problemas antes de que se tornem grandes;
- **Otimização da performance** - Logs podem revelar alguns gargalos e ineficiencias do código. Analisando os logs, os desenvolvedores podem identificar áreas que necessitam de otimização, como queries custosas, excesso de conexões a rede, e outros problemas.

Alguns dos pontos negativos são:
- **Overhead de performance** -gerar e armazenas logs de dbug podem gerar um overhead de performance no sistema. Escrever logs no disco ou transmitindo-os pela rede podem deixar a aplicação um pouco mais lenta;
- **Aumento de uso do armazenamento** Logs tendem a gerar um grande volume de dados, o que pode rapidamente consumir espaço de armazenamento. Isso pode se tornar um problema em cenários onde recursos de armazenamento são limitados ou são custosos;
- **Riscos de segurança** - Logs podem conter informações sensíveis, como dados do usuários, chaves de API, ou credenciais de databases. Se não tratado com atenção, os logs podem gerar um risco a segurança se acessado por pessoas não-autorizadas.

### 4. Dado os logs que você conhece, escreva alguma sugestão de usabilidade que possa deixar a leitura ou exposição de informações de logs mais organizada ou útil para sua própria leitura. Cite um exemplo e sua melhoria.

**Estruturação das Entradas de Log**: Organize as entradas de log de forma estruturada. Utilize formatação consistente e inclua metadados relevantes, como níveis de log, nomes de componentes ou IDs de transação. Isso ajuda na categorização e filtragem das entradas de log posteriormente. 


### 5. Dado o seguinte trecho de código em Delphi, qual o log de erro ou warning de compilação esperado? Qual uma correção esperada?

```delphi
function TOperacao.EhCoverbond: Boolean; 
    var 
        objProduto: TProduto; 
    begin 
        objProduto := ProdutoOperacao(Self, DataAquisicao); 
        if Assigned(objProduto) then begin 
            Result := (objProduto as TContrato).EhCoverbond; 
        end; 
end;
```

Nem todos os caminhos da função possuem um retorno. Se a função `ProdutoOperacao` retornar `nil`, ela não irá passar pelo `if Assigned(objProduto)`, e consequentemente não irá retornar `(objProduto as TContrato).EhCoverbond`. Um possível *warning* seria que a função espera um retorno booleano, e não terá nenhum retorno caso `objProduto` seja nulo. 

Além disso, poderá ocasionar um erro em tempo de execução quando uma variável booleana esperar um retorno booleano da operação `TOperacao.EhCoverbond` quando a chamada `ProdutoOperacao`  retorne `nil`.

### 6. Dado o seguinte caso de teste pelo Framework NUnit em C#, qual os casos de erro e qual seu log esperado? Há diferença entre o TestaConversao1 e TestaConversao2? Por que?

```c#
    static object[] Casos = 
    {
    new object[] { 61010002 },
    new object[] { 6.00M },
    new object[] { "LUZ" },
    new object[] { null },
    new object[] { 9999d },
    new object[] { 9999.99d },
    new object[] { IntPtr.Zero },
    new object[] { IntPtr.Size }
    }; 
    [Test]
    [TestCaseSource("Casos")]
    public void TestaConversao1(object casoDeTeste)
    {
    object tratado = int.Parse(casoDeTeste.ToString());
    }
    [Test]
    [TestCaseSource("Casos")]
    public void TestaConversao2(object casoDeTeste)
    {
    object tratado = (int)casoDeTeste;
    }
```

Em ambos os testes, as funções TestaConversao tentam converter os elementos de um array de objetos em int, no entanto, em ambas funções são testados diferentes métodos que não irão conseguir executar a função.

No `TesteConversao1`, é criado uma varável tratado esperando um objeto que irá receber o retorno do tipo int, que é o retorno da função `int.Parse()`. No entanto, a string passada ao `int.Parse()` é a conversão do casoDeTeste para string utilizando o método `ToString()`. Isso gera um problema pois a conversão de um array de objetos irá sempre ser a string `Object[]`, que é uma string inválida para a conversão para int. O que irá causar o log: `Input string was not in a correct format.`

Já na `TesteConversão2`, foi tentada fazer uma conversão explícita de `Object[]` para `Int32`, que é um tipo de conversão inválida, o que irá gerar o log: `Unable to cast object of type 'System.Object[]' to type 'System.Int32'.`

### 7. Dado o seguinte trecho de código em Delphi, qual o erro em tempo de execução possível e qual seu log esperado? Qual uma sugestão adequada?

```delphi
function TArvore.GetNomeFolha: String; 
    begin 
        Result := Filho.Folha.Nome;
    end;
```

Um possível runtime error para esse código seria o caso dos objetos `Filho` ou `Folha` não serem inicializados, ou instanciados de forma incorreta. Se `Filho` ou `Folha` serem `nil`, irá causar um erro em tempo de execução quando houver alguma tentativa de acessar propriedades ou métodos desses objetos.

Para evitar esse erro, seria possível adicionar checagem para verificar se esses objetos existem ou lidar com alguma exceção caso ocorra.

### 8. Dado o seguinte trecho de código em C#, qual o erro em tempo de execução possível e qual seu log esperado? Qual uma sugestão adequada?

```c#
public static void LimpaCacheDosDicionarios(Dictionary<int, object> dicionario) 
{ 
 int i = 0; 
 do 
 { 
     dicionario[i] = null; 
     i++; 
 } 
 while (i < dicionario.Count); 
 dicionario.Clear(); 
}
```

Um possível erro em tempo de execução seria chamar essa função passando um ponteiro inicializado como null.

Uma possível correção para esse bug é realizar uma verificação para saber se a variável `dicionario` está inicializada ou não, caso não, não executar a função:

```c#
public static void LimpaCacheDosDicionarios(Dictionary<int, object> dicionario) 
{ 
    if (dicionario == null) return;
    ...
```