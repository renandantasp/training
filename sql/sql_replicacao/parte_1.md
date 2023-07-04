## 1. O que é a RECRIA_TODOS ?

A RECRIA_TODOS é uma procedure responsável por remover e recriar um usuário do banco de dados.

Recria permissões e sinonimos para o USER_GERENCIAL.

## 2. Qual a diferença entre o usuário GERENCIAL e USER_GERENCIAL?
GERENCIAL é o usuário que possui permissões de DDL e outras persmissões de admnistrador.

O usuário USER_GERENCIAL apenas possui permissões de DML

## 3. Qual a relação entre a RECRIA_TODOS e a LEF.INSERE_PERM_OBJETOS_PADRAO?

A RECRIA_TODOS usa todos os dados que existem na LEF.INSERE_PERM_OBJETOS_PADRAO pra poder criar o USER_GERENCIAL.

## 4. O que é um Objeto Inválido em ORACLE?

Um objeto inválido é uma PROCEDURE/TRIGGER que está compilado para operar em uma tabela da qual o estado foi alterado.

Seja essa alteração a adição de uma nova coluna, ou o drop dessa tabela. Dependendo do estado da tabela da qual a
PROCEDURE/TRIGGER opera, e como esse procedimento opera o ato de recompilar pode fazer essa PROCEDURE/TRIGGER 
    deixar de ser inválida.

## 5. O que é uma Consulta Hierárquica? Você conseguiu identificar alguma até agora? Se sim, o que ela faz? 

Uma Consulta Hierarquica ou Hierarchical Retrieval é uma consulta que utiliza de recursão e por isso consegue consultar dados organizados em estruras de arvore/grafos ou qualquer outra estrutra de parentesco. 

## 6. Qual a função da procedure PERMISSOES_OBJETOS?

A PERMISSOES_OBJETOS controla as permissões dos usuários do Gerencial. Essa Procedure chama a PRIV_CONCEDE_DBA que o usuario GERENCIAL concede permissoes para o GERENCIAL_DBA

## 7. O que é PL/SQL?
PL/SQL (Procedural Language/Structured Query Language) é uma extensão de programação procedural para desenvolvimento dentro do ambiente Oracle. Ele provê ações adicionais ao SQL por permitir a criação de stored procedures, triggers, functions, packages e mais.

Algumas das features do PL/SQL são:
    
- Programação procedural: loops, condicionais, error handling (expception).
- Segurança: Utilizar de stored procedures e funções, que podem encapsular regras de negócio e restringir acesso direto aos dados, adicionando uma camada de segurança.
- Exception Handling - catch and handle exceptions.
- Performance: por executar do server da database reduzingo a necessidade de comunicação entre a aplicação e o banco de dados.

## 8. O que é um cursor?

Cursor é um objeto que permite iterar sobre cada linha do resultado de uma query para executar operações em cada linha individualmente.

## 9. O que é um fetch?
Você pode usar o FETCH para obter a linha atual do dado contido no cursor. Isso permite que se possa fazer operações e cálculos baseado nos valores contido da linha retornada.
