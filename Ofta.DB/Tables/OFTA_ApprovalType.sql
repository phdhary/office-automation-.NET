CREATE TABLE [dbo].[OFTA_ApprovalType]
(
	ApprovalTypeID VARCHAR(2) NOT NULL CONSTRAINT DF_OFTA_Approval_ApprovalTypeID DEFAULT(''),
	ApprovalTypeName VARCHAR(30) NOT NULL CONSTRAINT DF_OFTA_Approval_ApprovalTypeName DEFAULT('')

	CONSTRAINT PK_ApprovalType_ApprovalTypeID PRIMARY KEY CLUSTERED(ApprovalTypeID)
)
