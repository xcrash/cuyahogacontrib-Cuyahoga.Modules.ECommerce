/*
 *  Remove module definitions
 */
 
DELETE FROM cuyahoga_version WHERE assembly = 'Cuyahoga.Modules.ECommerce'

go
 
DELETE cuyahoga_modulesetting 
FROM cuyahoga_modulesetting ms
	INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce'

go

DELETE cuyahoga_moduleservice
FROM cuyahoga_moduleservice ms
	INNER JOIN cuyahoga_moduletype mt ON mt.moduletypeid = ms.moduletypeid AND mt.assemblyname = 'Cuyahoga.Modules.ECommerce'

go

DELETE FROM cuyahoga_moduletype
WHERE assemblyname = 'Cuyahoga.Modules.ECommerce'

go

/*
 *  Remove module specific tables
 */
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] DROP CONSTRAINT FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneCountry] DROP CONSTRAINT FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneState_ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneState] DROP CONSTRAINT FK_ECommerce_TaxZoneState_ECommerce_TaxZone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_UserDetail_ECommerce_AccountType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_UserDetail] DROP CONSTRAINT FK_ECommerce_UserDetail_ECommerce_AccountType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_OrderHeader_ECommerce_Address]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_ECommerce_OrderHeader_ECommerce_Address
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_OrderHeader_ECommerce_Address1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_ECommerce_OrderHeader_ECommerce_Address1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserDetails_address]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_UserDetail] DROP CONSTRAINT FK_UserDetails_address
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_AttributeOption_ECommerce_Attribute]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_AttributeOptionValue] DROP CONSTRAINT FK_ECommerce_AttributeOption_ECommerce_Attribute
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BasketItem_Basket]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_BasketItem] DROP CONSTRAINT FK_BasketItem_Basket
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrderHeader_Basket]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_OrderHeader_Basket
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_Payment_ECommerce_Basket]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Payment] DROP CONSTRAINT FK_ECommerce_Payment_ECommerce_Basket
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BasketItemAttributes_BasketItem]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_BasketItemAttribute] DROP CONSTRAINT FK_BasketItemAttributes_BasketItem
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_Category_ECommerce_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Category] DROP CONSTRAINT FK_ECommerce_Category_ECommerce_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_address_Countries]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Address] DROP CONSTRAINT FK_address_Countries
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_State_ECommerce_Country]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_State] DROP CONSTRAINT FK_ECommerce_State_ECommerce_Country
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TaxZoneCountries_Countries]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneCountry] DROP CONSTRAINT FK_TaxZoneCountries_Countries
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_Basket_ECommerce_Currency]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Basket] DROP CONSTRAINT FK_ECommerce_Basket_ECommerce_Currency
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_Country_ECommerce_Currency]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Country] DROP CONSTRAINT FK_ECommerce_Country_ECommerce_Currency
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_Payment_ECommerce_Currency]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Payment] DROP CONSTRAINT FK_ECommerce_Payment_ECommerce_Currency
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrderHeader_deliveryType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_OrderHeader_deliveryType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_OrderHeader_OrderStatus]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_OrderHeader_OrderStatus
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Payment_PaymentType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Payment] DROP CONSTRAINT FK_Payment_PaymentType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BasketItem_Products1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_BasketItem] DROP CONSTRAINT FK_BasketItem_Products1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductAttributeOptionValue] DROP CONSTRAINT FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductCategories_Products]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductCategory] DROP CONSTRAINT FK_ProductCategories_Products
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductImage_ECommerce_Product1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductImage] DROP CONSTRAINT FK_ECommerce_ProductImage_ECommerce_Product1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductRelation_ECommerce_Product]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductRelation] DROP CONSTRAINT FK_ECommerce_ProductRelation_ECommerce_Product
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductRelation_products]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductRelation] DROP CONSTRAINT FK_ProductRelation_products
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductSKU_Products]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductSKU] DROP CONSTRAINT FK_ProductSKU_Products
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductTaxClass_ECommerce_Product]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductTaxClass] DROP CONSTRAINT FK_ECommerce_ProductTaxClass_ECommerce_Product
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductRelation_RelationType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductRelation] DROP CONSTRAINT FK_ProductRelation_RelationType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_Address_ECommerce_State]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Address] DROP CONSTRAINT FK_ECommerce_Address_ECommerce_State
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_CountryStateBasedDelivery_States]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_CountryDeliveryState] DROP CONSTRAINT FK_CountryStateBasedDelivery_States
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_CountryStateWeightBasedDelivery_States]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_CountryDeliveryStateWeight] DROP CONSTRAINT FK_CountryStateWeightBasedDelivery_States
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TaxZoneStates_States]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneState] DROP CONSTRAINT FK_TaxZoneStates_States
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductTaxClass_ECommerce_TaxClass]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductTaxClass] DROP CONSTRAINT FK_ECommerce_ProductTaxClass_ECommerce_TaxClass
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneClassRate_ECommerce_TaxClass]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] DROP CONSTRAINT FK_ECommerce_TaxZoneClassRate_ECommerce_TaxClass
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TranslationText_TranslationTags]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TranslationText] DROP CONSTRAINT FK_TranslationText_TranslationTags
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductTaxClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductTaxClass]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZone]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZoneClassRate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZoneClassRate]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_AccountType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_AccountType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Address]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Address]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Attribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Attribute]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_AttributeGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_AttributeGroup]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_AttributeGroupAttribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_AttributeGroupAttribute]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_AttributeOptionValue]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_AttributeOptionValue]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Basket]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Basket]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_BasketItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_BasketItem]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_BasketItemAttribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_BasketItemAttribute]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Category]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Country]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Country]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CountryDeliveryState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CountryDeliveryState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CountryDeliveryStateWeight]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CountryDeliveryStateWeight]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Currency]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Currency]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_DeliveryType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_DeliveryType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_OrderHeader]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_OrderHeader]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_OrderStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_OrderStatus]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Payment]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_PaymentType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_PaymentType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Product]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Product]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductAttributeOptionValue]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductAttributeOptionValue]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductCategory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductCategory]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductImage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductImage]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductRelation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductRelation]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductSKU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductSKU]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_RelationType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_RelationType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_State]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxClass]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZoneCountry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZoneCountry]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZoneState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZoneState]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TranslationTag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TranslationTag]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TranslationText]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TranslationText]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_UserDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_UserDetail]
GO
