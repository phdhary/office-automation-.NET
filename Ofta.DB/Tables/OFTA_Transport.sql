CREATE TABLE [dbo].[OFTA_Transport]
(
	TransportID VARCHAR(3) NOT NULL CONSTRAINT DF_OFTA_Transport_TransportID DEFAULT(''),
	TransportName VARCHAR(20) NOT NULL CONSTRAINT DF_OFTA_Transport_TransportName DEFAULT(''),

	CONSTRAINT PK_OFTA_Transport_TransportID PRIMARY KEY CLUSTERED(TransportID)
)
