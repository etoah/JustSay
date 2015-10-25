CREATE TABLE [dbo].[Pro] (
    [ID]        INT           IDENTITY (1, 1) NOT NULL,
    [PostID]    INT           NOT NULL,
    [TypeID]    INT           NOT NULL,
    [TypeName]  NVARCHAR (50) NULL,
    [Info]      NVARCHAR (50) NULL,
    [TimeStart] DATETIME      NOT NULL,
    [DeadLine]  DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

