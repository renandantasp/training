## 1. O que são as estatísticas de uma base Oracle? O que precisa ser feito para elas serem coletadas?

Estatísticas são uma parte chave para o Otimizador conseguir definir o melhor plano de execução para uma SQL statement. As estatísticas do Otimizador incluem:

- **Estatisticas de Tabela**
  - Número de linhas
  - Número de blocos
  - Média do tamanho das linhas
- **Estatistica de Coluna**
  - Número de valores distintos (NDV) em uma coluna
  - Número de valores núlos em uma coluna
  - Distribuição dos dados (histograma)
  - Estatísticas extendidas (um tipo de estatística que aperfeiçoa a estimativa de cardinalidade quando múltiplos predicados existem)
- **Estatistica dos Índices**
  - Número de blocos folha
  - Número de níveis
  - Fator de clustering do Índice
- **Estatísticas do sistema**
  - Desempenho e utilização de Input/Output
  - Desempenho e utilização de CPU

 Para coletar as estatísticas de uma base Oracle, você pode:

1. Habilitar a coleta automática de estatísticas:

    O Oracle possui um recurso chamado "coleta automática de estatísticas" que pode ser ativado para coletar automaticamente estatísticas sobre as tabelas e índices da base de dados. Para habilitar esse recurso, é executado esse comando:

    ```sql
    ALTER SYSTEM SET STATISTICS_LEVEL = ALL;
    ```

2. Ativar a opção de coleta automática de objetos:

    Para garantir que a coleta automática de estatísticas seja realizada regularmente em todos os objetos, é executado esse comando:    

    ```sql
    ALTER DATABASE SET AUTO_STAT_EXTENSIONS = TRUE;
    ```

3. Agendar a coleta automática de estatísticas

    É possível configurar o Oracle para coletar estatísticas automaticamente em intervalo regulares. Para fazer isso, você pode criar um job no Oracle Scheduler para executar o procedimento de coleta de estatísticas. Por exemplo, é possível criar um job para executar a seguinte procedure:
    ```sql
    DBMS_STATS.GATHER_DATABASE_STATS;
    ```

4. Consultar as estatísticas

    Cada tipo de estatística fica armazenada em uma tabela específica, sendo elas:
    - Tabelas: `DBA_TAB_STATISTICS`
    - Colunas: `DBA_TAB_COL_STATISTICS`
    - Índices: `DBA_IND_STATISTICS`


## 2. O que são os métodos de acesso do Oracle? Qual a diferença entre Full Table Scan e Index Scan?

Métodos de acesso, ou *access paths* são operações unárias utilizadas por uma consulta para retornar as linhas de alguma fonte, também chamada de, *row source*, uma row source pode ser uma tabela, uma view, o resultado de um join ou uma operação de group.

A database usa diferentes *access paths* dependendo da estrutura dos dados e como eles estão organizados. Num geral eles podem cair em quatro categorias:

| Estrutura dos dados | Access Paths |
| --- | ---- |
| Tabelas organizadas em Heap | Full Table Scan, Table Access by Rowid, Sample Table Scans|
| Tabelas indexadas com B-Tree | Index Unique Scans, I. Range Scans, I. Full Scans, I. Fast Full Scans, etc |
| Tabelas indexadas com Bitmap | Bitmap Index Single Value, Bitmap Index Range Scans, Bitmap Merge |
| Clusters de Tabelas | Cluster Scans, Hash Scans |

----

Com isso vemos a primeira diferença entre um Full Table Scan e um Index Scan, o Full Table Scan é utilizado para tabelas que estão organizadas em heap, isto é, não existe nenhum tipo de índice atrelado a tabela, diferente do Index Scan, que é utilizado em tabelas que possuem índices para os campos solicitados na consulta.

### Full Table Scans

Um Full Table Scan, como o nome indica, lê todas as linhas de uma tabela, e depois filtra as que não atendem aos filtros. Normalmente ela é utilizada quando o Otimizador não pode usar diferentes access paths, ou algum outro access path teria uso maior.

Normalmente as razões pra se utilizar um Full Table Scan invés de um Index Scan são:

- Inexistência de índices - se não existe nenhum índice, então o Otimizador utiliza o full table scan;
- O predicado da query aplica uma função para a coluna indexada - A não ser que o índice é um function-based index, o índice criado para uma coluna não irá funcionar para os valores de uma coluna retornados por uma funlção
- Uma query `SELECT COUNT(*)` em uma coluna indexada, mas a coluna possui nulos
- A query é `unselective` - para casos onde a grande maioria dos blocos da tabela serão retornados na query, não é interessante usar scans indexados. A Full Table Scan utiliza chamadas de I/O no disco maiores do que os outros tipos de scans. E poucas chamadas de I/O retornando grandes blocos é mais eficiente do que varias chamadas de I/O retornando blocos menores;
- As estatísticas da tabela estão antigas - Se as estatísticas da tabela não refletem o estado atual da tabela, o Otimizador pode não saber que um index scan seria mais eficiente do que um full table scan.

---

### Index Scan

Um índice, ou index, é uma estrutura opcional associada a uma tabela ou a um cluster de tabelas, que podem acelerar o acesso aos dados, por poder reduzir o número de acesso ao disco.

A utilização de índices com B-Trees (árvores balanceadas) são o tipo mais comum de índice. Associando uma chave a uma linha ou a um range de linhas. Por isso, A criação de um índice para uma coluna significa ordenar os valores dessa coluna e construir uma árvore balanceada que contenha os valores da coluna seguindo as regras de uma árvore de busca. Isso faz com que o acesso às colunas da árvore indexada seja muito mais eficiente por abandonar a necessidade de varrer a tabela todas as vezes para buscar um dado, que gera uma complexidade próxima a $O(N)$, com a utilização de uma B-Tree essa complexidade cai para $O(log(N))$.

Por exemplo, vejamos uma query que seleciona de uma tabela `empregados`, os que pertencem ao `id_departamento = 20` e o `salario > 1000`. A database que possui um índice na coluna `id_departamento` irá utilizar desse índice para filtrar todas as linhas que possuem o departamento com id 20 fazendo uma busca pela árvore criada, e após isso filtrar as linhas que possuem salário maior que 1000. Note que a ordem desses procedimentos é muito relevante.

O custo de filtragens é sempre decrescente, isto é, a terceira filtragem irá analisar uma quantidade menor ou igual a uma segunda filtragem e assim por diante. Logo, como o índice é uma estrutura mais eficiente de busca, é relevante utilizar as filtragens com índice antes das filtragens sem índice.

## 3. O que é plano de execução?

O plano de execução define a combinação de passos utilizados pela database para executar um SQL statement. Cada passo ou retorna linhas de dados fisicamente da base de dados ou as prepara para a sessão que está utilizando-as.

O plano de execução é gerado pelo otimizador de consultas do oracle com base nas estatísticas disponíveis sobre as tabelas, índices e outras estruturas envolvidas na consulta. Por isso, é interessante que as estatísticas estejam sempre atualizadas para estar de acordo com o estado dos dados, pois estatísticas erradas poderá levar o otimizador a montar planos de execução menos eficientes por considerar os dados em um estado diferente do atual.

O otimizador de consultas analisa e consulta SQL e decide a mlehor maneira de executá-la, considerando os custos estimados dentre as opções disponíveis. O plano de execução resultante é uma representação do caminho escolhido pelo otimizador para acessar os dados de maneira mais eficiente possível.

## 4. Na teoria, entre Hash Join e Nested Join, qual possui a melhor performance? Quando o Otimizador opta por um ou pelo outro? Explicar a diferença deles e o que faz um ser mais rápido que o outro.

O Nested Loop Join, conceitualmente, são equivalente a dois laços `for` aninhados. Ele faz a junção de uma tabela X (`for` externo) com uma Y (`for` interno), para cada linha da tabela Y que satisfaz as condições, a database retorna todas as linhas que satisfazem as condições do join. Se um índice está disponível, então a database pode utilizar para acessar a tabela Y utilizando o rowid. O pior caso do Nested Loop Join é $O(N*M)$.

Nested Loop Joins são uteis quando a database está juntando dois datasets pequenos, ou as condições de join são um método eficiente de acessar a tabela Y.

Num geral, os nested loop joins funcionam melhor em pequenas tabelas com índices presentes nas condições de join, e somado a isso, o número de linhas esperadas pelo join é um fator que influencia a decisão do otimizador para escolher o uso de Nested Loop Joins.

Já, o Hash Join é utilizado para juntar datasets grandes.

O otimizador utiliza a menor dataset entre as duas para construir uma hash table na memória com a chave usada para o join, utilizando uma função hash deterministica para especificar o lugar dentro da hash table que cada linha irá ser armazenada. Então a database varre a database maior para construir a hash table procurando as linhas que atendem a condição especificada.

Num geral, o otimizador considera o uso da hash join quando um grande conjunto de dados irá sofrer um join, e o join é um equijoin (um join feito por equidade, identificando matching columns). O hash join é mais eficaz quando o dataset menor cabe na memória, nesse caso, o custo é limitado a uma única varredura por cada dataset. Que resulta em uma complexidade $O(N + M)$ sendo N e M o tamanho das duas databases que estão sofrendo o join.

Se o menor dataset não cabe na memória, então a database particiona as linhas, e o join procede por cada partição. Esse método utiliza muita memória, e muitas chamadas de I/O para a tablespace temporária. O hash join ainda pode ser mais *cost effective*, principalmente quando a database se utiliza de paralelização de queries.




## 5. Descreva o funcionamento do Otimizador Oracle.

O otimizador tenta gerar o melhor plano de execução para um SQL statement.

O otimizador escolhe o plano com o menor custo entre todos os planos de execução considerados. Ele irá utilizar as estatísticas disponíveis para calcular os custos. Para uma query especifica em um dado ambiente, o custo computacional envolve as operações de I/O, CPU e de comunicação.

A otimização das queries é o processo mais comum para escolher o método mais eficiente de executar um SQL statement. O otimizador determina o plano ótimo para um SQL Statement analisando múltiplos métodos de acesso, múltiplos métodos de join, ordens diferentes de join e possíveis transformações.

Para cada query o otimizador atribui um valor numérico que é relativo ao custo de processamento de cada passo do plano de execução, e depois junta os valores para cada passo para ter um custo médio estimado para o plano. Depois de fazer esse cálculo para todos os planos, o otimizador escolhe o que apresenta o menor custo, e por essa razão o otimizador também é chamado de **Cost-Based Optimizer (CBO)**.

## 6. Por que uma mesma consulta pode apresentar tempos de execuções distintos entre a LUZ e a base dos clientes, sendo que ambas as bases estão na mesma versão?

Mesmo que a versão seja a mesma é possível que a base de dados esteja diferente o que iria causar diferença no tempo de tempo entre as consultas. Além disso, o estado das estatísticas afeta como o otimizador monta o plano de execução para a query. Então mesmo que os clientes estejam na mesma versão com a mesma base de dados, um cliente que esteja com suas estatísticas não atualizadas para o formato atual dos dados irá obter uma performance pior na execução da consulta.

## 7. Ainda no contexto da pergunta acima, a consulta pode apresentar tempos de execução distintos sendo que as bases possuem exatamente os mesmos dados? Por que?

Sim. Assumindo que dois sistemas que estejam na mesma versão possuindo a mesma base de dados, é possível que o tempo de uma consulta seja diferente. O tempo de uma consulta não é definido apenas pela forma que as consultas são implementadas, mas também por como o otimizador monta o plano de execução. Para o otimizador conseguir montar o plano de execução ele necessita de uma versão atualizada das estatísticas das tabelas para conseguir tomar decisões mais adequadas referente ao modelo dos dados.

Portanto, a razão de dois sistemas na mesma versão possuindo a mesma base de dados possuem tempos de consulta diferente é a diferença no estado das estatísticas referente às tabelas.

## 8. Por que um índice melhora o desempenho da consulta? Ele vai melhorar o desempenho em todos os casos? Por que?

Foi muito discutido na questão 2 a razão de consultas utilizando índices tender a ter uma eficiência maior do que algum full table scan. No entanto, nem sempre ela irá garantir esse ganho de eficiência.

O uso de índices em tabelas pequenas não é recomendado, normalmente o overhead de manter um índice pode não justificar o ganho em eficiência de um índice, a real vantagem do uso de índices escala com o tamanho da tabela indexada.

Queries que irão retornar a maior parte dos dados da tabela não se beneficiam do uso de um índice, já que a maior parte dos dados irá ser retornada na consulta, talvez o uso de um full table scan seja mais eficiente, por possibilitar o retorno de maiores blocos de dados em cada chamada de I/O, em contraste aos scans indexados que fazem pequenos fetches de dados ao disco que para casos como esse não é tão interessante.

E por fim, tabelas que recebem muitas alterações (inserção, atualização, deleção) precisam ter seus índices atualizados constantemente, e o overhead gerado por uma tabela precisar ter seus índices atualizados constantemente costuma não compensar o ganho em consulta do uso de um índice.

Portanto, é necessário analisar a necessidade da criação de um índice, cada índice requer um espaço adicional de armazenamento proporcional ao tamanho da tabela, e eles precisam ser atualizados a cada alteração, o que pode impactar as operações de write. Os índices possuem suas vantagens e desvantagem muito bem definidos, portanto é necessário levá-los em conta para se tomar a decisão de se criar um índice ou não.

## 9. Escreva sobre as boas práticas de joins apresentadas no treinamento.

Boas práticas referentes ao join garantem ganhos substanciais na eficiência de uma consulta. Algumas das principais são:

- **Filtrar os dados antes de executar algum join**: o join tem o potencial de ter uma complexidade maior do que a filtragem, por isso é muito interessante executar as filtragens (que são operações que costumam ser menos custosas que um join) primeiro, e depois o join;
- **Fazer somente a seleção dos campos necessários para a consulta**: evitar o uso de `SELECT *` a não ser que seja realmente necessário. Toda coluna que for retornado pelo select utiliza de recursos e impactam na performance da query, por isso é interessante selecionar apenas os campos relevantes;
- **Não utilizar joins na `select` query**: O SELECT é uma query que irá ser executada N vezes, sendo N o número de linhas resultantes da query, logo, o uso de um join dentro de um select irá fazer com que esse join seja executado N vezes invés de 1 vez que é o esperado, esse mal uso do join pode causar um impacto muito grande na performance de um join;
- **Otimizar os índices**: Se for interessante para a tabela, considerar o uso de índices e utilizar joins nas colunas indexadas, em queries que utilizam de equijoins ou qualquer filtro WHERE ou principalmente ON que irá retornar um número muito menor de linhas comparados com a tabela original, o uso de índices pode garantir um ganho de performance muito grande;
- **Utilizar os Joins apropriados, e se possível, evitar joins excessivos**: Saber qual tipo de Join utilizar (INNER, RIGHT, LEFT) é imprescindível para se obter os resultados desejados. Somado a isso, é necessário se atentar ao modelo dos seus dados, e se possível, evitar o uso de muitos joins por ser uma operação custosa. Levar em conta a possibilidade do uso da desnormalização dos dados, utilizar de operadores UNION ALL e DISTINCT, as vezes eles podem garantir um custo de performance inferior a um join.
- **Testar e analisar diferentes formas de execução**: Como o plano de execução pode variar dependendo de como a query é escrita, e fatores como cache podem otimizar algumas queries de custo alto, é interessante analisar o custo no plano de execução e testar formas diferentes de executar a mesma query buscando uma performance maior. Via de regra, escrevemos a query do jeito mais simples possível para conseguirmos obter o resultado que queremos, e após isso fazer o refinamento necessário para a query se tornar o mais otimizada possível.

Essas são algumas boas práticas para se considerar quando estiver trabalhando com joins, não são as únicas, mas garantindo essas é muito provável que as queries sejam mais eficientes do que se elas não fossem levadas em consideração.

## 10. Pensando em uma consulta que retorna mais de 1 milhão de dados, quais são as boas práticas para manipular os dados dela em código de forma eficiente?

É necessário utilizar de tudo que foi discutido até o momento para otimizar uma query, desde a discussões sobre os índices, boas práticas que garantem uma maior eficiência na consulta.

Construir uma consulta funcional que retorna os dados que queremos é o primeiro passo para a otimização dessa query, com ela funcionando podemos começar a pensar em estratégias de otimização.

Se essa consulta utiliza muitos joins, é interessante filtrar os dados dessas tabelas a priori para que os joins sejam mais eficientes. Se as tabelas utilizadas nas consultas possuírem índices, utilizar esses índices para executar as filtragens e os joins, principalmente os que irão filtrar uma parte considerável da tabela, (uma filtragem que transforma uma tabela de 30mi de linhas em 5mi de linhas) pois são nesses casos que os índices são mais eficientes.

Também é muito importante manter as estatísticas das tabelas atualizadas, pois muito do trabalho de otimização da query é feita pelo otimizador e não pelo programador, então é muito relevante que o otimizador possa fazer decisões em cima de estatísticas atuais da tabela.

Levando tudo isso em conta, conseguimos tanto otimizar a consulta na tomada de decisão da ordem e forma das queries utilizando de boas práticas, quanto conseguimos tirar o maior proveito do otimizador da oracle em escolher o melhor plano de execução para a consulta. Com isso conseguimos garantir uma forma eficiente de se executar uma query complexa.

