## 1. O que é uma Role?
    
A Role representa um conjunto de privilegios/permissões que um usuário pode ter.

## 2. O que é o comando Grant?
    
O comando Grant *concede* privilégios a um usuário específico, a uma Role ou a todos os usuários para poder executar ações no banco de dados. 
Além disso, o comando GRANT também *concede* roles a um usuário, a outra role ou ao PUBLIC.

## 3. Explique a complexidade de um merge

A complexidade de tempo e de espaço de uma operação merge depende de diversos fatores, como o tamanho das tabelas envolvidas, o número de linhas que serão mergeadas, a complexidade das condições de merge, e caso haja ou não índices referentes as tabelas envolvidas. Portanto, a complexidade de um merge é na verdade a complexidade das queries utilizadas para efetuar o merge, e as queries normalmente irão utilizar ou um HASH JOIN ou um MERGE JOIN para otimizar suas consultas.

A complexidade de espaço pode ser $O(1)$, ou $(N + M)$ que é o caso de um HASH JOIN, sendo $N$ e $M$ o tamanho das tableas target e source, ou até até $O(N  \log(n) + M \log(M))$

Já a complexidade de espaço de uma operação merge irá depender de fatores como o tamanho das tabelas, o número de linhas que será mergeado. Pode ir desde $O(1)$ onde nenhum espaço adicional é necessário, ou até $O(N)$ no caso onde o MERGE utiliza um HASH JOIN.

## 4. Explique o comando ORACLE SELECT utilizando o DUAL

O DUAL é uma tabela especial de uma linha e uma coluna que é utilizado para executar cálculos e validações que não necessitam de nenhuma tabela específica.

A tabela DUAL é utilizada junto com o comando SELECT para executar as operações. É muito utilizado para gerar constante, avaliar expressões, entre outros.

## 5. De um exemplo da utilização do DUAL

```sql
declare
  novo_salario number;
begin
  for I in 
  (
    select SALARIO, ROWID from EMPREGADO_RDP
  )
  loop
    select AUMENTOSALARIO(1.3, SALARIO) into novo_salario from DUAL;
    update EMPREGADO_RDP set SALARIO = AUMENTOSALARIO(1.3, SALARIO) where ROWID = I.ROWID;
  end loop;  
end;   
```

O uso do DUAL nesse caso serve para executar a função `AUMENTOSALARIO` e armazenar na variável `novo_salario` que será utilizado para atualizar o salário.


## 6. Qual a diferença entre exists e in?

O operador `EXISTS` é utilizado para verificar a *existência* de colunas de uma subquery. Ele retorna um valor booleano, isto é
retorna True se a subquery tem alguma coluna, e retorna False caso não haja.

O operador `IN` é utilizado para verificar a existencia de um valor em uma lista de valores. Ele também retorna um booleano,
True caso o valor esteja contido, e False caso não.

`EXISTS` é utilizado para checar a existencia de linhas em uma subquery, já o `IN` é utilizado para comparar se um valor X está dentro dos valores retornados da subquery.

## 7. No vídeo "Permissões Base", identifique o trecho apresentado em 00:19s de vídeo e explique a responsabilidade deste trecho.

O trecho de código apresentado no texto é um pedaço da procedure `PERMISSOES_OBJETOS`, que é uma parte da lógica responsável pela 
verificação das permissoes que devem ser concedidas a partir do que está cadastrado nas tabelas `PERM_%`.

O trecho em específico serve para não mostrar falso positivo para sinônimos. Isso porque se conceder permissão para sinônimos, 
vai aparecer na ALL_TAB_PRIVS apenas o objeto que o sinônimo aponta e não o sinônimo em si.

## 8. O que é uma Tabela Temporária?

Uma Tabela temporária em SQL é um database object que dura por apenas uma sessão, ou uma transação. 

É utilizada para armazenar e manipular dados temporários dentro de um contexto específico, como uma stored procedure, uma user session, ou uma transação.

## 9. Porque é interessante usar uma Tabela Temporária?

Tabelas temporárias são uteis quando você precisa armazenar dados ou algum tipo de cálculo que não necessita de algum tipo de persistencia na database.

As tabelas temporárias provê uma forma de armazenar dados temporariamente, fazer as operações necessárias e discartá-los logo em seguida.

## 10. Como se cria uma Tabela Temporária?

```sql
CREATE GLOBAL TEMPORARY TABLE my_temp_table (
    id           NUMBER,
    description  VARCHAR2(20)
)
ON COMMIT DELETE ROWS;
```
A Clausula ```ON COMMIT DELETE ROWS``` garante que a tabela será deletada no final da sessão.