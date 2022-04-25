
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/19/2021 19:56:12
-- Generated from EDMX file: C:\Users\Bruno\source\repos\PPPKBrunoHrgovicMVC\PPPKBrunoHrgovicMVC\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PPPK_MVC];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActorSet'
CREATE TABLE [dbo].[ActorSet] (
    [IDActor] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Age] int  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'DirectorSet'
CREATE TABLE [dbo].[DirectorSet] (
    [IDDirector] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Age] int  NOT NULL,
    [Gender] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'MovieSet'
CREATE TABLE [dbo].[MovieSet] (
    [IDMovie] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Duration] nvarchar(max)  NOT NULL,
    [Release] int  NOT NULL,
    [ActorIDActor] int  NOT NULL,
    [DirectorIDDirector] int  NOT NULL
);
GO

-- Creating table 'ActorUploadedFilesSet'
CREATE TABLE [dbo].[ActorUploadedFilesSet] (
    [IDActorUploadedFiles] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ContentType] nvarchar(max)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [ActorIDActor] int  NOT NULL
);
GO

-- Creating table 'DirectorUploadedFilesSet'
CREATE TABLE [dbo].[DirectorUploadedFilesSet] (
    [IDDirectorUploadedFiles] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ContentType] nvarchar(max)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [DirectorIDDirector] int  NOT NULL
);
GO

-- Creating table 'MovieUploadedFilesSet'
CREATE TABLE [dbo].[MovieUploadedFilesSet] (
    [IDMovieUploadedFiles] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ContentType] nvarchar(max)  NOT NULL,
    [Content] varbinary(max)  NOT NULL,
    [MovieIDMovie] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [IDActor] in table 'ActorSet'
ALTER TABLE [dbo].[ActorSet]
ADD CONSTRAINT [PK_ActorSet]
    PRIMARY KEY CLUSTERED ([IDActor] ASC);
GO

-- Creating primary key on [IDDirector] in table 'DirectorSet'
ALTER TABLE [dbo].[DirectorSet]
ADD CONSTRAINT [PK_DirectorSet]
    PRIMARY KEY CLUSTERED ([IDDirector] ASC);
GO

-- Creating primary key on [IDMovie] in table 'MovieSet'
ALTER TABLE [dbo].[MovieSet]
ADD CONSTRAINT [PK_MovieSet]
    PRIMARY KEY CLUSTERED ([IDMovie] ASC);
GO

-- Creating primary key on [IDActorUploadedFiles] in table 'ActorUploadedFilesSet'
ALTER TABLE [dbo].[ActorUploadedFilesSet]
ADD CONSTRAINT [PK_ActorUploadedFilesSet]
    PRIMARY KEY CLUSTERED ([IDActorUploadedFiles] ASC);
GO

-- Creating primary key on [IDDirectorUploadedFiles] in table 'DirectorUploadedFilesSet'
ALTER TABLE [dbo].[DirectorUploadedFilesSet]
ADD CONSTRAINT [PK_DirectorUploadedFilesSet]
    PRIMARY KEY CLUSTERED ([IDDirectorUploadedFiles] ASC);
GO

-- Creating primary key on [IDMovieUploadedFiles] in table 'MovieUploadedFilesSet'
ALTER TABLE [dbo].[MovieUploadedFilesSet]
ADD CONSTRAINT [PK_MovieUploadedFilesSet]
    PRIMARY KEY CLUSTERED ([IDMovieUploadedFiles] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ActorIDActor] in table 'MovieSet'
ALTER TABLE [dbo].[MovieSet]
ADD CONSTRAINT [FK_ActorMovie]
    FOREIGN KEY ([ActorIDActor])
    REFERENCES [dbo].[ActorSet]
        ([IDActor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActorMovie'
CREATE INDEX [IX_FK_ActorMovie]
ON [dbo].[MovieSet]
    ([ActorIDActor]);
GO

-- Creating foreign key on [DirectorIDDirector] in table 'MovieSet'
ALTER TABLE [dbo].[MovieSet]
ADD CONSTRAINT [FK_DirectorMovie]
    FOREIGN KEY ([DirectorIDDirector])
    REFERENCES [dbo].[DirectorSet]
        ([IDDirector])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DirectorMovie'
CREATE INDEX [IX_FK_DirectorMovie]
ON [dbo].[MovieSet]
    ([DirectorIDDirector]);
GO

-- Creating foreign key on [ActorIDActor] in table 'ActorUploadedFilesSet'
ALTER TABLE [dbo].[ActorUploadedFilesSet]
ADD CONSTRAINT [FK_ActorActorUploadedFiles]
    FOREIGN KEY ([ActorIDActor])
    REFERENCES [dbo].[ActorSet]
        ([IDActor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ActorActorUploadedFiles'
CREATE INDEX [IX_FK_ActorActorUploadedFiles]
ON [dbo].[ActorUploadedFilesSet]
    ([ActorIDActor]);
GO

-- Creating foreign key on [DirectorIDDirector] in table 'DirectorUploadedFilesSet'
ALTER TABLE [dbo].[DirectorUploadedFilesSet]
ADD CONSTRAINT [FK_DirectorDirectorUploadedFiles]
    FOREIGN KEY ([DirectorIDDirector])
    REFERENCES [dbo].[DirectorSet]
        ([IDDirector])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DirectorDirectorUploadedFiles'
CREATE INDEX [IX_FK_DirectorDirectorUploadedFiles]
ON [dbo].[DirectorUploadedFilesSet]
    ([DirectorIDDirector]);
GO

-- Creating foreign key on [MovieIDMovie] in table 'MovieUploadedFilesSet'
ALTER TABLE [dbo].[MovieUploadedFilesSet]
ADD CONSTRAINT [FK_MovieMovieUploadedFiles]
    FOREIGN KEY ([MovieIDMovie])
    REFERENCES [dbo].[MovieSet]
        ([IDMovie])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MovieMovieUploadedFiles'
CREATE INDEX [IX_FK_MovieMovieUploadedFiles]
ON [dbo].[MovieUploadedFilesSet]
    ([MovieIDMovie]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------