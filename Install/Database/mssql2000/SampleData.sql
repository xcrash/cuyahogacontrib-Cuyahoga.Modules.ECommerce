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
           ('Test Category ' +  CAST(newid() AS nVARCHAR(4000))
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
						   ,'Test Product ' +  CAST(newid() AS nVARCHAR(4000))
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

				SET @intFlag = @intFlag + 1
				END

SET @catFlag = @catFlag + 1
END
GO


