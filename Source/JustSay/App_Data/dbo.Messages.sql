CREATE TABLE [dbo].[Messages] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [FromID]   INT            NOT NULL,
    [ToID]     INT            NOT NULL,
    [FromName] NVARCHAR (20)  NOT NULL,
    [ToName]   NVARCHAR (20)  NOT NULL,
    [IsNew]    BIT            DEFAULT ((1)) NOT NULL,
    [Time]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [Content]  NVARCHAR (500) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Messages_ToFromID] FOREIGN KEY ([FromID]) REFERENCES [dbo].[Members] ([ID]),
    CONSTRAINT [FK_Messages_ToToID] FOREIGN KEY ([ToID]) REFERENCES [dbo].[Members] ([ID])
);

