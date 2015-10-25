CREATE TABLE [dbo].[EmailQueue] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [ConfessID]  INT           DEFAULT ((0)) NOT NULL,
    [RelationID] INT           DEFAULT ((0)) NOT NULL,
    [Email]      VARCHAR (100) NOT NULL,
    [Time]       DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_EmailQueue_ToConfess] FOREIGN KEY ([ConfessID]) REFERENCES [dbo].[Confesses] ([ID]),
    CONSTRAINT [FK_EmailQueue_ToRelation] FOREIGN KEY ([RelationID]) REFERENCES [dbo].[Relations] ([ID])
);

