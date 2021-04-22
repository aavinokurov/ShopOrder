CREATE TABLE [dbo].[Customer]
(
	 [customer_name] NVARCHAR (50) NOT NULL  PRIMARY KEY
)

INSERT INTO [dbo].[Customer] ([customer_name]) VALUES (N'Дмитрий')
INSERT INTO [dbo].[Customer] ([customer_name]) VALUES (N'Иван')
INSERT INTO [dbo].[Customer] ([customer_name]) VALUES (N'Сергей')

CREATE TABLE [dbo].[Order] (
    [idOrder]    INT           NOT NULL PRIMARY KEY,
    [idCustomer] NVARCHAR (50) NOT NULL,
    [date]       DATE          NOT NULL,
    FOREIGN KEY (idCustomer) REFERENCES [Customer] (customer_name)
)

INSERT INTO [dbo].[Order] ([idOrder], [idCustomer], [date]) VALUES (1, N'Дмитрий', N'2021-04-16')
INSERT INTO [dbo].[Order] ([idOrder], [idCustomer], [date]) VALUES (2, N'Дмитрий', N'2021-04-17')
INSERT INTO [dbo].[Order] ([idOrder], [idCustomer], [date]) VALUES (3, N'Иван', N'2021-04-18')
INSERT INTO [dbo].[Order] ([idOrder], [idCustomer], [date]) VALUES (4, N'Сергей', N'2021-04-19')

CREATE TABLE [dbo].[Product] (
    [idProduct] INT           NOT NULL PRIMARY KEY,
    [name]      NVARCHAR (50) NOT NULL,
    [price]     FLOAT        NOT NULL,
)

INSERT INTO [dbo].[Product] ([idProduct], [name], [price]) VALUES (1, N'Мыло', 10)
INSERT INTO [dbo].[Product] ([idProduct], [name], [price]) VALUES (2, N'Шампунь', 100)
INSERT INTO [dbo].[Product] ([idProduct], [name], [price]) VALUES (3, N'Кофе', 50)
INSERT INTO [dbo].[Product] ([idProduct], [name], [price]) VALUES (4, N'Шоколад', 30)

CREATE TABLE [dbo].[OrderProduct] (
    [idOrder]   INT NOT NULL,
    [idProduct] INT NOT NULL,
    [quantity]  INT NOT NULL
	FOREIGN KEY (idOrder) REFERENCES [Order] (idOrder),
    FOREIGN KEY (idProduct) REFERENCES [Product] (idProduct),
)

INSERT INTO [dbo].[OrderProduct] ([idOrder], [idProduct], [quantity]) VALUES (1, 1, 5)
INSERT INTO [dbo].[OrderProduct] ([idOrder], [idProduct], [quantity]) VALUES (1, 2, 1)
INSERT INTO [dbo].[OrderProduct] ([idOrder], [idProduct], [quantity]) VALUES (2, 3, 2)
INSERT INTO [dbo].[OrderProduct] ([idOrder], [idProduct], [quantity]) VALUES (3, 4, 1)
INSERT INTO [dbo].[OrderProduct] ([idOrder], [idProduct], [quantity]) VALUES (4, 2, 2)