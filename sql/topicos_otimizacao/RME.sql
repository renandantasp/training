select
  P.NOME as "Nome do Produto", -- vem da tabela produtos
  DC.CONTRATO as "Contrato",  -- o contrato é referente a um produto, conseguimos joinando a tabela contrato x produtos pelo cod_produto 
  VE.NOME_CARTEIRA as "Nome da Carteira", -- pela tabela TODAS_OPERACOES conseguimos correlacionar os produtos com outras informacoes como cod_produto, cod_carteira, assim relacionar a tabela produtos com a todas_operacoes e ela com a view_estrutura
  VE.NOME_TESOURARIA as "Nome da Tesouraria", -- mesma coisa que o nome_carteira
  VE.NOME_INSTITUICAO as "Nome da Instituição", -- mesma coisa que o nome_carteira
  DC.NOME_EMISSOR as "Nome do Emissor", -- juntando os dados do contrato pelo cod_produto conseguimor obter o nome_emissor
  CON.NOME as "Contraparte", --unindo as tabelas futuros_lef, opcoes_lef, fundos_lef e as juntando pelo cod_produto conseguir obter o nome da cod_contraparte que unindo com a CONTRAPARTES conseguimos o nome
  COR.NOME as "Corretora", -- mesma coisa que o cod_contraparte para conseguir o cod_corretora e assim juntando com a CORRETORA conseguimos obter o nome da corretora
  TOP.DATA_ENTRADA as "Data de Aquisição", -- dado presente na tabela TODAS_OPERACOES
  TOP.QUANTIDADE  as "Quantidade", -- dado presente na tabela TODAS_OPERACOES
  COTA.VALOR "Cotação da Data de Aquisição",
  COTE.VALOR "Cotação da Data de Cálculo",
  ROUND((TOP.PU * TOP.QUANTIDADE), 3) as "Posição de Aquisição", --Obs: o PU registrado na tabela TOP é da data_entrada (data de aquisição)
  ROUND((COTE.VALOR * TOP.QUANTIDADE), 3) as "Posição do Dia",
  ROUND((( COTA.VALOR / TOP.PU) * TOP.QUANTIDADE),3) AS "Resultado Início da Operação (%)",
  ROUND((( COTA.VALOR - TOP.PU) * TOP.QUANTIDADE),3) AS "Resultado Início da Operação (ABS)"

from 
  produtos p 
  inner join todas_operacoes top on top.cod_produto = p.cod_produto
  inner join (SELECT COD_CORRETORA, NOME FROM CORRETORAS WHERE COD_CORRETORA <> -508) cor on top.cod_corretora = cor.cod_corretora
  inner join (SELECT COD_CARTEIRA, NOME_CARTEIRA, NOME_TESOURARIA, NOME_INSTITUICAO FROM VIEW_ESTRUTURA where COD_INSTITUICAO <> 160813 and nome_carteira = 'ELETRAWEST703 | GOYA FI MULTIMERCADO') VE on VE.COD_CARTEIRA = TOP.COD_CARTEIRA
  inner join (SELECT CONTRATO, NOME_EMISSOR, COD_PRODUTO FROM DADOS_CONTRATO ) DC on P.COD_PRODUTO = DC.COD_PRODUTO
  inner join (
  
              select COD_PRODUTO, COD_CONTRAPARTE from FUTUROS_LEF
              union all
              select COD_PRODUTO, COD_CONTRAPARTE from OPCOES_LEF
              union all
              select COD_PRODUTO, COD_CONTRAPARTE from FUNDOS_LEF
  
             ) LEF on LEF.COD_PRODUTO = P.COD_PRODUTO
          
    inner join contrapartes con on con.cod_contraparte = lef.cod_contraparte
    inner join (select cod_produto, valor, data from cotacoes where data = (SELECT TO_CHAR(SYSDATE-1, 'dd/mm/yy') FROM DUAL)) COTE on (p.cod_produto = COTE.cod_produto) 
    inner join (select cod_produto, valor, data from cotacoes) COTA on (top.cod_produto = COTA.cod_produto and top.data_entrada = COTA.DATA) 
;