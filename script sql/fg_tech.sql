CREATE DATABASE fg_tech;
USE fg_tech;

CREATE TABLE Cliente (
    id_cliente INT PRIMARY KEY AUTO_INCREMENT,
    nome_cli VARCHAR(100) NOT NULL,
    email_cli VARCHAR(100) NOT NULL,
    telefone_cli VARCHAR(20),
    endereco_cli VARCHAR(200)
);

CREATE TABLE Funcionario (
    id_funcionario INT PRIMARY KEY AUTO_INCREMENT,
    nome_fun VARCHAR(100) NOT NULL,
    cargo_fun VARCHAR(50) NOT NULL,
    salario_fun DECIMAL(10,2) NOT NULL
);

CREATE TABLE Fornecedor (
    id_fornecedor INT PRIMARY KEY AUTO_INCREMENT,
    nome_for VARCHAR(100) NOT NULL,
    cnpj_for VARCHAR(18) NOT NULL,
    telefone_for VARCHAR(20)
);

CREATE TABLE CategoriaProduto (
    id_categoria INT PRIMARY KEY AUTO_INCREMENT,
    nome_cat VARCHAR(50) NOT NULL
);

CREATE TABLE Produto (
    id_produto INT PRIMARY KEY AUTO_INCREMENT,
    nome_pro VARCHAR(100) NOT NULL,
    preco_pro DECIMAL(10,2) NOT NULL,
    estoque_pro INT NOT NULL,
    id_fornecedor_fk INT NOT NULL,
    id_categoria_fk INT NOT NULL,
    FOREIGN KEY (id_fornecedor_fk) REFERENCES Fornecedor(id_fornecedor),
    FOREIGN KEY (id_categoria_fk) REFERENCES CategoriaProduto(id_categoria)
);

CREATE TABLE Venda (
    id_venda INT PRIMARY KEY AUTO_INCREMENT,
    data_ven DATE NOT NULL,
    horario_ven TIME NOT NULL,
    id_cliente_fk INT,
    id_funcionario_fk INT NOT NULL,
    FOREIGN KEY (id_cliente_fk) REFERENCES Cliente(id_cliente),
    FOREIGN KEY (id_funcionario_fk) REFERENCES Funcionario(id_funcionario)
);

CREATE TABLE Pagamento (
    id_pagamento INT PRIMARY KEY AUTO_INCREMENT,
    forma_pag VARCHAR(50) NOT NULL,
    valor_pag DECIMAL(10,2) NOT NULL,
    data_pag DATE NOT NULL,
    id_venda_fk INT NOT NULL,
    FOREIGN KEY (id_venda_fk) REFERENCES Venda(id_venda)
);

CREATE TABLE ItemVenda (
    id_item INT PRIMARY KEY AUTO_INCREMENT,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    id_venda_fk INT NOT NULL,
    id_produto_fk INT NOT NULL,
    FOREIGN KEY (id_venda_fk) REFERENCES Venda(id_venda),
    FOREIGN KEY (id_produto_fk) REFERENCES Produto(id_produto)
);

CREATE TABLE GarantiaProduto (
    id_gar_pro INT PRIMARY KEY AUTO_INCREMENT,
    data_inicio_gar DATE NOT NULL,
    validade_meses_gar INT NOT NULL,
    id_item_fk INT NOT NULL,
    FOREIGN KEY (id_item_fk) REFERENCES ItemVenda(id_item)
);

CREATE TABLE EstoqueMovimento (
    id_movimento INT PRIMARY KEY AUTO_INCREMENT,
    tipo_mov VARCHAR(20) NOT NULL,
    quantidade_mov INT NOT NULL,
    data_mov DATE NOT NULL,
    id_produto_fk INT NOT NULL,
    FOREIGN KEY (id_produto_fk) REFERENCES Produto(id_produto)
);

#INSERTS

#CLIENTE
INSERT INTO Cliente (nome_cli, email_cli, telefone_cli, endereco_cli) VALUES
('Ana Silva', 'ana@email.com', '99999-1111', 'Rua A, 123'),
('Bruno Costa', 'bruno@email.com', '98888-2222', 'Av. B, 456'),
('Carla Souza', 'carla@email.com', '97777-3333', 'Rua C, 789'),
('Daniel Lima', 'daniel@email.com', '96666-4444', 'Travessa D, 321'),
('Eduarda Ramos', 'eduarda@email.com', '95555-5555', 'Alameda E, 654'),
('Fernando Alves', 'fernando@email.com', '94444-6666', 'Rua F, 147'),
('Gabriela Dias', 'gabriela@email.com', '93333-7777', 'Av. G, 258'),
('Henrique Moura', 'henrique@email.com', '92222-8888', 'Rua H, 369'),
('Isabela Pinto', 'isabela@email.com', '91111-9999', 'Rua I, 741'),
('João Ferreira', 'joao@email.com', '90000-0000', 'Rua J, 852');

select * from Cliente;

#  FUNCIONÁRIOS
INSERT INTO Funcionario (nome_fun, cargo_fun, salario_fun) VALUES
('Carlos Mendes', 'Vendedor', 2500.00),
('Luciana Alves', 'Gerente', 5000.00),
('Paulo Henrique', 'Técnico', 3000.00),
('Mariana Souza', 'Caixa', 2000.00),
('Rafael Lima', 'Estoquista', 2200.00),
('Juliana Rocha', 'Supervisora', 4000.00),
('André Moura', 'Vendedor', 2600.00),
('Bianca Ferreira', 'Técnica', 3100.00),
('Thiago Duarte', 'Assistente', 1900.00),
('Fernanda Ribeiro', 'Financeiro', 3500.00);

select * from Funcionario;

#FORNECEDOR
INSERT INTO Fornecedor (nome_for, cnpj_for, telefone_for) VALUES
('KaBuM! S.A.', '05.570.714/0001-59', '19 2113-8250'),
('Lojas Americanas S.A.', '33.014.556/0001-96', '21 2206-6708'),
('Magazine Luiza S.A.', '47.960.950/0001-21', '11 3508-9400'),
('Ponto (ex-Ponto Frio)', '61.366.460/0001-87', '11 4003-8388'),
('Fast Shop S.A.', '60.060.533/0001-81', '11 4002-2273'),
('Submarino (B2W Digital)', '00.776.574/0001-56', '21 2206-6000'),
('Dell Computadores do Brasil Ltda.', '03.435.371/0001-80', '0800 970 3355'),
('Lenovo Tecnologia Brasil Ltda.', '05.681.599/0001-48', '11 3140-0500'),
('Multilaser Industrial S.A.', '59.717.553/0001-38', '11 3198-5898'),
('Positivo Tecnologia S.A.', '81.243.735/0001-48', '41 3235-6100');

select * from Fornecedor;

#CATEGORIAPRODUTO
INSERT INTO CategoriaProduto (nome_cat) VALUES
('Notebooks'), 
('Monitores'), 
('Placas de vídeo'),
('Memórias RAM'), 
('HDs'), 
('SSDs'), 
('Fontes'),
('Processadores'), 
('Periféricos'), 
('Gabinetes');

select * from CategoriaProduto;

#PRODUTO
INSERT INTO Produto (nome_pro, preco_pro, estoque_pro, id_fornecedor_fk, id_categoria_fk) VALUES
('Notebook Dell i5', 3500.00, 20, 1, 1),
('Monitor LG 24"', 800.00, 15, 2, 2),
('GTX 1660 Super', 1800.00, 10, 3, 3),
('Memória Kingston 8GB', 250.00, 50, 4, 4),
('HD 1TB Seagate', 300.00, 30, 5, 5),
('SSD 512GB NVMe', 400.00, 40, 6, 6),
('Fonte Corsair 550W', 500.00, 25, 7, 7),
('Ryzen 5 5600G', 950.00, 12, 8, 8),
('Mouse Gamer Logitech', 150.00, 35, 9, 9),
('Gabinete Gamer RGB', 350.00, 20, 10, 10);

select * from Produto;

#VENDA
INSERT INTO Venda (data_ven, horario_ven, id_cliente_fk, id_funcionario_fk) VALUES
('2025-07-01', '10:30:00', 1, 1),
('2025-07-02', '11:00:00', 2, 2),
('2025-07-03', '12:15:00', 3, 3),
('2025-07-04', '14:00:00', 4, 4),
('2025-07-05', '15:20:00', 5, 5),
('2025-07-06', '09:40:00', 6, 6),
('2025-07-07', '13:35:00', 7, 7),
('2025-07-08', '16:00:00', 8, 8),
('2025-07-09', '17:45:00', 9, 9),
('2025-07-10', '08:10:00', 10, 10);

select * from Venda;

#PAGAMENTO
INSERT INTO Pagamento (forma_pag, valor_pag, data_pag, id_venda_fk) VALUES
('Cartão de Crédito', 3500.00, '2025-07-01', 1),
('PIX', 800.00, '2025-07-02', 2),
('Boleto', 1800.00, '2025-07-03', 3),
('Cartão de Débito', 250.00, '2025-07-04', 4),
('Dinheiro', 300.00, '2025-07-05', 5),
('PIX', 400.00, '2025-07-06', 6),
('Cartão de Crédito', 500.00, '2025-07-07', 7),
('Cartão de Crédito', 950.00, '2025-07-08', 8),
('PIX', 150.00, '2025-07-09', 9),
('Dinheiro', 350.00, '2025-07-10', 10);

select * from Pagamento;

#ITEMVENDA
INSERT INTO ItemVenda (id_venda_fk, id_produto_fk, quantidade, preco_unitario) VALUES
(1, 1, 1, 3500.00),
(2, 2, 1, 800.00),
(3, 3, 1, 1800.00),
(4, 4, 1, 250.00),
(5, 5, 1, 300.00),
(6, 6, 1, 400.00),
(7, 7, 1, 500.00),
(8, 8, 1, 950.00),
(9, 9, 1, 150.00),
(10, 10, 1, 350.00);

select * from ItemVenda;

#GARANTIAPRODUTO
INSERT INTO GarantiaProduto (id_item_fk,  data_inicio_gar, validade_meses_gar) VALUES
(1, '2025-07-01', 12), 
(2, '2025-07-02', 24), 
(3, '2025-07-03', 12),
(4, '2025-07-04', 6), 
(5, '2025-07-05', 6), 
(6, '2025-07-06', 12),
(7, '2025-07-07', 12), 
(8, '2025-07-08', 24), 
(9, '2025-07-09', 12), 
(10, '2025-07-10', 6);

select * from GarantiaProduto;

#ESTOQUEMOVIMENTO
INSERT INTO EstoqueMovimento (tipo_mov, quantidade_mov, data_mov, id_produto_fk) VALUES
('Entrada', 20, '2025-06-01', 1), 
('Entrada', 15, '2025-06-01', 2),
('Entrada', 10, '2025-06-01', 3), 
('Entrada', 50, '2025-06-01', 4),
('Entrada', 30, '2025-06-01', 5), 
('Entrada', 40, '2025-06-01', 6),
('Entrada', 25, '2025-06-01', 7), 
('Entrada', 12, '2025-06-01', 8),
('Entrada', 35, '2025-06-01', 9), 
('Entrada', 20, '2025-06-01', 10);

select * from EstoqueMovimento;



#CONSULTAS COM JOINs
##INNER JOIN: Clientes e Vendas
SELECT c.nome_cli, v.data_ven, v.horario_ven
FROM Cliente c
INNER JOIN Venda v ON c.id_cliente = v.id_cliente_fk;

#LEFT JOIN: Produtos e Garantias
SELECT p.nome_pro, g.data_inicio_gar
FROM Produto p
LEFT JOIN ItemVenda iv ON p.id_produto = iv.id_produto_fk
LEFT JOIN GarantiaProduto g ON iv.id_item = g.id_item_fk;

#RIGHT JOIN: Vendas e Pagamentos
SELECT v.id_venda, p.forma_pag, p.valor_pag
FROM Venda v
RIGHT JOIN Pagamento p ON v.id_venda = p.id_venda_fk;


#CONSULTAS COM SUBCONSULTAS

#Clientes que compraram acima da média dos pagamentos
SELECT nome_cli FROM Cliente
WHERE id_cliente IN (
  SELECT v.id_cliente_fk FROM Venda v
  JOIN Pagamento p ON v.id_venda = p.id_venda_fk
  WHERE p.valor_pag > (SELECT AVG(valor_pag) FROM Pagamento)
);

#Produtos com menor estoque
SELECT nome_pro FROM Produto
WHERE estoque_pro = (
  SELECT MIN(estoque_pro) FROM Produto
);



#CONSULTAS COM GROUP BY / HAVING

#Total de vendas por funcionário
SELECT f.nome_fun, COUNT(v.id_venda) AS total_vendas
FROM Funcionario f
JOIN Venda v ON f.id_funcionario = v.id_funcionario_fk
GROUP BY f.nome_fun;

#Funcionários com mais de 1 venda
SELECT f.nome_fun, COUNT(v.id_venda) AS total_vendas
FROM Funcionario f
JOIN Venda v ON f.id_funcionario = v.id_funcionario_fk
GROUP BY f.nome_fun
HAVING COUNT(v.id_venda) > 1;
