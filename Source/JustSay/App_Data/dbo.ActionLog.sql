CREATE TABLE [dbo].[ActionLog] (
    [ID]             INT            IDENTITY (1, 1) NOT NULL,
    [MemberID]       INT            NOT NULL,
    [Action]         NVARCHAR (255) NOT NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [ClientIP]       VARCHAR (50)   NOT NULL,
    [AdminReplyTime] DATETIME       NULL,
    [CreateTime]     DATETIME       DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_ActionLog_ToMember] FOREIGN KEY ([MemberID]) REFERENCES [dbo].[Members] ([ID])
);

