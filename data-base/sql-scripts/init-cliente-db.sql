USE [master]
GO

IF DB_ID('DBCliente') IS NOT NULL
  set noexec on 

CREATE DATABASE [DBCliente];
GO

USE [DBCliente]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE LOGIN [migrator] WITH PASSWORD = 'migrator123!'
GO

CREATE SCHEMA app
GO

CREATE USER [migrator] FOR LOGIN [migrator] WITH DEFAULT_SCHEMA=[app]
GO

EXEC sp_addrolemember N'db_owner', N'migrator'
GO

CREATE LOGIN [AdmCliente] WITH PASSWORD = 'ClienteSenha@247'
GO

CREATE USER [AdmCliente] FOR LOGIN [AdmCliente] WITH DEFAULT_SCHEMA=[app]
GO

EXEC sp_addrolemember N'db_owner', N'AdmCliente'
GO