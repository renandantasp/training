----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 1

SELECT 
    * 
FROM
    ALL_CONSTRAINTS 
where 
    TABLE_NAME in ('PESSOA_RDP', 'DEPARTAMENTO_RDP');

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

SELECT 
    TEXT 
FROM 
    USER_SOURCE 
WHERE 
    NAME='NEW_EMPREGADO_RDP';

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 3

SELECT 
    * 
FROM ALL_OBJECTS;


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 4

select OSUSER, SID, SERIAL# from V$SESSION where USERNAME is not null and STATUS = 'ACTIVE';