## 1. O que é uma Role?
    
A Role representa um conjunto de privilegios/permissões que um usuário pode ter.

## 2. O que é o comando Grant?
    
O comando Grant *concede* privilégios a um usuário específico, a uma Role ou a todos os usuários para poder executar ações no banco de dados. 
Além disso, o comando GRANT também *concede* roles a um usuário, a outra role ou ao PUBLIC.

## 3. Explique a complexidade de um merge


## 4. Explique o comando ORACLE SELECT utilizando o DUAL

O DUAL é uma tabela especial de uma linha e uma coluna que é utilizado para executar cálculos e validações que não necessitam de nenhuma tabela específica.

A tabela DUAL é utilizada junto com o comando SELECT para executar as operações. É muito utilizado para gerar constante, avaliar expressões, entre outros.

## 5. De um exemplo da utilização do DUAL

`SELECT 1 + 2 AS RESULT FROM DUAL;`

Nesse exemplo o comando SELECT executa uma simples operação de soma. A Tabela DUAL é utilizada pra permitir que o calculo seja feito sem referenciar nenhuma tabela ou dado real. O resultado desse cálculo fica em uma tabela de uma linha e uma coluna com o titulo da coluna sendo "RESULT"

## 6. Qual a diferença entre exists e in?

O operador `EXISTS` é utilizado para verificar a *existência* de colunas de uma subquery. Ele retorna um valor booleano, isto é
retorna True se a subquery tem alguma coluna, e retorna False caso não haja.

O operador `IN` é utilizado para verificar a existencia de um valor em uma lista de valores. Ele também retorna um booleano,
True caso o valor esteja contido, e False caso não.

`EXISTS` é utilizado para checar a existencia de linhas em uma subquery, já o `IN` é utilizado para comparar se um valor X está dentro dos valores retornados da subquery.

## 7. No vídeo "Permissões Base", identifique o trecho apresentado em 00:19s de vídeo e explique a responsabilidade deste trecho.

## 8. O que é uma Tabela Temporária?

Uma Tabela temporária em SQL é um database object que dura por apenas uma sessão, ou uma transação. 

É utilizada para armazenar e manipular dados temporários dentro de um contexto específico, como uma stored procedure, uma user session, ou uma transação.

## 9. Porque é interessante usar uma Tabela Temporária?

Tabelas temporárias são uteis quando você precisa armazenar dados ou algum tipo de cálculo que não necessita de algum tipo de persistencia na database.

As tabelas temporárias provê uma forma de armazenar dados temporariamente, fazer as operações necessárias e discartá-los logo em seguida.

## 10. Como se cria uma Tabela Temporária?

```
CREATE GLOBAL TEMPORARY TABLE my_temp_table (
    id           NUMBER,
    description  VARCHAR2(20)
)
ON COMMIT DELETE ROWS;
```
A Clausula ```ON COMMIT DELETE ROWS``` garante que a tabela será deletada no final da sessão.