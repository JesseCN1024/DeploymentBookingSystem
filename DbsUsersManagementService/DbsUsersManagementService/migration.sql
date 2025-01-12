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

CREATE TABLE [Roles] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [DisplayName] nvarchar(max) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Teams] (
    [Id] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_Teams] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRoles] (
    [Id] uniqueidentifier NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [CreatedBy] uniqueidentifier NOT NULL,
    [UpdatedBy] uniqueidentifier NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [UpdatedDate] datetime2 NOT NULL,
    [IsDeleted] bit NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'CreatedDate', N'DisplayName', N'IsDeleted', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] ON;
INSERT INTO [Roles] ([Id], [CreatedBy], [CreatedDate], [DisplayName], [IsDeleted], [Name], [UpdatedBy], [UpdatedDate])
VALUES ('1fbd0afe-4e8c-4b9a-8631-735107d30cb2', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845213+07:00', N'Power User', CAST(0 AS bit), N'POWER_USER', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845214+07:00'),
('37d00899-66a7-4ed2-b8da-6ee0f8395201', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845216+07:00', N'General User', CAST(0 AS bit), N'GENERAL_USER', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845217+07:00'),
('e4b257ef-3638-4156-ad81-c98692b06229', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845209+07:00', N'Admin', CAST(0 AS bit), N'ADMIN', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845209+07:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'CreatedDate', N'DisplayName', N'IsDeleted', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Roles]'))
    SET IDENTITY_INSERT [Roles] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Teams]'))
    SET IDENTITY_INSERT [Teams] ON;
INSERT INTO [Teams] ([Id], [CreatedBy], [CreatedDate], [IsDeleted], [Name], [UpdatedBy], [UpdatedDate])
VALUES ('54466f17-02af-48e7-8ed3-5a4a8bfacf6f', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845123+07:00', CAST(0 AS bit), N'Mocha', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845133+07:00'),
('f808ddcd-b5e5-4d80-b732-1ca523e48434', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845137+07:00', CAST(0 AS bit), N'Latte', 'ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c', '2024-12-05T23:16:25.0845138+07:00');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'CreatedBy', N'CreatedDate', N'IsDeleted', N'Name', N'UpdatedBy', N'UpdatedDate') AND [object_id] = OBJECT_ID(N'[Teams]'))
    SET IDENTITY_INSERT [Teams] OFF;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241205161627_AddTeamsAndRoles', N'8.0.11');
GO

COMMIT;
GO

