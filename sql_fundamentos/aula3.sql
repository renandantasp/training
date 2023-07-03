----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

create sequence SEQ_CODIGO_EMPREGADO_RDP start with 100 increment by 1;

alter table EMPREGADO_RDP add(
	CODIGO number(9, 0)
);


declare
  new_codigo number;
begin
  for I in 
  (
    select ROWID from EMPREGADO_RDP
  )
  loop
    select SEQ_CODIGO_EMPREGADO_RDP.NEXTVAL into new_codigo from DUAL;
    update EMPREGADO_RDP set CODIGO = new_codigo where ROWID = I.ROWID;
  end loop;  
end;


alter table EMPREGADO_RDP drop constraint PK_EMPREGADO_RDP;

alter table EMPREGADO_RDP add constraint PK_EMPREGADO_RDP primary key (CODIGO);


create or replace trigger NEW_EMPREGADO_RDP
  BEFORE INSERT ON EMPREGADO_RDP
  for each row
begin
    select SEQ_CODIGO_EMPREGADO_RDP.NEXTVAL into :NEW.CODIGO from DUAL;
end;

insert into empregado_rdp (codigo_pessoa, cod_departamento, data_admissao, salario) values(7,2,'12/06/2023',3000);

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

create or replace view VIEW_PESSOA_ANIVERSARIO (NOME, DATA_NASCIMENTO, FAZ_IDADE) as 
SELECT 
NOME,
DATA_NASCIMENTO,
TRUNC(MONTHS_BETWEEN(SYSDATE,DATA_NASCIMENTO))+1
FROM 
  PESSOA_RDP 
WHERE 
  extract(MONTH from DATA_NASCIMENTO) = extract(MONTH from SYSDATE) AND
  extract(YEAR from DATA_NASCIMENTO) <> extract(YEAR from SYSDATE);

create or replace view VIEW_ANIVERSARIO_ADMISSAO (NOME, DATA_ADMISSAO, FAZ_IDADE) AS
SELECT 
NOME,
DATA_ADMISSAO,
TRUNC(MONTHS_BETWEEN(SYSDATE,DATA_ADMISSAO))+1
FROM 
  PESSOA_RDP 
  INNER JOIN EMPREGADO_RDP ON CODIGO = CODIGO_PESSOA
WHERE 
  extract(MONTH from DATA_ADMISSAO) = extract(MONTH from sysdate) AND
  extract(YEAR from DATA_ADMISSAO) != extract(YEAR from sysdate); 

create or replace view EMPREGADO_ANIVERSARIO as
select 
  nome,
  data_nascimento,
  data_admissao
from 
  pessoa_rdp P
  inner join empregado_rdp E on P.codigo = E.codigo_pessoa
where
  (extract(MONTH from DATA_ADMISSAO) = extract(MONTH from sysdate) AND
  extract(YEAR from DATA_ADMISSAO) != extract(YEAR from sysdate)) 
  OR
  (extract(MONTH from DATA_NASCIMENTO) = extract(MONTH from SYSDATE) AND
  extract(YEAR from DATA_NASCIMENTO) != extract(YEAR from SYSDATE)) ;