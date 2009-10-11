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


