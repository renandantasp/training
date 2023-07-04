----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 1

select 
    P.nome,
    trunc(months_between(sysdate, data_nascimento)/12) as Idade,
    D.nome as Departamento,
    E.data_admissao
from 
    empregado_rdp E
    inner join pessoa_rdp P
    on E.codigo_pessoa = P.codigo
    inner join departamento_rdp D
    on E.cod_departamento = D.codigo;


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

select 
    P.nome,
    trunc(months_between(sysdate, data_nascimento)/12) as Idade,
    D.nome as Departamento,
    E.data_admissao
from 
    empregado_rdp E
    inner join pessoa_rdp P on E.codigo_pessoa = P.codigo
    inner join departamento_rdp D on E.cod_departamento = D.codigo
where 
    data_nascimento < '10/10/1991';

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 3

select 
    avg(trunc(months_between(sysdate, data_nascimento)/12)) as media_idade
from 
    empregado_rdp E
    inner join pessoa_rdp P on E.codigo_pessoa = P.codigo;

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 4

select 
  P.nome,
  D.nome as Departamento,
  E.data_admissao
from 
  empregado_rdp E
  inner join pessoa_rdp P on E.codigo_pessoa = P.codigo
  inner join departamento_rdp D on E.cod_departamento = D.codigo
where data_admissao in ( 
  select max(data_admissao)
  from empregado_rdp
  group by cod_departamento
);



----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 5


select 
  nome,
  data_nascimento
from 
  pessoa_rdp
where data_nascimento = ( 
  select 
    max(data_nascimento)
  from 
    empregado_rdp E
    inner join pessoa_rdp P on E.codigo_pessoa = P.codigo
);