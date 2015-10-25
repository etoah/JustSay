CREATE TABLE [dbo].[Relations] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [MemberID]  INT           NOT NULL,
    [ShowName]  NVARCHAR (20) NULL,
    [FromName]  NVARCHAR (10) NOT NULL,
    [FromPhone] VARCHAR (20)  NOT NULL,
    [FromEmail] VARCHAR (50)  NOT NULL,
    [ToName]    NVARCHAR (10) NOT NULL,
    [ToPhone]   VARCHAR (20)  NOT NULL,
    [ToEmail]   VARCHAR (50)  NOT NULL,
    [Time]      DATETIME      DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Relations_ToTable] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([ID])
);

