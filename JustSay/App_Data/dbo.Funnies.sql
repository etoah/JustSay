CREATE TABLE [dbo].[Funnies] (
    [ID]           INT            IDENTITY (1, 1) NOT NULL,
    [MemberID]     INT            NOT NULL,
    [ShowName]     NVARCHAR (20)  NULL,
    [Content]      NVARCHAR (MAX) NOT NULL,
    [Title]        NVARCHAR (30)  NULL,
    [Up]           INT            DEFAULT ((1)) NOT NULL,
    [Time]         DATETIME       NOT NULL,
    [CommentCount] INT            DEFAULT ((0)) NOT NULL,
    [ImgUrl]       VARCHAR (100)  NULL,
    [ConfessID]    INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Funnies_ToTable] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([ID])
);

