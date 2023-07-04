----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 1

SELECT
  UI.INDEX_NAME,
  UIC.COLUMN_NAME,
  UI.TABLE_NAME
FROM
  USER_INDEXES UI
  INNER JOIN USER_IND_COLUMNS UIC ON UIC.INDEX_NAME = UI.INDEX_NAME;

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

SELECT
  NOME,
  SEXO
from
  PESSOA_RDP
where
 UPPER(NOME) like 'A%';


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 3

CREATE INDEX IX_PESSOA_RDP_NOME ON PESSOA_RDP (NOME) TABLESPACE G2K_PRIV_INDX COMPUTE STATISTICS
CREATE INDEX IX_PESSOA_RDP_SEXO ON PESSOA_RDP (SEXO) TABLESPACE G2K_PRIV_INDX COMPUTE STATISTICS

ALTER INDEX IX_PESSOA_RDP_NOME UNUSABLE;
ALTER INDEX IX_PESSOA_RDP_SEXO UNUSABLE;

ALTER INDEX IX_PESSOA_RDP_NOME REBUILD;
ALTER INDEX IX_PESSOA_RDP_SEXO REBUILD;


SELECT
  NOME,
  SEXO
from
  PESSOA_RDP
where
 UPPER(NOME) like 'A%';