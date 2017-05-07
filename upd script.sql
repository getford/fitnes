create database PRODUCT;
use PRODUCT;

create table pr 
(nomer int constraint pk_p primary key , 
product nvarchar(100),
kalorii nvarchar(100));

create table ex (
	nomer int constraint pk_e primary key, 
	exercises nvarchar(100),
	kalorii nvarchar(100)
);

create table NewUser
	(id_user int IDENTITY(1,1) constraint pk_id PRIMARY KEY,
	name nvarchar(50),
	-- product int CONSTRAINT fk_p foreign key references pr(nomer),
	exercises int constraint fk_e foreign key references ex(nomer),
	age int,
	sex varchar(1),
	weight int,
	height int,
	kalorii nvarchar(100),
	wyw nvarchar(20)
);

/*-----------------------------------------------------------*/
create table breakfast(
	id int IDENTITY(1,1)  PRIMARY KEY,
	id_user int CONSTRAINT fk_nu foreign key references NewUser(id_user),
	date nvarchar(20) not null,
	product nvarchar(25),
	kalorii nvarchar(10)
);

create table lunch(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_user int CONSTRAINT fk_nul foreign key references NewUser(id_user),
	date nvarchar(20) not null,
	product nvarchar(25),
	kalorii nvarchar(10)
);

create table dinner(
	id int IDENTITY(1,1) PRIMARY KEY,
	id_user int CONSTRAINT fk_nud foreign key references NewUser(id_user),
	date nvarchar(20) not null,
	product nvarchar(25),
	kalorii nvarchar(10)
);



use PRODUCT
select *from NewUser
Drop TABLE ex;
DROP database ex;
Drop TABLE pr;
DROP database pr;
drop table NewUser;
drop table breakfast;
drop table lunch;
drop table dinner;

select * from pr;
select * from ex;
select * from NewUser;

select * from breakfast;
select * from lunch;
select * from dinner;

delete from NewUser;

SELECT IDENT_CURRENT('NewUser');