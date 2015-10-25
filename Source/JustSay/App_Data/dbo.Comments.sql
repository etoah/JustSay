CREATE TABLE [dbo].[Comments] (
    [ID]       INT             IDENTITY (1, 1) NOT NULL,
    [MemberID] INT             NOT NULL,
    [ShowName] NVARCHAR (20)   NULL,
    [Time]     DATETIME        DEFAULT (getdate()) NOT NULL,
    [Content]  NVARCHAR (1000) NOT NULL,
    [FunnyID]  INT             NOT NULL,
    [Up]       INT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Comments_ToFunnies] FOREIGN KEY ([FunnyID]) REFERENCES [dbo].[Funnies] ([ID]),
    CONSTRAINT [FK_Comments_ToTable] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([ID])
);

