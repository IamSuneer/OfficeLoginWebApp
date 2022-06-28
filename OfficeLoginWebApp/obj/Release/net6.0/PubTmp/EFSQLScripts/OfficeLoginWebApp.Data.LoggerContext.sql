IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324040417_InitialCreate')
BEGIN
    CREATE TABLE [officeLoggers] (
        [LoggerId] int NOT NULL IDENTITY,
        [LoggerEmail] nvarchar(max) NOT NULL,
        [LogginTime] datetime2 NOT NULL,
        [IPAddress] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_officeLoggers] PRIMARY KEY ([LoggerId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324040417_InitialCreate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220324040417_InitialCreate', N'7.0.0-preview.2.22153.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324052539_AddLocalIP')
BEGIN
    ALTER TABLE [officeLoggers] ADD [IPAddressLocal] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324052539_AddLocalIP')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220324052539_AddLocalIP', N'7.0.0-preview.2.22153.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324073309_SecondTable')
BEGIN
    ALTER TABLE [officeLoggers] DROP CONSTRAINT [PK_officeLoggers];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324073309_SecondTable')
BEGIN
    EXEC sp_rename N'[officeLoggers]', N'OfficeLogger';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324073309_SecondTable')
BEGIN
    ALTER TABLE [OfficeLogger] ADD CONSTRAINT [PK_OfficeLogger] PRIMARY KEY ([LoggerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324073309_SecondTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220324073309_SecondTable', N'7.0.0-preview.2.22153.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324090032_SecondTableUpdate')
BEGIN
    ALTER TABLE [OfficeLogger] DROP CONSTRAINT [PK_OfficeLogger];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324090032_SecondTableUpdate')
BEGIN
    EXEC sp_rename N'[OfficeLogger]', N'officeLoggers';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324090032_SecondTableUpdate')
BEGIN
    ALTER TABLE [officeLoggers] ADD CONSTRAINT [PK_officeLoggers] PRIMARY KEY ([LoggerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324090032_SecondTableUpdate')
BEGIN
    CREATE TABLE [userSigninLogs] (
        [UserId] int NOT NULL IDENTITY,
        [LoggerEmail] nvarchar(max) NOT NULL,
        [LogginTime] datetime2 NOT NULL,
        [IPAddress] nvarchar(max) NOT NULL,
        [IPAddressLocal] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_userSigninLogs] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220324090032_SecondTableUpdate')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220324090032_SecondTableUpdate', N'7.0.0-preview.2.22153.1');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419100606_UpdatedTable')
BEGIN
    EXEC sp_rename N'[userSigninLogs].[LogginTime]', N'LoginTime', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419100606_UpdatedTable')
BEGIN
    EXEC sp_rename N'[officeLoggers].[LogginTime]', N'LoginTime', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419100606_UpdatedTable')
BEGIN
    ALTER TABLE [userSigninLogs] ADD [LogoutTime] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419100606_UpdatedTable')
BEGIN
    ALTER TABLE [officeLoggers] ADD [LogoutTime] datetime2 NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220419100606_UpdatedTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220419100606_UpdatedTable', N'7.0.0-preview.2.22153.1');
END;
GO

COMMIT;
GO

