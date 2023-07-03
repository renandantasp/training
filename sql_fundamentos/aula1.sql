----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 1

insert into pessoa_rdp values(7, 'Renan Dantas Pasquantonio', '20/10/1999', 'M');
insert into pessoa_rdp values(8, 'Lúcio Fontana Soares', '12/05/1987', 'M');
insert into pessoa_rdp values(9, 'Luíza Mantovani Teixeira', '25/03/2000', 'F');
insert into pessoa_rdp values(10, 'Sílvio Pires Ventura', '13/11/1995', 'M');
insert into pessoa_rdp values(11, 'Ágata Falcão Leite', '16/05/1991', 'F');


insert into empregado_rdp values(7,  2,'12/06/2023', 3000);
insert into empregado_rdp values(8,  2,'20/08/2022', 3500);
insert into empregado_rdp values(9,  3,'14/08/2020', 4500);
insert into empregado_rdp values(10, 1,'13/06/2020', 4500);
insert into empregado_rdp values(11, 1,'10/10/2019', 5000);


----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 2

update pessoa_rdp
  set DATA_NASCIMENTO = DATA_NASCIMENTO + 1;

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 3

delete from empregado_rdp 
    where codigo_pessoa = (
        select codigo_pessoa from (
            select * 
            from empregado_rdp
            where codigo_pessoa >=7
            order by salario
        )
    where rownum = 1);

----------------------------------------------------------------------------------------------------------------------------
-- Exercicio 4

delete from pessoa_rdp where codigo=9;
-- Codigo 9 tambem foi deletado na tabela de empregados

