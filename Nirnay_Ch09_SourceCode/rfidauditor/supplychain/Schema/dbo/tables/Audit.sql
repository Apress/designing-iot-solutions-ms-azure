CREATE TABLE [dbo].[Audit](
[AuditId] [int] NOT NULL,
[ReaderId] [int] NOT NULL,
[EPC] [int] NOT NULL,
[ReadingDateTime] [datetime] NOT NULL,
CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED ([AuditId] ASC),
CONSTRAINT [FK_Audit_RFIDReader] FOREIGN KEY([ReaderId]) REFERENCES [dbo].[RFIDReader] ([ReaderId]),
)
