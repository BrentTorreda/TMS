CREATE TABLE [dbo].[Notes] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [AssignedTo]  INT             NOT NULL,
    [DateCreated] DATETIME        NOT NULL,
    [Subject]     NVARCHAR (500)  NULL,
    [Body]        NVARCHAR (1000) NULL,
    [CreatedBy] INT NOT NULL, 
    CONSTRAINT [PK_dbo.Notes] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_dbo.Notes_dbo.Members_CreatedBy] FOREIGN KEY ([AssignedTo]) REFERENCES [dbo].[Members] ([MemberId]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AssignedTo]
    ON [dbo].[Notes]([AssignedTo] ASC);

