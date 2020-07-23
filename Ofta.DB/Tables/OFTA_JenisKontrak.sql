CREATE TABLE [dbo].[OFTA_JenisKontrak]
(
	JenisKontrakID VARCHAR(2) NOT NULL CONSTRAINT DF_OFTA_JenisKontrak_JenisKontrakID DEFAULT(''),
	JenisKontrakName VARCHAR(20) NOT NULL CONSTRAINT DF_OFTA_JenisKontrak_JenisKontrakName DEFAULT(''),

	CONSTRAINT PK_OFTA_JenisKontrak_JenisKontrakID PRIMARY KEY CLUSTERED(JenisKontrakID)
)
