
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 08/13/2014 21:30:54
-- Generated from EDMX file: E:\WEB\JustSay\Code2\JustSay\Justsay.Models\JustSay.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [JustSay];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ActionLog_ToMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionLog] DROP CONSTRAINT [FK_ActionLog_ToMember];
GO
IF OBJECT_ID(N'[dbo].[FK_Comments_ToFunnies]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_ToFunnies];
GO
IF OBJECT_ID(N'[dbo].[FK_Comments_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comments] DROP CONSTRAINT [FK_Comments_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailQueue_ToConfess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailQueue] DROP CONSTRAINT [FK_EmailQueue_ToConfess];
GO
IF OBJECT_ID(N'[dbo].[FK_EmailQueue_ToRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EmailQueue] DROP CONSTRAINT [FK_EmailQueue_ToRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_Funnies_ToConfess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Funnies] DROP CONSTRAINT [FK_Funnies_ToConfess];
GO
IF OBJECT_ID(N'[dbo].[FK_Funnies_ToMember]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Funnies] DROP CONSTRAINT [FK_Funnies_ToMember];
GO
IF OBJECT_ID(N'[dbo].[FK_Members_ToRole]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Members] DROP CONSTRAINT [FK_Members_ToRole];
GO
IF OBJECT_ID(N'[dbo].[FK_Messages_ToFromID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_ToFromID];
GO
IF OBJECT_ID(N'[dbo].[FK_Messages_ToToID]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Messages] DROP CONSTRAINT [FK_Messages_ToToID];
GO
IF OBJECT_ID(N'[dbo].[FK_PhoneQueue_ToConfess]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhoneQueue] DROP CONSTRAINT [FK_PhoneQueue_ToConfess];
GO
IF OBJECT_ID(N'[dbo].[FK_PhoneQueue_ToRelation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PhoneQueue] DROP CONSTRAINT [FK_PhoneQueue_ToRelation];
GO
IF OBJECT_ID(N'[dbo].[FK_Relations_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Relations] DROP CONSTRAINT [FK_Relations_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_Table_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Confesses] DROP CONSTRAINT [FK_Table_ToTable];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ActionLog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionLog];
GO
IF OBJECT_ID(N'[dbo].[Comments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comments];
GO
IF OBJECT_ID(N'[dbo].[Confesses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Confesses];
GO
IF OBJECT_ID(N'[dbo].[EmailQueue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EmailQueue];
GO
IF OBJECT_ID(N'[dbo].[Funnies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Funnies];
GO
IF OBJECT_ID(N'[dbo].[Members]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Members];
GO
IF OBJECT_ID(N'[dbo].[Messages]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Messages];
GO
IF OBJECT_ID(N'[dbo].[PhoneQueue]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PhoneQueue];
GO
IF OBJECT_ID(N'[dbo].[Pro]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pro];
GO
IF OBJECT_ID(N'[dbo].[Relations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Relations];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[ToDoLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ToDoLists];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ActionLogs'
CREATE TABLE [dbo].[ActionLogs] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [Action] nvarchar(255)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [ClientIP] varchar(50)  NOT NULL,
    [AdminReplyTime] datetime  NULL,
    [CreateTime] datetime  NOT NULL
);
GO

-- Creating table 'Comments'
CREATE TABLE [dbo].[Comments] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ShowName] nvarchar(20)  NULL,
    [Time] datetime  NOT NULL,
    [Content] nvarchar(1000)  NOT NULL,
    [FunnyID] int  NOT NULL,
    [Up] int  NOT NULL
);
GO

-- Creating table 'Confesses'
CREATE TABLE [dbo].[Confesses] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ShowName] nvarchar(20)  NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Time] datetime  NOT NULL,
    [Up] int  NOT NULL,
    [Click] int  NOT NULL,
    [Message] nvarchar(30)  NULL,
    [IsSms] bit  NOT NULL,
    [FlashUrl] varchar(500)  NULL,
    [MusicUrl] varchar(500)  NULL,
    [ToEmail] varchar(50)  NOT NULL,
    [ToPhone] varchar(30)  NULL,
    [ToName] nvarchar(20)  NOT NULL,
    [ImgUrl] varchar(100)  NULL,
    [ViewName] varchar(50)  NULL
);
GO

-- Creating table 'Funnies'
CREATE TABLE [dbo].[Funnies] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ShowName] nvarchar(20)  NULL,
    [Content] nvarchar(max)  NOT NULL,
    [Title] nvarchar(30)  NULL,
    [Up] int  NOT NULL,
    [Time] datetime  NOT NULL,
    [CommentCount] int  NOT NULL,
    [ImgUrl] varchar(100)  NULL,
    [ConfessID] int  NULL,
    [Status] int  NOT NULL
);
GO

-- Creating table 'Members'
CREATE TABLE [dbo].[Members] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Password] varchar(32)  NOT NULL,
    [JoinDate] datetime  NOT NULL,
    [ShowName] nvarchar(20)  NULL,
    [LastVisit] datetime  NULL,
    [QQ] varchar(15)  NULL,
    [Phone] varchar(20)  NULL,
    [RealName] nvarchar(20)  NULL,
    [RoleDeadLine] datetime  NULL,
    [Score] int  NOT NULL,
    [Money] int  NOT NULL,
    [LastPostTime] datetime  NOT NULL,
    [MessageCount] int  NOT NULL,
    [RoleID] int  NOT NULL
);
GO

-- Creating table 'Messages'
CREATE TABLE [dbo].[Messages] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [FromID] int  NOT NULL,
    [ToID] int  NOT NULL,
    [FromName] nvarchar(20)  NULL,
    [ToName] nvarchar(20)  NULL,
    [IsNew] bit  NOT NULL,
    [Time] datetime  NOT NULL,
    [Content] nvarchar(500)  NOT NULL
);
GO

-- Creating table 'Relations'
CREATE TABLE [dbo].[Relations] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [MemberID] int  NOT NULL,
    [ShowName] nvarchar(20)  NULL,
    [FromName] nvarchar(10)  NOT NULL,
    [FromPhone] varchar(20)  NOT NULL,
    [FromEmail] varchar(50)  NOT NULL,
    [ToName] nvarchar(10)  NOT NULL,
    [ToPhone] varchar(20)  NOT NULL,
    [ToEmail] varchar(50)  NOT NULL,
    [Time] datetime  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleName] varchar(50)  NOT NULL,
    [Department] int  NOT NULL
);
GO

-- Creating table 'ToDoLists'
CREATE TABLE [dbo].[ToDoLists] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Content] nvarchar(300)  NOT NULL,
    [Status] int  NOT NULL,
    [From] nvarchar(50)  NOT NULL,
    [Time] datetime  NOT NULL
);
GO

-- Creating table 'EmailQueues'
CREATE TABLE [dbo].[EmailQueues] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ConfessID] int  NOT NULL,
    [RelationID] int  NOT NULL,
    [Email] varchar(100)  NOT NULL,
    [Time] datetime  NULL
);
GO

-- Creating table 'PhoneQueues'
CREATE TABLE [dbo].[PhoneQueues] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ConfessID] int  NOT NULL,
    [RelationID] int  NOT NULL,
    [Phone] varchar(100)  NOT NULL,
    [Time] datetime  NULL
);
GO

-- Creating table 'Pros'
CREATE TABLE [dbo].[Pros] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PostID] int  NOT NULL,
    [TypeID] int  NOT NULL,
    [TypeName] nvarchar(50)  NULL,
    [Info] nvarchar(50)  NULL,
    [TimeStart] datetime  NOT NULL,
    [DeadLine] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'ActionLogs'
ALTER TABLE [dbo].[ActionLogs]
ADD CONSTRAINT [PK_ActionLogs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [PK_Comments]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Confesses'
ALTER TABLE [dbo].[Confesses]
ADD CONSTRAINT [PK_Confesses]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Funnies'
ALTER TABLE [dbo].[Funnies]
ADD CONSTRAINT [PK_Funnies]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [PK_Members]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [PK_Messages]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [PK_Relations]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ToDoLists'
ALTER TABLE [dbo].[ToDoLists]
ADD CONSTRAINT [PK_ToDoLists]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'EmailQueues'
ALTER TABLE [dbo].[EmailQueues]
ADD CONSTRAINT [PK_EmailQueues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PhoneQueues'
ALTER TABLE [dbo].[PhoneQueues]
ADD CONSTRAINT [PK_PhoneQueues]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Pros'
ALTER TABLE [dbo].[Pros]
ADD CONSTRAINT [PK_Pros]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MemberID] in table 'ActionLogs'
ALTER TABLE [dbo].[ActionLogs]
ADD CONSTRAINT [FK_ActionLog_ToMember]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionLog_ToMember'
CREATE INDEX [IX_FK_ActionLog_ToMember]
ON [dbo].[ActionLogs]
    ([MemberID]);
GO

-- Creating foreign key on [FunnyID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_Comments_ToFunnies]
    FOREIGN KEY ([FunnyID])
    REFERENCES [dbo].[Funnies]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Comments_ToFunnies'
CREATE INDEX [IX_FK_Comments_ToFunnies]
ON [dbo].[Comments]
    ([FunnyID]);
GO

-- Creating foreign key on [MemberID] in table 'Comments'
ALTER TABLE [dbo].[Comments]
ADD CONSTRAINT [FK_Comments_ToTable]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Comments_ToTable'
CREATE INDEX [IX_FK_Comments_ToTable]
ON [dbo].[Comments]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'Confesses'
ALTER TABLE [dbo].[Confesses]
ADD CONSTRAINT [FK_Table_ToTable]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Table_ToTable'
CREATE INDEX [IX_FK_Table_ToTable]
ON [dbo].[Confesses]
    ([MemberID]);
GO

-- Creating foreign key on [MemberID] in table 'Funnies'
ALTER TABLE [dbo].[Funnies]
ADD CONSTRAINT [FK_Funnies_ToTable]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Funnies_ToTable'
CREATE INDEX [IX_FK_Funnies_ToTable]
ON [dbo].[Funnies]
    ([MemberID]);
GO

-- Creating foreign key on [RoleID] in table 'Members'
ALTER TABLE [dbo].[Members]
ADD CONSTRAINT [FK_Members_ToTable]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Roles]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Members_ToTable'
CREATE INDEX [IX_FK_Members_ToTable]
ON [dbo].[Members]
    ([RoleID]);
GO

-- Creating foreign key on [FromID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Messages_ToFromID]
    FOREIGN KEY ([FromID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Messages_ToFromID'
CREATE INDEX [IX_FK_Messages_ToFromID]
ON [dbo].[Messages]
    ([FromID]);
GO

-- Creating foreign key on [ToID] in table 'Messages'
ALTER TABLE [dbo].[Messages]
ADD CONSTRAINT [FK_Messages_ToToID]
    FOREIGN KEY ([ToID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Messages_ToToID'
CREATE INDEX [IX_FK_Messages_ToToID]
ON [dbo].[Messages]
    ([ToID]);
GO

-- Creating foreign key on [MemberID] in table 'Relations'
ALTER TABLE [dbo].[Relations]
ADD CONSTRAINT [FK_Relations_ToTable]
    FOREIGN KEY ([MemberID])
    REFERENCES [dbo].[Members]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Relations_ToTable'
CREATE INDEX [IX_FK_Relations_ToTable]
ON [dbo].[Relations]
    ([MemberID]);
GO

-- Creating foreign key on [ConfessID] in table 'EmailQueues'
ALTER TABLE [dbo].[EmailQueues]
ADD CONSTRAINT [FK_EmailQueue_ToConfess]
    FOREIGN KEY ([ConfessID])
    REFERENCES [dbo].[Confesses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailQueue_ToConfess'
CREATE INDEX [IX_FK_EmailQueue_ToConfess]
ON [dbo].[EmailQueues]
    ([ConfessID]);
GO

-- Creating foreign key on [ConfessID] in table 'PhoneQueues'
ALTER TABLE [dbo].[PhoneQueues]
ADD CONSTRAINT [FK_PhoneQueue_ToConfess]
    FOREIGN KEY ([ConfessID])
    REFERENCES [dbo].[Confesses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhoneQueue_ToConfess'
CREATE INDEX [IX_FK_PhoneQueue_ToConfess]
ON [dbo].[PhoneQueues]
    ([ConfessID]);
GO

-- Creating foreign key on [RelationID] in table 'EmailQueues'
ALTER TABLE [dbo].[EmailQueues]
ADD CONSTRAINT [FK_EmailQueue_ToRelation]
    FOREIGN KEY ([RelationID])
    REFERENCES [dbo].[Relations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_EmailQueue_ToRelation'
CREATE INDEX [IX_FK_EmailQueue_ToRelation]
ON [dbo].[EmailQueues]
    ([RelationID]);
GO

-- Creating foreign key on [RelationID] in table 'PhoneQueues'
ALTER TABLE [dbo].[PhoneQueues]
ADD CONSTRAINT [FK_PhoneQueue_ToRelation]
    FOREIGN KEY ([RelationID])
    REFERENCES [dbo].[Relations]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PhoneQueue_ToRelation'
CREATE INDEX [IX_FK_PhoneQueue_ToRelation]
ON [dbo].[PhoneQueues]
    ([RelationID]);
GO

-- Creating foreign key on [ConfessID] in table 'Funnies'
ALTER TABLE [dbo].[Funnies]
ADD CONSTRAINT [FK_Funnies_ToConfess]
    FOREIGN KEY ([ConfessID])
    REFERENCES [dbo].[Confesses]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_Funnies_ToConfess'
CREATE INDEX [IX_FK_Funnies_ToConfess]
ON [dbo].[Funnies]
    ([ConfessID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------