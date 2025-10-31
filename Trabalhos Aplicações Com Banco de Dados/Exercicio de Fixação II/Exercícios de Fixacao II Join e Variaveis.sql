--1.1 filtro por teto de preço 
declare @Teto decimal(10,2) = 100000.00;

select 
	m.Nome as Marca,
	c.Modelo,
	c.Ano,
	PrecoTabela
from Carros "c"
join Marcas m on (m.MarcaID = c.MarcaID)
where c.PrecoTabela < = @Teto and c.Ativo = 1
order by PrecoTabela asc;


--1.2 Vendas do Mes atual
declare @Ano int = 2025;
declare @Mes int = 08;

select 
	v.VendaID,
	v.DataVenda,
	m.Nome as Marca,
	c.Modelo,
	v.PrecoTabela,
	v.DescontoPercentual,
	v.ValorVenda
from  Vendas v
join Carros c on (v.CarroID = c.CarroID)
join Marcas m on (c.MarcaID = m.MarcaID)
where YEAR (DataVenda) = @Ano and
month (DataVenda) = @Mes
order by m.Nome desc;


--2.1 Top 3 mais caros no estoque 
select top 3
	m.Nome,
	Modelo,
	Ano,
	PrecoTabela
from Carros c
join Marcas m on (c.MarcaID = m.MarcaID)
where c.Ativo = 1
order by c.PrecoTabela desc;


--3.1
with CarroComDesconto as (
	select
		v.VendaID, c.Modelo, m.Nome as Marca,
		v.PrecoTabela, v.DescontoPercentual, 
		v.PrecoTabela - v.ValorVenda as ValorDesconto
	from Vendas v
	join Carros c on (c.CarroID = v.CarroID)
	join Marcas m on (m.MarcaID = c.MarcaID)
)
select * from CarroComDesconto
where valorDesconto > 5000;

--4.1
select * from Carros c 
where CarroID not in (select CarroID from Vendas);


