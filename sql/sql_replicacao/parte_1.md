## 1. O que é a RECRIA_TODOS ?

A `RECRIA_TODOS` é uma procedure responsável por excluir e reciar os usuários do banco de dados.

Recria permissões e sinonimos para o `USER_GERENCIAL`.

## 2. Qual a diferença entre o usuário GERENCIAL e USER_GERENCIAL?
`GERENCIAL` é o usuário que possui permissões de DDL e outras persmissões de admnistrador.

As tabelas do `GERENCIAL` são recriadas para dentro do `USER_GERENCIAL`.

O usuário `USER_GERENCIAL`, que é utilizado pelas aplicações, apenas possui permissões de DML.

## 3. Qual a relação entre a RECRIA_TODOS e a LEF.INSERE_PERM_OBJETOS_PADRAO?

A `RECRIA_TODOS` usa todos os dados que existem na `LEF.INSERE_PERM_OBJETOS_PADRAO` pra poder criar o `USER_GERENCIAL`.

## 4. O que é um Objeto Inválido em ORACLE?

Um objeto inválido é uma PROCEDURE, TRIGGER ou algum outro objeto que está compilado para operar em uma tabela da qual 
o estado foi alterado, seja essa alteração a adição de uma nova coluna, ou o drop dessa tabela. 

Dependendo do estado da tabela da qual o objeto opera, e da forma que esse procedimento opera o 
ato de recompilar pode fazer essa objeto deixar de ser inválido.

Por exemplo, um trigger T que gera um log a cada inserção em uma tabela A, no entanto, a tabela A sofreu uma alteração, 
a adição de uma nova coluna. O trigger irá ficar inválido após essa atualização, no entanto, por nenhuma das funções do 
trigger ser impactado por haver uma nova coluna na tabela, a recompilação do trigger irá ser possível e ele irá funcionar novamente.

No entanto, se a tabela for excluida, a recompilação não irá funcionar pois a tabela do qual
o trigger opera já não existe mais.

## 5. O que é uma Consulta Hierárquica? Você conseguiu identificar alguma até agora? Se sim, o que ela faz? 

Uma Consulta Hierarquica ou Hierarchical Retrieval é uma consulta que utiliza de recursão para conseguir consultar dados 
organizados em estruras de arvore/grafos, sistema de arquivos, ou qualquer outra estrutura que possua algum grau de hierarquia. 

Para executar uma consulta hierarquica, o SQL possui uma feature chamada recursive queries, que permitem a traversia em uma 
estrutura hierarquica e retorne os dados de diferentes niveis de hierarquia. No ORACLE, essa feature é implementada pela 
clausula CONNECT BY.

```sql
SELECT 
    COLUMNS
FROM 
    TABLE   
CONNECT BY [NOCYCLE] PRIOR CHILD_COLUMN = PARENT_COLUMN
START WITH CONDITION;
```
`CONNECT BY` - Indica o início da hierarchical query.

`[NOCYCLE]` - Uma keyword adicional que previne que a query de entrar loops dentro da hierarquia. Evitando
loops infinitos quando estão dentro de relacionamentos circulares.

`PRIOR CHILD_COLUMN = PARENT_COLUMN` - Especifica o relacionamento entre as colunas pai e filho da tabela.
Define como a estrutura hierarquica é formada.

`STARTS WITH` - Especifica o início, ou a raiz, da hierarquia baseado na condição.

## 6. Qual a função da procedure PERMISSOES_OBJETOS?

A PERMISSOES_OBJETOS controla as permissões dos usuários do Gerencial. 
Essa Procedure chama a PRIV_CONCEDE_DBA que o usuario GERENCIAL concede permissoes para o GERENCIAL_DBA.

A procedure PERMISSOES_OBJETOS também chama a procedure RECRIA_TODOS_USUARIOS que cria sinônimos dos usuários do Gerencial

## 7. O que é PL/SQL?
PL/SQL (Procedural Language/Structured Query Language) é uma extensão de programação procedural para desenvolvimento dentro do ambiente Oracle. 
Ele provê ações adicionais ao SQL por permitir a criação de stored procedures, triggers, functions, packages e mais.

Algumas das features do PL/SQL são:
    
- Programação procedural: loops, condicionais, error handling (expception).
- Segurança: Utilizar de stored procedures e funções, que podem encapsular regras de negócio e restringir acesso direto aos dados, adicionando uma camada de segurança.
- Exception Handling - catch and handle exceptions.
- Performance: por executar do server da database reduzingo a necessidade de comunicação entre a aplicação e o banco de dados.

## 8. O que é um cursor?

Cursor é um objeto que permite iterar sobre cada linha do resultado de uma query para executar operações em cada linha individualmente. 

Ele fornece uma forma de processar o resultado de uma query sequencialmente, linha por linha, permitindo que possa executar operações 
em cada linha retornada por uma query.

Um exemplo de uso de um cursor seria:

```sql
DECLARE
    EMP_ID VARCHAR(10);
    EMP_NAME VARCHAR(100);

    CURSOR EMP_CURSOR IS
        SELECT EMPLOYEE_ID, NAME
        FROM EMPLOYEES
        WHERE DEPARTMENT_ID = '029319230';
    BEGIN
        --abre o cursor
        OPEN EMP_CURSOR

        --itera sobre os resultados
        LOOP
            --retorna o valor da próxima linha para as variaveis
            FETCH EMP_CURSOR INTO EMP_ID, EMP_NAME;
            
            -- sai do loop caso não tenha mais linhas
            EXIT WHEN EMP_CURSOR%NOTFOUND;
            
            --executa o procedimento da linha atual
            DBMS_OUTPUT.PUT_LINE('Employee ID: ' || emp_id || ', Employee Name: ' || emp_name);

        END LOOP;
        -- fecha o cursor
        CLOSE EMP_CURSOR;
    END;
```

## 9. O que é um fetch?

O FETCH é o comando utilizado para obter os dados contidos na linha atual do objeto do cursor e retorná-los para as variaveis utilizadas no loop. 

Isso permite que se possa fazer operações e cálculos baseado nos valores contido da linha retornada.
