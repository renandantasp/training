SELECT
  P.NOME AS "Nome do Produto", -- vem da tabela produtos
  DC.CONTRATO AS "Contrato",  -- o contrato é referente a um produto, conseguimos JOIN ando a tabela contrato x produtos pelo cod_produto 
  VE.NOME_CARTEIRA AS "Nome da Carteira", -- pela tabela TODAS_OPERACOES conseguimos correlacionar os produtos com outras informacoes como cod_produto, cod_carteira, assim relacionar a tabela produtos com a todas_operacoes e ela com a view_estrutura
  VE.NOME_TESOURARIA AS "Nome da Tesouraria", -- mesma coisa que o NOME_CARTEIRA
  VE.NOME_INSTITUICAO AS "Nome da Instituição", -- mesma coisa que o NOME_CARTEIRA
  DC.NOME_EMISSOR AS "Nome do Emissor", -- juntando os dados do contrato pelo cod_produto conseguimor obter o nome_emissor
  CON.NOME AS "Contraparte", --unindo as tabelas futuros_lef, opcoes_lef, fundos_lef e as juntando pelo cod_produto conseguir obter o nome da cod_contraparte que unindo com a CONTRAPARTES conseguimos o nome
  COR.NOME AS "Corretora", -- mesma coisa que o cod_contraparte para conseguir o COD_CORRETORA e assim juntando com a CORRETORA conseguimos obter o nome da corretora
  TOP.DATA_ENTRADA AS "Data de Aquisição", -- dado presente na tabela TODAS_OPERACOES
  TOP.QUANTIDADE  AS "Quantidade", -- dado presente na tabela TODAS_OPERACOES
  COTA.VALOR AS "Cotação da Data de Aquisição", -- Cotação de acordo com a Data de Aquisição, retirada da tabela COTACOES
  COTE.VALOR AS "Cotação da Data de Cálculo", -- Cotação de acordo com a Data de Cálculo, retirada da tabela COTACOES
  ROUND((TOP.PU * TOP.QUANTIDADE), 5) AS "Posição de Aquisição", -- Multiplicamos o PU calculado na data de aquisicao (data_entrada) pela quantidade
  ROUND((COTE.VALOR * TOP.QUANTIDADE), 5) AS "Posição do Dia", -- Utilizamos a cotação na data de cálculo no lugar do PU para obter a posição no dia do cálculo
  ROUND((( COTA.VALOR / TOP.PU) * TOP.QUANTIDADE), 5) AS "Resultado Início da Operação (%)", -- Cálculo do Resultado Início da Operação em porcentagem
  ROUND((( COTA.VALOR - TOP.PU) * TOP.QUANTIDADE), 5) AS "Resultado Início da Operação (ABS)"  -- Cálculo do Resultado Início da Operação em valores absolutos

FROM 
  PRODUTOS P 

  INNER JOIN TODAS_OPERACOES TOP ON TOP.COD_PRODUTO = P.COD_PRODUTO
  
  INNER JOIN ( 
    SELECT COD_CORRETORA, NOME 
    FROM CORRETORAS 
    WHERE COD_CORRETORA <> -508 ) COR
  ON TOP.COD_CORRETORA = COR.COD_CORRETORA
  
  INNER JOIN ( 
    SELECT COD_CARTEIRA, NOME_CARTEIRA, NOME_TESOURARIA, NOME_INSTITUICAO 
    FROM VIEW_ESTRUTURA 
    WHERE COD_INSTITUICAO <> 160813 AND NOME_CARTEIRA = 'ELETRAWEST703 | GOYA FI MULTIMERCADO') VE 
  ON VE.COD_CARTEIRA = TOP.COD_CARTEIRA
  
  INNER JOIN ( 
    SELECT CONTRATO, NOME_EMISSOR, COD_PRODUTO 
    FROM DADOS_CONTRATO ) DC 
  ON P.COD_PRODUTO = DC.COD_PRODUTO

  INNER JOIN (
  
    SELECT COD_PRODUTO, COD_CONTRAPARTE FROM FUTUROS_LEF
    UNION ALL
    SELECT COD_PRODUTO, COD_CONTRAPARTE FROM OPCOES_LEF
    UNION ALL
    SELECT COD_PRODUTO, COD_CONTRAPARTE FROM FUNDOS_LEF
  
  ) LEF ON LEF.COD_PRODUTO = P.COD_PRODUTO
          
  INNER JOIN contrapartes CON ON CON.COD_CONTRAPARTE = LEF.COD_CONTRAPARTE

  INNER JOIN (
    SELECT COD_PRODUTO, VALOR, DATA 
    FROM COTACOES WHERE DATA = (SELECT TO_CHAR(SYSDATE-5, 'dd/mm/yy') FROM DUAL)) COTE 
    ON (P.COD_PRODUTO = COTE.COD_PRODUTO) 
  
  INNER JOIN (
    SELECT COD_PRODUTO, VALOR, DATA 
    FROM COTACOES) COTA 
  ON (TOP.COD_PRODUTO = COTA.COD_PRODUTO AND TOP.DATA_ENTRADA = COTA.DATA);