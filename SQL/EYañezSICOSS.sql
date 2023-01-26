CREATE DATABASE EYañezSICOSS
USE EYañezSICOSS

CREATE TABLE Usuario
(
	IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
	UserName VARCHAR(50) UNIQUE,
	Password VARCHAR(50) UNIQUE
)

CREATE TABLE Calculadora
(
	IdCalculadora INT IDENTITY(1,1) PRIMARY KEY,
	Numero VARCHAR(MAX),
	Resultado INT,
	FechaHora DATETIME,
	IdUsuario INT FOREIGN KEY REFERENCES Usuario(IdUsuario)
)
GO

CREATE PROCEDURE UsuarioLogin 
@UserName VARCHAR(50)
AS
	SELECT Usuario.[IdUsuario]
		  ,Usuario.[UserName]
		  ,Usuario.[Password]		  
	  FROM [Usuario]
	WHERE UserName = @UserName 
GO

CREATE PROCEDURE UsuarioAdd
@UserName VARCHAR(50),
@Password VARCHAR(50)
AS
	INSERT INTO [dbo].[Usuario]
           ([UserName]
           ,[Password])
     VALUES
           (@UserName
           ,@Password)
GO

ALTER PROCEDURE CalculadoraAdd
@IdUsuario INT,
@Numero VARCHAR(MAX),
@Resultado INT
AS
	INSERT INTO [dbo].[Calculadora]
           ([Numero]
           ,[Resultado]
           ,[FechaHora]
           ,[IdUsuario])
     VALUES
           (@Numero
           ,@Resultado
           ,GETDATE()
           ,@IdUsuario)
GO

ALTER PROCEDURE CalculadoraGetAll 
@IdUsuario INT
AS
	SELECT [IdCalculadora]
      ,[Numero]
      ,[Resultado]
      ,[FechaHora]
	  ,Usuario.[IdUsuario]
	  ,0 AS 'Existe'
  FROM [dbo].[Calculadora]
  INNER JOIN Usuario ON Usuario.IdUsuario = Calculadora.IdUsuario
  WHERE Calculadora.IdUsuario = @IdUsuario
GO

CREATE PROCEDURE CalculadoraDelete
@IdUsuario INT,
@IdCalculadora INT
AS
	DELETE FROM [dbo].[Calculadora]
      WHERE IdUsuario = @IdUsuario AND IdCalculadora = @IdCalculadora
GO

ALTER PROCEDURE ActualizarFecha 
@Numero VARCHAR(MAX)
AS
	SELECT IdCalculadora, Numero, Resultado, FechaHora, IdUsuario,0 AS 'Existe'
	FROM Calculadora
	WHERE Numero = @Numero
	UPDATE Calculadora
	SET FechaHora=GETDATE()
	WHERE Numero = @Numero
GO

ALTER PROCEDURE VerificarNumero
@Numero VARCHAR(MAX)
AS
	SELECT COUNT(*) AS 'Existe',
	 0 AS IdCalculadora,
	 '' AS Numero,
	 0 AS Resultado,
	 NULL AS FechaHora,
	 0 AS IdUsuario
	FROM Calculadora
	WHERE Numero = @Numero
GO
