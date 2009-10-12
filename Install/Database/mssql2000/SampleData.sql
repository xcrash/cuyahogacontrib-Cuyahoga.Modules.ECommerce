
USE CuyahogaECommerceDemov3
DECLARE @RelatedID_1 int 
DECLARE @RelatedID_2 int 
DECLARE @RelatedID_3 int 

SET @RelatedID_1 = 0
SET @RelatedID_2 = 0
SET @RelatedID_3 = 0



INSERT INTO [ECommerce_RelationType]
           ([relationTypeID]
           ,[relationshipName]
           ,[relationshipDescription])
     VALUES
           (1
           ,'CrossSell'
           ,'Cross Sell')

INSERT INTO [ECommerce_RelationType]
           ([relationTypeID]
           ,[relationshipName]
           ,[relationshipDescription])
     VALUES
           (2
           ,'Up Sell'
           ,'Cross Sell')

INSERT INTO [ECommerce_RelationType]
           ([relationTypeID]
           ,[relationshipName]
           ,[relationshipDescription])
     VALUES
           (3
           ,'Combined Delivery Charge'
           ,'CombinedDeliveryCharge')




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


DECLARE @CAT_ID int

/*Documetns Types*/

INSERT INTO [Ecommerce_DocumentType]
           ([TypeName]
           ,[CssClass])
     VALUES
           ('PDF'
           ,'pdf')

DECLARE @DOCUMENT_TYPE_ID int
SET @DOCUMENT_TYPE_ID = @@IDENTITY

/* Documents */
INSERT INTO [Ecommerce_Document]
           ([DocumentName]
           ,[FilePath]
           ,[TypeID]
           ,[inserttimestamp]
           ,[updatetimestamp]
           ,[isPublished])
     VALUES
           ('Technical Specification'
           ,'/Modules/ECommerce/Documents/Spec.pdf'
           ,@DOCUMENT_TYPE_ID
           ,GETDATE()
           ,GETDATE()
           ,1)

DECLARE @DOCUMENT_ID_1 int
SET @DOCUMENT_ID_1 = @@IDENTITY

INSERT INTO [Ecommerce_Document]
           ([DocumentName]
           ,[FilePath]
           ,[TypeID]
           ,[inserttimestamp]
           ,[updatetimestamp]
           ,[isPublished])
     VALUES
           ('Product Manual'
           ,'/Modules/ECommerce/Documents/Manual.pdf'
           ,@DOCUMENT_TYPE_ID
           ,GETDATE()
           ,GETDATE()
           ,1)

DECLARE @DOCUMENT_ID_2 int
SET @DOCUMENT_ID_2 = @@IDENTITY


INSERT INTO [Ecommerce_Document]
           ([DocumentName]
           ,[FilePath]
           ,[TypeID]
           ,[inserttimestamp]
           ,[updatetimestamp]
           ,[isPublished])
     VALUES
           ('Product Features'
           ,'/Modules/ECommerce/Documents/Features.pdf'
           ,@DOCUMENT_TYPE_ID
           ,GETDATE()
           ,GETDATE()
           ,1)

DECLARE @DOCUMENT_ID_3 int
SET @DOCUMENT_ID_3 = @@IDENTITY



/*Products */

DECLARE @catFlag INT
SET @catFlag = 1
WHILE (@catFlag <=8)
BEGIN


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
           (SubString('Test ' +  CAST(newid() AS nVARCHAR(4000)), 1, 13)
           ,'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin tempus vulputate urna, eu euismod ipsum ornare dignissim. Morbi hendrerit justo in felis varius volutpat. Nunc ut tellus lacus. Etiam aliquam, nibh eu feugiat porta, ante arcu condimentum metus, non elementum tortor felis id eros. Curabitur venenatis bibendum orci, vitae viverra dolor pulvinar vitae. Sed id purus est, in dignissim mi. Vivamus pretium dolor vitae dolor suscipit vel convallis nisi varius. Etiam sagittis ante non velit fringilla rutrum. Nunc eleifend fringilla consequat. Suspendisse nibh sapien, consequat eget aliquam ac, lacinia in ipsum. '
           ,@ROOT_ID
           ,0
           ,1
           ,null
           ,400
           ,200
           ,'Lorem ipsum dolor sit amet'
           ,GETDATE()
           ,GETDATE()
           ,'Lorem Category'
           ,null
           ,null
           )

SET @CAT_ID = @@identity

				DECLARE @intFlag INT
				SET @intFlag = 1
				WHILE (@intFlag <=50)
				BEGIN


				INSERT INTO [ECommerce_Product]
						   ([itemCode]
						   ,[productName]
						   ,[productDescription]
						   ,[stockLevel]
						   ,[isPublished]
						   ,[basePrice]
						   ,[baseCurrencyCode]
						   ,[inserttimestamp]
						   ,[updatetimestamp]
						   ,[additionalInformation]
						   ,[features]
						   ,[basePriceDescription]
						   ,[shortProductDescription])
					 VALUES
						   (newid()
						   ,Substring('Test Product ' +  CAST(newid() AS nVARCHAR(4000)),1,18)
						   ,'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin tempus vulputate urna, eu euismod ipsum ornare dignissim. Morbi hendrerit justo in felis varius volutpat. Nunc ut tellus lacus. Etiam aliquam, nibh eu feugiat porta, ante arcu condimentum metus, non elementum tortor felis id eros. Curabitur venenatis bibendum orci, vitae viverra dolor pulvinar vitae. Sed id purus est, in dignissim mi. Vivamus pretium dolor vitae dolor suscipit vel convallis nisi varius. Etiam sagittis ante non velit fringilla rutrum. Nunc eleifend fringilla consequat. Suspendisse nibh sapien, consequat eget aliquam ac, lacinia in ipsum.

				Fusce eleifend malesuada nisi. Ut diam leo, ultrices porttitor elementum non, congue at leo. Nullam elit felis, mattis in vulputate ut, fermentum a est. Mauris eu leo non mauris dapibus ultricies sit amet sed ipsum. Mauris vitae mi in quam volutpat tristique. Pellentesque id elit sit amet lacus sagittis pretium nec eget augue. Donec malesuada lacus sed est gravida pharetra. Donec sed ipsum non augue congue ullamcorper id ut nisi. Nulla congue est nunc, vel egestas neque. Nullam sed sapien eu nisl scelerisque dapibus. Proin posuere dolor in quam tempor rhoncus.

				Curabitur vehicula laoreet justo. Suspendisse potenti. Praesent eget diam quam, vitae viverra arcu. Suspendisse posuere nisl leo. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Vivamus pretium facilisis suscipit. Ut ac nunc nisl, id elementum erat. Ut lacinia consectetur augue dignissim hendrerit. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque et diam dictum lacus ullamcorper convallis. '
						   ,100
						   ,1
						   ,9.99
						   ,'GBP'
						  , GETDATE()
						   ,GETDATE()
						   ,'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin tempus vulputate urna, eu euismod ipsum ornare dignissim. Morbi hendrerit justo in felis varius volutpat. Nunc ut tellus lacus. Etiam aliquam, nibh eu feugiat porta, ante arcu condimentum metus, non elementum tortor felis id eros. Curabitur venenatis bibendum orci, vitae viverra dolor pulvinar vitae. Sed id purus est, in dignissim mi. Vivamus pretium dolor vitae dolor suscipit vel convallis nisi varius. Etiam sagittis ante non velit fringilla rutrum. Nunc eleifend fringilla consequat. Suspendisse nibh sapien, consequat eget aliquam ac, lacinia in ipsum. '
						   ,'Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Curabitur elementum nibh eu enim malesuada cursus. Vivamus id volutpat felis. Nunc quis risus vitae urna varius tempor sit amet ac urna. Aenean orci odio, scelerisque bibendum pellentesque et, dapibus nec elit. Phasellus sollicitudin ante eget sapien pretium hendrerit. Pellentesque libero tortor, sagittis vitae porttitor elementum, convallis ac nisl. Ut vitae eros in justo varius suscipit. Vivamus lobortis felis quis orci tincidunt nec scelerisque tortor malesuada. Nullam a facilisis diam. Duis id ultrices metus. Vestibulum odio ligula, eleifend laoreet interdum sit amet, lacinia nec elit. Fusce bibendum ipsum et eros varius condimentum. Morbi vehicula, odio eu tincidunt iaculis, mi risus interdum dui, ut rutrum nulla dui eu neque. Sed rutrum mollis ultricies. Duis eget est quam, a pharetra dui. Curabitur congue, elit et sagittis cursus, lectus mi suscipit tortor, quis egestas sapien nunc in libero. '
						   ,''
						   ,'')

DECLARE @PRODUCT_ID int
SET @PRODUCT_ID = @@identity

/* handle related id's */
IF @RelatedID_1 = 0 
	SET @RelatedID_1 = @@IDENTITY

ELSE IF @RelatedID_2 = 0 
	SET @RelatedID_2 = @@IDENTITY
ELSE IF @RelatedID_3 = 0 
	SET @RelatedID_3 = @@IDENTITY



			  INSERT INTO [ECommerce_ProductCategory]
			   ([categoryID]
			   ,[productID]
			   ,[sortOrder]
			   ,[inserttimestamp]
			   ,[updatetimestamp])
			   VALUES
			   (@CAT_ID
			   ,@@identity
			   ,@intFlag
			   ,GETDATE()
			   ,GETDATE())

/* product images */
				INSERT INTO [ECommerce_ProductImage]
						   ([productID]
						   ,[imageUrl]
						   ,[width]
						   ,[height]
						   ,[altText]
						   ,[imageType]
						   ,[inserttimestamp]
						   ,[updatetimestamp])
					 VALUES
						   (@PRODUCT_ID
						   ,'/Modules/ECommerce/cuyahoga-logo-only.png'
						   ,172
						   ,60
						   ,'Cuyahoga Logo Image'
						   ,1
						   ,GETDATE()
						   ,GETDATE())

				INSERT INTO [ECommerce_ProductImage]
						   ([productID]
						   ,[imageUrl]
						   ,[width]
						   ,[height]
						   ,[altText]
						   ,[imageType]
						   ,[inserttimestamp]
						   ,[updatetimestamp])
					 VALUES
						   (@PRODUCT_ID
						   ,'/Modules/ECommerce/Images/blackberry8100_1.jpg'
						   ,172
						   ,60
						   ,'blackberry  Image'
						   ,2
						   ,GETDATE()
						   ,GETDATE())

				INSERT INTO [ECommerce_ProductImage]
						   ([productID]
						   ,[imageUrl]
						   ,[width]
						   ,[height]
						   ,[altText]
						   ,[imageType]
						   ,[inserttimestamp]
						   ,[updatetimestamp])
					 VALUES
						   (@PRODUCT_ID
						   ,'/Modules/ECommerce/Images/best_selling_img04.jpg'
						   ,172
						   ,60
						   ,'laptop image'
						   ,2
						   ,GETDATE()
						   ,GETDATE())


						/* product documents */

						INSERT INTO [Ecommerce_ProductDocument]
						   ([ProductID]
						   ,[DocumentID]
						   ,[updatetimestamp]
						   ,[inserttimestamp])
						VALUES
						   (@PRODUCT_ID
						   ,@DOCUMENT_ID_1
						   ,GETDATE()
						   ,GETDATE())

						INSERT INTO [Ecommerce_ProductDocument]
						   ([ProductID]
						   ,[DocumentID]
						   ,[updatetimestamp]
						   ,[inserttimestamp])
						VALUES
						   (@PRODUCT_ID
						   ,@DOCUMENT_ID_2
						   ,GETDATE()
						   ,GETDATE())

						INSERT INTO [Ecommerce_ProductDocument]
						   ([ProductID]
						   ,[DocumentID]
						   ,[updatetimestamp]
						   ,[inserttimestamp])
						VALUES
						   (@PRODUCT_ID
						   ,@DOCUMENT_ID_3
						   ,GETDATE()
						   ,GETDATE())


					/* Related products */
				IF @RelatedID_3 <> 0 

BEGIN
					INSERT INTO [ECommerce_ProductRelation]
						   ([productID]
						   ,[parentID]
						   ,[relationTypeID]
						   ,[inserttimestamp]
						   ,[updatetimestamp])
					 VALUES
						   (@RelatedID_3
						   ,@PRODUCT_ID
						   ,1
						   ,GETDATE()
						   ,GETDATE())

					INSERT INTO [ECommerce_ProductRelation]
						   ([productID]
						   ,[parentID]
						   ,[relationTypeID]
						   ,[inserttimestamp]
						   ,[updatetimestamp])
					 VALUES
						   (@RelatedID_2
						   ,@PRODUCT_ID
						   ,1
						   ,GETDATE()
						   ,GETDATE())

					INSERT INTO [ECommerce_ProductRelation]
						   ([productID]
						   ,[parentID]
						   ,[relationTypeID]
						   ,[inserttimestamp]
						   ,[updatetimestamp])
					 VALUES
						   (@RelatedID_1
						   ,@PRODUCT_ID
						   ,1
						   ,GETDATE()
						   ,GETDATE())
				END
	SET @intFlag = @intFlag + 1
	END

SET @catFlag = @catFlag + 1
END
GO


