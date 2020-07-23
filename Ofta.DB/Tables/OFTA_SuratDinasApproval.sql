CREATE TABLE [dbo].[OFTA_SuratDinasApproval]
(
	SuratDinasID VARCHAR(14) NOT NULL CONSTRAINT DF_OFTA_SuratDinasApproval_SuratDinasID DEFAULT(''),
	PegID VARCHAR(5) NOT NULL CONSTRAINT DF_OFTA_SuratDinasApproval_PegID DEFAULT(''),
	ApprovalTypeID VARCHAR(2) NOT NULL CONSTRAINT DF_OFTA_SuratDinasApproval_ApprovalTypeID DEFAULT(''),
)
GO

CREATE CLUSTERED INDEX CX_OFTA_SuratDinasApproval_SuratDinasID
	ON OFTA_SuratDinasApproval (SuratDinasID)
GO
