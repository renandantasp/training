# 1. O que são as estatísticas de uma base Oracle? O que precisa ser feito para elas serem coletadas?

Estatísticas são uma parte chave para o Otimizador conseguir definir o melhor plano de execução para uma SQL statement. As estatisticas do Otimizador incluem:

- Estatisticas de Tabela
 - Número de linhas
 - Número de blocos
 - Média do tamanho das linhas
- Estatistica de Coluna
 - Número de valores distintos (NDV) em uma coluna
 - Número de valores núlos em uma coluna
 - Distribuição dos dados (histograma)
 - Estatísticas extendidas (um tipo de estatística que aperfeiçoa a estimativa de cardinalidade quando multiplos predicados existem)
- Estatistica dos Índices
 - Número de blocos folha
 - Número de níveis
 - Fator de clustering do Índice
- Estatísticas do sistema
 - Desempenho e utilização de Input/Output
 - Desempenho e utiliziação de CPU

 Para coletar as estatísticas de uma base Oracle, você pode:

1. Habilitar a coleta automática de estatísticas:

    O Oracle possui um recurso chamado "coelta automática de estatísticas" que pode ser ativado para coletar automaticamente estatisticas sobre as tabelas e índices da base de dados. Para habilitar esse recurso, é executado esse comando:

    ```sql
    ALTER SYSTEM SET STATISTICS_LEVEL = ALL;
    ```

2. Ativar a opção de coleta automática de objetos:

    Para garantir que a coleta automática de estatíticas seja realizada regularmente em todos os objetos, é executado esse comando:    

    ```sql
    ALTER DATABASE SET AUTO_STAT_EXTENSIONS = TRUE;
    ```

3. Agendar a coleta automática de estatisticas

    É possível configurar o Oracle para coletar estatísticas automaticamente em intervalo regulares. Para fazer isso, você pode criar um job no Oracle Scheduler para executar o procedimento de coleta de estatísticas. Por exemplo, é possível criar um job para executar a seguinte procedure:
    ```sql
    DBMS_STATS.GATHER_DATABASE_STATS;
    ```

4. Consultar as estatísticas

    Cada tipo de estatística fica armazenada em uma tabela específica, sendo elas:
    - Tabelas: `DBA_TAB_STATISTICS`
    - Colunas: `DBA_TAB_COL_STATISTICS`
    - Índices: `DBA_IND_STATISTICS`
    - Tabelas: `DBA_TAB_STATISTICS`



# 2. O que são os métodos de acesso do Oracle? Qual a diferença entre Full Table Scan e Index Scan?

Métodos de acesso, ou *access paths* são operações unárias utilizada por uma consulta para retornar as linhas de alguma fonte a.k.a. *row source*, uma row source pode ser uma tabela, uma view, o resultado de um join ou uma operação de group.

A database usa diferentes access paths dependendo da estrutura dos dados e como eles estão organizados. Num geral eles podem cair em quatro categorias:

| Estrutura dos dados | Access Paths |
| --- | ---- |
| Tabelas organizadas em Heap | Full Table Scan, Table Access by Rowid, Sample Table Scans|
| Tabelas indexadas com B-Tree | Index Unique Scans, I. Range Scans, I. Full Scans, I. Fast Full Scans, etc |
| Tabelas indexadas com Bitmap | Bitmap Index Single Value, Bitmap Index Range Scans, Bitmap Merge |
| Clusters de Tabelas | Cluster Scans, Hash Scans |

----

Com isso vemos a primeira diferença entre um Full Table Scan e um Index Scan, o Full Table Scan é utilizado para tabelas que estão organizadas em heap, isto é, não existe nenhum tipo de índice atrelado a tabela, diferente do Index Scan, que é utilizado em tabelas que possuem índices para os campos solicitados na consulta.

### Full Table Scan

Um Full Table Scan, como o nome indica, lê todas as linhas de uma tabela, e depois filtra as que não atendem aos filtros. Normalmente ela é utilizada quando o Otimizador não pode usar diferentes access paths, ou algum outro access path teria uso maior.

Normalmente as razões pra se utilizar um Full Table Scan inves de um Index Scan são:

- Inexistência de índices - se não existe nenhum índice, então o Otimizador utiliza o full table scan;
- O predicado da query aplica uma função para a coluna indexada - A não ser que o índice é um function-based index, o índice criado para uma coluna não irá funcionar para os valores de uma coluna retornados por uma funlção
- Uma query `SELECT COUNT(*)` em uma coluna indexada, mas a coluna possui nulos
- A query é `unselective` - para casos onde a graande maioria dos blocos da tabela serão retornados na query, não é interessante usar scans indexados. A Full Table Scan utiliza chamadas de I/O no disco maiores do que os outros tipos de scans. E poucas chamadas de I/O retornando grandes blocos é mais eficiente do que varias chamadas de I/O retornando blocos menores;
- As estatisticas da tabela estão antigas - Se as estatisticas da tabela não refletem o estado atual da tabela, o Otimizador pode não saber que um index scan seria mais eficiente do que um full table scan.

---

### Index Scan

Um índice, ou index, é uma estrutura opcional associada a uma tabela ou a um cluster de tabelas, que podem acelerar o acesso aos dados, por poder reduzir o número de acesso ao disco.

A utilização de índices com B-Trees (árvores balanceadas) são o tipo mais comum de índice. Associando uma chave a uma linha ou a um range de linhas, a B-Tree provê um desempenho




# 3. O que é plano de execução?

A cominação de passos utilizados pela database para executar um SQL statement. Cada passo ou retorna linhas de dados fisicamente da base de dados ou as prepara para a sessão que está utilizando-as.

O plano de execução é gerado pelo otimizador de consultas do oracle com base nas estatísticas disponíveis sobre as tabelas, índices e outras estruturas envolvidas na consulta.

O otimizador de consultas analisa e consulta SQL e decide a mlehor maneira de executá-la, considerando os custos estimados dentre as opções disponíveis. O plano de execução resultante é uma representação do caminho escolhido pelo otimizador para acessar os dados de maneira mais eficiente possível.

# 4. Na teoria, entre Hash Join e Nested Join, qual possui a melhor performance? Quando o Otimizador opta por um ou pelo outro? Explicar a diferença deles e o que faz um ser mais rápido que o outro.




O Hash Join é utilizado para juntar datasets grandes.

O otimizador utiliza a menor dataset entre as duas para construir uma hash table na memória com a chave usada para o join, utilizando uma função hash deterministica para especificar o lugar dentro da hash table que cada linha irá ser armazenada. Então a database varre a database maior para construir a hash table procurando as linhas que atendem a condição especificada.

Num geral, o otimizador considera o uso da hash join quando um grande conjunto de dados irá sofrer um join, e o join é um equijoin (um join feito por equidade, identificando matching columns). O hash join é mais eficaz quando o dataset menor cabe na memória, nesse caso, o custo é limitado a uma única varredura por cada dataset. Que resulta em uma complexidade O(N + M) sendo N e M o tamanho das duas databases que estão sofrendo o join.

Se o menor dataset não cabe na memória, então a database particiona as linhas, e o join procede por cada partição. Esse método utiliza muita memória, e muitas chamadas de I/O para a tablespace temporária. O hash join ainda pode ser mais *cost effective*, principalmente quando a database se utiliza de paralelização de queries.





# 5. Descreva o funcionamento do Otimizador Oracle.


# 6. Por que uma mesma consulta pode apresentar tempos de execuções distintos entre a LUZ e a base dos clientes, sendo que ambas as bases estão na mesma versão?


# 7. Ainda no contexto da pergunta acima, a consulta pode apresentar tempos de execução distintos sendo que as bases possuem exatamente os mesmos dados? Por que?


# 8. Por que um índice melhora o desempenho da consulta? Ele vai melhorar o desempenho em todos os casos? Por que?    


# 9. Escreva sobre as boas práticas de joins apresentadas no treinamento.


# 10. Pensando em uma consulta que retorna mais de 1 milhão de dados, quais são as boas práticas para manipular os dados dela em código de forma eficiente?