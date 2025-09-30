CREATE TABLE products 
(
	id INT PRIMARY KEY IDENTITY (1,1),
	prod_id VARCHAR(MAX) NULL,
	prod_name VARCHAR(MAX) NULL,
	prod_type VARCHAR(MAX) NULL,
	prod_stock int NULL,
	prod_price FLOAT NULL,
	prod_status VARCHAR(MAX) NULL,
	prod_image VARCHAR(MAX) NULL,
	date_insert DATE Null,
	date_update DATE Null,
	date_delete DATE Null,

);

SELECT * FROM products;

SELECT * FROM products;

CREATE TABLE orders
(
	id INT PRIMARY KEY IDENTITY (1,1),
	prod_id VARCHAR(MAX) NULL,
	prod_name VARCHAR(MAX) NULL,
	prod_type VARCHAR(MAX) NULL,
	prod_price FLOAT NULL,
	prod_date DATE NULL,
);