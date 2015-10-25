CREATE TABLE [dbo].[Members] (
    [ID]           INT           IDENTITY (1, 1) NOT NULL,
    [Email]        VARCHAR (50)  NOT NULL,
    [Password]     VARCHAR (50)  NOT NULL,
    [JoinDate]     DATETIME      DEFAULT (getdate()) NOT NULL,
    [ShowName]     NVARCHAR (20) NULL,
    [LastVisit]    DATETIME      NULL,
    [QQ]           VARCHAR (15)  NULL,
    [Phone]        VARCHAR (20)  NULL,
    [RealName]     NVARCHAR (20) NULL,
    [RoleDeadLine] DATETIME      NULL,
    [Score]        INT           DEFAULT ((0)) NOT NULL,
    [Money]        INT           DEFAULT ((0)) NOT NULL,
    [LastPostTime] DATETIME      NULL,
    [MessageCount] INT           DEFAULT ((0)) NOT NULL,
    [RoleID]       INT           DEFAULT ((4)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Members_ToTable] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([ID])
);

