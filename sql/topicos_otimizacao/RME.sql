select DISTINCT C.COD_CORRETORA, C.NOME, C.NOME_VISUAL from TODAS_OPERACOES T INNER JOIN CORRETORAS C ON T.COD_CORRETORA = C.COD_CORRETORA WHERE C.COD_CORRETORA <> -508 ORDER BY C.COD_CORRETORA ASC; 
SELECT * FROM STOCKS_OPER;
SELECT SUM(PU) FROM FUTUROS_OPER;

SELECT * FROM VIEW_ESTRUTURA;

select
  P.NOME as "Nome do Produto", -- vem da tabela produtos
  DC.CONTRATO as "Nome do Contrato", -- o contrato é referente a um produto, conseguimos joinando a tabela contrato x produtos pelo cod_produto 
  VE.NOME_CARTEIRA as "Nome da Carteira", -- pela tabela TODAS_OPERACOES conseguimos correlacionar os produtos com outras informacoes como cod_produto, cod_carteira, assim relacionar a tabela produtos com a todas_operacoes e ela com a view_estrutura
  VE.NOME_TESOURARIA as "Nome da Tesouraria", -- mesma coisa que o nome_carteira
  VE.NOME_INSTITUICAO as "Nome da Instituição", -- mesma coisa que o nome_carteira
  DC.NOME_EMISSOR as "Nome do Emissor", -- juntando os dados do contrato pelo cod_produto conseguimor obter o nome_emissor
  CON.NOME as "Contraparte", --unindo as tabelas futuros_lef, opcoes_lef, fundos_lef e as juntando pelo cod_produto conseguir obter o nome da cod_contraparte que unindo com a CONTRAPARTES conseguimos o nome
  C.NOME as "Corretora", -- mesma coisa que o cod_contraparte para conseguir o cod_corretora e assim juntando com a CORRETORA conseguimos obter o nome da corretora
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

/*
[x] NOME DO PRODUTO -- vem da tabela produtos
[x] NOME DO CONTRATO -- o contrato é referente a um produto, conseguimos joinando a tabela contrato x produtos pelo cod_produto 
[x] NOME CARTEIRA -- pela tabela TODAS_OPERACOES conseguimos correlacionar os produtos com outras informacoes como cod_produto, cod_carteira, assim relacionar a tabela produtos com a todas_operacoes e ela com a view_estrutura
[x] NOME TESOURARIA -- mesma coisa que o nome_carteira
[x] NOME INSTITUIÇÃO -- mesma coisa que o nome_carteira
[x] NOME EMISSOR -- juntando os dados do contrato pelo cod_produto conseguimor obter o nome_emissor
[ ] NOME DEVEDOR
[x] NOME CONTRAPARTE --unindo as tabelas futuros_lef, opcoes_lef, fundos_lef e as juntando pelo cod_produto conseguir obter o nome da cod_contraparte que unindo com a CONTRAPARTES conseguimos o nome
[x] NOME CORRETORA -- mesma coisa que o cod_contraparte para conseguir o cod_corretora e assim juntando com a CORRETORA conseguimos obter o nome da corretora
[ ] DATA DE AQUISIÇÃO
[ ] QUANTIDADE
[ ] COTAÇÃO DA DATA DE AQUISIÇÃO DA OPERAÇÃO
[ ] COTAÇÃO DA DATA DE CÁLCULO
[ ] POSIÇÃO AQUISIÇÃO
[ ] POSIÇÃO DO DIA
[ ] RESULTADO INICIO DA OPERAÇÃO
[ ] DESPESAS CALCULADAS PELO MITRA
*/