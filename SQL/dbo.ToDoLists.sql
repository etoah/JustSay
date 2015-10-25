CREATE TABLE [dbo].[ToDoLists] (
    [ID]      INT            IDENTITY (1, 1) NOT NULL,
    [Content] NVARCHAR (300) NOT NULL,
    [Status]  INT            DEFAULT ((10)) NOT NULL,
    [From]    NVARCHAR (50)  NOT NULL,
    [Time]    DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

