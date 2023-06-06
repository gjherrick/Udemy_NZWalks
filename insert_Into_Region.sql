SELECT TOP (1000) 
		[Id]
      ,[Code]
      ,[Name]
      ,[RegionImageUrl]
  FROM [NZWalksDb].[dbo].[Region];
  /*** the regions data for the db **/
	INSERT into Region
	([Id], [Code], [Name], [RegionImageUrl])
	values ('DF215E10-8BD4-4401-B2DC-99BB03135F2E', 'AKL', 'Auckland', NULL)