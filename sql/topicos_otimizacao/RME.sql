select DISTINCT C.COD_CORRETORA, C.NOME, C.NOME_VISUAL from TODAS_OPERACOES T INNER JOIN CORRETORAS C ON T.COD_CORRETORA = C.COD_CORRETORA WHERE C.COD_CORRETORA <> -508 ORDER BY C.COD_CORRETORA ASC; 
SELECT * FROM STOCKS_OPER;
SELECT SUM(PU) FROM FUTUROS_OPER;

SELECT * FROM VIEW_ESTRUTURA;

select
  P.NOME as "Nome do Produto",
  DC.CONTRATO as "Nome do Contrato",
  VE.NOME_CARTEIRA as "Nome da Carteira",
  VE.NOME_TESOURARIA as "Nome da Tesouraria", -- também pode ser encontrada como NOME da tabela SETORES
  VE.NOME_INSTITUICAO as "Nome da Instituição",
  DC.NOME_EMISSOR as "Nome do Emissor", -- ao menos para alguns produtos o emissor do contrato é o devedor
  CON.NOME as "Contraparte", 
  C.NOME as "Corretoras",
  TOP.DATA_ENTRADA as "Data de Aquisição",
  TOP.QUANTIDADE
  --Usar tabela COTACOES
  --Corrertoras: TODAS_OPERACOES tem o COD_CORRETORA, tem relação do COD_CORRETORA com o nome na tab DADOS_CORRETORAS, mas não é um pra um

FROM
  PRODUTOS P
  inner join (SELECT CONTRATO, NOME_EMISSOR, COD_PRODUTO FROM DADOS_CONTRATO ) DC on P.COD_PRODUTO = DC.COD_PRODUTO
  inner join (SELECT COD_PRODUTO, QUANTIDADE, COD_CORRETORA, COD_CARTEIRA, COD_OPER, data_entrada FROM TODAS_OPERACOES) TOP on DC.COD_PRODUTO = TOP.COD_PRODUTO
  inner join (SELECT COD_CORRETORA, NOME FROM CORRETORAS) C ON C.COD_CORRETORA = TOP.COD_CORRETORA
  inner join VIEW_ESTRUTURA VE on VE.COD_CARTEIRA = TOP.COD_CARTEIRA
        
        
        inner join (

            select COD_PRODUTO, COD_CONTRAPARTE from FUTUROS_LEF
            union all
            select COD_PRODUTO, COD_CONTRAPARTE from OPCOES_LEF
            union all
            select COD_PRODUTO, COD_CONTRAPARTE from FUNDOS_LEF

        ) LEF on LEF.COD_PRODUTO = P.COD_PRODUTO
        
        inner join contrapartes con on con.cod_contraparte = lef.cod_contraparte

        left join COTACOES C on P.COD_PRODUTO = C.COD_PRODUTO

where

    "NOME_CARTEIRA" in ('BB ECO PLUS FI', 'BNP PARIBAS MATCH DI FI RF REFERENCIADO CREDITO PR');
