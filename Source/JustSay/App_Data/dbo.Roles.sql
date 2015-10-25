CREATE TABLE [dbo].[Roles] (
    [ID]         INT          IDENTITY (1, 1) NOT NULL,
    [RoleName]   VARCHAR (50) NOT NULL,
    [Department] INT          DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

