CREATE TABLE [dbo].[RFIDReader](
[ReaderId] [int] NOT NULL,
[ReaderLocation] [varchar](50) NOT NULL,
[ReaderIPAddress] [varchar](11) NOT NULL,
CONSTRAINT [PK_RFIDReader] PRIMARY KEY CLUSTERED ([ReaderId] ASC)
)
