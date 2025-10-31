--1

create function fn_totalitenspedido (@pedidoid int)
returns int
as
begin
    declare @totalitens int;

    select @totalitens = sum(quantidade)
    from itenspedido
    where pedidoid = @pedidoid;

    return isnull(@totalitens, 0);
end;

--2

create function fn_precofinal (@preco decimal(10,2), @descontopercent decimal(5,2))
returns decimal(10,2)
as
begin
    declare @precofinal decimal(10,2);

    set @precofinal = @preco - (@preco * (@descontopercent / 100));

    return @precofinal;
end;


--3

create function fn_produtosativos ()
returns table
as
return
(
    select produtoid, nome, preco
    from produtos
    where ativo = 1
);

--4 

create function fn_qtdpedidoscliente (@clienteid int)
returns int
as
begin
    declare @qtdpedidos int;

    select @qtdpedidos = count(*)
    from pedidos
    where clienteid = @clienteid;

    return isnull(@qtdpedidos, 0);
end;
