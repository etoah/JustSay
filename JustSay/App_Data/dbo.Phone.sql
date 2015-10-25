CREATE TABLE [dbo].[PhoneQueue]
(
	[ID] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ConfessID] INT NOT NULL DEFAULT 0, 
    [RelationID] INT NOT NULL DEFAULT 0, 
    [Phone] VARCHAR(100) NOT NULL, 
    [Time] DATETIME NULL, 
    CONSTRAINT [FK_PhoneQueue_ToConfess] FOREIGN KEY ([ConfessID]) REFERENCES [Confesses]([ID]), 
    CONSTRAINT [FK_PhoneQueue_ToRelation] FOREIGN KEY ([RelationID]) REFERENCES [Relations]([ID])
)