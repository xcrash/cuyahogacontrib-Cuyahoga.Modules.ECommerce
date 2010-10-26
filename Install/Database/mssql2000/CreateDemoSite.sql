INSERT INTO [dbo].[cuyahoga_template]
		           ([name]
		           ,[basepath]
		           ,[templatecontrol]
		           ,[css]
		           ,[inserttimestamp]
		           ,[updatetimestamp])
		     VALUES
		           ('ECommerceTemplate'
		           ,'Templates/ECommerce'
		           ,'Cuyahoga.ascx'
		           ,'ECommerce.css'
		           ,GETDATE()
           		   ,GETDATE())
           		   
           	 DECLARE @TEMPLATE_ID int
	         SET @TEMPLATE_ID = @@identity
           		   
		   INSERT INTO [cuyahoga_site]
			      ([TemplateId]
				  ,[roleid]
			      ,[name]
			      ,[homeurl]
			      ,[defaultculture]
			      ,[defaultplaceholder]
			      ,[webmasteremail]
			      ,[usefriendlyurls]
			      ,[metakeywords]
			      ,[metadescription]
			      ,[inserttimestamp]
			      ,[updatetimestamp])
			VALUES
			      (@TEMPLATE_ID
					,1
			      ,'ECommerceDemoSite'
			      ,'http://localhost/leeCommerce'
			      ,'en-GB'
			      ,'plhNodes'
			      ,'leethomascook@hotmail.co.uk'
			      ,'true'
			      ,'Cuyahoga ECommerce Demo Site'
			      ,'Cuyahoga ECommerce Demo Site, Lee Cook, leethomascook@hotmail.co.uk'
			      ,GETDATE()
           		      ,GETDATE())
           		      
     		 DECLARE @SITE_ID int
	         SET @SITE_ID = @@identity
	         
	         /* create pages */
	         
	         /*Homepage */
	         INSERT INTO [cuyahoga_node]
		            ([parentnodeid]
		            ,[templateid]
		            ,[siteid]
		            ,[title]
		            ,[shortdescription]
		            ,[position]
		            ,[culture]
		            ,[showinnavigation]
		            ,[linkurl]
		            ,[linktarget]
		            ,[metakeywords]
		            ,[metadescription]
		            ,[inserttimestamp]
		            ,[updatetimestamp])
		      VALUES
		            (NULL
		            ,@TEMPLATE_ID
		            ,@SITE_ID
		            ,'Home'
		            ,'Home'
		            ,0
		            ,'en-GB'
		            ,'true'
		            ,NULL
		            ,0
		            ,NULL
		            ,NULL
		            ,GETDATE()
           		    ,GETDATE())
           		    
           	 DECLARE @HOMEPAGE_ID int
	         SET @HOMEPAGE_ID = @@identity
	         
	         /*Basket */
		 INSERT INTO [cuyahoga_node]
			    ([parentnodeid]
			    ,[templateid]
			    ,[siteid]
			    ,[title]
			    ,[shortdescription]
			    ,[position]
			    ,[culture]
			    ,[showinnavigation]
			    ,[linkurl]
			    ,[linktarget]
			    ,[metakeywords]
			    ,[metadescription]
			    ,[inserttimestamp]
			    ,[updatetimestamp])
		      VALUES
			    (@HOMEPAGE_ID
			    ,@TEMPLATE_ID
			    ,@SITE_ID
			    ,'Basket'
			    ,'Basket'
			    ,0
			    ,'en-GB'
			    ,'true'
			    ,NULL
			    ,0
			    ,NULL
			    ,NULL
			    ,GETDATE()
			    ,GETDATE())

		 DECLARE @BASKET_ID int
	         SET @BASKET_ID = @@identity
	         
	          /*Checkout */
		 INSERT INTO [cuyahoga_node]
			    ([parentnodeid]
			    ,[templateid]
			    ,[siteid]
			    ,[title]
			    ,[shortdescription]
			    ,[position]
			    ,[culture]
			    ,[showinnavigation]
			    ,[linkurl]
			    ,[linktarget]
			    ,[metakeywords]
			    ,[metadescription]
			    ,[inserttimestamp]
			    ,[updatetimestamp])
		      VALUES
			    (@HOMEPAGE_ID
			    ,@TEMPLATE_ID
			    ,@SITE_ID
			    ,'Checkout'
			    ,'Checkout'
			    ,0
			    ,'en-GB'
			    ,'true'
			    ,NULL
			    ,0
			    ,NULL
			    ,NULL
			    ,GETDATE()
			    ,GETDATE())

		 DECLARE @CHECKOUT_ID int
	         SET @CHECKOUT_ID = @@identity
	         
	          /*Catalogue */
		 INSERT INTO [cuyahoga_node]
			    ([parentnodeid]
			    ,[templateid]
			    ,[siteid]
			    ,[title]
			    ,[shortdescription]
			    ,[position]
			    ,[culture]
			    ,[showinnavigation]
			    ,[linkurl]
			    ,[linktarget]
			    ,[metakeywords]
			    ,[metadescription]
			    ,[inserttimestamp]
			    ,[updatetimestamp])
		      VALUES
			    (@HOMEPAGE_ID
			    ,@TEMPLATE_ID
			    ,@SITE_ID
			    ,'Catalogue'
			    ,'Catalogue'
			    ,0
			    ,'en-GB'
			    ,'true'
			    ,NULL
			    ,0
			    ,NULL
			    ,NULL
			    ,GETDATE()
			    ,GETDATE())

		 DECLARE @CATALOGUE_ID int
	         SET @CATALOGUE_ID = @@identity
	         
	            /*Register */
		 INSERT INTO [cuyahoga_node]
			    ([parentnodeid]
			    ,[templateid]
			    ,[siteid]
			    ,[title]
			    ,[shortdescription]
			    ,[position]
			    ,[culture]
			    ,[showinnavigation]
			    ,[linkurl]
			    ,[linktarget]
			    ,[metakeywords]
			    ,[metadescription]
			    ,[inserttimestamp]
			    ,[updatetimestamp])
		      VALUES
			    (@HOMEPAGE_ID
			    ,@TEMPLATE_ID
			    ,@SITE_ID
			    ,'Register'
			    ,'Register'
			    ,0
			    ,'en-GB'
			    ,'true'
			    ,NULL
			    ,0
			    ,NULL
			    ,NULL
			    ,GETDATE()
			    ,GETDATE())

		 DECLARE @REGISTER_ID int
	         SET @REGISTER_ID = @@identity
	         
		 /*Account */
		 INSERT INTO [cuyahoga_node]
			    ([parentnodeid]
			    ,[templateid]
			    ,[siteid]
			    ,[title]
			    ,[shortdescription]
			    ,[position]
			    ,[culture]
			    ,[showinnavigation]
			    ,[linkurl]
			    ,[linktarget]
			    ,[metakeywords]
			    ,[metadescription]
			    ,[inserttimestamp]
			    ,[updatetimestamp])
		      VALUES
			    (@HOMEPAGE_ID
			    ,@TEMPLATE_ID
			    ,@SITE_ID
			    ,'Account'
			    ,'Account'
			    ,0
			    ,'en-GB'
			    ,'true'
			    ,NULL
			    ,0
			    ,NULL
			    ,NULL
			    ,GETDATE()
			    ,GETDATE())

		 DECLARE @ACCOUNT_ID int
	         SET @ACCOUNT_ID = @@identity
	         
	         /* define Module id */
	         
	          DECLARE @MODULE_TYPE_ID int
		  SET @MODULE_TYPE_ID = (Select moduletypeid from cuyahoga_moduletype where name = 'ECommerce')
	        
	         /* Insert Node Roles */
	         
	         INSERT INTO [cuyahoga_noderole]
		            ([nodeid]
		            ,[roleid]
		            ,[viewallowed]
		            ,[editallowed])
		      VALUES
		            (@ACCOUNT_ID
		            ,1
		            ,1
		            ,1)
		 
		 INSERT INTO [cuyahoga_noderole]
		            ([nodeid]
		            ,[roleid]
		            ,[viewallowed]
		            ,[editallowed])
		      VALUES
		            (@ACCOUNT_ID
		            ,2
		            ,1
		            ,1)
		 
		 
		 INSERT INTO [cuyahoga_noderole]
		            ([nodeid]
		            ,[roleid]
		            ,[viewallowed]
		            ,[editallowed])
		      VALUES
		            (@ACCOUNT_ID
		            ,3
		            ,1
		            ,0)
		 
		 
		 INSERT INTO [cuyahoga_noderole]
		            ([nodeid]
		            ,[roleid]
		            ,[viewallowed]
		            ,[editallowed])
		      VALUES
		            (@ACCOUNT_ID
		            ,4
		            ,1
           ,0)

INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@REGISTER_ID
           ,1
           ,1
           ,1)

INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@REGISTER_ID
           ,2
           ,1
           ,1)


INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@REGISTER_ID
           ,3
           ,1
           ,0)


INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@REGISTER_ID
           ,4
           ,1
           ,0)
           

INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@CATALOGUE_ID
           ,1
           ,1
           ,1)

INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@CATALOGUE_ID
           ,2
           ,1
           ,1)


INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@CATALOGUE_ID
           ,3
           ,1
           ,0)


INSERT INTO [cuyahoga_noderole]
           ([nodeid]
           ,[roleid]
           ,[viewallowed]
           ,[editallowed])
     VALUES
           (@CATALOGUE_ID
           ,4
           ,1
           ,0)
           
	           
	     
	     INSERT INTO [cuyahoga_noderole]
	                ([nodeid]
	                ,[roleid]
	                ,[viewallowed]
	                ,[editallowed])
	          VALUES
	                (@CHECKOUT_ID
	                ,1
	                ,1
	                ,1)
	     
	     INSERT INTO [cuyahoga_noderole]
	                ([nodeid]
	                ,[roleid]
	                ,[viewallowed]
	                ,[editallowed])
	          VALUES
	                (@CHECKOUT_ID
	                ,2
	                ,1
	                ,1)
	     
	     
	     INSERT INTO [cuyahoga_noderole]
	                ([nodeid]
	                ,[roleid]
	                ,[viewallowed]
	                ,[editallowed])
	          VALUES
	                (@CHECKOUT_ID
	                ,3
	                ,1
	                ,0)
	     
	     
	     INSERT INTO [cuyahoga_noderole]
	                ([nodeid]
	                ,[roleid]
	                ,[viewallowed]
	                ,[editallowed])
	          VALUES
	                (@CHECKOUT_ID
	                ,4
	                ,1
           ,0)
           
           
           
           INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_ID
	              ,1
	              ,1
	              ,1)
	   
	   INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_ID
	              ,2
	              ,1
	              ,1)
	   
	   
	   INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_ID
	              ,3
	              ,1
	              ,0)
	   
	   
	   INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_ID
	              ,4
	              ,1
           ,0)
           
           INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@HOMEPAGE_ID
	              ,1
	              ,1
	              ,1)
	   
	   INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@HOMEPAGE_ID
	              ,2
	              ,1
	              ,1)
	   
	   
	   INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@HOMEPAGE_ID
	              ,3
	              ,1
	              ,0)
	   
	   
	   INSERT INTO [cuyahoga_noderole]
	              ([nodeid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@HOMEPAGE_ID
	              ,4
	              ,1
           ,0)
	         /* Insert Sections */
	         
			 /*BASKET SECTION */
			 INSERT INTO [cuyahoga_section]
			            ([nodeid]
			            ,[moduletypeid]
			            ,[title]
			            ,[showtitle]
			            ,[placeholder]
			            ,[position]
			            ,[cacheduration]
			            ,[inserttimestamp]
			            ,[updatetimestamp]
			            ,[siteid])
			      VALUES
			            (@BASKET_ID
			            ,@MODULE_TYPE_ID
			            ,'Basket'
			            ,0
			            ,'maincontent'
			            ,0
			            ,0
			            ,GETDATE()
			            ,GETDATE()
			            ,@SITE_ID)
			 
			 DECLARE @BASKET_SECTION_ID int
			 SET @BASKET_SECTION_ID = @@identity
			 
			 INSERT INTO [cuyahoga_sectionsetting]
			            ([sectionid]
			            ,[name]
			            ,[value])
			      VALUES
			            (@BASKET_SECTION_ID
			            ,'DISPLAY_MODE'
			            ,'BasketView')
			 
			 INSERT INTO [cuyahoga_sectionrole]
			            ([sectionid]
			            ,[roleid]
			            ,[viewallowed]
			            ,[editallowed])
			      VALUES
			            (@BASKET_SECTION_ID
			            ,1
			            ,1
			            ,1)
			 
			 INSERT INTO [cuyahoga_sectionrole]
			            ([sectionid]
			            ,[roleid]
			            ,[viewallowed]
			            ,[editallowed])
			      VALUES
			            (@BASKET_SECTION_ID
			            ,2
			            ,1
			            ,1)
			 
			 INSERT INTO [cuyahoga_sectionrole]
			            ([sectionid]
			            ,[roleid]
			            ,[viewallowed]
			            ,[editallowed])
			      VALUES
			            (@BASKET_SECTION_ID
			            ,3
			            ,1
			            ,0)
			 
			 INSERT INTO [cuyahoga_sectionrole]
			            ([sectionid]
			            ,[roleid]
			            ,[viewallowed]
			            ,[editallowed])
			      VALUES
			            (@BASKET_SECTION_ID
			            ,4
			            ,1
          			    ,0)
           
           
		   /* Checkout section */
		   INSERT INTO [cuyahoga_section]
			      ([nodeid]
			      ,[moduletypeid]
			      ,[title]
			      ,[showtitle]
			      ,[placeholder]
			      ,[position]
			      ,[cacheduration]
			      ,[inserttimestamp]
			      ,[updatetimestamp]
			      ,[siteid])
			VALUES
			      (@CHECKOUT_ID
			      ,@MODULE_TYPE_ID
			      ,'Checkout'
			      ,0
			      ,'maincontent'
			      ,0
			      ,0
			      ,GETDATE()
			      ,GETDATE()
			      ,@SITE_ID)

		   DECLARE @CHECKOUT_SECTION_ID int
		   SET @CHECKOUT_SECTION_ID = @@identity

		   INSERT INTO [cuyahoga_sectionsetting]
			      ([sectionid]
			      ,[name]
			      ,[value])
			VALUES
			      (@CHECKOUT_SECTION_ID
			      ,'DISPLAY_MODE'
			      ,'Checkout')

		   INSERT INTO [cuyahoga_sectionrole]
			      ([sectionid]
			      ,[roleid]
			      ,[viewallowed]
			      ,[editallowed])
			VALUES
			      (@CHECKOUT_SECTION_ID
			      ,1
			      ,1
			      ,1)

		   INSERT INTO [cuyahoga_sectionrole]
			      ([sectionid]
			      ,[roleid]
			      ,[viewallowed]
			      ,[editallowed])
			VALUES
			      (@CHECKOUT_SECTION_ID
			      ,2
			      ,1
			      ,1)

		   INSERT INTO [cuyahoga_sectionrole]
			      ([sectionid]
			      ,[roleid]
			      ,[viewallowed]
			      ,[editallowed])
			VALUES
			      (@CHECKOUT_SECTION_ID
			      ,3
			      ,1
			      ,0)

		   INSERT INTO [cuyahoga_sectionrole]
			      ([sectionid]
			      ,[roleid]
			      ,[viewallowed]
			      ,[editallowed])
			VALUES
			      (@CHECKOUT_SECTION_ID
			      ,4
			      ,1
		   ,0)
		   
		   /* CatNav section */
		   INSERT INTO [cuyahoga_section]
		              ([nodeid]
		              ,[moduletypeid]
		              ,[title]
		              ,[showtitle]
		              ,[placeholder]
		              ,[position]
		              ,[cacheduration]
		              ,[inserttimestamp]
		              ,[updatetimestamp]
		              ,[siteid])
		        VALUES
		              (@CATALOGUE_ID
		              ,@MODULE_TYPE_ID
		              ,'Catalogue'
		              ,0
		              ,'maincontent'
		              ,0
		              ,0
		              ,GETDATE()
		              ,GETDATE()
		              ,@SITE_ID)
		   
		   DECLARE @CATALOGUE_SECTION_ID int
		   SET @CATALOGUE_SECTION_ID = @@identity
		   
		   INSERT INTO [cuyahoga_sectionsetting]
		              ([sectionid]
		              ,[name]
		              ,[value])
		        VALUES
		              (@CATALOGUE_SECTION_ID
		              ,'DISPLAY_MODE'
		              ,'CatNav')
		   
		   INSERT INTO [cuyahoga_sectionrole]
		              ([sectionid]
		              ,[roleid]
		              ,[viewallowed]
		              ,[editallowed])
		        VALUES
		              (@CATALOGUE_SECTION_ID
		              ,1
		              ,1
		              ,1)
		   
		   INSERT INTO [cuyahoga_sectionrole]
		              ([sectionid]
		              ,[roleid]
		              ,[viewallowed]
		              ,[editallowed])
		        VALUES
		              (@CATALOGUE_SECTION_ID
		              ,2
		              ,1
		              ,1)
		   
		   INSERT INTO [cuyahoga_sectionrole]
		              ([sectionid]
		              ,[roleid]
		              ,[viewallowed]
		              ,[editallowed])
		        VALUES
		              (@CATALOGUE_SECTION_ID
		              ,3
		              ,1
		              ,0)
		   
		   INSERT INTO [cuyahoga_sectionrole]
		              ([sectionid]
		              ,[roleid]
		              ,[viewallowed]
		              ,[editallowed])
		        VALUES
		              (@CATALOGUE_SECTION_ID
		              ,4
		              ,1
           ,0)
	         
	        
	    
	    /*Register section */
	    INSERT INTO [cuyahoga_section]
	               ([nodeid]
	               ,[moduletypeid]
	               ,[title]
	               ,[showtitle]
	               ,[placeholder]
	               ,[position]
	               ,[cacheduration]
	               ,[inserttimestamp]
	               ,[updatetimestamp]
	               ,[siteid])
	         VALUES
	               (@REGISTER_ID
	               ,@MODULE_TYPE_ID
	               ,'Register'
	               ,0
	               ,'maincontent'
	               ,0
	               ,0
	               ,GETDATE()
	               ,GETDATE()
	               ,@SITE_ID)
	    
	    DECLARE @REGISTER_SECTION_ID int
	    SET @REGISTER_SECTION_ID = @@identity
	    
	    INSERT INTO [cuyahoga_sectionsetting]
	               ([sectionid]
	               ,[name]
	               ,[value])
	         VALUES
	               (@REGISTER_SECTION_ID
	               ,'DISPLAY_MODE'
	               ,'Register')
	    
	    INSERT INTO [cuyahoga_sectionrole]
	               ([sectionid]
	               ,[roleid]
	               ,[viewallowed]
	               ,[editallowed])
	         VALUES
	               (@REGISTER_SECTION_ID
	               ,1
	               ,1
	               ,1)
	    
	    INSERT INTO [cuyahoga_sectionrole]
	               ([sectionid]
	               ,[roleid]
	               ,[viewallowed]
	               ,[editallowed])
	         VALUES
	               (@REGISTER_SECTION_ID
	               ,2
	               ,1
	               ,1)
	    
	    INSERT INTO [cuyahoga_sectionrole]
	               ([sectionid]
	               ,[roleid]
	               ,[viewallowed]
	               ,[editallowed])
	         VALUES
	               (@REGISTER_SECTION_ID
	               ,3
	               ,1
	               ,0)
	    
	    INSERT INTO [cuyahoga_sectionrole]
	               ([sectionid]
	               ,[roleid]
	               ,[viewallowed]
	               ,[editallowed])
	         VALUES
	               (@REGISTER_SECTION_ID
	               ,4
	               ,1
           ,0)
           
           
           /*MyAccount section */
           
           INSERT INTO [cuyahoga_section]
	              ([nodeid]
	              ,[moduletypeid]
	              ,[title]
	              ,[showtitle]
	              ,[placeholder]
	              ,[position]
	              ,[cacheduration]
	              ,[inserttimestamp]
	              ,[updatetimestamp]
	              ,[siteid])
	        VALUES
	              (@ACCOUNT_ID
	              ,@MODULE_TYPE_ID
	              ,'MyAccount'
	              ,0
	              ,'maincontent'
	              ,0
	              ,0
	              ,GETDATE()
	              ,GETDATE()
	              ,@SITE_ID)
	   
	   DECLARE @ACCOUNT_SECTION_ID int
	   SET @ACCOUNT_SECTION_ID = @@identity
	   
	   INSERT INTO [cuyahoga_sectionsetting]
	              ([sectionid]
	              ,[name]
	              ,[value])
	        VALUES
	              (@ACCOUNT_SECTION_ID
	              ,'DISPLAY_MODE'
	              ,'Account')
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@ACCOUNT_SECTION_ID
	              ,1
	              ,1
	              ,1)
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@ACCOUNT_SECTION_ID
	              ,2
	              ,1
	              ,1)
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@ACCOUNT_SECTION_ID
	              ,3
	              ,1
	              ,0)
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@ACCOUNT_SECTION_ID
	              ,4
	              ,1
           ,0)
           
           /* Standalone sections */
           
           /*Mini Basket */
           
           INSERT INTO [cuyahoga_section]
	              ([nodeid]
	              ,[moduletypeid]
	              ,[title]
	              ,[showtitle]
	              ,[placeholder]
	              ,[position]
	              ,[cacheduration]
	              ,[inserttimestamp]
	              ,[updatetimestamp]
	              ,[siteid])
	        VALUES
	              (NULL
	              ,@MODULE_TYPE_ID
	              ,'Basket'
	              ,0
	              ,NULL
	              ,0
	              ,0
	              ,GETDATE()
	              ,GETDATE()
	              ,@SITE_ID)
	   
	   DECLARE @BASKET_SUMMARY_SECTION_ID int
	   SET @BASKET_SUMMARY_SECTION_ID = @@identity
	   
	   INSERT INTO [cuyahoga_sectionsetting]
	              ([sectionid]
	              ,[name]
	              ,[value])
	        VALUES
	              (@BASKET_SUMMARY_SECTION_ID
	              ,'DISPLAY_MODE'
	              ,'BasketSummary')
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_SUMMARY_SECTION_ID
	              ,1
	              ,1
	              ,1)
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_SUMMARY_SECTION_ID
	              ,2
	              ,1
	              ,1)
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_SUMMARY_SECTION_ID
	              ,3
	              ,1
	              ,0)
	   
	   INSERT INTO [cuyahoga_sectionrole]
	              ([sectionid]
	              ,[roleid]
	              ,[viewallowed]
	              ,[editallowed])
	        VALUES
	              (@BASKET_SUMMARY_SECTION_ID
	              ,4
	              ,1
	              ,0)
	   
	   INSERT INTO [cuyahoga_templatesection]
	              ([templateid]
	              ,[sectionid]
	              ,[placeholder])
	        VALUES
	              (@TEMPLATE_ID
	              ,@BASKET_SUMMARY_SECTION_ID
           	     ,'side1content')
           	     
           	     /* Navigation menu */
           	     
           	     
           	     INSERT INTO [cuyahoga_section]
		                ([nodeid]
		                ,[moduletypeid]
		                ,[title]
		                ,[showtitle]
		                ,[placeholder]
		                ,[position]
		                ,[cacheduration]
		                ,[inserttimestamp]
		                ,[updatetimestamp]
		                ,[siteid])
		          VALUES
		                (NULL
		                ,@MODULE_TYPE_ID
		                ,'Category Menu'
		                ,0
		                ,NULL
		                ,0
		                ,0
		                ,GETDATE()
		                ,GETDATE()
		                ,@SITE_ID)
		     
		     DECLARE @NAVIGATION_SECTION_ID int
		     SET @NAVIGATION_SECTION_ID = @@identity
		     
		     INSERT INTO [cuyahoga_sectionsetting]
		                ([sectionid]
		                ,[name]
		                ,[value])
		          VALUES
		                (@NAVIGATION_SECTION_ID
		                ,'DISPLAY_MODE'
		                ,'Navigation')
		     
		     INSERT INTO [cuyahoga_sectionrole]
		                ([sectionid]
		                ,[roleid]
		                ,[viewallowed]
		                ,[editallowed])
		          VALUES
		                (@NAVIGATION_SECTION_ID
		                ,1
		                ,1
		                ,1)
		     
		     INSERT INTO [cuyahoga_sectionrole]
		                ([sectionid]
		                ,[roleid]
		                ,[viewallowed]
		                ,[editallowed])
		          VALUES
		                (@NAVIGATION_SECTION_ID
		                ,2
		                ,1
		                ,1)
		     
		     INSERT INTO [cuyahoga_sectionrole]
		                ([sectionid]
		                ,[roleid]
		                ,[viewallowed]
		                ,[editallowed])
		          VALUES
		                (@NAVIGATION_SECTION_ID
		                ,3
		                ,1
		                ,0)
		     
		     INSERT INTO [cuyahoga_sectionrole]
		                ([sectionid]
		                ,[roleid]
		                ,[viewallowed]
		                ,[editallowed])
		          VALUES
		                (@NAVIGATION_SECTION_ID
		                ,4
		                ,1
		                ,0)
		     
		     INSERT INTO [cuyahoga_templatesection]
		                ([templateid]
		                ,[sectionid]
		                ,[placeholder])
		          VALUES
		                (@TEMPLATE_ID
		                ,@NAVIGATION_SECTION_ID
           			,'navigationMenu')
	         