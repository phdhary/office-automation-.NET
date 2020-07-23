CREATE TABLE [dbo].[OFTA_JenisSuratDinas]
(
	JenisSuratDinasID VARCHAR(2) NOT NULL CONSTRAINT DF_OFTA_JenisSuratDinas_JenisSuratDinasID DEFAULT(''),
	JenisSuratDinasName VARCHAR(20) NOT NULL CONSTRAINT DF_OFTA_JenisSuratDinas_JenisSuratDinasName DEFAULT(''),

	CONSTRAINT PK_OFTA_JenisSuratDinas_JenisSuratDinasID PRIMARY KEY CLUSTERED (JenisSuratDinasID)
)
