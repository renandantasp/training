create table DEPARTAMENTO_RDP (
	CODIGO number(9, 0),
	NOME varchar2(100) constraint NN_NOME_DEPARTAMENTO_RDP not null,
	SIGLA varchar2(5)
	constraint PK_DEPARTAMENTO_RDP primary key (CODIGO),
	constraint UK_NOME_DEPARTAMENTO_RDP unique (NOME)
);

create table PESSOA_RDP (
	CODIGO number(9, 0),
	NOME varchar2(100) constraint NN_NOME_PESSOA not null,
	SEXO varchar2(1) constraint CK_SEXO_PESSOA check (SEXO in ('M' , 'F')),
	DATA_NASCIMENTO date,
	constraint PK_PESSOA_RDP primary key (CODIGO),
	constraint UK_NOME_PESSOA_RDP unique (NOME)
);

create table EMPREGADO_RDP (
	CODIGO_PESSOA number(9, 0),
	COD_DEPARTAMENTO number (9, 0) default 1 constraint NN_CODDEP_EMPREGADO_RDP not null,
	DATA_ADMISSAO date constraint NN_DATAADM_EMPREGADO_RDP not null,
	constraint PK_EMPREGADO_RDP primary key (CODIGO_PESSOA),
	constraint FK_CODPESSOA_EMPREGADO_RDP foreign key (CODIGO_PESSOA) references PESSOA_RDP (CODIGO) on delete cascade,
	constraint FK_CODDEP_EMPREGADO_RDP foreign key (COD_DEPARTAMENTO) references DEPARTAMENTO_RDP (CODIGO)
);

alter table EMPREGADO_RDP add (
	SALARIO number(25, 10) default 2000 constraint NN_SALARIO_EMPREGADO_RDP not null
);

create index IX_PESSOA_NOME on PESSOA_RDP (NOME);

--------------------------------------------------------------------------------------------------------------------------

insert into DEPARTAMENTO_RDP values(1, 'Departamento Pessoal', 'DP');
insert into DEPARTAMENTO_RDP values(2, 'Pesquisa e Desenvolvimento', 'P' || '&' || 'D');
insert into DEPARTAMENTO_RDP values(3, 'Recursos Humanos', 'RH');

insert into PESSOA_RDP values(1, 'Abner Gomes',       '13/12/1991', 'M');
insert into PESSOA_RDP values(2, 'Patricio Frota',    '01/02/1988', 'M');
insert into PESSOA_RDP values(3, 'Bruno Rodrigues',   '01/01/1988', 'M');
insert into PESSOA_RDP values(4, 'Leticia Maluf',     '01/01/1990', 'F');
insert into PESSOA_RDP values(5, 'Danielle Teixeira', '01/01/1989', 'F');
insert into PESSOA_RDP values(6, 'Amanda Guereschi',  '01/01/1994', 'F');

insert into EMPREGADO_RDP values(1, 2, '01/08/2016', 1000);
insert into EMPREGADO_RDP values(2, 2, '01/01/2010', 7000);
insert into EMPREGADO_RDP values(3, 2, '01/02/2011', 20000.01);
insert into EMPREGADO_RDP values(4, 1, '01/03/2017', 3000.90);
insert into EMPREGADO_RDP values(5, 3, '01/04/2016', 13452.10);
insert into EMPREGADO_RDP values(6, 3, '01/05/2018', 1928.50);

--------------------------------------------------------------------------------------------------------------------------

select * from DEPARTAMENTO_RDP where SIGLA like '%D%';
select NOME, SIGLA from DEPARTAMENTO_RDP where SIGLA like '%D%';

merge into EMPREGADO_RDP E
	using PESSOA_RDP P on (E.CODIGO_PESSOA = P.CODIGO)
	when matched then
		update EMPREGADO_RDP set SALARIO = E.SALARIO * 1.1 where SALARIO < 6000
	when not matched then
		insert into EMPREGADO_RDP values(P.CODIGO, 2, SYSDATE, 2000);


