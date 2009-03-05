if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_OrderHeader_ECommerce_Address]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_ECommerce_OrderHeader_ECommerce_Address
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_OrderHeader_ECommerce_Address1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_OrderHeader] DROP CONSTRAINT FK_ECommerce_OrderHeader_ECommerce_Address1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserDetails_address]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_UserDetail] DROP CONSTRAINT FK_UserDetails_address
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BasketItem_Basket]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_BasketItem] DROP CONSTRAINT FK_BasketItem_Basket
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

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_CategoryLink_ECommerce_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_CategoryLink] DROP CONSTRAINT FK_Ecommerce_CategoryLink_ECommerce_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_CategoryPriceChange_ECommerce_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_CategoryPriceChange] DROP CONSTRAINT FK_ECommerce_CategoryPriceChange_ECommerce_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_Kit_ECommerce_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_Kit] DROP CONSTRAINT FK_Ecommerce_Kit_ECommerce_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductCategory_ECommerce_Category]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductCategory] DROP CONSTRAINT FK_ECommerce_ProductCategory_ECommerce_Category
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_address_Countries]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_Address] DROP CONSTRAINT FK_address_Countries
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_CountryDeliveryWeight_ECommerce_Country]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_CountryDeliveryWeight] DROP CONSTRAINT FK_ECommerce_CountryDeliveryWeight_ECommerce_Country
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

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_BasketItem_Products1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_BasketItem] DROP CONSTRAINT FK_BasketItem_Products1
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductAttributeOptionValue] DROP CONSTRAINT FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ProductCategories_Products]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_ProductCategory] DROP CONSTRAINT FK_ProductCategories_Products
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_ProductDocument_Ecommerce_Product]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_ProductDocument] DROP CONSTRAINT FK_Ecommerce_ProductDocument_Ecommerce_Product
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

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_ProductSynonym_Ecommerce_Product]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_ProductSynonym] DROP CONSTRAINT FK_Ecommerce_ProductSynonym_Ecommerce_Product
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

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] DROP CONSTRAINT FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneCountry] DROP CONSTRAINT FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ECommerce_TaxZoneState_ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TaxZoneState] DROP CONSTRAINT FK_ECommerce_TaxZoneState_ECommerce_TaxZone
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TranslationText_TranslationTags]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ECommerce_TranslationText] DROP CONSTRAINT FK_TranslationText_TranslationTags
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_AttributeOption_Ecommerce_Attribute]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_AttributeOptionValue] DROP CONSTRAINT FK_Ecommerce_AttributeOption_Ecommerce_Attribute
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_AttributeGroupAttribute_Ecommerce_AttributeGroup]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_AttributeGroupAttribute] DROP CONSTRAINT FK_Ecommerce_AttributeGroupAttribute_Ecommerce_AttributeGroup
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_Attribute_Ecommerce_AttributeType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_Attribute] DROP CONSTRAINT FK_Ecommerce_Attribute_Ecommerce_AttributeType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_ProductDocument_Ecommerce_Document]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_ProductDocument] DROP CONSTRAINT FK_Ecommerce_ProductDocument_Ecommerce_Document
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Ecommerce_Document_Ecommerce_DocumentType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Ecommerce_Document] DROP CONSTRAINT FK_Ecommerce_Document_Ecommerce_DocumentType
GO

/****** Object:  Table [dbo].[ECommerce_Address]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Address]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Address]
GO

/****** Object:  Table [dbo].[ECommerce_Basket]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Basket]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Basket]
GO

/****** Object:  Table [dbo].[ECommerce_BasketItem]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_BasketItem]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_BasketItem]
GO

/****** Object:  Table [dbo].[ECommerce_BasketItemAttribute]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_BasketItemAttribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_BasketItemAttribute]
GO

/****** Object:  Table [dbo].[ECommerce_Category]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Category]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Category]
GO

/****** Object:  Table [dbo].[ECommerce_CategoryPriceChange]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CategoryPriceChange]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CategoryPriceChange]
GO

/****** Object:  Table [dbo].[ECommerce_Charge]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Charge]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Charge]
GO

/****** Object:  Table [dbo].[ECommerce_Country]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Country]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Country]
GO

/****** Object:  Table [dbo].[ECommerce_CountryCharge]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CountryCharge]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CountryCharge]
GO

/****** Object:  Table [dbo].[ECommerce_CountryDeliveryState]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CountryDeliveryState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CountryDeliveryState]
GO

/****** Object:  Table [dbo].[ECommerce_CountryDeliveryStateWeight]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CountryDeliveryStateWeight]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CountryDeliveryStateWeight]
GO

/****** Object:  Table [dbo].[ECommerce_CountryDeliveryWeight]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_CountryDeliveryWeight]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_CountryDeliveryWeight]
GO

/****** Object:  Table [dbo].[ECommerce_Currency]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Currency]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Currency]
GO

/****** Object:  Table [dbo].[ECommerce_DeliveryType]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_DeliveryType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_DeliveryType]
GO

/****** Object:  Table [dbo].[ECommerce_OrderHeader]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_OrderHeader]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_OrderHeader]
GO

/****** Object:  Table [dbo].[ECommerce_Payment]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Payment]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Payment]
GO

/****** Object:  Table [dbo].[ECommerce_Product]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_Product]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_Product]
GO

/****** Object:  Table [dbo].[ECommerce_ProductAttributeOptionValue]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductAttributeOptionValue]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductAttributeOptionValue]
GO

/****** Object:  Table [dbo].[ECommerce_ProductCategory]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductCategory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductCategory]
GO

/****** Object:  Table [dbo].[ECommerce_ProductImage]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductImage]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductImage]
GO

/****** Object:  Table [dbo].[ECommerce_ProductRelation]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductRelation]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductRelation]
GO

/****** Object:  Table [dbo].[ECommerce_ProductSKU]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductSKU]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductSKU]
GO

/****** Object:  Table [dbo].[ECommerce_ProductTaxClass]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_ProductTaxClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_ProductTaxClass]
GO

/****** Object:  Table [dbo].[ECommerce_RelationType]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_RelationType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_RelationType]
GO

/****** Object:  Table [dbo].[ECommerce_State]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_State]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_State]
GO

/****** Object:  Table [dbo].[ECommerce_TaxClass]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxClass]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxClass]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZone]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZone]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZone]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZoneClassRate]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZoneClassRate]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZoneClassRate]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZoneCountry]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZoneCountry]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZoneCountry]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZoneState]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TaxZoneState]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TaxZoneState]
GO

/****** Object:  Table [dbo].[ECommerce_TranslationTag]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TranslationTag]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TranslationTag]
GO

/****** Object:  Table [dbo].[ECommerce_TranslationText]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_TranslationText]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_TranslationText]
GO

/****** Object:  Table [dbo].[ECommerce_UserDetail]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ECommerce_UserDetail]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ECommerce_UserDetail]
GO

/****** Object:  Table [dbo].[Ecommerce_Attribute]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_Attribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_Attribute]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeGroup]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_AttributeGroup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_AttributeGroup]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeGroupAttribute]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_AttributeGroupAttribute]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_AttributeGroupAttribute]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeOptionValue]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_AttributeOptionValue]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_AttributeOptionValue]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeType]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_AttributeType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_AttributeType]
GO

/****** Object:  Table [dbo].[Ecommerce_CategoryLink]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_CategoryLink]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_CategoryLink]
GO

/****** Object:  Table [dbo].[Ecommerce_Document]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_Document]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_Document]
GO

/****** Object:  Table [dbo].[Ecommerce_DocumentType]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_DocumentType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_DocumentType]
GO

/****** Object:  Table [dbo].[Ecommerce_Kit]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_Kit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_Kit]
GO

/****** Object:  Table [dbo].[Ecommerce_ProductDocument]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_ProductDocument]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_ProductDocument]
GO

/****** Object:  Table [dbo].[Ecommerce_ProductSynonym]    Script Date: 03/05/2008 00:29:36 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Ecommerce_ProductSynonym]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Ecommerce_ProductSynonym]
GO

/****** Object:  Table [dbo].[ECommerce_Address]    Script Date: 03/05/2008 00:29:45 ******/
CREATE TABLE [dbo].[ECommerce_Address] (
	[addressID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[isDeleted] [bit] NOT NULL ,
	[contactName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[addressLine1] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[addressLine2] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[addressLine3] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[state] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[postCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[county] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stateID] [smallint] NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Basket]    Script Date: 03/05/2008 00:29:45 ******/
CREATE TABLE [dbo].[ECommerce_Basket] (
	[basketID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[orderHeaderID] [bigint] NULL ,
	[userID] [int] NULL ,
	[altUserID] [int] NULL ,
	[currencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[taxPrice] [decimal](18, 4) NOT NULL ,
	[subtotalPrice] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL ,
	[deliveryCost] [decimal](18, 0) NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_BasketItem]    Script Date: 03/05/2008 00:29:45 ******/
CREATE TABLE [dbo].[ECommerce_BasketItem] (
	[basketItemID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[basketID] [bigint] NOT NULL ,
	[productID] [bigint] NULL ,
	[itemTax] [decimal](19, 4) NOT NULL ,
	[linePrice] [decimal](19, 4) NOT NULL ,
	[quantity] [int] NOT NULL ,
	[itemTypeID] [smallint] NOT NULL ,
	[pricingStatusID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_BasketItemAttribute]    Script Date: 03/05/2008 00:29:46 ******/
CREATE TABLE [dbo].[ECommerce_BasketItemAttribute] (
	[basketitemID] [bigint] NOT NULL ,
	[attributeID] [int] NOT NULL ,
	[optionValue] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[optionPrice] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Category]    Script Date: 03/05/2008 00:29:46 ******/
CREATE TABLE [dbo].[ECommerce_Category] (
	[categoryID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[categoryName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[categoryDescription] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[parentCategoryID] [bigint] NULL ,
	[sortOrder] [smallint] NOT NULL ,
	[isPublished] [bit] NOT NULL ,
	[imageUrl] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[width] [smallint] NULL ,
	[height] [smallint] NULL ,
	[altText] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[updateTimeStamp] [datetime] NOT NULL ,
	[insertTimeStamp] [datetime] NOT NULL ,
	[cssClass] [varchar] (128) COLLATE Latin1_General_CI_AS NULL ,
	[KitDescription] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL ,
	[KitPicture] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL ,
	[priceChangePercent] [decimal](18, 2) NULL ,
	[flashAnimationQuality] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL ,
	[flashAnimationUrl] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL ,
	[flashAnimationWidth] [smallint] NULL ,
	[flashAnimationAltText] [nvarchar] (128) COLLATE Latin1_General_CI_AS NULL ,
	[flashAnimationHeight] [smallint] NULL ,
	[flashAnimationBackgroundColour] [nvarchar] (7) COLLATE Latin1_General_CI_AS NULL ,
	[tylosandimageurl] [nvarchar] (255) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_CategoryPriceChange]    Script Date: 03/05/2008 00:29:46 ******/
CREATE TABLE [dbo].[ECommerce_CategoryPriceChange] (
	[categoryID] [bigint] NOT NULL ,
	[priceChangePercent] [decimal](18, 2) NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Charge]    Script Date: 03/05/2008 00:29:46 ******/
CREATE TABLE [dbo].[ECommerce_Charge] (
	[ChargeID] [int] IDENTITY (1, 1) NOT NULL ,
	[ChargeName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Country]    Script Date: 03/05/2008 00:29:46 ******/
CREATE TABLE [dbo].[ECommerce_Country] (
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[countryName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[defaultCurrencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_CountryCharge]    Script Date: 03/05/2008 00:29:46 ******/
CREATE TABLE [dbo].[ECommerce_CountryCharge] (
	[CountryCode] [char] (2) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ChargeID] [int] NOT NULL ,
	[Price] [decimal](18, 4) NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_CountryDeliveryState]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_CountryDeliveryState] (
	[stateID] [smallint] NOT NULL ,
	[price] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_CountryDeliveryStateWeight]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_CountryDeliveryStateWeight] (
	[stateID] [smallint] NOT NULL ,
	[weightLevel] [decimal](5, 2) NOT NULL ,
	[price] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_CountryDeliveryWeight]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_CountryDeliveryWeight] (
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[weightLevel] [decimal](5, 2) NOT NULL ,
	[price] [decimal](18, 4) NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Currency]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_Currency] (
	[currencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[exchangeRate] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_DeliveryType]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_DeliveryType] (
	[deliveryTypeID] [smallint] IDENTITY (1, 1) NOT NULL ,
	[name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[status] [bit] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_OrderHeader]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_OrderHeader] (
	[orderHeaderID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[purchaseOrderNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[orderStatusID] [smallint] NOT NULL ,
	[orderedDate] [datetime] NOT NULL ,
	[invoiceAddressID] [bigint] NULL ,
	[deliveryAddressID] [bigint] NULL ,
	[deliveryTypeID] [smallint] NULL ,
	[paymentMethodID] [smallint] NULL ,
	[comment] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Payment]    Script Date: 03/05/2008 00:29:47 ******/
CREATE TABLE [dbo].[ECommerce_Payment] (
	[paymentID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[basketID] [bigint] NOT NULL ,
	[paymentTypeID] [smallint] NOT NULL ,
	[paymentStatusID] [smallint] NOT NULL ,
	[paymentProviderID] [smallint] NOT NULL ,
	[currencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[amount] [decimal](19, 4) NOT NULL ,
	[transactionRef] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_Product]    Script Date: 03/05/2008 00:29:48 ******/
CREATE TABLE [dbo].[ECommerce_Product] (
	[productID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[itemCode] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[productName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[productDescription] [nvarchar] (4000) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stockLevel] [int] NOT NULL ,
	[isPublished] [bit] NOT NULL ,
	[basePrice] [decimal](18, 4) NOT NULL ,
	[baseCurrencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL ,
	[additionalInformation] [nvarchar] (1024) COLLATE Latin1_General_CI_AS NULL ,
	[productfamily] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[features] [nvarchar] (1024) COLLATE Latin1_General_CI_AS NULL ,
	[IsKit] [bit] NULL ,
	[basePriceDescription] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[shortProductDescription] [nvarchar] (512) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_ProductAttributeOptionValue]    Script Date: 03/05/2008 00:29:48 ******/
CREATE TABLE [dbo].[ECommerce_ProductAttributeOptionValue] (
	[productID] [bigint] NOT NULL ,
	[optionValueID] [bigint] NOT NULL ,
	[optionPrice] [decimal](19, 4) NOT NULL ,
	[optionValueCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[sortOrder] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_ProductCategory]    Script Date: 03/05/2008 00:29:48 ******/
CREATE TABLE [dbo].[ECommerce_ProductCategory] (
	[categoryID] [bigint] NOT NULL ,
	[productID] [bigint] NOT NULL ,
	[sortOrder] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_ProductImage]    Script Date: 03/05/2008 00:29:48 ******/
CREATE TABLE [dbo].[ECommerce_ProductImage] (
	[imageID] [int] IDENTITY (1, 1) NOT NULL ,
	[productID] [bigint] NOT NULL ,
	[imageUrl] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[width] [smallint] NOT NULL ,
	[height] [smallint] NOT NULL ,
	[altText] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[imageType] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_ProductRelation]    Script Date: 03/05/2008 00:29:49 ******/
CREATE TABLE [dbo].[ECommerce_ProductRelation] (
	[productID] [bigint] NOT NULL ,
	[parentID] [bigint] NOT NULL ,
	[relationTypeID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_ProductSKU]    Script Date: 03/05/2008 00:29:49 ******/
CREATE TABLE [dbo].[ECommerce_ProductSKU] (
	[sku] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[productID] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_ProductTaxClass]    Script Date: 03/05/2008 00:29:50 ******/
CREATE TABLE [dbo].[ECommerce_ProductTaxClass] (
	[productID] [bigint] NOT NULL ,
	[taxClassID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_RelationType]    Script Date: 03/05/2008 00:29:50 ******/
CREATE TABLE [dbo].[ECommerce_RelationType] (
	[relationTypeID] [smallint] NOT NULL ,
	[relationshipName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[relationshipDescription] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_State]    Script Date: 03/05/2008 00:29:50 ******/
CREATE TABLE [dbo].[ECommerce_State] (
	[stateID] [smallint] NOT NULL ,
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stateCode] [nvarchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stateName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TaxClass]    Script Date: 03/05/2008 00:29:50 ******/
CREATE TABLE [dbo].[ECommerce_TaxClass] (
	[taxClassID] [smallint] NOT NULL ,
	[taxClassName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZone]    Script Date: 03/05/2008 00:29:51 ******/
CREATE TABLE [dbo].[ECommerce_TaxZone] (
	[taxZoneID] [smallint] NOT NULL ,
	[taxZoneName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZoneClassRate]    Script Date: 03/05/2008 00:29:51 ******/
CREATE TABLE [dbo].[ECommerce_TaxZoneClassRate] (
	[taxZoneID] [smallint] NOT NULL ,
	[taxClassID] [smallint] NOT NULL ,
	[taxRate] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZoneCountry]    Script Date: 03/05/2008 00:29:51 ******/
CREATE TABLE [dbo].[ECommerce_TaxZoneCountry] (
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[taxZoneID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TaxZoneState]    Script Date: 03/05/2008 00:29:51 ******/
CREATE TABLE [dbo].[ECommerce_TaxZoneState] (
	[stateID] [smallint] NOT NULL ,
	[taxZoneID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TranslationTag]    Script Date: 03/05/2008 00:29:52 ******/
CREATE TABLE [dbo].[ECommerce_TranslationTag] (
	[tagReference] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[tagID] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_TranslationText]    Script Date: 03/05/2008 00:29:52 ******/
CREATE TABLE [dbo].[ECommerce_TranslationText] (
	[tagID] [bigint] NOT NULL ,
	[cultureCode] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[textValue] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ECommerce_UserDetail]    Script Date: 03/05/2008 00:29:52 ******/
CREATE TABLE [dbo].[ECommerce_UserDetail] (
	[userID] [int] IDENTITY (1, 1) NOT NULL ,
	[accountTypeID] [smallint] NOT NULL ,
	[addressID] [bigint] NULL ,
	[firstName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[lastName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[emailAddress] [varchar] (200) COLLATE Latin1_General_CI_AS NULL ,
	[telephoneNumber] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[companyName] [nvarchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[accountNumber] [varchar] (100) COLLATE Latin1_General_CI_AS NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_Attribute]    Script Date: 03/05/2008 00:29:53 ******/
CREATE TABLE [dbo].[Ecommerce_Attribute] (
	[attributeID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[attributeReference] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[attributeDescription] [nvarchar] (128) COLLATE Latin1_General_CI_AS NOT NULL ,
	[isDisplayable] [bit] NULL ,
	[baseUnit] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[TypeID] [int] NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeGroup]    Script Date: 03/05/2008 00:29:53 ******/
CREATE TABLE [dbo].[Ecommerce_AttributeGroup] (
	[attributeGroupID] [smallint] IDENTITY (1, 1) NOT NULL ,
	[attributeGroupName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeGroupAttribute]    Script Date: 03/05/2008 00:29:53 ******/
CREATE TABLE [dbo].[Ecommerce_AttributeGroupAttribute] (
	[attributeGroupID] [smallint] NOT NULL ,
	[attributeID] [int] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeOptionValue]    Script Date: 03/05/2008 00:29:54 ******/
CREATE TABLE [dbo].[Ecommerce_AttributeOptionValue] (
	[attributeID] [bigint] NOT NULL ,
	[optionID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[optionName] [nvarchar] (150) COLLATE Latin1_General_CI_AS NOT NULL ,
	[optionData] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_AttributeType]    Script Date: 03/05/2008 00:29:54 ******/
CREATE TABLE [dbo].[Ecommerce_AttributeType] (
	[TypeID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_CategoryLink]    Script Date: 03/05/2008 00:29:54 ******/
CREATE TABLE [dbo].[Ecommerce_CategoryLink] (
	[categoryLinkID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[Categoryid] [bigint] NOT NULL ,
	[nodeID] [char] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ImageUrl] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[Title] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_Document]    Script Date: 03/05/2008 00:29:55 ******/
CREATE TABLE [dbo].[Ecommerce_Document] (
	[DocumentID] [int] IDENTITY (1, 1) NOT NULL ,
	[DocumentName] [nvarchar] (128) COLLATE Latin1_General_CI_AS NOT NULL ,
	[FilePath] [nvarchar] (1024) COLLATE Latin1_General_CI_AS NOT NULL ,
	[TypeID] [int] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL ,
	[isPublished] [bit] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_DocumentType]    Script Date: 03/05/2008 00:29:55 ******/
CREATE TABLE [dbo].[Ecommerce_DocumentType] (
	[TypeID] [int] IDENTITY (1, 1) NOT NULL ,
	[TypeName] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[CssClass] [nvarchar] (50) COLLATE Latin1_General_CI_AS NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_Kit]    Script Date: 03/05/2008 00:29:55 ******/
CREATE TABLE [dbo].[Ecommerce_Kit] (
	[KitID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[_Productid] [bigint] NOT NULL ,
	[categoryID] [bigint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_ProductDocument]    Script Date: 03/05/2008 00:29:55 ******/
CREATE TABLE [dbo].[Ecommerce_ProductDocument] (
	[ProductID] [bigint] NOT NULL ,
	[DocumentID] [int] NOT NULL ,
	[updatetimestamp] [datetime] NULL ,
	[inserttimestamp] [datetime] NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ecommerce_ProductSynonym]    Script Date: 03/05/2008 00:29:55 ******/
CREATE TABLE [dbo].[Ecommerce_ProductSynonym] (
	[ProductID] [bigint] NOT NULL ,
	[AlternativePhrase] [nvarchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ECommerce_Address] WITH NOCHECK ADD 
	CONSTRAINT [PK_address] PRIMARY KEY  CLUSTERED 
	(
		[addressID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Basket] WITH NOCHECK ADD 
	CONSTRAINT [PK_Basket] PRIMARY KEY  CLUSTERED 
	(
		[basketID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_BasketItem] WITH NOCHECK ADD 
	CONSTRAINT [PK_BasketItem] PRIMARY KEY  CLUSTERED 
	(
		[basketItemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_BasketItemAttribute] WITH NOCHECK ADD 
	CONSTRAINT [PK_BasketItemAttributes] PRIMARY KEY  CLUSTERED 
	(
		[basketitemID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Category] WITH NOCHECK ADD 
	CONSTRAINT [PK_ProductCategory] PRIMARY KEY  CLUSTERED 
	(
		[categoryID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_CategoryPriceChange] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_CategoryPriceChange] PRIMARY KEY  CLUSTERED 
	(
		[categoryID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Country] WITH NOCHECK ADD 
	CONSTRAINT [PK_Countries] PRIMARY KEY  CLUSTERED 
	(
		[countryCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_CountryCharge] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_CountryCharge] PRIMARY KEY  CLUSTERED 
	(
		[CountryCode],
		[ChargeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryState] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_CountryDeliveryState] PRIMARY KEY  CLUSTERED 
	(
		[stateID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryStateWeight] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_CountryDeliveryStateWeight] PRIMARY KEY  CLUSTERED 
	(
		[stateID],
		[weightLevel]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryWeight] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_CountryDeliveryWeight] PRIMARY KEY  CLUSTERED 
	(
		[countryCode],
		[weightLevel]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Currency] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_Currency] PRIMARY KEY  CLUSTERED 
	(
		[currencyCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_DeliveryType] WITH NOCHECK ADD 
	CONSTRAINT [PK_deliveryType] PRIMARY KEY  CLUSTERED 
	(
		[deliveryTypeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_OrderHeader] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_OrderHeader] PRIMARY KEY  CLUSTERED 
	(
		[orderHeaderID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Payment] WITH NOCHECK ADD 
	CONSTRAINT [PK_Payment] PRIMARY KEY  CLUSTERED 
	(
		[paymentID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Product] WITH NOCHECK ADD 
	CONSTRAINT [PK_products] PRIMARY KEY  CLUSTERED 
	(
		[productID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductAttributeOptionValue] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_ProductAttributeOptionValue] PRIMARY KEY  CLUSTERED 
	(
		[productID],
		[optionValueID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductCategory] WITH NOCHECK ADD 
	CONSTRAINT [PK_ProductCategories] PRIMARY KEY  CLUSTERED 
	(
		[categoryID],
		[productID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductImage] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_ProductImage] PRIMARY KEY  CLUSTERED 
	(
		[imageID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductRelation] WITH NOCHECK ADD 
	CONSTRAINT [PK_ProductRelation] PRIMARY KEY  CLUSTERED 
	(
		[productID],
		[parentID],
		[relationTypeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductSKU] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_ProductSKU] PRIMARY KEY  CLUSTERED 
	(
		[sku]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductTaxClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_ProductTaxClass] PRIMARY KEY  CLUSTERED 
	(
		[productID],
		[taxClassID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_RelationType] WITH NOCHECK ADD 
	CONSTRAINT [PK_RelationType] PRIMARY KEY  CLUSTERED 
	(
		[relationTypeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_State] WITH NOCHECK ADD 
	CONSTRAINT [PK_States] PRIMARY KEY  CLUSTERED 
	(
		[stateID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TaxClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_TaxRates] PRIMARY KEY  CLUSTERED 
	(
		[taxClassID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TaxZone] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_TaxZone] PRIMARY KEY  CLUSTERED 
	(
		[taxZoneID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_TaxZoneClassRate] PRIMARY KEY  CLUSTERED 
	(
		[taxZoneID],
		[taxClassID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneCountry] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_TaxZoneCountry] PRIMARY KEY  CLUSTERED 
	(
		[countryCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneState] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_TaxZoneState] PRIMARY KEY  CLUSTERED 
	(
		[stateID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TranslationTag] WITH NOCHECK ADD 
	CONSTRAINT [PK_TranslationTags] PRIMARY KEY  CLUSTERED 
	(
		[tagID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TranslationText] WITH NOCHECK ADD 
	CONSTRAINT [PK_TranslationText] PRIMARY KEY  CLUSTERED 
	(
		[cultureCode],
		[tagID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_UserDetail] WITH NOCHECK ADD 
	CONSTRAINT [PK_UserDetails] PRIMARY KEY  CLUSTERED 
	(
		[userID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_Attribute] WITH NOCHECK ADD 
	CONSTRAINT [PK_attributes] PRIMARY KEY  CLUSTERED 
	(
		[attributeID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_AttributeGroup] WITH NOCHECK ADD 
	CONSTRAINT [PK_AttributeGroups_1] PRIMARY KEY  CLUSTERED 
	(
		[attributeGroupID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_AttributeGroupAttribute] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ecommerce_AttributeGroupAttribute] PRIMARY KEY  CLUSTERED 
	(
		[attributeGroupID],
		[attributeID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_AttributeOptionValue] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ecommerce_AttributeOption] PRIMARY KEY  CLUSTERED 
	(
		[optionID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_AttributeType] WITH NOCHECK ADD 
	CONSTRAINT [PK_EcommerceAttributeType] PRIMARY KEY  CLUSTERED 
	(
		[TypeID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_CategoryLink] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ecommerce_CategoryLink] PRIMARY KEY  CLUSTERED 
	(
		[categoryLinkID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_Document] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ecommerce_Documents] PRIMARY KEY  CLUSTERED 
	(
		[DocumentID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_DocumentType] WITH NOCHECK ADD 
	CONSTRAINT [PK_DocumentType] PRIMARY KEY  CLUSTERED 
	(
		[TypeID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_Kit] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ecommerce_Kit] PRIMARY KEY  CLUSTERED 
	(
		[KitID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_ProductDocument] WITH NOCHECK ADD 
	CONSTRAINT [PK_Ecommerce_ProductDocuments] PRIMARY KEY  CLUSTERED 
	(
		[ProductID],
		[DocumentID]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_ProductSynonym] WITH NOCHECK ADD 
	CONSTRAINT [PK_ProductSynonym] PRIMARY KEY  CLUSTERED 
	(
		[ProductID],
		[AlternativePhrase]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Address] ADD 
	CONSTRAINT [DF_ECommerce_Address_isDeleted] DEFAULT (0) FOR [isDeleted],
	CONSTRAINT [DF_ECommerce_Address_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Address_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Basket] ADD 
	CONSTRAINT [DF_ECommerce_Basket_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Basket_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_BasketItem] ADD 
	CONSTRAINT [DF_ECommerce_BasketItem_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_BasketItem_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_BasketItemAttribute] ADD 
	CONSTRAINT [DF_ECommerce_BasketItemAttribute_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_BasketItemAttribute_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Category] ADD 
	CONSTRAINT [DF_ECommerce_Category_categoryDescription] DEFAULT ('') FOR [categoryDescription],
	CONSTRAINT [DF_ECommerce_Category_isPublished] DEFAULT (0) FOR [isPublished],
	CONSTRAINT [DF_ECommerce_Category_updatetimestamp] DEFAULT (getdate()) FOR [updateTimeStamp],
	CONSTRAINT [DF_ECommerce_Category_inserttimestamp] DEFAULT (getdate()) FOR [insertTimeStamp]
GO

ALTER TABLE [dbo].[ECommerce_Country] ADD 
	CONSTRAINT [DF_ECommerce_Country_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Country_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryState] ADD 
	CONSTRAINT [DF_ECommerce_CountryDeliveryState_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_CountryDeliveryState_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryStateWeight] ADD 
	CONSTRAINT [DF_ECommerce_CountryDeliveryStateWeight_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_CountryDeliveryStateWeight_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Currency] ADD 
	CONSTRAINT [DF_ECommerce_Currency_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Currency_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_DeliveryType] ADD 
	CONSTRAINT [DF_ECommerce_DeliveryType_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_DeliveryType_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_OrderHeader] ADD 
	CONSTRAINT [DF_ECommerce_OrderHeader_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_OrderHeader_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Payment] ADD 
	CONSTRAINT [DF_ECommerce_Payment_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Payment_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Product] ADD 
	CONSTRAINT [DF_ECommerce_Product_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Product_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp],
	CONSTRAINT [IX_ECommerce_Product] UNIQUE  NONCLUSTERED 
	(
		[itemCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_ProductAttributeOptionValue] ADD 
	CONSTRAINT [DF_ECommerce_ProductAttributeOptionValue_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductAttributeOptionValue_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_ProductCategory] ADD 
	CONSTRAINT [DF_ECommerce_ProductCategory_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductCategory_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_ProductImage] ADD 
	CONSTRAINT [DF_ECommerce_ProductImage_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductImage_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_ProductRelation] ADD 
	CONSTRAINT [DF_ECommerce_ProductRelation_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductRelation_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_ProductSKU] ADD 
	CONSTRAINT [DF_ECommerce_ProductSKU_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductSKU_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_ProductTaxClass] ADD 
	CONSTRAINT [DF_ECommerce_ProductTaxClass_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductTaxClass_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_State] ADD 
	CONSTRAINT [DF_ECommerce_State_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_State_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp],
	CONSTRAINT [IX_ECommerce_State] UNIQUE  NONCLUSTERED 
	(
		[countryCode],
		[stateCode]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TaxClass] ADD 
	CONSTRAINT [DF_ECommerce_TaxClass_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxClass_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TaxZone] ADD 
	CONSTRAINT [DF_ECommerce_TaxZone_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxZone_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] ADD 
	CONSTRAINT [DF_ECommerce_TaxZoneClassRate_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxZoneClassRate_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneCountry] ADD 
	CONSTRAINT [DF_ECommerce_TaxZoneCountry_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxZoneCountry_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneState] ADD 
	CONSTRAINT [DF_ECommerce_TaxZoneState_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxZoneState_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TranslationTag] ADD 
	CONSTRAINT [DF_ECommerce_TranslationTag_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TranslationTag_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp],
	CONSTRAINT [IX_ECommerce_TranslationTag] UNIQUE  NONCLUSTERED 
	(
		[tagReference]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_TranslationText] ADD 
	CONSTRAINT [DF_ECommerce_TranslationText_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TranslationText_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_UserDetail] ADD 
	CONSTRAINT [DF_ECommerce_UserDetail_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_UserDetail_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[Ecommerce_Attribute] ADD 
	CONSTRAINT [IX_Ecommerce_Attribute] UNIQUE  NONCLUSTERED 
	(
		[attributeReference]
	) WITH  FILLFACTOR = 90  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Ecommerce_AttributeGroupAttribute] ADD 
	CONSTRAINT [DF_Ecommerce_AttributeGroupAttribute_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_Ecommerce_AttributeGroupAttribute_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

 CREATE  INDEX [IX_Ecommerce_AttributeOptionValue] ON [dbo].[Ecommerce_AttributeOptionValue]([attributeID], [optionName]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Ecommerce_Document] ADD 
	CONSTRAINT [DF_Ecommerce_Document_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_Ecommerce_Document_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Address] ADD 
	CONSTRAINT [FK_address_Countries] FOREIGN KEY 
	(
		[countryCode]
	) REFERENCES [dbo].[ECommerce_Country] (
		[countryCode]
	),
	CONSTRAINT [FK_ECommerce_Address_ECommerce_State] FOREIGN KEY 
	(
		[stateID]
	) REFERENCES [dbo].[ECommerce_State] (
		[stateID]
	)
GO

ALTER TABLE [dbo].[ECommerce_Basket] ADD 
	CONSTRAINT [FK_ECommerce_Basket_cuyahoga_user] FOREIGN KEY 
	(
		[userID]
	) REFERENCES [dbo].[cuyahoga_user] (
		[userid]
	),
	CONSTRAINT [FK_ECommerce_Basket_ECommerce_Currency] FOREIGN KEY 
	(
		[currencyCode]
	) REFERENCES [dbo].[ECommerce_Currency] (
		[currencyCode]
	)
GO

ALTER TABLE [dbo].[ECommerce_BasketItem] ADD 
	CONSTRAINT [FK_BasketItem_Basket] FOREIGN KEY 
	(
		[basketID]
	) REFERENCES [dbo].[ECommerce_Basket] (
		[basketID]
	),
	CONSTRAINT [FK_BasketItem_Products1] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO

ALTER TABLE [dbo].[ECommerce_BasketItemAttribute] ADD 
	CONSTRAINT [FK_BasketItemAttributes_BasketItem] FOREIGN KEY 
	(
		[basketitemID]
	) REFERENCES [dbo].[ECommerce_BasketItem] (
		[basketItemID]
	)
GO

ALTER TABLE [dbo].[ECommerce_Category] ADD 
	CONSTRAINT [FK_ECommerce_Category_ECommerce_Category] FOREIGN KEY 
	(
		[parentCategoryID]
	) REFERENCES [dbo].[ECommerce_Category] (
		[categoryID]
	)
GO

ALTER TABLE [dbo].[ECommerce_CategoryPriceChange] ADD 
	CONSTRAINT [FK_ECommerce_CategoryPriceChange_ECommerce_Category] FOREIGN KEY 
	(
		[categoryID]
	) REFERENCES [dbo].[ECommerce_Category] (
		[categoryID]
	)
GO

ALTER TABLE [dbo].[ECommerce_Country] ADD 
	CONSTRAINT [FK_ECommerce_Country_ECommerce_Currency] FOREIGN KEY 
	(
		[defaultCurrencyCode]
	) REFERENCES [dbo].[ECommerce_Currency] (
		[currencyCode]
	)
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryState] ADD 
	CONSTRAINT [FK_CountryStateBasedDelivery_States] FOREIGN KEY 
	(
		[stateID]
	) REFERENCES [dbo].[ECommerce_State] (
		[stateID]
	)
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryStateWeight] ADD 
	CONSTRAINT [FK_CountryStateWeightBasedDelivery_States] FOREIGN KEY 
	(
		[stateID]
	) REFERENCES [dbo].[ECommerce_State] (
		[stateID]
	)
GO

ALTER TABLE [dbo].[ECommerce_CountryDeliveryWeight] ADD 
	CONSTRAINT [FK_ECommerce_CountryDeliveryWeight_ECommerce_Country] FOREIGN KEY 
	(
		[countryCode]
	) REFERENCES [dbo].[ECommerce_Country] (
		[countryCode]
	)
GO

ALTER TABLE [dbo].[ECommerce_OrderHeader] ADD 
	CONSTRAINT [FK_ECommerce_OrderHeader_ECommerce_Address] FOREIGN KEY 
	(
		[invoiceAddressID]
	) REFERENCES [dbo].[ECommerce_Address] (
		[addressID]
	),
	CONSTRAINT [FK_ECommerce_OrderHeader_ECommerce_Address1] FOREIGN KEY 
	(
		[deliveryAddressID]
	) REFERENCES [dbo].[ECommerce_Address] (
		[addressID]
	),
	CONSTRAINT [FK_OrderHeader_deliveryType] FOREIGN KEY 
	(
		[deliveryTypeID]
	) REFERENCES [dbo].[ECommerce_DeliveryType] (
		[deliveryTypeID]
	)
GO

ALTER TABLE [dbo].[ECommerce_Payment] ADD 
	CONSTRAINT [FK_ECommerce_Payment_ECommerce_Basket] FOREIGN KEY 
	(
		[basketID]
	) REFERENCES [dbo].[ECommerce_Basket] (
		[basketID]
	),
	CONSTRAINT [FK_ECommerce_Payment_ECommerce_Currency] FOREIGN KEY 
	(
		[currencyCode]
	) REFERENCES [dbo].[ECommerce_Currency] (
		[currencyCode]
	)
GO

ALTER TABLE [dbo].[ECommerce_ProductAttributeOptionValue] ADD 
	CONSTRAINT [FK_ECommerce_ProductAttributeOptionValue_ECommerce_Product] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO

ALTER TABLE [dbo].[ECommerce_ProductCategory] ADD 
	CONSTRAINT [FK_ECommerce_ProductCategory_ECommerce_Category] FOREIGN KEY 
	(
		[categoryID]
	) REFERENCES [dbo].[ECommerce_Category] (
		[categoryID]
	),
	CONSTRAINT [FK_ProductCategories_Products] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO

ALTER TABLE [dbo].[ECommerce_ProductImage] ADD 
	CONSTRAINT [FK_ECommerce_ProductImage_ECommerce_Product1] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO

ALTER TABLE [dbo].[ECommerce_ProductRelation] ADD 
	CONSTRAINT [FK_ECommerce_ProductRelation_ECommerce_Product] FOREIGN KEY 
	(
		[parentID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	),
	CONSTRAINT [FK_ProductRelation_products] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	),
	CONSTRAINT [FK_ProductRelation_RelationType] FOREIGN KEY 
	(
		[relationTypeID]
	) REFERENCES [dbo].[ECommerce_RelationType] (
		[relationTypeID]
	)
GO

ALTER TABLE [dbo].[ECommerce_ProductSKU] ADD 
	CONSTRAINT [FK_ProductSKU_Products] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO

ALTER TABLE [dbo].[ECommerce_ProductTaxClass] ADD 
	CONSTRAINT [FK_ECommerce_ProductTaxClass_ECommerce_Product] FOREIGN KEY 
	(
		[productID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	),
	CONSTRAINT [FK_ECommerce_ProductTaxClass_ECommerce_TaxClass] FOREIGN KEY 
	(
		[taxClassID]
	) REFERENCES [dbo].[ECommerce_TaxClass] (
		[taxClassID]
	)
GO

ALTER TABLE [dbo].[ECommerce_State] ADD 
	CONSTRAINT [FK_ECommerce_State_ECommerce_Country] FOREIGN KEY 
	(
		[countryCode]
	) REFERENCES [dbo].[ECommerce_Country] (
		[countryCode]
	)
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] ADD 
	CONSTRAINT [FK_ECommerce_TaxZoneClassRate_ECommerce_TaxClass] FOREIGN KEY 
	(
		[taxClassID]
	) REFERENCES [dbo].[ECommerce_TaxClass] (
		[taxClassID]
	),
	CONSTRAINT [FK_ECommerce_TaxZoneClassRate_ECommerce_TaxZone] FOREIGN KEY 
	(
		[taxZoneID]
	) REFERENCES [dbo].[ECommerce_TaxZone] (
		[taxZoneID]
	)
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneCountry] ADD 
	CONSTRAINT [FK_ECommerce_TaxZoneCountry_ECommerce_TaxZone] FOREIGN KEY 
	(
		[taxZoneID]
	) REFERENCES [dbo].[ECommerce_TaxZone] (
		[taxZoneID]
	),
	CONSTRAINT [FK_TaxZoneCountries_Countries] FOREIGN KEY 
	(
		[countryCode]
	) REFERENCES [dbo].[ECommerce_Country] (
		[countryCode]
	)
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneState] ADD 
	CONSTRAINT [FK_ECommerce_TaxZoneState_ECommerce_TaxZone] FOREIGN KEY 
	(
		[taxZoneID]
	) REFERENCES [dbo].[ECommerce_TaxZone] (
		[taxZoneID]
	),
	CONSTRAINT [FK_TaxZoneStates_States] FOREIGN KEY 
	(
		[stateID]
	) REFERENCES [dbo].[ECommerce_State] (
		[stateID]
	)
GO

ALTER TABLE [dbo].[ECommerce_TranslationText] ADD 
	CONSTRAINT [FK_TranslationText_TranslationTags] FOREIGN KEY 
	(
		[tagID]
	) REFERENCES [dbo].[ECommerce_TranslationTag] (
		[tagID]
	)
GO

ALTER TABLE [dbo].[ECommerce_UserDetail] ADD 
	CONSTRAINT [FK_UserDetails_address] FOREIGN KEY 
	(
		[addressID]
	) REFERENCES [dbo].[ECommerce_Address] (
		[addressID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_Attribute] ADD 
	CONSTRAINT [FK_Ecommerce_Attribute_Ecommerce_AttributeType] FOREIGN KEY 
	(
		[TypeID]
	) REFERENCES [dbo].[Ecommerce_AttributeType] (
		[TypeID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_AttributeGroupAttribute] ADD 
	CONSTRAINT [FK_Ecommerce_AttributeGroupAttribute_Ecommerce_AttributeGroup] FOREIGN KEY 
	(
		[attributeGroupID]
	) REFERENCES [dbo].[Ecommerce_AttributeGroup] (
		[attributeGroupID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_AttributeOptionValue] ADD 
	CONSTRAINT [FK_Ecommerce_AttributeOption_Ecommerce_Attribute] FOREIGN KEY 
	(
		[attributeID]
	) REFERENCES [dbo].[Ecommerce_Attribute] (
		[attributeID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_CategoryLink] ADD 
	CONSTRAINT [FK_Ecommerce_CategoryLink_ECommerce_Category] FOREIGN KEY 
	(
		[Categoryid]
	) REFERENCES [dbo].[ECommerce_Category] (
		[categoryID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_Document] ADD 
	CONSTRAINT [FK_Ecommerce_Document_Ecommerce_DocumentType] FOREIGN KEY 
	(
		[TypeID]
	) REFERENCES [dbo].[Ecommerce_DocumentType] (
		[TypeID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_Kit] ADD 
	CONSTRAINT [FK_Ecommerce_Kit_ECommerce_Category] FOREIGN KEY 
	(
		[categoryID]
	) REFERENCES [dbo].[ECommerce_Category] (
		[categoryID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_ProductDocument] ADD 
	CONSTRAINT [FK_Ecommerce_ProductDocument_Ecommerce_Document] FOREIGN KEY 
	(
		[DocumentID]
	) REFERENCES [dbo].[Ecommerce_Document] (
		[DocumentID]
	),
	CONSTRAINT [FK_Ecommerce_ProductDocument_Ecommerce_Product] FOREIGN KEY 
	(
		[ProductID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO

ALTER TABLE [dbo].[Ecommerce_ProductSynonym] ADD 
	CONSTRAINT [FK_Ecommerce_ProductSynonym_Ecommerce_Product] FOREIGN KEY 
	(
		[ProductID]
	) REFERENCES [dbo].[ECommerce_Product] (
		[productID]
	)
GO



DECLARE @moduletypeid int

INSERT INTO cuyahoga_moduletype (name, assemblyname, classname, path, editpath, inserttimestamp, updatetimestamp) VALUES ('ECommerce', 'Cuyahoga.Modules.ECommerce', 'Cuyahoga.Modules.ECommerce.ECommerceModule', 'Modules/ECommerce/Views/BasketView.ascx', 'Modules/ECommerce/Admin/Default.aspx', getdate(), getdate())

SELECT @moduletypeid = Scope_Identity()

--Add settings
INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, 'DISPLAY_MODE', 'Display Mode', 'Cuyahoga.Modules.ECommerce.Util.Enums.DisplayMode', 1, 1)

-- Register services
INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'ecommerce.catalogueviewer', 'Cuyahoga.Modules.ECommerce.Service.ICatalogueViewService,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Service.CatalogueViewService,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'ecommerce.catalogueModifier', 'Cuyahoga.Modules.ECommerce.Service.ICatalogueModificationService,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Service.CatalogueModificationService,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'ecommerce.commerceservice', 'Cuyahoga.Modules.ECommerce.Service.ICommerceService,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Service.CommerceService,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'ecommerce.commercedao', 'Cuyahoga.Modules.ECommerce.DataAccess.ICommerceDao,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.DataAccess.CommerceDao,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)
VALUES (@moduletypeid, 'ecommerce.commondao', 'Cuyahoga.Modules.ECommerce.DataAccess.IExtCommonDao,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.DataAccess.ExtCommonDao,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype) VALUES (@moduletypeid, 'ecommerce.processor',
'Cuyahoga.Modules.ECommerce.Service.OrderProcessor.IOrderProcessorFactory,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Service.OrderProcessor.SimpleOrderProcessorFactory,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype) VALUES (@moduletypeid, 'ecommerce.rules',
'Cuyahoga.Modules.ECommerce.Util.Interfaces.IBasketRules,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Util.GenericBasketRules,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype) VALUES (@moduletypeid, 'ecommerce.payment',
'Cuyahoga.Modules.ECommerce.Service.PaymentProvider.IPaymentProvider,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Service.PaymentProvider.TestPaymentProvider,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype) VALUES (@moduletypeid, 'ecommerce.accountservice',
'Cuyahoga.Modules.ECommerce.Service.IAccountService,Cuyahoga.Modules.ECommerce',
'Cuyahoga.Modules.ECommerce.Service.AccountService,Cuyahoga.Modules.ECommerce')

INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype) VALUES (@moduletypeid, 'ecommerce.orderservice',
'Cuyahoga.Modules.ECommerce.Service.IOrderService,Cuyahoga.Modules.ECommerce', 'Cuyahoga.Modules.ECommerce.Service.OrderService,Cuyahoga.Modules.ECommerce')

go 
 

INSERT INTO cuyahoga_version (assembly, major, minor, patch) VALUES ('Cuyahoga.Modules.ECommerce', 1, 5, 0)
go


if not exists (select * from ECommerce_Currency where currencycode = 'GBP')
insert into ECommerce_Currency (currencyCode, exchangeRate) values ('GBP', 1)
if not exists (select * from ECommerce_Currency where currencycode = 'USD')
insert into ECommerce_Currency (currencyCode, exchangeRate) values ('USD', 1.6)
if not exists (select * from ECommerce_Currency where currencycode = 'EUR')
insert into ECommerce_Currency (currencyCode, exchangeRate) values ('EUR', 1.6)

if not exists (select * from ECommerce_Country where countrycode = 'US')
insert into ECommerce_Country (countrycode, countryname, defaultCurrencyCode) values ('US', 'United States', 'USD')
if not exists (select * from ECommerce_Country where countrycode = 'GB')
insert into ECommerce_Country (countrycode, countryname, defaultCurrencyCode) values ('GB', 'United Kingdom', 'GBP')

declare @rootID bigint

set @rootID = (select categoryID from ECommerce_Category where parentCategoryID is null)

if @rootID is null
begin
  insert into ECommerce_Category(categoryname, categorydescription, sortorder, ispublished) values ('Root Category', 'Root Category Description', 1, 1)
  set @rootID = @@IDENTITY
end

declare @catID1 bigint
insert into ECommerce_Category(categoryname, categorydescription, parentcategoryID, sortorder, ispublished) values ('Category 1', 'Category 1 Description', @rootID, 1, 1)
set @catID1 = @@IDENTITY

declare @catID2 bigint
insert into ECommerce_Category(categoryname, categorydescription, parentcategoryID, sortorder, ispublished) values ('Category 2', 'Category 1 Description', @rootID, 2, 1)
set @catID2 = @@IDENTITY

declare @catID3 bigint
insert into ECommerce_Category(categoryname, categorydescription, parentcategoryID, sortorder, ispublished) values ('Category 3', 'Category 1 Description', @rootID, 3, 1)
set @catID3 = @@IDENTITY

-- Create some random products
declare @itemCode char(7)
declare @loopCounter int
set @loopcounter = 0

set nocount on
while @loopCounter < 100
begin
set @itemCode = convert(char(3), convert(int, rand() * 900 + 100)) + '-' + convert(char(3), convert(int, rand() * 900 + 100))
insert into ecommerce_product (itemcode, productname, productdescription, stocklevel, ispublished, baseprice, basecurrencycode) values (
@itemcode, 'Product ' + @itemcode, 'Product ' + @itemcode + ' description', convert(int, rand() * 1000), 1, rand() * 100, 'GBP')
set @loopcounter = @loopcounter + 1
end
set nocount off

--Add the products to categories
insert into ecommerce_productcategory(categoryid, productid, sortorder) (
select @catID1, productid, productid from ecommerce_product where productid % 3 = 0
)

insert into ecommerce_productcategory(categoryid, productid, sortorder) (
select @catID2, productid, productid from ecommerce_product where productid % 3 = 1
)

insert into ecommerce_productcategory(categoryid, productid, sortorder) (
select @catID3, productid, productid from ecommerce_product where productid % 3 = 2
)

GO

create procedure GetModuleServices  
  
@moduletypeid int  
  
as  
  
select 'INSERT INTO cuyahoga_moduleservice (moduletypeid, servicekey, servicetype, classtype)  
VALUES (@moduletypeid, ''' + servicekey + ''', ''' + servicetype + ''', ''' + classtype + ''')'  
from cuyahoga_moduleservice where moduletypeid = @moduletypeid  

go
  
create procedure GetModuleSettings  
  
@moduletypeid int  
  
as  
  
select 'INSERT INTO cuyahoga_modulesetting (moduletypeid, name, friendlyname, settingdatatype, iscustomtype, isrequired) VALUES (@moduletypeid, '''   
+ name + ''', ''' + friendlyname + ''', ''' + settingdatatype + ''', ' + convert(varchar(1), iscustomtype) + ', ' + convert(varchar(1), isrequired) + ')'  
from cuyahoga_modulesetting where moduletypeid = @moduletypeid  

go
