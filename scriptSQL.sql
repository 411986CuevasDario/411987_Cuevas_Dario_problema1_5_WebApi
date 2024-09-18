create database problema1_5
go
USE [problema1_5]
GO
/****** Object:  Table [dbo].[Articulos]    Script Date: 4/9/2024 13:02:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Articulos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](30) NULL,
	[PrecioUnitar] [decimal](18, 0) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetallesFacturas]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetallesFacturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FacturaId] [int] NULL,
	[ArticuloId] [int] NULL,
	[Cantidad] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Facturas]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Facturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FormaPagoId] [int] NULL,
	[Fecha] [datetime] NULL,
	[Cliente] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FormasPagos]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormasPagos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetallesFacturas]  WITH CHECK ADD FOREIGN KEY([ArticuloId])
REFERENCES [dbo].[Articulos] ([Id])
GO
ALTER TABLE [dbo].[DetallesFacturas]  WITH CHECK ADD FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Facturas] ([Id])
GO
ALTER TABLE [dbo].[Facturas]  WITH CHECK ADD FOREIGN KEY([FormaPagoId])
REFERENCES [dbo].[FormasPagos] ([Id])
GO
/****** Object:  StoredProcedure [dbo].[crear_articulo]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[crear_articulo]
@nombre varchar(50),
@precio decimal
as
begin
insert into Articulos (Nombre,PrecioUnitar)
values (@nombre,@precio)
end
GO
/****** Object:  StoredProcedure [dbo].[crear_detalle]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[crear_detalle]
@articuloId int,
@facturaId int,
@cantidad int
as
begin
insert into DetallesFacturas (ArticuloId, FacturaId,Cantidad)
values (@articuloId,@facturaId,@cantidad)
end
GO
/****** Object:  StoredProcedure [dbo].[crear_factura]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[crear_factura]
@cliente varchar(100),
@formaPago int,
@fecha DateTime,
@id int output
as
begin
insert into Facturas (Cliente, FormaPagoId,Fecha)
values (@cliente,@formaPago,@fecha)
SET @id =Scope_Identity();
end
GO
/****** Object:  StoredProcedure [dbo].[crear_formapago]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[crear_formapago]
@forma varchar(30)
as
begin
insert into FormasPagos (Nombre)
values (@forma)
end

GO
/****** Object:  StoredProcedure [dbo].[obtener_articulos]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[obtener_articulos]
as
begin
select * from articulos
end
GO
/****** Object:  StoredProcedure [dbo].[obtener_factura_id]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[obtener_factura_id]
@id int
as
begin 
select f.id as 'facturaId', df.id as 'detalleid',f.fecha, f.Cliente, df.ArticuloId, df.cantidad, fp.nombre , f.formapagoid, a.nombre as 'articulonombre' from facturas f left join detallesfacturas df on df.facturaid = f.id 
join articulos a on a.id = df.articuloid
join formaspagos fp on fp.id = f.formapagoid
where f.id = @id
end
GO
/****** Object:  StoredProcedure [dbo].[obtener_facturas]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[obtener_facturas]
as 
begin 
select * from facturas
end
GO
/****** Object:  StoredProcedure [dbo].[obtener_formas_pagos]    Script Date: 4/9/2024 13:02:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[obtener_formas_pagos]
as 
begin
select * from formaspagos
end
GO
create procedure obtener_articulo_id
@id int
as
begin
select * from articulos where id = @id
end
go
create procedure modificar_articulo
@id int,
@nombre varchar(50),
@precio decimal
as
begin
update Articulos set Nombre = @nombre ,PrecioUnitar = @precio where id = @id;
end
go
create procedure eliminar_articulo
@id int
as
begin
delete from articulos where id=@id
end
go



