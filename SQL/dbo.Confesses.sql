CREATE TABLE [dbo].[Confesses] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [MemberID] INT            NOT NULL,
    [ShowName] NVARCHAR (20)  NULL,
    [Content]  NVARCHAR (MAX) NOT NULL,
    [Time]     DATETIME       NOT NULL,
    [Up]       INT            DEFAULT ((1)) NOT NULL,
    [Click]    INT            DEFAULT ((1)) NOT NULL,
    [Message]  NVARCHAR (30)  NULL,
    [IsSms]    BIT            DEFAULT ((0)) NOT NULL,
    [FlashUrl] VARCHAR (500)  NULL,
    [MusicUrl] VARCHAR (500)  NULL,
    [ToEmail]  VARCHAR (50)   NOT NULL,
    [ToPhone]  VARCHAR (30)   NULL,
    [ToName]   NVARCHAR (20)  NOT NULL,
    [ImgUrl]   VARCHAR (100)  NULL,
    [ViewName] VARCHAR (50)   NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Table_ToTable] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([ID])
);

