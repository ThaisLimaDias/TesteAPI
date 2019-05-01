USE [TESTE]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Clientes](
	[IdCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](150) NOT NULL,
	[CPF] [varchar](50) NOT NULL,
	[Sexo] [varchar](10) NOT NULL,
	[IdTipoCliente] [bigint] NOT NULL,
	[IdSituacaoCliente] [bigint] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SituacaoCliente]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SituacaoCliente](
	[IdSituacaoCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[DescSituacaoCliente] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SituacaoCliente] PRIMARY KEY CLUSTERED 
(
	[IdSituacaoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SP_TB_ERROR]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SP_TB_ERROR](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Mensagem] [varchar](1000) NOT NULL,
	[ProcedureName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_SP_TB_ERROR] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TipoCliente]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoCliente](
	[IdTipoCliente] [bigint] IDENTITY(1,1) NOT NULL,
	[DescTipoCliente] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TipoCliente] PRIMARY KEY CLUSTERED 
(
	[IdTipoCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Clientes] ON 

INSERT [dbo].[Clientes] ([IdCliente], [Nome], [CPF], [Sexo], [IdTipoCliente], [IdSituacaoCliente]) VALUES (4, N'THAIS LIMA DIAS', N'379.069.998-51', N'FEMININO', 1, 1)
SET IDENTITY_INSERT [dbo].[Clientes] OFF
SET IDENTITY_INSERT [dbo].[SituacaoCliente] ON 

INSERT [dbo].[SituacaoCliente] ([IdSituacaoCliente], [DescSituacaoCliente]) VALUES (1, N'Ativo')
SET IDENTITY_INSERT [dbo].[SituacaoCliente] OFF

SET IDENTITY_INSERT [dbo].[TipoCliente] ON 

INSERT [dbo].[TipoCliente] ([IdTipoCliente], [DescTipoCliente]) VALUES (1, N'Pessoa Física')
INSERT [dbo].[TipoCliente] ([IdTipoCliente], [DescTipoCliente]) VALUES (2, N'Pessoa Juridica')
SET IDENTITY_INSERT [dbo].[TipoCliente] OFF
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Situacao_Cliente] FOREIGN KEY([IdSituacaoCliente])
REFERENCES [dbo].[SituacaoCliente] ([IdSituacaoCliente])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Situacao_Cliente]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Tipo_Cliente] FOREIGN KEY([IdTipoCliente])
REFERENCES [dbo].[TipoCliente] ([IdTipoCliente])
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Tipo_Cliente]
GO
/****** Object:  StoredProcedure [dbo].[SP_TB_CLIENTES_CREATE]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_TB_CLIENTES_CREATE]
	@NOME AS NVARCHAR(150)
	,@CPF AS VARCHAR(50)
	,@SEXO AS VARCHAR(10)
	,@TIPOCLIENTE AS BIGINT
	,@SITCLIENTE AS BIGINT
	,@OUTSTATUS AS BIT output

	AS
BEGIN
	DECLARE @ERRO AS VARCHAR(1000)

	BEGIN TRY
		INSERT INTO Clientes (Nome,CPF,Sexo,IdTipoCliente,IdSituacaoCliente) VALUES (@NOME,@CPF,@SEXO,@TIPOCLIENTE,@SITCLIENTE)
		
		SET @OUTSTATUS = 1

	END TRY
	BEGIN CATCH
		SET @OUTSTATUS = 0

		SET @ERRO = ERROR_PROCEDURE() + ': Line(' + CONVERT(varchar,ERROR_LINE()) + ') # ' + ERROR_MESSAGE()
		INSERT INTO SP_TB_ERROR ([Date],[Mensagem],[ProcedureName]) VALUES (GETDATE(),@ERRO,'[SP_TB_CLIENTES_CREATE]')
				
	END CATCH


END
GO
/****** Object:  StoredProcedure [dbo].[SP_TB_CLIENTES_DELETE]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_TB_CLIENTES_DELETE]
	@ID AS BIGINT	
	,@OUTSTATUS AS BIT output

	AS
BEGIN
	DECLARE @ERRO AS VARCHAR(1000)

	BEGIN TRY
		DELETE FROM Clientes WHERE IdCliente=@ID
		
		SET @OUTSTATUS = 1

	END TRY
	BEGIN CATCH
		SET @OUTSTATUS = 0

		SET @ERRO = ERROR_PROCEDURE() + ': Line(' + CONVERT(varchar,ERROR_LINE()) + ') # ' + ERROR_MESSAGE()
		INSERT INTO SP_TB_ERROR ([Date],[Mensagem],[ProcedureName]) VALUES (GETDATE(),@ERRO,'[SP_TB_CLIENTES_DELETE]')
				
	END CATCH


END
GO
/****** Object:  StoredProcedure [dbo].[SP_TB_CLIENTES_READ]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_TB_CLIENTES_READ]
		@ID AS VARCHAR(100)
	AS
BEGIN
	DECLARE @sSQL AS VARCHAR(300)
	SET @sSQL='SELECT IdCliente,Nome,CPF,Sexo,IdTipoCliente,IdSituacaoCliente FROM Clientes WHERE 1=1'
	IF @ID <>0 BEGIN
		SET @sSQL= @sSQL + ' AND IdCliente=' + @ID
	END
	EXEC (@sSQL)
END
GO
/****** Object:  StoredProcedure [dbo].[SP_TB_CLIENTES_UPDATE]    Script Date: 30/04/2019 17:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_TB_CLIENTES_UPDATE]
	@ID AS BIGINT
	,@NOME AS NVARCHAR(150)
	,@CPF AS VARCHAR(50)
	,@SEXO AS VARCHAR(10)
	,@TIPOCLIENTE AS BIGINT
	,@SITCLIENTE AS BIGINT
	,@OUTSTATUS AS BIT output

	AS
BEGIN
	DECLARE @ERRO AS VARCHAR(1000)

	BEGIN TRY
		UPDATE Clientes 
			SET Nome= @NOME,CPF=@CPF,Sexo=@SEXO,IdTipoCliente=@TIPOCLIENTE,IdSituacaoCliente=@SITCLIENTE 
			WHERE IdCliente=@ID
		
		SET @OUTSTATUS = 1

	END TRY
	BEGIN CATCH
		SET @OUTSTATUS = 0

		SET @ERRO = ERROR_PROCEDURE() + ': Line(' + CONVERT(varchar,ERROR_LINE()) + ') # ' + ERROR_MESSAGE()
		INSERT INTO SP_TB_ERROR ([Date],[Mensagem],[ProcedureName]) VALUES (GETDATE(),@ERRO,'[SP_TB_CLIENTES_UPDATE]')
				
	END CATCH


END
GO
