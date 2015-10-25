CREATE TABLE [dbo].[EmailQueue]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ConfessID] INT NOT NULL DEFAULT 0, 
    [RelationID] INT NOT NULL DEFAULT 0, 
    [Email] VARCHAR(100) NOT NULL, 
    [Time] DATETIME NOT NULL, 
    CONSTRAINT [FK_EmailQueue_ToConfess] FOREIGN KEY ([ConfessID]) REFERENCES [Confesses]([ID]), 
    CONSTRAINT [FK_EmailQueue_ToRelation] FOREIGN KEY ([RelationID]) REFERENCES [Relations]([ID])
)
