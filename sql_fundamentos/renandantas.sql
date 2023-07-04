----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 1

SELECT
    P.NOME AS EMPREGADO,
    TRUNC(MONTHS_BETWEEN(SYSDATE, P.DATA_NASCIMENTO)/12) as IDADE,
    D.NOME DEPARTAMENTO,
    TRUNC(MONTHS_BETWEEN(SYSDATE, E.DATA_ADMISSAO)/12) as TEMPO_DE_CASA
FROM
    EMPREGADO_RDP E
    INNER JOIN PESSOA_RDP P on E.CODIGO_PESSOA = P.CODIGO
    INNER JOIN DEPARTAMENTO_RDP D on E.COD_DEPARTAMENTO = D.CODIGO
where 
    DATA_NASCIMENTO < '01/01/1990';


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

CREATE OR REPLACE VIEW INFO_EMPREGADOS (EMPREGADO, SEXO, IDADE, TEMPO_DE_CASA, SIGLA, SALARIO) AS
SELECT
    UPPER(P.NOME) AS EMPREGADO,
    P.SEXO AS SEXO,
    TRUNC(MONTHS_BETWEEN(SYSDATE, P.DATA_NASCIMENTO)/12) as IDADE,
    TRUNC(MONTHS_BETWEEN(SYSDATE, E.DATA_ADMISSAO)/12) as TEMPO_DE_CASA,
    UPPER(D.SIGLA) AS SIGLA,
    E.SALARIO AS SALARIO
FROM
    EMPREGADO_RDP E
    INNER JOIN PESSOA_RDP P on E.CODIGO_PESSOA = P.CODIGO
    INNER JOIN DEPARTAMENTO_RDP D on E.COD_DEPARTAMENTO = D.CODIGO;

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 3

DELETE FROM PESSOA_RDP WHERE 
  CODIGO IN (SELECT
  P.CODIGO
FROM
  PESSOA_RDP P
  LEFT JOIN EMPREGADO_RDP E ON P.CODIGO = E.CODIGO_PESSOA
WHERE
  E.CODIGO IS NULL);


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 4

Um exemplo de trigger onde deve ser utilizado a clausula "before" é em casos como apresentado no treinamento, antes da inserção
de algum elemento em uma tabela, campos como de chave primária ou qualquer outro campo not null e unique que seja interessante que 
a própria tabela admnistre esses casos. Um outro exemplo é o caso de integridade dos dados, algumas condições podem 

Alguns exemplos de trigger onde se pode ser utilizado a clausula "after" são:
- Logging, como a inserção de timestamps em uma tabela T2 que guarda as alterações feitas em outra tabela T1;
- Atualização ou qualquer aplicação de regras de negócio após uma deleção/inserção/alteração feita em uma tabela T1


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 5

Indices são utilizados para o aumento da performance na execução de queries utilizando campos específicos de alguma tabela.
Os indices fornecem uma estrutura de dados que permitem que ações como: ordenação de dados, retorno mais eficiente dos dados, 
entre outras operações.
No entanto, o uso de indices também apresentam alguns trade-offs, como o aumento do espaço em disco para armazenas os indices,
os índices também podem impactar o tempo de inserção/deleção/alteração em uma tabela dado que o índice precisa se atualizar para
continuar funcionando.

Um indice pode ser criado com a seguinte sintaxe:

CREATE INDEX <INDEX_NAME> ON <TABLE_NAME> (<FIELDS>)
OU
CREATE INDEX <INDEX_NAME> ON <TABLE_NAME> (<FIELDS>) TABLESPACE <TABLESPACE_NAME> COMPUTE STATISTICS

Abaixo tem um exemplo de um indice chamado IX_PESSOA_RDP_NOME sendo criado para a tabela PESSOA_RDP
no campo NOME, esse indice será armazenado no tablespace G2K_PRIV_INDX:

CREATE INDEX IX_PESSOA_RDP_NOME ON PESSOA_RDP (NOME) TABLESPACE G2K_PRIV_INDX COMPUTE STATISTICS

após um índice ser criado e ele estiver buildado, irão utilizar do índice qualquer query 
que utilize os campos que estão presentes no índice