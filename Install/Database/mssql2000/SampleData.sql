INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Root'
           ,'Root Category'
           ,null
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Root Category'
           ,null
           ,null
           )

DECLARE @ROOT_ID int
SET @ROOT_ID = @@identity



INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Hardware'
           ,'Hardware Description'
           ,@ROOT_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Hardware Category'
           ,null
           ,null
           )


DECLARE @HARDWARE_ID int
SET @HARDWARE_ID = @@identity


INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Software'
           ,'Software Description'
           ,@ROOT_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Software Category'
           ,null
           ,null
           )

DECLARE @SOFTWARE_ID int
SET @SOFTWARE_ID = @@identity

INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Operating Systems'
           ,'Operating Systems Description'
           ,@SOFTWARE_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Operating Systems Category'
           ,null
           ,null
           )

DECLARE @OPERATING_ID int
SET @OPERATING_ID = @@identity

INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Office Applications'
           ,'Office Applications Description'
           ,@SOFTWARE_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Office Applications Category'
           ,null
           ,null
           )

DECLARE @OFFICE_ID int
SET @OFFICE_ID = @@identity


INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Games'
           ,'Games Description'
           ,@SOFTWARE_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Games Category'
           ,null
           ,null
           )

DECLARE @GAMES_ID int
SET @GAMES_ID = @@identity

INSERT INTO [ECommerce_Category]
           ([categoryName]
           ,[categoryDescription]
           ,[parentCategoryID]
           ,[sortOrder]
           ,[isPublished]
           ,[imageUrl]
           ,[width]
           ,[height]
           ,[altText]
           ,[updateTimeStamp]
           ,[insertTimeStamp]
           ,[cssClass]
           ,[priceChangePercent]
           ,[bannerImageUrl])
     VALUES
           ('Development software'
           ,'Development software Description'
           ,@SOFTWARE_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Alt Text'
           ,GETDATE()
           ,GETDATE()
           ,'Development software Category'
           ,null
           ,null
           )

DECLARE @DEVELOPMENT_ID int
SET @DEVELOPMENT_ID = @@identity


/*Products */