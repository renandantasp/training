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

insert into pessoa_rdp values(12, 'Pedro Serra', '13/11/1999', 'M');
insert into pessoa_rdp values(13, 'Bruna Alves', '16/05/2003', 'F');

merge into PESSOA_RDP P
using (SELECT PE.CODIGO FROM PESSOA_RDP PE LEFT JOIN EMPREGADO_RDP EM ON PE.CODIGO = EM.CODIGO_PESSOA WHERE EM.SALARIO IS NULL) E
on (P.CODIGO = E.CODIGO)
WHEN MATCHED THEN
 UPDATE SET P.NOME = P.NOME 
 DELETE WHERE P.NOME = P.NOME;


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 4
/*

Um trigger é um objeto da database associado a uma tabela e executado automáticamente como resposta a eventos ou 
ações especificas executadas na tabela. Um Trigger consiste em um Trigger Event que especifica qual ação vai ativar
esse Trigger, e um Trigger Body, que contém as ações que serão executadas quando o trigger for ativado.
Os dois principais tipos de trigger são os trigger "before" e "after".

Os "before" trigger são executados ANTES da ação que o ativou. Isso permite que você possa modificar o dado antes
de ser inserido/removido/alterado. Os before trigger costumam ser usados para validação dos dados. Por exemplo,
um before trigger em uma operação de INSERT pode validar o dado que está sendo inserido e rejeitar a inserção caso
falhe as condições.

Os "after" trigger são executados DEPOIS da ação que o ativou. Isso permite ações adicionais ou a implementação de
ações que refletem as alterações feitas pela ação que o ativou. After triggers são utilizados para Logging, como 
a inserção de timestamps em uma tabela T2 que guarda as alterações feitas em outra tabela T1, e para atualização ou 
qualquer aplicação de regras de negócio após uma deleção/inserção/alteração feita em uma tabela T1.

*/

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 5

/*
Indices são utilizados para o aumento da performance na execução de queries utilizando campos específicos de alguma tabela.
Os indices fornecem uma estrutura de dados que permitem que ações como: ordenação de dados, retorno mais eficiente dos dados, 
entre outras operações.
No entanto, o uso de indices também apresentam alguns trade-offs, como o aumento do espaço em disco para armazenas os indices,
os índices também podem impactar o tempo de inserção/deleção/alteração em uma tabela dado que o índice precisa se atualizar para
continuar funcionando.
*/
-- Um indice pode ser criado com a seguinte sintaxe:

CREATE INDEX <INDEX_NAME> ON <TABLE_NAME> (<FIELDS>)
-- OU
CREATE INDEX <INDEX_NAME> ON <TABLE_NAME> (<FIELDS>) TABLESPACE <TABLESPACE_NAME> COMPUTE STATISTICS

/*
Abaixo tem um exemplo de um indice chamado IX_PESSOA_RDP_NOME sendo criado para a tabela PESSOA_RDP
no campo NOME, esse indice será armazenado no tablespace G2K_PRIV_INDX:
*/
CREATE INDEX IX_PESSOA_RDP_NOME ON PESSOA_RDP (NOME) TABLESPACE G2K_PRIV_INDX COMPUTE STATISTICS

/*
após um índice ser criado e ele estiver buildado, toda query que utilize os campos que 
estão presentes no índice farão uso do índice.
*/