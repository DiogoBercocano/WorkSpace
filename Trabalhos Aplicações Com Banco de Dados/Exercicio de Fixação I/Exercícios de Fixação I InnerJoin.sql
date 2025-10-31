-- exercicio 1.1
SELECT 
    p.ProdutosID,
    p.Nome AS Produto,
    c.Nome AS Categoria,
    f.Nome AS Fornecedor,
    p.Preco
FROM Produtos p
INNER JOIN Categorias c ON p.CategoriasID = c.CategoriasID
INNER JOIN Fornecedores f ON p.FornecedoresID = f.FornecedoresID
WHERE p.Ativo = 1;

-- exercicio 1.2
SELECT 
    p.Nome AS Produto,
    a.Nome AS Armazem,
    e.Quantidade
FROM Estoque e
INNER JOIN Produtos p ON e.ProdutosID = p.ProdutosID
INNER JOIN Armazens a ON e.ArmazensID = a.ArmazensID
WHERE a.Nome = 'Matriz'
ORDER BY e.Quantidade DESC;


-- exercicio 1.3
SELECT 
    m.MovimentosID,
    p.Nome AS Produto,
    a.Nome AS Armazem,
    m.Tipo,
    m.Quantidade,
    m.DataMovimento
FROM Movimentos m
INNER JOIN Produtos p ON m.ProdutosID = p.ProdutosID
INNER JOIN Armazens a ON m.ArmazensID = a.ArmazensID
WHERE m.Tipo = 'SAIDA';

-- exercicio 2.1
SELECT 
    p.Nome AS Produto,
    e.Quantidade AS QuantidadeFilialSul
FROM Produtos p
LEFT JOIN Estoque e ON p.ProdutosID = e.ProdutosID
LEFT JOIN Armazens a ON e.ArmazensID = a.ArmazensID AND a.Nome = 'Filial Sul';

-- exercicio 2.2
SELECT 
    p.Nome AS Produto,
    MAX(m.DataMovimento) AS UltimaData
FROM Produtos p
LEFT JOIN Movimentos m ON p.ProdutosID = m.ProdutosID
GROUP BY p.Nome
ORDER BY UltimaData DESC;

-- exercicio 2.3
SELECT 
    c.Nome AS Categoria,
    COUNT(p.ProdutosID) AS TotalProdutos
FROM Categorias c
LEFT JOIN Produtos p ON p.CategoriasID = c.CategoriasID
GROUP BY c.Nome;

-- exercicio 3.1
SELECT 
    c.Nome AS Categoria,
    SUM(e.Quantidade) AS QuantidadeTotal
FROM Estoque e
INNER JOIN Produtos p ON e.ProdutosID = p.ProdutosID
INNER JOIN Categorias c ON p.CategoriasID = c.CategoriasID
GROUP BY c.Nome;

-- exercicio 3.2
SELECT 
    a.Nome AS Armazem,
    SUM(e.Quantidade * p.Preco) AS ValorTotal
FROM Estoque e
INNER JOIN Produtos p ON e.ProdutosID = p.ProdutosID
INNER JOIN Armazens a ON e.ArmazensID = a.ArmazensID
GROUP BY a.Nome;

-- exercicio 4.1
SELECT 
    p.Nome,
    p.Preco
FROM Produtos p
WHERE p.Ativo = 1
ORDER BY p.Preco DESC, p.Nome ASC;

-- exercicio 4.2
SELECT 
    p.Nome AS Produto,
    e.Quantidade
FROM Estoque e
INNER JOIN Produtos p ON e.ProdutosID = p.ProdutosID
INNER JOIN Armazens a ON e.ArmazensID = a.ArmazensID
WHERE a.Nome = 'Filial Sul'
ORDER BY e.Quantidade ASC;

-- exercicio 4.3
SELECT TOP 5
    m.MovimentosID,
    p.Nome AS Produto,
    m.Tipo,
    m.Quantidade,
    m.DataMovimento
FROM Movimentos m
INNER JOIN Produtos p ON m.ProdutosID = p.ProdutosID
ORDER BY m.DataMovimento DESC;

-- exercicio 5.1
SELECT 
    ProdutosID,
    Nome,
    Preco
FROM Produtos
WHERE Preco > (SELECT AVG(Preco) FROM Produtos);

-- exercicio 5.2
SELECT 
    ProdutosID,
    Nome,
    Preco
FROM Produtos
WHERE Preco > (
    SELECT Preco FROM Produtos WHERE Nome = 'Café 250g'
);

-- exercicio 5.3
SELECT DISTINCT f.Nome AS Fornecedor
FROM Fornecedores f
INNER JOIN Produtos p ON f.FornecedoresID = p.FornecedoresID
INNER JOIN Categorias c ON p.CategoriasID = c.CategoriasID
WHERE c.Nome = 'Bebidas';

