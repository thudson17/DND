
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/23/2015 07:23:30
-- Generated from EDMX file: C:\Users\Ray\Documents\GitHub\DND\DnDMaps\Maps\Models\Maps.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DND_MAPS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Character_Player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Characters] DROP CONSTRAINT [FK_Character_Player];
GO
IF OBJECT_ID(N'[dbo].[FK_EncounterEncounter_Monster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Encounter_Monster] DROP CONSTRAINT [FK_EncounterEncounter_Monster];
GO
IF OBJECT_ID(N'[dbo].[FK_MonsterEncounter_Monster]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Encounter_Monster] DROP CONSTRAINT [FK_MonsterEncounter_Monster];
GO
IF OBJECT_ID(N'[dbo].[FK_EncounterEncounter_Character]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Encounter_Character] DROP CONSTRAINT [FK_EncounterEncounter_Character];
GO
IF OBJECT_ID(N'[dbo].[FK_Encounter_CharacterCharacter]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Encounter_Character] DROP CONSTRAINT [FK_Encounter_CharacterCharacter];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Characters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Characters];
GO
IF OBJECT_ID(N'[dbo].[Maps]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Maps];
GO
IF OBJECT_ID(N'[dbo].[Players]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Players];
GO
IF OBJECT_ID(N'[dbo].[Encounters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Encounters];
GO
IF OBJECT_ID(N'[dbo].[Monsters]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Monsters];
GO
IF OBJECT_ID(N'[dbo].[Encounter_Monster]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Encounter_Monster];
GO
IF OBJECT_ID(N'[dbo].[Encounter_Character]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Encounter_Character];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Characters'
CREATE TABLE [dbo].[Characters] (
    [Character_ID] int  NOT NULL,
    [Player_ID] int  NOT NULL,
    [Name] varchar(1000)  NOT NULL,
    [Avatar_IMG] varchar(max)  NULL,
    [Initative_Bonus] smallint  NOT NULL
);
GO

-- Creating table 'Maps'
CREATE TABLE [dbo].[Maps] (
    [Map_ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(2000)  NOT NULL,
    [Background_IMG_Path] varchar(max)  NOT NULL,
    [Width_Squares] int  NOT NULL,
    [Height_Squares] int  NOT NULL,
    [SortOrder] int  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [Player_ID] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(500)  NOT NULL
);
GO

-- Creating table 'Encounters'
CREATE TABLE [dbo].[Encounters] (
    [Encounter_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Monsters'
CREATE TABLE [dbo].[Monsters] (
    [Monster_ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Avatar_IMG] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Encounter_Monster'
CREATE TABLE [dbo].[Encounter_Monster] (
    [Encounter_ID] int  NOT NULL,
    [Monster_ID] int  NOT NULL,
    [Initiative] smallint  NOT NULL
);
GO

-- Creating table 'Encounter_Character'
CREATE TABLE [dbo].[Encounter_Character] (
    [Encounter_ID] int  NOT NULL,
    [Character_ID] int  NOT NULL,
    [Initiative] smallint  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Character_ID] in table 'Characters'
ALTER TABLE [dbo].[Characters]
ADD CONSTRAINT [PK_Characters]
    PRIMARY KEY CLUSTERED ([Character_ID] ASC);
GO

-- Creating primary key on [Map_ID] in table 'Maps'
ALTER TABLE [dbo].[Maps]
ADD CONSTRAINT [PK_Maps]
    PRIMARY KEY CLUSTERED ([Map_ID] ASC);
GO

-- Creating primary key on [Player_ID] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([Player_ID] ASC);
GO

-- Creating primary key on [Encounter_ID] in table 'Encounters'
ALTER TABLE [dbo].[Encounters]
ADD CONSTRAINT [PK_Encounters]
    PRIMARY KEY CLUSTERED ([Encounter_ID] ASC);
GO

-- Creating primary key on [Monster_ID] in table 'Monsters'
ALTER TABLE [dbo].[Monsters]
ADD CONSTRAINT [PK_Monsters]
    PRIMARY KEY CLUSTERED ([Monster_ID] ASC);
GO

-- Creating primary key on [Monster_ID], [Encounter_ID] in table 'Encounter_Monster'
ALTER TABLE [dbo].[Encounter_Monster]
ADD CONSTRAINT [PK_Encounter_Monster]
    PRIMARY KEY CLUSTERED ([Monster_ID], [Encounter_ID] ASC);
GO

-- Creating primary key on [Encounter_ID], [Character_ID] in table 'Encounter_Character'
ALTER TABLE [dbo].[Encounter_Character]
ADD CONSTRAINT [PK_Encounter_Character]
    PRIMARY KEY CLUSTERED ([Encounter_ID], [Character_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Player_ID] in table 'Characters'
ALTER TABLE [dbo].[Characters]
ADD CONSTRAINT [FK_Character_Player]
    FOREIGN KEY ([Player_ID])
    REFERENCES [dbo].[Players]
        ([Player_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Character_Player'
CREATE INDEX [IX_FK_Character_Player]
ON [dbo].[Characters]
    ([Player_ID]);
GO

-- Creating foreign key on [Encounter_ID] in table 'Encounter_Monster'
ALTER TABLE [dbo].[Encounter_Monster]
ADD CONSTRAINT [FK_EncounterEncounter_Monster]
    FOREIGN KEY ([Encounter_ID])
    REFERENCES [dbo].[Encounters]
        ([Encounter_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EncounterEncounter_Monster'
CREATE INDEX [IX_FK_EncounterEncounter_Monster]
ON [dbo].[Encounter_Monster]
    ([Encounter_ID]);
GO

-- Creating foreign key on [Monster_ID] in table 'Encounter_Monster'
ALTER TABLE [dbo].[Encounter_Monster]
ADD CONSTRAINT [FK_MonsterEncounter_Monster]
    FOREIGN KEY ([Monster_ID])
    REFERENCES [dbo].[Monsters]
        ([Monster_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Encounter_ID] in table 'Encounter_Character'
ALTER TABLE [dbo].[Encounter_Character]
ADD CONSTRAINT [FK_EncounterEncounter_Character]
    FOREIGN KEY ([Encounter_ID])
    REFERENCES [dbo].[Encounters]
        ([Encounter_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Character_ID] in table 'Encounter_Character'
ALTER TABLE [dbo].[Encounter_Character]
ADD CONSTRAINT [FK_Encounter_CharacterCharacter]
    FOREIGN KEY ([Character_ID])
    REFERENCES [dbo].[Characters]
        ([Character_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Encounter_CharacterCharacter'
CREATE INDEX [IX_FK_Encounter_CharacterCharacter]
ON [dbo].[Encounter_Character]
    ([Character_ID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------