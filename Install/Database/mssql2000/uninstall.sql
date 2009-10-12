-- *********************************************************************
-- Update Database Script
-- *********************************************************************
-- Change Log: master.xml
-- Ran at: 11/10/09 18:17
-- Against: cuyahoga@jdbc:sqlserver://localhost:1433;responseBuffering=full;encrypt=false;databaseName=CuyahogaECommerceDemov2;selectMethod=direct;trustServerCertificate=false;lastUpdateCount=true;
-- LiquiBase version: 1.8.0
-- *********************************************************************



-- Changeset r1.0/Uninstall.xml::4::cook::(MD5Sum: b9da2f347fa1da5cb9c9a2de9419181)
-- Delete Module
DELETE FROM cuyahoga_version WHERE assembly = 'Cuyahoga.Modules.ECommerce';
GO

DELETE cuyahoga_modulesetting 
								FROM cuyahoga_modulesetting ms
								INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce';
GO

DELETE cuyahoga_moduleservice
								FROM cuyahoga_moduleservice ms
								INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce';
GO

DELETE cuyahoga_sectionsetting from cuyahoga_sectionsetting css
				                		INNER JOIN cuyahoga_section cs on css.sectionid = cs.sectionid
								INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = cs.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce';
GO

DELETE cuyahoga_templatesection from cuyahoga_templatesection ts
								INNER JOIN cuyahoga_section cs on ts.sectionid = cs.sectionid
								INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = cs.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce';
GO

DELETE cuyahoga_sectionrole from cuyahoga_sectionrole sr
								INNER JOIN cuyahoga_section cs on sr.sectionid = cs.sectionid
								INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = cs.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce';
GO

DELETE cuyahoga_section from cuyahoga_section cs
								INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = cs.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce';
GO

DELETE FROM cuyahoga_moduletype
								WHERE assemblyname = 'Cuyahoga.Modules.ECommerce';
GO



-- Changeset r1.0/Uninstall.xml::5::cook::(MD5Sum: e8993a7fcad1341a7398b9ec5b891f76)
-- Drop ECommerce tables and SP's
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Payment_ECommerce_Basket]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Payment]'))
	  ALTER TABLE [ECommerce_Payment] DROP CONSTRAINT [FK_ECommerce_Payment_ECommerce_Basket];
GO



-- Changeset r1.0/Uninstall.xml::6::cook::(MD5Sum: 567717f41a8bab7667ee65b133d2a25)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Payment_ECommerce_Currency]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Payment]'))
	  ALTER TABLE [ECommerce_Payment] DROP CONSTRAINT [FK_ECommerce_Payment_ECommerce_Currency];
GO


-- Changeset r1.0/Uninstall.xml::7::cook::(MD5Sum: 192c526ba36bd5d254d6fd4436468fad)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_ProductDocument_Ecommerce_Document]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_ProductDocument]'))
	  ALTER TABLE [Ecommerce_ProductDocument] DROP CONSTRAINT [FK_Ecommerce_ProductDocument_Ecommerce_Document];
GO



-- Changeset r1.0/Uninstall.xml::8::cook::(MD5Sum: d593826ce1453108765d4311643308)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_ProductDocument_Ecommerce_Product]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_ProductDocument]'))
	  ALTER TABLE [Ecommerce_ProductDocument] DROP CONSTRAINT [FK_Ecommerce_ProductDocument_Ecommerce_Product];
GO



-- Changeset r1.0/Uninstall.xml::9::cook::(MD5Sum: 55b33c72d24a982cd554c7d2227eeeb)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ProductSKU_Products]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductSKU]'))
	  ALTER TABLE [ECommerce_ProductSKU] DROP CONSTRAINT [FK_ProductSKU_Products];
GO



-- Changeset r1.0/Uninstall.xml::11::cook::(MD5Sum: e8a99de8e848568e77f9dd59402adda9)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_ProductSynonym_Ecommerce_Product]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_ProductSynonym]'))
	  ALTER TABLE [Ecommerce_ProductSynonym] DROP CONSTRAINT [FK_Ecommerce_ProductSynonym_Ecommerce_Product];
GO

-- Changeset r1.0/Uninstall.xml::12::cook::(MD5Sum: dab84a598123ca4992e6bcf2fd9657)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_ProductTaxClass_ECommerce_Product]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductTaxClass]'))
	  ALTER TABLE [ECommerce_ProductTaxClass] DROP CONSTRAINT [FK_ECommerce_ProductTaxClass_ECommerce_Product];
GO


-- Changeset r1.0/Uninstall.xml::13::cook::(MD5Sum: ceba631674c451c1210a4c980bb8ddd)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_ProductTaxClass_ECommerce_TaxClass]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductTaxClass]'))
	  ALTER TABLE [ECommerce_ProductTaxClass] DROP CONSTRAINT [FK_ECommerce_ProductTaxClass_ECommerce_TaxClass];
GO



-- Changeset r1.0/Uninstall.xml::14::cook::(MD5Sum: ddef3acc43d822f85e7b63c9602a9525)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_TaxZoneClassRate_ECommerce_TaxClass]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TaxZoneClassRate]'))
	  ALTER TABLE [ECommerce_TaxZoneClassRate] DROP CONSTRAINT [FK_ECommerce_TaxZoneClassRate_ECommerce_TaxClass];
GO


-- Changeset r1.0/Uninstall.xml::15::cook::(MD5Sum: df82fb86faebf2ef6dc76bc959a6895)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TaxZoneClassRate]'))
	  ALTER TABLE [ECommerce_TaxZoneClassRate] DROP CONSTRAINT [FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone];
GO



-- Changeset r1.0/Uninstall.xml::16::cook::(MD5Sum: 59605682802788e07cdc8438c234ca8)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TaxZoneCountry]'))
	  ALTER TABLE [ECommerce_TaxZoneCountry] DROP CONSTRAINT [FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone];
GO


-- Changeset r1.0/Uninstall.xml::17::cook::(MD5Sum: e3ace297e2e5fc492a7756432695dadd)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_TaxZoneCountries_Countries]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TaxZoneCountry]'))
	  ALTER TABLE [ECommerce_TaxZoneCountry] DROP CONSTRAINT [FK_TaxZoneCountries_Countries];
GO



-- Changeset r1.0/Uninstall.xml::18::cook::(MD5Sum: 2979fda35546dd49bbf189895ae44e)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_TaxZoneState_ECommerce_TaxZone]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TaxZoneState]'))
	  ALTER TABLE [ECommerce_TaxZoneState] DROP CONSTRAINT [FK_ECommerce_TaxZoneState_ECommerce_TaxZone];
GO


-- Changeset r1.0/Uninstall.xml::19::cook::(MD5Sum: 1d6739cbec875c4c2f36ef6b178b039)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_TaxZoneStates_States]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TaxZoneState]'))
	  ALTER TABLE [ECommerce_TaxZoneState] DROP CONSTRAINT [FK_TaxZoneStates_States];
GO


-- Changeset r1.0/Uninstall.xml::20::cook::(MD5Sum: babbb1dad47e4639d84a61bac6b1d)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_TranslationText_TranslationTags]') AND parent_object_id = OBJECT_ID(N'[ECommerce_TranslationText]'))
	  ALTER TABLE [ECommerce_TranslationText] DROP CONSTRAINT [FK_TranslationText_TranslationTags];
GO



-- Changeset r1.0/Uninstall.xml::21::cook::(MD5Sum: 76d2fb878d6bc81f95d1d3de6d4971)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_UserDetails_address]') AND parent_object_id = OBJECT_ID(N'[ECommerce_UserDetail]'))
	  ALTER TABLE [ECommerce_UserDetail] DROP CONSTRAINT [FK_UserDetails_address];
GO



-- Changeset r1.0/Uninstall.xml::22::cook::(MD5Sum: 7991442b87d5b5f38926adb6c329ffa)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_BasketItemAttributes_BasketItem]') AND parent_object_id = OBJECT_ID(N'[ECommerce_BasketItemAttribute]'))
	  ALTER TABLE [ECommerce_BasketItemAttribute] DROP CONSTRAINT [FK_BasketItemAttributes_BasketItem];
GO



-- Changeset r1.0/Uninstall.xml::23::cook::(MD5Sum: e4c9cdee89136c55490a9c5cc3ac)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_AttributeGroupAttribute_Ecommerce_AttributeGroup]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_AttributeGroupAttribute]'))
	  ALTER TABLE [Ecommerce_AttributeGroupAttribute] DROP CONSTRAINT [FK_Ecommerce_AttributeGroupAttribute_Ecommerce_AttributeGroup];
GO



-- Changeset r1.0/Uninstall.xml::24::cook::(MD5Sum: fefc687c831c7a40623259257b7298eb)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CountryStateBasedDelivery_States]') AND parent_object_id = OBJECT_ID(N'[ECommerce_CountryDeliveryState]'))
	  ALTER TABLE [ECommerce_CountryDeliveryState] DROP CONSTRAINT [FK_CountryStateBasedDelivery_States];
GO


-- Changeset r1.0/Uninstall.xml::25::cook::(MD5Sum: 85affb78fffc721e93fe7e5f65f1117)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_State_ECommerce_Country]') AND parent_object_id = OBJECT_ID(N'[ECommerce_State]'))
	  ALTER TABLE [ECommerce_State] DROP CONSTRAINT [FK_ECommerce_State_ECommerce_Country];
GO



-- Changeset r1.0/Uninstall.xml::26::cook::(MD5Sum: f166d395d1dfebb9c04c6d8772ba5936)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Country_ECommerce_Currency]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Country]'))
	  ALTER TABLE [ECommerce_Country] DROP CONSTRAINT [FK_ECommerce_Country_ECommerce_Currency];
GO



-- Changeset r1.0/Uninstall.xml::27::cook::(MD5Sum: a774994cc39c7b49c51ec69c6d33081)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_address_Countries]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Address]'))
	  ALTER TABLE [ECommerce_Address] DROP CONSTRAINT [FK_address_Countries];
GO



-- Changeset r1.0/Uninstall.xml::28::cook::(MD5Sum: d19bfdaa566f986169d9248ccd2f7b37)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Address_ECommerce_State]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Address]'))
	  ALTER TABLE [ECommerce_Address] DROP CONSTRAINT [FK_ECommerce_Address_ECommerce_State];
GO



-- Changeset r1.0/Uninstall.xml::29::cook::(MD5Sum: 136918d26fad5cfa526de7ce68603a9d)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_Document_Ecommerce_DocumentType]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_Document]'))
	  ALTER TABLE [Ecommerce_Document] DROP CONSTRAINT [FK_Ecommerce_Document_Ecommerce_DocumentType];
GO



-- Changeset r1.0/Uninstall.xml::30::cook::(MD5Sum: b26475cef318acaf513384cba324b9)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_BasketItem_Basket]') AND parent_object_id = OBJECT_ID(N'[ECommerce_BasketItem]'))
	  ALTER TABLE [ECommerce_BasketItem] DROP CONSTRAINT [FK_BasketItem_Basket];
GO



-- Changeset r1.0/Uninstall.xml::31::cook::(MD5Sum: 17d6caf459b5c72851a9537a41d2a078)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_BasketItem_Products1]') AND parent_object_id = OBJECT_ID(N'[ECommerce_BasketItem]'))
	  ALTER TABLE [ECommerce_BasketItem] DROP CONSTRAINT [FK_BasketItem_Products1];
GO



-- Changeset r1.0/Uninstall.xml::32::cook::(MD5Sum: 9a1a9abcb654f2bedc8eb2a1726e7bc)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Category_ECommerce_Category]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Category]'))
	  ALTER TABLE [ECommerce_Category] DROP CONSTRAINT [FK_ECommerce_Category_ECommerce_Category];
GO



-- Changeset r1.0/Uninstall.xml::33::cook::(MD5Sum: 65369f84c6dc14a16b7571b124b677)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_ProductCategory_ECommerce_Category]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductCategory]'))
	  ALTER TABLE [ECommerce_ProductCategory] DROP CONSTRAINT [FK_ECommerce_ProductCategory_ECommerce_Category];
GO



-- Changeset r1.0/Uninstall.xml::34::cook::(MD5Sum: f6dfd9adca1de266b754eb8556998ef7)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ProductCateries_Products]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductCategory]'))
	  ALTER TABLE [ECommerce_ProductCategory] DROP CONSTRAINT [FK_ProductCateries_Products];
GO


-- Changeset r1.0/Uninstall.xml::35::cook::(MD5Sum: f64afe1b788f34ba6c919269f2193190)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_ProductImage_ECommerce_Product1]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductImage]'))
	  ALTER TABLE [ECommerce_ProductImage] DROP CONSTRAINT [FK_ECommerce_ProductImage_ECommerce_Product1];
GO



-- Changeset r1.0/Uninstall.xml::36::cook::(MD5Sum: 8197b64e28be1fd542c375ac32b51b9)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Basket_cuyahoga_user]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Basket]'))
	  ALTER TABLE [ECommerce_Basket] DROP CONSTRAINT [FK_ECommerce_Basket_cuyahoga_user];
GO



-- Changeset r1.0/Uninstall.xml::37::cook::(MD5Sum: 8783bfbd5eea43a7d3e0a08e9831)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_Basket_ECommerce_Currency]') AND parent_object_id = OBJECT_ID(N'[ECommerce_Basket]'))
	  ALTER TABLE [ECommerce_Basket] DROP CONSTRAINT [FK_ECommerce_Basket_ECommerce_Currency];
GO



-- Changeset r1.0/Uninstall.xml::38::cook::(MD5Sum: 1676c219bdf5b95fc07f6fed2764bfc3)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_Kit_ECommerce_Category]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_Kit]'))
	  ALTER TABLE [Ecommerce_Kit] DROP CONSTRAINT [FK_Ecommerce_Kit_ECommerce_Category];
GO



-- Changeset r1.0/Uninstall.xml::39::cook::(MD5Sum: 959ecc506e74e93e9b2826189459b0d6)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_CategoryLink_ECommerce_Category]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_CategoryLink]'))
	  ALTER TABLE [Ecommerce_CategoryLink] DROP CONSTRAINT [FK_Ecommerce_CategoryLink_ECommerce_Category];
GO



-- Changeset r1.0/Uninstall.xml::40::cook::(MD5Sum: 39c01e4ac670af799b80cfc885ec2139)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_CategoryPriceChange_ECommerce_Category]') AND parent_object_id = OBJECT_ID(N'[ECommerce_CategoryPriceChange]'))
	  ALTER TABLE [ECommerce_CategoryPriceChange] DROP CONSTRAINT [FK_ECommerce_CategoryPriceChange_ECommerce_Category];
GO



-- Changeset r1.0/Uninstall.xml::41::cook::(MD5Sum: 614395df9233c7f1913aef74a8a74b)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_ProductRelation_ECommerce_Product]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductRelation]'))
	  ALTER TABLE [ECommerce_ProductRelation] DROP CONSTRAINT [FK_ECommerce_ProductRelation_ECommerce_Product];
GO


-- Changeset r1.0/Uninstall.xml::42::cook::(MD5Sum: 50a3f61228365df3e9906597fab0975)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ProductRelation_products]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductRelation]'))
	  ALTER TABLE [ECommerce_ProductRelation] DROP CONSTRAINT [FK_ProductRelation_products];
GO



-- Changeset r1.0/Uninstall.xml::43::cook::(MD5Sum: c4256bf581423dca275f76cae49ae93)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ProductRelation_RelationType]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductRelation]'))
	  ALTER TABLE [ECommerce_ProductRelation] DROP CONSTRAINT [FK_ProductRelation_RelationType];
GO



-- Changeset r1.0/Uninstall.xml::44::cook::(MD5Sum: 5f93753b2259151ae26f65256545b3)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product]') AND parent_object_id = OBJECT_ID(N'[ECommerce_ProductAttributeOptionValue]'))
	  ALTER TABLE [ECommerce_ProductAttributeOptionValue] DROP CONSTRAINT [FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product];
GO



-- Changeset r1.0/Uninstall.xml::45::cook::(MD5Sum: d02c9eb59ad31dd23f53b9393ea5bb87)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_AttributeOption_Ecommerce_Attribute]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_AttributeOptionValue]'))
	  ALTER TABLE [Ecommerce_AttributeOptionValue] DROP CONSTRAINT [FK_Ecommerce_AttributeOption_Ecommerce_Attribute];
GO



-- Changeset r1.0/Uninstall.xml::46::cook::(MD5Sum: 39286cca7535c12ae2f6fb6ef1829f0)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_Ecommerce_Attribute_Ecommerce_AttributeType]') AND parent_object_id = OBJECT_ID(N'[Ecommerce_Attribute]'))
	  ALTER TABLE [Ecommerce_Attribute] DROP CONSTRAINT [FK_Ecommerce_Attribute_Ecommerce_AttributeType];
GO



-- Changeset r1.0/Uninstall.xml::47::cook::(MD5Sum: 3838073c954a1b787113773a4bac4)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_CountryStateWeightBasedDelivery_States]') AND parent_object_id = OBJECT_ID(N'[ECommerce_CountryDeliveryStateWeight]'))
	  ALTER TABLE [ECommerce_CountryDeliveryStateWeight] DROP CONSTRAINT [FK_CountryStateWeightBasedDelivery_States];
GO



-- Changeset r1.0/Uninstall.xml::48::cook::(MD5Sum: 50722a5475ecfb27ea54e16bf6ce14b1)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_CountryDeliveryWeight_ECommerce_Country]') AND parent_object_id = OBJECT_ID(N'[ECommerce_CountryDeliveryWeight]'))
	  ALTER TABLE [ECommerce_CountryDeliveryWeight] DROP CONSTRAINT [FK_ECommerce_CountryDeliveryWeight_ECommerce_Country];
GO



-- Changeset r1.0/Uninstall.xml::49::cook::(MD5Sum: 2d86738eb3e9e9eaa8f0b45adc65e757)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_OrderHeader_ECommerce_Address]') AND parent_object_id = OBJECT_ID(N'[ECommerce_OrderHeader]'))
	  ALTER TABLE [ECommerce_OrderHeader] DROP CONSTRAINT [FK_ECommerce_OrderHeader_ECommerce_Address];
GO


-- Changeset r1.0/Uninstall.xml::50::cook::(MD5Sum: 62274ad86bce1e4ae23b941c89ec78ca)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_ECommerce_OrderHeader_ECommerce_Address1]') AND parent_object_id = OBJECT_ID(N'[ECommerce_OrderHeader]'))
	  ALTER TABLE [ECommerce_OrderHeader] DROP CONSTRAINT [FK_ECommerce_OrderHeader_ECommerce_Address1];
GO


-- Changeset r1.0/Uninstall.xml::51::cook::(MD5Sum: 956d305eb9956d862ad8b5257f673e8)
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_OrderHeader_deliveryType]') AND parent_object_id = OBJECT_ID(N'[ECommerce_OrderHeader]'))
	  ALTER TABLE [ECommerce_OrderHeader] DROP CONSTRAINT [FK_OrderHeader_deliveryType];
GO



-- Changeset r1.0/Uninstall.xml::52::cook::(MD5Sum: 17e868792dfedad71f2789d20517fdd)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getImageDownloadScript]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getImageDownloadScript];
GO



-- Changeset r1.0/Uninstall.xml::53::cook::(MD5Sum: 757224df66ebebb2fe699a0be8bb0dd)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getImageResizeScript]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getImageResizeScript];
GO



-- Changeset r1.0/Uninstall.xml::54::cook::(MD5Sum: 627e142e92f9c4d17509d57e7489c)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductCateries]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductCateries];
GO


-- Changeset r1.0/Uninstall.xml::55::cook::(MD5Sum: b67af296913d13ee988c32372bb733)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductLines]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductLines];
GO



-- Changeset r1.0/Uninstall.xml::56::cook::(MD5Sum: 5d495e40fa9010dae820112cff3ad29b)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Payment]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Payment];
GO



-- Changeset r1.0/Uninstall.xml::57::cook::(MD5Sum: 4cfcdd9ad5263d30284e182b1fa87396)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getMinimumDeliveryCharge]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getMinimumDeliveryCharge];
GO


-- Changeset r1.0/Uninstall.xml::58::cook::(MD5Sum: c73e873db6344623fcc08124fd812bb2)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[saveProductPriceChange]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [saveProductPriceChange];
GO


-- Changeset r1.0/Uninstall.xml::59::cook::(MD5Sum: d5c9b97761f970ae5a07f84f6f3958c)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_DeleteCategory]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_DeleteCategory];
GO



-- Changeset r1.0/Uninstall.xml::60::cook::(MD5Sum: 70201bb6278fd9c2893d6725b51bbd1f)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetCategory]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetCategory];
GO



-- Changeset r1.0/Uninstall.xml::61::cook::(MD5Sum: 2a40dbc89580fe72dec78bd4f6c68ea4)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_PaymentProvider]') AND type in (N'U'))
	  DROP TABLE [ECommerce_PaymentProvider];
GO



-- Changeset r1.0/Uninstall.xml::62::cook::(MD5Sum: a8a74d2a4956c2f7c01877784eed8385)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CreateImageDownloadScript]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [CreateImageDownloadScript];
GO



-- Changeset r1.0/Uninstall.xml::63::cook::(MD5Sum: d62f483946d7b415f79fda8207862)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetRootCategory]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetRootCategory];
GO



-- Changeset r1.0/Uninstall.xml::64::cook::(MD5Sum: edb999e2df99a55e57525f27bed61a)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[DeleteAllProcedures]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [DeleteAllProcedures];
GO



-- Changeset r1.0/Uninstall.xml::65::cook::(MD5Sum: 905b2c6a508f604d1bab6971588a3dcb)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[CreateImageResizeScript]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [CreateImageResizeScript];
GO



-- Changeset r1.0/Uninstall.xml::66::cook::(MD5Sum: 5f1dd5b63fd309d51135cb26cc61d37)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[deleteUnpublishedCateries]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [deleteUnpublishedCateries];
GO



-- Changeset r1.0/Uninstall.xml::67::cook::(MD5Sum: ebf58d7bf44ef3117347e87df3025)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getCategoryKits]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getCategoryKits];
GO

-- Changeset r1.0/Uninstall.xml::68::cook::(MD5Sum: e624ffe81343357d3dc38bef7c4342c9)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getFamilyRelatedProducts]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getFamilyRelatedProducts];
GO



-- Changeset r1.0/Uninstall.xml::69::cook::(MD5Sum: d980149f9a9909da3fd323b83983d10)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductLineForProduct]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductLineForProduct];
GO



-- Changeset r1.0/Uninstall.xml::70::cook::(MD5Sum: 9a3e05ec398f49dfa98fb486f22286d)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductPriceChange]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductPriceChange];
GO



-- Changeset r1.0/Uninstall.xml::71::cook::(MD5Sum: d947623636c582cad7116b9eac370)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetImageByItemCode]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetImageByItemCode];
GO


-- Changeset r1.0/Uninstall.xml::72::cook::(MD5Sum: 3bec5515814e4af06ceae783f72b6c)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetProductList]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetProductList];
GO



-- Changeset r1.0/Uninstall.xml::73::cook::(MD5Sum: 30546729eb656b12239d68accac3ce4)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_UpdateProductCategory]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_UpdateProductCategory];
GO



-- Changeset r1.0/Uninstall.xml::74::cook::(MD5Sum: 2d1450a1814737963606ae3db339cf)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[UpdateCategoryPriceByPercentage]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [UpdateCategoryPriceByPercentage];
GO



-- Changeset r1.0/Uninstall.xml::75::cook::(MD5Sum: b711d82bebb8f93fb0fe26c8e117d8ff)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_ProductDocument]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_ProductDocument];
GO



-- Changeset r1.0/Uninstall.xml::76::cook::(MD5Sum: 885eeb583da5cb2cb77c9787caefd2d)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getExtendedProperties]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getExtendedProperties];
GO



-- Changeset r1.0/Uninstall.xml::77::cook::(MD5Sum: 7f4f95a3df315a3d67a243587bac439)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetProduct]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetProduct];
GO



-- Changeset r1.0/Uninstall.xml::78::cook::(MD5Sum: 3ac321e1a7d8aaafa5322aa68d06c48)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetProductListWebService]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetProductListWebService];
GO



-- Changeset r1.0/Uninstall.xml::79::cook::(MD5Sum: 1d10c566f2e17968a2e1f73a66d8ce)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getDeliveryChargeByWeight]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getDeliveryChargeByWeight];
GO



-- Changeset r1.0/Uninstall.xml::80::cook::(MD5Sum: bc12e39ce9a18c7b37e82ce5445acee)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_ProductSKU]') AND type in (N'U'))
	  DROP TABLE [ECommerce_ProductSKU];
GO



-- Changeset r1.0/Uninstall.xml::81::cook::(MD5Sum: 264f42bdf088fe90f0e1c35be8951b8)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[showTestOrderUrls]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [showTestOrderUrls];
GO



-- Changeset r1.0/Uninstall.xml::82::cook::(MD5Sum: 2774e86bb3a913af789a53ff1da3)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductCount]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductCount];
GO



-- Changeset r1.0/Uninstall.xml::83::cook::(MD5Sum: 44863a612ca6f82325ae014a735bab3)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetModuleSettings]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [GetModuleSettings];
GO



-- Changeset r1.0/Uninstall.xml::84::cook::(MD5Sum: 4139b2a173a3366489f11bfe220a4)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[GetModuleServices]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [GetModuleServices];
GO



-- Changeset r1.0/Uninstall.xml::85::cook::(MD5Sum: 2d77dda2f6b4106b305751f9b456ba65)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_ProductSynonym]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_ProductSynonym];
GO



-- Changeset r1.0/Uninstall.xml::86::cook::(MD5Sum: 92f6a4b363fc9fc202460d6a79f2b9a)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_ProductTaxClass]') AND type in (N'U'))
	  DROP TABLE [ECommerce_ProductTaxClass];
GO


-- Changeset r1.0/Uninstall.xml::87::cook::(MD5Sum: e65ee7bae7aee84a245101b3574859e)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductAttributes]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductAttributes];
GO


-- Changeset r1.0/Uninstall.xml::88::cook::(MD5Sum: 2919f22ceba2cb1faa71cabf117756b)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductAttributeValueByReference]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductAttributeValueByReference];
GO



-- Changeset r1.0/Uninstall.xml::89::cook::(MD5Sum: eadce51fd120889a5751c554125f62ad)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[getProductAttributeValues]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [getProductAttributeValues];
GO



-- Changeset r1.0/Uninstall.xml::90::cook::(MD5Sum: ef178fea08e1ded4a59f2987cbdffd)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[setProductAttributeValueByReference]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [setProductAttributeValueByReference];
GO



-- Changeset r1.0/Uninstall.xml::91::cook::(MD5Sum: 4a9a58f7ee719e6aa36a792a3dbb2dc)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TaxZoneClassRate]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TaxZoneClassRate];
GO



-- Changeset r1.0/Uninstall.xml::92::cook::(MD5Sum: 9416a57f47f3d02677aa2820aa6cfc94)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TaxZoneCountry]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TaxZoneCountry];
GO



-- Changeset r1.0/Uninstall.xml::93::cook::(MD5Sum: 344ec8ca596a774392bcdc5ab12c793c)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TaxZoneState]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TaxZoneState];
GO



-- Changeset r1.0/Uninstall.xml::94::cook::(MD5Sum: 22f49b37ed29e93ab82e17a3eb887b2)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[showCateriesWithNoProducts]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [showCateriesWithNoProducts];
GO



-- Changeset r1.0/Uninstall.xml::95::cook::(MD5Sum: a1dae8d83d14b67ebbb934c164953)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[SP_GetImageByID]') AND type in (N'P', N'PC'))
	  DROP PROCEDURE [SP_GetImageByID];
GO



-- Changeset r1.0/Uninstall.xml::96::cook::(MD5Sum: 816a1c278a5a92adbc339f407b067f1)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TranslationText]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TranslationText];
GO



-- Changeset r1.0/Uninstall.xml::97::cook::(MD5Sum: 59fa9674ed952a7bc3d4da5cd4f16a8d)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_UserDetail]') AND type in (N'U'))
	  DROP TABLE [ECommerce_UserDetail];
GO



-- Changeset r1.0/Uninstall.xml::98::cook::(MD5Sum: c2558615efadce47154030148b18d3)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_BasketItemAttribute]') AND type in (N'U'))
	  DROP TABLE [ECommerce_BasketItemAttribute];
GO



-- Changeset r1.0/Uninstall.xml::99::cook::(MD5Sum: 7ea0d068d3a60fce9583a5a8c48444)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_AttributeGroupAttribute]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_AttributeGroupAttribute];
GO


-- Changeset r1.0/Uninstall.xml::101::cook::(MD5Sum: 58aa8bae96f40487dcba0c911c673e0)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[PaymentProvider]') AND type in (N'U'))
	  DROP TABLE [PaymentProvider];
GO



-- Changeset r1.0/Uninstall.xml::102::cook::(MD5Sum: 9e5b27dd03cf6f38257c4f4ca727eff)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_CountryDeliveryState]') AND type in (N'U'))
	  DROP TABLE [ECommerce_CountryDeliveryState];
GO



-- Changeset r1.0/Uninstall.xml::103::cook::(MD5Sum: 1cbaf41a7297cf192ed20784cb4312)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_State]') AND type in (N'U'))
	  DROP TABLE [ECommerce_State];
GO



-- Changeset r1.0/Uninstall.xml::104::cook::(MD5Sum: 68a64aeb48d29d3273d1cad42ff86f12)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Country]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Country];
GO



-- Changeset r1.0/Uninstall.xml::105::cook::(MD5Sum: 4cdd2439bbcca333836b9d184205314)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_DocumentType]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_DocumentType];
GO

-- Changeset r1.0/Uninstall.xml::106::cook::(MD5Sum: 8257a4184933b041429939f7eb0feed)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Address]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Address];
GO


-- Changeset r1.0/Uninstall.xml::107::cook::(MD5Sum: d0e7ac9c4c8c48549baea841ac16a3b)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_DeliveryType]') AND type in (N'U'))
	  DROP TABLE [ECommerce_DeliveryType];
GO



-- Changeset r1.0/Uninstall.xml::108::cook::(MD5Sum: f49b2ecf2311f199125fc49c5f582e4)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Currency]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Currency];
GO



-- Changeset r1.0/Uninstall.xml::109::cook::(MD5Sum: ab5bdf293627511f4891dac835e41325)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_Document]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_Document];
GO



-- Changeset r1.0/Uninstall.xml::110::cook::(MD5Sum: 8bad0b953e0a54ba3c79ca0c7a4206d)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TaxClass]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TaxClass];
GO



-- Changeset r1.0/Uninstall.xml::111::cook::(MD5Sum: 526d8b5c44e02bd9bb19618f55553aa9)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TaxZone]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TaxZone];
GO



-- Changeset r1.0/Uninstall.xml::112::cook::(MD5Sum: 46a61abedcad2c7ceb1236e459172d6)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_TranslationTag]') AND type in (N'U'))
	  DROP TABLE [ECommerce_TranslationTag];
GO



-- Changeset r1.0/Uninstall.xml::113::cook::(MD5Sum: 79ae99c048f3c661e8bf146dc38f6b)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_AttributeGroup]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_AttributeGroup];
GO



-- Changeset r1.0/Uninstall.xml::114::cook::(MD5Sum: bd92ea3ab989804dd4aaa2e66221cbda)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_BasketItem]') AND type in (N'U'))
	  DROP TABLE [ECommerce_BasketItem];
GO



-- Changeset r1.0/Uninstall.xml::115::cook::(MD5Sum: b15c4082679ff1884353b4fbc6d4d3a)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Category]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Category];
GO



-- Changeset r1.0/Uninstall.xml::116::cook::(MD5Sum: 56a46dfca54716546049b86775c4f1a5)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_ProductCategory]') AND type in (N'U'))
	  DROP TABLE [ECommerce_ProductCategory];
GO



-- Changeset r1.0/Uninstall.xml::117::cook::(MD5Sum: 968685c0ac4a9db5abc7da1b245287)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_ProductImage]') AND type in (N'U'))
	  DROP TABLE [ECommerce_ProductImage];
GO



-- Changeset r1.0/Uninstall.xml::118::cook::(MD5Sum: 5b9e5d319935d41f13e7d93422218d3a)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Basket]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Basket];
GO



-- Changeset r1.0/Uninstall.xml::119::cook::(MD5Sum: a0b9461262b5f832e917973c8ba558e4)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_CountryCharge]') AND type in (N'U'))
	  DROP TABLE [ECommerce_CountryCharge];
GO



-- Changeset r1.0/Uninstall.xml::120::cook::(MD5Sum: 974fbb11d5bf10eb1fac7a6b36b18185)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Charge]') AND type in (N'U'))
	  DROP TABLE [ECommerce_Charge];
GO



-- Changeset r1.0/Uninstall.xml::121::cook::(MD5Sum: f3aa188a46ef18a08c9c3aad3f57a35)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_Kit]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_Kit];
GO


-- Changeset r1.0/Uninstall.xml::123::cook::(MD5Sum: c6f0c4aa3f25527b2f8541a19f6a1)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_CategoryLink]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_CategoryLink];
GO



-- Changeset r1.0/Uninstall.xml::124::cook::(MD5Sum: 4dfdc35367425372a41f57ec838c78)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_CategoryPriceChange]') AND type in (N'U'))
	  DROP TABLE [ECommerce_CategoryPriceChange];
GO



-- Changeset r1.0/Uninstall.xml::125::cook::(MD5Sum: 48d439843ec3d158a2cce6f7cc83f8fe)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_ProductRelation]') AND type in (N'U'))
	  DROP TABLE [ECommerce_ProductRelation];
GO


-- Changeset r1.0/Uninstall.xml::126::cook::(MD5Sum: a71b727d7328b8c77c77b1e86687a7f3)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_RelationType]') AND type in (N'U'))
	  DROP TABLE [ECommerce_RelationType];
GO



-- Changeset r1.0/Uninstall.xml::127::cook::(MD5Sum: 4e25e5c66d7413e19d6381a83afabc4)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_ProductAttributeOptionValue]') AND type in (N'U'))
	  DROP TABLE [ECommerce_ProductAttributeOptionValue];
GO



-- Changeset r1.0/Uninstall.xml::128::cook::(MD5Sum: 58f46a16afce06525ef2c8e6d5d391)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_AttributeType]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_AttributeType];
GO



-- Changeset r1.0/Uninstall.xml::129::cook::(MD5Sum: 425ec3e31afb1596c6cdd719659e50d9)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_AttributeOptionValue]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_AttributeOptionValue];
GO



-- Changeset r1.0/Uninstall.xml::130::cook::(MD5Sum: 79f722b33ea56d73babb364afb751f75)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[Ecommerce_Attribute]') AND type in (N'U'))
	  DROP TABLE [Ecommerce_Attribute];
GO



-- Changeset r1.0/Uninstall.xml::131::cook::(MD5Sum: 5543261f565b47e95befc4e67862e)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_CountryDeliveryStateWeight]') AND type in (N'U'))
	  DROP TABLE [ECommerce_CountryDeliveryStateWeight];
GO


-- Changeset r1.0/Uninstall.xml::132::cook::(MD5Sum: dc8c684da143c67bd088d157ff6c818d)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_CountryDeliveryWeight]') AND type in (N'U'))
	  DROP TABLE [ECommerce_CountryDeliveryWeight];
GO



-- Changeset r1.0/Uninstall.xml::133::cook::(MD5Sum: b7c35fcafbfa8c39577ca09c6c912e)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_OrderHeader]') AND type in (N'U'))
	  DROP TABLE [ECommerce_OrderHeader];
GO


-- Changeset r1.0/Uninstall.xml::134::cook::(MD5Sum: 548e6751c3acc5a05ef94434472e9ce1)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[ECommerce_Product]') AND type in (N'U'))
	 	  DROP TABLE [ECommerce_Product];
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getECommerceSectionId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getECommerceSectionId]

GO