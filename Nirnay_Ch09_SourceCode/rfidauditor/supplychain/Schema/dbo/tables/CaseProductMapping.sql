CREATE TABLE [dbo].[CaseProductMapping](
[CaseProductMappingId] [int] NOT NULL,
[CaseId] [int] NOT NULL,
[ProductId] [int] NOT NULL,
CONSTRAINT [PK_Mapping] PRIMARY KEY CLUSTERED (	[CaseProductMappingId] ASC),
CONSTRAINT [FK_Mapping_Case] FOREIGN KEY([CaseId]) REFERENCES [dbo].[Case] ([CaseId]),
CONSTRAINT [FK_Mapping_Product] FOREIGN KEY([ProductId]) REFERENCES [dbo].[Product] ([ProductId])
)
