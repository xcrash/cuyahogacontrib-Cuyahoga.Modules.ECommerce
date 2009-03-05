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

CREATE TABLE [dbo].[ECommerce_ProductTaxClass] (
	[productID] [bigint] NOT NULL ,
	[taxClassID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TaxZone] (
	[taxZoneID] [smallint] NOT NULL ,
	[taxZoneName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TaxZoneClassRate] (
	[taxZoneID] [smallint] NOT NULL ,
	[taxClassID] [smallint] NOT NULL ,
	[taxRate] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_AccountType] (
	[accountTypeID] [smallint] NOT NULL ,
	[accountTypeName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[discountRate] [decimal](18, 0) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

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

CREATE TABLE [dbo].[ECommerce_Attribute] (
	[attributeID] [bigint] NOT NULL ,
	[attributeReference] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[attributeDescription] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[isDisplayable] [bit] NULL ,
	[baseUnit] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_AttributeGroup] (
	[attributeGroupID] [int] IDENTITY (1, 1) NOT NULL ,
	[attributeGroupName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_AttributeGroupAttribute] (
	[attributeGroupID] [smallint] NOT NULL ,
	[attributeID] [int] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_AttributeOptionValue] (
	[attributeID] [bigint] NOT NULL ,
	[optionID] [bigint] NOT NULL ,
	[optionName] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_Basket] (
	[basketID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[userID] [int] NULL ,
	[currencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_BasketItem] (
	[basketItemID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[basketID] [bigint] NOT NULL ,
	[productID] [bigint] NOT NULL ,
	[itemTax] [decimal](19, 4) NOT NULL ,
	[linePrice] [decimal](19, 4) NOT NULL ,
	[quantity] [int] NOT NULL ,
	[itemTypeID] [smallint] NOT NULL ,
	[pricingStatusID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_BasketItemAttribute] (
	[basketitemID] [bigint] NOT NULL ,
	[attributeID] [int] NOT NULL ,
	[optionValue] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[optionPrice] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_Category] (
	[categoryID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[categoryName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[categoryDescription] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[parentCategoryID] [bigint] NULL ,
	[sortOrder] [smallint] NOT NULL ,
	[isPublished] [bit] NOT NULL ,
	[imageUrl] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[width] [smallint] NULL ,
	[height] [smallint] NULL ,
	[altText] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[updateTimeStamp] [datetime] NOT NULL ,
	[insertTimeStamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_Country] (
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[countryName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[defaultCurrencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_CountryDeliveryState] (
	[stateID] [smallint] NOT NULL ,
	[price] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_CountryDeliveryStateWeight] (
	[stateID] [smallint] NOT NULL ,
	[weightLevel] [smallint] NOT NULL ,
	[price] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_Currency] (
	[currencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[exchangeRate] [decimal](18, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_DeliveryType] (
	[deliveryTypeID] [smallint] IDENTITY (1, 1) NOT NULL ,
	[name] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[status] [bit] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_OrderHeader] (
	[basketID] [bigint] NOT NULL ,
	[orderReference] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[purchaseOrderNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[orderStatusID] [smallint] NOT NULL ,
	[orderedDate] [datetime] NOT NULL ,
	[invoiceAddressID] [bigint] NULL ,
	[deliveryAddressID] [bigint] NULL ,
	[deliveryTypeID] [smallint] NULL ,
	[comment] [nvarchar] (512) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_OrderStatus] (
	[statusID] [smallint] NOT NULL ,
	[status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_Payment] (
	[paymentID] [bigint] NOT NULL ,
	[basketID] [bigint] NOT NULL ,
	[paymentTypeID] [smallint] NOT NULL ,
	[paymentStatusID] [smallint] NOT NULL ,
	[currencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[amount] [decimal](19, 4) NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_PaymentType] (
	[paymentTypeID] [smallint] NOT NULL ,
	[name] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[description] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_Product] (
	[productID] [bigint] IDENTITY (1, 1) NOT NULL ,
	[itemCode] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[productName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[productDescription] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stockLevel] [int] NOT NULL ,
	[isPublished] [bit] NOT NULL ,
	[basePrice] [decimal](18, 4) NOT NULL ,
	[baseCurrencyCode] [char] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

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

CREATE TABLE [dbo].[ECommerce_ProductCategory] (
	[categoryID] [bigint] NOT NULL ,
	[productID] [bigint] NOT NULL ,
	[sortOrder] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

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

CREATE TABLE [dbo].[ECommerce_ProductRelation] (
	[productID] [bigint] NOT NULL ,
	[parentID] [bigint] NOT NULL ,
	[relationTypeID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_ProductSKU] (
	[sku] [varchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[productID] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_RelationType] (
	[relationTypeID] [smallint] NOT NULL ,
	[relationshipName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[relationshipDescription] [nvarchar] (1024) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_State] (
	[stateID] [smallint] NOT NULL ,
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stateCode] [nvarchar] (12) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[stateName] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TaxClass] (
	[taxClassID] [smallint] NOT NULL ,
	[taxClassName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TaxZoneCountry] (
	[countryCode] [char] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[taxZoneID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TaxZoneState] (
	[stateID] [smallint] NOT NULL ,
	[taxZoneID] [smallint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TranslationTag] (
	[tagReference] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[tagID] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_TranslationText] (
	[tagID] [bigint] NOT NULL ,
	[cultureCode] [varchar] (5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[textValue] [ntext] COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[ECommerce_UserDetail] (
	[userID] [int] NOT NULL ,
	[accountTypeID] [smallint] NOT NULL ,
	[addressID] [bigint] NOT NULL ,
	[inserttimestamp] [datetime] NOT NULL ,
	[updatetimestamp] [datetime] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ECommerce_ProductTaxClass] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_ProductTaxClass] PRIMARY KEY  CLUSTERED 
	(
		[productID],
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

ALTER TABLE [dbo].[ECommerce_AccountType] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_AccountType] PRIMARY KEY  CLUSTERED 
	(
		[accountTypeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Address] WITH NOCHECK ADD 
	CONSTRAINT [PK_address] PRIMARY KEY  CLUSTERED 
	(
		[addressID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Attribute] WITH NOCHECK ADD 
	CONSTRAINT [PK_attributes] PRIMARY KEY  CLUSTERED 
	(
		[attributeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_AttributeGroup] WITH NOCHECK ADD 
	CONSTRAINT [PK_AttributeGroups_1] PRIMARY KEY  CLUSTERED 
	(
		[attributeGroupID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_AttributeGroupAttribute] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_AttributeGroupAttribute] PRIMARY KEY  CLUSTERED 
	(
		[attributeGroupID],
		[attributeID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_AttributeOptionValue] WITH NOCHECK ADD 
	CONSTRAINT [PK_ECommerce_AttributeOption] PRIMARY KEY  CLUSTERED 
	(
		[optionID]
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

ALTER TABLE [dbo].[ECommerce_Country] WITH NOCHECK ADD 
	CONSTRAINT [PK_Countries] PRIMARY KEY  CLUSTERED 
	(
		[countryCode]
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
		[basketID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_OrderStatus] WITH NOCHECK ADD 
	CONSTRAINT [PK_OrderStatus] PRIMARY KEY  CLUSTERED 
	(
		[statusID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_Payment] WITH NOCHECK ADD 
	CONSTRAINT [PK_Payment] PRIMARY KEY  CLUSTERED 
	(
		[paymentID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_PaymentType] WITH NOCHECK ADD 
	CONSTRAINT [PK_PaymentType] PRIMARY KEY  CLUSTERED 
	(
		[paymentTypeID]
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

ALTER TABLE [dbo].[ECommerce_ProductTaxClass] ADD 
	CONSTRAINT [DF_ECommerce_ProductTaxClass_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_ProductTaxClass_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TaxZone] ADD 
	CONSTRAINT [DF_ECommerce_TaxZone_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxZone_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_TaxZoneClassRate] ADD 
	CONSTRAINT [DF_ECommerce_TaxZoneClassRate_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_TaxZoneClassRate_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_AccountType] ADD 
	CONSTRAINT [DF_ECommerce_AccountType_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_AccountType_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Address] ADD 
	CONSTRAINT [DF_ECommerce_Address_isDeleted] DEFAULT (0) FOR [isDeleted],
	CONSTRAINT [DF_ECommerce_Address_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Address_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Attribute] ADD 
	CONSTRAINT [IX_ECommerce_Attribute] UNIQUE  NONCLUSTERED 
	(
		[attributeReference]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_AttributeGroupAttribute] ADD 
	CONSTRAINT [DF_ECommerce_AttributeGroupAttribute_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_AttributeGroupAttribute_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
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
	CONSTRAINT [DF_ECommerce_Category_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Category_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
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
	CONSTRAINT [DF_ECommerce_OrderHeader_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp],
	CONSTRAINT [IX_ECommerce_OrderHeader] UNIQUE  NONCLUSTERED 
	(
		[orderReference]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ECommerce_OrderStatus] ADD 
	CONSTRAINT [DF_ECommerce_OrderStatus_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_OrderStatus_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_Payment] ADD 
	CONSTRAINT [DF_ECommerce_Payment_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_Payment_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
GO

ALTER TABLE [dbo].[ECommerce_PaymentType] ADD 
	CONSTRAINT [DF_ECommerce_PaymentType_inserttimestamp] DEFAULT (getdate()) FOR [inserttimestamp],
	CONSTRAINT [DF_ECommerce_PaymentType_updatetimestamp] DEFAULT (getdate()) FOR [updatetimestamp]
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

ALTER TABLE [dbo].[ECommerce_AttributeOptionValue] ADD 
	CONSTRAINT [FK_ECommerce_AttributeOption_ECommerce_Attribute] FOREIGN KEY 
	(
		[attributeID]
	) REFERENCES [dbo].[ECommerce_Attribute] (
		[attributeID]
	)
GO

ALTER TABLE [dbo].[ECommerce_Basket] ADD 
	CONSTRAINT [FK_ECommerce_Basket_cuyahoga_user] FOREIGN KEY 
	(
		[userID]
	) REFERENCES [cuyahoga_user] (
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
	CONSTRAINT [FK_OrderHeader_Basket] FOREIGN KEY 
	(
		[basketID]
	) REFERENCES [dbo].[ECommerce_Basket] (
		[basketID]
	),
	CONSTRAINT [FK_OrderHeader_deliveryType] FOREIGN KEY 
	(
		[deliveryTypeID]
	) REFERENCES [dbo].[ECommerce_DeliveryType] (
		[deliveryTypeID]
	),
	CONSTRAINT [FK_OrderHeader_OrderStatus] FOREIGN KEY 
	(
		[orderStatusID]
	) REFERENCES [dbo].[ECommerce_OrderStatus] (
		[statusID]
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
	),
	CONSTRAINT [FK_Payment_PaymentType] FOREIGN KEY 
	(
		[paymentTypeID]
	) REFERENCES [dbo].[ECommerce_PaymentType] (
		[paymentTypeID]
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

ALTER TABLE [dbo].[ECommerce_State] ADD 
	CONSTRAINT [FK_ECommerce_State_ECommerce_Country] FOREIGN KEY 
	(
		[countryCode]
	) REFERENCES [dbo].[ECommerce_Country] (
		[countryCode]
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
	CONSTRAINT [FK_ECommerce_UserDetail_cuyahoga_user] FOREIGN KEY 
	(
		[userID]
	) REFERENCES [cuyahoga_user] (
		[userid]
	),
	CONSTRAINT [FK_ECommerce_UserDetail_ECommerce_AccountType] FOREIGN KEY 
	(
		[accountTypeID]
	) REFERENCES [dbo].[ECommerce_AccountType] (
		[accountTypeID]
	),
	CONSTRAINT [FK_UserDetails_address] FOREIGN KEY 
	(
		[addressID]
	) REFERENCES [dbo].[ECommerce_Address] (
		[addressID]
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