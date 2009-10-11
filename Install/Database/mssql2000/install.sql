-- *********************************************************************
-- Update Database Script
-- *********************************************************************
-- Change Log: master.xml
-- Ran at: 11/10/09 18:07
-- Against: cuyahoga@jdbc:sqlserver://localhost:1433;responseBuffering=full;encrypt=false;databaseName=CuyahogaECommerceDemov2;selectMethod=direct;trustServerCertificate=false;lastUpdateCount=true;
-- LiquiBase version: 1.8.0
-- *********************************************************************

-- Changeset initial.xml::1255116619640-37::Lee (generated)::(MD5Sum: 9226ac261a2da0c9328d47922a51)
CREATE TABLE [ECommerce_Address] (addressID BIGINT IDENTITY  NOT NULL, isDeleted bit CONSTRAINT DF_ECommerce_Address_isDeleted DEFAULT 0 NOT NULL, contactName nvarchar(128) NOT NULL, addressLine1 nvarchar(50) NOT NULL, addressLine2 nvarchar(50) NOT NULL, addressLine3 nvarchar(50) NOT NULL, state nvarchar(50) NOT NULL, countryCode char(2) NOT NULL, postCode nvarchar(50) NOT NULL, county nvarchar(50) NOT NULL, stateID smallint, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_Address_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_Address_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_address] PRIMARY KEY (addressID));
GO



-- Changeset initial.xml::1255116619640-38::Lee (generated)::(MD5Sum: fce413c5f384d79c95281aa337ec475c)
CREATE TABLE [Ecommerce_Attribute] (attributeID BIGINT IDENTITY  NOT NULL, attributeReference nvarchar(50) NOT NULL, attributeDescription nvarchar(128) NOT NULL, isDisplayable bit, baseUnit nvarchar(50), TypeID int, CONSTRAINT [PK_attributes] PRIMARY KEY (attributeID));
GO



-- Changeset initial.xml::1255116619640-39::Lee (generated)::(MD5Sum: bda5ce7973cb686145fff98966ae37b)
CREATE TABLE [Ecommerce_AttributeGroup] (attributeGroupID smallint IDENTITY  NOT NULL, attributeGroupName nvarchar(50) NOT NULL, CONSTRAINT [PK_AttributeGroups_1] PRIMARY KEY (attributeGroupID));
GO



-- Changeset initial.xml::1255116619640-40::Lee (generated)::(MD5Sum: 2a867cacfe24cea36a57c46332ca4716)
CREATE TABLE [Ecommerce_AttributeGroupAttribute] (attributeGroupID smallint NOT NULL, attributeID int NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_Ecommerce_AttributeGroupAttribute_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_Ecommerce_AttributeGroupAttribute_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-41::Lee (generated)::(MD5Sum: 244d32abfddf39ae79386571686e048)
CREATE TABLE [Ecommerce_AttributeOptionValue] (attributeID BIGINT NOT NULL, optionID BIGINT IDENTITY  NOT NULL, optionName nvarchar(150) NOT NULL, optionData nvarchar(50), CONSTRAINT [PK_Ecommerce_AttributeOption] PRIMARY KEY (optionID));
GO



-- Changeset initial.xml::1255116619640-42::Lee (generated)::(MD5Sum: d2445de1a5d7de60237b99db8b79967)
CREATE TABLE [Ecommerce_AttributeType] (TypeID int IDENTITY  NOT NULL, Name nvarchar(50) NOT NULL, CONSTRAINT [PK_EcommerceAttributeType] PRIMARY KEY (TypeID));
GO


-- Changeset initial.xml::1255116619640-43::Lee (generated)::(MD5Sum: d0f16a6058505812312f43874c167e2)
CREATE TABLE [ECommerce_Basket] (basketID BIGINT IDENTITY  NOT NULL, orderHeaderID BIGINT, userID int, altUserID int, currencyCode char(3) NOT NULL, taxPrice decimal(18,4) NOT NULL, subtotalPrice decimal(18,4) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_Basket_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_Basket_updatetimestamp DEFAULT (getdate()) NOT NULL, deliveryCost decimal(18,0), CONSTRAINT [PK_Basket] PRIMARY KEY (basketID));
GO



-- Changeset initial.xml::1255116619640-44::Lee (generated)::(MD5Sum: 444d8dcb7173d4b617e1c95d8dee37b2)
CREATE TABLE [ECommerce_BasketItem] (basketItemID BIGINT IDENTITY  NOT NULL, basketID BIGINT NOT NULL, productID BIGINT, itemTax decimal(19,4) NOT NULL, linePrice decimal(19,4) NOT NULL, quantity int NOT NULL, itemTypeID smallint NOT NULL, pricingStatusID smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_BasketItem_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_BasketItem_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_BasketItem] PRIMARY KEY (basketItemID));
GO



-- Changeset initial.xml::1255116619640-45::Lee (generated)::(MD5Sum: a5d9309acca3745f924f901e80f7f7b0)
CREATE TABLE [ECommerce_BasketItemAttribute] (basketitemID BIGINT NOT NULL, attributeID int NOT NULL, optionValue char(10) NOT NULL, optionPrice BIGINT NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_BasketItemAttribute_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_BasketItemAttribute_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_BasketItemAttributes] PRIMARY KEY (basketitemID));
GO



-- Changeset initial.xml::1255116619640-46::Lee (generated)::(MD5Sum: dff596dce04b772253987a2fe3931db2)
CREATE TABLE [ECommerce_Category] (categoryID BIGINT IDENTITY  NOT NULL, categoryName nvarchar(128) NOT NULL, categoryDescription nvarchar(1024) CONSTRAINT DF_ECommerce_Category_categoryDescription DEFAULT '' NOT NULL, parentCategoryID BIGINT, sortOrder smallint NOT NULL, isPublished bit CONSTRAINT DF_ECommerce_Category_isPublished DEFAULT 0 NOT NULL, imageUrl varchar(128), width smallint, height smallint, altText nvarchar(128), updateTimeStamp DATETIME CONSTRAINT DF_ECommerce_Category_updateTimeStamp DEFAULT (getdate()) NOT NULL, insertTimeStamp DATETIME CONSTRAINT DF_ECommerce_Category_insertTimeStamp DEFAULT (getdate()) NOT NULL, cssClass varchar(128), KitDescription nvarchar(128), KitPicture nvarchar(128), priceChangePercent decimal(18,2), flashAnimationQuality nvarchar(128), flashAnimationUrl nvarchar(128), flashAnimationWidth smallint, flashAnimationAltText nvarchar(128), flashAnimationHeight smallint, flashAnimationBackgroundColour nvarchar(7), tylosandimageurl nvarchar(255), bannerImageUrl varchar(50), CONSTRAINT [PK_ProductCategory] PRIMARY KEY (categoryID));
GO



-- Changeset initial.xml::1255116619640-47::Lee (generated)::(MD5Sum: 41805da1f816a8140f836db3c49b7c0)
CREATE TABLE [Ecommerce_CategoryLink] (categoryLinkID BIGINT IDENTITY  NOT NULL, Categoryid BIGINT NOT NULL, nodeID char(10) NOT NULL, ImageUrl nvarchar(50), Title nvarchar(50), CONSTRAINT [PK_Ecommerce_CategoryLink] PRIMARY KEY (categoryLinkID));
GO



-- Changeset initial.xml::1255116619640-48::Lee (generated)::(MD5Sum: 8cf7aec7ae28a25db520ef6d26c4ad)
CREATE TABLE [ECommerce_CategoryPriceChange] (categoryID BIGINT NOT NULL, priceChangePercent decimal(18,2) NOT NULL, CONSTRAINT [PK_ECommerce_CategoryPriceChange] PRIMARY KEY (categoryID));
GO


-- Changeset initial.xml::1255116619640-49::Lee (generated)::(MD5Sum: 714797b92a6eda72365254d4baa34)
CREATE TABLE [ECommerce_Charge] (ChargeID int IDENTITY  NOT NULL, ChargeName nvarchar(50) NOT NULL);
GO

-- Changeset initial.xml::1255116619640-50::Lee (generated)::(MD5Sum: 2c8085c54a7e53799cfe14beb1ce3b)
CREATE TABLE [ECommerce_Country] (countryCode char(2) NOT NULL, countryName nvarchar(128) NOT NULL, defaultCurrencyCode char(3) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_Country_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_Country_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_Countries] PRIMARY KEY (countryCode));
GO



-- Changeset initial.xml::1255116619640-51::Lee (generated)::(MD5Sum: 4a2ae67237d5cbdde602914d8662bed)
CREATE TABLE [ECommerce_CountryCharge] (CountryCode char(2) NOT NULL, ChargeID int NOT NULL, Price decimal(18,4) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-52::Lee (generated)::(MD5Sum: f1670bc5e40c7d5779597bb738a1)
CREATE TABLE [ECommerce_CountryDeliveryState] (stateID smallint NOT NULL, price decimal(18,4) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_CountryDeliveryState_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_CountryDeliveryState_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_CountryDeliveryState] PRIMARY KEY (stateID));
GO



-- Changeset initial.xml::1255116619640-53::Lee (generated)::(MD5Sum: 90536e4f82a584dfe5c6fe9cc5b5a)
CREATE TABLE [ECommerce_CountryDeliveryStateWeight] (stateID smallint NOT NULL, weightLevel decimal(5,2) NOT NULL, price decimal(18,4) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_CountryDeliveryStateWeight_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_CountryDeliveryStateWeight_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-54::Lee (generated)::(MD5Sum: aa1d1a508d523e4d26beb6a58161a13)
CREATE TABLE [ECommerce_CountryDeliveryWeight] (countryCode char(2) NOT NULL, weightLevel decimal(5,2) NOT NULL, price decimal(18,4) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-55::Lee (generated)::(MD5Sum: ff617fcb5b395fcaacb7c4fb3839d)
CREATE TABLE [ECommerce_Currency] (currencyCode char(3) NOT NULL, exchangeRate decimal(18,4) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_Currency_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_Currency_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_Currency] PRIMARY KEY (currencyCode));
GO


-- Changeset initial.xml::1255116619640-56::Lee (generated)::(MD5Sum: 91e2ee8b413d285833ebc7f65628594c)
CREATE TABLE [ECommerce_DeliveryType] (deliveryTypeID smallint IDENTITY  NOT NULL, name varchar(50) NOT NULL, status bit NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_DeliveryType_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_DeliveryType_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_deliveryType] PRIMARY KEY (deliveryTypeID));
GO



-- Changeset initial.xml::1255116619640-57::Lee (generated)::(MD5Sum: ad70b6ca1efb81aad6dedd457cbbab9a)
CREATE TABLE [Ecommerce_Document] (DocumentID int IDENTITY  NOT NULL, DocumentName nvarchar(128) NOT NULL, FilePath nvarchar(1024) NOT NULL, TypeID int NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_Ecommerce_Document_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_Ecommerce_Document_updatetimestamp DEFAULT (getdate()) NOT NULL, isPublished bit NOT NULL, CONSTRAINT [PK_Ecommerce_Documents] PRIMARY KEY (DocumentID));
GO



-- Changeset initial.xml::1255116619640-58::Lee (generated)::(MD5Sum: 28e21149724c38f26eeaffced42cefbc)
CREATE TABLE [Ecommerce_DocumentType] (TypeID int IDENTITY  NOT NULL, TypeName nvarchar(50) NOT NULL, CssClass nvarchar(50), CONSTRAINT [PK_DocumentType] PRIMARY KEY (TypeID));
GO



-- Changeset initial.xml::1255116619640-59::Lee (generated)::(MD5Sum: a3a8b9c7d9dca380cca36916f2c5808c)
CREATE TABLE [Ecommerce_Kit] (KitID BIGINT IDENTITY  NOT NULL, _Productid BIGINT NOT NULL, categoryID BIGINT NOT NULL, CONSTRAINT [PK_Ecommerce_Kit] PRIMARY KEY (KitID));
GO



-- Changeset initial.xml::1255116619640-60::Lee (generated)::(MD5Sum: aa5b3ac64ea6e8c1ad78d773fca8d5)
CREATE TABLE [ECommerce_OrderHeader] (orderHeaderID BIGINT IDENTITY  NOT NULL, purchaseOrderNumber varchar(50) NOT NULL, orderStatusID smallint NOT NULL, orderedDate DATETIME NOT NULL, invoiceAddressID BIGINT, deliveryAddressID BIGINT, deliveryTypeID smallint, paymentMethodID smallint, comment nvarchar(512), inserttimestamp DATETIME CONSTRAINT DF_ECommerce_OrderHeader_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_OrderHeader_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_OrderHeader] PRIMARY KEY (orderHeaderID));
GO



-- Changeset initial.xml::1255116619640-61::Lee (generated)::(MD5Sum: 8b8a8a3fbae662a7f868dd358437d92)
CREATE TABLE [ECommerce_Payment] (paymentID BIGINT IDENTITY  NOT NULL, basketID BIGINT NOT NULL, paymentTypeID smallint NOT NULL, paymentStatusID smallint NOT NULL, paymentProviderID smallint NOT NULL, currencyCode char(3) NOT NULL, amount decimal(19,4) NOT NULL, transactionRef varchar(100) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_Payment_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_Payment_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_Payment] PRIMARY KEY (paymentID));
GO



-- Changeset initial.xml::1255116619640-62::Lee (generated)::(MD5Sum: f0d160ef2cbc56e8a7945d4f23342)
CREATE TABLE [ECommerce_PaymentProvider] (Id int NOT NULL, Name nvarchar(50) NOT NULL, ServiceName nvarchar(100) NOT NULL, CONSTRAINT [PK_ECommerce_PaymentProvider] PRIMARY KEY (Id));
GO



-- Changeset initial.xml::1255116619640-63::Lee (generated)::(MD5Sum: 62e9b49bc0889f4dfbbe49bfcd3a4546)
CREATE TABLE [ECommerce_Product] (productID BIGINT IDENTITY  NOT NULL, itemCode varchar(128) NOT NULL, productName nvarchar(128) NOT NULL, productDescription nvarchar(4000) NOT NULL, stockLevel int NOT NULL, isPublished bit NOT NULL, basePrice decimal(18,4) NOT NULL, baseCurrencyCode char(3), inserttimestamp DATETIME CONSTRAINT DF_ECommerce_Product_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_Product_updatetimestamp DEFAULT (getdate()) NOT NULL, additionalInformation nvarchar(1024), productfamily nvarchar(50), features nvarchar(1024), IsKit bit, basePriceDescription nvarchar(50), shortProductDescription nvarchar(512), CONSTRAINT [PK_products] PRIMARY KEY (productID));
GO


-- Changeset initial.xml::1255116619640-64::Lee (generated)::(MD5Sum: 3ff89ccb2f4def781ad6eaf77556583)
CREATE TABLE [ECommerce_ProductAttributeOptionValue] (productID BIGINT NOT NULL, optionValueID BIGINT NOT NULL, optionPrice decimal(19,4) NOT NULL, optionValueCode varchar(50) NOT NULL, sortOrder smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_ProductAttributeOptionValue_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_ProductAttributeOptionValue_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-65::Lee (generated)::(MD5Sum: dde9b780125dad2311c69d7f291bd37)
CREATE TABLE [ECommerce_ProductCategory] (categoryID BIGINT NOT NULL, productID BIGINT NOT NULL, sortOrder smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_ProductCategory_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_ProductCategory_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-66::Lee (generated)::(MD5Sum: 43e5241f8ea1e62235d092683dfd1136)
CREATE TABLE [Ecommerce_ProductDocument] (ProductID BIGINT NOT NULL, DocumentID int NOT NULL, updatetimestamp DATETIME, inserttimestamp DATETIME);
GO



-- Changeset initial.xml::1255116619640-67::Lee (generated)::(MD5Sum: 57ff5a678ce2cefda114bf093a2681f)
CREATE TABLE [ECommerce_ProductImage] (imageID int IDENTITY  NOT NULL, productID BIGINT NOT NULL, imageUrl varchar(128) NOT NULL, width smallint NOT NULL, height smallint NOT NULL, altText nvarchar(128) NOT NULL, imageType smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_ProductImage_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_ProductImage_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_ProductImage] PRIMARY KEY (imageID));
GO



-- Changeset initial.xml::1255116619640-68::Lee (generated)::(MD5Sum: 3c8d49065865bb6cd799cf882e2b2)
CREATE TABLE [ECommerce_ProductRelation] (productID BIGINT NOT NULL, parentID BIGINT NOT NULL, relationTypeID smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_ProductRelation_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_ProductRelation_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-69::Lee (generated)::(MD5Sum: 125232253a368fa71e859c58a86f680)
CREATE TABLE [ECommerce_ProductSKU] (sku varchar(128) NOT NULL, productID BIGINT NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_ProductSKU_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_ProductSKU_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_ProductSKU] PRIMARY KEY (sku));
GO



-- Changeset initial.xml::1255116619640-70::Lee (generated)::(MD5Sum: 423378ba81ef5c393a2b0ff3d709b7a)
CREATE TABLE [Ecommerce_ProductSynonym] (ProductID BIGINT NOT NULL, AlternativePhrase nvarchar(50) NOT NULL, inserttimestamp DATETIME NOT NULL, updatetimestamp DATETIME NOT NULL);
GO



-- Changeset initial.xml::1255116619640-71::Lee (generated)::(MD5Sum: f7e087ec38d36045b7b6c9507d5db19)
CREATE TABLE [ECommerce_ProductTaxClass] (productID BIGINT NOT NULL, taxClassID smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_ProductTaxClass_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_ProductTaxClass_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-72::Lee (generated)::(MD5Sum: a139684a7b50a555a5423dde785ee3b1)
CREATE TABLE [ECommerce_RelationType] (relationTypeID smallint NOT NULL, relationshipName nvarchar(128) NOT NULL, relationshipDescription nvarchar(1024), CONSTRAINT [PK_RelationType] PRIMARY KEY (relationTypeID));
GO



-- Changeset initial.xml::1255116619640-73::Lee (generated)::(MD5Sum: ae92732233c86818f7a944a5682dda7)
CREATE TABLE [ECommerce_State] (stateID smallint NOT NULL, countryCode char(2) NOT NULL, stateCode nvarchar(12) NOT NULL, stateName nvarchar(128) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_State_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_State_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_States] PRIMARY KEY (stateID));
GO


-- Changeset initial.xml::1255116619640-74::Lee (generated)::(MD5Sum: 4af38750e16826ba28465861909687)
CREATE TABLE [ECommerce_TaxClass] (taxClassID smallint NOT NULL, taxClassName varchar(50) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TaxClass_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TaxClass_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_TaxRates] PRIMARY KEY (taxClassID));
GO



-- Changeset initial.xml::1255116619640-75::Lee (generated)::(MD5Sum: d6782330b2de54578ac51956916219a)
CREATE TABLE [ECommerce_TaxZone] (taxZoneID smallint NOT NULL, taxZoneName nvarchar(128) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZone_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZone_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_TaxZone] PRIMARY KEY (taxZoneID));
GO



-- Changeset initial.xml::1255116619640-76::Lee (generated)::(MD5Sum: afff6e65fb26d775a9be75565f650d6)
CREATE TABLE [ECommerce_TaxZoneClassRate] (taxZoneID smallint NOT NULL, taxClassID smallint NOT NULL, taxRate decimal(18,4) NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZoneClassRate_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZoneClassRate_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO



-- Changeset initial.xml::1255116619640-77::Lee (generated)::(MD5Sum: 7d9fb4c3a71c55b4af728328d62e4eb)
CREATE TABLE [ECommerce_TaxZoneCountry] (countryCode char(2) NOT NULL, taxZoneID smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZoneCountry_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZoneCountry_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_TaxZoneCountry] PRIMARY KEY (countryCode));
GO



-- Changeset initial.xml::1255116619640-78::Lee (generated)::(MD5Sum: ad4bf9f3b333c47e1fc44e84a598b47b)
CREATE TABLE [ECommerce_TaxZoneState] (stateID smallint NOT NULL, taxZoneID smallint NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZoneState_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TaxZoneState_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_ECommerce_TaxZoneState] PRIMARY KEY (stateID));
GO


-- Changeset initial.xml::1255116619640-79::Lee (generated)::(MD5Sum: 472225beee5f28d1616eab784702e20)
CREATE TABLE [ECommerce_TranslationTag] (tagReference varchar(100) NOT NULL, tagID BIGINT NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TranslationTag_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TranslationTag_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_TranslationTags] PRIMARY KEY (tagID));
GO



-- Changeset initial.xml::1255116619640-80::Lee (generated)::(MD5Sum: cef8aa8f163e0256497881e95169e18)
CREATE TABLE [ECommerce_TranslationText] (tagID BIGINT NOT NULL, cultureCode varchar(5) NOT NULL, textValue ntext NOT NULL, inserttimestamp DATETIME CONSTRAINT DF_ECommerce_TranslationText_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_TranslationText_updatetimestamp DEFAULT (getdate()) NOT NULL);
GO


-- Changeset initial.xml::1255116619640-81::Lee (generated)::(MD5Sum: 4bc4e64f67f7b246b3175156877c68c)
CREATE TABLE [ECommerce_UserDetail] (userID int IDENTITY  NOT NULL, accountTypeID smallint NOT NULL, addressID BIGINT, firstName nvarchar(100), lastName nvarchar(100), emailAddress varchar(200), telephoneNumber varchar(100), companyName nvarchar(100), accountNumber varchar(100), inserttimestamp DATETIME CONSTRAINT DF_ECommerce_UserDetail_inserttimestamp DEFAULT (getdate()) NOT NULL, updatetimestamp DATETIME CONSTRAINT DF_ECommerce_UserDetail_updatetimestamp DEFAULT (getdate()) NOT NULL, CONSTRAINT [PK_UserDetails] PRIMARY KEY (userID));
GO



-- Changeset initial.xml::1255116619640-82::Lee (generated)::(MD5Sum: bb3db0dfb81e8c93a44e9178d051a08f)
CREATE TABLE [PaymentProvider] (Id int IDENTITY  NOT NULL, CONSTRAINT [PK_PaymentProvider] PRIMARY KEY (Id));
GO

-- Changeset initial.xml::1255116619640-83::Lee (generated)::(MD5Sum: 8c69c229dbbfad8afda5ec8eda9c8060)
ALTER TABLE [ECommerce_CountryCharge] ADD CONSTRAINT PK_ECommerce_CountryCharge PRIMARY KEY (CountryCode, ChargeID);
GO



-- Changeset initial.xml::1255116619640-84::Lee (generated)::(MD5Sum: a8d3175f7ea69d1dab36cd32fae6be)
ALTER TABLE [ECommerce_CountryDeliveryStateWeight] ADD CONSTRAINT PK_ECommerce_CountryDeliveryStateWeight PRIMARY KEY (stateID, weightLevel);
GO


-- Changeset initial.xml::1255116619640-85::Lee (generated)::(MD5Sum: 4b2ae2a5dc66c7c5e06d8f3316424c4f)
ALTER TABLE [ECommerce_CountryDeliveryWeight] ADD CONSTRAINT PK_ECommerce_CountryDeliveryWeight PRIMARY KEY (countryCode, weightLevel);
GO



-- Changeset initial.xml::1255116619640-86::Lee (generated)::(MD5Sum: 1860747e31a567d273b394b9b87e41a2)
ALTER TABLE [ECommerce_ProductAttributeOptionValue] ADD CONSTRAINT PK_ECommerce_ProductAttributeOptionValue PRIMARY KEY (productID, optionValueID);
GO



-- Changeset initial.xml::1255116619640-87::Lee (generated)::(MD5Sum: 2cf5e0283e61229c844a160edc54cde)
ALTER TABLE [ECommerce_ProductCategory] ADD CONSTRAINT PK_ProductCategories PRIMARY KEY (categoryID, productID);
GO



-- Changeset initial.xml::1255116619640-88::Lee (generated)::(MD5Sum: 40d123446349d59bbe1197185623b42)
ALTER TABLE [ECommerce_ProductRelation] ADD CONSTRAINT PK_ProductRelation PRIMARY KEY (productID, parentID, relationTypeID);
GO



-- Changeset initial.xml::1255116619640-89::Lee (generated)::(MD5Sum: 92cfd486684ecdf96b473eaed7997d0)
ALTER TABLE [ECommerce_ProductTaxClass] ADD CONSTRAINT PK_ECommerce_ProductTaxClass PRIMARY KEY (productID, taxClassID);
GO



-- Changeset initial.xml::1255116619640-90::Lee (generated)::(MD5Sum: 9235ce5b8da85255e07886056ec5d67)
ALTER TABLE [ECommerce_TaxZoneClassRate] ADD CONSTRAINT PK_ECommerce_TaxZoneClassRate PRIMARY KEY (taxZoneID, taxClassID);
GO


-- Changeset initial.xml::1255116619640-91::Lee (generated)::(MD5Sum: 21615aa72728f78b9279abe589c279)
ALTER TABLE [ECommerce_TranslationText] ADD CONSTRAINT PK_TranslationText PRIMARY KEY (cultureCode, tagID);
GO



-- Changeset initial.xml::1255116619640-92::Lee (generated)::(MD5Sum: aa4df18a58951d5856ab6ce6f24c44a0)
ALTER TABLE [Ecommerce_AttributeGroupAttribute] ADD CONSTRAINT PK_Ecommerce_AttributeGroupAttribute PRIMARY KEY (attributeGroupID, attributeID);
GO



-- Changeset initial.xml::1255116619640-93::Lee (generated)::(MD5Sum: 6fb2b1e3302080aeb4c3dcdd7d9784c)
ALTER TABLE [Ecommerce_ProductDocument] ADD CONSTRAINT PK_Ecommerce_ProductDocuments PRIMARY KEY (ProductID, DocumentID);
GO



-- Changeset initial.xml::1255116619640-94::Lee (generated)::(MD5Sum: e4a795cc6230ca289edd38e15078c531)
ALTER TABLE [Ecommerce_ProductSynonym] ADD CONSTRAINT PK_ProductSynonym PRIMARY KEY (ProductID, AlternativePhrase);
GO



-- Changeset initial.xml::1255116619640-96::Lee (generated)::(MD5Sum: 66d81fa14debe9296c42f453179f76)
CREATE UNIQUE INDEX IX_ECommerce_Product ON [ECommerce_Product](itemCode);
GO



-- Changeset initial.xml::1255116619640-97::Lee (generated)::(MD5Sum: 55f4ee85d13689696bf4194a5ef809f)
CREATE UNIQUE INDEX IX_ECommerce_State ON [ECommerce_State](countryCode, stateCode);
GO



-- Changeset initial.xml::1255116619640-98::Lee (generated)::(MD5Sum: 50f73a875a6b496b0c55787baa0a311)
CREATE UNIQUE INDEX IX_ECommerce_TranslationTag ON [ECommerce_TranslationTag](tagReference);
GO



-- Changeset initial.xml::1255116619640-99::Lee (generated)::(MD5Sum: bfd312404252d91a29fdd8c8b55a558c)
CREATE UNIQUE INDEX IX_Ecommerce_Attribute ON [Ecommerce_Attribute](attributeReference);
GO



-- Changeset initial.xml::1255116619640-100::Lee (generated)::(MD5Sum: b08f3a46c31ca3248e619a042ad95cc)
CREATE INDEX IX_Ecommerce_AttributeOptionValue ON [Ecommerce_AttributeOptionValue](attributeID, optionName);
GO

-- Changeset initial.xml::1255116619640-155::Lee (generated)::(MD5Sum: 4521ba9e532547fd8bd0f51ca6f9ed)
ALTER TABLE [ECommerce_Address] ADD CONSTRAINT FK_address_Countries FOREIGN KEY (countryCode) REFERENCES [ECommerce_Country](countryCode);
GO



-- Changeset initial.xml::1255116619640-156::Lee (generated)::(MD5Sum: 8adb4473397f1559d5aa8e4a52b120)
ALTER TABLE [ECommerce_Address] ADD CONSTRAINT FK_ECommerce_Address_ECommerce_State FOREIGN KEY (stateID) REFERENCES [ECommerce_State](stateID);
GO


-- Changeset initial.xml::1255116619640-157::Lee (generated)::(MD5Sum: 87cbd783ece4baa9902fddcf2abfd5d5)
ALTER TABLE [Ecommerce_Attribute] ADD CONSTRAINT FK_Ecommerce_Attribute_Ecommerce_AttributeType FOREIGN KEY (TypeID) REFERENCES [Ecommerce_AttributeType](TypeID);
GO



-- Changeset initial.xml::1255116619640-158::Lee (generated)::(MD5Sum: e58a952d476ffff45b89a8349b28b366)
ALTER TABLE [Ecommerce_AttributeGroupAttribute] ADD CONSTRAINT FK_Ecommerce_AttributeGroupAttribute_Ecommerce_AttributeGroup FOREIGN KEY (attributeGroupID) REFERENCES [Ecommerce_AttributeGroup](attributeGroupID);
GO



-- Changeset initial.xml::1255116619640-159::Lee (generated)::(MD5Sum: f56da533cfe3925c9154a5b165f5a7)
ALTER TABLE [Ecommerce_AttributeOptionValue] ADD CONSTRAINT FK_Ecommerce_AttributeOption_Ecommerce_Attribute FOREIGN KEY (attributeID) REFERENCES [Ecommerce_Attribute](attributeID);
GO



-- Changeset initial.xml::1255116619640-160::Lee (generated)::(MD5Sum: 47c026a8752eec3a3a610bfb63f5a1f)
ALTER TABLE [ECommerce_Basket] ADD CONSTRAINT FK_ECommerce_Basket_ECommerce_Currency FOREIGN KEY (currencyCode) REFERENCES [ECommerce_Currency](currencyCode);
GO


-- Changeset initial.xml::1255116619640-161::Lee (generated)::(MD5Sum: 9bdf5d45f53f2edf3d76fcce3bc31ec)
ALTER TABLE [ECommerce_Basket] ADD CONSTRAINT FK_ECommerce_Basket_cuyahoga_user FOREIGN KEY (userID) REFERENCES [cuyahoga_user](userid);
GO

-- Changeset initial.xml::1255116619640-162::Lee (generated)::(MD5Sum: fe71b0ece49ada7e827c9a92e4d1dbd)
ALTER TABLE [ECommerce_BasketItem] ADD CONSTRAINT FK_BasketItem_Basket FOREIGN KEY (basketID) REFERENCES [ECommerce_Basket](basketID);
GO


-- Changeset initial.xml::1255116619640-163::Lee (generated)::(MD5Sum: 364ac7a805dc04d12fe30c121f8627)
ALTER TABLE [ECommerce_BasketItem] ADD CONSTRAINT FK_BasketItem_Products1 FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-164::Lee (generated)::(MD5Sum: e33bb33705ca18877646e159d28cdfa)
ALTER TABLE [ECommerce_BasketItemAttribute] ADD CONSTRAINT FK_BasketItemAttributes_BasketItem FOREIGN KEY (basketitemID) REFERENCES [ECommerce_BasketItem](basketItemID);
GO



-- Changeset initial.xml::1255116619640-165::Lee (generated)::(MD5Sum: c77f18ce38ac1ae44526385be729cda7)
ALTER TABLE [ECommerce_Category] ADD CONSTRAINT FK_ECommerce_Category_ECommerce_Category FOREIGN KEY (parentCategoryID) REFERENCES [ECommerce_Category](categoryID);
GO

-- Changeset initial.xml::1255116619640-166::Lee (generated)::(MD5Sum: 3f60f144efc723312bac85993642c4e7)
ALTER TABLE [Ecommerce_CategoryLink] ADD CONSTRAINT FK_Ecommerce_CategoryLink_ECommerce_Category FOREIGN KEY (Categoryid) REFERENCES [ECommerce_Category](categoryID);
GO



-- Changeset initial.xml::1255116619640-167::Lee (generated)::(MD5Sum: cc7b1441def126d6ecdad1785259b4b)
ALTER TABLE [ECommerce_CategoryPriceChange] ADD CONSTRAINT FK_ECommerce_CategoryPriceChange_ECommerce_Category FOREIGN KEY (categoryID) REFERENCES [ECommerce_Category](categoryID);
GO

-- Changeset initial.xml::1255116619640-168::Lee (generated)::(MD5Sum: 4dd3ec1d3a4ef4c869bbaf9698fb7327)
ALTER TABLE [ECommerce_Country] ADD CONSTRAINT FK_ECommerce_Country_ECommerce_Currency FOREIGN KEY (defaultCurrencyCode) REFERENCES [ECommerce_Currency](currencyCode);
GO



-- Changeset initial.xml::1255116619640-169::Lee (generated)::(MD5Sum: 5c3274f242586d218b9a8322393c64a5)
ALTER TABLE [ECommerce_CountryDeliveryState] ADD CONSTRAINT FK_CountryStateBasedDelivery_States FOREIGN KEY (stateID) REFERENCES [ECommerce_State](stateID);
GO


-- Changeset initial.xml::1255116619640-170::Lee (generated)::(MD5Sum: e49bd9eda87c8158e14b49be4f1c2a)
ALTER TABLE [ECommerce_CountryDeliveryStateWeight] ADD CONSTRAINT FK_CountryStateWeightBasedDelivery_States FOREIGN KEY (stateID) REFERENCES [ECommerce_State](stateID);
GO



-- Changeset initial.xml::1255116619640-171::Lee (generated)::(MD5Sum: 402650e08df3c3e180d4122a118af76)
ALTER TABLE [ECommerce_CountryDeliveryWeight] ADD CONSTRAINT FK_ECommerce_CountryDeliveryWeight_ECommerce_Country FOREIGN KEY (countryCode) REFERENCES [ECommerce_Country](countryCode);
GO


-- Changeset initial.xml::1255116619640-172::Lee (generated)::(MD5Sum: 55b4fba95585cf9f3b40d2ffc9ca2937)
ALTER TABLE [Ecommerce_Document] ADD CONSTRAINT FK_Ecommerce_Document_Ecommerce_DocumentType FOREIGN KEY (TypeID) REFERENCES [Ecommerce_DocumentType](TypeID);
GO


-- Changeset initial.xml::1255116619640-173::Lee (generated)::(MD5Sum: 246359867ed8e9b22234bca2467d2fd)
ALTER TABLE [Ecommerce_Kit] ADD CONSTRAINT FK_Ecommerce_Kit_ECommerce_Category FOREIGN KEY (categoryID) REFERENCES [ECommerce_Category](categoryID);
GO



-- Changeset initial.xml::1255116619640-174::Lee (generated)::(MD5Sum: 1e9f847587d59e9926e389e247f228)
ALTER TABLE [ECommerce_OrderHeader] ADD CONSTRAINT FK_ECommerce_OrderHeader_ECommerce_Address1 FOREIGN KEY (deliveryAddressID) REFERENCES [ECommerce_Address](addressID);
GO



-- Changeset initial.xml::1255116619640-175::Lee (generated)::(MD5Sum: e1e93f20ead1edb178a011697bcb641a)
ALTER TABLE [ECommerce_OrderHeader] ADD CONSTRAINT FK_OrderHeader_deliveryType FOREIGN KEY (deliveryTypeID) REFERENCES [ECommerce_DeliveryType](deliveryTypeID);
GO


-- Changeset initial.xml::1255116619640-176::Lee (generated)::(MD5Sum: 6150b6c2613bcec156ecff6a727dc042)
ALTER TABLE [ECommerce_OrderHeader] ADD CONSTRAINT FK_ECommerce_OrderHeader_ECommerce_Address FOREIGN KEY (invoiceAddressID) REFERENCES [ECommerce_Address](addressID);
GO

-- Changeset initial.xml::1255116619640-177::Lee (generated)::(MD5Sum: 688d71dbb2661de994c698111c6a37)
ALTER TABLE [ECommerce_Payment] ADD CONSTRAINT FK_ECommerce_Payment_ECommerce_Basket FOREIGN KEY (basketID) REFERENCES [ECommerce_Basket](basketID);
GO



-- Changeset initial.xml::1255116619640-178::Lee (generated)::(MD5Sum: 7e8c2ebe5b4b2ba1b8639237edb8a)
ALTER TABLE [ECommerce_Payment] ADD CONSTRAINT FK_ECommerce_Payment_ECommerce_Currency FOREIGN KEY (currencyCode) REFERENCES [ECommerce_Currency](currencyCode);
GO


-- Changeset initial.xml::1255116619640-179::Lee (generated)::(MD5Sum: 179c9e71e8701dcc15833a1627d0846c)
ALTER TABLE [ECommerce_ProductAttributeOptionValue] ADD CONSTRAINT FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-180::Lee (generated)::(MD5Sum: c3722b9fd9273944b8f9abc15a0791a)
ALTER TABLE [ECommerce_ProductCategory] ADD CONSTRAINT FK_ECommerce_ProductCategory_ECommerce_Category FOREIGN KEY (categoryID) REFERENCES [ECommerce_Category](categoryID);
GO



-- Changeset initial.xml::1255116619640-181::Lee (generated)::(MD5Sum: 131efd975f6a88c8bcc6248841e715)
ALTER TABLE [ECommerce_ProductCategory] ADD CONSTRAINT FK_ProductCategories_Products FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO


-- Changeset initial.xml::1255116619640-182::Lee (generated)::(MD5Sum: 6a60e21cd76e2db08591f647fb24b1)
ALTER TABLE [Ecommerce_ProductDocument] ADD CONSTRAINT FK_Ecommerce_ProductDocument_Ecommerce_Document FOREIGN KEY (DocumentID) REFERENCES [Ecommerce_Document](DocumentID);
GO



-- Changeset initial.xml::1255116619640-183::Lee (generated)::(MD5Sum: b92a7a1e70192321dc14c7d6a4b3d38f)
ALTER TABLE [Ecommerce_ProductDocument] ADD CONSTRAINT FK_Ecommerce_ProductDocument_Ecommerce_Product FOREIGN KEY (ProductID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-184::Lee (generated)::(MD5Sum: a8997a8e1bb7e72509d532f855d2bd6)
ALTER TABLE [ECommerce_ProductImage] ADD CONSTRAINT FK_ECommerce_ProductImage_ECommerce_Product1 FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-185::Lee (generated)::(MD5Sum: 6f7646195527d65eb8d79b6aa1e8eb8b)
ALTER TABLE [ECommerce_ProductRelation] ADD CONSTRAINT FK_ECommerce_ProductRelation_ECommerce_Product FOREIGN KEY (parentID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-186::Lee (generated)::(MD5Sum: 71caa1ead4d0e0639324e69f6dd93)
ALTER TABLE [ECommerce_ProductRelation] ADD CONSTRAINT FK_ProductRelation_products FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-187::Lee (generated)::(MD5Sum: 2723acc5464c6bec2be9a7422f2cb5)
ALTER TABLE [ECommerce_ProductRelation] ADD CONSTRAINT FK_ProductRelation_RelationType FOREIGN KEY (relationTypeID) REFERENCES [ECommerce_RelationType](relationTypeID);
GO



-- Changeset initial.xml::1255116619640-188::Lee (generated)::(MD5Sum: 1328664a05a59daf53518daf6d1d93)
ALTER TABLE [ECommerce_ProductSKU] ADD CONSTRAINT FK_ProductSKU_Products FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-189::Lee (generated)::(MD5Sum: 5269fa4bbfad6e2bb87c566c44889f1)
ALTER TABLE [Ecommerce_ProductSynonym] ADD CONSTRAINT FK_Ecommerce_ProductSynonym_Ecommerce_Product FOREIGN KEY (ProductID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-190::Lee (generated)::(MD5Sum: 22b2ad209b456c12fcf3a16f6519c2f)
ALTER TABLE [ECommerce_ProductTaxClass] ADD CONSTRAINT FK_ECommerce_ProductTaxClass_ECommerce_Product FOREIGN KEY (productID) REFERENCES [ECommerce_Product](productID);
GO



-- Changeset initial.xml::1255116619640-191::Lee (generated)::(MD5Sum: 8e4633e4fd24ae8c8918f689df051fa)
ALTER TABLE [ECommerce_ProductTaxClass] ADD CONSTRAINT FK_ECommerce_ProductTaxClass_ECommerce_TaxClass FOREIGN KEY (taxClassID) REFERENCES [ECommerce_TaxClass](taxClassID);
GO


-- Changeset initial.xml::1255116619640-192::Lee (generated)::(MD5Sum: 3899e929cf39e10520a01d80d6939)
ALTER TABLE [ECommerce_State] ADD CONSTRAINT FK_ECommerce_State_ECommerce_Country FOREIGN KEY (countryCode) REFERENCES [ECommerce_Country](countryCode);
GO



-- Changeset initial.xml::1255116619640-193::Lee (generated)::(MD5Sum: a89785d6701ae29ab26ceb70dbfab3a0)
ALTER TABLE [ECommerce_TaxZoneClassRate] ADD CONSTRAINT FK_ECommerce_TaxZoneClassRate_ECommerce_TaxClass FOREIGN KEY (taxClassID) REFERENCES [ECommerce_TaxClass](taxClassID);
GO



-- Changeset initial.xml::1255116619640-194::Lee (generated)::(MD5Sum: b9297c8fc0817cc2932c3282fda31b)
ALTER TABLE [ECommerce_TaxZoneClassRate] ADD CONSTRAINT FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone FOREIGN KEY (taxZoneID) REFERENCES [ECommerce_TaxZone](taxZoneID);
GO


-- Changeset initial.xml::1255116619640-195::Lee (generated)::(MD5Sum: 9f1fe483d778efbe2e9dde9b6fa7628)
ALTER TABLE [ECommerce_TaxZoneCountry] ADD CONSTRAINT FK_TaxZoneCountries_Countries FOREIGN KEY (countryCode) REFERENCES [ECommerce_Country](countryCode);
GO



-- Changeset initial.xml::1255116619640-196::Lee (generated)::(MD5Sum: 3755696a954f77e806ad669bf7b43ea)
ALTER TABLE [ECommerce_TaxZoneCountry] ADD CONSTRAINT FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone FOREIGN KEY (taxZoneID) REFERENCES [ECommerce_TaxZone](taxZoneID);
GO



-- Changeset initial.xml::1255116619640-197::Lee (generated)::(MD5Sum: 67cfe98be9e15366e448d1dd12ace5)
ALTER TABLE [ECommerce_TaxZoneState] ADD CONSTRAINT FK_TaxZoneStates_States FOREIGN KEY (stateID) REFERENCES [ECommerce_State](stateID);
GO


-- Changeset initial.xml::1255116619640-198::Lee (generated)::(MD5Sum: aa3aa369f2b27a8884bc8ce0cfce3c5d)
ALTER TABLE [ECommerce_TaxZoneState] ADD CONSTRAINT FK_ECommerce_TaxZoneState_ECommerce_TaxZone FOREIGN KEY (taxZoneID) REFERENCES [ECommerce_TaxZone](taxZoneID);
GO



-- Changeset initial.xml::1255116619640-199::Lee (generated)::(MD5Sum: 82a2fd3696bee475d97c5a4897f419)
ALTER TABLE [ECommerce_TranslationText] ADD CONSTRAINT FK_TranslationText_TranslationTags FOREIGN KEY (tagID) REFERENCES [ECommerce_TranslationTag](tagID);
GO


-- Changeset initial.xml::1255116619640-200::Lee (generated)::(MD5Sum: a5b0d39a49edf5482e492c1ade8e519a)
ALTER TABLE [ECommerce_UserDetail] ADD CONSTRAINT FK_UserDetails_address FOREIGN KEY (addressID) REFERENCES [ECommerce_Address](addressID);
GO



-- Changeset r1.0/Structure.xml::2::cook::(MD5Sum: a3db6e38628e406873c72c7229d4bab)
-- Add in stored procedures.
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[showCategoriesWithNoProducts]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [showCategoriesWithNoProducts]
	  as
	  select c1.categoryname, c2.categoryname, c3.* from ecommerce_category c3
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid
	  where c1.parentcategoryid = 1
	  and c1.ispublished = 1
	  and c2.ispublished = 1
	  and c3.ispublished = 1
	  and c3.categoryid not in (
	  select distinct categoryid from ecommerce_productcategory
	  )
	  order by c1.categoryname, c2.categoryname, c3.categoryname
	  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetImageByID]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetImageByID]
	  @ID int
	  
	  as
	  
	  select Top 1 i.imageurl, i.width, i.height, i.altText from   
	                        ECommerce_ProductImage i
	    where i.imageID = @ID
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getImageDownloadScript]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getImageDownloadScript]  
	  as  
	  select distinct (''wget "http://www.albion-manufacturing.com/uploaded_photos/'' + Img + ''"'') from Albion_Import_Product  
	  where Img is not null and ltrim(rtrim(Img)) <> ''''  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getImageResizeScript]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getImageResizeScript]  
	  as  
	  select distinct (''NConvert -o "resize\'' + Img + ''" -ratio -resize 130 100 "'' + Img + ''"'') from Albion_Import_Product  
	  where Img is not null and ltrim(rtrim(Img)) <> ''''  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductCategories]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getProductCategories]
	  @catID bigint
	  as
	  select * from ECommerce_Category
	  where parentCategoryID = @catID
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductLines]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [getProductLines]
	  as
	  select c.categoryid, c.categoryname from ecommerce_category c
	  inner join ecommerce_category rc on c.parentcategoryid = rc.categoryid
	  where rc.parentcategoryid is null and c.ispublished = 1
	  order by c.sortorder
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getMinimumDeliveryCharge]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getMinimumDeliveryCharge]
	  @CountryCode VARCHAR,
	  @chargeName VARCHAR
	  as
	  select cc.price from ECommerce_Charge c join ECommerce_CountryCharge cc on c.ChargeID = cc.ChargeID where cc.CountryCode = @CountryCode AND  c.ChargeName = @chargeName
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[saveProductPriceChange]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [saveProductPriceChange]
	  @newPrice decimal,
	  @productID int
	  as
	  UPDATE ECommerce_Product
	  SET basePrice = @newPrice
	  WHERE productID = @productID
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_DeleteCategory]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_DeleteCategory]
	  @id int
	  as
	  DELETE from ECommerce_Category
	  WHERE categoryID = @id
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetCategory]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetCategory]
	  @id int
	  as
	  SELECT     categoryID AS id, categoryName AS name, categoryDescription AS cdescription, c.parentCategoryID AS parentID, tylosandimageurl as tylosandimageurl
	  FROM         ECommerce_Category c
	  where 	    c.parentCategoryID = @id
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CreateImageDownloadScript]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [CreateImageDownloadScript]
	  as
	  select ''wget "http://localhost/uploaded_photos/'' + i.imageurl + ''"'' from ecommerce_product p
	  inner join ECommerce_ProductImage i on p.productid = i.productid
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetRootCategory]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetRootCategory]
	  as
	  SELECT     categoryID AS id, categoryName AS name, categoryDescription AS description
	  FROM         ECommerce_Category c
	  where 	    c.parentCategoryID IS NULL
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CreateImageResizeScript]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [CreateImageResizeScript]
	  @width int = 130,
	  @height int = 100,
	  @folder varchar(50) = ''resize''
	  as
	  select ''NConvert -o "'' + @folder + ''\'' + i.imageurl + ''" -ratio -resize '' + convert(varchar(5), @width) + '' '' + convert(varchar(5), @height) + '' "'' + i.imageurl + ''"'' from ecommerce_product p
	  inner join ECommerce_ProductImage i on p.productid = i.productid
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[deleteUnpublishedCategories]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [deleteUnpublishedCategories]  
	    
	  as  
	    
	  delete pc  
	  from ecommerce_category c3  
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  inner join ecommerce_categorypricechange pc on c3.categoryid = pc.categoryid  
	  where c1.ispublished = 0  
	    
	  delete pc  
	  from ecommerce_category c2  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  inner join ecommerce_categorypricechange pc on c2.categoryid = pc.categoryid  
	  where c1.ispublished = 0  
	    
	  delete pc  
	  from ecommerce_category c1  
	  inner join ecommerce_categorypricechange pc on c1.categoryid = pc.categoryid  
	  where c1.ispublished = 0  
	    
	  delete k  
	  from ecommerce_category c3  
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  inner join Ecommerce_Kit k on c3.categoryid = k.categoryid  
	  where c1.ispublished = 0  
	    
	  delete k  
	  from ecommerce_category c2  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  inner join Ecommerce_Kit k on c2.categoryid = k.categoryid  
	  where c1.ispublished = 0  
	    
	  delete k  
	  from ecommerce_category c1  
	  inner join Ecommerce_Kit k on c1.categoryid = k.categoryid  
	  where c1.ispublished = 0  
	    
	  delete cl  
	  from ecommerce_category c3  
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  inner join Ecommerce_CategoryLink cl on c3.categoryid = cl.categoryid  
	  where c1.ispublished = 0  
	    
	  delete cl  
	  from ecommerce_category c2  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  inner join Ecommerce_CategoryLink cl on c2.categoryid = cl.categoryid  
	  where c1.ispublished = 0  
	    
	  delete cl  
	  from ecommerce_category c1  
	  inner join Ecommerce_CategoryLink cl on c1.categoryid = cl.categoryid  
	  where c1.ispublished = 0  
	    
	  delete c3  
	  from ecommerce_category c3  
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  where c1.ispublished = 0  
	    
	  delete c2  
	  from ecommerce_category c2  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  where c1.ispublished = 0  
	    
	  delete from ecommerce_category where ispublished = 0  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getCategoryKits]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getCategoryKits]  
	  @categoryID bigint  
	    
	  as  
	    
	  select p.productName, p.itemCode, p.productID, pim.imageUrl, pim.width, pim.height, pim.imageurl, pim.width, pim.height from ecommerce_product p  
	  inner join ecommerce_productcategory pc3 on pc3.productid = p.productid  
	  left join ecommerce_productimage pim on pim.productid = p.productid  
	  where p.iskit = 1 and pc3.categoryid = @categoryID  
	    
	  union  
	    
	  select p.productName, p.itemCode, p.productID, pim.imageUrl, pim.width, pim.height, pim.imageurl, pim.width, pim.height from ecommerce_product p  
	  inner join ecommerce_productcategory pc3 on pc3.productid = p.productid  
	  inner join ecommerce_category c3 on pc3.categoryid = c3.categoryid  
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid  
	  left join ecommerce_productimage pim on pim.productid = p.productid  
	  where p.iskit = 1 and c2.categoryid = @categoryID  
	    
	  union  
	    
	  select p.productName, p.itemCode, p.productID, pim.imageUrl, pim.width, pim.height, pim.imageurl, pim.width, pim.height from ecommerce_product p  
	  inner join ecommerce_productcategory pc3 on pc3.productid = p.productid  
	  inner join ecommerce_category c3 on pc3.categoryid = c3.categoryid  
	  inner join ecommerce_category c2 on c3.parentcategoryid = c2.categoryid  
	  inner join ecommerce_category c1 on c2.parentcategoryid = c1.categoryid  
	  left join ecommerce_productimage pim on pim.productid = p.productid  
	  where p.iskit = 1 and c1.categoryid = @categoryID  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getFamilyRelatedProducts]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getFamilyRelatedProducts]    
	  @productID bigint,    
	  @relationshipType varchar(50) = ''CrossSell''    
	      
	  as    
	      
	  declare @family nvarchar(50)    
	  select @family = productfamily from ecommerce_product where productid = @productid    
	      
	  select distinct pcs.* from ecommerce_product pcs    
	  inner join ECommerce_ProductRelation pr on pcs.productid = pr.productid    
	  inner join ECommerce_RelationType rt on pr.relationtypeid = rt.relationtypeid    
	  inner join ECommerce_Product p on pr.parentid = p.productid    
	  where (p.productid = @productid or (p.productfamily = @family and p.productfamily is not null)) and rt.relationshipname = @relationshipType  
	  order by pcs.itemcode  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductLineForProduct]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [getProductLineForProduct]
	  @productID bigint
	  as
	  select c.categoryid, c.categoryname from ECommerce_ProductCategory pc
	  inner join vwProductLineCategoryLookup cl on cl.categoryid = pc.categoryid
	  inner join ECommerce_Category c on cl.productlinecategoryid = c.categoryid
	  where pc.productid = @productid
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductPriceChange]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getProductPriceChange]    
	  @productID bigint    
	      
	  as    
	      
	  declare @catID bigint    
	  declare @change decimal (18,2)    
	  declare @loopCount int    
	    
	  set @change = null  
	  set @loopCount = 1  
	  select @catID = categoryid from ecommerce_productcategory where productid = @productID    
	      
	  while (@catID is not null and @change is null and @loopCount < 100)    
	  begin    
	  select @change = pricechangepercent from ecommerce_category where categoryid = @catID    
	  --print ''Found: '' + coalesce(convert(varchar(50), @catID), ''NULL'') + '', '' + coalesce(convert(varchar(50), @change), ''NULL'')  
	  select @catID = parentcategoryid from ecommerce_category where categoryid = @catID    
	  set @loopCount = @loopCount + 1  
	  END  
	      
	  select coalesce(@change, 0)  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetImageByItemCode]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetImageByItemCode]
	  @itemCodeID varchar(150)
	  as
	  select Top 1 i.imageurl, i.width, i.height, i.altText from   
	                        ECommerce_Product p inner JOIN
	                        ECommerce_ProductImage i ON p.productID = i.productID
	    	        where p.itemCode = @itemCodeID
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetProductList]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetProductList]
	  @id int
	  as
	  SELECT      p.*
	  FROM         ECommerce_Category c
	  inner join ECommerce_ProductCategory pc on c.categoryid = pc.categoryid
	  inner join ECommerce_Product p on pc.productID = p.productID
	  where 	      c.categoryID = @id
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_UpdateProductCategory]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_UpdateProductCategory]
	  @categoryID int,
	  @oldCategoryID int,
	  @productID int
	  as
	  UPDATE ECommerce_ProductCategory
	  SET categoryid = @categoryID
	  WHERE productID = @productID AND categoryID = @oldCategoryID
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UpdateCategoryPriceByPercentage]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [UpdateCategoryPriceByPercentage]
	  @catID bigint,
	  @change bigint
	  as
	  select * from ECommerce_Product p inner join ECommerce_ProductCategory pc on p.productID = pc.ProductID Inner join ECommerce_Category c on pc.CategoryID = c.CategoryID
	  where c.parentCategoryID = @catID
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getExtendedProperties]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [getExtendedProperties]
	  @productID int
	  as
	  select attributereference, optionvalue from ECommerce_ProductAttributeView
	  where productid = @productid
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductAttributes]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getProductAttributes]
	  @productID bigint
	  as
	  select a.*, t.[name] as type, aov.optionid, aov.optionName, aov.optionData, paov.optionprice from Ecommerce_Attribute a
	  inner join Ecommerce_AttributeType t on t.typeid = a.typeid
	  inner join Ecommerce_AttributeOptionValue aov on a.attributeid = aov.attributeid
	  inner join ECommerce_ProductAttributeOptionValue paov on aov.optionid = paov.optionvalueid
	  where paov.productid = @productID
	  order by a.attributeid, paov.sortorder
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductAttributeValueByReference]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [getProductAttributeValueByReference]
	  @productID bigint,
	  @attributeReference varchar(100)
	  as
	  select a.*, aov.optionname, aov.optiondata, paov.optionprice from ECommerce_ProductAttributeOptionValue paov 
	  inner join Ecommerce_AttributeOptionValue aov on paov.optionvalueid = aov.optionid
	  inner join Ecommerce_Attribute a on aov.attributeid = a.attributeid
	  where paov.productid = @productID and a.attributereference = @attributeReference
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductAttributeValues]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [getProductAttributeValues]
	  @productID bigint
	  as
	  select a.*, aov.optionname, aov.optiondata, paov.optionprice from ECommerce_ProductAttributeOptionValue paov 
	  inner join Ecommerce_AttributeOptionValue aov on paov.optionvalueid = aov.optionid
	  inner join Ecommerce_Attribute a on aov.attributeid = a.attributeid
	  where paov.productid = @productID
	  order by paov.sortorder, a.attributedescription, a.attributeid, aov.optionname
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[setProductAttributeValueByReference]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [setProductAttributeValueByReference]  
	  @productID bigint,  
	  @attributeReference nvarchar(300),
	  @attributeValue nvarchar(100)
	    
	  as
	  set nocount on 
	  declare @attributeID int
	  select @attributeID = attributeid from Ecommerce_Attribute where attributereference = @attributeReference
	  if @attributeID is null
	  begin
	  	insert into ecommerce_attribute (attributereference, attributedescription, isdisplayable, typeid) values (@attributeReference, @attributeReference, 1, 1)
	  	select @attributeID = @@identity
	  END 
	  
	  declare @optionID int
	  -- see if this value already exists
	  select @optionID = optionID from ecommerce_attributeoptionvalue where attributeID = @attributeID and optionname = @attributeValue
	  if @optionID is null
	  begin
	  	insert into ecommerce_attributeoptionvalue (attributeid, optionname) values (@attributeID, @attributeValue)
	  	select @optionID = @@identity
	  END 
	  if exists (select * from ecommerce_productattributeoptionvalue where productID = @productID and optionvalueid = @optionid)
	  return
	  -- remove existing value
	  delete ecommerce_productattributeoptionvalue
	  from ecommerce_productattributeoptionvalue paov
	  inner join ecommerce_attributeoptionvalue aov on paov.optionvalueid = aov.optionid
	  where aov.attributeID = @attributeID and paov.productID = @productID
	  insert into ecommerce_productattributeoptionvalue(productID, optionValueID, optionPrice, optionvaluecode, sortorder) values (@productID, @optionID, 0, @optionID, 1)
	  set nocount off
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetProduct]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetProduct]
	  @id varchar(150)
	  as
	  select  p.productID as [id], p.productName as [name], p.productDescription as [description], p.basePrice as price, i.imageUrl as [img],  pc.categoryID as categoryID, aov.optionName as weight, p.itemCode as itemCode from   
	                        ECommerce_Product p LEFT OUTER JOIN
	                        ECommerce_ProductImage i ON p.productID = i.productID LEFT OUTER JOIN
	                        ECommerce_ProductCategory pc ON p.productID = pc.productID LEFT OUTER JOIN
	                        ECommerce_ProductAttributeOptionValue paov ON p.productID = paov.productID LEFT OUTER JOIN
	                        Ecommerce_AttributeOptionValue aov ON paov.optionValueID = aov.optionID
	    	        where itemCode = @id
	                       AND (aov.attributeID = ''1'') /* make sure it is a weight attribute  - sorry for the magic number.*/
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetProductListWebService]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [SP_GetProductListWebService]
	  @id varchar(150)
	  as
	  select  p.productID as [id], p.productName as [name], p.productDescription as [description], p.basePrice as price, i.imageUrl as [img],  pc.categoryID as categoryID, aov.optionName as weight, p.itemCode as itemCode from   
	                        ECommerce_Product p LEFT OUTER JOIN
	                        ECommerce_ProductImage i ON p.productID = i.productID LEFT OUTER JOIN
	                        ECommerce_ProductCategory pc ON p.productID = pc.productID LEFT OUTER JOIN
	                        ECommerce_ProductAttributeOptionValue paov ON p.productID = paov.productID LEFT OUTER JOIN
	                        Ecommerce_AttributeOptionValue aov ON paov.optionValueID = aov.optionID
	    	       where 	     pc.categoryID = @id
	                       AND (aov.attributeID = ''1'') /* make sure it is a weight attribute  - sorry for the magic number.*/
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getDeliveryChargeByWeight]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [getDeliveryChargeByWeight]
	  @countrycode char(2),
	  @weight decimal(5,2),
	  @stateid smallint = null
	  as
	  if @stateid is null
	  begin
	  select top 1 price from ECommerce_CountryDeliveryWeight
	  where weightlevel >= @weight and countrycode = @countrycode
	  order by weightlevel
	  END 
	  else
	  begin
	  select top 1 price from ECommerce_CountryDeliveryStateWeight
	  where weightlevel >= @weight and stateid = @stateid
	  order by weightlevel
	  END 
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[showTestOrderUrls]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [showTestOrderUrls]  
	  as  
	  select ''http://localhost/modules/ecommerce/ccpostback.aspx?oid='' + convert(varchar(50), b.orderheaderid) + ''&total='' + convert(varchar(50), taxprice + subtotalprice) + ''&datetime=2007-10-20&transactionstatus=success''  
	  from ecommerce_basket b inner join ecommerce_orderheader oh on b.orderheaderid = oh.orderheaderid  
	  where b.orderheaderid is not null and invoiceaddressid is not null and oh.orderstatusid = 3
	  order by b.orderheaderid desc
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductCount]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  CREATE procedure [getProductCount]
	  @categoryID bigint
	  as
	  select count(p.productID) as productCount from ECommerce_Product p
	  Inner Join ECommerce_ProductCategory pc on p.productid = pc.productID
	  where pc.categoryID = @categoryID' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetModuleSettings]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [GetModuleSettings]  
	    
	  @moduletypeid int  
	    
	  as  
	    
	  select ''INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, ''''''   
	  + name + '''''', '''''' + friendlyname + '''''', '''''' + settingdatatype + '''''', '' + convert(varchar(1), iscustomtype) + '', '' + convert(varchar(1), isrequired) + '')''  
	  from cuyahoga_modulesetting where moduletypeid = @moduletypeid  
	  
	  ' 
	  END;
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetModuleServices]') AND type in (N'P', N'PC'))
	  BEGIN
	  EXEC dbo.sp_executesql @statement = N'
	  create procedure [GetModuleServices]  
	    
	  @moduletypeid int  
	    
	  as  
	    
	  select ''INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)  
	  VALUES (@moduletypeid, '''''' + servicekey + '''''', '''''' + servicetype + '''''', '''''' + classtype + '''''')''  
	  from cuyahoga_moduleservice where moduletypeid = @moduletypeid  
	  
	  ' 
	  END;
GO


-- Changeset r1.0/ModuleConfiguration.xml::1::cook::(MD5Sum: 8560b57b4f7d446245349214cf6e61b)
-- Module config
INSERT INTO [cuyahoga_moduletype]
						            ([name]
						            ,[assemblyname]
						            ,[classname]
						            ,[path]
						            ,[editpath]
						            ,[autoactivate]
						            ,[inserttimestamp]
						            ,[updatetimestamp])
						      VALUES
						            ('ECommerce'
						            ,'Cuyahoga.Modules.ECommerce'
						            ,'Cuyahoga.Modules.ECommerce.ECommerceModule'
						            ,'Modules/ECommerce/Views/BasketView.ascx'
						            ,'Modules/ECommerce/Admin/Default.aspx'
						            ,1
						            ,GETDATE()
				           		    ,GETDATE())
				           		    
				           	 DECLARE @MODULE_TYPE_ID int
					         SET @MODULE_TYPE_ID = @@identity
					         
					         /* Insert Module Settings */
					         INSERT INTO [cuyahoga_modulesetting]
						            ([moduletypeid]
						            ,[name]
						            ,[friendlyname]
						            ,[settingdatatype]
						            ,[iscustomtype]
						            ,[isrequired])
						      VALUES
						            (@MODULE_TYPE_ID
						            ,'DISPLAY_MODE'
						            ,'Display Mode'
						            ,'Cuyahoga.Modules.ECommerce.Util.Enums.DisplayMode'
						            ,1
				          		    ,1)
				          		    
				          		    
					         /* Insert Module Services */
					         INSERT INTO [cuyahoga_moduleservice]
						            ([moduletypeid]
						            ,[servicekey]
						            ,[servicetype]
						            ,[classtype]
						            ,[lifestyle])
						      VALUES
						            (@MODULE_TYPE_ID
						            ,'ecommerce.catalogueviewer'
						            ,'Cuyahoga.Modules.ECommerce.Service.ICatalogueViewService,Cuyahoga.Modules.ECommerce'
						            ,'Cuyahoga.Modules.ECommerce.Service.CatalogueViewService,Cuyahoga.Modules.ECommerce'
				           		    ,NULL)
				           		    
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.catalogueModifier'
							    ,'Cuyahoga.Modules.ECommerce.Service.ICatalogueModificationService,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.Service.CatalogueModificationService,Cuyahoga.Modules.ECommerce'
							    ,NULL)
				
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.commerceservice'
							    ,'Cuyahoga.Modules.ECommerce.Service.ICommerceService,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.Service.CommerceService,Cuyahoga.Modules.ECommerce'
							    ,NULL)			    
							    
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.commercedao'
							    ,'Cuyahoga.Modules.ECommerce.DataAccess.ICommerceDao,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.DataAccess.CommerceDao,Cuyahoga.Modules.ECommerce'
							    ,NULL)
				
				
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.commondao'
							    ,'Cuyahoga.Modules.ECommerce.DataAccess.IExtCommonDao,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.DataAccess.ExtCommonDao,Cuyahoga.Modules.ECommerce'
							    ,NULL)
				
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.processor'
							    ,'Cuyahoga.Modules.ECommerce.Service.OrderProcessor.IOrderProcessorFactory,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.Service.OrderProcessor.SimpleOrderProcessorFactory,Cuyahoga.Modules.ECommerce'
							    ,NULL)			    
							    
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.rules'
							    ,'Cuyahoga.Modules.ECommerce.Util.Interfaces.IBasketRules,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.Util.GenericBasketRules,Cuyahoga.Modules.ECommerce'
							    ,NULL)
				
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.accountservice'
							    ,'Cuyahoga.Modules.ECommerce.Service.IAccountService,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.Service.AccountService,Cuyahoga.Modules.ECommerce'
							    ,NULL)	
				
						   INSERT INTO [cuyahoga_moduleservice]
							    ([moduletypeid]
							    ,[servicekey]
							    ,[servicetype]
							    ,[classtype]
							    ,[lifestyle])
						      VALUES
							    (@MODULE_TYPE_ID
							    ,'ecommerce.orderservice'
							    ,'Cuyahoga.Modules.ECommerce.Service.IOrderService,Cuyahoga.Modules.ECommerce'
							    ,'Cuyahoga.Modules.ECommerce.Service.OrderService,Cuyahoga.Modules.ECommerce'
							    ,NULL)	
							    
							    
							    INSERT INTO [cuyahoga_version]
							               ([assembly]
							               ,[major]
							               ,[minor]
							               ,[patch])
							         VALUES
							               ('Cuyahoga.Modules.ECommerce'
							               ,1
							               ,5
           ,2);
GO



