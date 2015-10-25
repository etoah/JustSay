CREATE TABLE [dbo].[PhoneQueue] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [ConfessID]  INT           DEFAULT ((0)) NOT NULL,
    [RelationID] INT           DEFAULT ((0)) NOT NULL,
    [Phone]      VARCHAR (100) NOT NULL,
    [Time]       DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_PhoneQueue_ToConfess] FOREIGN KEY ([ConfessID]) REFERENCES [dbo].[Confesses] ([ID]),
    CONSTRAINT [FK_PhoneQueue_ToRelation] FOREIGN KEY ([RelationID]) REFERENCES [dbo].[Relations] ([ID])
);

