USE [master]
GO
/****** Object:  Database [Arad.SMS.Gateway.DB]    Script Date: 1/1/2021 1:40:05 PM ******/
CREATE DATABASE [Arad.SMS.Gateway.DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Arad.SMS.Gateway.DB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Arad.SMS.Gateway.DB.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Arad.SMS.Gateway.DB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Arad.SMS.Gateway.DB_log.ldf' , SIZE = 139264KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Arad.SMS.Gateway.DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET TRUSTWORTHY ON 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET RECOVERY FULL 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET  MULTI_USER 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Arad.SMS.Gateway.DB', N'ON'
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET QUERY_STORE = OFF
GO
USE [Arad.SMS.Gateway.DB]
GO

/****** Object:  UserDefinedTableType [dbo].[BulkRecipient]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[BulkRecipient] AS TABLE(
	[ZoneGuid] [uniqueidentifier] NOT NULL,
	[ZipCode] [nvarchar](8) NULL,
	[Prefix] [nvarchar](16) NULL,
	[Type] [tinyint] NULL,
	[Operator] [tinyint] NULL,
	[FromIndex] [int] NULL,
	[Count] [int] NOT NULL,
	[ScopeCount] [int] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Content]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[Content] AS TABLE(
	[Text] [nvarchar](512) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DeliveryStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[DeliveryStatus] AS TABLE(
	[Agent] [int] NULL,
	[BatchId] [nvarchar](255) NULL,
	[Mobile] [nvarchar](255) NULL,
	[ReturnId] [nvarchar](255) NULL,
	[CheckId] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[DeliveryDateTime] [datetime] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[DomainSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[DomainSetting] AS TABLE(
	[Key] [int] NULL,
	[Value] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[PhoneNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[PhoneNumber] AS TABLE(
	[Guid] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[Telephone] [nvarchar](50) NULL,
	[CellPhone] [nvarchar](50) NOT NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[Job] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
	[F1] [nvarchar](100) NULL,
	[F2] [nvarchar](100) NULL,
	[F3] [nvarchar](100) NULL,
	[F4] [nvarchar](100) NULL,
	[F5] [nvarchar](100) NULL,
	[F6] [nvarchar](100) NULL,
	[F7] [nvarchar](100) NULL,
	[F8] [nvarchar](100) NULL,
	[F9] [nvarchar](100) NULL,
	[F10] [nvarchar](100) NULL,
	[F11] [nvarchar](100) NULL,
	[F12] [nvarchar](100) NULL,
	[F13] [nvarchar](100) NULL,
	[F14] [nvarchar](100) NULL,
	[F15] [nvarchar](100) NULL,
	[F16] [nvarchar](100) NULL,
	[F17] [nvarchar](100) NULL,
	[F18] [nvarchar](100) NULL,
	[F19] [nvarchar](100) NULL,
	[F20] [nvarchar](100) NULL,
	[Sex] [int] NULL,
	[PhoneBookGuid] [uniqueidentifier] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Recipient]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[Recipient] AS TABLE(
	[Mobile] [nvarchar](16) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SentMessages]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[SentMessages] AS TABLE(
	[OutboxGuid] [uniqueidentifier] NOT NULL,
	[ItemId] [nvarchar](64) NOT NULL,
	[ToNumber] [nvarchar](255) NOT NULL,
	[DeliveryStatus] [smallint] NULL,
	[SendStatus] [smallint] NULL,
	[ReturnId] [nvarchar](255) NULL,
	[CheckId] [nvarchar](255) NULL,
	[SmsSenderAgentReference] [tinyint] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[Setting]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[Setting] AS TABLE(
	[Key] [int] NULL,
	[Value] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[SmsParserOptions]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[SmsParserOptions] AS TABLE(
	[IsCorrect] [bit] NULL,
	[Title] [nvarchar](50) NULL,
	[Key] [nvarchar](50) NULL,
	[ReactionExtention] [nvarchar](max) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UpdateExportDataRequest]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[UpdateExportDataRequest] AS TABLE(
	[Guid] [uniqueidentifier] NOT NULL,
	[PageNo] [int] NOT NULL,
	[Status] [tinyint] NOT NULL,
	[TxtPageNo] [int] NOT NULL,
	[TxtStatus] [tinyint] NOT NULL,
	[SendStatus] [tinyint] NOT NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[UserSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
CREATE TYPE [dbo].[UserSetting] AS TABLE(
	[Key] [int] NULL,
	[Value] [nvarchar](max) NULL,
	[Status] [tinyint] NULL
)
GO
/****** Object:  UserDefinedFunction [dbo].[GenerateSmsFromFormat]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GenerateSmsFromFormat](@NumberGuid UNIQUEIDENTIFIER,@FormatGuid UNIQUEIDENTIFIER) RETURNS NVARCHAR(MAX)
  AS

	BEGIN
		DECLARE @Format NVARCHAR(MAX),
						@Prefix NVARCHAR(64),
						@Field NVARCHAR(64),
						@SmsBody NVARCHAR(MAX) = '',
						@UserText NVARCHAR(MAX),
						@FirstName NVARCHAR(32),
						@LastName NVARCHAR(64),
						@BirthDate DATETIME,
						@Telephone NVARCHAR(16),
						@CellPhone NVARCHAR(16),
						@FaxNumber NVARCHAR(16),
						@Job NVARCHAR(64),
						@Address NVARCHAR(MAX),
						@Email NVARCHAR(128),
						@F1 NVARCHAR(128),
						@F2 NVARCHAR(128),
						@F3 NVARCHAR(128),
						@F4 NVARCHAR(128),
						@F5 NVARCHAR(128),
						@F6 NVARCHAR(128),
						@F7 NVARCHAR(128),
						@F8 NVARCHAR(128),
						@F9 NVARCHAR(128),
						@F10 NVARCHAR(128),
						@F11 NVARCHAR(128),
						@F12 NVARCHAR(128),
						@F13 NVARCHAR(128),
						@F14 NVARCHAR(128),
						@F15 NVARCHAR(128),
						@F16 NVARCHAR(128),
						@F17 NVARCHAR(128),
						@F18 NVARCHAR(128),
						@F19 NVARCHAR(128),
						@F20 NVARCHAR(128),
						@Sex INT;
		
		SELECT @Format = [Format] FROM [dbo].[SmsFormats] WHERE [Guid] = @FormatGuid;
		SELECT 
			@FirstName = [FirstName] ,
			@LastName = [LastName] ,
			@BirthDate = [BirthDate] ,
			@Telephone = [Telephone] ,
			@CellPhone = [CellPhone],
			@FaxNumber = [FaxNumber] ,
			@Job = [Job] ,
			@Address = [Address] ,
			@Email = [Email] ,
			@F1 = [F1] ,
			@F2 = [F2] ,
			@F3 = [F3] ,
			@F4 = [F4] ,
			@F5 = [F5] ,
			@F6 = [F6] ,
			@F7 = [F7] ,
			@F8 = [F8] ,
			@F9 = [F9] ,
			@F10 = [F10] ,
			@F11 = [F11] ,
			@F12 = [F12] ,
			@F13 = [F13] ,
			@F14 = [F14] ,
			@F15 = [F15] ,
			@F16 = [F16] ,
			@F17 = [F17] ,
			@F18 = [F18] ,
			@F19 = [F19] ,
			@F20 = [F20] ,
			@Sex = [Sex] 
		FROM [dbo].[PhoneNumbers] WHERE [Guid] = @NumberGuid
		
		WHILE(LEN(@Format)>0)
		BEGIN
			SET @Prefix = SUBSTRING(@Format,1,4)
			
			IF (@Prefix = '<(%$')
			BEGIN
				SET @Format = SUBSTRING(@Format,5,LEN(@Format));
				SET @Field = SUBSTRING(@Format,1, (CHARINDEX('$%)>',@Format)-1));
				SET @Format = Substring(@Format,LEN(@Field) + 5,LEN(@Format));
				
				IF(CHARINDEX('@$!$@',@Field,0)=0)
				BEGIN
					IF (@Field = 'firstname')
						SET @SmsBody += ISNULL(@FirstName,'') + ' '
					IF (@Field='lastname')
						SET @SmsBody += ISNULL(@LastName,'') + ' '
					IF (@Field = 'birthDate')
						SET @SmsBody += ISNULL(dbo.GetSolarDate(@BirthDate),'') + ' ';	
					IF (@Field='telephone')
						SET @SmsBody += ISNULL(@Telephone,'') + ' '
					IF (@Field='cellphone')
						SET @SmsBody += ISNULL(@CellPhone,'') + ' '
					IF (@Field='faxnumber')
						SET @SmsBody += ISNULL(@FaxNumber,'') + ' '
					IF (@Field='job')
						SET @SmsBody += ISNULL(@Job,'') + ' '
					IF (@Field='address')
						SET @SmsBody += ISNULL(@Address,'') + ' '
					IF (@Field='email')
						SET @SmsBody += ISNULL(@email,'') + ' '
					IF(@Field = 'sex')
					BEGIN
						IF (@Sex = 1)
							SET @SmsBody += N'آقای ';
						ELSE IF	(@Sex = 2)
							SET @SmsBody += N'خانم '; 
					END
				END
				ELSE
				BEGIN
					SET @Field= Substring(@Field,1,CHARINDEX('@$!$@',@Field)-1);
					IF (@Field = 'field1')
						SET @SmsBody += ISNULL(@F1,'') + ' '
					IF (@Field = 'field2')
						SET @SmsBody += ISNULL(@F2,'') + ' '
					IF (@Field = 'field3')
						SET @SmsBody += ISNULL(@F3,'') + ' '
					IF (@Field = 'field4')
						SET @SmsBody += ISNULL(@F4,'') + ' '
					IF (@Field = 'field5')
						SET @SmsBody += ISNULL(@F5,'') + ' '
					IF (@Field = 'field6')
						SET @SmsBody += ISNULL(@F6,'') + ' '
					IF (@Field = 'field7')
						SET @SmsBody += ISNULL(@F7,'') + ' '
					IF (@Field = 'field8')
						SET @SmsBody += ISNULL(@F8,'') + ' '
					IF (@Field = 'field9')
						SET @SmsBody += ISNULL(@F9,'') + ' '
					IF (@Field = 'field10')
						SET @SmsBody += ISNULL(@F10,'') + ' '
					IF (@Field = 'field11')
						SET @SmsBody += ISNULL(@F11,'') + ' '
					IF (@Field = 'field12')
						SET @SmsBody += ISNULL(@F12,'') + ' '
					IF (@Field = 'field13')
						SET @SmsBody += ISNULL(@F13,'') + ' '
					IF (@Field = 'field14')
						SET @SmsBody += ISNULL(@F14,'') + ' '
					IF (@Field = 'field15')
						SET @SmsBody += ISNULL(@F15,'') + ' '
					IF (@Field = 'field16')
						SET @SmsBody += ISNULL(@F16,'') + ' '
					IF (@Field = 'field17')
						SET @SmsBody += ISNULL(@F17,'') + ' '
					IF (@Field = 'field18')
						SET @SmsBody += ISNULL(@F18,'') + ' '
					IF (@Field = 'field19')
						SET @SmsBody += ISNULL(@F19,'') + ' '
					IF (@Field = 'field20')
						SET @SmsBody += ISNULL(@F20,'') + ' '
				END
			END
			ELSE IF(@Prefix = '<(*$')
			BEGIN
				SET @Format = SUBSTRING(@Format,5,LEN(@Format));
				SET @UserText = Substring(@Format,1, (CHARINDEX('$*)>',@Format)-1))
				SET @Format = Substring(@Format,LEN(@UserText)+ 5,LEN(@Format));
				
				SET @SmsBody += @UserText + ' ';
			END
			ELSE IF(@Prefix = '<(!$')
			BEGIN
				SET @Format = SUBSTRING(@Format,5,LEN(@Format));
				SET @Format = Substring(@Format,13,LEN(@Format));
			END
		END
		RETURN @SmsBody;
	END


GO
/****** Object:  UserDefinedFunction [dbo].[GenerateSmsFromFormatForNationalCodeParser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GenerateSmsFromFormatForNationalCodeParser](@Number NVARCHAR(16),@FormatGuid UNIQUEIDENTIFIER,@SmsText NCHAR(10)) RETURNS NVARCHAR(MAX)
  AS
BEGIN

DECLARE @Guid UNIQUEIDENTIFIER;
DECLARE @Format NVARCHAR(MAX);
DECLARE	@Prefix NVARCHAR(64);
DECLARE	@Field NVARCHAR(64);
DECLARE	@SmsBody NVARCHAR(MAX) = '';
DECLARE	@UserText NVARCHAR(MAX);
DECLARE	@FirstName NVARCHAR(32);
DECLARE	@LastName NVARCHAR(64);
DECLARE @NationalCode NCHAR(10);
DECLARE	@BirthDate DATETIME;
DECLARE	@Telephone NVARCHAR(16);
DECLARE	@CellPhone NVARCHAR(16);
DECLARE	@FaxNumber NVARCHAR(16);
DECLARE	@Job NVARCHAR(64);
DECLARE	@Address NVARCHAR(MAX);
DECLARE	@Email NVARCHAR(128);
DECLARE	@F1 NVARCHAR(128);
DECLARE	@F2 NVARCHAR(128);
DECLARE	@F3 NVARCHAR(128);
DECLARE	@F4 NVARCHAR(128);
DECLARE	@F5 NVARCHAR(128);
DECLARE	@F6 NVARCHAR(128);
DECLARE	@F7 NVARCHAR(128);
DECLARE	@F8 NVARCHAR(128);
DECLARE	@F9 NVARCHAR(128);
DECLARE	@F10 NVARCHAR(128);
DECLARE	@F11 NVARCHAR(128);
DECLARE	@F12 NVARCHAR(128);
DECLARE	@F13 NVARCHAR(128);
DECLARE	@F14 NVARCHAR(128);
DECLARE	@F15 NVARCHAR(128);
DECLARE	@F16 NVARCHAR(128);
DECLARE	@F17 NVARCHAR(128);
DECLARE	@F18 NVARCHAR(128);
DECLARE	@F19 NVARCHAR(128);
DECLARE	@F20 NVARCHAR(128);
DECLARE	@Sex INT;
DECLARE @GroupGuid UNIQUEIDENTIFIER;

SELECT @Format = [Format],@GroupGuid = [PhoneBookGuid] FROM [dbo].[SmsFormats] WHERE [Guid] = @FormatGuid;

SELECT
	TOP 1
	@Guid = [Guid],
	@FirstName = [FirstName] ,
	@LastName = [LastName] ,
	@NationalCode = [NationalCode],
	@BirthDate = [BirthDate] ,
	@Telephone = [Telephone] ,
	@CellPhone = [CellPhone],
	@FaxNumber = [FaxNumber] ,
	@Job = [Job] ,
	@Address = [Address] ,
	@Email = [Email] ,
	@F1 = [F1] ,
	@F2 = [F2] ,
	@F3 = [F3] ,
	@F4 = [F4] ,
	@F5 = [F5] ,
	@F6 = [F6] ,
	@F7 = [F7] ,
	@F8 = [F8] ,
	@F9 = [F9] ,
	@F10 = [F10] ,
	@F11 = [F11] ,
	@F12 = [F12] ,
	@F13 = [F13] ,
	@F14 = [F14] ,
	@F15 = [F15] ,
	@F16 = [F16] ,
	@F17 = [F17] ,
	@F18 = [F18] ,
	@F19 = [F19] ,
	@F20 = [F20] ,
	@Sex = [Sex] 
FROM
	[dbo].[PhoneNumbers]
WHERE
	[IsDeleted] = 0 AND
	[PhoneBookGuid] = @GroupGuid AND
	ISNULL([NationalCode],'') = ''

IF(@@ROWCOUNT = 0)
	RETURN N'در حال حاضر امکان ارائه سرویس وجود ندارد';

WHILE(LEN(@Format)>0)
BEGIN
	SET @Prefix = SUBSTRING(@Format,1,4)
			
	IF (@Prefix = '<(%$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @Field = SUBSTRING(@Format,1, (CHARINDEX('$%)>',@Format)-1));
		SET @Format = Substring(@Format,LEN(@Field) + 5,LEN(@Format));
				
		IF(CHARINDEX('@$!$@',@Field,0)=0)
		BEGIN
			IF (@Field = 'firstname')
				SET @SmsBody += ISNULL(@FirstName,'') + ' '
			IF (@Field='lastname')
				SET @SmsBody += ISNULL(@LastName,'') + ' '
			IF (@Field='nationalcode')
				SET @SmsBody += @SmsText + ' '
			IF (@Field = 'birthDate')
				SET @SmsBody += ISNULL(dbo.GetSolarDate(@BirthDate),'') + ' ';	
			IF (@Field='telephone')
				SET @SmsBody += ISNULL(@Telephone,'') + ' '
			IF (@Field='cellphone')
				SET @SmsBody += @Number + ' '
			IF (@Field='faxnumber')
				SET @SmsBody += ISNULL(@FaxNumber,'') + ' '
			IF (@Field='job')
				SET @SmsBody += ISNULL(@Job,'') + ' '
			IF (@Field='address')
				SET @SmsBody += ISNULL(@Address,'') + ' '
			IF (@Field='email')
				SET @SmsBody += ISNULL(@email,'') + ' '
			IF(@Field = 'sex')
			BEGIN
				IF (@Sex = 1)
					SET @SmsBody += N'آقای ';
				ELSE IF	(@Sex = 2)
					SET @SmsBody += N'خانم '; 
			END
		END
		ELSE
		BEGIN
			SET @Field= Substring(@Field,1,CHARINDEX('@$!$@',@Field)-1);
			IF (@Field = 'field1')
				SET @SmsBody += ISNULL(@F1,'') + ' '
			IF (@Field = 'field2')
				SET @SmsBody += ISNULL(@F2,'') + ' '
			IF (@Field = 'field3')
				SET @SmsBody += ISNULL(@F3,'') + ' '
			IF (@Field = 'field4')
				SET @SmsBody += ISNULL(@F4,'') + ' '
			IF (@Field = 'field5')
				SET @SmsBody += ISNULL(@F5,'') + ' '
			IF (@Field = 'field6')
				SET @SmsBody += ISNULL(@F6,'') + ' '
			IF (@Field = 'field7')
				SET @SmsBody += ISNULL(@F7,'') + ' '
			IF (@Field = 'field8')
				SET @SmsBody += ISNULL(@F8,'') + ' '
			IF (@Field = 'field9')
				SET @SmsBody += ISNULL(@F9,'') + ' '
			IF (@Field = 'field10')
				SET @SmsBody += ISNULL(@F10,'') + ' '
			IF (@Field = 'field11')
				SET @SmsBody += ISNULL(@F11,'') + ' '
			IF (@Field = 'field12')
				SET @SmsBody += ISNULL(@F12,'') + ' '
			IF (@Field = 'field13')
				SET @SmsBody += ISNULL(@F13,'') + ' '
			IF (@Field = 'field14')
				SET @SmsBody += ISNULL(@F14,'') + ' '
			IF (@Field = 'field15')
				SET @SmsBody += ISNULL(@F15,'') + ' '
			IF (@Field = 'field16')
				SET @SmsBody += ISNULL(@F16,'') + ' '
			IF (@Field = 'field17')
				SET @SmsBody += ISNULL(@F17,'') + ' '
			IF (@Field = 'field18')
				SET @SmsBody += ISNULL(@F18,'') + ' '
			IF (@Field = 'field19')
				SET @SmsBody += ISNULL(@F19,'') + ' '
			IF (@Field = 'field20')
				SET @SmsBody += ISNULL(@F20,'') + ' '
		END
	END
	ELSE IF(@Prefix = '<(*$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @UserText = Substring(@Format,1, (CHARINDEX('$*)>',@Format)-1))
		SET @Format = Substring(@Format,LEN(@UserText)+ 5,LEN(@Format));
				
		SET @SmsBody += @UserText + ' ';
	END
	ELSE IF(@Prefix = '<(!$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @Format = Substring(@Format,13,LEN(@Format));
				
		SET @SmsBody += @SmsText + ' ';
  END
END

--EXEC [dbo].[PhoneNumbers_UpdateNationalCodeParser]
--			@Guid = @Guid,
--			@Mobile = @Number,
--			@NationalCode = @SmsText

RETURN @SmsBody;
END

GO
/****** Object:  UserDefinedFunction [dbo].[GenerateSmsFromFormatForParser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GenerateSmsFromFormatForParser](@Number NVARCHAR(16),@FormatGuid UNIQUEIDENTIFIER,@SmsText NVARCHAR(MAX),@Guid UNIQUEIDENTIFIER) RETURNS NVARCHAR(MAX)
  AS

BEGIN

DECLARE @Format NVARCHAR(MAX);
DECLARE	@Prefix NVARCHAR(64);
DECLARE	@Field NVARCHAR(64);
DECLARE	@SmsBody NVARCHAR(MAX) = '';
DECLARE	@UserText NVARCHAR(MAX);
DECLARE	@FirstName NVARCHAR(32);
DECLARE	@LastName NVARCHAR(64);
DECLARE @NationalCode NCHAR(10);
DECLARE	@BirthDate DATETIME;
DECLARE	@Telephone NVARCHAR(16);
DECLARE	@CellPhone NVARCHAR(16);
DECLARE	@FaxNumber NVARCHAR(16);
DECLARE	@Job NVARCHAR(64);
DECLARE	@Address NVARCHAR(MAX);
DECLARE	@Email NVARCHAR(128);
DECLARE	@F1 NVARCHAR(128);
DECLARE	@F2 NVARCHAR(128);
DECLARE	@F3 NVARCHAR(128);
DECLARE	@F4 NVARCHAR(128);
DECLARE	@F5 NVARCHAR(128);
DECLARE	@F6 NVARCHAR(128);
DECLARE	@F7 NVARCHAR(128);
DECLARE	@F8 NVARCHAR(128);
DECLARE	@F9 NVARCHAR(128);
DECLARE	@F10 NVARCHAR(128);
DECLARE	@F11 NVARCHAR(128);
DECLARE	@F12 NVARCHAR(128);
DECLARE	@F13 NVARCHAR(128);
DECLARE	@F14 NVARCHAR(128);
DECLARE	@F15 NVARCHAR(128);
DECLARE	@F16 NVARCHAR(128);
DECLARE	@F17 NVARCHAR(128);
DECLARE	@F18 NVARCHAR(128);
DECLARE	@F19 NVARCHAR(128);
DECLARE	@F20 NVARCHAR(128);
DECLARE	@Sex INT;
DECLARE @GroupGuid UNIQUEIDENTIFIER;

SELECT @Format = [Format],@GroupGuid = [PhoneBookGuid] FROM [dbo].[SmsFormats] WHERE [Guid] = @FormatGuid;

SELECT
	TOP 1
	@FirstName = [FirstName] ,
	@LastName = [LastName] ,
	@NationalCode = [NationalCode],
	@BirthDate = [BirthDate] ,
	@Telephone = [Telephone] ,
	@CellPhone = [CellPhone],
	@FaxNumber = [FaxNumber] ,
	@Job = [Job] ,
	@Address = [Address] ,
	@Email = [Email] ,
	@F1 = [F1] ,
	@F2 = [F2] ,
	@F3 = [F3] ,
	@F4 = [F4] ,
	@F5 = [F5] ,
	@F6 = [F6] ,
	@F7 = [F7] ,
	@F8 = [F8] ,
	@F9 = [F9] ,
	@F10 = [F10] ,
	@F11 = [F11] ,
	@F12 = [F12] ,
	@F13 = [F13] ,
	@F14 = [F14] ,
	@F15 = [F15] ,
	@F16 = [F16] ,
	@F17 = [F17] ,
	@F18 = [F18] ,
	@F19 = [F19] ,
	@F20 = [F20] ,
	@Sex = [Sex] 
FROM
	[dbo].[PhoneNumbers]
WHERE
	[Guid] = @Guid;

WHILE(LEN(@Format)>0)
BEGIN
	SET @Prefix = SUBSTRING(@Format,1,4)
			
	IF (@Prefix = '<(%$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @Field = SUBSTRING(@Format,1, (CHARINDEX('$%)>',@Format)-1));
		SET @Format = Substring(@Format,LEN(@Field) + 5,LEN(@Format));
				
		IF(CHARINDEX('@$!$@',@Field,0)=0)
		BEGIN
			IF (@Field = 'firstname')
				SET @SmsBody += ISNULL(@FirstName,'') + ' '
			IF (@Field='lastname')
				SET @SmsBody += ISNULL(@LastName,'') + ' '
			IF (@Field='nationalcode')
				SET @SmsBody += ISNULL(@NationalCode,'') + ' '
			IF (@Field = 'birthDate')
				SET @SmsBody += ISNULL(dbo.GetSolarDate(@BirthDate),'') + ' ';	
			IF (@Field='telephone')
				SET @SmsBody += ISNULL(@Telephone,'') + ' '
			IF (@Field='cellphone')
				SET @SmsBody += @Number + ' '
			IF (@Field='faxnumber')
				SET @SmsBody += ISNULL(@FaxNumber,'') + ' '
			IF (@Field='job')
				SET @SmsBody += ISNULL(@Job,'') + ' '
			IF (@Field='address')
				SET @SmsBody += ISNULL(@Address,'') + ' '
			IF (@Field='email')
				SET @SmsBody += ISNULL(@email,'') + ' '
			IF(@Field = 'sex')
			BEGIN
				IF (@Sex = 1)
					SET @SmsBody += N'آقای ';
				ELSE IF	(@Sex = 2)
					SET @SmsBody += N'خانم '; 
			END
		END
		ELSE
		BEGIN
			SET @Field= Substring(@Field,1,CHARINDEX('@$!$@',@Field)-1);
			IF (@Field = 'field1')
				SET @SmsBody += ISNULL(@F1,'') + ' '
			IF (@Field = 'field2')
				SET @SmsBody += ISNULL(@F2,'') + ' '
			IF (@Field = 'field3')
				SET @SmsBody += ISNULL(@F3,'') + ' '
			IF (@Field = 'field4')
				SET @SmsBody += ISNULL(@F4,'') + ' '
			IF (@Field = 'field5')
				SET @SmsBody += ISNULL(@F5,'') + ' '
			IF (@Field = 'field6')
				SET @SmsBody += ISNULL(@F6,'') + ' '
			IF (@Field = 'field7')
				SET @SmsBody += ISNULL(@F7,'') + ' '
			IF (@Field = 'field8')
				SET @SmsBody += ISNULL(@F8,'') + ' '
			IF (@Field = 'field9')
				SET @SmsBody += ISNULL(@F9,'') + ' '
			IF (@Field = 'field10')
				SET @SmsBody += ISNULL(@F10,'') + ' '
			IF (@Field = 'field11')
				SET @SmsBody += ISNULL(@F11,'') + ' '
			IF (@Field = 'field12')
				SET @SmsBody += ISNULL(@F12,'') + ' '
			IF (@Field = 'field13')
				SET @SmsBody += ISNULL(@F13,'') + ' '
			IF (@Field = 'field14')
				SET @SmsBody += ISNULL(@F14,'') + ' '
			IF (@Field = 'field15')
				SET @SmsBody += ISNULL(@F15,'') + ' '
			IF (@Field = 'field16')
				SET @SmsBody += ISNULL(@F16,'') + ' '
			IF (@Field = 'field17')
				SET @SmsBody += ISNULL(@F17,'') + ' '
			IF (@Field = 'field18')
				SET @SmsBody += ISNULL(@F18,'') + ' '
			IF (@Field = 'field19')
				SET @SmsBody += ISNULL(@F19,'') + ' '
			IF (@Field = 'field20')
				SET @SmsBody += ISNULL(@F20,'') + ' '
		END
	END
	ELSE IF(@Prefix = '<(*$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @UserText = Substring(@Format,1, (CHARINDEX('$*)>',@Format)-1))
		SET @Format = Substring(@Format,LEN(@UserText)+ 5,LEN(@Format));
				
		SET @SmsBody += @UserText + ' ';
	END
	ELSE IF(@Prefix = '<(!$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @Format = Substring(@Format,13,LEN(@Format));
				
		SET @SmsBody += @SmsText + ' ';
  END
END

RETURN @SmsBody;
END


GO
/****** Object:  UserDefinedFunction [dbo].[GetNumberOperator]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[GetNumberOperator](@Mobile [nvarchar](50))
RETURNS INT
  AS
 BEGIN

DECLARE @OperatorID INT = 0;
DECLARE @Opt TABLE([Id] INT,[IsMatch] BIT);

INSERT INTO @Opt
  ([Id],
	[IsMatch])
SELECT
	[ID],
	[dbo].[IsMatch](@Mobile,[Regex]) AS [IsMatch]
FROM 
	[Operators]
WHERE
	[ID] > 0;

SELECT TOP 1 @OperatorID = [ID] FROM @Opt WHERE [IsMatch] = 1;

RETURN ISNULL(@OperatorID,0);

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetPrivateNumberAgentReference]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetPrivateNumberAgentReference](@PrivateNumberGuid UNIQUEIDENTIFIER)
RETURNS INT
  AS
 BEGIN

DECLARE @SmsSenderAgentReference INT;

SELECT
			@SmsSenderAgentReference = [SmsSenderAgentReference]
FROM
			[PrivateNumbers] INNER JOIN
			[SmsSenderAgents] ON [PrivateNumbers].[SmsSenderAgentGuid] = [SmsSenderAgents].[Guid]
WHERE	
			[PrivateNumbers].[Guid] = @PrivateNumberGuid

RETURN @SmsSenderAgentReference;

END
GO
/****** Object:  UserDefinedFunction [dbo].[GetRawFormat]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[GetRawFormat](@FormatGuid UNIQUEIDENTIFIER) RETURNS NVARCHAR(MAX)
  AS

	BEGIN
		DECLARE 
						@PhoneBookGuid UNIQUEIDENTIFIER,
						@Format NVARCHAR(MAX),
						@Prefix NVARCHAR(50),
						@Field NVARCHAR(50),
						@SmsBody NVARCHAR(MAX)='',
						@UserText NVARCHAR(MAX),
						@FirstName NVARCHAR(50)=N'[نام]',
						@LastName NVARCHAR(50)=N'[نام خانوادگی]',
						@BirthDate NVARCHAR(50)=N'[تاریخ تولد]',
						@Telephone NVARCHAR(50)=N'[تلفن]',
						@CellPhone NVARCHAR(50)=N'[موبایل]',
						@FaxNumber NVARCHAR(50)=N'[فکس]',
						@Job NVARCHAR(50)=N'[شغل]',
						@Address NVARCHAR(MAX)=N'[آدرس]',
						@Email NVARCHAR(100)=N'[ایمیل]',
						@F1 NVARCHAR(100),
						@F2 NVARCHAR(100),
						@F3 NVARCHAR(100),
						@F4 NVARCHAR(100),
						@F5 NVARCHAR(100),
						@F6 NVARCHAR(100),
						@F7 NVARCHAR(100),
						@F8 NVARCHAR(100),
						@F9 NVARCHAR(100),
						@F10 NVARCHAR(100),
						@F11 NVARCHAR(100),
						@F12 NVARCHAR(100),
						@F13 NVARCHAR(100),
						@F14 NVARCHAR(100),
						@F15 NVARCHAR(100),
						@F16 NVARCHAR(100),
						@F17 NVARCHAR(100),
						@F18 NVARCHAR(100),
						@F19 NVARCHAR(100),
						@F20 NVARCHAR(100),
						@Sex INT;
		
		SELECT @Format = [Format] FROM [dbo].[SmsFormats] WHERE [Guid] = @FormatGuid AND [IsDeleted] = 0;
		SELECT @PhoneBookGuid = [PhoneBookGuid] FROM [dbo].[SmsFormatPhoneBooks] WHERE [SmsFormatGuid] = @FormatGuid;

		SELECT 
			@F1 = [Field1] ,
			@F2 = [Field2] ,
			@F3 = [Field3] ,
			@F4 = [Field4] ,
			@F5 = [Field5] ,
			@F6 = [Field6] ,
			@F7 = [Field7] ,
			@F8 = [Field8] ,
			@F9 = [Field9] ,
			@F10 = [Field10] ,
			@F11 = [Field11] ,
			@F12 = [Field12] ,
			@F13 = [Field13] ,
			@F14 = [Field14] ,
			@F15 = [Field15] ,
			@F16 = [Field16] ,
			@F17 = [Field17] ,
			@F18 = [Field18] ,
			@F19 = [Field19] ,
			@F20 = [Field20]
		FROM [dbo].[UserFields] WHERE [PhoneBookGuid] = @PhoneBookGuid
		
		WHILE(LEN(@Format)>0)
		BEGIN
			SET @Prefix = SUBSTRING(@Format,1,4)
			
			IF (@Prefix = '<(%$')
			BEGIN
				SET @Format = SUBSTRING(@Format,5,LEN(@Format));
				SET @Field = SUBSTRING(@Format,1, (CHARINDEX('$%)>',@Format)-1));
				SET @Format = Substring(@Format,LEN(@Field) + 5,LEN(@Format));
				
				IF(CHARINDEX('@$!$@',@Field,0)=0)
				BEGIN
					IF (@Field = 'firstname')
						SET @SmsBody += ISNULL(@FirstName,'') + ' '
					IF (@Field='lastname')
						SET @SmsBody += ISNULL(@LastName,'') + ' '
					IF (@Field = 'birthDate')
						SET @SmsBody += ISNULL(dbo.GetSolarDate(@BirthDate),'') + ' ';	
					IF (@Field='telephone')
						SET @SmsBody += ISNULL(@Telephone,'') + ' '
					IF (@Field='cellphone')
						SET @SmsBody += ISNULL(@CellPhone,'') + ' '
					IF (@Field='faxnumber')
						SET @SmsBody += ISNULL(@FaxNumber,'') + ' '
					IF (@Field='job')
						SET @SmsBody += ISNULL(@Job,'') + ' '
					IF (@Field='address')
						SET @SmsBody += ISNULL(@Address,'') + ' '
					IF (@Field='email')
						SET @SmsBody += ISNULL(@email,'') + ' '
					IF(@Field = 'sex')
					BEGIN
						IF (@Sex = 1)
							SET @SmsBody += N'[آقای] ';
						ELSE IF	(@Sex = 2)
							SET @SmsBody += N'[خانم] '; 
					END
				END
				ELSE
				BEGIN
					SET @Field= Substring(@Field,1,CHARINDEX('@$!$@',@Field)-1);
					IF (@Field = 'field1')
						SET @SmsBody += ISNULL(@F1,'') + ' '
					IF (@Field = 'field2')
						SET @SmsBody += ISNULL(@F2,'') + ' '
					IF (@Field = 'field3')
						SET @SmsBody += ISNULL(@F3,'') + ' '
					IF (@Field = 'field4')
						SET @SmsBody += ISNULL(@F4,'') + ' '
					IF (@Field = 'field5')
						SET @SmsBody += ISNULL(@F5,'') + ' '
					IF (@Field = 'field6')
						SET @SmsBody += ISNULL(@F6,'') + ' '
					IF (@Field = 'field7')
						SET @SmsBody += ISNULL(@F7,'') + ' '
					IF (@Field = 'field8')
						SET @SmsBody += ISNULL(@F8,'') + ' '
					IF (@Field = 'field9')
						SET @SmsBody += ISNULL(@F9,'') + ' '
					IF (@Field = 'field10')
						SET @SmsBody += ISNULL(@F10,'') + ' '
					IF (@Field = 'field11')
						SET @SmsBody += ISNULL(@F11,'') + ' '
					IF (@Field = 'field12')
						SET @SmsBody += ISNULL(@F12,'') + ' '
					IF (@Field = 'field13')
						SET @SmsBody += ISNULL(@F13,'') + ' '
					IF (@Field = 'field14')
						SET @SmsBody += ISNULL(@F14,'') + ' '
					IF (@Field = 'field15')
						SET @SmsBody += ISNULL(@F15,'') + ' '
					IF (@Field = 'field16')
						SET @SmsBody += ISNULL(@F16,'') + ' '
					IF (@Field = 'field17')
						SET @SmsBody += ISNULL(@F17,'') + ' '
					IF (@Field = 'field18')
						SET @SmsBody += ISNULL(@F18,'') + ' '
					IF (@Field = 'field19')
						SET @SmsBody += ISNULL(@F19,'') + ' '
					IF (@Field = 'field20')
						SET @SmsBody += ISNULL(@F20,'') + ' '
				END
			END
			ELSE IF(@Prefix = '<(*$')
			BEGIN
				SET @Format = SUBSTRING(@Format,5,LEN(@Format));
				SET @UserText = Substring(@Format,1, (CHARINDEX('$*)>',@Format)-1))
				SET @Format = Substring(@Format,LEN(@UserText)+ 5,LEN(@Format));
				
				SET @SmsBody += @UserText;
			END
		END
		RETURN @SmsBody;
	END


GO
/****** Object:  UserDefinedFunction [dbo].[GetSmsCount]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[GetSmsCount](@Text [nvarchar](max))
RETURNS [int] WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[GetSmsCount]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSolarDate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[GetSolarDate](@EDate smalldatetime) RETURNS varchar(10) AS BEGIN

	DECLARE @FDate varchar(10)	
	DECLARE @EYear int, @EMon smallint, @EDay smallint, @ELeap bit, @EMonArray Char(12), @EDayOfYear int 
	DECLARE @FYear int, @FMon smallint, @FDay smallint, @FLeap bit, @FMonArray Char(12) 
	SELECT @FMonArray = Char(31) + Char(31) + Char(31) + Char(31) + Char(31) + Char(31) + Char(30) + Char(30) + Char(30) + Char(30) + Char(30) + Char(29) 
	SELECT @EMonArray = Char(31) + Char(28) + Char(31) + Char(30) + Char(31) + Char(30) + Char(31) + Char(31) + Char(30) + Char(31) + Char(30) + Char(31) 
	
	SELECT @EYear = Year(@EDate) 
	SELECT @EMon = Month(@EDate) 
	SELECT @EDay = Day(@EDate) 
	IF (@EYear %4) = 0 SELECT @ELeap = 1 ELSE SELECT @ELeap = 0 
	--------------------- Calc Day Of Year 
	DECLARE @Temp int, @Cnt int 
	SELECT @Cnt = @EMon-1 
	SELECT @Temp = 0 
	WHILE @Cnt<>0 BEGIN 
		IF (@Cnt = 2)AND(@ELeap = 1)
			SELECT @Temp = @Temp + 29 
		ELSE
			SELECT @Temp = @Temp + Ascii(Substring(@EMonArray, @Cnt, 1)) 

		SELECT @Cnt = @Cnt-1 
	END 
	SELECT @EDayOfYear = @Temp + @EDay 
	---------------------- Convert to Farsi 
	SELECT @Temp = @EDayOfYear-79 
	IF @Temp>0
		SELECT @FYear = @EYear-621 
	ELSE BEGIN 
		SELECT @FYear = @EYear-622 
		IF ((@FYear %4) = 3)
			SELECT @Temp = @Temp + 366
		ELSE
			SELECT @Temp = @Temp + 365 
	END
	IF (@FYear %4) = 3
		SELECT @FLeap = 1
	ELSE
		SELECT @FLeap = 0

	SELECT @Cnt = 1
	WHILE (@Temp<>0) AND (@Temp>Ascii(Substring(@FMonArray, @Cnt, 1))) 	BEGIN 
		IF @Cnt = 12 
			IF (@FLeap = 1)
				SELECT @Temp = @Temp-30
			ELSE
				SELECT @Temp = @Temp-29 
		ELSE
			SELECT @Temp = @Temp-Ascii(Substring(@FMonArray, @Cnt, 1)) 

		SELECT @Cnt = @Cnt + 1 
	END 
	IF @Temp<>0 BEGIN 
		SELECT @FMon = @Cnt 
		SELECT @FDay = @Temp 
	END ELSE BEGIN 
		SELECT @FMon = 12 
		SELECT @FDay = 30 
	END

	------- Some years has a one_day disposition and has to be corrected manually!!!
	IF @FYear IN (1301, 1302, 1303, 1304, 1305, 1306, 1307, 1310, 1311, 1314, 1315, 1318, 1319, 1322, 1323, 1326, 1327, 1330, 1331, 1334, 1335, 1338, 1339, 1343, 1347, 1351, 1355, 1359, 1363, 1367, 1371)
		SELECT @FDay = @FDay - 1

	------------------ ALTER Output 
	DECLARE @YStr Char(4), @MStr char(2), @DStr Char(2) 
	SELECT @YStr = Convert(Char, @FYear) 
	IF @FMon<10 SELECT @MStr = '0' + Convert(Char,@FMon) ELSE SELECT 
	@MStr = Convert(Char, @FMon) 
	IF @FDay<10 SELECT @DStr = '0' + Convert(Char,@FDay) ELSE SELECT 
	@DStr = Convert(Char, @FDay) 
	SELECT @FDate = @YStr + '/' + @MStr + '/' + @DStr 
	------------------
	RETURN @FDate
END


GO
/****** Object:  UserDefinedFunction [dbo].[HasUniCodeCharacter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[HasUniCodeCharacter](@Text [nvarchar](max))
RETURNS [int] WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[HasUniCodeCharacter]
GO
/****** Object:  UserDefinedFunction [dbo].[InsertLog]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[InsertLog](@Type [int], @Source [nvarchar](256), @Name [nvarchar](256), @Text [nvarchar](max), @Ip [nvarchar](32), @Browser [nvarchar](32), @ReferenceGuid [uniqueidentifier], @UserGuid [uniqueidentifier])
RETURNS [nvarchar](max) WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[InsertLog]
GO
/****** Object:  UserDefinedFunction [dbo].[IsMatch]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[IsMatch](@input [nvarchar](32), @pattern [nvarchar](255))
RETURNS [int] WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[IsMatch]
GO
/****** Object:  UserDefinedFunction [dbo].[IsValidNationalCode]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[IsValidNationalCode](@nationalCode [nchar](10))
RETURNS [int] WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[IsValidNationalCode]
GO
/****** Object:  UserDefinedFunction [dbo].[JoinString]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[JoinString](@str [nvarchar](max), @separator [nvarchar](1), @columnName [nvarchar](64))
RETURNS [nvarchar](max) WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[JoinString]
GO
/****** Object:  UserDefinedFunction [dbo].[NumberToAlpha]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [dbo].[NumberToAlpha] (@Number Numeric (38, 0)) RETURNS VARCHAR(8000) -- Input number with as many as 18 digits
  AS
 
BEGIN

	DECLARE @inputNumber VARCHAR(38)
	DECLARE @outputString VARCHAR(8000)
	DECLARE @length INT
	DECLARE @counter INT
	DECLARE @loops INT
	DECLARE @position INT
	DECLARE @chunk CHAR(3) -- for chunks of 3 numbers
	DECLARE @tensones CHAR(2)
	DECLARE @hundreds CHAR(1)
	DECLARE @tens CHAR(1)
	DECLARE @ones CHAR(1)
	DECLARE @NumbersTable TABLE (number CHAR(2), word VARCHAR(10))

	IF @Number = 0 Return 'صفر'

	-- initialize the variables
	SELECT @inputNumber = CONVERT(varchar(38), @Number)
		 , @outputString = ''
		 , @counter = 1
	SELECT @length   = LEN(@inputNumber)
		 , @position = LEN(@inputNumber) - 2
		 , @loops    = LEN(@inputNumber)/3

	-- make sure there is an extra loop added for the remaining numbers
	IF LEN(@inputNumber) % 3 <> 0 SET @loops = @loops + 1

	-- insert data for the numbers and words
	INSERT INTO @NumbersTable   SELECT '00', ''
		UNION ALL SELECT '01', 'یک'      UNION ALL SELECT '02', 'دو'
		UNION ALL SELECT '03', 'سه'    UNION ALL SELECT '04', 'چهار'
		UNION ALL SELECT '05', 'پنج'     UNION ALL SELECT '06', 'شش'
		UNION ALL SELECT '07', 'هفت'    UNION ALL SELECT '08', 'هشت'
		UNION ALL SELECT '09', 'نه'     UNION ALL SELECT '10', 'ده'
		UNION ALL SELECT '11', 'یازده'   UNION ALL SELECT '12', 'دوازده'
		UNION ALL SELECT '13', 'سیزده' UNION ALL SELECT '14', 'چهارده'
		UNION ALL SELECT '15', 'پانزده'  UNION ALL SELECT '16', 'شانزده'
		UNION ALL SELECT '17', 'هفده' UNION ALL SELECT '18', 'هجده'
		UNION ALL SELECT '19', 'نوزده' UNION ALL SELECT '20', 'بیست'
		UNION ALL SELECT '30', 'سی'   UNION ALL SELECT '40', 'چهل'
		UNION ALL SELECT '50', 'پنجاه'    UNION ALL SELECT '60', 'شصت'
		UNION ALL SELECT '70', 'هفتاد'  UNION ALL SELECT '80', 'هشتاد'
		UNION ALL SELECT '90', 'نود'   

	WHILE @counter <= @loops BEGIN

		-- get chunks of 3 numbers at a time, padded with leading zeros
		SET @chunk = RIGHT('000' + SUBSTRING(@inputNumber, @position, 3), 3)

		IF @chunk <> '000' BEGIN
			SELECT @tensones = SUBSTRING(@chunk, 2, 2)
				 , @hundreds = SUBSTRING(@chunk, 1, 1)
				 , @tens = SUBSTRING(@chunk, 2, 1)
				 , @ones = SUBSTRING(@chunk, 3, 1)

			-- If twenty or less, use the word directly from @NumbersTable
			IF CONVERT(INT, @tensones) <= 20 OR @ones='0' BEGIN
				SET @outputString = (SELECT word 
										  FROM @NumbersTable 
										  WHERE @tensones = number)
					   + CASE @counter WHEN 1 THEN '' -- No name
						   WHEN 2 THEN ' هزار و' WHEN 3 THEN '  میلیون و'
						   WHEN 4 THEN ' بیلیون و '  WHEN 5 THEN ' ترلیون و '
						   WHEN 6 THEN ' quadrillion ' WHEN 7 THEN ' quintillion '
						   WHEN 8 THEN ' sextillion '  WHEN 9 THEN ' septillion '
						   WHEN 10 THEN ' octillion '  WHEN 11 THEN ' nonillion '
						   WHEN 12 THEN ' decillion '  WHEN 13 THEN ' undecillion '
						   ELSE '' END
								   + @outputString
				END
			 ELSE BEGIN -- break down the ones and the tens separately

				 SET @outputString = ' ' 
								+ (SELECT word 
										FROM @NumbersTable 
										WHERE @tens + '0' = number)
								 +' و '
								 + (SELECT word 
										FROM @NumbersTable 
										WHERE '0'+ @ones = number)
					   + CASE @counter WHEN 1 THEN '' -- No name
						   WHEN 2 THEN ' هزار و ' WHEN 3 THEN ' میلیون و'
						   WHEN 4 THEN ' بیلیون و'  WHEN 5 THEN ' ترلیون و '
						   WHEN 6 THEN ' quadrillion ' WHEN 7 THEN ' quintillion '
						   WHEN 8 THEN ' sextillion '  WHEN 9 THEN ' septillion '
						   WHEN 10 THEN ' octillion '  WHEN 11 THEN ' nonillion '
						   WHEN 12 THEN ' decillion '   WHEN 13 THEN ' undecillion '
						   ELSE '' END
								+ @outputString
			END

			-- now get the hundreds
			IF @hundreds <> '0' BEGIN
				SET @outputString  = (SELECT word 
										  FROM @NumbersTable 
										  WHERE '0' + @hundreds = number)
									+ ' صد ' 
									+ @outputString
			END
		END

		SELECT @counter = @counter + 1
			 , @position = @position - 3

	END

	-- Remove any double spaces
	SET @outputString = LTRIM(RTRIM(REPLACE(@outputString, '  ', ' ')))
	SET @outputString = UPPER(LEFT(@outputString, 1)) + SUBSTRING(@outputString, 2, 8000)


	SET @outputString = (select replace (@outputString, 'دو صد', 'دویست'))
	SET @outputString = (select replace (@outputString, 'سه صد', 'سیصد'))
	SET @outputString = (select replace (@outputString, 'پنج صد', 'پانصد'))

	RETURN REPLACE(@outputString + ' ریال ',' و ریال ',' ریال ')-- return the result
END


GO
/****** Object:  UserDefinedFunction [dbo].[RepairMobileNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[RepairMobileNumber](@Mobile [nvarchar](16))
RETURNS NVARCHAR(16)
  AS
 BEGIN

IF(SUBSTRING(@Mobile,1,2) = '09')
	SET @Mobile = '98' + SUBSTRING(@Mobile,2,LEN(@Mobile));
ELSE IF(SUBSTRING(@Mobile,1,3) = '+98')
	SET @Mobile = SUBSTRING(@Mobile,2,LEN(@Mobile));
ELSE IF(SUBSTRING(@Mobile,1,4) = '0098')
	SET @Mobile = SUBSTRING(@Mobile,3,LEN(@Mobile));
ELSE IF(SUBSTRING(@Mobile,1,2) = '98')
	SET @Mobile = SUBSTRING(@Mobile,1,LEN(@Mobile));
ELSE IF(SUBSTRING(@Mobile,1,1) = '9')
	SET @Mobile = '98' + SUBSTRING(@Mobile,1,LEN(@Mobile));

RETURN @Mobile;

END
GO
/****** Object:  UserDefinedFunction [dbo].[SendRequestToUrl]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[SendRequestToUrl](@Url [nvarchar](max))
RETURNS [nvarchar](max) WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[SendRequestToUrl]
GO
/****** Object:  UserDefinedFunction [dbo].[SendSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE FUNCTION [dbo].[SendSms](@Queue [nvarchar](128), @IsRemoteQueue [int], @RemoteQueueIP [nvarchar](64), @SmsSendType [int], @PageNo [int], @Sender [nvarchar](32), @PrivateNumberGuid [uniqueidentifier], @TotalCount [int], @Receivers [nvarchar](max), @ServiceId [nvarchar](32), @Message [nvarchar](max), @SmsLen [int], @TryCount [int], @SmsIdentifier [bigint] = 0, @SmsPartIndex [int] = 0, @IsFlash [int], @IsUnicode [int], @Id [nvarchar](64), @Guid [nvarchar](36), @Username [nvarchar](32), @Password [nvarchar](32), @Domain [nvarchar](32), @SendLink [nvarchar](512), @ReceiveLink [nvarchar](512), @DeliveryLink [nvarchar](512), @AgentReference [int])
RETURNS [nvarchar](max) WITH EXECUTE AS CALLER
AS 
EXTERNAL NAME [SqlLibrary].[SqlLibrary.SQLHelper].[SendSms]
GO
/****** Object:  UserDefinedFunction [dbo].[SplitLarge]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[SplitLarge](@String NVARCHAR(MAX), @Delimiter char(1))     
returns @temptable TABLE (items NVARCHAR(MAX))     
as     
begin     
    declare @idx int     
    declare @slice NVARCHAR(MAX)     

    select @idx = 1     
        if len(@String)<1 or @String is null  return     

    while @idx!= 0     
    begin     
        set @idx = charindex(@Delimiter,@String)     
        if @idx!=0     
            set @slice = left(@String,@idx - 1)     
        else     
            set @slice = @String     

        if(len(@slice)>0)
            insert into @temptable(Items) values(@slice)     

        set @String = right(@String,len(@String) - @idx)     
        if len(@String) = 0 break     
    end 
return     
end


GO
/****** Object:  UserDefinedFunction [dbo].[udfFileExist]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfFileExist](@path nvarchar(4000))
RETURNS BIT
  AS

BEGIN
  DECLARE @result INT
  EXEC master.dbo.xp_fileexist @path, @result OUTPUT
  RETURN cast(@result as bit)
END;

GO
/****** Object:  UserDefinedFunction [dbo].[udfGetAllAgentChildren]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetAllAgentChildren](@ParentGuid AS UNIQUEIDENTIFIER) RETURNS @ChildUsers TABLE([UserGuid] UNIQUEIDENTIFIER,[UserName] NVARCHAR(50))
  AS

BEGIN
	WITH [ChildUsers]([Guid], [ParentGuid])
		AS
		(
					SELECT 
								[Guid],
								[ParentGuid]
					FROM
								[Users]
					WHERE 
								[Guid] = @ParentGuid AND
								[IsDeleted] = 0
								
					UNION ALL
					
					SELECT 
								Child.[Guid],
								Child.[ParentGuid]
					FROM 
								[Users] Child INNER JOIN 
								[ChildUsers] Tree	ON Child.[ParentGuid] = Tree.[Guid]
					WHERE
								Child.[IsDeleted] = 0 AND
                Child.[MaximumUser] > 0
		)
		INSERT INTO @ChildUsers
			SELECT 
						[Users].[Guid],
						[Users].[UserName]
			FROM 
						[Users] INNER JOIN 
						[ChildUsers] AS Tree	ON [Users].[Guid] = Tree.[Guid]
						
	RETURN;
END


GO
/****** Object:  UserDefinedFunction [dbo].[udfGetAllChildren]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetAllChildren](@ParentGuid AS UNIQUEIDENTIFIER) RETURNS @ChildUsers TABLE([UserGuid] UNIQUEIDENTIFIER,[UserName] NVARCHAR(50))
  AS

BEGIN
	WITH [ChildUsers]([Guid], [ParentGuid])
		AS
		(
					SELECT 
								[Guid],
								[ParentGuid]
					FROM
								[Users]
					WHERE 
								[Guid] = @ParentGuid AND
								[IsDeleted] = 0
								
					UNION ALL
					
					SELECT 
								Child.[Guid],
								Child.[ParentGuid]
					FROM 
								[Users] Child INNER JOIN 
								[ChildUsers] Tree	ON Child.[ParentGuid] = Tree.[Guid]
					WHERE
								Child.[IsDeleted] = 0
		)
		INSERT INTO @ChildUsers
			SELECT 
						[Users].[Guid],
						[Users].[UserName]
			FROM 
						[Users] INNER JOIN 
						[ChildUsers] AS Tree	ON [Users].[Guid] = Tree.[Guid]
						
	RETURN;
END


GO
/****** Object:  UserDefinedFunction [dbo].[udfGetAllParents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetAllParents](@ChildGuid AS UNIQUEIDENTIFIER) RETURNS @ParentUsers TABLE([UserGuid] UNIQUEIDENTIFIER)
  AS

BEGIN
	WITH [ParentUsers]([Guid], [ParentGuid])
		AS
		(
					SELECT 
								[Guid],
								[ParentGuid]
					FROM
								[Users]
					WHERE 
								[Guid] = @ChildGuid AND
								[IsDeleted] = 0
								
					UNION ALL
					
					SELECT 
								Parent.[Guid],
								Parent.[ParentGuid]
					FROM 
								[Users] Parent INNER JOIN 
								[ParentUsers] Tree	ON Parent.[Guid] = Tree.[ParentGuid]
					WHERE
								Parent.[IsDeleted] = 0
		)
		INSERT INTO @ParentUsers
			SELECT 
						[Users].[Guid]
			FROM 
						[Users] INNER JOIN 
						[ParentUsers] AS Tree	ON [Users].[Guid] = Tree.[Guid]
						
	RETURN;
END


GO
/****** Object:  UserDefinedFunction [dbo].[udfGetBlockedProcessReport]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetBlockedProcessReport]
(
	@FileName NVARCHAR(256) = ''
)
RETURNS @BlockedProcess TABLE
(
	Duration										BIGINT,
	StartTime										DATETIME,
	EndTime											DATETIME,
	Blocking										NVARCHAR(2000),
	Blocked											NVARCHAR(2000),
	textdataXML									XML,
	Blocking_SPID								INT,
	Blocked_SPID								INT,
	Blocking_lastbatchstarted		NVARCHAR(2000),
	Blocking_Cur_DB							NVARCHAR(128),
	Blocked_Cur_DB							NVARCHAR(128),
	Blocking_HostName						NVARCHAR(2000),
	Blocked_HostName						NVARCHAR(2000),
	Blocking_LoginName					NVARCHAR(2000),
	Blocked_LoginName						NVARCHAR(2000)
)
AS
BEGIN
	EXEC @FileName = udfGetTraceFileName N'BlockedProcessTrace', @FileName;

	INSERT
		@BlockedProcess
	SELECT 
		MaxTraces.Duration,
		MaxTraces.StartTime,
		MaxTraces.EndTime,

		CASE WHEN 
			CHARINDEX('Proc [Database Id =', CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)')) > 0 
		THEN
			OBJECT_NAME(REPLACE(SUBSTRING(CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)'), 
			PATINDEX('%Object Id = [0-9]%',CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)')) + 12, 50), ']', ''), 
			CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/@currentdb', 'int'))
		ELSE
			CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)')
		END AS Blocking ,

		CASE WHEN 
			CHARINDEX('Proc [Database Id =', CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)')) > 0 
		THEN
			OBJECT_NAME(REPLACE(SUBSTRING(CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)'), 
			PATINDEX('%Object Id = [0-9]%',CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)')) + 12, 50), ']', ''), 
			CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/@currentdb', 'int'))
		ELSE
			CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/inputbuf[1]', 'nvarchar(2000)')
		END AS Blocked,

		CAST(TextData as xml) as textdataXML,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/@spid', 'int') AS Blocking_SPID ,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/@spid', 'int') AS Blocked_SPID ,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/@lastbatchstarted', 'nvarchar(2000)') AS Blocking_lastbatchstarted,    
		db_name(CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/@currentdb', 'int')) AS Blocking_Cur_DB ,
		db_name(CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/@currentdb', 'int')) AS Blocked_Cur_DB ,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/@hostname', 'nvarchar(2000)') AS Blocking_HostName ,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/@hostname', 'nvarchar(2000)') AS Blocked_HostName ,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocking-process[1]/process[1]/@loginname', 'nvarchar(2000)') AS Blocking_LoginName ,
		CAST(TextData AS XML).value('blocked-process-report[1]/blocked-process[1]/process[1]/@loginname', 'nvarchar(2000)') AS Blocked_LoginName     
	FROM 
		FN_TRACE_GETTABLE(@FileName, DEFAULT) AllTraces INNER JOIN
		(SELECT TransactionID, MAX(Duration) / 1000000 AS Duration, MIN(StartTime) AS StartTime, MAX(EndTime) AS EndTime, MAX(EventSequence) AS EventSequence
			FROM FN_TRACE_GETTABLE(@FileName, DEFAULT)
			GROUP BY TransactionID) MaxTraces ON (AllTraces.TransactionID = MaxTraces.TransactionID AND AllTraces.EventSequence = MaxTraces.EventSequence)
	WHERE
		TextData IS NOT NULL

	RETURN;
END
GO
/****** Object:  UserDefinedFunction [dbo].[udfGetDeadlockReport]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetDeadlockReport]
(
	@FileName NVARCHAR(256) = ''
)
RETURNS @Deadlock TABLE
(
	TextDataXml			XML,
	Deadlock_DB			NVARCHAR(128),
	Deadlock_Table	NVARCHAR(2000),
	StartTime				DATETIME,
	LoginName				NVARCHAR(256),
	ApplicationName	NVARCHAR(256),
	SPID						INT
)
AS
BEGIN
	EXEC @FileName = udfGetTraceFileName N'DeadlockTrace', @FileName;

	INSERT
		@Deadlock
	SELECT 
		CAST(TextData as xml) TextDataXml, 
		db_name(CAST(TextData AS XML).value('deadlock-list[1]/deadlock[1]/process-list[1]/process[1]/@currentdb', 'int')) AS Deadlock_DB,

		CASE WHEN 
			CAST(TextData AS XML).value('deadlock-list[1]/deadlock[1]/resource-list[1]/pagelock[1]/@objectname', 'nvarchar(2000)') IS NULL
		THEN
			CAST(TextData AS XML).value('deadlock-list[1]/deadlock[1]/resource-list[1]/keylock[1]/@objectname', 'nvarchar(2000)')
		ELSE
			CAST(TextData AS XML).value('deadlock-list[1]/deadlock[1]/resource-list[1]/pagelock[1]/@objectname', 'nvarchar(2000)')
		END AS Deadlock_Table,

		StartTime, 
		LoginName, 
		ApplicationName,
		SPID
	FROM 
		FN_TRACE_GETTABLE(@FileName, DEFAULT)
	WHERE 
		[TextData] IS NOT NULL

	RETURN;
END
GO
/****** Object:  UserDefinedFunction [dbo].[udfGetFirstParentMainAdmin]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetFirstParentMainAdmin](@UserGuid UNIQUEIDENTIFIER) RETURNS UNIQUEIDENTIFIER
  AS


BEGIN
	DECLARE @TempParentGuid UNIQUEIDENTIFIER;
	DECLARE	@EmptyGuid UNIQUEIDENTIFIER='00000000-0000-0000-0000-000000000000';
	DECLARE @IsMainAdmin BIT = 0;
		
	SELECT
		@TempParentGuid = [ParentGuid],
		@IsMainAdmin = [IsMainAdmin] 
	FROM
		[Users]
	WHERE
		[Guid] = @UserGuid;

	IF(@IsMainAdmin = 1)
		RETURN @UserGuid;
		
	RETURN [dbo].[udfGetFirstParentMainAdmin](@TempParentGuid)
END


GO
/****** Object:  UserDefinedFunction [dbo].[udfGetFirstParentNumberForUseChildren]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetFirstParentNumberForUseChildren](@UserGuid UNIQUEIDENTIFIER) RETURNS UNIQUEIDENTIFIER
  AS

	BEGIN
		
		DECLARE @TempParentGuid UNIQUEIDENTIFIER,
						@EmptyGuid UNIQUEIDENTIFIER='00000000-0000-0000-0000-000000000000';
		
		SELECT
			@TempParentGuid = [ParentGuid]
		FROM
			[Users]
		WHERE
			[Guid] = @UserGuid;

		IF((SELECT COUNT(*) FROM [UserPrivateNumbers] WHERE [UserGuid]=@TempParentGuid AND [UseForChildren]=1 AND [UseType]=2)= 0 AND @TempParentGuid!=@EmptyGuid)
			RETURN [dbo].[udfGetFirstParentNumberForUseChildren](@TempParentGuid)

		RETURN @TempParentGuid
	END


GO
/****** Object:  UserDefinedFunction [dbo].[udfGetLongDurationReport]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetLongDurationReport]
(
	@FileName NVARCHAR(256) = ''
)
RETURNS @LongDuration TABLE
(
	Duration_Second	BIGINT,
	IO_KB						BIGINT,
	DatabaseName		NVARCHAR(256),
	TextData				NVARCHAR(MAX),
	FromTable				NVARCHAR(100),
	StartTime				DATETIME,
	EndTime					DATETIME,
	Reads						BIGINT,
	Writes					BIGINT,
	CPU							INT,
	ApplicationName	NVARCHAR(256),
	LoginName				NVARCHAR(256),
	SPID						INT,
	TransactionID		BIGINT,
	Error						INT
)
AS
BEGIN
	EXEC @FileName = udfGetTraceFileName N'LongDurationTrace', @FileName;

	INSERT
		@LongDuration
	SELECT 
		Duration / 1000000 AS Duration_Second, 
		(Reads + Writes) * 8 AS IO_KB, 
		DatabaseName, 
		CAST(TextData AS NVARCHAR(MAX)) AS TextData,
		CASE WHEN CHARINDEX('From', TextData) > 0 THEN SUBSTRING(TextData, CHARINDEX('From', TextData) + 5, 100) ELSE NULL END AS FromTable,
		StartTime,
		EndTime,
		Reads, 
		Writes, 
		CPU, 
		ApplicationName, 
		LoginName, 
		SPID, 
		TransactionID,
		Error -- 0:Ok, 1:Error, 2:Abort
	FROM 
		FN_TRACE_GETTABLE(@FileName, DEFAULT) 
	WHERE
		TextData IS NOT NULL

	RETURN;
END
GO
/****** Object:  UserDefinedFunction [dbo].[udfGetTraceFileName]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetTraceFileName]
(
	@TraceName NVARCHAR(256) = '',
	@FileName NVARCHAR(256) = ''
)
RETURNS NVARCHAR(256)
AS
BEGIN
	IF(ISNULL(@TraceName, '') = '' AND ISNULL(@FileName, '') = '')	
		RETURN '';

	IF(ISNULL(@FileName, '') = '')
	BEGIN
		DECLARE @TraceFilesPath NVARCHAR(256);

		EXEC @TraceFilesPath = udfGetTraceFilesPath;

		SET @FileName = @TraceFilesPath + @TraceName;
	END
		
	IF(SUBSTRING(@FileName, LEN(@FileName) - 3, 4) != '.trc')
		SET @FileName = @FileName + '.trc';

	RETURN @FileName;
END
GO
/****** Object:  UserDefinedFunction [dbo].[udfGetTraceFilesPath]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfGetTraceFilesPath] ()
	RETURNS NVARCHAR(256)
AS
BEGIN
	DECLARE @TraceFilePath NVARCHAR(256) = 'E:\SQL Trace Logs';

	IF (ISNULL(@TraceFilePath, '') = '')
		RETURN '';

	IF(SUBSTRING(@TraceFilePath, LEN(@TraceFilePath), 1) != '\')
		SET @TraceFilePath = @TraceFilePath + '\';
	
	RETURN @TraceFilePath;
END
GO
/****** Object:  UserDefinedFunction [dbo].[udfIsUserHierarchyActive]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[udfIsUserHierarchyActive](@UserGuid UNIQUEIDENTIFIER) RETURNS BIT
  AS

BEGIN
	DECLARE @IsActive BIT = 1,
					@ParentGuid UNIQUEIDENTIFIER
	
	IF(@UserGuid = '00000000-0000-0000-0000-000000000000')
		RETURN @IsActive
	ELSE
		BEGIN
			SELECT
						@IsActive = [IsActive],
						@ParentGuid = [ParentGuid]
			FROM
						[Users]
			WHERE
						[Guid] = @UserGuid AND 
						[IsDeleted] = 0
			
			IF(@IsActive = 0)
				RETURN 0
			ELSE IF(@ParentGuid = '00000000-0000-0000-0000-000000000000')
				RETURN 1
			ELSE	
				RETURN [dbo].[udfIsUserHierarchyActive](@ParentGuid)
		END
		
	RETURN 0	
END


GO
/****** Object:  Table [dbo].[UserEmailSettings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEmailSettings](
	[Guid] [uniqueidentifier] NOT NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[Password] [nvarchar](100) NULL,
	[Type] [int] NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserEmailSettings] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1000,1) NOT NULL,
	[UserName] [nvarchar](32) NULL,
	[Password] [nvarchar](512) NULL,
	[SecondPassword] [nvarchar](512) NULL,
	[FirstName] [nvarchar](32) NULL,
	[LastName] [nvarchar](64) NULL,
	[FatherName] [nvarchar](32) NULL,
	[NationalCode] [nchar](10) NULL,
	[ZipCode] [nchar](10) NULL,
	[ShCode] [nvarchar](16) NULL,
	[Email] [nvarchar](128) NULL,
	[Phone] [nvarchar](16) NULL,
	[Mobile] [nvarchar](16) NULL,
	[FaxNumber] [nvarchar](16) NULL,
	[Address] [nvarchar](max) NULL,
	[ZoneGuid] [uniqueidentifier] NULL,
	[BirthDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[Credit] [decimal](18, 2) NULL,
	[CompanyName] [nvarchar](64) NULL,
	[CompanyNationalId] [nvarchar](32) NULL,
	[EconomicCode] [nvarchar](32) NULL,
	[CompanyAddress] [nvarchar](255) NULL,
	[CompanyPhone] [nvarchar](16) NULL,
	[CompanyZipCode] [nchar](10) NULL,
	[CompanyCEOName] [nvarchar](32) NULL,
	[CompanyCEONationalCode] [nchar](10) NULL,
	[CompanyCEOMobile] [nvarchar](16) NULL,
	[PanelPrice] [decimal](18, 2) NULL,
	[Type] [int] NULL,
	[IsActive] [bit] NULL,
	[IsAuthenticated] [bit] NULL,
	[IsActiveSend] [bit] NULL,
	[IsAdmin] [bit] NULL,
	[IsSuperAdmin] [bit] NULL,
	[IsMainAdmin] [bit] NULL,
	[MaximumAdmin] [int] NULL,
	[MaximumUser] [int] NULL,
	[MaximumEmailAddress] [int] NULL,
	[MaximumPhoneNumber] [int] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[DomainGroupPriceGuid] [uniqueidentifier] NULL,
	[PriceGroupGuid] [uniqueidentifier] NULL,
	[IsFixPriceGroup] [bit] NULL,
	[DomainGuid] [uniqueidentifier] NULL,
	[RoleGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwUserEmailSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwUserEmailSetting]
  AS

SELECT     dbo.UserEmailSettings.Guid, dbo.UserEmailSettings.EmailAddress, dbo.UserEmailSettings.Password, dbo.UserEmailSettings.Type, 
                      dbo.UserEmailSettings.CreateDate, dbo.UserEmailSettings.IsDeleted AS UserEmailSettingIsDeleted, dbo.UserEmailSettings.UserGuid, 
                      dbo.Users.Credit AS UserCredit, dbo.Users.IsActive AS IsUserActive, dbo.Users.IsAdmin, dbo.Users.IsMainAdmin, dbo.Users.ParentGuid, 
                      dbo.Users.IsDeleted AS UserIsDeleted
FROM         dbo.UserEmailSettings INNER JOIN
                      dbo.Users ON dbo.UserEmailSettings.UserGuid = dbo.Users.Guid
WHERE     (dbo.UserEmailSettings.IsDeleted = 0) AND (dbo.Users.IsDeleted = 0)


GO
/****** Object:  View [dbo].[vwUserEmailSettings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwUserEmailSettings]
  AS

	SELECT * FROM [UserEmailSettings]


GO
/****** Object:  Table [dbo].[UserMessages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserMessages](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Email] [nvarchar](50) NULL,
	[Telephone] [nvarchar](50) NULL,
	[CellPhone] [nvarchar](50) NULL,
	[Job] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[DomainGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwUserMessages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwUserMessages]
  AS

SELECT
	[Guid],
	[Name],
	[CreateDate],
	[Email],
	[IsDeleted],
	[DomainGuid],
	[Job], 
	[Description],
	[CellPhone],
	[Telephone]
FROM
	[dbo].[UserMessages]


GO
/****** Object:  Table [dbo].[TrafficRelays]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrafficRelays](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Url] [nvarchar](max) NOT NULL,
	[TryCount] [int] NULL,
	[CreateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_TrafficRelays] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwTrafficRelays]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwTrafficRelays]
  AS

SELECT        dbo.TrafficRelays.*
FROM            dbo.TrafficRelays

GO
/****** Object:  Table [dbo].[Accesses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accesses](
	[Guid] [uniqueidentifier] NOT NULL,
	[ReferencePermissionsKey] [int] NULL,
	[IsDeleted] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[ServiceGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Accesses] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwAccesses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwAccesses]
  AS

SELECT 
			[Guid] ,
			[ReferencePermissionsKey] ,
			[IsDeleted] ,
			[ServiceGuid] 
FROM [Accesses]


GO
/****** Object:  Table [dbo].[AccountInformations]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountInformations](
	[Guid] [uniqueidentifier] NOT NULL,
	[Owner] [nvarchar](50) NULL,
	[Branch] [nvarchar](50) NULL,
	[AccountNo] [nvarchar](100) NULL,
	[CardNo] [nvarchar](50) NULL,
	[TerminalID] [nvarchar](50) NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[PinCode] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Bank] [int] NULL,
	[IsActive] [bit] NULL,
	[OnlineGatewayIsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_AccountInformations] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwAccountInformations]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwAccountInformations]
  AS

SELECT        Guid, Owner, Branch, AccountNo, TerminalID, UserName, Password, CreateDate, Bank, IsActive, OnlineGatewayIsActive, IsDeleted, UserGuid, PinCode, CardNo
FROM            dbo.AccountInformations



GO
/****** Object:  Table [dbo].[DataCenters]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataCenters](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[Location] [int] NULL,
	[Desktop] [int] NULL,
	[CreateDate] [datetime] NULL,
	[IsArchived] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DataCenters] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDataCenters]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwDataCenters]
  AS

SELECT        dbo.DataCenters.*
FROM            dbo.DataCenters

GO
/****** Object:  Table [dbo].[Datas]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Datas](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Summary] [nvarchar](max) NULL,
	[Content] [nvarchar](max) NULL,
	[Keywords] [nvarchar](max) NULL,
	[FromDate] [datetime] NULL,
	[ToDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[Priority] [int] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[ShowInHomePage] [bit] NULL,
	[IsArchived] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[DataCenterGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Datas] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDatas]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwDatas]
  AS

SELECT
	[Guid],
	[Title],
	[Summary],
	[Content], 
	[FromDate],
	[ToDate],
	[CreateDate],
	[Priority],
	[ParentGuid],
	[IsActive], 
	[IsArchived], 
	[IsDeleted], 
	[DataCenterGuid],
	[ShowInHomePage],
	[Keywords],
	[ID]
FROM
    [dbo].[Datas]


GO
/****** Object:  Table [dbo].[Domains]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Domains](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Desktop] [int] NULL,
	[DefaultPage] [int] NULL,
	[Theme] [int] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Domains] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwDomains]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwDomains]
  AS

SELECT        dbo.Domains.Guid, dbo.Domains.Name, dbo.Domains.CreateDate, dbo.Domains.Desktop, dbo.Domains.DefaultPage, dbo.Domains.Theme, dbo.Domains.IsDeleted, dbo.Domains.UserGuid, 
                         dbo.Users.UserName
FROM            dbo.Domains INNER JOIN
                         dbo.Users ON dbo.Domains.UserGuid = dbo.Users.Guid

GO
/****** Object:  Table [dbo].[GalleryImages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GalleryImages](
	[Guid] [uniqueidentifier] NOT NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[Title] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_GalleryImages] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwGalleryImages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwGalleryImages]
  AS

SELECT     dbo.GalleryImages.*
FROM         dbo.GalleryImages


GO
/****** Object:  Table [dbo].[GroupPrices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPrices](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[MinimumMessage] [bigint] NULL,
	[MaximumMessage] [bigint] NULL,
	[BasePrice] [decimal](18, 2) NULL,
	[DecreaseTax] [bit] NULL,
	[AgentRatio] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[IsPrivate] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK_GroupPrices] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwGroupPrices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwGroupPrices]
  AS

SELECT        Guid, Title, CreateDate, IsDeleted, UserGuid, IsDefault, MinimumMessage, MaximumMessage, BasePrice, AgentRatio, DecreaseTax, IsPrivate
FROM            dbo.GroupPrices

GO
/****** Object:  Table [dbo].[Images]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[GalleryImageGuid] [uniqueidentifier] NULL,
	[DataGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwImages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwImages]
  AS

SELECT
	[Guid],
	[Title],
	[Description],
	[ImagePath],
	[CreateDate],
	[IsActive],
	[IsDeleted],
	[GalleryImageGuid],
	[DataGuid]
FROM
	[dbo].[Images]


GO
/****** Object:  Table [dbo].[Inboxes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inboxes](
	[Guid] [uniqueidentifier] NOT NULL,
	[Status] [smallint] NULL,
	[Receiver] [nvarchar](255) NULL,
	[Sender] [nvarchar](25) NULL,
	[SmsLen] [int] NULL,
	[SmsText] [nvarchar](max) NULL,
	[ReceiveDateTime] [datetime] NULL,
	[SendDateTime] [datetime] NULL,
	[IsUnicode] [bit] NULL,
	[IsSent] [bit] NULL,
	[IsRead] [bit] NULL,
	[ShowAlert] [bit] NULL,
	[SendSmsToUrlCount] [int] NULL,
	[Udh] [int] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
	[conversation_handle] [uniqueidentifier] NULL,
	[InboxGroupGuid] [uniqueidentifier] NULL,
	[ParserFormulaGuid] [uniqueidentifier] NULL,
	[ResponseOfRelay] [nvarchar](512) NULL,
 CONSTRAINT [PK_Inboxes] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwInboxes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwInboxes]
  AS

SELECT        dbo.Inboxes.*
FROM            dbo.Inboxes

GO
/****** Object:  Table [dbo].[SmsSenderAgents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsSenderAgents](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[SmsSenderAgentReference] [int] NULL,
	[Type] [tinyint] NULL,
	[SendSmsAlert] [bit] NULL,
	[IsSendActive] [bit] NULL,
	[IsRecieveActive] [bit] NULL,
	[IsSendBulkActive] [bit] NULL,
	[SendBulkIsAutomatic] [bit] NULL,
	[CheckMessageID] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[DefaultNumber] [nvarchar](32) NULL,
	[StartSendTime] [time](0) NULL,
	[EndSendTime] [time](0) NULL,
	[RouteActive] [bit] NULL,
	[QueueLength] [int] NULL,
	[IsSmpp] [bit] NULL,
	[Username] [nvarchar](32) NULL,
	[Password] [nvarchar](32) NULL,
	[SendLink] [nvarchar](512) NULL,
	[ReceiveLink] [nvarchar](512) NULL,
	[DeliveryLink] [nvarchar](512) NULL,
	[Domain] [nvarchar](32) NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SmsSenderAgents] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrivateNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivateNumbers](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1000,1) NOT NULL,
	[Number] [nvarchar](32) NULL,
	[Price] [decimal](18, 2) NULL,
	[ServiceID] [nvarchar](32) NULL,
	[MTNServiceId] [nvarchar](32) NULL,
	[AggServiceId] [nvarchar](32) NULL,
	[ServicePrice] [decimal](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[Type] [int] NULL,
	[Priority] [int] NULL,
	[ReturnBlackList] [bit] NULL,
	[SendToBlackList] [bit] NULL,
	[CheckFilter] [bit] NULL,
	[DeliveryBase] [bit] NULL,
	[HasSLA] [bit] NULL,
	[TryCount] [int] NULL,
	[Range] [nvarchar](255) NULL,
	[Regex] [nvarchar](255) NULL,
	[UseForm] [int] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[OwnerGuid] [uniqueidentifier] NULL,
	[IsRoot] [bit] NULL,
	[IsActive] [bit] NULL,
	[IsDefault] [bit] NULL,
	[IsPublic] [bit] NULL,
	[SendCount] [bigint] NULL,
	[RecieveCount] [bigint] NULL,
	[SuccessCount] [bigint] NULL,
	[IsDeleted] [bit] NULL,
	[SmsSenderAgentGuid] [uniqueidentifier] NULL,
	[SmsTrafficRelayGuid] [uniqueidentifier] NULL,
	[DeliveryTrafficRelayGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PrivateNumbers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwNumbers]
  AS

SELECT        dbo.PrivateNumbers.Guid AS NumberGuid, dbo.PrivateNumbers.Number, dbo.PrivateNumbers.IsActive AS NumberIsActive, dbo.PrivateNumbers.CreateDate AS NumberCreateDate, 
                         dbo.PrivateNumbers.ExpireDate AS NumberExpireDate, dbo.PrivateNumbers.UserGuid AS NumberUserGuid, dbo.Users.UserName, dbo.PrivateNumbers.IsDeleted AS NumberIsDeleted, dbo.Users.DomainGuid, 
                         dbo.Users.IsDeleted AS UserIsDeleted, dbo.PrivateNumbers.SmsSenderAgentGuid, dbo.SmsSenderAgents.Name AS SmsSenderAgentName, dbo.SmsSenderAgents.SmsSenderAgentReference, 
                         dbo.SmsSenderAgents.IsSendActive AS SmsSenderAgentIsSendActive, dbo.SmsSenderAgents.IsRecieveActive AS SmsSenderAgentIsRecieveActive, 
                         dbo.SmsSenderAgents.IsSendBulkActive AS SmsSenderAgentIsSendBulkActive, dbo.PrivateNumbers.ServicePrice AS NumberPrice
FROM            dbo.PrivateNumbers INNER JOIN
                         dbo.SmsSenderAgents ON dbo.PrivateNumbers.SmsSenderAgentGuid = dbo.SmsSenderAgents.Guid LEFT OUTER JOIN
                         dbo.Users ON dbo.Users.Guid = dbo.PrivateNumbers.UserGuid

GO
/****** Object:  View [dbo].[vwPrivateNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwPrivateNumbers]
  AS

SELECT        Guid, Number, ServiceID, ServicePrice, CreateDate, ExpireDate, Type, Priority, ReturnBlackList, SendToBlackList, CheckFilter, DeliveryBase, HasSLA, TryCount, Range, Regex, UseForm, ParentGuid, OwnerGuid, 
                         IsRoot, IsActive, IsDefault, IsPublic, SendCount, RecieveCount, SuccessCount, IsDeleted, SmsSenderAgentGuid, SmsTrafficRelayGuid, DeliveryTrafficRelayGuid, UserGuid, MTNServiceId, AggServiceId, Price, 
                         ID
FROM            dbo.PrivateNumbers

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Priority] [tinyint] NULL,
	[CreateDate] [datetime] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
	[IsDefault] [bit] NULL,
	[IsSalePackage] [bit] NULL,
	[Price] [decimal](18, 2) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwRoles]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwRoles]
  AS

SELECT        Guid, ID, Title, CreateDate, UserGuid, IsDeleted, IsDefault, Description, Price, IsSalePackage, Priority
FROM            dbo.Roles

GO
/****** Object:  Table [dbo].[ServiceGroups]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceGroups](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[TitleFa] [nvarchar](max) NULL,
	[IconAddress] [nvarchar](max) NULL,
	[LargeIcon] [nvarchar](max) NULL,
	[Order] [int] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_ServiceGroups] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwServiceGroups]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwServiceGroups]
  AS

SELECT     Guid, Title, [Order], CreateDate, IsDeleted, IconAddress, LargeIcon, ParentGuid
FROM         dbo.ServiceGroups


GO
/****** Object:  Table [dbo].[Services]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[TitleFa] [nvarchar](50) NULL,
	[IconAddress] [nvarchar](max) NULL,
	[LargeIcon] [nvarchar](max) NULL,
	[Presentable] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[ReferencePageKey] [int] NULL,
	[ReferenceServiceKey] [int] NULL,
	[Order] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ServiceGroupGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwServices]
  AS

SELECT     Guid, Title, IconAddress, Presentable, IsDeleted, ReferencePageKey, ReferenceServiceKey, CreateDate, ServiceGroupGuid, [Order], LargeIcon
FROM         dbo.Services


GO
/****** Object:  Table [dbo].[SmsParsers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsParsers](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[CreateDate] [datetime] NULL,
	[FromDateTime] [datetime] NULL,
	[ToDateTime] [datetime] NULL,
	[TypeConditionSender] [int] NULL,
	[ConditionSender] [nvarchar](50) NULL,
	[ReplyPrivateNumberGuid] [uniqueidentifier] NULL,
	[ReplySmsText] [nvarchar](max) NULL,
	[DuplicatePrivateNumberGuid] [uniqueidentifier] NULL,
	[DuplicateUserSmsText] [nvarchar](max) NULL,
	[Scope] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SmsParsers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwSmsParsers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwSmsParsers]
  AS

SELECT        dbo.SmsParsers.*
FROM            dbo.SmsParsers

GO
/****** Object:  Table [dbo].[SmsTemplates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsTemplates](
	[Guid] [uniqueidentifier] NOT NULL,
	[Body] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SmsTemplates] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwSmsTemplates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwSmsTemplates]
  AS

SELECT        IsDeleted, CreateDate, Body, Guid, UserGuid
FROM            dbo.SmsTemplates



GO
/****** Object:  Table [dbo].[Fishes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fishes](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[ReferenceID] [nvarchar](64) NULL,
	[CreateDate] [datetime] NULL,
	[PaymentDate] [datetime] NULL,
	[SmsCount] [bigint] NULL,
	[Amount] [decimal](18, 2) NULL,
	[OrderID] [nvarchar](64) NULL,
	[BillNumber] [nvarchar](64) NULL,
	[Description] [nvarchar](max) NULL,
	[Type] [int] NULL,
	[Status] [int] NULL,
	[ReferenceGuid] [uniqueidentifier] NULL,
	[AccountInformationGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Fishes] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwUserFishes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwUserFishes]
  AS

SELECT        dbo.AccountInformations.Owner, dbo.AccountInformations.AccountNo, dbo.Fishes.Guid, dbo.Fishes.CreateDate, dbo.Fishes.PaymentDate, dbo.Fishes.Amount, dbo.Fishes.Description, dbo.Fishes.Type, 
                         dbo.Fishes.Status, dbo.Users.UserName, dbo.Fishes.BillNumber, dbo.Fishes.UserGuid, dbo.Fishes.AccountInformationGuid, dbo.Users.ParentGuid, dbo.AccountInformations.CardNo, 
                         dbo.AccountInformations.Bank, dbo.Fishes.SmsCount, dbo.Users.IsDeleted AS UserIsDeleted, dbo.AccountInformations.IsDeleted AS AccountIsDeleted
FROM            dbo.AccountInformations INNER JOIN
                         dbo.Fishes ON dbo.AccountInformations.Guid = dbo.Fishes.AccountInformationGuid INNER JOIN
                         dbo.Users ON dbo.Users.Guid = dbo.Fishes.UserGuid

GO
/****** Object:  View [dbo].[vwUsers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwUsers]
  AS

SELECT        Guid, UserName, Password, SecondPassword, FirstName, LastName, FatherName, NationalCode, ZipCode, ShCode, Email, Phone, Mobile, FaxNumber, Address, ZoneGuid, BirthDate, CreateDate, ExpireDate, 
                         Credit, CompanyName, CompanyNationalId, EconomicCode, CompanyAddress, CompanyPhone, CompanyZipCode, CompanyCEOName, CompanyCEONationalCode, CompanyCEOMobile, PanelPrice, Type, 
                         IsActive, IsAuthenticated, IsActiveSend, IsAdmin, IsMainAdmin, MaximumAdmin, MaximumUser, MaximumPhoneNumber, MaximumEmailAddress, ParentGuid, DomainGroupPriceGuid, PriceGroupGuid, 
                         IsFixPriceGroup, DomainGuid, RoleGuid, IsDeleted, IsSuperAdmin, ID
FROM            dbo.Users

GO
/****** Object:  Table [dbo].[ScheduledSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduledSmses](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(264500,1) NOT NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
	[CheckId] [nvarchar](max) NULL,
	[SmsText] [nvarchar](max) NULL,
	[PresentType] [int] NULL,
	[Encoding] [int] NULL,
	[SmsLen] [int] NULL,
	[FilePath] [nvarchar](128) NULL,
	[SmsPattern] [nvarchar](1024) NULL,
	[DownRange] [nvarchar](50) NULL,
	[UpRange] [nvarchar](50) NULL,
	[Period] [int] NULL,
	[PeriodType] [int] NULL,
	[ExtractPageNo] [int] NULL,
	[SendPageNo] [int] NULL,
	[SendPageSize] [int] NULL,
	[RequestXML] [xml] NULL,
	[TypeSend] [int] NULL,
	[Status] [int] NULL,
	[DateTimeFuture] [datetime] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[ReferenceGuid] [nvarchar](max) NULL,
	[SmsSenderAgentReference] [int] NULL,
	[SmsSendFaildType] [int] NULL,
	[SmsSendError] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[VoiceURL] [nvarchar](500) NULL,
	[voiceMessageId] [int] NULL,
 CONSTRAINT [PK_ScheduledSmses] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwScheduledSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwScheduledSmses]
  AS

SELECT        Guid, PrivateNumberGuid, CheckId, SmsText, PresentType, Encoding, SmsLen, DownRange, UpRange, Period, PeriodType, ExtractPageNo, SendPageNo, SendPageSize, RequestXML, TypeSend, 
                         DateTimeFuture, StartDateTime, EndDateTime, CreateDate, ReferenceGuid, SmsSenderAgentReference, SmsSendFaildType, Status, SmsSendError, IsDeleted, UserGuid, FilePath, SmsPattern, ID
FROM            dbo.ScheduledSmses

GO
/****** Object:  Table [dbo].[PhoneBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneBooks](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1000,1) NOT NULL,
	[Type] [tinyint] NULL,
	[Name] [nvarchar](64) NULL,
	[MobileCount] [int] NULL,
	[EmailCount] [int] NULL,
	[RecordCount] [int] NULL,
	[ServiceId] [nvarchar](32) NULL,
	[VASRegisterKeys] [nvarchar](512) NULL,
	[VASUnsubscribeKeys] [nvarchar](512) NULL,
	[CreateDate] [datetime] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[IsPrivate] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[AlternativeUserGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[AdminGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PhoneBooks] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwPhoneBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwPhoneBooks]
  AS

SELECT        dbo.PhoneBooks.*
FROM            dbo.PhoneBooks

GO
/****** Object:  Table [dbo].[PhoneNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneNumbers](
	[Guid] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[BirthDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[Telephone] [nvarchar](50) NULL,
	[CellPhone] [nvarchar](50) NOT NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[Job] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
	[F1] [nvarchar](100) NULL,
	[F2] [nvarchar](100) NULL,
	[F3] [nvarchar](100) NULL,
	[F4] [nvarchar](100) NULL,
	[F5] [nvarchar](100) NULL,
	[F6] [nvarchar](100) NULL,
	[F7] [nvarchar](100) NULL,
	[F8] [nvarchar](100) NULL,
	[F9] [nvarchar](100) NULL,
	[F10] [nvarchar](100) NULL,
	[F11] [nvarchar](100) NULL,
	[F12] [nvarchar](100) NULL,
	[F13] [nvarchar](100) NULL,
	[F14] [nvarchar](100) NULL,
	[F15] [nvarchar](100) NULL,
	[F16] [nvarchar](100) NULL,
	[F17] [nvarchar](100) NULL,
	[F18] [nvarchar](100) NULL,
	[F19] [nvarchar](100) NULL,
	[F20] [nvarchar](100) NULL,
	[Sex] [int] NULL,
	[Operator] [int] NULL,
	[IsDeleted] [bit] NULL,
	[PhoneBookGuid] [uniqueidentifier] NULL,
	[NationalCode] [nchar](10) NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ZipCode] [nchar](10) NULL,
 CONSTRAINT [PK_PhoneNumbers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwPhoneNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwPhoneNumbers]
  AS

	SELECT *
	FROM [dbo].[PhoneNumbers]

GO
/****** Object:  Table [dbo].[SmsFormats]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsFormats](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Format] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[PhoneBookGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_SmsFormats] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwSmsFormats]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwSmsFormats]
  AS

SELECT 
	*
FROM
	[dbo].[SmsFormats]
GO
/****** Object:  Table [dbo].[RegularContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegularContents](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](128) NULL,
	[Type] [tinyint] NULL,
	[Config] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[PeriodType] [tinyint] NULL,
	[Period] [int] NULL,
	[EffectiveDateTime] [datetime] NULL,
	[WarningType] [tinyint] NULL,
	[CreateDate] [datetime] NULL,
	[StartDateTime] [datetime] NULL,
	[EndDateTime] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RegularContents] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwRegularContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwRegularContents]
  AS

SELECT        dbo.RegularContents.*
FROM            dbo.RegularContents

GO
/****** Object:  Table [dbo].[EmailTemplates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplates](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[AttachmentList] [nvarchar](max) NULL,
	[IsDeleted] [bit] NULL,
	[UserEmailSettingGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EmailTemplates] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[vwEmailTemplates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vwEmailTemplates]
  AS

SELECT     dbo.EmailTemplates.*
FROM         dbo.EmailTemplates


GO
/****** Object:  View [dbo].[vw_ScheduledSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ScheduledSmses]
  AS

SELECT        dbo.ScheduledSmses.*
FROM            dbo.ScheduledSmses



GO
/****** Object:  UserDefinedFunction [dbo].[Split]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[Split]
(
    @String NVARCHAR(4000),
    @Delimiter NCHAR(1)
)
RETURNS TABLE 
  AS

RETURN 
(
    WITH Split(stpos,endpos) 
    AS(
        SELECT 0 AS stpos, CHARINDEX(@Delimiter,@String) AS endpos
        UNION ALL
        SELECT endpos+1, CHARINDEX(@Delimiter,@String,endpos+1)
            FROM Split
            WHERE endpos > 0
    )
    SELECT 'Id' = ROW_NUMBER() OVER (ORDER BY (SELECT 1)),
        'Data' = SUBSTRING(@String,stpos,COALESCE(NULLIF(endpos,0),LEN(@String)+1)-stpos)
    FROM Split
)



GO
/****** Object:  UserDefinedFunction [dbo].[SplitString]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE function [dbo].[SplitString] 
(
    @str nvarchar(max), 
    @separator char(1)
)
returns table
  AS

return (
with tokens(p, a, b) AS (
    select 
        cast(1 as bigint), 
        cast(1 as bigint), 
        charindex(@separator, @str)
    union all
    select
        p + 1, 
        b + 1, 
        charindex(@separator, @str, b + 1)
    from tokens
    where b > 0
)
select
    p-1 ItemIndex,
    substring(
        @str, 
        a, 
        case when b > 0 then b-a ELSE LEN(@str) end) 
    AS Item
from tokens
);




GO
/****** Object:  Table [dbo].[AgentParameters]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgentParameters](
	[Guid] [uniqueidentifier] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Key] [nvarchar](255) NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
	[SmsSenderAgentGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AgentParameters] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AgentRatio]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AgentRatio](
	[Guid] [uniqueidentifier] NOT NULL,
	[SmsType] [tinyint] NOT NULL,
	[Ratio] [decimal](18, 2) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[OperatorID] [tinyint] NOT NULL,
	[SmsSenderAgentGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_AgentRatio] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchivedOutboxNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchivedOutboxNumbers](
	[Guid] [uniqueidentifier] NOT NULL,
	[BatchId] [bigint] NULL,
	[ItemId] [nvarchar](64) NULL,
	[ToNumber] [nvarchar](16) NOT NULL,
	[DeliveryStatus] [smallint] NULL,
	[StatusDateTime] [datetime] NULL,
	[ReturnId] [nvarchar](255) NULL,
	[CheckId] [nvarchar](255) NULL,
	[SendStatus] [smallint] NULL,
	[SendDeliveryStatus] [bit] NULL,
	[SendDeliveryToUrlCount] [smallint] NULL,
	[HasSign] [bit] NULL,
	[Operator] [tinyint] NULL,
	[SmsSenderAgentReference] [int] NULL,
	[OutboxGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK__ArchivedOutboxNumbers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlackList]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlackList](
	[Guid] [uniqueidentifier] NOT NULL,
	[Number] [nvarchar](255) NULL,
 CONSTRAINT [PK__BlackList] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BulkRecipient]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BulkRecipient](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[SendPageNo] [int] NULL,
	[Status] [int] NULL,
	[Prefix] [nvarchar](16) NULL,
	[ZipCode] [nvarchar](16) NULL,
	[Type] [int] NULL,
	[Operator] [int] NULL,
	[FromIndex] [int] NULL,
	[Count] [int] NULL,
	[ScopeCount] [int] NULL,
	[ZoneGuid] [uniqueidentifier] NULL,
	[ScheduledBulkSmsGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_BulkRecipient] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CellPhones]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CellPhones](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] NULL,
	[Number] [nchar](11) NULL,
	[Type] [int] NULL,
	[CityGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CellPhones] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cities]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[ProvinceID] [int] NULL,
	[ProvinceGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contents](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Text] [nvarchar](512) NOT NULL,
	[SmsLen] [tinyint] NULL,
	[IsUnicode] [bit] NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[RegularContentGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Contents] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Countries]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Countries](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] NULL,
	[Code] [char](2) NULL,
	[Name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DesktopMenuLocations]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DesktopMenuLocations](
	[Guid] [uniqueidentifier] NOT NULL,
	[Location] [int] NULL,
	[Desktop] [int] NULL,
	[DomainMenuGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_DesktopMenuLocation] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DomainAccounts]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainAccounts](
	[Guid] [uniqueidentifier] NOT NULL,
	[Type] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[NationalCode] [nvarchar](50) NULL,
	[CompanyName] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[City] [nvarchar](50) NULL,
	[Province] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[PostalCode] [char](10) NULL,
	[Telephone] [nvarchar](50) NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[NICType] [int] NULL,
	[CivilType] [int] NULL,
	[CountryOfCompany] [nvarchar](50) NULL,
	[ProvinceOfCompany] [nvarchar](50) NULL,
	[CityOfCompany] [nvarchar](50) NULL,
	[RegisteredCompanyName] [nvarchar](50) NULL,
	[CompanyID] [nvarchar](50) NULL,
	[CompanyType] [int] NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DomainNic] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DomainGroupPrices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainGroupPrices](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GroupPricesDomain] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DomainMenus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainMenus](
	[Guid] [uniqueidentifier] NOT NULL,
	[Type] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Link] [nvarchar](max) NULL,
	[DataCenterGuid] [uniqueidentifier] NULL,
	[StaticPageReference] [int] NULL,
	[TargetType] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Priority] [int] NULL,
	[IsActive] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[DomainGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DomainMenus] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DomainPrices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainPrices](
	[Guid] [uniqueidentifier] NOT NULL,
	[Extention] [int] NULL,
	[Period] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[DomainGroupPriceGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DomainPrice] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DomainSettings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DomainSettings](
	[Guid] [uniqueidentifier] NOT NULL,
	[Key] [int] NULL,
	[Value] [nvarchar](max) NULL,
	[DomainGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_DomainSettings] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailAddresses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailAddresses](
	[Guid] [uniqueidentifier] NOT NULL,
	[EmailAddress] [nvarchar](200) NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[EmailBookGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Email] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailBooks](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[IsPrivate] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[AdminGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EmailBook] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailOutboxes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailOutboxes](
	[Guid] [uniqueidentifier] NOT NULL,
	[UserEmailSettingGuid] [uniqueidentifier] NULL,
	[Reciever] [nvarchar](100) NULL,
	[Body] [nvarchar](max) NULL,
	[Subject] [nvarchar](max) NULL,
	[AttachmentList] [nvarchar](max) NULL,
	[DownRange] [nvarchar](50) NULL,
	[UpRange] [nvarchar](50) NULL,
	[GroupGuid] [uniqueidentifier] NULL,
	[TypeSend] [int] NULL,
	[State] [int] NULL,
	[DateTimeFuture] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[Description] [nvarchar](max) NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_EmailOutboxes] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailSentboxes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailSentboxes](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Reciever] [nvarchar](100) NULL,
	[Subject] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[SenderEmail] [nvarchar](200) NULL,
	[EffectiveDateTime] [datetime] NULL,
	[Status] [int] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[EmailOutboxGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_EmailSentboxes] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FailedOnlinePayments]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FailedOnlinePayments](
	[Guid] [uniqueidentifier] NOT NULL,
	[OrderID] [nvarchar](50) NULL,
	[ReferenceID] [nvarchar](50) NULL,
	[Bank] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_FailedOnlinePayments] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favorites]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favorites](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NULL,
	[ParentGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Favorites] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FilterWords]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FilterWords](
	[Guid] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralNumbers](
	[Guid] [uniqueidentifier] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Telephone] [nvarchar](50) NULL,
	[CellPhone] [nvarchar](50) NULL,
	[PostalCode] [char](10) NULL,
	[FaxNumber] [nvarchar](50) NULL,
	[Job] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
	[BirthDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[Type] [int] NULL,
	[Operator] [int] NULL,
	[Sex] [int] NULL,
	[IsDeleted] [bit] NULL,
	[GeneralPhoneBookGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GeneralNumbers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralPhoneBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralPhoneBooks](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[Order] [int] NULL,
	[Count] [int] NULL,
	[IsPrivate] [bit] NULL,
	[IsDeleted] [bit] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[AdminGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_GeneralPhoneBooks] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InboxGroups]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InboxGroups](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
	[ParentGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_InboxGroups] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](64) NULL,
	[ParentGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoginStats]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginStats](
	[Guid] [uniqueidentifier] NOT NULL,
	[IP] [nvarchar](50) NULL,
	[Type] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LoginStats] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Guid] [uniqueidentifier] NOT NULL,
	[Type] [int] NULL,
	[Source] [nvarchar](64) NULL,
	[Name] [nvarchar](64) NULL,
	[Text] [nvarchar](max) NULL,
	[IPAddress] [nvarchar](32) NULL,
	[Browser] [nvarchar](64) NULL,
	[CreateDate] [datetime] NULL,
	[ReferenceGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NumbersBlk]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NumbersBlk](
	[Column 0] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operators]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operators](
	[Guid] [uniqueidentifier] NULL,
	[ID] [tinyint] NOT NULL,
	[Title] [nvarchar](32) NULL,
	[Name] [nvarchar](64) NULL,
	[Regex] [nvarchar](512) NOT NULL,
 CONSTRAINT [PK_Operators] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Outboxes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Outboxes](
	[Guid] [uniqueidentifier] NOT NULL,
	[CheckId] [nvarchar](64) NULL,
	[ExportDataPageNo] [int] NULL,
	[ExportDataStatus] [tinyint] NULL,
	[SendStatus] [smallint] NULL,
	[Price] [decimal](18, 2) NULL,
	[ReceiverCount] [int] NULL,
	[SavedReceiverCount] [int] NULL,
	[DeliveredCount] [int] NULL,
	[FailedCount] [int] NULL,
	[SentToICTCount] [int] NULL,
	[DeliveredICTCount] [int] NULL,
	[BlackListCount] [int] NULL,
	[WrapperDateTime] [datetime] NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
	[SenderId] [nvarchar](255) NULL,
	[SmsPriority] [int] NULL,
	[IsUnicode] [bit] NULL,
	[SmsLen] [int] NULL,
	[SmsText] [nvarchar](max) NULL,
	[SendingTryCount] [int] NULL,
	[CreateDate] [datetime] NULL,
	[SentDateTime] [datetime] NULL,
	[DeliveryNeeded] [bit] NULL,
	[Udh] [int] NULL,
	[SenderIp] [nvarchar](255) NULL,
	[IsFlash] [bit] NULL,
	[SmsSendType] [int] NULL,
	[SmsIdentifier] [bigint] NULL,
	[SmsPartIndex] [int] NULL,
	[RequestXML] [xml] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[SmsSenderAgentReference] [int] NULL,
	[ID] [bigint] NULL,
	[ExportTxtPageNo] [int] NULL,
	[ExportTxtStatus] [int] NULL,
 CONSTRAINT [PK_Outboxes] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OutboxNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OutboxNumbers](
	[Guid] [uniqueidentifier] NOT NULL,
	[BatchId] [bigint] NULL,
	[ItemId] [nvarchar](64) NULL,
	[ToNumber] [nvarchar](255) NOT NULL,
	[DeliveryStatus] [smallint] NULL,
	[StatusDateTime] [datetime] NULL,
	[ReturnId] [nvarchar](255) NULL,
	[CheckId] [nvarchar](255) NULL,
	[SendStatus] [smallint] NULL,
	[SendDeliveryStatus] [bit] NULL,
	[SendDeliveryToUrlCount] [smallint] NULL,
	[HasSign] [bit] NULL,
	[Operator] [int] NULL,
	[SmsSenderAgentReference] [int] NULL,
	[OutboxGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK__OutboxNumbers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParserFormulas]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParserFormulas](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](512) NULL,
	[Key] [nvarchar](512) NULL,
	[IsCorrect] [bit] NULL,
	[PhoneNo] [nvarchar](64) NULL,
	[Condition] [int] NULL,
	[Priority] [int] NULL,
	[Counter] [int] NULL,
	[ReactionExtention] [nvarchar](max) NULL,
	[SmsParserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ParserFormulas] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonFavorites]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonFavorites](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] NOT NULL,
	[CreateDate] [datetime] NULL,
	[FavoriteGuid] [uniqueidentifier] NOT NULL,
	[PersonGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PersonFavorites] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonJobs]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonJobs](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] NOT NULL,
	[CreateDate] [datetime] NULL,
	[JobGuid] [uniqueidentifier] NOT NULL,
	[PersonGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PersonJobs] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] NOT NULL,
	[FirstName] [nvarchar](32) NULL,
	[LastName] [nvarchar](64) NULL,
	[FatherName] [nvarchar](32) NULL,
	[Gender] [tinyint] NULL,
	[BirthDate] [datetime] NULL,
	[NationalCode] [char](10) NULL,
	[ShCode] [nvarchar](16) NULL,
	[CreateDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PersonsInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonsInfo](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] NULL,
	[Mobile] [nvarchar](16) NULL,
	[MobileType] [tinyint] NULL,
	[MobileOperator] [tinyint] NULL,
	[ZoneGuid] [uniqueidentifier] NOT NULL,
	[ZipCode] [char](10) NULL,
	[IsBlackList] [bit] NULL,
	[Gender] [tinyint] NULL,
 CONSTRAINT [PK_PersonsInfo] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhoneBookRegularContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneBookRegularContents](
	[Guid] [uniqueidentifier] NOT NULL,
	[CreateDate] [datetime] NULL,
	[PhoneBookGuid] [uniqueidentifier] NULL,
	[RegularContentGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PhoneBookRegularContents] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostalCodes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostalCodes](
	[Guid] [uniqueidentifier] NOT NULL,
	[CellPhone] [nvarchar](15) NULL,
	[PostalCode] [nvarchar](10) NULL,
 CONSTRAINT [PK_PostalCodes] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PriceRanges]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceRanges](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] NULL,
	[Ratio] [decimal](18, 2) NULL,
	[CreateDate] [datetime] NULL,
	[GroupPriceGuid] [uniqueidentifier] NULL,
	[SmsSenderAgentGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_PriceRanges] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Provinces]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Provinces](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[CountryID] [int] NULL,
	[CountryGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Provinces] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ReceiveKeywords]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ReceiveKeywords](
	[Guid] [uniqueidentifier] NOT NULL,
	[Keyword] [nvarchar](32) NOT NULL,
	[CreateDate] [datetime] NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[PrivateNumberGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ReceiveKeywords] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipients]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipients](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Mobile] [nvarchar](50) NOT NULL,
	[Operator] [int] NULL,
	[IsBlackList] [bit] NULL,
	[ScheduledSmsGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Recipients] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisteredDomains]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisteredDomains](
	[Guid] [uniqueidentifier] NOT NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[DomainName] [nvarchar](50) NULL,
	[DomainExtention] [int] NULL,
	[Type] [int] NULL,
	[DNS1] [nvarchar](50) NULL,
	[DNS2] [nvarchar](50) NULL,
	[DNS3] [nvarchar](50) NULL,
	[DNS4] [nvarchar](50) NULL,
	[IP1] [nvarchar](50) NULL,
	[IP2] [nvarchar](50) NULL,
	[IP3] [nvarchar](50) NULL,
	[IP4] [nvarchar](50) NULL,
	[Period] [int] NULL,
	[CreateDate] [datetime] NULL,
	[ExpireDate] [datetime] NULL,
	[IsPayment] [bit] NULL,
	[Status] [int] NULL,
	[CustomerID] [nvarchar](50) NULL,
	[OfficeRelation] [nvarchar](50) NULL,
	[TechnicalRelation] [nvarchar](50) NULL,
	[FinancialRelation] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
 CONSTRAINT [PK_RegisteredDomain] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleGeneralPhoneBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleGeneralPhoneBooks](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[RoleGuid] [uniqueidentifier] NULL,
	[GeneralPhoneBookGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_RoleGeneralPhoneBooks] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleServices](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[ServiceGuid] [uniqueidentifier] NULL,
	[RoleGuid] [uniqueidentifier] NULL,
	[IsDefault] [bit] NULL,
 CONSTRAINT [PK_RoleServices] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Routes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Routes](
	[Guid] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](128) NULL,
	[Username] [nvarchar](32) NOT NULL,
	[Password] [nvarchar](32) NOT NULL,
	[Domain] [nvarchar](32) NULL,
	[Link] [nvarchar](512) NULL,
	[QueueLength] [int] NULL,
	[SmsSenderAgentGuid] [uniqueidentifier] NOT NULL,
	[OperatorID] [tinyint] NOT NULL,
 CONSTRAINT [PK_Route] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduledBulkSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduledBulkSmses](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] NOT NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
	[SmsText] [nvarchar](max) NULL,
	[PresentType] [int] NULL,
	[Encoding] [int] NULL,
	[SmsLen] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[ReceiverCount] [int] NULL,
	[SendPageNo] [int] NULL,
	[RequestXML] [xml] NOT NULL,
	[CreateDate] [datetime] NULL,
	[SendDateTime] [datetime] NULL,
	[SmsSenderAgentReference] [int] NULL,
	[Status] [int] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ScheduledBulkSmses] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Settings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Settings](
	[Guid] [uniqueidentifier] NOT NULL,
	[Key] [int] NULL,
	[Value] [nvarchar](max) NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Settings] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Smses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Smses](
	[Guid] [uniqueidentifier] NOT NULL,
	[Reciever] [nvarchar](50) NULL,
	[SmsBody] [nvarchar](max) NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[Encoding] [int] NULL,
	[PresentType] [int] NULL,
	[Status] [int] NULL,
	[DeliveryStatus] [int] NULL,
	[SmsCount] [int] NULL,
	[TypeSend] [int] NULL,
	[Operator] [int] NULL,
	[SmsSenderAgentReference] [int] NULL,
	[EffectiveDateTime] [datetime] NULL,
	[SmsSentGuid] [uniqueidentifier] NULL,
	[OuterSystemSmsID] [nvarchar](max) NULL,
	[CheckingMessageID] [nvarchar](max) NULL,
	[ErrorSendMessage] [nvarchar](max) NULL,
 CONSTRAINT [PK_Smses] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsFormatPhoneBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsFormatPhoneBooks](
	[PhoneBookGuid] [uniqueidentifier] NULL,
	[SmsFormatGuid] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SmsRates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SmsRates](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Farsi] [decimal](18, 0) NULL,
	[Latin] [decimal](18, 0) NULL,
	[Operator] [int] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[SmsSenderAgentGuid] [uniqueidentifier] NULL,
	[IsDeleted] [bit] NULL,
 CONSTRAINT [PK_SmsRates] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Guid] [uniqueidentifier] NOT NULL,
	[ReferenceGuid] [uniqueidentifier] NULL,
	[TypeTransaction] [int] NULL,
	[TypeCreditChange] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[CreateDate] [datetime] NULL,
	[CurrentCredit] [decimal](18, 2) NULL,
	[Amount] [decimal](18, 2) NULL,
	[Benefit] [decimal](18, 2) NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserAccesses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserAccesses](
	[UserGuid] [uniqueidentifier] NULL,
	[AccessGuid] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserDocuments]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserDocuments](
	[Guid] [uniqueidentifier] NOT NULL,
	[Type] [int] NULL,
	[Key] [int] NULL,
	[Value] [nvarchar](255) NULL,
	[Status] [int] NULL,
	[Description] [nvarchar](512) NULL,
	[CreateDate] [datetime] NULL,
	[UserGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserDocuments] PRIMARY KEY CLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserFields]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFields](
	[Guid] [uniqueidentifier] NOT NULL,
	[Field1] [nvarchar](100) NULL,
	[Field2] [nvarchar](100) NULL,
	[Field3] [nvarchar](100) NULL,
	[Field4] [nvarchar](100) NULL,
	[Field5] [nvarchar](100) NULL,
	[Field6] [nvarchar](100) NULL,
	[Field7] [nvarchar](100) NULL,
	[Field8] [nvarchar](100) NULL,
	[Field9] [nvarchar](100) NULL,
	[Field10] [nvarchar](100) NULL,
	[Field11] [nvarchar](100) NULL,
	[Field12] [nvarchar](100) NULL,
	[Field13] [nvarchar](100) NULL,
	[Field14] [nvarchar](100) NULL,
	[Field15] [nvarchar](100) NULL,
	[Field16] [nvarchar](100) NULL,
	[Field17] [nvarchar](100) NULL,
	[Field18] [nvarchar](100) NULL,
	[Field19] [nvarchar](100) NULL,
	[Field20] [nvarchar](100) NULL,
	[FieldType1] [int] NULL,
	[FieldType2] [int] NULL,
	[FieldType3] [int] NULL,
	[FieldType4] [int] NULL,
	[FieldType5] [int] NULL,
	[FieldType6] [int] NULL,
	[FieldType7] [int] NULL,
	[FieldType8] [int] NULL,
	[FieldType9] [int] NULL,
	[FieldType10] [int] NULL,
	[FieldType11] [int] NULL,
	[FieldType12] [int] NULL,
	[FieldType13] [int] NULL,
	[FieldType14] [int] NULL,
	[FieldType15] [int] NULL,
	[FieldType16] [int] NULL,
	[FieldType17] [int] NULL,
	[FieldType18] [int] NULL,
	[FieldType19] [int] NULL,
	[FieldType20] [int] NULL,
	[PhoneBookGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserField] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserGeneralPhoneBooks]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserGeneralPhoneBooks](
	[UserGuid] [uniqueidentifier] NULL,
	[GeneralPhoneBookGuid] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserPrivateNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserPrivateNumbers](
	[Guid] [uniqueidentifier] NOT NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreateDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[UseForChildren] [bit] NULL,
	[DecreaseFromPanel] [bit] NULL,
	[IsDelete] [bit] NULL,
	[Type] [int] NULL,
	[UseType] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[ActivationUserGuid] [uniqueidentifier] NULL,
	[UserPrivateNumberParentGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL,
	[PrivateNumberGuid] [uniqueidentifier] NULL,
 CONSTRAINT [PK_UserPrivateNumbers] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserServices](
	[ServiceGuid] [uniqueidentifier] NULL,
	[UserGuid] [uniqueidentifier] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSettings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSettings](
	[Guid] [uniqueidentifier] NOT NULL,
	[UserGuid] [uniqueidentifier] NOT NULL,
	[Key] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Status] [tinyint] NULL,
 CONSTRAINT [PK_UserSettings] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Zones]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Zones](
	[Guid] [uniqueidentifier] NOT NULL,
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Code] [char](2) NULL,
	[CreateDate] [datetime] NULL,
	[ParentGuid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Zones] PRIMARY KEY NONCLUSTERED 
(
	[Guid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_ExportDataStatus]  DEFAULT ((1)) FOR [ExportDataStatus]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_SavedReceiverCount]  DEFAULT ((0)) FOR [SavedReceiverCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_DeliveredCount]  DEFAULT ((0)) FOR [DeliveredCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_FailedCount]  DEFAULT ((0)) FOR [FailedCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_SentToICTCount]  DEFAULT ((0)) FOR [SentToICTCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_DeliveredICTCount]  DEFAULT ((0)) FOR [DeliveredICTCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF_Outboxes_BlackListCount]  DEFAULT ((0)) FOR [BlackListCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF__GW_Outbox__Sendi__2354350C]  DEFAULT ((0)) FOR [SendingTryCount]
GO
ALTER TABLE [dbo].[Outboxes] ADD  CONSTRAINT [DF__GW_Outbox__SentD__24485945]  DEFAULT (getdate()) FOR [SentDateTime]
GO
ALTER TABLE [dbo].[Outboxes] ADD  DEFAULT ((1)) FOR [ExportTxtStatus]
GO
ALTER TABLE [dbo].[PhoneBooks] ADD  CONSTRAINT [DF_PhoneBooks_Type]  DEFAULT ((1)) FOR [Type]
GO
ALTER TABLE [dbo].[AgentParameters]  WITH CHECK ADD  CONSTRAINT [FK_AgentParameters_SmsSenderAgents] FOREIGN KEY([SmsSenderAgentGuid])
REFERENCES [dbo].[SmsSenderAgents] ([Guid])
GO
ALTER TABLE [dbo].[AgentParameters] CHECK CONSTRAINT [FK_AgentParameters_SmsSenderAgents]
GO
ALTER TABLE [dbo].[AgentRatio]  WITH CHECK ADD  CONSTRAINT [FK_AgentRatio_Operators] FOREIGN KEY([OperatorID])
REFERENCES [dbo].[Operators] ([ID])
GO
ALTER TABLE [dbo].[AgentRatio] CHECK CONSTRAINT [FK_AgentRatio_Operators]
GO
ALTER TABLE [dbo].[AgentRatio]  WITH CHECK ADD  CONSTRAINT [FK_AgentRatio_SmsSenderAgents] FOREIGN KEY([SmsSenderAgentGuid])
REFERENCES [dbo].[SmsSenderAgents] ([Guid])
GO
ALTER TABLE [dbo].[AgentRatio] CHECK CONSTRAINT [FK_AgentRatio_SmsSenderAgents]
GO
ALTER TABLE [dbo].[BulkRecipient]  WITH CHECK ADD  CONSTRAINT [FK_BulkRecipient_ScheduledBulkSmses] FOREIGN KEY([ScheduledBulkSmsGuid])
REFERENCES [dbo].[ScheduledBulkSmses] ([Guid])
GO
ALTER TABLE [dbo].[BulkRecipient] CHECK CONSTRAINT [FK_BulkRecipient_ScheduledBulkSmses]
GO
ALTER TABLE [dbo].[Contents]  WITH CHECK ADD  CONSTRAINT [FK_Contents_RegularContents] FOREIGN KEY([RegularContentGuid])
REFERENCES [dbo].[RegularContents] ([Guid])
GO
ALTER TABLE [dbo].[Contents] CHECK CONSTRAINT [FK_Contents_RegularContents]
GO
ALTER TABLE [dbo].[PersonFavorites]  WITH CHECK ADD  CONSTRAINT [FK_PersonFavorites_Favorites] FOREIGN KEY([FavoriteGuid])
REFERENCES [dbo].[Favorites] ([Guid])
GO
ALTER TABLE [dbo].[PersonFavorites] CHECK CONSTRAINT [FK_PersonFavorites_Favorites]
GO
ALTER TABLE [dbo].[PersonFavorites]  WITH CHECK ADD  CONSTRAINT [FK_PersonFavorites_Persons] FOREIGN KEY([PersonGuid])
REFERENCES [dbo].[Persons] ([Guid])
GO
ALTER TABLE [dbo].[PersonFavorites] CHECK CONSTRAINT [FK_PersonFavorites_Persons]
GO
ALTER TABLE [dbo].[PersonJobs]  WITH CHECK ADD  CONSTRAINT [FK_PersonJobs_Jobs] FOREIGN KEY([JobGuid])
REFERENCES [dbo].[Jobs] ([Guid])
GO
ALTER TABLE [dbo].[PersonJobs] CHECK CONSTRAINT [FK_PersonJobs_Jobs]
GO
ALTER TABLE [dbo].[PersonJobs]  WITH CHECK ADD  CONSTRAINT [FK_PersonJobs_Persons] FOREIGN KEY([PersonGuid])
REFERENCES [dbo].[Persons] ([Guid])
GO
ALTER TABLE [dbo].[PersonJobs] CHECK CONSTRAINT [FK_PersonJobs_Persons]
GO
ALTER TABLE [dbo].[PhoneBookRegularContents]  WITH CHECK ADD  CONSTRAINT [FK_PhoneBookRegularContents_PhoneBooks] FOREIGN KEY([PhoneBookGuid])
REFERENCES [dbo].[PhoneBooks] ([Guid])
GO
ALTER TABLE [dbo].[PhoneBookRegularContents] CHECK CONSTRAINT [FK_PhoneBookRegularContents_PhoneBooks]
GO
ALTER TABLE [dbo].[PhoneBookRegularContents]  WITH CHECK ADD  CONSTRAINT [FK_PhoneBookRegularContents_RegularContents] FOREIGN KEY([RegularContentGuid])
REFERENCES [dbo].[RegularContents] ([Guid])
GO
ALTER TABLE [dbo].[PhoneBookRegularContents] CHECK CONSTRAINT [FK_PhoneBookRegularContents_RegularContents]
GO
ALTER TABLE [dbo].[ReceiveKeywords]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveKeywords_PrivateNumbers] FOREIGN KEY([PrivateNumberGuid])
REFERENCES [dbo].[PrivateNumbers] ([Guid])
GO
ALTER TABLE [dbo].[ReceiveKeywords] CHECK CONSTRAINT [FK_ReceiveKeywords_PrivateNumbers]
GO
ALTER TABLE [dbo].[ReceiveKeywords]  WITH CHECK ADD  CONSTRAINT [FK_ReceiveKeywords_Users] FOREIGN KEY([UserGuid])
REFERENCES [dbo].[Users] ([Guid])
GO
ALTER TABLE [dbo].[ReceiveKeywords] CHECK CONSTRAINT [FK_ReceiveKeywords_Users]
GO
ALTER TABLE [dbo].[RegularContents]  WITH CHECK ADD  CONSTRAINT [FK_RegularContents_Users] FOREIGN KEY([UserGuid])
REFERENCES [dbo].[Users] ([Guid])
GO
ALTER TABLE [dbo].[RegularContents] CHECK CONSTRAINT [FK_RegularContents_Users]
GO
ALTER TABLE [dbo].[Routes]  WITH CHECK ADD  CONSTRAINT [FK_Route_Operators] FOREIGN KEY([OperatorID])
REFERENCES [dbo].[Operators] ([ID])
GO
ALTER TABLE [dbo].[Routes] CHECK CONSTRAINT [FK_Route_Operators]
GO
ALTER TABLE [dbo].[Routes]  WITH CHECK ADD  CONSTRAINT [FK_Route_SmsSenderAgents] FOREIGN KEY([SmsSenderAgentGuid])
REFERENCES [dbo].[SmsSenderAgents] ([Guid])
GO
ALTER TABLE [dbo].[Routes] CHECK CONSTRAINT [FK_Route_SmsSenderAgents]
GO
ALTER TABLE [dbo].[ScheduledSmses]  WITH CHECK ADD  CONSTRAINT [FK_ScheduledSmses_Users] FOREIGN KEY([UserGuid])
REFERENCES [dbo].[Users] ([Guid])
GO
ALTER TABLE [dbo].[ScheduledSmses] CHECK CONSTRAINT [FK_ScheduledSmses_Users]
GO
/****** Object:  StoredProcedure [dbo].[Accesses_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Accesses_Delete]
    @Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [Accesses]
    SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Accesses_GetPagedAccess]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Accesses_GetPagedAccess]
    @ServiceGuid UNIQUEIDENTIFIER,
    @PageNo INT ,
    @PageSize INT ,
    @SortField NVARCHAR(256)
  AS

	 DECLARE @Where NVARCHAR(MAX) = '[Accesses].[IsDeleted] = 0 AND [Services].[IsDeleted]=0'
	 
	 IF ( @ServiceGuid != '00000000-0000-0000-0000-000000000000' )
		BEGIN
					IF ( @Where != '' ) 
							SET @Where += ' AND'
					SET @Where += ' [Accesses].[ServiceGuid]=''' + CAST(@ServiceGuid AS VARCHAR(36)) + ''''
		END
		
  IF ( @Where != '' ) 
		SET @Where = ' WHERE ' + @Where
----------------------------------------------------
    EXEC(' SELECT	DISTINCT
									[Accesses].[Guid],
									[Accesses].[CreateDate],
									[Services].[Title] AS [ServiceName] ,
									[Services].[Guid] AS [ServiceGuid] ,
									[Accesses].[ReferencePermissionsKey] ,
									CASE WHEN [UserAccesses].[UserGuid] IS NULL THEN 0
											 ELSE 1
									END AS [IsUsed]
									INTO
										#TempAccess
						FROM    [Services] INNER JOIN [Accesses] 
										ON [Services].[Guid] = [Accesses].[ServiceGuid]	LEFT JOIN [UserAccesses]
										ON [Accesses].[Guid] = [UserAccesses].[AccessGuid]'+@Where+'
						
						SELECT COUNT(*) AS [RowCount] FROM #TempAccess;
						
						WITH expTemp AS
						(
							SELECT
									Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
									*
							FROM
									#TempAccess
						)
						SELECT 
								*
						FROM
							expTemp
						WHERE 
							(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
							([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
						ORDER BY
							 [RowNumber] ;
				     
							
						DROP TABLE #TempAccess')


GO
/****** Object:  StoredProcedure [dbo].[Accesses_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Accesses_Insert]
    @Guid UNIQUEIDENTIFIER ,
    @ReferencePermissionsKey INT ,
    @CreateDate DATETIME,
    @ServiceGuid UNIQUEIDENTIFIER
  AS
 
    INSERT INTO [Accesses]
           ([Guid]
           ,[ReferencePermissionsKey]
           ,[IsDeleted]
           ,[CreateDate]
           ,[ServiceGuid])
     VALUES
           (@Guid,
            @ReferencePermissionsKey,
            0,
            @CreateDate,
            @ServiceGuid)


GO
/****** Object:  StoredProcedure [dbo].[Accesses_UpdateAccess]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Accesses_UpdateAccess]
    @Guid UNIQUEIDENTIFIER ,
    @ReferencePermissionsKey INT ,
    @ServiceGuid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [Accesses]
    SET     [ReferencePermissionsKey] = @ReferencePermissionsKey ,
            [ServiceGuid] = @ServiceGuid
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[AccountInformations_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[AccountInformations_Delete]
	@Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE	[AccountInformations] SET [IsDeleted]=1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[AccountInformations_GetAccountOfReferenceID]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AccountInformations_GetAccountOfReferenceID]
	@ReferenceID NVARCHAR(64)
  AS

SELECT 
	accountInfo.[PinCode],
	accountInfo.[Guid] AS [AccountInformationGuid],
	accountInfo.[TerminalID] ,
	accountInfo.[UserName] ,
	accountInfo.[Password] ,
	accountInfo.[Bank] ,
	accountInfo.[IsActive] ,
	accountInfo.[OnlineGatewayIsActive] ,
	accountInfo.[IsDeleted] ,
	fish.[Guid] AS [FishGuid] ,
	fish.[Amount] ,
	fish.[OrderID] ,
	fish.[BillNumber] ,
	fish.[Status] ,
	fish.[UserGuid],
	fish.[SmsCount],
	fish.[ReferenceGuid]
FROM		
	[dbo].[Fishes] fish INNER JOIN
	[dbo].[AccountInformations] accountInfo ON accountInfo.[Guid] = fish.[AccountInformationGuid]
WHERE		
	accountInfo.IsDeleted = 0 AND
	fish.[ReferenceID] = @ReferenceID


GO
/****** Object:  StoredProcedure [dbo].[AccountInformations_GetAccountsIsActiveOnline]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AccountInformations_GetAccountsIsActiveOnline]
	@UserGuid UNIQUEIDENTIFIER
  AS

	SELECT
				*
	FROM
				[dbo].[AccountInformations]
	WHERE
				[IsActive] = 1 AND
				[IsDeleted] = 0 AND
				[OnlineGatewayIsActive] = 1 AND
				[UserGuid] = @UserGuid;
	

GO
/****** Object:  StoredProcedure [dbo].[AccountInformations_GetPagedAccountInformations]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AccountInformations_GetPagedAccountInformations]
    @UserGuid UNIQUEIDENTIFIER ,
    @PageNo INT ,
    @PageSize INT ,
    @SortField NVARCHAR(256)
  AS
 
    DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0'

    IF ( @UserGuid != '00000000-0000-0000-0000-000000000000' )
			BEGIN
            IF ( @Where != '' ) 
                SET @Where += ' AND'
						SET @Where += ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
			END

    IF (@Where != '' ) 
        SET @Where = ' WHERE ' + @Where
	
--------------------------------------------------
    EXEC('SELECT COUNT(*) AS [RowCount] FROM [AccountInformations]' +	@Where + ';
	
		WITH expTemp AS
		(
			SELECT
					Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
					*
			FROM
					[AccountInformations]' +	@Where + '
		)
		SELECT 
				*
		FROM
			expTemp
		WHERE 
			(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
			([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
		ORDER BY
			 [RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[AccountInformations_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AccountInformations_Insert]
    @Guid UNIQUEIDENTIFIER ,
    @Owner NVARCHAR(50) ,
    @Branch NVARCHAR(50) ,
    @AccountNo NVARCHAR(100) ,
    @TerminalID NVARCHAR(50),
    @UserName NVARCHAR(50),
    @Password NVARCHAR(50),
    @PinCode NVARCHAR(50),
    @CreateDate DATETIME ,
    @Bank INT ,
    @CardNo NVARCHAR(50) ,
    @IsActive BIT ,
    @OnlineGatewayIsActive BIT,
    @UserGuid UNIQUEIDENTIFIER
  AS
 
    INSERT  INTO [AccountInformations]
            ( [Guid] ,
              [Owner] ,
              [Branch] ,
              [AccountNo] ,
              [TerminalID],
              [UserName],
              [Password],
              [PinCode],
              [CreateDate] ,
              [Bank] ,
              [CardNo] ,
              [IsActive] ,
              [OnlineGatewayIsActive],
              [IsDeleted] ,
              [UserGuid]
            )
    VALUES  ( @Guid ,
              @Owner ,
              @Branch ,
              @AccountNo ,
              @TerminalID,
              @UserName,
              @Password,
              @PinCode,
              @CreateDate ,
              @Bank ,
              @CardNo ,
              @IsActive ,
              @OnlineGatewayIsActive,
              0 ,
              @UserGuid
            )


GO
/****** Object:  StoredProcedure [dbo].[AccountInformations_UpdateAccount]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AccountInformations_UpdateAccount]
		@Guid UNIQUEIDENTIFIER ,
		@Owner NVARCHAR(50) ,
		@Branch NVARCHAR(50) ,
		@AccountNo NVARCHAR(100) ,
		@TerminalID NVARCHAR(50),
		@UserName NVARCHAR(50),
		@Password NVARCHAR(50),
		@PinCode NVARCHAR(50),
		@Bank INT ,
		@CardNo NVARCHAR(50) ,
		@IsActive BIT,
		@OnlineGatewayIsActive BIT
  AS
 
		UPDATE
			[AccountInformations]
		SET
			[Owner] = @Owner ,
			[Branch] = @Branch ,
			[AccountNo] = @AccountNo ,
			[TerminalID]=@TerminalID,
			[UserName]=@UserName,
			[Password]=@Password,
			[PinCode]=@PinCode,
			[Bank] = @Bank ,
			[CardNo] = @CardNo ,
			[IsActive] = @IsActive,
			[OnlineGatewayIsActive]=@OnlineGatewayIsActive 
		WHERE
			[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[AgentRatio_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgentRatio_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[AgentRatio] WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[AgentRatio_GetPagedAgentRatio]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgentRatio_GetPagedAgentRatio]
	@SmsSenderAgentGuid UNIQUEIDENTIFIER
  AS


SELECT
	ratio.[Guid] ,
	[SmsType] ,
	[Ratio] ,
	[CreateDate] ,
	[Name] AS [Operator],
	opt.[ID] AS [OperatorID]
FROM
	[dbo].[AgentRatio] ratio INNER JOIN
	[dbo].[Operators] opt ON ratio.[OperatorID] = opt.[ID]
WHERE
	ratio.[SmsSenderAgentGuid] = @SmsSenderAgentGuid;
GO
/****** Object:  StoredProcedure [dbo].[AgentRatio_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AgentRatio_Insert]
	@Guid UNIQUEIDENTIFIER,
	@SmsType TINYINT,
	@Ratio DECIMAL(18,2),
	@CreateDate DATETIME,
	@OperatorID TINYINT,
	@SmsSenderAgentGuid UNIQUEIDENTIFIER
  AS

	INSERT INTO [dbo].[AgentRatio]
	        ([Guid] ,
	         [SmsType] ,
	         [Ratio] ,
	         [CreateDate] ,
	         [OperatorID] ,
	         [SmsSenderAgentGuid])
					VALUES
					(@Guid,
					 @SmsType,
					 @Ratio,
					 @CreateDate,
					 @OperatorID,
					 @SmsSenderAgentGuid)
GO
/****** Object:  StoredProcedure [dbo].[CleanBrokerConversations]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CleanBrokerConversations]
  AS

BEGIN
    BEGIN TRY
        -- create a memory table. I dont prefer cursors usually
        DECLARE @t TABLE( AutoID INT IDENTITY, ConversationHandle UNIQUEIDENTIFIER)
        
        -- insert the handles of all open conversations to the 
        -- memory table
        INSERT INTO 
            @t (ConversationHandle)
        SELECT 
            [conversation_handle] 
        FROM
            sys.conversation_endpoints
            
        -- local variables
        DECLARE @cnt INT, @max INT, @handle UNIQUEIDENTIFIER
        SELECT @cnt = 1, @max = COUNT(*) FROM @t
        
        -- run a loop for each row in the memory table
        WHILE @cnt <= @max BEGIN
            -- read the conversation_handle
            SELECT
                @handle = ConversationHandle
            FROM @t WHERE AutoID = @cnt
            
            -- end conversation
            PRINT 'Closing conversation: ' + CAST(@handle AS VARCHAR(50))
            END CONVERSATION @handle WITH CLEANUP
            
            -- increment counter
            SELECT @cnt = @cnt + 1
        END
        
    END TRY
    BEGIN CATCH
        PRINT ERROR_MESSAGE()
    END CATCH
END
GO
/****** Object:  StoredProcedure [dbo].[Contents_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contents_Delete]
	@Guids NVARCHAR(MAX)
  AS


SELECT
	[Item]
	INTO #Temp
FROM
	[dbo].[SplitString](@Guids,',')
OPTION	(MAXRECURSION 0);

UPDATE [dbo].[Contents] SET [IsDeleted] = 1 WHERE [Guid] IN (SELECT * FROM #Temp);

DROP TABLE #Temp;


GO
/****** Object:  StoredProcedure [dbo].[Contents_GetPagedContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contents_GetPagedContents]
	@Guid UNIQUEIDENTIFIER,
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '';
DECLARE	@StartRow INT = (@PageNo - 1) * @PageSize;

IF (@Where != '') 
	SET @Where += ' AND';
SET @Where += ' [RegularContentGuid]=''' + CAST(@Guid AS VARCHAR(36)) + '''';

SET @Where = ' WHERE ' + @Where;

--------------------------------------------------
DECLARE @Statement NVARCHAR(MAX) = '';

SET @Statement = '
		SELECT
			*
			INTO #Temp
		FROM
			[dbo].[Contents] '+ @Where +';
		
		SELECT COUNT(*) [RowCount] FROM #Temp;
		SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);

GO
/****** Object:  StoredProcedure [dbo].[Contents_InsertContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contents_InsertContents]
  @RegularContentGuid UNIQUEIDENTIFIER ,
  @Contents [Content] READONLY
  AS
 
INSERT INTO	[dbo].[Contents]
				([Guid] ,
				 [Text] ,
				 [SmsLen] ,
				 [IsUnicode] ,
				 [CreateDate] ,
				 [RegularContentGuid])
			 SELECT
				 NEWID(),
			 	 [Text],
				 dbo.GetSmsCount([Text]),
				 dbo.HasUniCodeCharacter([Text]),
				 GETDATE(),
				 @RegularContentGuid
			 FROM
			 	 @Contents


GO
/****** Object:  StoredProcedure [dbo].[Contents_SendContentToReceiver]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Contents_SendContentToReceiver]
	@RegularContentGuid  UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@PeriodType TINYINT,
	@Period INT,
	@EffectiveDateTime DATETIME
  AS

DECLARE @Guid UNIQUEIDENTIFIER;
DECLARE @ID BIGINT;
DECLARE @Text NVARCHAR(512);
DECLARE @SmsLen TINYINT;
DECLARE @IsUnicode BIT;
DECLARE @PreviousContentGuid UNIQUEIDENTIFIER;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
--DECLARE @trancount INT;
DECLARE @NewGuid UNIQUEIDENTIFIER;
DECLARE @Encoding INT;
DECLARE @Now DATETIME;
DECLARE @ReceiverNumbers TABLE([Mobile] NVARCHAR(16));
DECLARE @NextDateTime DATETIME = @EffectiveDateTime;

SELECT [PhoneBookGuid] INTO #Phonebooks FROM [dbo].[PhoneBookRegularContents] WHERE [RegularContentGuid] = @RegularContentGuid;
SELECT [Guid] INTO #PhoneNumbers FROM [PhoneNumbers] WHERE IsDeleted = 0 AND [PhoneBookGuid] IN (SELECT [PhoneBookGuid] FROM #Phonebooks)
SELECT  [Guid],[ID],[Text],[SmsLen],[IsUnicode],ISNULL(LAG([Guid]) OVER (ORDER BY ID),@EmptyGuid) [PreviousGuid] INTO #Contents FROM [dbo].[Contents] WHERE	[RegularContentGuid] = @RegularContentGuid ORDER BY ID;

SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION
	DECLARE ContentCursor CURSOR FAST_FORWARD READ_ONLY FOR
	SELECT  
		[Guid] ,
		[ID] ,
		[Text] ,
		[SmsLen] ,
		[IsUnicode] ,
		[PreviousGuid]
	FROM #Contents

	OPEN ContentCursor
	FETCH NEXT FROM ContentCursor INTO @Guid,@ID,@Text,@SmsLen,@IsUnicode,@PreviousContentGuid
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF(@PreviousContentGuid != @EmptyGuid)
		BEGIN

			DELETE FROM @ReceiverNumbers;
			SET @Now = GETDATE();

			SELECT [Guid],[PhoneNumberGuid] INTO #Receivers FROM [dbo].[PhoneNumberContents] WHERE [ContentGuid] = @PreviousContentGuid AND [PhoneNumberGuid] IN (SELECT [Guid] FROM #PhoneNumbers);

			INSERT INTO [dbo].[PhoneNumberContents]([Guid] ,[CreateDate] ,[PhoneNumberGuid],[ContentGuid])
			SELECT NEWID(),GETDATE(),[PhoneNumberGuid],@Guid FROM #Receivers;

			DELETE FROM [dbo].[PhoneNumberContents] WHERE [Guid] IN (SELECT [Guid] FROM #Receivers);

			INSERT INTO @ReceiverNumbers(Mobile)
			SELECT [CellPhone] FROM [PhoneNumbers] WHERE [Guid] IN (SELECT [PhoneNumberGuid] FROM #Receivers)

			IF @IsUnicode = 1
				SET @Encoding = 2--UTF8
			ELSE
				SET @Encoding = 1--Default

			EXEC [dbo].[ScheduledSmses_InsertRegularContentSms]
			 @Guid = @NewGuid,
			 @PrivateNumberGuid = @PrivateNumberGuid,
			 @Receiver = @ReceiverNumbers,
			 @SmsText = @Text,
			 @SmsLen = @SmsLen,
			 @PresentType = 1, --Normal
			 @Encoding = @Encoding, -- int
			 @TypeSend = 9, -- SendRegularContentSms
			 @Status = 1, -- Stored
			 @DateTimeFuture = @Now,
			 @UserGuid = @UserGuid;
			
			DROP TABLE #Receivers;
		END
		FETCH NEXT FROM ContentCursor INTO @Guid,@ID,@Text,@SmsLen,@IsUnicode,@PreviousContentGuid
	END
	CLOSE ContentCursor
	DEALLOCATE ContentCursor

	DELETE FROM @ReceiverNumbers;

	SELECT @Guid = [Guid],@Text = [Text],@IsUnicode = [IsUnicode] FROM #Contents WHERE [PreviousGuid] = @EmptyGuid;

	SELECT [Guid],[PhoneNumberGuid] INTO #Recipients FROM [dbo].[PhoneNumberContents] WHERE [PhoneNumberGuid] NOT IN (SELECT [Guid] FROM #PhoneNumbers);

	INSERT INTO [dbo].[PhoneNumberContents]([Guid] ,[CreateDate] ,[PhoneNumberGuid],[ContentGuid])
	SELECT NEWID(),GETDATE(),[PhoneNumberGuid],@Guid FROM #Recipients;

	INSERT INTO @ReceiverNumbers(Mobile)
	SELECT [CellPhone] FROM [PhoneNumbers] WHERE [Guid] IN (SELECT [PhoneNumberGuid] FROM #Recipients)

	IF @IsUnicode = 1
		SET @Encoding = 2--UTF8
	ELSE
		SET @Encoding = 1--Default

	EXEC [dbo].[ScheduledSmses_InsertRegularContentSms]
		@Guid = @NewGuid,
		@PrivateNumberGuid = @PrivateNumberGuid,
		@Receiver = @ReceiverNumbers,
		@SmsText = @Text,
		@SmsLen = @SmsLen,
		@PresentType = 1, --Normal
		@Encoding = @Encoding, -- int
		@TypeSend = 9, -- SendRegularContentSms
		@Status = 1, -- Stored
		@DateTimeFuture = @Now,
		@UserGuid = @UserGuid;
			
	DROP TABLE #Recipients;


	SET	@NextDateTime = CASE 
												WHEN @PeriodType = 1 THEN DATEADD(MINUTE,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 2 THEN DATEADD(HOUR,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 3 THEN DATEADD(DAY,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 4 THEN DATEADD(WEEK,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 5 THEN DATEADD(MONTH,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 6 THEN DATEADD(YEAR,@Period,@EffectiveDateTime)
											END
	
	UPDATE [dbo].[RegularContents] SET [EffectiveDateTime] = @NextDateTime WHERE [Guid] = @RegularContentGuid;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
  IF (XACT_STATE() = -1)
    ROLLBACK TRANSACTION;
  IF (XACT_STATE() = 1)
    COMMIT TRANSACTION;

	 DECLARE @ErrorText NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'RegularContents',
			@Name = 'SendContentToReceiver' ,
			@Text = @ErrorText,
			@IP = '',
			@Browser = '',
			@ReferenceGuid = @RegularContentGuid,
			@UserGuid = @EmptyGuid;
END CATCH

DROP TABLE #Phonebooks;
DROP TABLE #PhoneNumbers;
DROP TABLE #Contents;
GO
/****** Object:  StoredProcedure [dbo].[CreateDirectory]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[CreateDirectory]
 @Path NVARCHAR(4000)
  AS


DECLARE @folder_exists AS INT
 
DECLARE @file_results TABLE
(file_exists INT,
file_is_a_directory INT,
parent_directory_exists INT)
 
INSERT INTO @file_results
(file_exists, file_is_a_directory, parent_directory_exists)
EXEC master.dbo.xp_fileexist @Path
    
SELECT @folder_exists = file_is_a_directory
FROM @file_results
     
IF @folder_exists = 0
BEGIN
  EXECUTE master.dbo.xp_create_subdir @Path
END
GO
/****** Object:  StoredProcedure [dbo].[DataCenters_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DataCenters_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


UPDATE  [dbo].[DataCenters]
SET     [IsDeleted] = 1
WHERE   [Guid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[DataCenters_GetDomainMenu]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[DataCenters_GetDomainMenu]
	@DomainGuid UNIQUEIDENTIFIER,
	@Location INT,
	@Desktop INT
  AS

	DECLARE @UserGuid UNIQUEIDENTIFIER;
	SELECT @UserGuid = [UserGuid] FROM [dbo].[Domains] WHERE [IsDeleted] = 0 AND [Guid] = @DomainGuid;
	
	SELECT
		 data.[Guid],
		 data.[ID] ,
		 data.[Title] ,
		 data.[Priority] ,
		 data.[ParentGuid]
	FROM
		[dbo].[DataCenters] center INNER JOIN
		[dbo].[Datas] data ON center.[Guid] = data.[DataCenterGuid]
	WHERE
		center.[IsDeleted] = 0 AND
		data.[IsDeleted] = 0 AND
		data.[IsActive] = 1 AND
		center.[Location] = @Location AND
		center.[Desktop] = @Desktop AND
		[UserGuid] = @UserGuid AND
		(data.[FromDate] = data.[ToDate] OR (CONVERT(DATE,GETDATE()) BETWEEN data.[FromDate] AND data.[ToDate])) AND
		[Type] = 2--Menu
	ORDER BY
		[Priority]

GO
/****** Object:  StoredProcedure [dbo].[DataCenters_GetUserDataCenter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DataCenters_GetUserDataCenter]
	@UserGuid UNIQUEIDENTIFIER,
	@DataCenterType INT
  AS

	DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0';
	
	IF ( @Where != '' ) 
			SET @Where += ' AND'
	SET @Where += ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
		
	IF (@DataCenterType != 0) 
  BEGIN
    IF ( @Where != '' ) 
      SET @Where += ' AND'
    SET @Where += ' [Type]=' + CAST(@DataCenterType AS VARCHAR(1))
  END 
  
  IF (@Where != '' ) 
    SET @Where = ' WHERE ' + @Where
---------------------------------------------------------
	EXEC('SELECT     
					*
				FROM
					[DataCenters]'+@Where)


GO
/****** Object:  StoredProcedure [dbo].[DataCenters_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[DataCenters_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(MAX),
	@Type INT,
	@Location INT,
	@Desktop INT,
	@CreateDate DATETIME,
	@IsArchived BIT,
	@UserGuid UNIQUEIDENTIFIER
  AS

INSERT INTO [dbo].[DataCenters]
           ([Guid],
           [Title],
           [Type],
           [CreateDate] ,
           [IsArchived] ,
           [UserGuid] ,
           [IsDeleted])
     VALUES
           (@Guid,
           @Title,
           @Type,
           @CreateDate ,
           @IsArchived,
           @UserGuid,
           0)


GO
/****** Object:  StoredProcedure [dbo].[DataCenters_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[DataCenters_Update]
	@Guid UNIQUEIDENTIFIER ,
	@Title NVARCHAR(50) ,
	@Type INT,
	@IsArchived BIT
  AS


UPDATE 
	[dbo].[DataCenters]
SET     
	[Title] = @Title ,
  [Type] = @Type ,
  [IsArchived] = @IsArchived
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[DataCenters_UpdateLocation]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DataCenters_UpdateLocation]
  @Guid UNIQUEIDENTIFIER ,
  @Location INT,
	@Desktop INT
  AS


UPDATE
	[dbo].[DataCenters]
SET
	[Location] = @Location,
	[Desktop] = @Desktop
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Datas_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Datas_Delete]
	  @Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [dbo].[Datas]
    SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Datas_GetDataOfDataCenter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Datas_GetDataOfDataCenter]
	@UserGuid UNIQUEIDENTIFIER,
	@DataCenterGuid UNIQUEIDENTIFIER,
	@DataCenterType INT
  AS

	DECLARE @Where NVARCHAR(MAX) = '[Datas].[IsDeleted] = 0 AND [DataCenters].[IsDeleted] = 0';
	
	IF ( @Where != '' ) 
		SET @Where += ' AND'
	SET @Where += ' [DataCenters].[UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
	
	IF ( @Where != '' ) 
		SET @Where += ' AND'
	SET @Where += ' [DataCenters].[Guid]=''' + CAST(@DataCenterGuid AS VARCHAR(36)) + ''''
	
	 IF (@DataCenterType != 0) 
	 BEGIN
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [Type]=' + CAST(@DataCenterType AS VARCHAR(1))
	 END 
	 
	 IF (@Where != '' ) 
		SET @Where = ' WHERE ' + @Where
		
----------------------------------------------------
EXEC('
	SELECT
		dbo.Datas.Guid ,
    dbo.Datas.Title ,
    Content ,
    FromDate ,
    ToDate ,
    dbo.Datas.CreateDate ,
    Priority ,
    ParentGuid ,
    IsActive ,
    dbo.Datas.IsArchived ,
    dbo.Datas.IsDeleted ,
    DataCenterGuid
	FROM
		[Datas] INNER JOIN [DataCenters]
		ON [Datas].[DataCenterGuid] = [DataCenters].[Guid]
	'+@Where);


GO
/****** Object:  StoredProcedure [dbo].[Datas_GetMenusOfDataCenter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Datas_GetMenusOfDataCenter]
	@DataCenterGuid UNIQUEIDENTIFIER
  AS


SELECT 
	*
FROM 
	[dbo].[Datas]
WHERE 
	[IsDeleted] = 0 AND
	[DataCenterGuid] = @DataCenterGuid AND
	[IsActive] = 1
ORDER BY 
	Priority


GO
/****** Object:  StoredProcedure [dbo].[Datas_GetPagedData]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Datas_GetPagedData]
	@UserGuid UNIQUEIDENTIFIER,
  @DataCenterGuid UNIQUEIDENTIFIER,
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


SELECT 
	COUNT(*) AS [RowCount] 
FROM 
	[Datas]
WHERE [DataCenterGuid] = @DataCenterGuid AND IsDeleted = 0;
	
WITH expTemp AS
(
	SELECT
		Row_Number() OVER (ORDER BY @SortField) AS [RowNumber], 
		*
	FROM
		[Datas]
	WHERE [DataCenterGuid] = @DataCenterGuid AND IsDeleted = 0
)

SELECT 
		*
FROM
	expTemp
WHERE 
	(@PageNo = 0 AND @PageSize = 0) OR
	([RowNumber] > (@PageNo + (-1)) * @PageSize AND [RowNumber] <= (@PageNo * @PageSize))
ORDER BY
		[RowNumber] ;


GO
/****** Object:  StoredProcedure [dbo].[Datas_GetUserData]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Datas_GetUserData]
	@UserGuid UNIQUEIDENTIFIER
  AS

	SELECT
		data.[Guid],
		data.[Title],
		data.[Summary], 
		data.[Content], 
		data.[FromDate], 
		data.[ToDate], 
		data.[CreateDate], 
		data.[IsActive], 
		data.[IsArchived], 
		data.[IsDeleted], 
    data.[DataCenterGuid], 
    center.[Title] AS [NewsCenterTitle]
FROM
    [dbo].[Datas] data INNER JOIN
    [dbo].[DataCenters] center ON data.[DataCenterGuid] = center.[Guid]
WHERE
	  data.[IsDeleted] = 0 AND
		center.[IsDeleted] = 0 AND
	  center.[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Datas_InsertData]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[Datas_InsertData]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(MAX),
	@Priority INT,
	@Summary NVARCHAR(MAX),
	@Content NVARCHAR(MAX),
	@Keywords NVARCHAR(MAX),
	@FromDate DATETIME,
	@ToDate DATETIME,
	@CreateDate DATETIME,
	@ParentGuid UNIQUEIDENTIFIER,
	@DataCenterGuid UNIQUEIDENTIFIER
  AS

INSERT INTO [Datas]
           ([Guid],
            [Title],
						[Priority],
            [Summary],
            [Content],
            [Keywords],
            [FromDate],
            [ToDate],
            [CreateDate],
						[ParentGuid],
            [IsActive],
            [IsArchived],
					  [IsDeleted],
					  [DataCenterGuid])
			 VALUES
					 (@Guid,
					  @Title,
					  @Priority,
					  @Summary,
					  @Content,
					  @Keywords,
					  @FromDate,
					  @ToDate,
					  @CreateDate,
					  @ParentGuid,
					  1,
					  0,
					  0,
					  @DataCenterGuid)


GO
/****** Object:  StoredProcedure [dbo].[Datas_UpdateData]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Datas_UpdateData]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(MAX),
	@Priority INT,
	@Summary NVARCHAR(MAX),
	@Content NVARCHAR(MAX),
	@Keywords NVARCHAR(MAX),
	@FromDate DATETIME,
	@ToDate DATETIME,
	@ParentGuid UNIQUEIDENTIFIER,
	@DataCenterGuid UNIQUEIDENTIFIER
  AS
 
	UPDATE  [dbo].[Datas]
	SET     
		[Title] = @Title ,
		[Priority] = @Priority,
		[Summary] = @Summary ,
		[Content] = @Content ,
		[Keywords] = @Keywords,
		[FromDate] = @FromDate ,
		[ToDate] = @ToDate ,
		[ParentGuid] = @ParentGuid,
		[DataCenterGuid] = @DataCenterGuid 
	WHERE   
		[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Domains_CheckValidDomainName]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_CheckValidDomainName]
	@Name NVARCHAR(50)
  AS

	SELECT * FROM [Domains]
	WHERE [Name]=@Name


GO
/****** Object:  StoredProcedure [dbo].[Domains_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_Delete]
   @Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [Domains]
    SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Domains_GetAgentChildrenDomains]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetAgentChildrenDomains]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @AgentGuid Table([Guid] UNIQUEIDENTIFIER);

INSERT INTO @AgentGuid([Guid])
SELECT UserGuid FROM dbo.[udfGetAllAgentChildren](@UserGuid);

SELECT * FROM [dbo].[Domains] WHERE [UserGuid] IN (SELECT	[Guid] FROM @AgentGuid);
GO
/****** Object:  StoredProcedure [dbo].[Domains_GetContent]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetContent]
	@DomainName NVARCHAR(50),
	@Location INT,
	@Desktop INT
  AS

	DECLARE @UserGuid UNIQUEIDENTIFIER;

	SELECT @UserGuid = [UserGuid] FROM [dbo].[Domains] WHERE [Name] = @DomainName;

	SELECT
		data.[Guid] ,
		data.[ID] ,
		data.[Title] ,
		data.[Summary] ,
		data.[Content]
	FROM
		[dbo].[DataCenters] center INNER JOIN
		[dbo].[Datas] data ON center.[Guid] = data.[DataCenterGuid]
	WHERE
		center.[IsDeleted] = 0 AND
		center.[UserGuid] = @UserGuid AND
		data.[IsDeleted] = 0 AND
		data.[IsActive] = 1 AND
		center.[Desktop] = @Desktop AND
		center.[Location] = @Location AND
		(CONVERT(DATE,GETDATE()) BETWEEN data.[FromDate] AND data.[ToDate])
	ORDER BY
		data.[Priority]

GO
/****** Object:  StoredProcedure [dbo].[Domains_GetDomain]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetDomain]
	@Guid UNIQUEIDENTIFIER
  AS

	SELECT 
		domain.[Guid] ,
		domain.[Name] ,
		domain.[CreateDate] ,
		domain.[Desktop] ,
		domain.[DefaultPage] ,
		domain.[Theme] ,
		domain.[UserGuid],
		users.[UserName]
	FROM
		[dbo].[Domains] domain LEFT JOIN
		[dbo].[Users] users ON domain.[UserGuid] = users.[Guid]
	WHERE
		domain.[Guid] = @Guid AND
		domain.[IsDeleted] = 0


GO
/****** Object:  StoredProcedure [dbo].[Domains_GetDomainGuid]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetDomainGuid]
	@Name NVARCHAR(64)
  AS

	SELECT * FROM [Domains]
	WHERE [Name] = @Name


GO
/****** Object:  StoredProcedure [dbo].[Domains_GetDomainInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetDomainInfo] 
	@DomainName NVARCHAR(50)
  AS
 
SELECT
				[Guid],
        [Name],
				[Desktop],
				[DefaultPage],
				[Theme],
				[CreateDate],
				[UserGuid]
FROM    
				[Domains]
WHERE		
				[IsDeleted] = 0 AND
				[Name] = @DomainName


GO
/****** Object:  StoredProcedure [dbo].[Domains_GetDomainName]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetDomainName]
	@DomainGuid UNIQUEIDENTIFIER
  AS


SELECT
			[Name]
FROM 
			[Domains]
WHERE 
			[Guid] = @DomainGuid


GO
/****** Object:  StoredProcedure [dbo].[Domains_GetDomainSlideShow]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Domains_GetDomainSlideShow]
	@DomainName NVARCHAR(50)
  AS

	DECLARE @GalleryGuid UNIQUEIDENTIFIER;

	SELECT 
		@GalleryGuid = [Value] 
	FROM
		[dbo].[DomainSettings] setting INNER JOIN
		[dbo].[Domains] domain ON domain.[Guid] = setting.[DomainGuid]
	WHERE
		domain.[IsDeleted] = 0 AND
		domain.[Name] = @DomainName AND
		[Key] = 5;--SlideShow

	SELECT 
		img.[Title] AS [ImageTitle],
		img.[Description] ,
		img.[ImagePath] ,
		img.[DataGuid],
		data.[ID],
		data.[Title] AS [DataTitle]
	FROM
		[dbo].[GalleryImages] gallery INNER JOIN
		[dbo].[Images] img ON gallery.[Guid] = img.[GalleryImageGuid] LEFT JOIN
		[dbo].[Datas] data ON img.[DataGuid] = data.[Guid] AND
													data.[IsDeleted] = 0 AND
													data.[IsActive] = 1
	WHERE
		gallery.[IsActive] = 1 AND
		gallery.IsDeleted = 0 AND
		img.[IsActive] = 1 AND
		img.IsDeleted = 0 AND
		gallery.[Guid] = @GalleryGuid
	

GO
/****** Object:  StoredProcedure [dbo].[Domains_GetGuidAdminOfDomain]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetGuidAdminOfDomain] 
	@Name NVARCHAR(50)
  AS


SELECT  
	[UserGuid]
FROM
	[dbo].[Domains]
WHERE
	[Name] = @Name


GO
/****** Object:  StoredProcedure [dbo].[Domains_GetPagedDomains]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_GetPagedDomains]
  @UserGuid UNIQUEIDENTIFIER ,
  @Query NVARCHAR(MAX),
  @PageNo INT,
  @PageSize INT,
  @SortField NVARCHAR(256)
  AS
 

DECLARE @Where NVARCHAR(MAX)= 'dmn.[IsDeleted] = 0';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';	

SET @Where = 'WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

--------------------------------------------------------------------
SET @Statement ='
SELECT * INTO #Children FROM dbo.udfGetAllChildren('''+ CAST(@UserGuid AS VARCHAR(36)) +''');

  SELECT 
		dmn.*
		INTO #Temp
	FROM
		[Domains] dmn WITH(NOLOCK) INNER JOIN
		#Children usr ON usr.[UserGuid] = dmn.[UserGuid] '+ @Where +';

	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';DROP TABLE #Temp;DROP TABLE #Children';

EXEC(@Statement);

GO
/****** Object:  StoredProcedure [dbo].[Domains_GetSalePackages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Domains_GetSalePackages]
	@DomainName NVARCHAR(50)
  AS


DECLARE @UserGuid UNIQUEIDENTIFIER;

SELECT @UserGuid = [UserGuid] FROM [dbo].[Domains] WHERE [Name] = @DomainName;

SELECT
	[Guid] ,
	[ID] ,
	[Title] ,
	[Price] ,
	[Description]
	INTO #roles
FROM
	[dbo].[Roles]
WHERE
	[IsDeleted] = 0 AND
	[IsSalePackage] = 1 AND
  [UserGuid] = @UserGuid
ORDER BY
	[Priority]

SELECT * FROM #roles;

SELECT
	[RoleGuid] ,
	[Title],
	[TitleFa]
FROM
	[dbo].[RoleServices] roleService INNER JOIN
	[dbo].[Services] srvic ON roleService.[ServiceGuid] = srvic.[Guid]
WHERE
	srvic.[Presentable] = 1 AND
	[RoleGuid] IN(SELECT [Guid] FROM #roles);

DROP TABLE #roles;
GO
/****** Object:  StoredProcedure [dbo].[Domains_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_Insert]
  @Guid UNIQUEIDENTIFIER ,
  @Name NVARCHAR(50) ,
  @Desktop INT,
  @DefaultPage INT,
  @Theme INT,
  @CreateDate DATETIME ,
  @UserGuid UNIQUEIDENTIFIER
  AS
 
  INSERT  INTO [Domains]
          ( [Guid] ,
            [Name] ,
            [Desktop],
            [DefaultPage],
            [Theme],
            [IsDeleted],
            [CreateDate] ,
            [UserGuid])
					VALUES
					( @Guid ,
						@Name ,
						@Desktop,
						@DefaultPage,
						@Theme,
						0,
						@CreateDate ,
						@UserGuid);

	UPDATE [dbo].[Users] SET [DomainGuid] = @Guid WHERE [Guid] = @UserGuid;


GO
/****** Object:  StoredProcedure [dbo].[Domains_UpdateDomain]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Domains_UpdateDomain]
	@Guid UNIQUEIDENTIFIER ,
	@Name NVARCHAR(50) ,
	@Desktop INT ,
	@DefaultPage INT,
	@Theme INT,
	@UserGuid UNIQUEIDENTIFIER
  AS

	DECLARE @CurrentUserGuid UNIQUEIDENTIFIER;
	DECLARE @ParentDomainGuid UNIQUEIDENTIFIER;
	DECLARE @ParentGuid UNIQUEIDENTIFIER;

	SELECT @ParentGuid = [ParentGuid],@CurrentUserGuid = [Guid] FROM [dbo].[Users] 
	WHERE [Guid] IN (SELECT [UserGuid] FROM [dbo].[Domains] WHERE [Guid] = @Guid);

	IF(@UserGuid != @CurrentUserGuid)
	BEGIN
		SELECT @ParentDomainGuid = [DomainGuid] FROM [dbo].[Users] WHERE [Guid] = @ParentGuid;

		UPDATE [dbo].[Users] SET [DomainGuid] = @ParentDomainGuid WHERE [Guid] = @CurrentUserGuid;
	END
	
  UPDATE
		[dbo].[Domains]
  SET
		[Name] = @Name ,
    [Desktop] = @Desktop ,
    [DefaultPage] = @DefaultPage,
    [Theme] = @Theme,
		[UserGuid] = @UserGuid
  WHERE   
		[Guid] = @Guid;

	UPDATE [dbo].[Users] SET [DomainGuid] = @Guid WHERE [Guid] = @UserGuid;


GO
/****** Object:  StoredProcedure [dbo].[DomainSettings_GetDomainSettings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[DomainSettings_GetDomainSettings]
	@DomainGuid UNIQUEIDENTIFIER
  AS

	Select 
			*
	From
			[dbo].[DomainSettings]
	Where
			[DomainGuid] = @DomainGuid


GO
/****** Object:  StoredProcedure [dbo].[DomainSettings_InsertSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DomainSettings_InsertSetting]
  @DomainGuid UNIQUEIDENTIFIER ,
  @Setting [DomainSetting] READONLY
  AS

	DELETE FROM [dbo].[DomainSettings]
	WHERE
		[DomainGuid] = @DomainGuid AND
		[Key] IN(SELECT [Key] FROM @Setting);

	INSERT INTO [dbo].[DomainSettings]
	        ([Guid],
					 [DomainGuid],
					 [Key],
					 [Value])
				 SELECT 
					 NEWID(),
					 @DomainGuid,
					 [Key],
					 [Value]
				 FROM
					 @Setting;


GO
/****** Object:  StoredProcedure [dbo].[FailedOnlinePayments_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FailedOnlinePayments_Insert]
    @Guid UNIQUEIDENTIFIER ,
    @OrderID NVARCHAR(50) ,
    @ReferenceID NVARCHAR(50) ,
    @Bank INT,
    @CreateDate DATETIME ,
    @UserGuid UNIQUEIDENTIFIER
  AS
 
    INSERT INTO [FailedOnlinePayments]
							( [Guid] ,
								[OrderID],
								[ReferenceID],
								[Bank],
								[CreateDate] ,
								[UserGuid],
								[IsDeleted]
							)
			VALUES  ( @Guid ,
								@OrderID,
								@ReferenceID,
								@Bank,
								@CreateDate ,
								@UserGuid,
								0
							)


GO
/****** Object:  StoredProcedure [dbo].[FilterWords_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FilterWords_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[FilterWords] WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[FilterWords_GetWords]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FilterWords_GetWords]
  AS


SELECT * FROM [dbo].[FilterWords];
GO
/****** Object:  StoredProcedure [dbo].[FilterWords_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[FilterWords_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(255)
  AS


INSERT INTO [dbo].[FilterWords]
        ([Guid],
				 [Title])
			 VALUES
				(@Guid,
         @Title)

GO
/****** Object:  StoredProcedure [dbo].[Fishes_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_Delete]
(
@Guid UNIQUEIDENTIFIER
)
  AS

DELETE FROM
	[Fishes]
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Fishes_GetFishStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_GetFishStatus]
	@Guid UNIQUEIDENTIFIER
  AS


SELECT [Status] FROM [Fishes]	WHERE [Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[Fishes_GetPagedFishesForConfirm]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_GetPagedFishesForConfirm]
  @Query NVARCHAR(MAX) ,
	@UserGuid UNIQUEIDENTIFIER,
  @ParentGuid UNIQUEIDENTIFIER,
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS
 

DECLARE @Where NVARCHAR(MAX) = '[UserIsDeleted] = 0 AND [AccountIsDeleted] = 0';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

IF (@ParentGuid != '00000000-0000-0000-0000-000000000000') 
	SET @Where += ' AND [ParentGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
ELSE
	SET @Where += ' AND ([ParentGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''' OR [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''')'

IF ( @Where != '' ) 
	SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

PRINT @Where;
---------------------------------------------------
SET @Statement ='
	SELECT * INTO #Temp FROM [vwUserFishes]' +	@Where + ';
	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';
	
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';

SELECT SUM([SmsCount]) [TotalSmsCount],SUM([Amount]) [TotalPrice] FROM #Temp;
DROP TABLE #Temp;';

EXEC(@Statement);
GO
/****** Object:  StoredProcedure [dbo].[Fishes_GetPagedUserFishes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_GetPagedUserFishes]
  @UserGuid UNIQUEIDENTIFIER ,
  @Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS
 

DECLARE @Where NVARCHAR(MAX) = '[UserIsDeleted] = 0 AND [AccountIsDeleted] = 0'
		
IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where = ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
    		
IF ( @Where != '' ) 
  SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;
---------------------------------------------------
EXEC('SELECT COUNT(*) AS [RowCount] FROM [vwUserFishes]' +	@Where + ';
	
WITH expTemp AS
(
	SELECT
			Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
			[Guid],
			[Bank],
			[AccountNo],
			[CardNo],
			[Owner],
			[PaymentDate],
      [CreateDate],
			[SmsCount],
      [Amount] ,
      [BillNumber] ,
      [Description] ,
      [Type] ,
      [Status] ,
      [AccountInformationGuid] ,
      [UserGuid],
      [ParentGuid],
      [UserName]
	FROM
			[vwUserFishes]' +	@Where + '
)
SELECT 
		*
FROM
	expTemp
WHERE 
	(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
	([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
ORDER BY
		[RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[Fishes_InsertFishPayment]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Fishes_InsertFishPayment]
	@Guid UNIQUEIDENTIFIER ,
	@CreateDate DATETIME ,
	@BillNumber NVARCHAR(50),
	@SmsCount BIGINT,
	@Amount DECIMAL(18, 2) ,
	@PaymentDate DATETIME ,
	@Description NTEXT ,
	@Type INT ,
	@Status INT,
	@AccountInformationGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS


INSERT INTO [Fishes]
        ([Guid],
         [CreateDate],
         [PaymentDate],
				 [SmsCount],
         [Amount],
         [BillNumber],
         [Description],
         [Type],
         [Status],
         [AccountInformationGuid],
         [UserGuid])
  VALUES
        (@Guid,
				 @CreateDate,
				 @PaymentDate,
				 @SmsCount,
         @Amount,
         @BillNumber,
         @Description ,
         @Type,
         @Status,
         @AccountInformationGuid,
         @UserGuid);
	
	EXEC [dbo].[Fishes_SendSmsForPayment]
		@Guid = @Guid,
	  @UserGuid = @UserGuid
	


GO
/****** Object:  StoredProcedure [dbo].[Fishes_InsertOnlinePayment]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_InsertOnlinePayment]
	@Guid UNIQUEIDENTIFIER,
	@ReferenceID NVARCHAR(50),
	@CreateDate DATETIME,
	@PaymentDate DATETIME,
	@SmsCount BIGINT,
	@Amount DECIMAL(18,2),
	@OrderID NVARCHAR(50),
	@Description NVARCHAR(MAX),
	@Type INT,
	@Status INT,
	@ReferenceGuid UNIQUEIDENTIFIER,
	@AccountInformationGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS

	INSERT INTO [Fishes]
           ([Guid]
           ,[ReferenceID]
           ,[CreateDate]
           ,[PaymentDate]
					 ,[SmsCount]
           ,[Amount]
           ,[OrderID]
           ,[Description]
           ,[Type]
           ,[Status]
					 ,[ReferenceGuid]
           ,[AccountInformationGuid]
           ,[UserGuid])
     VALUES
           (@Guid
           ,@ReferenceID
           ,@CreateDate
           ,@PaymentDate
					 ,@SmsCount
           ,@Amount
           ,@OrderID
           ,@Description
           ,@Type
           ,@Status
					 ,@ReferenceGuid
           ,@AccountInformationGuid
           ,@UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[Fishes_IsDuplicateBillNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_IsDuplicateBillNumber]
	@BillNumber NVARCHAR(64)
  AS


SELECT * FROM [dbo].[Fishes] WHERE [BillNumber] = @BillNumber;
GO
/****** Object:  StoredProcedure [dbo].[Fishes_SendSmsForPayment]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_SendSmsForPayment]
	@Guid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsText NVARCHAR(MAX)='';
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Mobile NVARCHAR(16);
DECLARE @UserName NVARCHAR(32);
DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @MainAdminGuid UNIQUEIDENTIFIER;
DECLARE @Amount DECIMAL(18,2);
DECLARE @BillNumber NVARCHAR(64);
DECLARE @ID BIGINT;
DECLARE @Type INT;
DECLARE @Status INT;
DECLARE @SmssenderAgentReference INT;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SELECT
	@UserName = [UserName],
	@ParentGuid = [ParentGuid]
FROM [dbo].[Users] WITH(NOLOCK)
WHERE [Guid] = @UserGuid;

SELECT @Mobile = [Mobile] FROM [dbo].[Users] WITH(NOLOCK) WHERE [Guid] = @ParentGuid;

EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT 
	@Amount = [Amount],
	@BillNumber = [BillNumber],
	@ID = [ID],
	@Type = [Type],
	@Status = [Status]
FROM [dbo].[Fishes] WHERE [Guid] = @Guid;

IF(@Type = 4)--Account
	SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 4 AND [UserGuid] = @MainAdminGuid; --RegisterFishSmsText
ELSE IF	(@Type = 5 AND--Online
				 @Status = 1) --Confirmed
	SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 10 AND [UserGuid] = @MainAdminGuid; --OnlinePaymentSmsText
	
SELECT @PrivateNumberGuid = CAST([Value] AS UNIQUEIDENTIFIER) FROM [dbo].[UserSettings] WHERE [UserGuid] = @ParentGuid AND [Key] = 5--DefaultNumber

SET @SmsText = REPLACE(@SmsText,'#username#',@UserName);
SET @SmsText = REPLACE(@SmsText,'#billnumber#',@BillNumber);
SET @SmsText = REPLACE(@SmsText,'#billamount#',@Amount);
SET @SmsText = REPLACE(@SmsText,'#id#',@ID);

IF(ISNULL(@SmsText,'') = '' OR @PrivateNumberGuid = @EmptyGuid)
	RETURN;

IF(dbo.GetNumberOperator(@Mobile) = 0)
	RETURN;


SELECT @Encoding = 	CASE WHEN [dbo].[HasUniCodeCharacter](@SmsText) = 1 THEN 2
												 ELSE 1 
										END;
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

SELECT
	@SmssenderAgentReference = agnt.[SmsSenderAgentReference]
FROM
	[PrivateNumbers] pn  INNER JOIN
	[SmsSenderAgents] agnt ON agnt.[Guid] = pn.[SmsSenderAgentGuid]
WHERE 
	pn.[Guid] = @PrivateNumberGuid;

INSERT INTO dbo.ScheduledSmses
				([Guid] ,
				 [PrivateNumberGuid] ,
				 [SmsText] ,
				 [PresentType] ,
				 [Encoding] ,
				 [SmsLen] ,
				 [TypeSend] ,
				 [DateTimeFuture] ,
				 [CreateDate] ,
				 [Status] ,
				 [SmsSenderAgentReference],
				 [IsDeleted] ,
				 [UserGuid])
			SELECT
					@NewGuid,
					@PrivateNumberGuid,
					@SmsText,
					1,--Normal
					@Encoding,
					[dbo].[GetSmsCount](@SmsText),
					1,--SendSms
					GETDATE(),
					GETDATE(),
					1,--Stored
					@SmssenderAgentReference,
					0,
					@ParentGuid;

INSERT INTO [dbo].[Recipients]
	      ([Guid] ,
	       [Mobile] ,
	       [Operator] ,
	       [IsBlackList] ,
	       [ScheduledSmsGuid])
				SELECT
				 NEWID(),
				 @Mobile,
				 dbo.GetNumberOperator(@Mobile),
				 0,
				 @NewGuid;

GO
/****** Object:  StoredProcedure [dbo].[Fishes_UpdateOnlineFish]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Fishes_UpdateOnlineFish]
	@Guid UNIQUEIDENTIFIER,
	@Status INT,
	@BillNumber NVARCHAR(MAX),
	@TransactionGuid UNIQUEIDENTIFIER
  AS

DECLARE @UserGuid UNIQUEIDENTIFIER;
DECLARE @SmsCount BIGINT;

SELECT @UserGuid = [UserGuid],@SmsCount = [SmsCount] FROM [dbo].[Fishes] WHERE [Guid] = @Guid;

UPDATE [dbo].[Fishes] SET	[Status] = @Status,[BillNumber] = @BillNumber WHERE [Guid] = @Guid;

IF(@Status = 1)--Confirmed
EXEC [dbo].[Transactions_CalculateBenefit] 
		 @UserGuid = @UserGuid,
		 @SmsCount = @SmsCount,
		 @TransactionGuid = @TransactionGuid


EXEC [dbo].[Fishes_SendSmsForPayment]
	@Guid = @Guid,
	@UserGuid = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Fishes_UpdateStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Fishes_UpdateStatus]
	@Guid UNIQUEIDENTIFIER,
	@Status INT,
	@TransactionGuid UNIQUEIDENTIFIER
  AS

DECLARE @UserGuid UNIQUEIDENTIFIER;
DECLARE @SmsCount BIGINT;

SELECT @UserGuid = [UserGuid],@SmsCount = [SmsCount] FROM [dbo].[Fishes] WHERE [Guid] = @Guid;
UPDATE [Fishes] SET [Status] = @Status WHERE [Guid] = @Guid;

IF(@Status = 1)--Confirmed
EXEC [dbo].[Transactions_CalculateBenefit] 
		 @UserGuid = @UserGuid,
		 @SmsCount = @SmsCount,
		 @TransactionGuid = @TransactionGuid

GO
/****** Object:  StoredProcedure [dbo].[GalleryImages_Activation]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GalleryImages_Activation]
  @Guid UNIQUEIDENTIFIER
  AS

DECLARE @IsActive BIT;

SELECT @IsActive = [IsActive] FROM [dbo].[GalleryImages] WHERE [Guid] = @Guid;

IF(@IsActive = 1)
	SET @IsActive = 0;
ELSE
	SET	@IsActive = 1;

UPDATE  [GalleryImages]
SET     [IsActive] = @IsActive
WHERE   [Guid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[GalleryImages_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GalleryImages_Delete]
	@Guid UNIQUEIDENTIFIER
  AS
 
  UPDATE  [GalleryImages]
  SET     [IsDeleted] = 1
  WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[GalleryImages_GetAllGalleryImage]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GalleryImages_GetAllGalleryImage]
	@UserGuid UNIQUEIDENTIFIER
  AS

	SELECT 
		*
	FROM 
		[GalleryImages]
	Where 
		[IsDeleted] = 0 AND
		[UserGuid]=@UserGuid


GO
/****** Object:  StoredProcedure [dbo].[GalleryImages_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GalleryImages_Insert]
	@Guid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@Title NVARCHAR(MAX),
	@CreateDate DATETIME,
	@IsActive BIT
  AS

INSERT INTO GalleryImages
           ([Guid],
						[UserGuid],
						[Title],
						[CreateDate],
						[IsActive],
						[IsDeleted])
			 VALUES
					 (@Guid,
						@UserGuid,
						@Title,
						@CreateDate,
						1,
						0)


GO
/****** Object:  StoredProcedure [dbo].[GalleryImages_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GalleryImages_Update]
    @Guid UNIQUEIDENTIFIER ,
    @Title NVARCHAR(50)
  AS
 
    UPDATE  [GalleryImages]
    SET     [Title] = @Title
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_CheckGroupPriceName]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GroupPrices_CheckGroupPriceName]
    @Title NVARCHAR(50) ,
    @UserGuid UNIQUEIDENTIFIER
  AS
 
    SELECT  
					*
    FROM  [GroupPrices]
	  WHERE [Title] = @Title AND
					[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GroupPrices_Delete]
    @Guid UNIQUEIDENTIFIER
  AS
 

DELETE FROM [GroupPrices] WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_GetDefaultGroupPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GroupPrices_GetDefaultGroupPrice]
 @Domain NVARCHAR(64)
  AS


DECLARE @UserGuid UNIQUEIDENTIFIER;
SELECT @UserGuid = [UserGuid] FROM [dbo].[Domains] WHERE [IsDeleted] = 0 AND [Name] = @Domain;

SELECT TOP 1 [Guid] FROM [dbo].[GroupPrices]
WHERE [IsDefault] = 1  AND [UserGuid] = @UserGuid;


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_GetGroupPrices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GroupPrices_GetGroupPrices]
	@UserGuid UNIQUEIDENTIFIER
  AS


SELECT 
	*
FROM
	[dbo].[GroupPrices]
WHERE 
	[IsDeleted] = 0 AND
	[IsPrivate] = 0 AND
	[UserGuid] = @UserGuid
ORDER	BY [MinimumMessage]
GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_GetPagedGroupPrices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GroupPrices_GetPagedGroupPrices]
	@UserGuid UNIQUEIDENTIFIER
  AS


SELECT 
		*
FROM
	[dbo].[GroupPrices]
WHERE
	[IsDeleted] = 0 AND
	[UserGuid] = @UserGuid
ORDER BY [MinimumMessage];


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_GetUserBaseSmsPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GroupPrices_GetUserBaseSmsPrice]
	@UserGuid UNIQUEIDENTIFIER,
	@ParentGuid UNIQUEIDENTIFIER,
	@SmsCount BIGINT
  AS

	DECLARE @GroupGuid UNIQUEIDENTIFIER,
					@IsFixGroupPrice BIT;

	IF(@ParentGuid = '00000000-0000-0000-0000-000000000000')
		SET @ParentGuid = @UserGuid;

	SELECT @GroupGuid = [PriceGroupGuid],
				 @IsFixGroupPrice = [IsFixPriceGroup]
	FROM [Users] WHERE [Guid] = @UserGuid;

	IF( @IsFixGroupPrice = 0)
		SELECT * FROM [GroupPrices] 
		WHERE 
			[UserGuid] = @ParentGuid AND
			@SmsCount BETWEEN [MinimumMessage] AND [MaximumMessage]
	ELSE
		SELECT * FROM [GroupPrices] WHERE [Guid] = @GroupGuid;
	


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GroupPrices_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@MinimumMessage BIGINT,
	@MaximumMessage BIGINT,
	@BasePrice DECIMAL(18,2),
	@DecreaseTax BIT,
	@AgentRatio NVARCHAR(MAX),
	@CreateDate DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IsPrivate BIT,
	@IsDefault BIT
  AS


INSERT INTO [GroupPrices]
           ([Guid]
           ,[Title]
					 ,[MinimumMessage]
					 ,[MaximumMessage]
					 ,[BasePrice]
					 ,[DecreaseTax]
					 ,[AgentRatio]
           ,[CreateDate]
           ,[IsDeleted]
           ,[UserGuid]
					 ,[IsPrivate]
           ,[IsDefault])
				VALUES
					 (@Guid
					 ,@Title
					 ,@MinimumMessage
					 ,@MaximumMessage
					 ,@BasePrice
					 ,@DecreaseTax
					 ,@AgentRatio
					 ,@CreateDate
					 ,0
					 ,@UserGuid
					 ,@IsPrivate
					 ,@IsDefault)


GO
/****** Object:  StoredProcedure [dbo].[GroupPrices_UpdateGroupPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[GroupPrices_UpdateGroupPrice]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@MinimumMessage BIGINT,
	@MaximumMessage BIGINT,
	@BasePrice DECIMAL(18,2),
	@DecreaseTax BIT,
	@AgentRatio NVARCHAR(MAX),
	@IsPrivate BIT,
	@IsDefault BIT
  AS
 

UPDATE 
		[dbo].[GroupPrices]
SET 
		[Title] = @Title,
		[MinimumMessage] = @MinimumMessage,
		[MaximumMessage] = @MaximumMessage,
		[BasePrice] = @BasePrice,
		[DecreaseTax] = @DecreaseTax,
		[AgentRatio] = @AgentRatio,
		[IsPrivate] = @IsPrivate,
		[IsDefault] = @IsDefault
WHERE 
		[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Images_Activation]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Images_Activation]
  @Guid UNIQUEIDENTIFIER
  AS

DECLARE @IsActive BIT;

SELECT @IsActive = [IsActive] FROM [dbo].[Images] WHERE [Guid] = @Guid;

IF(@IsActive = 1)
	SET @IsActive = 0;
ELSE
	SET	@IsActive = 1;

UPDATE  [dbo].[Images]
SET     [IsActive] = @IsActive
WHERE   [Guid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[Images_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Images_Delete]
    (
      @Guid UNIQUEIDENTIFIER
    )
  AS
 
    UPDATE  [Images]
    SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Images_GetAllImage]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Images_GetAllImage]
	@UserGuid UNIQUEIDENTIFIER
  AS

SELECT	
	[GalleryImages].[Title] AS [GalleryImageTitle],
	[Images].[Guid],
	[Images].[Title],
	[Images].[Description], 
	[Images].[ImagePath], 
	[Images].[CreateDate], 
	[Images].[IsActive], 
  [Images].[GalleryImageGuid]
FROM	
	[GalleryImages] INNER JOIN [Images] 
	ON [GalleryImages].[Guid] = [Images].[GalleryImageGuid]
WHERE 
	[Images].[IsDeleted] = 0 AND
	[GalleryImages].UserGuid = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Images_GetImagesOfGallery]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Images_GetImagesOfGallery]
	@GalleryImageGuid UNIQUEIDENTIFIER
  AS

SELECT	
	*
FROM	
	[dbo].[Images]
WHERE 
	[GalleryImageGuid] = @GalleryImageGuid AND
	[IsDeleted] = 0


GO
/****** Object:  StoredProcedure [dbo].[Images_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Images_Insert]
	@Guid UNIQUEIDENTIFIER,
	@GalleryImageGuid UNIQUEIDENTIFIER,
	@DataGuid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@Description NVARCHAR(MAX),
	@ImagePath NVARCHAR(MAX),
	@CreateDate DATETIME,
	@IsActive BIT
  AS

INSERT INTO [dbo].[Images]
           ([Guid],
            [GalleryImageGuid],
            [DataGuid],
            [Title],
            [Description],
            [ImagePath],
            [CreateDate],
            [IsActive],
            [IsDeleted])
			 VALUES
						 (@Guid,
							@GalleryImageGuid,
							@DataGuid,
							@Title,
							@Description,
							@ImagePath,
							@CreateDate,
							@IsActive,
							0)


GO
/****** Object:  StoredProcedure [dbo].[Images_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Images_Update]
	@Guid UNIQUEIDENTIFIER ,
	@DataGuid UNIQUEIDENTIFIER ,
	@Title NVARCHAR(50) ,
	@Description NVARCHAR(50) ,
	@ImagePath NVARCHAR(MAX)
  AS


UPDATE  [dbo].[Images]
SET     
	[DataGuid] = @DataGuid,
	[Title] = @Title ,
	[Description] = @Description ,
	[ImagePath] = @ImagePath
WHERE   
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Inboxes_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Inboxes_Delete]
  @Guid UNIQUEIDENTIFIER
  AS
 
  UPDATE
		[dbo].[Inboxes]
	SET
		[IsDeleted] = 1
	WHERE
		[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Inboxes_DeleteMultipleRow]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Inboxes_DeleteMultipleRow]
	@Guids NVARCHAR(MAX)
  AS


EXEC('UPDATE [dbo].[Inboxes]
			SET
				[IsDeleted] = 1
			WHERE
				[Guid] IN ('+ @Guids +')
		')


GO
/****** Object:  StoredProcedure [dbo].[Inboxes_GetChartDetailsAtSpecificDate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE Procedure [dbo].[Inboxes_GetChartDetailsAtSpecificDate]
	@UserGuid UNIQUEIDENTIFIER,
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@PageNo INT,
	@PageSize INT
  AS
	
	DECLARE @Where NVARCHAR(MAX) = ''
	
	IF ( @UserGuid != '00000000-0000-0000-0000-000000000000' )
		BEGIN
      IF ( @Where != '' ) 
          SET @Where += ' AND'
			SET @Where += '[UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
			END
			
	IF ( ISNULL(@FromDateTime, '') != '' ) 
    BEGIN
      IF ( @Where != '' ) 
          SET @Where += ' AND'
      SET @Where += ' CONVERT(DATETIME,[RecieverDateTime] )>'''
          + CAST(CONVERT(DATETIME, @FromDateTime) AS NVARCHAR(19))+''''
    END
    
  IF ( ISNULL(@ToDateTime, '') != '' ) 
    BEGIN
      IF ( @Where != '' ) 
          SET @Where += ' AND'
      SET @Where += ' CONVERT(DATETIME,[RecieverDateTime] )<'''
          + CAST(CONVERT(DATETIME, @ToDateTime) AS NVARCHAR(19))+''''
    END
    
    IF (@Where != '' ) 
	    SET @Where = ' WHERE ' + @Where
--------------------------------------------------------------------------------
exec('SELECT COUNT(*) AS [RowCount] From [Inboxes]' + @Where +';
			
		WITH expTemp AS
		(
			SELECT
					Row_Number() OVER (ORDER BY [RecieverDateTime] ) AS [RowNumber], 
					*
			FROM
					[Inboxes]' +	@Where + '
		)
		
		SELECT 
				*
		FROM
			expTemp
		WHERE 
			(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
			([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
		ORDER BY
			 [RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[Inboxes_GetPagedParserSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Inboxes_GetPagedParserSms]
	@ParserGuid UNIQUEIDENTIFIER,
	@FormulaGuid UNIQUEIDENTIFIER,
	@Lottery INT,
	@Sender NVARCHAR(32),
	@PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE	@StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE @Where NVARCHAR(MAX) = ' WHERE 	[IsDeleted] = 0 AND [ParserFormulaGuid] IN(SELECT [Guid] FROM @Options)';
DECLARE @Statement NVARCHAR(MAX) = '';

IF(ISNULL(@Sender,'') != '')
BEGIN
	SET @Where += ' AND [Sender] LIKE N''%'+ @Sender +'%''';
END

IF(ISNULL(@Lottery,0) != 0)
BEGIN
	SET @Where += ' ORDER BY NEWID()';
END

SET @Statement = '
DECLARE @EmptyGuid UNIQUEIDENTIFIER = ''00000000-0000-0000-0000-000000000000'';
DECLARE @FormulaGuid UNIQUEIDENTIFIER = ''' + CAST(@FormulaGuid AS VARCHAR(36)) + ''';
DECLARE @Lottery INT = ' + CAST(@Lottery AS NVARCHAR(8)) + '
DECLARE @Options TABLE([Guid] UNIQUEIDENTIFIER);
INSERT INTO @Options([Guid]) SELECT [Guid] FROM [dbo].[ParserFormulas] WHERE [SmsParserGuid] = '''+ CAST(@ParserGuid AS VARCHAR(36)) + ''';

IF(@FormulaGuid != @EmptyGuid)
	DELETE FROM @Options WHERE [Guid] != @FormulaGuid;'

IF(@Lottery != 0)
	SET @Statement += 'SELECT TOP (@Lottery) * INTO #Tmp FROM [dbo].[Inboxes]' + @Where;
ELSE
	SET @Statement += 'SELECT * INTO #Tmp FROM [dbo].[Inboxes]' + @Where;

SET @Statement += '
SELECT COUNT(*) [RowCount] FROM #Tmp;
SELECT * FROM #Tmp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Tmp;';

EXEC(@Statement);
GO
/****** Object:  StoredProcedure [dbo].[Inboxes_GetPagedSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Inboxes_GetPagedSmses]
	@UserGuid UNIQUEIDENTIFIER ,
	@InboxGroupGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0'
    
IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where += ' [UserGuid] = '''+CAST(@UserGuid AS VARCHAR(36))+''''
    
IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where += ' [InboxGroupGuid] = '''+ CAST(@InboxGroupGuid AS VARCHAR(36)) +''''
      
IF ( @Where != '' ) 
	SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;
	
--------------------------------------------------
EXEC('
	SELECT 
			COUNT(*) AS [RowCount]
	FROM
			[Inboxes] '+ @Where + ';
	
	WITH expTemp AS
	(
		SELECT
				Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
				*
		FROM
				[Inboxes]'+@Where + '
	)
	SELECT 
			*
	FROM
		expTemp
	WHERE 
		(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
		([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
	ORDER BY
			[RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[Inboxes_GetPagedUserSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Inboxes_GetPagedUserSmses]
	@UserGuid UNIQUEIDENTIFIER ,
	@Query NVARCHAR(MAX),
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0 '
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

IF(@Query != '')
	SET @Where += ' AND ' + @Query;
IF (@Where != '' ) 
	SET @Where = ' WHERE ' + @Where;
--------------------------------------------------
SET @Statement ='

SELECT * INTO #Children FROM dbo.udfGetAllChildren('''+ CAST(@UserGuid AS VARCHAR(36)) +''');

SELECT 
	[UserName],
	inbox.* 
	INTO #temp
FROM
	[Inboxes] inbox WITH(NOLOCK) INNER JOIN
	#Children ON #Children.[UserGuid] = inbox.[UserGuid] '+ @Where +';
		
SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';
DROP TABLE #Temp;
DROP TABLE #Children';

EXEC(@Statement);


GO
/****** Object:  StoredProcedure [dbo].[Inboxes_GetParserSmsReport]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Inboxes_GetParserSmsReport]
	@ParserGuid UNIQUEIDENTIFIER
  AS


DECLARE @Options TABLE([Guid] UNIQUEIDENTIFIER,[Key] NVARCHAR(512));
INSERT INTO @Options([Guid],[Key]) SELECT [Guid],[Key] FROM [dbo].[ParserFormulas] WHERE [SmsParserGuid] = @ParserGuid;

SELECT
	COUNT([Sender]) AS [Count],
	[ParserFormulaGuid]
	INTO #report
FROM 
	[dbo].[Inboxes]
WHERE
	[IsDeleted] = 0 AND
	[ParserFormulaGuid] IN (SELECT [Guid] FROM @Options)
GROUP BY
	[ParserFormulaGuid];

SELECT 
	[Key] ,
	[Count] ,
	[ParserFormulaGuid]
FROM
	@Options opt INNER JOIN 
	#report ON opt.[Guid] = #report.[ParserFormulaGuid]

DROP TABLE #report;

GO
/****** Object:  StoredProcedure [dbo].[Inboxes_InsertReceiveSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Inboxes_InsertReceiveSms]
  @SmsText NVARCHAR(1024) ,
  @Sender NVARCHAR(255) ,
  @ReceiveDateTime DATETIME ,
  @Receiver NVARCHAR(255),
	@Udh INT,
	@InboxGroupGuid UNIQUEIDENTIFIER
  AS


DECLARE @SmsTextPrefix NVARCHAR(32);
DECLARE @Keyword TABLE([UserGuid] UNIQUEIDENTIFIER,[Keyword] NVARCHAR(32),[PrivateNumberGuid] UNIQUEIDENTIFIER);

SET @SmsText = ISNULL(@SmsText,'');

SELECT
		NEWID() [SmsGuid],
		[Guid] [NumberGuid],
		[OwnerGuid],
		[UseForm],
		[Number],
		CASE WHEN [dbo].[IsMatch](@Receiver,[Regex]) = 1 THEN 1
		ELSE 0 
		END [IsMatch],
		[SmsTrafficRelayGuid]
		INTO #Numbers
FROM
		[dbo].[PrivateNumbers]
WHERE
		[IsDeleted] = 0;

PRINT LEN(@SmsText);
IF(LEN(@SmsText) > 0)
BEGIN
	IF(CHARINDEX (' ' ,@SmsText) > 0)
		SET @SmsTextPrefix = SUBSTRING(@SmsText,1,CHARINDEX (' ' ,@SmsText)-1);
	ELSE
		SET @SmsTextPrefix = @SmsText;

	INSERT INTO @Keyword
					(UserGuid ,
					 Keyword ,
					 PrivateNumberGuid)
	SELECT [UserGuid],[Keyword],[PrivateNumberGuid] FROM [dbo].[ReceiveKeywords] WHERE [PrivateNumberGuid] IN (SELECT [NumberGuid] FROM #Numbers WHERE [IsMatch] = 1)

	DELETE FROM @Keyword WHERE [Keyword] != @SmsTextPrefix;
END 

INSERT INTO [dbo].[Inboxes]
	       ([Guid] ,
	        [Receiver] ,
	        [Sender] ,
	        [SmsLen] ,
	        [SmsText] ,
	        [ReceiveDateTime] ,
	        [IsUnicode] ,
	        [IsRead] ,
	        [ShowAlert] ,
	        [Udh] ,
	        [IsDeleted] ,
	        [UserGuid] ,
	        [PrivateNumberGuid] ,
	        [InboxGroupGuid])
			SELECT
					[SmsGuid],
					@Receiver,
					@Sender,
					[dbo].[GetSmsCount](@SmsText),
					@SmsText,
					GETDATE(),
					[dbo].[HasUniCodeCharacter](@SmsText),
					0,
					0,
					@Udh,
					0,
					[OwnerGuid],
					[NumberGuid],
					@InboxGroupGuid
			FROM 
					#Numbers
			WHERE [IsMatch] = 1;

INSERT INTO [dbo].[Inboxes]
	 ([Guid] ,
	  [Receiver] ,
	  [Sender] ,
	  [SmsLen] ,
	  [SmsText] ,
	  [ReceiveDateTime] ,
	  [IsUnicode] ,
	  [IsRead] ,
	  [ShowAlert] ,
	  [Udh] ,
	  [IsDeleted] ,
	  [UserGuid] ,
	  [PrivateNumberGuid] ,
	  [InboxGroupGuid])
SELECT
		NEWID(),
		@Receiver,
		@Sender,
		[dbo].[GetSmsCount](@SmsText),
		@SmsText,
		GETDATE(),
		[dbo].[HasUniCodeCharacter](@SmsText),
		0,
		0,
		@Udh,
		0,
		[UserGuid],
		[PrivateNumberGuid],
		@InboxGroupGuid
FROM 
		@Keyword;

SELECT
		[SmsGuid],
		[NumberGuid] ,
		[OwnerGuid],
		[Number],
		[SmsTrafficRelayGuid]
FROM
		#Numbers
WHERE
		[IsMatch] = 1 AND
		(
			[UseForm] = 0 OR
			([UseForm] = 2 AND ISNULL([Number],'') != '')
		)

DROP TABLE #Numbers;




GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InboxGroups_Delete]
	@Guid UNIQUEIDENTIFIER
  AS

UPDATE	
	[InboxGroups]
SET
	[IsDeleted] = 1
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_GetInboxGroups]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InboxGroups_GetInboxGroups]
  @UserGuid UNIQUEIDENTIFIER
  AS
 
 	
SELECT
	[Guid],
  [Title]
FROM
	[dbo].[InboxGroups]
WHERE 
	[IsDeleted] = 0 AND
	[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_GetUserInboxGroups]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InboxGroups_GetUserInboxGroups]
  @UserGuid UNIQUEIDENTIFIER,
	@ParentNodeGuid UNIQUEIDENTIFIER,
	@Name NVARCHAR(50)
  AS
 
 	DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0';

	IF ( ISNULL(@Name, '') != '' ) 
  BEGIN
    IF ( @Where != '' ) 
			SET @Where += ' AND'
    SET @Where += ' [Title] LIKE N''%' + @Name + '%'''
  END

	IF ( LEN(@Name) = 0 )
	BEGIN
		IF ( @Where != '' )
			SET @Where += ' AND'
		SET @Where += ' [ParentGuid]=''' + CAST(@ParentNodeGuid AS VARCHAR(36)) + ''''
	END

	IF ( @Where != '' )
		SET @Where += ' AND'
	SET @Where += ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''

	IF (@Where != '' ) 
		SET @Where = ' WHERE ' + @Where;

	EXEC('
     SELECT
			[Guid] ,
      [Title] ,
      [CreateDate] ,
      [ParentGuid] ,
      [UserGuid] ,
      [IsDeleted]
    FROM
			[InboxGroups]'+@Where);


GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InboxGroups_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@CreateDate DATETIME,
	@ParentGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS

	INSERT INTO [InboxGroups]
         ([Guid]
         ,[Title]
         ,[CreateDate]
         ,[ParentGuid]
         ,[UserGuid]
         ,[IsDeleted])
	VALUES
         (@Guid
         ,@Title
         ,@CreateDate
         ,@ParentGuid
         ,@UserGuid
         ,0)


GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InboxGroups_Update]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@CreateDate DATETIME,
	@ParentGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS

UPDATE	[InboxGroups] 
SET			[Title] = @Title,
				[CreateDate] = @CreateDate,
				[ParentGuid] = @ParentGuid,
				[UserGuid] = @UserGuid
WHERE
				[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_UpdateName]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[InboxGroups_UpdateName]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50)
  AS

	UPDATE 	
		[InboxGroups]
	SET
		[Title]=@Title
	WHERE
		[Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[InboxGroups_UpdateParent]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[InboxGroups_UpdateParent]
	@Guid UNIQUEIDENTIFIER,
	@ParentGuid UNIQUEIDENTIFIER
  AS

	UPDATE
		[dbo].[InboxGroups]
	SET
		[ParentGuid]=@ParentGuid
	WHERE 
		[Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[LoginStats_GetUserLoginStats]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[LoginStats_GetUserLoginStats]
	@UserGuid UNIQUEIDENTIFIER,
	@PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS

	DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;

EXEC('
		SELECT * FROM	[LoginStats] WHERE [UserGuid]=''' +	@UserGuid + '''
		ORDER BY '+ @SortField +'
		OFFSET '+ @StartRow +' ROWS FETCH NEXT '+ @PageSize +'ROWS ONLY;

		SELECT @@ROWCOUNT [RowCount];');


GO
/****** Object:  StoredProcedure [dbo].[LoginStats_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[LoginStats_Insert]
	@Guid UNIQUEIDENTIFIER,
	@IP NVARCHAR(50),
	@Type INT,
	@CreateDate DATETIME,
	@UserGuid UNIQUEIDENTIFIER
  AS

	INSERT INTO [dbo].[LoginStats]
						 ([Guid],
							[IP],
							[Type],
							[CreateDate],
							[UserGuid])
				 VALUES
							 (@Guid, 
								@IP,
								@Type,
								@CreateDate, 
								@UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[Outboxes_ArchiveNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_ArchiveNumbers]
	@OutboxGuid UNIQUEIDENTIFIER
  AS


DECLARE @DeliveredCount INT = 0;
DECLARE @FailedCount INT = 0;
DECLARE @SentToICTCount INT = 0 ;
DECLARE @DeliveredICTCount INT = 0;
DECLARE @BlackListCount INT = 0;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION 
	
	SELECT
		COUNT([DeliveryStatus]) AS [Count],
		[DeliveryStatus]
		INTO #report
	FROM
		[dbo].[OutboxNumbers]
	WHERE
		[OutboxGuid] = @OutboxGuid
	GROUP BY [DeliveryStatus];

	SELECT @DeliveredCount = [Count] FROM #report WHERE [DeliveryStatus] = 1;
	SELECT @FailedCount = [Count] FROM #report WHERE [DeliveryStatus] = 10;
	SELECT @SentToICTCount = [Count] FROM #report WHERE [DeliveryStatus] = 3;
	SELECT @DeliveredICTCount = [Count] FROM #report WHERE [DeliveryStatus] = 4;
	SELECT @BlackListCount = [Count] FROM #report WHERE [DeliveryStatus] = 14;
	SELECT @BlackListCount += [Count] FROM #report WHERE [DeliveryStatus] = 30;

	UPDATE [dbo].[Outboxes] SET
		[DeliveredCount] = @DeliveredCount,
		[FailedCount] = @FailedCount,
		[SentToICTCount] = @SentToICTCount,
		[DeliveredICTCount] = @DeliveredICTCount,
		[BlackListCount] = @BlackListCount,
		[SendStatus] = 11, --Archiving
		[ExportDataStatus] = 2, --Get
		[ExportDataPageNo] = 0
	WHERE
		[Guid] = @OutboxGuid;
	
	DROP TABLE #report;

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	 IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	 IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'Outboxes',
			@Name = 'Archive Numbers' ,
			@Text = @Text,
			@Ip = '',
			@Browser = '',
			@ReferenceGuid = @OutboxGuid,
			@UserGuid = @EmptyGuid;
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_CheckReceiverCount]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_CheckReceiverCount]
	@OutboxGuid UNIQUEIDENTIFIER
  AS

DECLARE @SavedReceiverCount INT;
DECLARE @ReceiverCount INT;

SELECT @SavedReceiverCount = COUNT([ToNumber]) FROM [OutboxNumbers] WHERE [OutboxGuid] = @OutboxGuid;
SELECT @ReceiverCount = [ReceiverCount] FROM [dbo].[Outboxes] WHERE [Guid] = @OutboxGuid;

IF(@ReceiverCount = @SavedReceiverCount)
BEGIN
	UPDATE [dbo].[Outboxes]
	SET [SendStatus] = 4
	WHERE [Guid] = @OutboxGuid;
END
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_DecreaseSmsSendPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_DecreaseSmsSendPrice]
	@ScheduledGuid UNIQUEIDENTIFIER
  AS

DECLARE @Guid UNIQUEIDENTIFIER,
				@UserGuid UNIQUEIDENTIFIER,
				@SmsLen INT,
				@SmsType INT,
				@GroupPriceGuid UNIQUEIDENTIFIER,
				@PrivateNumberGuid UNIQUEIDENTIFIER,
				@AgentRatio XML,
				@SmsSenderAgentGuid UNIQUEIDENTIFIER,
				@GroupPriceRatio XML,
				@Ratio DECIMAL(18,2) = 1,
				@NumberType INT,
				@UserCredit DECIMAL(18,2),
				@RecipientCount INT,
				@TypeSend INT,
				@XmlRequest XML;

SET NOCOUNT ON;

BEGIN TRY

SELECT 
		@UserGuid = [UserGuid],
		@SmsLen = [SmsLen],
		@SmsType = CASE
									WHEN [Encoding] = 2 THEN 1
									ELSE 2
							 END,
		@PrivateNumberGuid = [PrivateNumberGuid],
		@TypeSend = [TypeSend]--,
		--@XmlRequest = [RequestXML]
FROM 
		[dbo].[ScheduledSmses]
WHERE
		[Guid] = @ScheduledGuid;
--------------------------------------------------------------------------
SELECT
		@SmsSenderAgentGuid = [SmsSenderAgentGuid],
		@NumberType = [Type]
FROM
		[dbo].[PrivateNumbers]
WHERE
		[Guid] = @PrivateNumberGuid;

--------------------------------------------------------------------------
IF(@NumberType != 0)--0 IS Number Bulk Type
 SET @SmsLen = 1;

--------------------------------------------------------------------------
SELECT
		@GroupPriceRatio = groups.[AgentRatio]
FROM
		[dbo].[Users] users INNER JOIN 
		[GroupPrices] groups ON users.[PriceGroupGuid] = groups.[Guid]
WHERE 
		users.[Guid] = @UserGuid;

--------------------------------------------------------------------------
SELECT
	[SmsType] ,
	[Ratio] ,
	[OperatorID] AS [Operator]
	INTO #AgentRatio
FROM
	[dbo].[AgentRatio]
WHERE
	[SmsSenderAgentGuid] = @SmsSenderAgentGuid AND
	[SmsType] = @SmsType;

--------------------------------------------------------------------------
SELECT  
		--Tbl.Col.value('AgentID[1]', 'UNIQUEIDENTIFIER') [AgentGuid],  
		@Ratio = ISNULL(Tbl.Col.value('Ratio[1]', 'decimal(18,2)'),1)
		--INTO #GroupPriceRatio
FROM
		@GroupPriceRatio.nodes('//NewDataSet/Table') Tbl(Col)
WHERE
		Tbl.Col.value('AgentID[1]', 'UNIQUEIDENTIFIER') = @SmsSenderAgentGuid;

--------------------------------------------------------------------------
--SET @Ratio = (SELECT [Ratio] FROM #GroupPriceRatio);

CREATE TABLE #OutboxSmsInfo
	([ID] INT IDENTITY (1, 1) Primary key NOT NULL ,
	 [Operator] INT,
	 [Count] INT,
	 [AgentRatio] DECIMAL(18,2) DEFAULT 1,
	 [GroupRatio] DECIMAL(18,2),
	 [SmsPartCount] INT,
	 [SendPice] DECIMAL(18,2));

IF(@TypeSend != 7)--not bulk
BEGIN
	INSERT INTO #OutboxSmsInfo
					([Operator] ,
					 [Count],
					 [GroupRatio],
					 [SmsPartCount])
				 SELECT
					 [Operator],
					 COUNT(*),
					 @Ratio,
					 @SmsLen
				 FROM
					 [dbo].[Recipients] WITH (NOLOCK)
				 WHERE
					 [ScheduledSmsGuid] = @ScheduledGuid GROUP BY [Operator];
END
ELSE
BEGIN
	INSERT INTO #OutboxSmsInfo
			([Operator] ,
			 [Count],
			 [GroupRatio],
			 [SmsPartCount])
		 SELECT
			 --Tbl.Col.value('Operator[1]', 'INT'),
			 --Tbl.Col.value('Count[1]', 'INT'),
			 [Operator],
			 [Count],
			 @Ratio,
			 @SmsLen
		FROM
			 [dbo].[BulkRecipient]
		WHERE
			 [ScheduledBulkSmsGuid] = @ScheduledGuid;
				--@XmlRequest.nodes('//NewDataSet/Table') Tbl(Col);
END

IF((SELECT COUNT(*) FROM #OutboxSmsInfo))=0
 THROW 61000,'No receiver found',1;

UPDATE #OutboxSmsInfo
SET	[AgentRatio] = (SELECT [Ratio] FROM #AgentRatio WHERE [Operator] = #OutboxSmsInfo.[Operator]);

--UPDATE	#OutboxSmsInfo
--SET			[AgentRatio] = t2.[Ratio]
--FROM		#OutboxSmsInfo t1 INNER JOIN 
--				#AgentRatio t2 ON t1.Operator = t2.Operator

UPDATE #OutboxSmsInfo
SET [SendPice] = [Count] * ISNULL([AgentRatio],1) * ISNULL([GroupRatio],1) * [SmsPartCount];
 
DECLARE @SendPrice DECIMAL(18,2);
SELECT @SendPrice = SUM([SendPice]) FROM #OutboxSmsInfo;

SELECT @UserCredit = [Credit] FROM [dbo].[Users] WHERE [Guid] = @UserGuid;

SELECT @RecipientCount = SUM([Count]) FROM #OutboxSmsInfo;

IF(ISNULL(@UserCredit,0) < @SendPrice)
	THROW 60000,'Lack of credit',1;

DECLARE @Description NVARCHAR(MAX) = N'Decrease credit due to sending SMS to  '+ CAST(@RecipientCount AS NVARCHAR) + N' recipient  ';
	
INSERT INTO [dbo].[Transactions]
					([Guid] ,
	        [ReferenceGuid] ,
	        [TypeTransaction] ,
	        [TypeCreditChange] ,
	        [Description] ,
	        [CreateDate] ,
	        [CurrentCredit] ,
	        [Amount] ,
	        [UserGuid])
				VALUES 
					(NEWID(),
					@ScheduledGuid,
					2,--Decrease
					32,--SendSms
					@Description,
					GETDATE(),
					@UserCredit,
					@SendPrice,
					@UserGuid);

UPDATE [dbo].[Users] SET [Credit] -= @SendPrice WHERE [Guid] = @UserGuid;

EXEC [dbo].[UserSettings_CheckCreditNotification] @UserGuid = @UserGuid;

IF(@TypeSend = 7)--bulk
	UPDATE [dbo].[ScheduledBulkSmses] SET [Price] = @SendPrice,[ReceiverCount] = @RecipientCount WHERE [Guid] = @ScheduledGuid;
ELSE
	UPDATE [dbo].[Outboxes] SET [Price] = @SendPrice,[ReceiverCount] = @RecipientCount WHERE [Guid] = @ScheduledGuid;

DROP TABLE #AgentRatio;
--DROP TABLE #GroupPriceRatio;
DROP TABLE #OutboxSmsInfo;

EXEC [dbo].[InsertLog]
	@Type = 3, --Action
	@Source = 'Decrease Sms Send Price',
	@Name = 'Transaction' ,
	@Text = N'Decrease Amount For Outbox Pack' ,
	@IP = '',
	@Browser = '',
	@ReferenceGuid = @ScheduledGuid,
	@UserGuid = @UserGuid;

END TRY
BEGIN CATCH
	THROW;
END CATCH;

GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetExportDataRequest]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE	PROCEDURE [dbo].[Outboxes_GetExportDataRequest]
	@RecordCount INT
  AS


SELECT
	TOP (@RecordCount)
	[Guid],
	[ID],
	ISNULL([ExportDataPageNo],1) [PageNo],
	[ExportDataStatus] [Status],
	ISNULL([ExportTxtPageNo],1) [TxtPageNo],
	[ExportTxtStatus] [TxtStatus],
	[SendStatus]		 
FROM
	[dbo].[Outboxes]
WHERE
	[ExportDataStatus] = 2 OR --Get
	[ExportTxtStatus] = 2

GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetOutboxForGiveBackCredit]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GetOutboxForGiveBackCredit]
  AS

 
SELECT
	TOP 1000
	[Guid] ,
	[ID],
	CONVERT(DATE,[SentDateTime]) [SendDate],
	[SendStatus]
FROM
	[dbo].[Outboxes]
WHERE
	(
		CONVERT(DATE,[SentDateTime]) < CONVERT(DATE,GETDATE()) AND
		[SendStatus] IN (2,3,4,5) --Stored,IsBeingSent,Sent,SentAndGiveBackCredit
	)
		OR
	(
		CONVERT(DATE,[SentDateTime]) < CONVERT(DATE,GETDATE()-3) AND
		[SendStatus] = 11 AND --Archiving
		[ExportDataStatus] = 4 --Archived
	)

ORDER BY
	[SentDateTime] DESC
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetOutboxReport]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Outboxes_GetOutboxReport]
	@OutboxGuid UNIQUEIDENTIFIER
  AS

SELECT
	[ReceiverCount],
	[DeliveredCount],
	[FailedCount],
	[SentToICTCount],
	[DeliveredICTCount],
	[BlackListCount],
	[SmsLen] * [DeliveredCount] AS [DeliveredSmsCount],
	[SmsLen] * [FailedCount] AS [FailedSmsCount],
	[SmsLen] * [SentToICTCount] AS [SentToICTSmsCount],
	[SmsLen] * [DeliveredICTCount] AS [ReceivedByItcSmsCount],
	[SmsLen] * [BlackListCount] AS [BlackListSmsCount]
FROM 
	[dbo].[Outboxes]
WHERE
	[Guid] = @OutboxGuid;
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetPagedExportData]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GetPagedExportData]
	@Guid UNIQUEIDENTIFIER,
	@PageNo INT,
	@PageSize INT
  AS

DECLARE @IsArchived BIT;
DECLARE @SmsText NVARCHAR(512);
DECLARE @Sender NVARCHAR(32);
DECLARE @SendDate DATETIME;
DECLARE @SendType INT;

SELECT
	@SmsText = [SmsText],
	@Sender = [SenderId],
	@SendDate = [SentDateTime],
	@SendType = [SmsSendType],
	@IsArchived = CASE WHEN [SendStatus] = 6 THEN 1
										 ELSE 0
								END
FROM
	[dbo].[Outboxes]
WHERE
	[Guid] = @Guid;

SELECT
	[ToNumber] [Receiver],
	[DeliveryStatus] [Delivery] ,
	[ReturnId] [Id] ,
	@SmsText [SmsText],
	@Sender [Sender],
	@SendDate [SendDate]
FROM
	[dbo].[OutboxNumbers]
WHERE
	[OutboxGuid] = @Guid
ORDER BY [Guid]
OFFSET ((@PageNo - 1) * @PageSize) Rows
FETCH NEXT @PageSize ROWS ONLY;
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetPagedExportText]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GetPagedExportText]
	@Guid UNIQUEIDENTIFIER,
	@PageNo INT,
	@PageSize INT
  AS

SELECT
	[ToNumber] [Receiver]
FROM
	[dbo].[OutboxNumbers]
WHERE
	[OutboxGuid] = @Guid AND
	[DeliveryStatus] NOT IN (14,30) --black list
ORDER BY [Guid]
OFFSET ((@PageNo - 1) * @PageSize) Rows
FETCH NEXT @PageSize ROWS ONLY;
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetPagedSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GetPagedSmses]
  @UserGuid UNIQUEIDENTIFIER ,
	@ReferenceGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @Where AS NVARCHAR(MAX) ='';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

SET @Where = ' [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + '''';

IF(@ReferenceGuid != '00000000-0000-0000-0000-000000000000')
BEGIN
	SET @Where = ' [Guid] =''' + CAST(@ReferenceGuid AS varchar(36)) + '''';
END

IF ( @Where != '' ) 
	SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

--------------------------------------------------
SET @Statement ='
  SELECT * INTO #Temp FROM [Outboxes] WITH(NOLOCK) ' +	@Where + ';
	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';
	
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';

SELECT SUM([ReceiverCount]) [TotalReceiverCount],SUM([Price]) [TotalPrice] FROM #Temp;
DROP TABLE #Temp;';

EXEC(@Statement);

GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetPagedUserManualSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GetPagedUserManualSmses]
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[SmsSenderAgentReference] = 10 AND 
								CONVERT(DATE,[SentDateTime]) <= CONVERT(DATE,''2017-03-14'') AND 
								[DomainGuid] NOT IN(''12C74AFC-E385-4C6D-BDB4-AFDDDBB71508'',''E1D9DB43-BA81-426B-88C5-327F00736156'')
								' ;
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

IF(@Query != '')
	SET @Where += ' AND '+ @Query;
IF (@Where != '' ) 
	SET @Where = ' WHERE ' + @Where;

SET @Statement ='
  SELECT 
		outbox.*,
		[UserName]
		INTO #Temp
	FROM
		[dbo].[Outboxes] outbox WITH(NOLOCK) INNER JOIN
		[dbo].[Users] users ON users.[Guid] = outbox.[UserGuid] '+ @Where +';

	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';
	
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';
SELECT SUM([ReceiverCount]) [TotalReceiverCount],SUM([Price]) [TotalPrice] FROM #Temp;
DROP TABLE #Temp';

EXEC(@Statement);

GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetPagedUserSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GetPagedUserSmses]
  @UserGuid UNIQUEIDENTIFIER ,
	@DomainGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

IF(@Query != '')
	SET @Where = @Query;
IF (@Where != '' ) 
	SET @Where = ' WHERE ' + @Where;

CREATE TABLE #Children	(
 [UserGuid] UNIQUEIDENTIFIER NULL,
 [Username] [nvarchar](32) NULL	
) 

IF(@DomainGuid = '00000000-0000-0000-0000-000000000000')
BEGIN
	INSERT INTO #Children([UserGuid],[Username])
	SELECT [UserGuid],[Username] FROM dbo.udfGetAllChildren(@UserGuid);
END
ELSE
BEGIN
	INSERT INTO #Children([UserGuid],[Username])
	SELECT [Guid],[UserName] FROM [dbo].[Users] WHERE [DomainGuid] = @DomainGuid AND [IsDeleted] = 0;
END

SET @Statement ='
SELECT 
	[Username],
	outbox.*,
	([SmsLen] * [DeliveredCount]) [SmsDeliveredCount],
	([SmsLen] * [FailedCount]) [SmsFailedCount],
	([SmsLen] * [SentToICTCount]) [SmsSentToICTCount],
	([SmsLen] * [DeliveredICTCount]) [SmsDeliveredICTCount],
	([SmsLen] * [BlackListCount]) [SmsBlackListCount]
	INTO #temp
FROM
	[Outboxes] outbox WITH(NOLOCK) INNER JOIN
	#Children ON #Children.[UserGuid] = outbox.[UserGuid] '+ @Where +';
		
SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';

	SELECT
		SUM([ReceiverCount]) [TotalReceiverCount],
		SUM([Price]) [TotalPrice],
		SUM([DeliveredCount]) [TotalDeliveredCount],
		SUM([SmsDeliveredCount]) [TotalSmsDeliveredCount],
		SUM([FailedCount]) [TotalFailedCount],
		SUM([SmsFailedCount]) [TotalSmsFailedCount],
		SUM([SentToICTCount]) [TotalSentToICTCount],
		SUM([SmsSentToICTCount]) [TotalSmsSentToICTCount],
		SUM([DeliveredICTCount]) [TotalDeliveredICTCount],
		SUM([SmsDeliveredICTCount]) [TotalSmsDeliveredICTCount],
		SUM([BlackListCount]) [TotalBlackListCount],
		SUM([SmsBlackListCount]) [TotalSmsBlackListCount]
	FROM
		#Temp;

	DROP TABLE #Temp;
	DROP TABLE #Children';

EXECUTE SP_EXECUTESQL @Statement;


GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GetSendQueue]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Outboxes_GetSendQueue]
	@SmsSenderAgentRefrence INT,
	@Count INT,
	@ThreadCount INT
  AS
 

SELECT TOP (@Count * @ThreadCount)
			outbox.[Guid] AS [OutboxGuid],
      outbox.[PrivateNumberGuid],
			outbox.[SenderId] AS [SenderNumber],
      outbox.[IsUnicode],
			outbox.[IsFlash],
			outbox.[SmsText],
			outbox.[SmsLen],
			outbox.[SmsSendType],
			number.[Guid] AS [NumberGuid],
			number.[ToNumber],
			number.[CheckId],
			number.[ReturnId],
			number.[SendStatus]
FROM
			[dbo].[Outboxes] outbox with (nolock) INNER JOIN
			[dbo].[OutboxNumbers] number with (nolock) ON outbox.[Guid] = number.[OutboxGuid] 
WHERE 
			outbox.[SmsSenderAgentReference] = @SmsSenderAgentRefrence AND
			number.Operator > 0 AND --0 is invalid operator
			number.[SendStatus] IN (2,5,6) AND --WatingForSend and ErrorInSending and ErrorInGetItc
			ISNULL(outbox.[SenderId],'') != '' AND
			outbox.[SendStatus] = 2 --WatingForSend
ORDER BY
			[SmsPriority] DESC,
			[SmsSendType] ASC


GO
/****** Object:  StoredProcedure [dbo].[Outboxes_GiveBackBlackListAndFailedSend]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_GiveBackBlackListAndFailedSend]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE	@UserGuid UNIQUEIDENTIFIER;
DECLARE	@SmsLen INT;
DECLARE	@SmsType INT;
DECLARE	@GroupPriceGuid UNIQUEIDENTIFIER;
DECLARE	@PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE	@AgentRatio XML;
DECLARE	@SmsSenderAgentGuid UNIQUEIDENTIFIER;
DECLARE	@GroupPriceRatio XML;
DECLARE	@Ratio DECIMAL(18,2) = 1;
DECLARE	@NumberType INT;
DECLARE	@UserCredit DECIMAL(18,2);
DECLARE	@SendStatus INT;
DECLARE @trancount INT;
DECLARE @ReturnBlackList BIT;

SET XACT_ABORT ON;
SET NOCOUNT ON;

BEGIN TRY
 BEGIN TRANSACTION;
	SELECT
		@SendStatus = [SendStatus],
		@Guid = [Guid],
		@UserGuid = [UserGuid],
		@SmsLen = [SmsLen],
		@SmsType = CASE
									WHEN [IsUnicode] = 1 THEN 1
									ELSE 2
								END,
		@PrivateNumberGuid = [PrivateNumberGuid]
	FROM
		[dbo].[Outboxes] WITH (NOLOCK)
	WHERE
		[Guid] = @Guid;
	--------------------------------------------------------------------------
	SELECT
			@SmsSenderAgentGuid = agent.[Guid],
			@NumberType = numbers.[Type],
			@ReturnBlackList = [ReturnBlackList]
	FROM
			[dbo].[PrivateNumbers] numbers WITH (NOLOCK) INNER JOIN 
			[dbo].[SmsSenderAgents] agent WITH (NOLOCK) ON numbers.[SmsSenderAgentGuid] = agent.[Guid]
	WHERE
			numbers.[Guid] = @PrivateNumberGuid;

	--------------------------------------------------------------------------
	IF(@ReturnBlackList = 1)
	BEGIN
		IF(@NumberType != 0)--0 IS Bulk Type
			SET @SmsLen = 1;
	--------------------------------------------------------------------------
		SELECT
				@GroupPriceRatio = groups.[AgentRatio]
		FROM
				[dbo].[Users] users WITH (NOLOCK) INNER JOIN 
				[GroupPrices] groups WITH (NOLOCK) ON users.[PriceGroupGuid] = groups.[Guid]
		WHERE 
				users.[Guid] = @UserGuid;
	--------------------------------------------------------------------------
		SELECT
			[SmsType] ,
			[Ratio] ,
			[OperatorID] AS [Operator]
			INTO #AgentRatio
		FROM
			[dbo].[AgentRatio] WITH (NOLOCK)
		WHERE
			[SmsSenderAgentGuid] = @SmsSenderAgentGuid AND
			[SmsType] = @SmsType;
	--------------------------------------------------------------------------
		SELECT  
				Tbl.Col.value('AgentID[1]', 'UNIQUEIDENTIFIER') [AgentGuid],  
				Tbl.Col.value('Ratio[1]', 'decimal(18,2)') [Ratio]
				INTO #GroupPriceRatio
		FROM
				@GroupPriceRatio.nodes('//NewDataSet/Table') Tbl(Col)
		WHERE
				Tbl.Col.value('AgentID[1]', 'UNIQUEIDENTIFIER') = @SmsSenderAgentGuid;
	--------------------------------------------------------------------------
		SET @Ratio = (SELECT [Ratio] FROM #GroupPriceRatio);

		CREATE TABLE #SmsInfo
		([ID] INT IDENTITY (1, 1) Primary key NOT NULL ,
		 [Operator] INT,
		 [Count] INT,
		 [AgentRatio] DECIMAL(18,2) DEFAULT 1,
		 [GroupRatio] DECIMAL(18,2),
		 [SmsPartCount] INT,
		 [SendPice] DECIMAL(18,2));

		INSERT INTO #SmsInfo
					([Operator] ,
					 [Count],
					 [GroupRatio],
					 [SmsPartCount])
				 SELECT
					 [Operator],
					 COUNT(*),
					 @Ratio,
					 @SmsLen
				 FROM
					 [dbo].[OutboxNumbers]
				 WHERE
					 [OutboxGuid] = @Guid AND
					 [DeliveryStatus] IN (14,30,10,12) --BlackList & NotSend && IsSending
				 GROUP BY [Operator];

		IF(SELECT COUNT(*) FROM #SmsInfo)!= 0
		BEGIN
			UPDATE #SmsInfo
				SET	[AgentRatio] = (SELECT [Ratio] FROM #AgentRatio WHERE [Operator] = #SmsInfo.[Operator]);

			UPDATE #SmsInfo
				SET [SendPice] = [Count] * ISNULL([AgentRatio],1) * ISNULL([GroupRatio],1) * [SmsPartCount];
 
			DECLARE @SendPrice DECIMAL(18,2);
			SELECT @SendPrice = SUM([SendPice]) FROM #SmsInfo;

			IF(@SendPrice>0)
			BEGIN
				DECLARE @Description NVARCHAR(MAX) = N'افزایش اعتبار به دلیل بازگشت '+ cast((SELECT SUM([Count]) FROM #SmsInfo) as nvarchar) + N'گیرنده لیست سیاه و ارسالهای انجام نشده ';
				SELECT @UserCredit = [Credit] FROM [dbo].[Users] WHERE [Guid] = @UserGuid;

				INSERT INTO [dbo].[Transactions]
								 ([Guid] ,
									[ReferenceGuid] ,
									[TypeTransaction] ,
									[TypeCreditChange] ,
									[Description] ,
									[CreateDate] ,
									[CurrentCredit] ,
									[Amount] ,
									[UserGuid])
							 VALUES 
								 (NEWID(),
									@Guid,
									1,--Increase
									23,--GiveBackCostOfUnsuccessfulSent
									@Description,
									GETDATE(),
									@UserCredit,
									@SendPrice,
									@UserGuid);

				UPDATE [dbo].[Users] SET [Credit] += @SendPrice WHERE [Guid] = @UserGuid;
			END
		END
	END
  
	UPDATE [dbo].[Outboxes] SET [SendStatus] = 5--SentAndGiveBackCredit
	WHERE [Guid] = @Guid;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	IF(XACT_STATE() = -1)
	ROLLBACK TRANSACTION;

	IF(XACT_STATE() = 1)
	COMMIT TRANSACTION;

	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	EXEC [dbo].[InsertLog]
			@Type = 2, -- Error
	    @Source = N'Give Back Credit',
	    @Name = N'GiveBackBlackListAndFailedSend',
	    @Text = @Text,
	    @Ip = '',
	    @Browser = '',
	    @ReferenceGuid = @Guid,
	    @UserGuid = @UserGuid;
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_ResendFailedSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_ResendFailedSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE @IsArchived BIT;

SELECT @IsArchived = CASE WHEN [SendStatus] = 6 THEN 1
										 ELSE 0
										 END
FROM [dbo].[Outboxes] WHERE [Guid] = @Guid;

DECLARE @Receivers TABLE([Mobile] NVARCHAR(16),[Operator] TINYINT);

INSERT INTO @Receivers([Mobile],[Operator])
SELECT [ToNumber],[Operator] FROM [dbo].[OutboxNumbers] WHERE [OutboxGuid] = @Guid AND [DeliveryStatus] = 10;
 
IF(@@ROWCOUNT = 0)
	RETURN;

SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION;
		DECLARE @SchGuid UNIQUEIDENTIFIER = NEWID();
		INSERT INTO [dbo].[ScheduledSmses]
		        ([Guid] ,
		         [PrivateNumberGuid] ,
		         [SmsText] ,
		         [PresentType] ,
		         [Encoding] ,
		         [SmsLen] ,
		         [TypeSend] ,
						 [Status] ,
		         [DateTimeFuture] ,
		         [CreateDate] ,
		         [SmsSenderAgentReference] ,
		         [IsDeleted] ,
		         [UserGuid])
					SELECT 
							@SchGuid,
							[PrivateNumberGuid],
							[SmsText],
							1,--Normal
							CASE WHEN [IsUnicode] = 1 THEN 2--Utf8
									 ELSE 1--default
							END [Encoding],
							[SmsLen],
							1,--SendSms
							1,--Stored
							GETDATE(),
							GETDATE(),
							dbo.[GetPrivateNumberAgentReference]([PrivateNumberGuid]),
							0,
							[UserGuid]
					FROM [dbo].[Outboxes] WHERE [Guid] = @Guid;
		INSERT INTO [dbo].[Recipients]
		        ([Guid] ,
		         [Mobile] ,
		         [Operator] ,
		         [IsBlackList] ,
		         [ScheduledSmsGuid])
						SELECT
							NEWID(),
							[Mobile],
							[Operator],
							0,
							@SchGuid
						FROM 
							@Receivers;

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	EXEC [dbo].[InsertLog]
		@Type = 2, --Error
		@Source = 'outboxes',
		@Name = 'resend failed sms' ,
		@Text = @Text,
		@IP = '',
		@Browser = '',
		@ReferenceGuid = @Guid,
		@UserGuid = '';

	ROLLBACK TRANSACTION;
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[Outboxes_UpdateExportDataRequest]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_UpdateExportDataRequest]
  @Requests [UpdateExportDataRequest] READONLY
  AS


UPDATE [dbo].[Outboxes]
SET
	[ExportDataPageNo] = t2.[PageNo],
	[ExportDataStatus] = t2.[Status],
	[ExportTxtPageNo] = t2.[TxtPageNo],
	[ExportTxtStatus] = t2.[TxtStatus],
	[SendStatus] = t2.[SendStatus]
FROM
	[dbo].[Outboxes] t1 INNER JOIN
	@Requests t2 ON  t1.[Guid] = t2.[Guid]


GO
/****** Object:  StoredProcedure [dbo].[Outboxes_UpdateStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Outboxes_UpdateStatus]
	@Guid UNIQUEIDENTIFIER,
	@Id BIGINT,
	@Status INT
  AS


IF(@Id = 0)
 UPDATE	[Outboxes] SET [SendStatus] = @Status WHERE	[Guid] = @Guid;
ELSE
 UPDATE [Outboxes] SET [SendStatus] = @Status WHERE [ID] = @Id;
GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_DeleteNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_DeleteNumbers]
	@OutboxGuid UNIQUEIDENTIFIER
  AS


DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SET XACT_ABORT ON ;

BEGIN TRY
	BEGIN	TRANSACTION;

	DELETE FROM [dbo].[OutboxNumbers] WHERE [OutboxGuid] = @OutboxGuid;
	UPDATE [dbo].[Outboxes] SET [SendStatus] = 6 WHERE [Guid] = @OutboxGuid;--Archived

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH

	IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	EXEC [dbo].[InsertLog]
		@Type = 2, --Error
		@Source = 'OutboxNumbers',
		@Name = 'DeleteNumbers' ,
		@Text = @Text,
		@Ip = '',
		@Browser = '',
		@ReferenceGuid = @OutboxGuid,
		@UserGuid = @EmptyGuid;

END CATCH
GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_GetCountSmsInPeriodDate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_GetCountSmsInPeriodDate]
  @UserGuid UNIQUEIDENTIFIER
  AS

	SELECT
		COUNT(*) [SmsCount],
		CONVERT(DATE,[EffectiveDateTime]) [EffectiveDateTime],
		[Status]
	FROM
		[dbo].[Smses]
	GROUP BY
		[EffectiveDateTime],
		[Status],
		[UserGuid]
	HAVING
		CONVERT(DATE,[EffectiveDateTime]) >= CONVERT(DATE,GETDATE()-10) AND
		[UserGuid] = @UserGuid;

	SELECT
		COUNT(*) [SmsCount],
		CONVERT(DATE,[ReceiveDateTime]) [RecieverDateTime]
	FROM
		[dbo].[Inboxes]
	GROUP BY
		[ReceiveDateTime],
		[UserGuid]
	HAVING
		CONVERT(DATE,[ReceiveDateTime]) >= CONVERT(DATE,GETDATE()-10) AND
		[UserGuid] = @UserGuid;


GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_GetPagedSmses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_GetPagedSmses]
  @OutboxGuid UNIQUEIDENTIFIER ,
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS

DECLARE @SendStatus INT;

SELECT
	@SendStatus = [SendStatus]
FROM [dbo].[Outboxes] WHERE [Guid] = @OutboxGuid;

--IF(@SendStatus = 10)--WatingForConfirm
--	RETURN;

DECLARE @Where NVARCHAR(MAX) = '[OutboxGuid] = '''+ CAST(@OutboxGuid AS VARCHAR(36)) +'''';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @Statement ='
	SELECT * INTO #Temp FROM [dbo].[OutboxNumbers] WITH(NOLOCK) '+ @Where +';
	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';

	
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement += ';DROP TABLE #Temp;';

EXEC(@Statement);


GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_GetUncertainDeliveryStatusSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[OutboxNumbers_GetUncertainDeliveryStatusSms]
	@SmsSenderAgentRefrence INT
  AS


SELECT TOP 5000
      [ReturnId],
      [DeliveryStatus],
			[ToNumber] AS [Reciever]
FROM 
			[OutboxNumbers]
WHERE
			[SmsSenderAgentReference] = @SmsSenderAgentRefrence AND
			[DeliveryStatus] IN (3,4,31) --Uncertain , DeliveredCommunication,GetDeliveryStatus


GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_InsertSentMessages]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_InsertSentMessages]
	@SentMessage [dbo].[SentMessages] READONLY
  AS

DECLARE @Count INT;
DECLARE @OutboxGuid UNIQUEIDENTIFIER;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000'

SET XACT_ABORT ON;

BEGIN TRY
	BEGIN	TRANSACTION;

	SELECT @Count = COUNT([ToNumber]) FROM @SentMessage;
	SELECT TOP 1 @OutboxGuid = [OutboxGuid] FROM @SentMessage;

	INSERT INTO [dbo].[OutboxNumbers]
							([Guid] ,
							 [ItemId],
							 [ToNumber] ,
							 [DeliveryStatus] ,
							 [ReturnId] ,
							 [CheckId] ,
							 [SendStatus] ,
							 [StatusDateTime],
							 [Operator] ,
							 [SmsSenderAgentReference],
							 [OutboxGuid])
					SELECT
							 NEWID(),
							 [ItemId],
							 [ToNumber],
							 [DeliveryStatus] ,
							 [ReturnId] ,
							 [CheckId] ,
							 [SendStatus],
							 GETDATE(),
							 [dbo].[GetNumberOperator]([ToNumber]),
							 [SmsSenderAgentReference],
							 [OutboxGuid]
					FROM 
							 @SentMessage;

	UPDATE [dbo].[Outboxes] 
	SET
		[SavedReceiverCount] = ISNULL([SavedReceiverCount],0) + @Count,
		[SendStatus] =  CASE WHEN	[SendStatus] != 10 THEN 
															CASE 
																WHEN [ReceiverCount] = ISNULL([SavedReceiverCount],0) + @Count THEN 4 --Sent	
																ELSE	3 --IsBeingSent
															END
												 ELSE [SendStatus] 
										END
	WHERE [Guid] = @OutboxGuid;
	
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			 @Type = 2, -- Error
	     @Source = N'OutboxNumbers',
	     @Name = N'InsertSentMessages',
	     @Text = @Text,
	     @Ip = '',
	     @Browser = '',
	     @ReferenceGuid = @OutboxGuid,
	     @UserGuid = @EmptyGuid;

	 THROW;
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_UpdateDeliveryStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_UpdateDeliveryStatus]
	@DeliveryStatus [dbo].[DeliveryStatus] READONLY
  AS


UPDATE
	[dbo].[OutboxNumbers]
SET
	[DeliveryStatus] = info.[Status],
	[StatusDateTime] = info.[DeliveryDateTime]
FROM
	[dbo].[OutboxNumbers] numbers INNER JOIN
	@DeliveryStatus info
ON
	numbers.[SmsSenderAgentReference] IN (5,7) AND --RahyabRG & SLS
	numbers.[ToNumber] = info.[Mobile] AND
	numbers.[ReturnId] = info.[BatchId]

UPDATE
	[dbo].[OutboxNumbers]
SET
	[DeliveryStatus] = info.[Status],
	[StatusDateTime] = info.[DeliveryDateTime]
FROM
	[dbo].[OutboxNumbers] numbers INNER JOIN
	@DeliveryStatus info
ON
	numbers.[SmsSenderAgentReference] NOT IN (5,7) AND
	numbers.[ReturnId] = info.[ReturnId]
		
GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_UpdateGsmDeliveryStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_UpdateGsmDeliveryStatus]
	@Status INT,
	@Mobiles NVARCHAR(MAX),
	@ReturnId VARCHAR(MAX)
  AS


EXEC('
	DECLARE @ReturnId NVARCHAR(255) = ' + @ReturnId + ';
	DECLARE @Status INT = ' + @Status + ';
	DECLARE @Receivers TABLE([Mobile] NVARCHAR(16));

	INSERT INTO @Receivers([Mobile])
	SELECT [ToNumber] FROM [dbo].[OutboxNumbers] WHERE [ReturnId] = @ReturnId AND [ToNumber] IN( ' + @Mobiles + ');

	UPDATE [dbo].[OutboxNumbers] SET [DeliveryStatus] = @Status
	WHERE
		[ReturnId] = @ReturnId AND
		[ToNumber] IN (SELECT [Mobile] FROM @Receivers);

	


			SELECT
				[OutboxGuid],
				[PrivateNumberGuid],
				[ToNumber] as Mobile,
				[DeliveryStatus],
				[UserGuid]
			FROM
				[dbo].[OutboxNumbers] number INNER JOIN
				[dbo].[Outboxes] otbx ON number.[OutboxGuid] = otbx.[Guid]
			WHERE
				number.[ReturnID] IN (''' + @ReturnId + ''');


	');

	--SELECT [Mobile] FROM @Receivers;
GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_UpdateSmsDeliveryStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[OutboxNumbers_UpdateSmsDeliveryStatus]
	@OuterSystemSmsIDs VARCHAR(MAX),
	@DeliveryStatus INT
  AS


EXEC('UPDATE [dbo].[OutboxNumbers]
			SET		 [DeliveryStatus] = '+ @DeliveryStatus +',
						 [StatusDateTime] = GETDATE()
			WHERE
						 [ReturnID] IN ('+ @OuterSystemSmsIDs +');

			SELECT
				[OutboxGuid],
				[PrivateNumberGuid],
				[ToNumber],
				[DeliveryStatus],
				[UserGuid]
			FROM
				[dbo].[OutboxNumbers] number INNER JOIN
				[dbo].[Outboxes] otbx ON number.[OutboxGuid] = otbx.[Guid]
			WHERE
				number.[ReturnID] IN ('+ @OuterSystemSmsIDs +');
		');




GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_UpdateSmsDeliveryStatusByNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[OutboxNumbers_UpdateSmsDeliveryStatusByNumber]
	@BatchID NVARCHAR(MAX),
	@Numbers VARCHAR(MAX),
	@DeliveryStatus INT
  AS


EXEC('UPDATE [OutboxNumbers]
			SET		 [DeliveryStatus] = '+ @DeliveryStatus +',
						 [StatusDateTime] = GETDATE()
			WHERE
			[ReturnID] = '''+@BatchID+''' AND
			[ToNumber] IN ('+ @Numbers +');

			SELECT 
				[OutboxGuid],
				[PrivateNumberGuid],
				[ToNumber],
				[DeliveryStatus],
				[UserGuid]
			FROM
				[dbo].[OutboxNumbers] number INNER JOIN
				[dbo].[Outboxes] otbx ON number.[OutboxGuid] = otbx.[Guid]
			WHERE
				[ReturnID] = '''+@BatchID+''' AND
				[ToNumber] IN ('+ @Numbers +');
		')
GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_UpdateSmsDeliveryStatusManually]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_UpdateSmsDeliveryStatusManually]
	@OutboxGuid UNIQUEIDENTIFIER,
	@ID BIGINT,
	@Numbers VARCHAR(MAX),
	@DeliveryStatus INT
  AS


IF(@ID != 0)
	SELECT @OutboxGuid = [Guid] FROM [dbo].[Outboxes] WHERE [ID]  = @ID;

EXEC('UPDATE [dbo].[OutboxNumbers]
			SET		 [DeliveryStatus] = '+ @DeliveryStatus +',
							[StatusDateTime] = GETDATE()
			WHERE
							[OutboxGuid] = '''+ @OutboxGuid +''' AND
							[ToNumber] IN ('+ @Numbers +');
		');




GO
/****** Object:  StoredProcedure [dbo].[OutboxNumbers_UpdateSmsSendInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[OutboxNumbers_UpdateSmsSendInfo]
	@XmlSmsInfo NVARCHAR(MAX)
  AS


	DECLARE @XMLDocPointer INT;
  EXEC sp_xml_preparedocument @XMLDocPointer OUTPUT, @XmlSmsInfo
  
  CREATE TABLE #Info
  ([NumberGuid] UNIQUEIDENTIFIER,
	 [SendStatus] INT,
	 [DeliveryStatus] INT,
	 [CheckID] NVARCHAR(MAX),
	 [ReturnID] NVARCHAR(MAX)
  );
  
  INSERT INTO #Info
          ( [NumberGuid] ,
            [SendStatus] ,
            [DeliveryStatus] ,
            [CheckID],
            [ReturnID])
					SELECT 
								[NumberGuid] ,
								[SendStatus] ,
								[DeliveryStatus] ,
								[CheckID],
								[ReturnID]
					FROM OPENXML(@XMLDocPointer,'/NewDataSet/Table',6)
					WITH
					 ( [NumberGuid] UNIQUEIDENTIFIER,
						 [SendStatus] INT,
						 [DeliveryStatus] INT,
						 [CheckID] NVARCHAR(MAX),
						 [ReturnID] NVARCHAR(MAX)) 
						 
EXEC sp_xml_removedocument @XMLDocPointer

UPDATE
			[dbo].[OutboxNumbers]
SET
			[SendStatus] = #Info.[SendStatus],
			[DeliveryStatus] = #Info.[DeliveryStatus],
			[CheckId] = #Info.[CheckID],
			[ReturnId] = #Info.[ReturnID],
			[StatusDateTime] = GETDATE()
FROM
			[dbo].[OutboxNumbers] number
			INNER JOIN #Info ON number.[Guid] = #Info.[NumberGuid];

DROP TABLE #Info;


GO
/****** Object:  StoredProcedure [dbo].[ParserFormulas_GetParserFormulas]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ParserFormulas_GetParserFormulas]
	@SmsParserGuid UNIQUEIDENTIFIER
  AS

	SELECT
		*
	FROM
		[ParserFormulas] 
	WHERE
		[SmsParserGuid] = @SmsParserGuid


GO
/****** Object:  StoredProcedure [dbo].[ParserFormulas_InsertFilterOption]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ParserFormulas_InsertFilterOption]
	@Guid UNIQUEIDENTIFIER,
	@Key NVARCHAR(50),
	@Condition INT,
	@ReactionExtention NVARCHAR(MAX),
	@SmsParserGuid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[ParserFormulas] WHERE SmsParserGuid = @SmsParserGuid;

INSERT INTO [ParserFormulas]
						([Guid],
						 [Key],
						 [Condition],
						 [ReactionExtention],
						 [SmsParserGuid])
				VALUES
						 (@Guid,
							@Key,
							@Condition,
							@ReactionExtention,
							@SmsParserGuid)


GO
/****** Object:  StoredProcedure [dbo].[PersonsInfo_GetCount]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PersonsInfo_GetCount]
	@ZoneGuid UNIQUEIDENTIFIER,
	@Prefix NVARCHAR(11),
	@ZipCode NVARCHAR(8),
	@NumberType INT,
	@Operator INT
  AS


DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @Where NVARCHAR(MAX) = 'WHERE [ZoneGuid] IN (SELECT [Guid] FROM @ZoneGuids)';

SET @Where += ' AND [MobileOperator]=' + CAST(@Operator AS VARCHAR(1));

IF (@NumberType != 0) 
  SET @Where += ' AND [MobileType]=' + CAST(@NumberType AS VARCHAR(1));

IF (@Prefix != 0) 
  SET @Where += ' AND [Mobile] LIKE ''' + @Prefix +'%''';

IF(ISNULL(@ZipCode,'') != '')
	SET @Where += ' AND [ZipCode] LIKE ''' + @ZipCode +'%''';

EXEC('
	DECLARE @ZoneGuids TABLE([Guid] UNIQUEIDENTIFIER);
	INSERT INTO @ZoneGuids([Guid]) EXEC [dbo].[Zones_GetAllChildren] '''+ @ZoneGuid +''';

	SELECT COUNT([Mobile]) AS [Count] FROM [dbo].[PersonsInfo] WITH(NOLOCK) ' + @Where +';')

GO
/****** Object:  StoredProcedure [dbo].[PersonsInfo_GetPagedBlackListNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PersonsInfo_GetPagedBlackListNumbers]
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[IsBlackList] = 1';
	
IF(@Query != '')
	SET @Where += ' AND ' + @Query;
	
IF (@where != '')		
	SET @Where=' WHERE '+@Where
--------------------------------------------------
  EXEC('
  SELECT COUNT([Mobile]) [RowCount] FROM [dbo].[PersonsInfo] ' +	@Where + ';
	
	WITH expTemp AS
	(
		SELECT
				Row_Number() OVER (ORDER BY '+ @SortField + ') AS [RowNumber], 
				*
		FROM
				[dbo].[PersonsInfo]' +	@Where + '
	)
	SELECT 
			*
	FROM
		expTemp
	WHERE 
		(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
		([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
	ORDER BY
			[RowNumber] ;')
GO
/****** Object:  StoredProcedure [dbo].[PersonsInfo_UpdateBlackListStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PersonsInfo_UpdateBlackListStatus]
	@Guid UNIQUEIDENTIFIER,
	@IsBlackList BIT
  AS


UPDATE [dbo].[PersonsInfo] SET [IsBlackList] = @IsBlackList WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[PersonsInfo_UpdateBlackListTableStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PersonsInfo_UpdateBlackListTableStatus]
	 @Numbers [dbo].[Recipient] READONLY,
	 @IsBlackList BIT
  AS


UPDATE [dbo].[PersonsInfo] SET [IsBlackList] = @IsBlackList
WHERE [Mobile] IN (SELECT [Mobile] FROM @Numbers);

GO
/****** Object:  StoredProcedure [dbo].[PhoneBookRegularContents_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBookRegularContents_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[PhoneBookRegularContents] WHERE [Guid] = @Guid;

GO
/****** Object:  StoredProcedure [dbo].[PhoneBookRegularContents_GetRegularContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBookRegularContents_GetRegularContents]
	@PhoneBookGuid UNIQUEIDENTIFIER
  AS


SELECT
	pbrc.[Guid],
	pbrc.[CreateDate],
	rc.[Title] ,
	rc.[Type]
FROM
	[dbo].[RegularContents] rc INNER JOIN
	[dbo].[PhoneBookRegularContents] pbrc ON rc.[Guid] = pbrc.[RegularContentGuid]
WHERE
	[PhoneBookGuid] = @PhoneBookGuid;
GO
/****** Object:  StoredProcedure [dbo].[PhoneBookRegularContents_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBookRegularContents_Insert]
	@Guid UNIQUEIDENTIFIER,
	@CreateDate DATETIME,
	@PhoneBookGuid UNIQUEIDENTIFIER,
	@RegularContentGuid UNIQUEIDENTIFIER
  AS


IF(SELECT COUNT(*) FROM [dbo].[PhoneBookRegularContents] WHERE [PhoneBookGuid] = @PhoneBookGuid AND [RegularContentGuid] = @RegularContentGuid)=0
BEGIN
	INSERT INTO [dbo].[PhoneBookRegularContents]
					([Guid] ,
					 [CreateDate] ,
					 [PhoneBookGuid] ,
					 [RegularContentGuid])
				 VALUES
					(@Guid,
					 @CreateDate,
					 @PhoneBookGuid,
					 @RegularContentGuid)
END
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_Delete]
  @Guid UNIQUEIDENTIFIER
  AS

	UPDATE
		[dbo].[PhoneBooks]
	SET
		[IsDeleted] = 1
	WHERE
		[Guid] = @Guid OR
		[ParentGuid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetActiveServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetActiveServices]
	@Mobile NVARCHAR(16)
  AS


SELECT
	book.[Guid],
	book.[Name] [Description],
	book.[ServiceId] [ServiceId],
	number.[Number] [Prefix],
	book.[VASRegisterKeys],
	book.[VASUnsubscribeKeys]
	INTO #vasGroups
FROM
	[dbo].[PhoneBooks] book WITH(NOLOCK) INNER JOIN
	[dbo].[PrivateNumbers] number WITH(NOLOCK) ON book.[ServiceId] = number.[ServiceID]
WHERE
	book.[Type] = 2 AND--vas
	book.[IsDeleted] = 0 AND
	number.[IsDeleted] = 0 AND
	number.[ServiceID] != '' AND
	number.[ServiceID] != '0'

SELECT 
	[PhoneBookGuid] AS [Guid]
	INTO #activeServices
FROM
	[dbo].[PhoneNumbers]
WHERE
	[IsDeleted] = 0 AND
	[PhoneBookGuid] IN (SELECT [Guid] FROM #vasGroups) AND
	[CellPhone] = @Mobile

SELECT * FROM #vasGroups WHERE [Guid] IN (SELECT [Guid] FROM #activeServices)

DROP TABLE #activeServices;
DROP TABLE #vasGroups;
	

GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetAllServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetAllServices]
  AS


SELECT
	book.[Guid],
	book.[Name] [Description],
	book.[ServiceId] [ServiceId],
	number.[Number] [Prefix]
FROM
	[dbo].[PhoneBooks] book WITH(NOLOCK) INNER JOIN
	[dbo].[PrivateNumbers] number WITH(NOLOCK) ON book.[ServiceId] = number.[ServiceID]
WHERE
	book.[Type] = 2 AND--vas
	book.[IsDeleted] = 0 AND
	number.[IsDeleted] = 0
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetCountNumberUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetCountNumberUser]
    @UserGuid UNIQUEIDENTIFIER
  AS
 
    SELECT SUM([CountPhoneNumbers]) AS [CountNumber] FROM(
    SELECT 
		books.[Guid],
        books.[Name],
        books.[CreateDate],
        books.[ParentGuid],
        books.[UserGuid],
        books.[IsPrivate],
        ISNULL(numbers.[CountPhoneNumbers],0) AS [CountPhoneNumbers]
    FROM    
		[PhoneBooks] books LEFT JOIN
		(SELECT 
			COUNT([Guid]) AS [CountPhoneNumbers],
			[PhoneBookGuid]
		 FROM 
			[PhoneNumbers] 
		 GROUP BY 
			[PhoneBookGuid]) numbers ON books.[Guid] = numbers.[PhoneBookGuid]
    WHERE   
		[UserGuid] = @UserGuid) p


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetDisabledServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetDisabledServices]
	@Mobile NVARCHAR(16)
  AS


SELECT
	book.[Guid],
	book.[Name] [Description],
	book.[ServiceId] [ServiceId],
	number.[Number] [Prefix]
	INTO #vasGroups
FROM
	[dbo].[PhoneBooks] book WITH(NOLOCK) INNER JOIN
	[dbo].[PrivateNumbers] number WITH(NOLOCK) ON book.[ServiceId] = number.[ServiceID]
WHERE
	book.[Type] = 2 AND--vas
	book.[IsDeleted] = 0 AND
	number.[IsDeleted] = 0

SELECT 
	[PhoneBookGuid] AS [Guid]
	INTO #disabledServices
FROM
	[dbo].[PhoneNumbers]
WHERE
	[IsDeleted] = 1 AND
	[PhoneBookGuid] IN (SELECT [Guid] FROM #vasGroups) AND
	[CellPhone] = @Mobile

SELECT * FROM #vasGroups WHERE [Guid] IN (SELECT [Guid] FROM #disabledServices)

DROP TABLE #disabledServices;
DROP TABLE #vasGroups;
	

GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetPhoneBookAdmin]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetPhoneBookAdmin]
    @UserGuid UNIQUEIDENTIFIER ,
    @ParentUserGuid UNIQUEIDENTIFIER
  AS
 
    SELECT  books.[Guid] ,
            books.[Name] ,
            books.[CreateDate] ,
            books.[ParentGuid] ,
            books.[UserGuid] ,
            books.[IsPrivate] ,
            books.[IsDeleted],
            ISNULL(numbers.[CountPhoneNumbers], 0) AS [CountPhoneNumbers]
    FROM    [PhoneBooks] books
            LEFT JOIN ( SELECT  COUNT([Guid]) AS [CountPhoneNumbers] ,
                                [PhoneBookGuid]
                        FROM    [PhoneNumbers]
                        WHERE   [PhoneNumbers].[IsDeleted] = 0
                        GROUP BY [PhoneBookGuid]
                      ) numbers ON books.[Guid] = numbers.[PhoneBookGuid]
    WHERE   
						books.[IsDeleted]=0 AND
						books.UserGuid = @ParentUserGuid AND
						( books.IsPrivate = 'False'
                  OR books.[UserGuid] = @UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetPhoneBookInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetPhoneBookInfo]
	@PhoneBookGuids NVARCHAR(MAX)
  AS


EXEC('SELECT [Name],[Type] FROM [PhoneBooks] WHERE [Guid] IN ('+@PhoneBookGuids+')')
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetPhoneBookUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetPhoneBookUser]
    @UserGuid UNIQUEIDENTIFIER,
		@ParentNodeGuid UNIQUEIDENTIFIER,
		@Name NVARCHAR(50),
		@LoadAllPhoneBook BIT
  AS
 
 		DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0';

		IF ( ISNULL(@Name, '') != '' ) 
    BEGIN
      IF ( @Where != '' ) 
				SET @Where += ' AND'
      SET @Where += ' [Name] LIKE N''%' + @Name + '%'''
    END

		IF ( LEN(@Name) = 0 AND @LoadAllPhoneBook = 0)
		BEGIN
			IF ( @Where != '' )
				SET @Where += ' AND'
			SET @Where += ' [ParentGuid]=''' + CAST(@ParentNodeGuid AS VARCHAR(36)) + ''''
		END

		IF ( @Where != '' )
			SET @Where += ' AND'
		SET @Where += ' ([UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''' OR [AlternativeUserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''')'

		IF (@Where != '' ) 
			SET @Where = ' WHERE ' + @Where;

		EXEC('
      SELECT
				books.[Guid] ,
				books.[ID],
				books.[ServiceId],
				books.[Type] ,
        books.[Name] ,
        books.[CreateDate] ,
        books.[ParentGuid] ,
        books.[UserGuid] ,
        books.[IsPrivate] ,
        books.[IsDeleted],
        ISNULL(numbers.[CountPhoneNumbers], 0) AS [CountPhoneNumbers]
			FROM    [PhoneBooks] books
							LEFT JOIN ( SELECT  COUNT([Guid]) AS [CountPhoneNumbers] ,
																	[PhoneBookGuid]
													FROM    [PhoneNumbers]
													WHERE   [PhoneNumbers].[IsDeleted] = 0
													GROUP BY [PhoneBookGuid]
												) numbers ON books.[Guid] = numbers.[PhoneBookGuid]'+@Where)




GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetUserMaximumRecordInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetUserMaximumRecordInfo]
	@UserGuid UNIQUEIDENTIFIER
  AS


SELECT SUM([MobileCount]) AS [MobileCount],SUM([EmailCount]) AS [EmailCount] FROM [dbo].[PhoneBooks] WHERE [UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_GetUserVasGroup]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_GetUserVasGroup]
	@UserGuid UNIQUEIDENTIFIER,
	@ServiceId NVARCHAR(32),
	@GroupId BIGINT
  AS


SELECT
	[Guid]
FROM
	[dbo].[PhoneBooks]
WHERE
	[IsDeleted] = 0 AND
	[Type] = 2 AND--VAS
	[ServiceId] = @ServiceId AND
	[ID] = @GroupId AND
	(
		[AlternativeUserGuid] = @UserGuid OR
		[UserGuid] = @UserGuid
	)
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Type TINYINT,
	@Name NVARCHAR(64),
	@ServiceId NVARCHAR(32),
	@VASRegisterKeys NVARCHAR(512),
	@VASUnsubscribeKeys NVARCHAR(512),
	@CreateDate DATETIME,
	@ParentGuid UNIQUEIDENTIFIER,
	@IsPrivate BIT,
	@AlternativeUserGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@AdminGuid UNIQUEIDENTIFIER
  AS


INSERT INTO PhoneBooks
        ([Guid],
				 [Type],
				 [Name],
				 [ServiceId],
				 [CreateDate],
				 [ParentGuid],
				 [IsPrivate],
				 [IsDeleted],
				 [AlternativeUserGuid],
				 [UserGuid],
				 [AdminGuid])
			 VALUES
				(@Guid,
				 @Type,
				 @Name,
				 @ServiceId,
				 @CreateDate,
				 @ParentGuid,
				 @IsPrivate,
				 0,
				 @AlternativeUserGuid,
				 @UserGuid,
				 @AdminGuid)


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_RecipientIsRegisteredToVasGroup]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_RecipientIsRegisteredToVasGroup]
	@GroupId BIGINT,
	@Receiver NVARCHAR(16)
  AS


DECLARE @Guid UNIQUEIDENTIFIER;

SELECT @Guid = [Guid] FROM [dbo].[PhoneBooks] WHERE [IsDeleted] = 0 AND [ID] = @GroupId;

SELECT
	[Guid]
FROM
	[dbo].[PhoneNumbers]
WHERE
	[IsDeleted] = 0 AND
	[PhoneBookGuid] = @Guid AND
	[CellPhone] = @Receiver
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_RegisterService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_RegisterService]
	@Mobile NVARCHAR(16),
	@ServiceId NVARCHAR(32)
  AS


DECLARE @GroupGuid UNIQUEIDENTIFIER;
DECLARE @ServiceName NVARCHAR(64);
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE @UserGuid UNIQUEIDENTIFIER;
DECLARE @Guid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Date DATETIME = GETDATE();
DECLARE @SmsText NVARCHAR(MAX) = '';
DECLARE @NumberExist INT;

SELECT @GroupGuid = [Guid],@ServiceName = [Name] FROM [dbo].[PhoneBooks] WHERE [ServiceId] = @ServiceId;
SELECT @PrivateNumberGuid = [Guid],@UserGuid = [OwnerGuid] FROM [dbo].[PrivateNumbers] WHERE [IsDeleted] = 0 AND [ServiceID] = @ServiceId;

IF(SELECT COUNT([CellPhone]) FROM [PhoneNumbers] WHERE [IsDeleted] = 0 AND [PhoneBookGuid] = @GroupGuid) > 0 
 SET @NumberExist = 1;
ELSE
 SET @NumberExist = 0;

IF(@NumberExist = 0)
BEGIN
	INSERT INTO [dbo].[PhoneNumbers]
					([Guid] ,
					 [CreateDate] ,
					 [CellPhone] ,
					 [Operator] ,
					 [IsDeleted] ,
					 [PhoneBookGuid])
				 VALUES
					(NEWID(),
					 GETDATE(),
					 @Mobile,
					 [dbo].[GetNumberOperator](@Mobile),
					 0,
					 @GroupGuid)

	SET @SmsText = N'عضویت شما در سرویس ' + @ServiceName + N'تایید شد';
	SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SmsText);
	SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

	EXEC [dbo].[ScheduledSmses_InsertSms]
			@Guid = @Guid,
			@PrivateNumberGuid = @PrivateNumberGuid,
			@Reciever = @Mobile,
			@SmsText = @SmsText,
			@SmsLen = @SmsLen,
			@PresentType = 1, -- Normal
			@Encoding = @Encoding,
			@TypeSend = 1, -- SendSms
			@Status = 1, -- Stored
			@DateTimeFuture = @Date,
			@UserGuid = @UserGuid,
			@IPAddress = N'',
			@Browser = N''
END

SELECT @NumberExist;
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_UnSubscribeService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_UnSubscribeService]
	@Mobile NVARCHAR(16),
	@ServiceId NVARCHAR(32)
  AS


DECLARE @GroupGuids TABLE([Guid] UNIQUEIDENTIFIER);
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE @UserGuid UNIQUEIDENTIFIER;
DECLARE @Guid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Date DATETIME = GETDATE();
DECLARE @SmsText NVARCHAR(MAX) = '';
DECLARE @Number NVARCHAR(MAX) = '';
DECLARE @NumberExist INT = 0;

SELECT @PrivateNumberGuid = [Guid],@UserGuid = [OwnerGuid],@Number = [Number] FROM [dbo].[PrivateNumbers] WHERE [IsDeleted] = 0 AND [ServiceID] = @ServiceId;
SELECT @PrivateNumberGuid = [Guid] FROM [dbo].[PrivateNumbers] WHERE [IsDeleted] = 0 AND [IsRoot] = 1 AND [Number] = SUBSTRING(@Number,1,4);

INSERT INTO @GroupGuids([Guid])
SELECT [Guid] FROM [dbo].[PhoneBooks] WHERE [ServiceId] = @ServiceId;

UPDATE [dbo].[PhoneNumbers] SET [IsDeleted] = 1 WHERE [PhoneBookGuid] IN (SELECT [Guid] FROM @GroupGuids) AND [CellPhone] = @Mobile;
SET @NumberExist = @@ROWCOUNT;

SET @SmsText = N'عضویت شما در سرویس موردنظر لغو شد.';
SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SmsText);
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

EXEC [dbo].[ScheduledSmses_InsertSms]
		@Guid = @Guid,
	  @PrivateNumberGuid = @PrivateNumberGuid,
	  @Reciever = @Mobile,
	  @SmsText = @SmsText,
	  @SmsLen = @SmsLen,
	  @PresentType = 1, -- Normal
	  @Encoding = @Encoding,
	  @TypeSend = 1, -- SendSms
	  @Status = 1, -- Stored
	  @DateTimeFuture = @Date,
	  @UserGuid = @UserGuid,
	  @IPAddress = N'',
	  @Browser = N'';

SELECT @NumberExist;
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_UpdateGroup]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_UpdateGroup]
	@PhoneBookGuid UNIQUEIDENTIFIER,
	@Type INT,
	@Name NVARCHAR(64)
  AS


UPDATE [dbo].[PhoneBooks]
SET
	[Type] = @Type,
	[Name] = @Name
WHERE
	[Guid] = @PhoneBookGuid;
GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_UpdateName]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_UpdateName]
			@Guid UNIQUEIDENTIFIER,
			@Name NVARCHAR(50)
  AS

		UPDATE 	[PhoneBooks] SET
		[Name]=@Name
		WHERE [Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_UpdateParent]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[PhoneBooks_UpdateParent]
			@Guid uniqueidentifier,
			@ParentGuid uniqueidentifier
  AS

		update PhoneBooks set
		[ParentGuid]=@ParentGuid
		where [Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[PhoneBooks_UpdateVasSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneBooks_UpdateVasSetting]
	@Guid UNIQUEIDENTIFIER,
	@Type TINYINT,
	@ServiceId NVARCHAR(32),
	@AlternativeUserGuid UNIQUEIDENTIFIER,
	@VASRegisterKeys NVARCHAR(512),
	@VASUnsubscribeKeys NVARCHAR(512)
  AS


UPDATE [dbo].[PhoneBooks]
SET
	[Type] = @Type,
	[ServiceId] = @ServiceId,
	[AlternativeUserGuid] = @AlternativeUserGuid,
	[VASRegisterKeys] = @VASRegisterKeys,
	[VASUnsubscribeKeys] = @VASUnsubscribeKeys
WHERE
	[Guid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_CheckNumberInScopOfSmsParser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_CheckNumberInScopOfSmsParser]
	@PhoneBookGuids NVARCHAR(MAX),
	@Numbers NVARCHAR(MAX)
  AS

	EXEC('
		SELECT
			[CellPhone] 
		FROM 
			[PhoneNumbers]
		WHERE
			[IsDeleted]=0 AND
			[PhoneBookGuid] IN('+@PhoneBookGuids+') AND
			[CellPhone] IN('+@Numbers+')
	')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_Delete]
    @Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE [PhoneNumbers] SET [IsDeleted]=1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_DeleteMultipleNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_DeleteMultipleNumber]
	@PhonebookGuid UNIQUEIDENTIFIER,
	@Numbers NVARCHAR(MAX)
  AS


EXEC('UPDATE [dbo].[PhoneNumbers]
			SET		 [IsDeleted] = 1
			WHERE
				[PhoneBookGuid] = '''+@PhonebookGuid+''' AND
				[CellPhone] IN ('+ @Numbers +')				
		')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_DeleteMultipleRecord]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_DeleteMultipleRecord]
	@GuidList NVARCHAR(MAX)
  AS


EXEC('UPDATE [dbo].[PhoneNumbers]
			SET		 [IsDeleted]=1
			WHERE
						 [Guid] IN ('+ @GuidList +')
		')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetCountNumberOfOperators]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[PhoneNumbers_GetCountNumberOfOperators]
  @PhoneBookGuid NVARCHAR(MAX),
  @DownRange INT,
  @UpRange INT,
  @IsSendBirthDate BIT
  AS
 

DECLARE @Where NVARCHAR(MAX)='' ;

IF (@DownRange != 0 ) 
BEGIN
  IF ( @Where != '' ) 
    SET @Where += ' AND'
  SET @Where += ' [RowNumber] >= ' + CAST(@DownRange AS VARCHAR(10))
END
  
IF (@UpRange != 0 ) 
BEGIN
  IF ( @Where != '' ) 
    SET @Where += ' AND'
  SET @Where += ' [RowNumber] <= ' + CAST(@UpRange AS VARCHAR(10))
END

IF ( @IsSendBirthDate = 1 )
BEGIN
	IF ( @Where != '' ) 
		SET @Where += ' AND'
	SET @Where += ' CONVERT(DATE,GETDATE()) = CONVERT(DATE,[BirthDate])'
END
     
IF (@Where != '' ) 
	SET @Where = ' WHERE ' + @Where
--------------------------------------------------------
EXEC('
WITH expTemp AS
(SELECT
		ROW_NUMBER() OVER (ORDER BY [CellPhone]) AS [RowNumber],
		[CellPhone],
		[PhoneBookGuid],
		[BirthDate],
		[IsDeleted],
		[Operator]
 FROM 
		[PhoneNumbers]
 WHERE
		[IsDeleted] = 0 AND
		[PhoneBookGuid] IN (' + @PhoneBookGuid + '))

 SELECT
	COUNT(*) AS [Count],
	[Operator]
 FROM expTemp '+ @Where +'
 GROUP BY [Operator]')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetCountNumberOfOperatorsForSendSmsFormat]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_GetCountNumberOfOperatorsForSendSmsFormat]
	@FormatGuid UNIQUEIDENTIFIER,
	@GroupsGuid NVARCHAR(MAX)
  AS

	EXEC('
	DECLARE @Encoding INT,
					@SmsPart INT;
					
	CREATE  TABLE #MsgInfo(ID int IDENTITY (1, 1) Primary key NOT NULL ,Operator INT, SmsBody NVARCHAR(MAX),SmsPartCount INT,Encoding INT)
	
	INSERT INTO #MsgInfo (Operator,SmsBody) SELECT
		[Operator],
		dbo.GenerateSmsFromFormat([Guid],'''+@FormatGuid+''') AS SmsBody
	FROM
		[dbo].[PhoneNumbers]
	WHERE
		[IsDeleted] = 0 AND 
		[PhoneBookGuid] IN ('+@GroupsGuid+');
	
	CREATE TABLE #SmsPart(SmsPartCount INT)
	CREATE TABLE #Encoding(Encoding INT)
	
	DECLARE @ID INT,@SmsBody NVARCHAR(MAX)
	DECLARE PhoneNumberCursor CURSOR LOCAL FOR 
	SELECT ID,SmsBody FROM #MsgInfo 
	 
	OPEN PhoneNumberCursor
	FETCH NEXT FROM PhoneNumberCursor INTO @ID,@SmsBody
	WHILE @@FETCH_STATUS = 0 
	BEGIN
    DELETE #SmsPart
    DELETE #Encoding
    
    EXEC @Encoding = dbo.HasUniCodeCharacter @SmsBody
    
    INSERT INTO #Encoding SELECT @Encoding
    
    EXEC @SmsPart = dbo.GetSmsCount @SmsBody
    
    INSERT INTO #SmsPart SELECT @SmsPart
    
    UPDATE #MsgInfo SET SmsPartCount = (SELECT TOP 1 SmsPartCount FROM #SmsPart),Encoding = (SELECT TOP 1 Encoding FROM #Encoding) WHERE ID = @ID
    FETCH NEXT FROM PhoneNumberCursor INTO @ID,@SmsBody
	END
	CLOSE PhoneNumberCursor
	DEALLOCATE PhoneNumberCursor
	
	SELECT 
				COUNT(*) AS [Count],
				[Operator],
				[SmsPartCount],
				[Encoding]
	FROM
				#MsgInfo
	GROUP BY 
				[Operator],
				[SmsPartCount],
				[Encoding]
	
	DROP TABLE #SmsPart
	DROP TABLE #Encoding
	DROP TABLE #MsgInfo')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetCountPhoneBooksNumberOperator]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_GetCountPhoneBooksNumberOperator]
	@PhoneBookGuids NVARCHAR(MAX)
  AS

	SELECT
		[Item]
		INTO #Temp
	FROM
		[dbo].[SplitString](@PhoneBookGuids,',')
	OPTION	(MAXRECURSION 0);

	SELECT
			COUNT(*) AS [Count],
			[Operator]
	FROM
			[PhoneNumbers]
	WHERE
			[IsDeleted] = 0 AND
			[Operator] > 0 AND
			[PhoneBookGuid] IN (SELECT * FROM #Temp)
	GROUP BY [Operator];

	DROP TABLE #Temp;
			  

GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetLimitedPagedPhoneNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_GetLimitedPagedPhoneNumbers]
  @PhoneBookGuid UNIQUEIDENTIFIER,
  @DownRange INT,
	@PageSize INT
  AS
 

WITH expTemp AS(
 SELECT
		ROW_NUMBER() OVER (ORDER BY [CellPhone]) AS [RowNumber],
		[CellPhone] AS [Number]
 FROM 
		[PhoneNumbers]
 WHERE
		[IsDeleted]=0 AND 
		[PhoneBookGuid] = @PhoneBookGuid)

	SELECT
		[Number] 
	FROM 
		expTemp
	WHERE
		RowNumber > @DownRange AND
		[RowNumber] <= (@DownRange + @PageSize)


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_GetNumber]
    @PhoneBookGuid UNIQUEIDENTIFIER ,
    @FirstName NVARCHAR(50) ,
    @LastName NVARCHAR(50) ,
    @BirthDate DATETIME ,
    @Telephone NVARCHAR(50) ,
    @CellPhone NVARCHAR(50) ,
    @FaxNumber NVARCHAR(50) ,
    @Job NVARCHAR(50) ,
    @Sex NVARCHAR(50) ,
    @Address NVARCHAR(MAX) ,
    @Email NVARCHAR(50)
  AS
 
    SELECT  [Guid] ,
            [FirstName] ,
            [LastName] ,
            [BirthDate] ,
            dbo.GetSolarDate([BirthDate]) AS [SolarBirthDate] ,
            [Telephone] ,
            [CellPhone] ,
            [FaxNumber] ,
            [Job] ,
            [Sex] ,
            [Address] ,
            [Email] ,
            [PhoneBookGuid]
    FROM    [PhoneNumbers]
    WHERE   [PhoneBookGuid] = @PhoneBookGuid
            AND ( @FirstName = ''
                  OR [FirstName] LIKE '%' + @FirstName + '%'
                )
            AND ( @LastName = ''
                  OR [LastName] LIKE '%' + @LastName + '%'
                )
            AND ( ISNULL(@BirthDate, '') = ''
                  OR [BirthDate] = @BirthDate
                )
            AND ( @Telephone = ''
                  OR [Telephone] LIKE '%' + @Telephone + '%'
                )
            AND ( @CellPhone = ''
                  OR [CellPhone] LIKE '%' + @CellPhone + '%'
                )
            AND ( @FaxNumber = ''
                  OR [FaxNumber] LIKE '%' + @FaxNumber + '%'
                )
            AND ( @Job = ''
                  OR [Job] LIKE '%' + @Job + '%'
                )
            AND ( @Sex = ''
                  OR [Sex] LIKE '%' + @Sex + '%'
                )
            AND ( @Address = ''
                  OR [Address] LIKE '%' + @Address + '%'
                )
            AND ( @Email = ''
                  OR [Email] LIKE '%' + @Email + '%'
                )


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetPagedAllNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_GetPagedAllNumbers]
		@UserGuid UNIQUEIDENTIFIER,
    @Query NVARCHAR(MAX),
    @PageNo INT ,
    @PageSize INT ,
    @SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[PhoneNumbers].[IsDeleted] = 0 AND [PhoneBooks].[IsDeleted] = 0'
		
IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where += ' [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
		
IF (@Where != '' ) 
  SET @Where = ' WHERE ' + @Where

IF(@Query != '')
	SET @Where += ' AND ' + @Query;
      
--------------------------------------------------
EXEC('
SELECT 
	COUNT(*) AS [RowCount] 
FROM 
	[PhoneNumbers] INNER JOIN 
	[PhoneBooks] ON [PhoneBooks].[Guid]=[PhoneNumbers].[PhoneBookGuid]' +	@Where + ';
	
WITH expTemp AS
(
	SELECT
			Row_Number() OVER (ORDER BY [PhoneNumbers].' + @SortField + ') AS [RowNumber], 
			[PhoneNumbers].[Guid],
			[PhoneNumbers].[FirstName],
			[PhoneNumbers].[LastName],
			[PhoneNumbers].[BirthDate],
			[PhoneNumbers].[CreateDate],
			[PhoneNumbers].[Telephone],
			[PhoneNumbers].[CellPhone],
			[PhoneNumbers].[FaxNumber],
			[PhoneNumbers].[Job],
			[PhoneNumbers].[Address],
			[PhoneNumbers].[Email],
			[PhoneNumbers].[Sex],
			[PhoneNumbers].[PhoneBookGuid],
			[PhoneBooks].[Name] AS [PhoneBookTitle]
	FROM
			[PhoneNumbers] INNER JOIN
			[PhoneBooks] ON [PhoneBooks].[Guid] = [PhoneNumbers].[PhoneBookGuid]' +	@Where + '
)
SELECT 
		*
FROM
	expTemp
WHERE 
	(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
	([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
ORDER BY
		[RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetPagedNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_GetPagedNumbers]
	@PhoneBookGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS
 

DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0'

IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where += ' [PhoneBookGuid]=''' + CAST(@PhoneBookGuid AS VARCHAR(36)) + ''''
			
IF (@Where != '' ) 
  SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where += ' AND ' + @Query;
	
--------------------------------------------------
EXEC('SELECT COUNT(*) AS [RowCount] FROM [dbo].[PhoneNumbers]' +	@Where + ';
	
WITH expTemp AS
(
	SELECT
			Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
			*
	FROM
			[dbo].[PhoneNumbers]' +	@Where + '
)
SELECT 
		*
FROM
	expTemp
WHERE 
	(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
	([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
ORDER BY
		[RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetPagedPhoneNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PhoneNumbers_GetPagedPhoneNumbers]
	@PhoneBookGuid UNIQUEIDENTIFIER,
	@PageNo INT,
	@PageSize INT
  AS

	
	WITH expTemp AS
	(
		SELECT
			ROW_NUMBER() OVER(ORDER BY [CellPhone]) AS [RowNumber],
			[CellPhone] AS [Number],
			*
		FROM
			[PhoneNumbers]
		WHERE
			[IsDeleted]=0 AND
			[PhoneBookGuid] =@PhoneBookGuid
	)
	
	SELECT * 
	FROM 
			expTemp
	WHERE
			RowNumber > ((@PageNo - 1) * @PageSize) AND
			[RowNumber] <= (@PageNo * @PageSize)


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetPhoneNumbersByAdvancedSearch]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PhoneNumbers_GetPhoneNumbersByAdvancedSearch]
    @AdvancedSearchQuery NVARCHAR(MAX)
  AS
 
  
EXEC @AdvancedSearchQuery


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetPhoneNumbersByPhoneBookGuid]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PhoneNumbers_GetPhoneNumbersByPhoneBookGuid]
    @PhoneBookGuid UNIQUEIDENTIFIER
  AS
 
  
SELECT 
			[Guid],
			[FirstName],
			[LastName],
			[BirthDate],
			[CreateDate],
			[Telephone],
			[CellPhone],
			[FaxNumber],
			[Job],
			[Address],
			[Email],
			[F1],
			[F2],
			[F3],
			[F4],
			[F5],
			[F6],
			[F7],
			[F8],
			[F9],
			[F10],
			[F11],
			[F12],
			[F13],
			[F14],
			[F15],
			[F16],
			[F17],
			[F18],
			[F19],
			[F20],
			[Sex],
			[IsDeleted],
			[PhoneBookGuid]
FROM 
			[PhoneNumbers]
WHERE
			[IsDeleted]=0	AND
			[PhoneBookGuid] = @PhoneBookGuid


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_GetTodayBirthdaysPagedPhoneNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PhoneNumbers_GetTodayBirthdaysPagedPhoneNumbers]
  @PhoneBookGuid UNIQUEIDENTIFIER,
  @PageNo INT,
	@PageSize INT
  AS
 

	WITH expTemp AS
	(
		SELECT
			ROW_NUMBER() OVER(ORDER BY [CellPhone]) AS [RowNumber],
			[CellPhone] AS [Number]
		FROM
			[PhoneNumbers]
		WHERE	
			[IsDeleted]=0	AND
			[PhoneBookGuid] = @PhoneBookGuid AND
			CONVERT(DATE,GETDATE())=CONVERT(DATE,[BirthDate])
	)
	
	SELECT [Number] 
	FROM 
			expTemp
	WHERE
			RowNumber > ((@PageNo - 1) * @PageSize) AND
			[RowNumber] <= (@PageNo * @PageSize)


GO
/****** Object:  StoredProcedure [dbo].[Phonenumbers_InsertBulkNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Phonenumbers_InsertBulkNumbers]
  @Numbers [dbo].[PhoneNumber] READONLY,
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @PhoneBookGuid UNIQUEIDENTIFIER;
DECLARE @EmailCount INT = 0;
DECLARE @MobileCount INT = 0;
DECLARE @InsertedNumberCount INT;

SELECT TOP 1 @PhoneBookGuid = [PhoneBookGuid] FROM @Numbers;
SELECT @EmailCount = COUNT(*) FROM @Numbers WHERE ISNULL([Email],'') != '';
SELECT @MobileCount = COUNT(*) FROM @Numbers WHERE ISNULL([CellPhone],'') != '';

INSERT INTO [dbo].[PhoneNumbers]
						([Guid] ,
						 [FirstName] ,
						 [LastName] ,
						 [BirthDate] ,
						 [CreateDate] ,
						 [Telephone] ,
						 [CellPhone] ,
						 [FaxNumber] ,
						 [Job] ,
						 [Address] ,
						 [Email] ,
						 [F1] ,
						 [F2] ,
						 [F3] ,
						 [F4] ,
						 [F5] ,
						 [F6] ,
						 [F7] ,
						 [F8] ,
						 [F9] ,
						 [F10] ,
						 [F11] ,
						 [F12] ,
						 [F13] ,
						 [F14] ,
						 [F15] ,
						 [F16] ,
						 [F17] ,
						 [F18] ,
						 [F19] ,
						 [F20] ,
						 [Sex] ,
						 [Operator] ,
						 [IsDeleted] ,
						 [PhoneBookGuid])
			SELECT
						[Guid],
						[FirstName] ,
						[LastName] ,
						[BirthDate] ,
						[CreateDate] ,
						[Telephone] ,
						[CellPhone] ,
						[FaxNumber] ,
						[Job] ,
						[Address] ,
						[Email] ,
						[F1] ,
						[F2] ,
						[F3] ,
						[F4] ,
						[F5] ,
						[F6] ,
						[F7] ,
						[F8] ,
						[F9] ,
						[F10] ,
						[F11] ,
						[F12] ,
						[F13] ,
						[F14] ,
						[F15] ,
						[F16] ,
						[F17] ,
						[F18] ,
						[F19] ,
						[F20] ,
						[Sex] ,
						[dbo].[GetNumberOperator]([CellPhone]),
						0 ,
						[PhoneBookGuid]	
			FROM
						@Numbers;

SET @InsertedNumberCount = @@ROWCOUNT;

UPDATE [dbo].[PhoneBooks]
SET
	[MobileCount] = ISNULL([MobileCount],0) + @MobileCount,
	[EmailCount] = ISNULL([EmailCount],0) + @EmailCount,
	[RecordCount] = ISNULL([RecordCount],0) + @InsertedNumberCount
WHERE
	[Guid] = @PhoneBookGuid;

GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_InsertEmailAddress]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_InsertEmailAddress]
  @UserGuid UNIQUEIDENTIFIER ,
  @PhoneBookGuid UNIQUEIDENTIFIER,
  @EmailsXml NVARCHAR(MAX) ,
  @Scope INT
  AS
 
	DECLARE @XMLDocPointer INT;
	DECLARE @InsertedRowCout INT;

	CREATE  TABLE #Emails(Email NVARCHAR(128),IsDuplicate BIT);
  EXEC sp_xml_preparedocument @XMLDocPointer OUTPUT, @EmailsXml
  
  INSERT INTO #Emails
          ( Email, IsDuplicate )
					SELECT 
							[Email],0
					FROM	OPENXML(@XMLDocPointer,'/NewDataSet/Table',6)
					WITH
					([Email] NVARCHAR(128))
 
		
  IF ( @Scope = 1 ) 
  BEGIN
		UPDATE #Emails 
		SET [IsDuplicate] = 1 WHERE [Email] IN(
						SELECT  
								[Email]
						FROM    
								[PhoneNumbers]
						WHERE 
								[IsDeleted] = 0 AND
								[PhoneBookGuid] = @PhoneBookGuid)
									
		INSERT  INTO [PhoneNumbers]
            ( [Guid] ,
              [CreateDate] ,
              [Email] ,
              [IsDeleted] ,
              [PhoneBookGuid])
           SELECT
							NEWID(),
							GETDATE(),
							[Email],
							0,
							@PhoneBookGuid
					 FROM	#Emails WHERE [IsDuplicate] = 0
					 
		SET @InsertedRowCout = @@ROWCOUNT;

		UPDATE [dbo].[PhoneBooks]
		SET
			[EmailCount] = ISNULL([EmailCount],0) + @InsertedRowCout,
			[RecordCount] = ISNULL([RecordCount],0) + @InsertedRowCout
		WHERE
			[Guid] = @PhoneBookGuid;

		SELECT 
			COUNT(*) AS CountDuplicateEmails
		FROM #Emails WHERE [IsDuplicate] = 1
  END
  
  ELSE IF ( @scope = 2 )
  BEGIN
		UPDATE #Emails 
		SET [IsDuplicate] = 1 WHERE [Email] IN(
						SELECT  
									[Email]
						FROM    
									[PhoneNumbers] number INNER JOIN [PhoneBooks] phoneBook
									ON phoneBook.[Guid] = number.[PhoneBookGuid]
						WHERE 
									number.[IsDeleted] = 0 AND
									phoneBook.[IsDeleted] = 0 AND
									[UserGuid] = @UserGuid)
		
		INSERT  INTO [PhoneNumbers]
            ( [Guid] ,
              [CreateDate] ,
              [Email] ,
              [IsDeleted] ,
              [PhoneBookGuid])
           SELECT
							NEWID(),
							GETDATE(),
							[Email],
							0,
							@PhoneBookGuid
					 FROM	#Emails WHERE [IsDuplicate] = 0
		
		SET @InsertedRowCout = @@ROWCOUNT;

		UPDATE [dbo].[PhoneBooks]
		SET
			[EmailCount] = ISNULL([EmailCount],0) + @InsertedRowCout,
			[RecordCount] = ISNULL([RecordCount],0) + @InsertedRowCout
		WHERE
			[Guid] = @PhoneBookGuid;

		SELECT 
					COUNT(*) AS CountDuplicateNumbers
		FROM #Emails WHERE [IsDuplicate] = 1
		
  END
  
  DROP TABLE #Emails;
  EXEC sp_xml_removedocument @XMLDocPointer


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_InsertListNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PhoneNumbers_InsertListNumber]
  @UserGuid UNIQUEIDENTIFIER ,
  @PhoneBookGuid UNIQUEIDENTIFIER,
  @NumbersXml NVARCHAR(MAX) ,
  @Scope INT
  AS
 
	DECLARE @XMLDocPointer INT;
	DECLARE @InsertedRowCout INT;

	CREATE  TABLE #Numbers(CellPhone NVARCHAR(16),IsDuplicate BIT);
  EXEC sp_xml_preparedocument @XMLDocPointer OUTPUT, @NumbersXml
  
  INSERT INTO #Numbers
          ( CellPhone, IsDuplicate )
					SELECT 
							[CellPhone],0
					FROM	OPENXML(@XMLDocPointer,'/NewDataSet/Table',6)
					WITH
					([CellPhone] NVARCHAR(16))
 
		
  IF ( @Scope = 1 ) 
  BEGIN
		UPDATE #Numbers 
		SET [IsDuplicate] = 1 WHERE [CellPhone] IN(
						SELECT  
								[CellPhone]
						FROM    
								[PhoneNumbers]
						WHERE 
								[IsDeleted] = 0 AND
								[PhoneBookGuid] = @PhoneBookGuid)
									
		INSERT  INTO [PhoneNumbers]
            ( [Guid] ,
              [CreateDate] ,
              [CellPhone] ,
              [IsDeleted] ,
              [PhoneBookGuid],
              [Operator])
           SELECT
							NEWID(),
							GETDATE(),
							[CellPhone],
							0,
							@PhoneBookGuid,
							dbo.GetNumberOperator([CellPhone])
					 FROM	#Numbers WHERE [IsDuplicate] = 0
					 
		SET @InsertedRowCout = @@ROWCOUNT;

		UPDATE [dbo].[PhoneBooks]
		SET
			[MobileCount] = ISNULL([MobileCount],0) + @InsertedRowCout,
			[RecordCount] += @InsertedRowCout
		WHERE
			[Guid] = @PhoneBookGuid;

		SELECT 
			COUNT(*) AS CountDuplicateNumbers
		FROM #Numbers WHERE [IsDuplicate] = 1
  END
  
  ELSE IF ( @scope = 2 )
  BEGIN
		UPDATE #Numbers 
		SET [IsDuplicate] = 1 WHERE [CellPhone] IN(
						SELECT  
									[CellPhone]
						FROM    
									[PhoneNumbers] number INNER JOIN [PhoneBooks] phoneBook
									ON phoneBook.[Guid] = number.[PhoneBookGuid]
						WHERE 
									number.[IsDeleted] = 0 AND
									phoneBook.[IsDeleted] = 0 AND
									[UserGuid] = @UserGuid)
		
		INSERT  INTO [PhoneNumbers]
            ( [Guid] ,
              [CreateDate] ,
              [CellPhone] ,
              [IsDeleted] ,
              [PhoneBookGuid],
              [Operator])
           SELECT
							NEWID(),
							GETDATE(),
							[CellPhone],
							0,
							@PhoneBookGuid,
							dbo.GetNumberOperator([CellPhone])
					 FROM	#Numbers WHERE [IsDuplicate] = 0
		
		SET @InsertedRowCout = @@ROWCOUNT;

		UPDATE [dbo].[PhoneBooks]
		SET
			[MobileCount] = ISNULL([MobileCount],0) + @InsertedRowCout,
			[RecordCount] += @InsertedRowCout
		WHERE
			[Guid] = @PhoneBookGuid;

		SELECT 
					COUNT(*) AS CountDuplicateNumbers
		FROM #Numbers WHERE [IsDuplicate] = 1
		
  END
  
  DROP TABLE #Numbers;
  EXEC sp_xml_removedocument @XMLDocPointer


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_InsertNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PhoneNumbers_InsertNumber]
	@Guid UNIQUEIDENTIFIER ,
	@FirstName NVARCHAR(32) ,
	@LastName NVARCHAR(64) ,
	@NationalCode NCHAR(10),
	@BirthDate DATETIME ,
	@CreateDate DATETIME ,
	@Telephone NVARCHAR(16) ,
	@CellPhone NVARCHAR(16) ,
	@FaxNumber NVARCHAR(16) ,
	@Job NVARCHAR(64) ,
	@Address NVARCHAR(MAX) ,
	@Email NVARCHAR(128) ,
	@F1 NVARCHAR(128) ,
	@F2 NVARCHAR(128) ,
	@F3 NVARCHAR(128) ,
	@F4 NVARCHAR(128) ,
	@F5 NVARCHAR(128) ,
	@F6 NVARCHAR(128) ,
	@F7 NVARCHAR(128) ,
	@F8 NVARCHAR(128) ,
	@F9 NVARCHAR(128) ,
	@F10 NVARCHAR(128) ,
	@F11 NVARCHAR(128) ,
	@F12 NVARCHAR(128) ,
	@F13 NVARCHAR(128) ,
	@F14 NVARCHAR(128) ,
	@F15 NVARCHAR(128) ,
	@F16 NVARCHAR(128) ,
	@F17 NVARCHAR(128) ,
	@F18 NVARCHAR(128) ,
	@F19 NVARCHAR(128) ,
	@F20 NVARCHAR(128) ,
	@Sex TINYINT ,
	@PhoneBookGuid UNIQUEIDENTIFIER
  AS

DECLARE @MobileCount INT = 0;
DECLARE @EmailCount INT = 0;

IF(@CellPhone != '')
 SET @MobileCount = 1;
IF(@Email != '')
	SET @EmailCount = 1;

INSERT  INTO [PhoneNumbers]
        ( [Guid] ,
          [FirstName] ,
          [LastName] ,
					[NationalCode],
          [BirthDate] ,
          [CreateDate] ,
          [Telephone] ,
          [CellPhone] ,
          [FaxNumber] ,
          [Job] ,
          [Address] ,
          [Email] ,
          [F1] ,
          [F2] ,
          [F3] ,
          [F4] ,
          [F5] ,
          [F6] ,
          [F7] ,
          [F8] ,
          [F9] ,
          [F10] ,
          [F11] ,
          [F12] ,
          [F13] ,
          [F14] ,
          [F15] ,
          [F16] ,
          [F17] ,
          [F18] ,
          [F19] ,
          [F20] ,
          [Sex] ,
          [IsDeleted],
          [PhoneBookGuid],
          [Operator])
VALUES  ( @Guid ,
          @FirstName ,
          @LastName ,
					@NationalCode,
          @BirthDate ,
          @CreateDate ,
          @Telephone ,
          @CellPhone ,
          @FaxNumber ,
          @Job ,
          @Address ,
          @Email ,
          @F1 ,
          @F2 ,
          @F3 ,
          @F4 ,
          @F5 ,
          @F6 ,
          @F7 ,
          @F8 ,
          @F9 ,
          @F10 ,
          @F11 ,
          @F12 ,
          @F13 ,
          @F14 ,
          @F15 ,
          @F16 ,
          @F17 ,
          @F18 ,
          @F19 ,
          @F20 ,
          @Sex ,
          0,
          @PhoneBookGuid,
          dbo.GetNumberOperator(@CellPhone));

UPDATE [dbo].[PhoneBooks]
SET
	[MobileCount] = ISNULL([MobileCount],0) + @MobileCount,
	[EmailCount] = ISNULL([EmailCount],0) + @EmailCount,
	[RecordCount] += 1
WHERE
	[Guid] = @PhoneBookGuid;
								


GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_NumberStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_NumberStatus]
  @Scope INT ,
  @CellPhone NVARCHAR(16) ,
	@Email NVARCHAR(128),
  @PhoneBookGuid UNIQUEIDENTIFIER ,
  @UserGuid UNIQUEIDENTIFIER
  AS


IF ( @Scope = 1 ) 
BEGIN
	SELECT  
			[CellPhone]
	FROM    
			[PhoneNumbers]
	WHERE 
			[IsDeleted]=0 AND
			[PhoneBookGuid] = @PhoneBookGuid AND
			[CellPhone] = @CellPhone AND
			[Email] = @Email
END
ELSE IF ( @Scope = 2 )
BEGIN
  SELECT  
			[CellPhone]
  FROM    
			[dbo].[PhoneNumbers] number INNER JOIN [dbo].[PhoneBooks] phoneBook
			ON phoneBook.[Guid] = number.[PhoneBookGuid]
  WHERE 
			number.[IsDeleted] = 0 AND
			phoneBook.[IsDeleted] = 0 AND
			[UserGuid] = @UserGuid AND
			[CellPhone] = @CellPhone AND
			[Email] = @Email
END

GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_UpdateNationalCodeParser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_UpdateNationalCodeParser]
	@Guid UNIQUEIDENTIFIER,
	@Mobile NVARCHAR(16),
	@NationalCode NCHAR(10)
  AS


UPDATE [dbo].[PhoneNumbers]
SET
	[NationalCode] = @NationalCode,
	[CellPhone] = @Mobile,
	[CreateDate] = GETDATE()
WHERE
	[Guid] = @Guid;

GO
/****** Object:  StoredProcedure [dbo].[PhoneNumbers_UpdateNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PhoneNumbers_UpdateNumber]
	@Guid UNIQUEIDENTIFIER ,
	@FirstName NVARCHAR(32) ,
	@LastName NVARCHAR(64) ,
	@NationalCode NCHAR(10),
	@BirthDate DATETIME ,
	@Telephone NVARCHAR(16) ,
	@CellPhone NVARCHAR(16) ,
	@FaxNumber NVARCHAR(16) ,
	@Job NVARCHAR(64) ,
	@Address NVARCHAR(MAX) ,
	@Email NVARCHAR(128) ,
	@F1 NVARCHAR(128) ,
	@F2 NVARCHAR(128) ,
	@F3 NVARCHAR(128) ,
	@F4 NVARCHAR(128) ,
	@F5 NVARCHAR(128) ,
	@F6 NVARCHAR(128) ,
	@F7 NVARCHAR(128) ,
	@F8 NVARCHAR(128) ,
	@F9 NVARCHAR(128) ,
	@F10 NVARCHAR(128) ,
	@F11 NVARCHAR(128) ,
	@F12 NVARCHAR(128) ,
	@F13 NVARCHAR(128) ,
	@F14 NVARCHAR(128) ,
	@F15 NVARCHAR(128) ,
	@F16 NVARCHAR(128) ,
	@F17 NVARCHAR(128) ,
	@F18 NVARCHAR(128) ,
	@F19 NVARCHAR(128) ,
	@F20 NVARCHAR(128) ,
	@Sex INT 
  AS
 
UPDATE
	[dbo].[PhoneNumbers]
SET
	[FirstName] = @FirstName ,
  [LastName] = @LastName ,
	[NationalCode] = @NationalCode,
  [BirthDate] = @BirthDate ,
  [Telephone] = @Telephone ,
  [CellPhone] = @CellPhone ,
  [FaxNumber] = @FaxNumber ,
  [Job] = @Job ,
  [Address] = @Address ,
  [Email] = @Email ,
  [F1] = @F1 ,
  [F2] = @F2 ,
  [F3] = @F3 ,
  [F4] = @F4 ,
  [F5] = @F5 ,
  [F6] = @F6 ,
  [F7] = @F7 ,
  [F8] = @F8 ,
  [F9] = @F9 ,
  [F10] = @F10 ,
  [F11] = @F11 ,
  [F12] = @F12 ,
  [F13] = @F13 ,
  [F14] = @F14 ,
  [F15] = @F15 ,
  [F16] = @F16 ,
  [F17] = @F17 ,
  [F18] = @F18 ,
  [F19] = @F19 ,
  [F20] = @F20 ,
  [Sex] = @Sex,
	[Operator] = dbo.GetNumberOperator([CellPhone])
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_AssignNumberToUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_AssignNumberToUser]
	@NumberGuid UNIQUEIDENTIFIER,
	@OwnerGuid UNIQUEIDENTIFIER,
	@Keyword NVARCHAR(64),
	@Price DECIMAL(18,2),
	@ExpireDate DATETIME
  AS


IF(ISNULL(@Keyword,'') = '')
BEGIN
	UPDATE [PrivateNumbers] 
	SET 
		[OwnerGuid] = @OwnerGuid,
		[Price] = @Price,
		[ExpireDate] = @ExpireDate
	WHERE
		[Guid] = @NumberGuid
END
ELSE
BEGIN
	INSERT INTO [dbo].[ReceiveKeywords]
		      ([Guid] ,
		       [Keyword] ,
					 [CreateDate],
		       [UserGuid] ,
		       [PrivateNumberGuid])
					VALUES
					(NEWID(),
					 @Keyword,
					 GETDATE(),
					 @OwnerGuid,
					 @NumberGuid)
END


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_AssignRangeNumberToUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_AssignRangeNumberToUser]
	@NumberGuid UNIQUEIDENTIFIER,
	@OwnerGuid UNIQUEIDENTIFIER,
	@Price DECIMAL(18,2),
	@ExpireDate DATETIME
  AS


UPDATE
	[dbo].[PrivateNumbers]
SET
	[OwnerGuid] = @OwnerGuid,
	[Price] = @Price,
	[ExpireDate] = @ExpireDate
WHERE
	[Guid] = @NumberGuid;


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_AssignSubRangeNumberToUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_AssignSubRangeNumberToUser]
	@Number NVARCHAR(50),
	@Range NVARCHAR(255),
	@Regex NVARCHAR(255),
	@ParentGuid UNIQUEIDENTIFIER,
	@OwnerGuid UNIQUEIDENTIFIER,
	@Keyword NVARCHAR(50),
	@Price DECIMAL(18,2),
	@ExpireDate DATETIME
  AS


DECLARE @Guid UNIQUEIDENTIFIER = NEWID();

WITH expTemp AS(
	SELECT * FROM [dbo].[PrivateNumbers]
	WHERE [Guid] = @ParentGuid
)

INSERT INTO [dbo].[PrivateNumbers]
						 ([Guid],
							[CreateDate],
							[ExpireDate],
							[Number],
							[Price],
							[Type],
							[ServiceID],
							[ServicePrice],
							[Priority],
							[ReturnBlackList],
							[CheckFilter],
							[DeliveryBase],
							[UseForm],
							[Range],
							[Regex],
							[ParentGuid],
							[OwnerGuid],
							[IsActive],
							[IsPublic],
							[SmsSenderAgentGuid],
							[UserGuid],
							[IsDeleted])
				SELECT 
							@Guid,
							GETDATE(),
							@ExpireDate,
							@Number,
							@Price,
							[Type],
							[ServiceID],
							[ServicePrice],
							[Priority],
							[ReturnBlackList],
							[CheckFilter],
							[DeliveryBase],
							[UseForm],
							@Range,
							@Regex,
							[Guid],
							CASE ISNULL(@Keyword,'')
								WHEN '' THEN @OwnerGuid
							ELSE [OwnerGuid] END,
							1,
							0,
							[SmsSenderAgentGuid],
							[UserGuid],
							0
				FROM expTemp;

IF(ISNULL(@Keyword,'') != '')
BEGIN
	INSERT INTO [dbo].[ReceiveKeywords]
		        ([Guid] ,
		         [Keyword] ,
						 [CreateDate],
		         [UserGuid] ,
		         [PrivateNumberGuid])
						VALUES
						(NEWID(),
						 @Keyword,
						 GETDATE(),
						 @OwnerGuid,
						 @Guid)
END


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_Delete]
  @Guid UNIQUEIDENTIFIER
  AS


UPDATE  [PrivateNumbers]
SET     [IsDeleted] = 1
WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_DeleteKeyword]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_DeleteKeyword]
	@Guid UNIQUEIDENTIFIER
  AS

DELETE FROM [dbo].[ReceiveKeywords] WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_DeleteNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_DeleteNumber]
	@Guid UNIQUEIDENTIFIER
  AS

	DECLARE @OwnerGuid UNIQUEIDENTIFIER;
	DECLARE @ParentGuid UNIQUEIDENTIFIER;
	DECLARE @UseForm INT;

	SELECT @OwnerGuid = [OwnerGuid],@UseForm = [UseForm] FROM [dbo].[PrivateNumbers] WHERE [Guid] = @Guid;

	SELECT @ParentGuid = [ParentGuid] FROM [dbo].[Users] WHERE [Guid] = @OwnerGuid;

	IF(@UseForm = 2)--RangeNumber
	BEGIN
		DELETE FROM [dbo].[ReceiveKeywords] WHERE [PrivateNumberGuid] = @Guid;
		UPDATE [dbo].[PrivateNumbers] SET IsDeleted = 1 WHERE [Guid] = @Guid;
	END
	ELSE
	BEGIN
		UPDATE [dbo].[PrivateNumbers]
		SET [OwnerGuid] = @ParentGuid
		WHERE [Guid] = @Guid;
	END

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetAgentInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetAgentInfo]
	@NumberGuid UNIQUEIDENTIFIER
  AS


SELECT
	number.[Guid],
	number.[Number],
	agent.[SmsSenderAgentReference],
	agent.[IsSendActive],
	agent.[IsSendBulkActive]
FROM 
	[PrivateNumbers] number INNER JOIN
	[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	number.[Guid] = @NumberGuid
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetAllRanges]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetAllRanges]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SELECT
	*
FROM
	[dbo].[PrivateNumbers]
WHERE
	[IsDeleted] = 0 AND
	[UseForm] IN(0,2) AND
  [ParentGuid] = @EmptyGuid AND
	[UserGuid] = @UserGuid
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetAllSmsSendrAgentPrivateNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PrivateNumbers_GetAllSmsSendrAgentPrivateNumber]
	@SmsSenderAgentRefrence INT
  AS
 

DECLARE @Now DATETIME = GETDATE()

SELECT DISTINCT
			[PrivateNumbers].[Number] 
FROM
			[PrivateNumbers] INNER JOIN 
      [SmsSenderAgents] ON [PrivateNumbers].[SmsSenderAgentGuid] = [SmsSenderAgents].[Guid] INNER JOIN 
      [UserPrivateNumbers] AS userNumbers ON [PrivateNumbers].[Guid] = userNumbers.[PrivateNumberGuid]
WHERE 
			[PrivateNumbers].[IsDeleted] = 0 AND
			userNumbers.[IsDelete] = 0 AND
			userNumbers.[IsActive] = 1 AND
			userNumbers.[Type] = 2 AND --Recieve 
			[SmsSenderAgents].[SmsSenderAgentReference] = @SmsSenderAgentRefrence AND
			@Now BETWEEN userNumbers.[StartDate] AND userNumbers.[EndDate]


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetPagedAllAssignedLines]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetPagedAllAssignedLines]
	@Query NVARCHAR(MAX),
	@PageNo INT,
	@PageSize INT,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = 'number.[IsDeleted] = 0 AND users.[IsDeleted] = 0';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE @Statement NVARCHAR(MAX) = '';

SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @Statement = '

SELECT
		number.[Guid] ,
		users.[Guid] [UserGuid],
		[Number] ,
		[Range] ,
		[ServiceID],
		[ServicePrice],
		[UseForm] ,
		number.[CreateDate],
		number.[ParentGuid] ,
		[OwnerGuid] ,
		number.[IsActive] ,
		[IsPublic] ,
		number.[Type],
		[UserName] INTO #Temp
FROM
	[dbo].[PrivateNumbers] number	 INNER JOIN 
	[dbo].[Users] users ON number.[OwnerGuid] = users.[Guid] '+ @Where +';
		
SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);

	
	

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetPagedAssignedKeywords]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetPagedAssignedKeywords]
	@UserGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT,
	@PageSize INT,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE @Statement NVARCHAR(MAX) = '';

IF(@Where != '')
	SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @Statement = '

DECLARE @Childrens TABLE([UserGuid] UNIQUEIDENTIFIER,[UserName] NVARCHAR(50));

INSERT INTO @Childrens([UserGuid],[UserName])
	SELECT [UserGuid],[UserName] FROM dbo.udfGetAllChildren(''' + CAST(@UserGuid AS VARCHAR(36)) + ''');

DELETE FROM @Childrens WHERE [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + ''';

SELECT
		[Number] ,
		[UserName],
		keyword.[Guid] ,
		keyword.[keyword],
		keyword.[CreateDate] INTO #Temp
FROM
	[dbo].[PrivateNumbers] number	 INNER JOIN
	[dbo].[ReceiveKeywords] keyword ON keyword.[PrivateNumberGuid] = number.[Guid] INNER JOIN
	@Childrens children ON keyword.[UserGuid] = children.[UserGuid] '+ @Where +';
		
SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);

	
	

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetPagedAssignedLines]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetPagedAssignedLines]
	@UserGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT,
	@PageSize INT,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE @Statement NVARCHAR(MAX) = '';

SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @Statement = '

DECLARE @Childrens TABLE([UserGuid] UNIQUEIDENTIFIER,[UserName] NVARCHAR(50));

INSERT INTO @Childrens([UserGuid],[UserName])
	SELECT [UserGuid],[UserName] FROM dbo.udfGetAllChildren(''' + CAST(@UserGuid AS VARCHAR(36)) + ''');

DELETE FROM @Childrens WHERE [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + ''';

SELECT
		[Guid] ,
		[Number] ,
		[Range] ,
		[ServiceID],
		[ServicePrice],
		[UseForm] ,
		[CreateDate],
		[ParentGuid] ,
		[OwnerGuid] ,
		[IsActive] ,
		[IsPublic] ,
		[Type],
		[UserName] INTO #Temp
FROM
	[dbo].[PrivateNumbers] number	 INNER JOIN 
	@Childrens children ON number.[OwnerGuid] = children.[UserGuid] '+ @Where +';
		
SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);

	
	

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetPagedNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetPagedNumbers]
	@UserGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX) ,
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where AS NVARCHAR(MAX) ='agent.[IsDeleted] = 0 AND number.[IsDeleted] = 0 AND [ParentGuid] = ''00000000-0000-0000-0000-000000000000'' AND number.[UserGuid] = '''+CAST(@UserGuid AS VARCHAR(36))+'''';
DECLARE	@Statement NVARCHAR(MAX) = '';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;

SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where = @Where + ' AND ' + @Query;
--------------------------------------------------

SET @Statement ='
    SELECT 
		number.*,
		agent.[Name]
		INTO #Temp
	FROM
		[dbo].[PrivateNumbers] number INNER JOIN
		[dbo].[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid] '+ @Where +';
			
	SELECT COUNT(*) AS [RowCount] FROM #Temp;
		
	SELECT * FROM #Temp';
		
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';
     
EXEC(@Statement);

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetServiceId]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetServiceId]
	@TypeServiceId INT,
	@ServiceId NVARCHAR(32)
  AS


IF(@TypeServiceId = 1)--MCIServiceId
	SELECT [ServiceID] FROM PrivateNumbers WHERE ServiceId = @ServiceId;
ELSE IF(@TypeServiceId = 2)--MTNServiceId
	SELECT [ServiceID] FROM PrivateNumbers WHERE MTNServiceId = @ServiceId;
ELSE IF(@TypeServiceId = 2)--AggServiceId
	SELECT [ServiceID] FROM PrivateNumbers WHERE AggServiceId = @ServiceId;
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetSmsSenderAgentReference]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PrivateNumbers_GetSmsSenderAgentReference]
	@PrivateNumberGuid UNIQUEIDENTIFIER
  AS
 

SELECT
			[SmsSenderAgentReference]
FROM
			[PrivateNumbers] INNER JOIN
			[SmsSenderAgents] ON [PrivateNumbers].[SmsSenderAgentGuid] = [SmsSenderAgents].[Guid]
WHERE	
			[PrivateNumbers].[Guid] = @PrivateNumberGuid


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetUserNumberGuid]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetUserNumberGuid]
 @Number NVARCHAR(32),
 @UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @Numbers TABLE([Guid] UNIQUEIDENTIFIER,[Number] nvarchar(32),[Type] INT,[IsDefault] BIT);

INSERT INTO @Numbers([Guid],[Number],[Type],[IsDefault])
EXEC dbo.PrivateNumbers_GetUserPrivateNumbersForSend @UserGuid = @UserGuid;

IF(ISNULL(@Number,'') = '')
BEGIN
	SELECT 
		[Guid]
	FROM
		@Numbers
	WHERE
		[IsDefault] = 1
END
ELSE
BEGIN
	SELECT 
		[Guid]
	FROM
		@Numbers
	WHERE
		[Number] = @Number
END
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetUserNumbers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetUserNumbers]
	@OwnerGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0 AND';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE @Statement NVARCHAR(MAX) = '';

SET @Where += ' [OwnerGuid] = ''' + CAST(@OwnerGuid AS VARCHAR(36)) + '''';

SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @Statement = '
	SELECT 
				*,
				CASE
					WHEN CONVERT(DATE,[ExpireDate]) <= CONVERT(DATE,GETDATE()) THEN 1
					ELSE 0
				END [IsExpired] INTO #Temp
	FROM
		[dbo].[PrivateNumbers] '+ @Where +';

	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetUserPrivateNumbersForReceive]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetUserPrivateNumbersForReceive]
	@UserGuid UNIQUEIDENTIFIER
  AS


SELECT 
		*
FROM
		[dbo].[PrivateNumbers]
WHERE
		[OwnerGuid] = @UserGuid AND
		[IsDeleted] = 0 AND
		[IsActive] = 1 AND
		(
			[UseForm] = 0 OR
			([UseForm] = 2 AND ISNULL([Number],'') != '')
		)


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetUserPrivateNumbersForSend]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetUserPrivateNumbersForSend]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @Parents TABLE([Guid] UNIQUEIDENTIFIER);

INSERT INTO @Parents
	SELECT [UserGuid] FROM dbo.udfGetAllParents(@UserGuid);

SELECT 
	DISTINCT [PrivateNumberGuid]
	INTO #keywords
FROM
	[dbo].[ReceiveKeywords]
WHERE
	[UserGuid] = @UserGuid;

SELECT 
		number.[Guid],
		number.[Number],
		number.[Type],
		number.[IsDefault]
FROM
		[dbo].[PrivateNumbers] number WITH (NOLOCK) INNER JOIN
		[dbo].[SmsSenderAgents] agent  WITH (NOLOCK)	ON agent.[Guid] = number.[SmsSenderAgentGuid]
WHERE
		number.[IsDeleted] = 0 AND
		agent.IsDeleted = 0 AND
		agent.IsSendActive = 1 AND
		[IsActive] = 1 AND
		CONVERT(DATE,[number].[ExpireDate]) > CONVERT(DATE,GETDATE()) AND
		(
			[OwnerGuid] = @UserGuid OR
			([OwnerGuid] IN(SELECT [Guid] FROM @Parents) AND [IsPublic] = 1) OR
			number.[Guid] IN (SELECT * FROM #keywords)
		) AND
		([UseForm] = 0 OR [UseForm] = 1 OR ([UseForm] = 2 AND ISNULL([Number],'') != ''))
ORDER BY [IsDefault] DESC

DROP TABLE #keywords;

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetUserPrivateNumbersForSendBulk]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetUserPrivateNumbersForSendBulk]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @Parents TABLE([Guid] UNIQUEIDENTIFIER);

INSERT INTO @Parents
	SELECT [UserGuid] FROM dbo.udfGetAllParents(@UserGuid);

SELECT 
	DISTINCT [PrivateNumberGuid]
	INTO #keywords
FROM
	[dbo].[ReceiveKeywords]
WHERE
	[UserGuid] = @UserGuid;

SELECT 
		number.[Guid],
		number.[Number],
		number.[Type],
		number.[IsDefault]
FROM
		[dbo].[PrivateNumbers] number WITH (NOLOCK) INNER JOIN
		[dbo].[SmsSenderAgents] agent  WITH (NOLOCK)	ON agent.[Guid] = number.[SmsSenderAgentGuid]
WHERE
		number.[IsDeleted] = 0 AND
		agent.IsDeleted = 0 AND
		--agent.IsSendActive = 1 AND
		agent.IsSendBulkActive = 1 AND
		[IsActive] = 1 AND
		CONVERT(DATE,[number].[ExpireDate]) > CONVERT(DATE,GETDATE()) AND
		(
			[OwnerGuid] = @UserGuid OR
			([OwnerGuid] IN(SELECT [Guid] FROM @Parents) AND [IsPublic] = 1) OR
			number.[Guid] IN (SELECT * FROM #keywords)
		) AND
		([UseForm] = 0 OR [UseForm] = 1 OR ([UseForm] = 2 AND ISNULL([Number],'') != ''))
ORDER BY [IsDefault] DESC

DROP TABLE #keywords;

GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_GetVASNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_GetVASNumber]
	@ServiceId NVARCHAR(32)
  AS


SELECT 
	[Guid],[Number]
FROM
	[dbo].[PrivateNumbers]
WHERE
	[IsDeleted] = 0 AND
	[ServiceID] = @ServiceId;
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_Insert]
  @Guid UNIQUEIDENTIFIER ,
	@ID BIGINT,
  @Number NVARCHAR(32) ,
	@Price DECIMAL(18,2),
	@ServiceID NVARCHAR(32),
	@MTNServiceId NVARCHAR(32),
	@AggServiceId NVARCHAR(32),
  @ServicePrice DECIMAL(18,2) ,
  @CreateDate DATETIME ,
  @ExpireDate DATETIME ,
	@Type INT,
	@Priority INT,
	@ReturnBlackList BIT,
	@SendToBlackList BIT,
	@CheckFilter BIT,
	@DeliveryBase BIT,
	@HasSLA BIT,
	@TryCount INT,
	@Range NVARCHAR(255),
	@Regex NVARCHAR(255),
	@UseForm INT,
	@ParentGuid UNIQUEIDENTIFIER,
	@OwnerGuid UNIQUEIDENTIFIER,
	@IsRoot BIT,
  @IsActive BIT ,
	@IsDefault BIT,
	@IsPublic BIT,
	@SendCount BIGINT,
	@RecieveCount BIGINT,
	@SuccessCount BIGINT,
  @SmsSenderAgentGuid UNIQUEIDENTIFIER,
	@SmsTrafficRelayGuid UNIQUEIDENTIFIER,
	@DeliveryTrafficRelayGuid UNIQUEIDENTIFIER,
  @UserGuid UNIQUEIDENTIFIER
  AS
 
  INSERT  INTO [PrivateNumbers]
          ( [Guid] ,
            [Number] ,
						[Price],
						[ServiceID],
						[MTNServiceId],
						[AggServiceId],
            [ServicePrice] ,
            [CreateDate] ,
            [ExpireDate] ,
						[Type],
						[Priority],
						[ReturnBlackList],
						[SendToBlackList],
						[CheckFilter],
						[DeliveryBase],
						[HasSLA],
						[TryCount],
						[Range],
						[Regex],
						[UseForm],
						[ParentGuid],
						[OwnerGuid],
						[IsRoot],
            [IsActive] ,
						[IsDefault],
						[IsPublic],
            [IsDeleted],
            [SmsSenderAgentGuid],
            [UserGuid])
  VALUES  ( @Guid ,
            @Number ,
						@Price,
						@ServiceID,
						@MTNServiceId,
						@AggServiceId,
            @ServicePrice ,
            @CreateDate ,
            @ExpireDate ,
						@Type,
						@Priority,
						@ReturnBlackList,
						@SendToBlackList,
						@CheckFilter,
						@DeliveryBase,
						@HasSLA,
						@TryCount,
						@Range,
						@Regex,
						@UseForm,
						@ParentGuid,
						@OwnerGuid,
						@IsRoot,
            @IsActive ,
						@IsDefault,
						@IsPublic,
            0,
            @SmsSenderAgentGuid,
            @UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_SetPublicNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_SetPublicNumber]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE @IsPublic BIT;

SELECT @IsPublic = [IsPublic] FROM [dbo].[PrivateNumbers] WHERE [Guid] = @Guid;

IF(@IsPublic = 1) SET @IsPublic = 0; ELSE SET	@IsPublic = 1;

UPDATE  [dbo].[PrivateNumbers]
SET     [IsPublic] = @IsPublic
WHERE   [Guid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_UpdateExpireDate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_UpdateExpireDate]
	@Guid UNIQUEIDENTIFIER,
	@ExpireDate DATETIME
  AS


UPDATE [dbo].[PrivateNumbers]
SET
	[ExpireDate] = @ExpireDate
WHERE
	[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_UpdateNumber]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_UpdateNumber]
  @Guid UNIQUEIDENTIFIER ,
  @Number NVARCHAR(32) ,
	@Price DECIMAL(18,2),
	@ServiceID NVARCHAR(32),
	@MTNServiceId NVARCHAR(32),
	@AggServiceId NVARCHAR(32),
  @ServicePrice DECIMAL(18,2) ,
	@ExpireDate DATETIME,
	@Type INT,
	@Priority INT,
	@ReturnBlackList BIT,
	@SendToBlackList BIT,
	@CheckFilter BIT,
	@DeliveryBase BIT,
	@HasSLA BIT,
	@TryCount INT,
	@Range NVARCHAR(255),
	@Regex NVARCHAR(255),
	@UseForm INT,
	@IsRoot BIT,
  @IsActive BIT ,
	@IsPublic BIT,
  @SmsSenderAgentGuid UNIQUEIDENTIFIER
  AS
 
  UPDATE  [PrivateNumbers]
  SET     [Number] = @Number ,
					[Price] = @Price,
					[ServiceID] = @ServiceID,
					[MTNServiceId] = @MTNServiceId,
					[AggServiceId] = @AggServiceId,
					[ServicePrice] = @ServicePrice,
					[ExpireDate] = @ExpireDate,
					[Type] = @Type ,
					[Priority] = @Priority ,
					[ReturnBlackList] = @ReturnBlackList ,
					[SendToBlackList] = @SendToBlackList,
					[CheckFilter] = @CheckFilter ,
					[DeliveryBase] = @DeliveryBase ,
					[HasSLA] = @HasSLA,
					[TryCount] = @TryCount,
					[Range] = @Range ,
					[Regex] = @Regex ,
					[UseForm] = @UseForm ,
					[IsRoot] = @IsRoot,
          [IsActive] = @IsActive ,
					[IsPublic] = @IsPublic ,
          [SmsSenderAgentGuid] = @SmsSenderAgentGuid
  WHERE   [Guid] = @Guid;

	UPDATE [dbo].[PrivateNumbers]
	SET		 [Type] = @Type ,
				 [Priority] = @Priority ,
				 [ReturnBlackList] = @ReturnBlackList ,
				 [SendToBlackList] = @SendToBlackList,
				 [CheckFilter] = @CheckFilter ,
				 [DeliveryBase] = @DeliveryBase ,
				 [TryCount] = @TryCount,
				 [HasSLA] = @HasSLA,
				 [IsActive] = @IsActive ,
				 [SmsSenderAgentGuid] = @SmsSenderAgentGuid
	WHERE	 [ParentGuid] = [Guid];
GO
/****** Object:  StoredProcedure [dbo].[PrivateNumbers_UpdateTrafficRelay]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[PrivateNumbers_UpdateTrafficRelay]
	@Guid UNIQUEIDENTIFIER,
	@SmsTrafficRelayGuid UNIQUEIDENTIFIER,
	@DeliveryTrafficRelay UNIQUEIDENTIFIER
  AS


UPDATE
	[dbo].[PrivateNumbers]
SET
	[SmsTrafficRelayGuid] = @SmsTrafficRelayGuid,
	[DeliveryTrafficRelayGuid] = @DeliveryTrafficRelay
WHERE
	[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[Recipients_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Recipients_Insert]
	@Receivers NVARCHAR(MAX),
	@ScheduledSmsGuid UNIQUEIDENTIFIER
  AS


BEGIN TRY
	DECLARE @Recipients TABLE([Guid] UNIQUEIDENTIFIER,[Mobile] NVARCHAR(16),[Operator] TINYINT,[ScheduledSmsGuid] UNIQUEIDENTIFIER,[IsBlackList] BIT);

	INSERT INTO @Recipients
	        ([Guid] ,
	         [Mobile] ,
	         [Operator] ,
	         [ScheduledSmsGuid] ,
	         [IsBlackList])
				 SELECT
				 		NEWID(),
				 		[Item],
				 		[dbo].[GetNumberOperator]([Item]),
				 		@ScheduledSmsGuid,
				 		0
				 FROM
				 	[dbo].[SplitString](@Receivers,',')
				 OPTION (MAXRECURSION 0);

	INSERT INTO [dbo].[Recipients]
					 ([Guid] ,
						[Mobile] ,
						[Operator] ,
						[IsBlackList] ,
						[ScheduledSmsGuid])
					SELECT
						[Guid],
						[Mobile] ,
						[Operator] ,
						[IsBlackList],
						[ScheduledSmsGuid]
					FROM @Recipients;
END TRY
BEGIN CATCH
	THROW;
END CATCH

GO
/****** Object:  StoredProcedure [dbo].[Recipients_InsertFromPhonebook]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Recipients_InsertFromPhonebook]
	@Groups NVARCHAR(MAX),
	@ScheduledSmsGuid UNIQUEIDENTIFIER
  AS


SET NOCOUNT ON;

BEGIN TRY
	DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER;

	SELECT
		[Item]
		INTO #Temp
	FROM
	[dbo].[SplitString](@Groups,',')
	OPTION (MAXRECURSION 0)
  
	SELECT
		DISTINCT [CellPhone] [Mobile],
		[Operator] [Operator],
		0 [IsBlackList]
		INTO #Recipients
	FROM
		[dbo].[PhoneNumbers] WITH (NOLOCK)
	WHERE
		[IsDeleted] = 0 AND
		[Operator] > 0 AND
		[PhoneBookGuid] IN(SELECT * FROM #Temp);

	INSERT INTO [dbo].[Recipients]
					([Guid] ,
						[Mobile] ,
						[Operator] ,
						[IsBlackList] ,
						[ScheduledSmsGuid])
					SELECT
						NEWID(),
						[Mobile] ,
						[Operator] ,
						[IsBlackList],
						@ScheduledSmsGuid
					FROM #Recipients;
	
	DROP TABLE #Recipients;
	DROP TABLE #Temp;
END TRY
BEGIN CATCH
	THROW;
END CATCH



GO
/****** Object:  StoredProcedure [dbo].[Recipients_InsertTable]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Recipients_InsertTable]
	@Receivers [dbo].[Recipient] READONLY,
	@ScheduledSmsGuid UNIQUEIDENTIFIER
  AS


SELECT
	NEWID() [Guid],
	[Mobile] [Mobile],
	[dbo].[GetNumberOperator]([Mobile]) [Operator],
	@ScheduledSmsGuid [ScheduledSmsGuid],
	0 [IsBlackList]
	INTO #Recipients
FROM
	@Receivers

INSERT INTO [dbo].[Recipients]
				([Guid] ,
					[Mobile] ,
					[Operator] ,
					[IsBlackList] ,
					[ScheduledSmsGuid])
				SELECT
					[Guid],
					[Mobile] ,
					[Operator] ,
					[IsBlackList],
					[ScheduledSmsGuid]
				FROM #Recipients;
	
DROP TABLE #Recipients;


GO
/****** Object:  StoredProcedure [dbo].[RegularContents_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_Delete]
	@Guid UNIQUEIDENTIFIER
  AS

UPDATE [dbo].[RegularContents] SET [IsDeleted] = 1 WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[RegularContents_GetPagedRegularContents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_GetPagedRegularContents]
  @UserGuid UNIQUEIDENTIFIER ,
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS
 
DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0';
DECLARE	@StartRow INT = (@PageNo - 1) * @PageSize;

IF (@Where != '') 
	SET @Where += ' AND';
SET @Where += ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + '''';

SET @Where = ' WHERE ' + @Where;

--------------------------------------------------
DECLARE @Statement NVARCHAR(MAX) = '';

SET @Statement = '
		SELECT
			*
			INTO #Temp
		FROM
			[dbo].[RegularContents] '+ @Where +';
		
		SELECT COUNT(*) [RowCount] FROM #Temp;
		SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +=' 
			ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);



GO
/****** Object:  StoredProcedure [dbo].[RegularContents_GetRegularContentFileType]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_GetRegularContentFileType]
  AS

DECLARE @Now DATETIME = GETDATE();

SELECT 
	*
FROM
	[dbo].[RegularContents]
WHERE
	[Type] = 1 AND
	[IsDeleted] = 0 AND
	[IsActive] = 1 AND
	(
		([StartDateTime]	<= @Now AND
		 [EndDateTime] >= @Now) OR
		 [StartDateTime] = [EndDateTime]
	) AND
	CONVERT(DATE,[EffectiveDateTime]) = CONVERT(DATE,GETDATE())
GO
/****** Object:  StoredProcedure [dbo].[RegularContents_GetRegularContentForProcess]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_GetRegularContentForProcess]
  AS


DECLARE @Now DATETIME = GETDATE();

SELECT 
	*
FROM
	[dbo].[RegularContents]
WHERE
	[Type] != 1 AND
	[IsDeleted] = 0 AND
	[IsActive] = 1 AND
	(
		([StartDateTime]	<= @Now AND
		 [EndDateTime] >= @Now) OR
		 [StartDateTime] = [EndDateTime]
	) AND
	[EffectiveDateTime] <= @Now
	

GO
/****** Object:  StoredProcedure [dbo].[RegularContents_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(128),
	@Type TINYINT,
	@Config NVARCHAR(MAX),
	@IsActive BIT,
	@PeriodType TINYINT,
	@Period INT,
	@EffectiveDateTime DATETIME,
	@WarningType TINYINT,
	@CreateDate DATETIME,
	@StartDateTime DATETIME,
	@EndDateTime DATETIME,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS


INSERT INTO [dbo].[RegularContents]
        ([Guid] ,
         [Title] ,
         [Type] ,
         [Config] ,
         [IsActive] ,
         [PeriodType] ,
         [Period] ,
         [EffectiveDateTime] ,
         [WarningType] ,
         [CreateDate] ,
         [StartDateTime] ,
         [EndDateTime] ,
				 [IsDeleted],
				 [PrivateNumberGuid],
         [UserGuid])
				VALUES
				(@Guid,
				 @Title,
				 @Type,
				 @Config,
				 @IsActive,
				 @PeriodType,
				 @Period,
				 @EffectiveDateTime,
				 @WarningType,
				 @CreateDate,
				 @StartDateTime,
				 @EndDateTime,
				 0,
				 @PrivateNumberGuid,
				 @UserGuid)
GO
/****** Object:  StoredProcedure [dbo].[RegularContents_SendDBContentToReceiver]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_SendDBContentToReceiver]
	@RegularContentGuid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@SmsText NVARCHAR(255),
	@PeriodType TINYINT,
	@Period INT,
	@EffectiveDateTime DATETIME
  AS

DECLARE @Now DATETIME = GETDATE();
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @PhoneBookGuids NVARCHAR(MAX);
DECLARE @NextDateTime DATETIME = @EffectiveDateTime;

SET @PhoneBookGuids = (SELECT [PhoneBookGuid] FROM [dbo].[PhoneBookRegularContents] WHERE [RegularContentGuid] = @RegularContentGuid FOR XML PATH('Node'), ROOT('Root'));
SET @PhoneBookGuids = [dbo].[JoinString](@PhoneBookGuids,',','PhoneBookGuid');

SET @SmsLen = dbo.GetSmsCount(@SmsText);
IF(dbo.[HasUniCodeCharacter](@SmsText)=1)
	SET @Encoding = 2--UTF8
ELSE
	SET @Encoding = 1--Default

EXEC [dbo].[ScheduledSmses_InsertGroupSms]
		@Guid = @NewGuid,
    @PrivateNumberGuid = @PrivateNumberGuid,
    @ReferenceGuid = @PhoneBookGuids,
    @SmsText = @SmsText,
    @SmsLen = @SmsLen,
    @PresentType = 1, -- Normal
    @Encoding = @Encoding,
    @TypeSend = 9, -- SendRegularContentSms
    @Status = 1, -- Stored
    @DateTimeFuture = @Now,
    @UserGuid = @UserGuid,
    @IPAddress = N'',
    @Browser = N''

SET	@NextDateTime = CASE 
												WHEN @PeriodType = 1 THEN DATEADD(MINUTE,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 2 THEN DATEADD(HOUR,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 3 THEN DATEADD(DAY,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 4 THEN DATEADD(WEEK,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 5 THEN DATEADD(MONTH,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 6 THEN DATEADD(YEAR,@Period,@EffectiveDateTime)
											END
	
UPDATE [dbo].[RegularContents] SET [EffectiveDateTime] = @NextDateTime WHERE [Guid] = @RegularContentGuid;
GO
/****** Object:  StoredProcedure [dbo].[RegularContents_SendURLContentToReceiver]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_SendURLContentToReceiver]
	@RegularContentGuid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@SmsText NVARCHAR(255),
	@PeriodType TINYINT,
	@Period INT,
	@EffectiveDateTime DATETIME
  AS

DECLARE @Now DATETIME = GETDATE();
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @PhoneBookGuids NVARCHAR(MAX);
DECLARE @NextDateTime DATETIME = @EffectiveDateTime;

SET @PhoneBookGuids = (SELECT [PhoneBookGuid] FROM [dbo].[PhoneBookRegularContents] WHERE [RegularContentGuid] = @RegularContentGuid FOR XML PATH('Node'), ROOT('Root'));
SET @PhoneBookGuids = [dbo].[JoinString](@PhoneBookGuids,',','PhoneBookGuid');

SET @SmsLen = dbo.GetSmsCount(@SmsText);
IF(dbo.[HasUniCodeCharacter](@SmsText)=1)
	SET @Encoding = 2--UTF8
ELSE
	SET @Encoding = 1--Default

EXEC [dbo].[ScheduledSmses_InsertGroupSms]
		@Guid = @NewGuid,
    @PrivateNumberGuid = @PrivateNumberGuid,
    @ReferenceGuid = @PhoneBookGuids,
    @SmsText = @SmsText,
    @SmsLen = @SmsLen,
    @PresentType = 1, -- Normal
    @Encoding = @Encoding,
    @TypeSend = 9, -- SendRegularContentSms
    @Status = 1, -- Stored
    @DateTimeFuture = @Now,
    @UserGuid = @UserGuid,
    @IPAddress = N'',
    @Browser = N''

SET	@NextDateTime = CASE 
												WHEN @PeriodType = 1 THEN DATEADD(MINUTE,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 2 THEN DATEADD(HOUR,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 3 THEN DATEADD(DAY,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 4 THEN DATEADD(WEEK,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 5 THEN DATEADD(MONTH,@Period,@EffectiveDateTime)
												WHEN @PeriodType = 6 THEN DATEADD(YEAR,@Period,@EffectiveDateTime)
											END
	
UPDATE [dbo].[RegularContents] SET [EffectiveDateTime] = @NextDateTime WHERE [Guid] = @RegularContentGuid;
GO
/****** Object:  StoredProcedure [dbo].[RegularContents_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RegularContents_Update]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(128),
	@Type TINYINT,
	@Config NVARCHAR(MAX),
	@IsActive BIT,
	@WarningType TINYINT,
	@PrivateNumberGuid UNIQUEIDENTIFIER
  AS


UPDATE [dbo].[RegularContents]
SET
	[Title] = @Title,
	[Type] = @Type,
	[Config] = @Config,
	[IsActive] = @IsActive,
	[WarningType] = @WarningType,
	[PrivateNumberGuid] = @PrivateNumberGuid
WHERE
	[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[Roles_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Roles_Delete]
	@Guid UNIQUEIDENTIFIER
  AS

UPDATE 
	[dbo].[Roles]
SET
	[IsDeleted] = 1
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Roles_GetDefaultRoleGuid]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Roles_GetDefaultRoleGuid]
 @Domain NVARCHAR(64)
  AS


DECLARE @UserGuid UNIQUEIDENTIFIER;
SELECT @UserGuid = [UserGuid] FROM [dbo].[Domains] WHERE [IsDeleted] = 0 AND [Name] = @Domain;

SELECT TOP 1 [Guid] FROM  [dbo].[Roles]
WHERE [IsDefault] = 1 AND [UserGuid] = @UserGuid;


GO
/****** Object:  StoredProcedure [dbo].[Roles_GetPackage]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Roles_GetPackage]
	@ID INT
  AS


SELECT * FROM [dbo].[Roles] WHERE [IsDeleted] = 0 AND [ID] = @ID;
GO
/****** Object:  StoredProcedure [dbo].[Roles_GetRoles]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Roles_GetRoles]
	@UserGuid UNIQUEIDENTIFIER
  AS

	SELECT 
		[Guid],
		[Title],
		[CreateDate],
		[IsDefault],
		[Description],
		[Price],
		[IsSalePackage]
	FROM
	 [dbo].[Roles]
	WHERE
	 [IsDeleted] = 0 AND
	 [UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Roles_InsertRole]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Roles_InsertRole]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(64),
	@Priority TINYINT,
	@CreateDate DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IsDefault BIT,
	@Description NVARCHAR(MAX),
	@Price DECIMAL(18,2),
	@IsSalePackage BIT
  AS


IF(@IsDefault = 1)
	UPDATE [dbo].[Roles] SET [IsDefault] = 0 WHERE [UserGuid] = @UserGuid;

INSERT INTO [dbo].[Roles]
						([Guid],
						 [Title],
						 [Priority],
						 [CreateDate],
						 [UserGuid],
						 [IsDeleted],
						 [IsDefault],
						 [Description],
						 [Price],
						 [IsSalePackage])
			 VALUES
						(@Guid,
						 @Title,
						 @Priority,
						 @CreateDate,
						 @UserGuid,
						 0,
						 @IsDefault,
						 @Description,
						 @Price,
						 @IsSalePackage)


GO
/****** Object:  StoredProcedure [dbo].[Roles_UpdateRole]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Roles_UpdateRole]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(64),
	@Priority TINYINT,
	@IsDefault BIT,
	@Description NVARCHAR(MAX),
	@Price DECIMAL(18,2),
	@IsSalePackage BIT
  AS


DECLARE @UserGuid UNIQUEIDENTIFIER;

SELECT @UserGuid = [UserGuid] FROM [dbo].[Roles] WHERE [Guid] = @Guid;

IF(@IsDefault = 1)
	UPDATE [dbo].[Roles] SET [IsDefault] = 0 WHERE [UserGuid] = @UserGuid;

UPDATE
	[dbo].[Roles]
SET
	[Title] = @Title,
	[Priority] = @Priority,
	[IsDefault]=@IsDefault,
	[Description] =@Description,
	[Price] =@Price,
	[IsSalePackage] =@IsSalePackage
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[RoleServices_DeleteServicesOfRole]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[RoleServices_DeleteServicesOfRole]
	@RoleGuid UNIQUEIDENTIFIER
  AS

	DELETE FROM 
		[dbo].[RoleServices]
	WHERE 
		[RoleGuid]=@RoleGuid;


GO
/****** Object:  StoredProcedure [dbo].[RoleServices_GetRoleServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RoleServices_GetRoleServices]
		@UserGuid UNIQUEIDENTIFIER
  AS

	SELECT * FROM [dbo].[Roles]
	WHERE [IsSalePackage] = 1 AND
				[UserGuid] = @UserGuid;

	SELECT 
		RoleServices.[Guid],
		RoleServices.[ID],
		ISNULL(Roles.Price,0),
		Roles.[Description],
		RoleServices.[ServiceGuid],
		RoleServices.[RoleGuid],
		[Services].[Title]
	FROM         
		RoleServices LEFT OUTER JOIN
		[Services] ON RoleServices.[ServiceGuid] = [Services].[Guid] LEFT OUTER JOIN
		Roles ON RoleServices.[RoleGuid] = Roles.[Guid]
	WHERE 
		Roles.[IsSalePackage] = 1 AND
		Roles.[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[RoleServices_GetServiceOfRole]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RoleServices_GetServiceOfRole]
	@UserGuid UNIQUEIDENTIFIER,
  @RoleGuid UNIQUEIDENTIFIER
  AS

DECLARE @UserRoleGuid UNIQUEIDENTIFIER;

SELECT @UserRoleGuid=[RoleGuid] FROM [dbo].[Users] WHERE [Guid]=@UserGuid;


SELECT
		[dbo].[RoleServices].[ServiceGuid],
		[RoleServices].[Price],
		[RoleServices].[IsDefault]
		INTO #RoleServices
FROM
		[RoleServices]
WHERE 
		[RoleServices].[RoleGuid] = @RoleGuid
		
SELECT 
			[Services].[Guid],
			[Services].[Title],
			[RoleServices].[RoleGuid],
			groups.[Title] AS [GroupTitle],
			groups.[Guid] AS [GroupGuid]
			INTO #UserRoleServices
FROM 
		[Services] INNER JOIN
		[RoleServices] ON [Services].[Guid] = [RoleServices].[ServiceGuid]  INNER JOIN
		[ServiceGroups] groups ON [Services].[ServiceGroupGuid] = groups.[Guid]
WHERE 
		[RoleServices].[RoleGuid] = @UserRoleGuid AND
		[Services].[IsDeleted]=0 AND
		groups.[IsDeleted]=0
ORDER BY
		[Services].[Order]
		
SELECT 
	  [Guid] ,
      [Title] ,
      [GroupTitle] ,
      [GroupGuid] ,
      [Price],
      CASE WHEN [IsDefault] = 1 THEN 1
			ELSE 0
			END [IsDefault]
FROM 
	#UserRoleServices
	LEFT JOIN #RoleServices ON #UserRoleServices.[Guid] = #RoleServices.[ServiceGuid]
ORDER BY
	[GroupTitle]

DROP TABLE #RoleServices;
DROP TABLE #UserRoleServices;


GO
/****** Object:  StoredProcedure [dbo].[RoleServices_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[RoleServices_Insert]
	@XmlString NVARCHAR(MAX),
	@RoleGuid UNIQUEIDENTIFIER
  AS

	DECLARE @XMLDocPointer INT;
  EXEC sp_xml_preparedocument @XMLDocPointer OUTPUT, @XmlString

	INSERT INTO [dbo].[RoleServices]
								([Guid],
								 [Price], 
								 [ServiceGuid], 
								 [RoleGuid],
								 [IsDefault])
					SELECT 
						NEWID(),
						[Price],
						[ServiceGuid],
						@RoleGuid,
						[IsDefault]
					FROM
						OPENXML(@XMLDocPointer,'/NewDataSet/Table',6)
					WITH
						([ServiceGuid] UNIQUEIDENTIFIER,
						 [Price] DECIMAL(18,2),
						 [IsDefault] BIT)
	
	EXEC sp_xml_removedocument @XMLDocPointer
GO
/****** Object:  StoredProcedure [dbo].[Routes_GetPagedRoutes]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Routes_GetPagedRoutes]
	@AgentGuid UNIQUEIDENTIFIER
  AS

	SELECT
		rout.[Guid] ,
		rout.[Name],
		rout.[Username] ,
		opt.[Name] AS [Operator]
	FROM
		[dbo].[Routes] rout INNER JOIN
		[dbo].[Operators] opt ON rout.[OperatorID] = opt.[ID]
	WHERE
		rout.[SmsSenderAgentGuid] = @AgentGuid;

GO
/****** Object:  StoredProcedure [dbo].[Routes_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Routes_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Name NVARCHAR(128),
	@Username NVARCHAR(32),
	@Password NVARCHAR(32),
	@Domain NVARCHAR(32),
	@Link NVARCHAR(512),
	@QueueLength INT,
	@SmsSenderAgentGuid UNIQUEIDENTIFIER,
	@OperatorID TINYINT
  AS


INSERT INTO [dbo].[Routes]
        ([Guid] ,
         [Name] ,
         [Username] ,
         [Password] ,
         [Domain] ,
				 [Link],
				 [QueueLength],
         [SmsSenderAgentGuid] ,
         [OperatorID])
				VALUES
				(@Guid,
				 @Name,
				 @Username,
				 @Password,
				 @Domain,
				 @Link,
				 @QueueLength,
				 @SmsSenderAgentGuid,
				 @OperatorID)

GO
/****** Object:  StoredProcedure [dbo].[Routes_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Routes_Update]
	@Guid UNIQUEIDENTIFIER,
	@Name NVARCHAR(128),
	@Username NVARCHAR(32),
	@Password NVARCHAR(32),
	@Domain NVARCHAR(32),
	@Link NVARCHAR(512),
	@QueueLength INT,
	@SmsSenderAgentGuid UNIQUEIDENTIFIER,
	@OperatorID TINYINT
  AS


UPDATE
	[dbo].[Routes]
SET
	[Name] = @Name,
	[Username] = @Username,
	[Password] = @Password,
	[Domain] = @Domain,
	[Link] = @Link,
	[QueueLength] = @QueueLength,
	[SmsSenderAgentGuid] = @SmsSenderAgentGuid,
	[OperatorID] = @OperatorID
WHERE
	[Guid] = @Guid;

GO
/****** Object:  StoredProcedure [dbo].[ScheduledBulkSmses_GetQueue]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledBulkSmses_GetQueue]
	@Count INT
  AS


DECLARE @TimeNow TIME = CAST(GETDATE() as TIME(0));

SELECT 
	TOP (@Count)
	sch.[Guid],
	sch.[ID],
	7 [TypeSend],--type send
	sch.[Status],
	sch.[PrivateNumberGuid],
	sch.[UserGuid],
	sch.[SmsSenderAgentReference],
	agent.[SendBulkIsAutomatic],
	agent.[SendSmsAlert],
	agent.[StartSendTime],
	agent.[EndSendTime]
FROM
	[dbo].[ScheduledBulkSmses] sch WITH (NOLOCK) INNER JOIN
	[dbo].[PrivateNumbers] number WITH (NOLOCK) ON sch.[PrivateNumberGuid] = number.[Guid] INNER JOIN
	[dbo].[SmsSenderAgents] agent WITH (NOLOCK) ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	[Status] IN (1,--Stored
							 2,--Extracting
							 --3,--FailedExtract
							 4,--Ready
							 6,--Completed
							 7,--Extracted
							 9--Confirmed
							 )AND
	--(
	--	agent.[StartSendTime] = agent.[EndSendTime] OR
	--	(@TimeNow BETWEEN [StartSendTime] AND [EndSendTime])
	--) AND
	[SendDateTime] <= GETDATE()
GO
/****** Object:  StoredProcedure [dbo].[ScheduledBulkSmses_ProcessRequest]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledBulkSmses_ProcessRequest]
	@Guid UNIQUEIDENTIFIER,
	@ID BIGINT,
	@Status INT,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@SmsSenderAgentReference INT,
	@SendBulkIsAutomatic BIT,
	@SendSmsAlert BIT,
	@StartSendTime TIME,
	@EndSendTime TIME
  AS


DECLARE @Number nvarchar(50);
DECLARE @IsUnicode INT;
DECLARE @IsFlash INT;
DECLARE @Priority INT;
DECLARE @DeliveryBase BIT;
DECLARE @SmsText NVARCHAR(MAX) = '';
DECLARE @SmsLen INT;
DECLARE	@ErrorCode INT;
DECLARE @trancount INT;
DECLARE @Username NVARCHAR(32);
DECLARE @TimeNow TIME(0) = CAST(GETDATE() as TIME(0));

SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION

	IF(@SendBulkIsAutomatic = 0 AND @Status = 1) --Send Bulk Is Not Automatic
	BEGIN
		SELECT @Username = [UserName] FROM [Users] WHERE [Guid] = @UserGuid
		SET @SmsText = N'درخواست ارسال مجازی بالک ثبت شد' + CHAR(13)+ N'کاربر'+ @Username + CHAR(13) + N'شناسه:' + CAST(@ID AS NVARCHAR(16));
		EXEC [dbo].[Users_SendSmsForParentOfUser]
					@UserGuid = @UserGuid,
					@SmsText = @SmsText

		UPDATE [ScheduledSmses] SET [Status] = 8 	WHERE [Guid] = @Guid; --WatingForConfirm
		UPDATE [ScheduledBulkSmses] SET [Status] = 8 	WHERE [Guid] = @Guid; --WatingForConfirm

		COMMIT TRANSACTION;
		RETURN;
	END

	IF(@Status IN (1,9))--Stored,Confirmed
	BEGIN
		EXEC @ErrorCode = [dbo].[ScheduledSmses_CheckSendSmsException] @Guid;
		IF(@ErrorCode != 0)
		BEGIN
			UPDATE [dbo].[ScheduledSmses] SET [SmsSendFaildType] = @ErrorCode,[Status] = 5--Failed
			WHERE [Guid] = @Guid;

			
			UPDATE [dbo].[ScheduledBulkSmses] SET [Status] = 5 --Failed
			WHERE [Guid] = @Guid;

			COMMIT TRANSACTION;
			RETURN;
		END

		EXEC [dbo].[Outboxes_DecreaseSmsSendPrice] @Guid;

		UPDATE [dbo].[ScheduledBulkSmses] SET [Status] = 7--Extracted
		WHERE [Guid] = @Guid;

		UPDATE [dbo].[ScheduledSmses] SET [Status] = 7--Extracted
		WHERE [Guid] = @Guid;
	END
	ELSE IF (@Status = 4)--Ready
	BEGIN
		EXEC [dbo].[ScheduledBulkSmses_SendSms] @Guid = @Guid;
	END
	ELSE IF(@Status = 6)--Completed
	BEGIN
		IF(@SendSmsAlert = 1 AND (@TimeNow NOT BETWEEN @StartSendTime AND @EndSendTime)) --Send message for alert
		BEGIN
			SELECT @Username = [UserName] FROM [Users] WHERE [Guid] = @UserGuid
			SET @SmsText = N'درخواست ارسال مجازی بالک ثبت شد' + CHAR(13)+ N'کاربر'+ @Username + CHAR(13) + N'شناسه:' + CAST(@ID AS NVARCHAR(16));
			EXEC [dbo].[Users_SendSmsForParentOfUser]
						@UserGuid = @UserGuid,
						@SmsText = @SmsText
		END

		DELETE [dbo].[ScheduledSmses] WHERE [Guid] = @Guid;
		DELETE [dbo].[BulkRecipient] WHERE [ScheduledBulkSmsGuid] = @Guid;
		DELETE [dbo].[ScheduledBulkSmses] WHERE [Guid] = @Guid;
	END
	ELSE IF(@Status = 7)--Extracted
	BEGIN
		SELECT 
			@Number = [Number],
			@Priority = [Priority],
			@DeliveryBase = [DeliveryBase]
		FROM 
			[dbo].[PrivateNumbers] WITH (NOLOCK)
		WHERE
			[Guid] = @PrivateNumberGuid;

		INSERT INTO [dbo].[Outboxes]
							( [Guid],
								[ID],
								[ExportDataStatus],
								[SendStatus],
								[SavedReceiverCount],
								[PrivateNumberGuid],
								[SmsSenderAgentReference],
								[SenderId],
								[SmsPriority],
								[IsUnicode],
								[SmsLen],
								[Price],
								[ReceiverCount],
								[SmsText],
								[SendingTryCount],
								[SentDateTime],
								[CreateDate],
								[DeliveryNeeded],
								[IsFlash],
								[SmsSendType],
								[RequestXML],
								[UserGuid])
					SELECT 
								@Guid,
								[ID],
								1,--None
								CASE @SendBulkIsAutomatic
										 WHEN 0 THEN 10--WatingForConfirm
										 WHEn 1 THEN 2--WatingForSend
								END,
								0,
								[PrivateNumberGuid],
								@SmsSenderAgentReference,
								@Number,
								@Priority,
								CASE 
									WHEN [Encoding] = 2 THEN 1
									ELSE 0
								END,
								[SmsLen],
								[Price],
								[ReceiverCount],
								[SmsText],
								0,
								GETDATE(),
								GETDATE(),
								@DeliveryBase,
								CASE 
									WHEN [PresentType] = 0 THEN 1
									ELSE 0
								END,
								7,--bulk
								[RequestXML],
								[UserGuid]
					FROM 
							[dbo].[ScheduledBulkSmses] WITH (NOLOCK)
					WHERE [Guid] = @Guid;

		UPDATE [dbo].[ScheduledBulkSmses] SET [Status] = 4 --Ready
		WHERE [Guid] = @Guid;
	END

	COMMIT TRANSACTION
END TRY
BEGIN CATCH

	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE())+';XACT_STATE():'+CAST(XACT_STATE() AS NVARCHAR(2))+';TranCount:'+CAST(@@TRANCOUNT AS NVARCHAR(2));

  IF (XACT_STATE() = -1)
    ROLLBACK TRANSACTION;
  IF (XACT_STATE() = 1)
    COMMIT TRANSACTION;

	DECLARE @FailedType INT = 9;--SendError
	DECLARE @State INT = 3;

	IF(ERROR_NUMBER() = 60000)--NotEnoughCredit
	BEGIN
		SET @FailedType = 1;
		SET @State = 5;
	END
	ELSE IF(ERROR_NUMBER() = 61000)--RecipientNotExist
		SET @State = 5;--Failed
	ELSE
		SET @State = 3;--FailedExtract

 UPDATE dbo.ScheduledSmses 
 SET 
 	[SmsSendFaildType] = @FailedType,
 	[SmsSendError] = ERROR_MESSAGE(),
	[Status] = @State
 WHERE [Guid] = @Guid;

 UPDATE dbo.ScheduledBulkSmses SET [Status] = @State --Failed
 WHERE [Guid] = @Guid;

 IF(@FailedType!=1)
 BEGIN
	DELETE FROM dbo.BulkRecipient WHERE [ScheduledBulkSmsGuid] = @Guid;
	DELETE FROM [dbo].[ScheduledBulkSmses] WHERE [Guid] = @Guid;
 END

 EXEC [dbo].[InsertLog]
		@Type = 2, --Error
		@Source = 'BulkProcessRequest',
		@Name = '' ,
		@Text = @Text,
		@IP = '',
		@Browser = '',
		@ReferenceGuid = @Guid,
		@UserGuid = '00000000-0000-0000-0000-000000000000';

END CATCH; 

GO
/****** Object:  StoredProcedure [dbo].[ScheduledBulkSmses_SendSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledBulkSmses_SendSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE @Id BIGINT;
DECLARE	@Number nvarchar(32);
DECLARE	@IsUnicode INT;
DECLARE	@IsFlash INT;
DECLARE	@PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE	@SmsSenderAgentReference INT;
DECLARE	@UserGuid UNIQUEIDENTIFIER;
DECLARE	@QueueName NVARCHAR(32);
DECLARE	@QueueType NVARCHAR(32);
DECLARE	@QueueLength INT;
DECLARE	@Username NVARCHAR(32);
DECLARE	@Password NVARCHAR(32);
DECLARE	@Domain NVARCHAR(32);
DECLARE	@SendLink NVARCHAR(512);
DECLARE	@ReceiveLink NVARCHAR(512);
DECLARE	@DeliveryLink NVARCHAR(512);
DECLARE	@Link NVARCHAR(512);
DECLARE	@RouteActive BIT;
DECLARE	@SmsText NVARCHAR(MAX) = '';
DECLARE	@SmsLen INT;
DECLARE	@AgentGuid UNIQUEIDENTIFIER;
DECLARE	@IsSmpp BIT;
DECLARE	@OperatorName NVARCHAR(32);
DECLARE	@ServiceId NVARCHAR(32);
DECLARE	@PageNo INT = 1;
DECLARE	@TryCount INT;
DECLARE	@TypeSend INT;
DECLARE @IsRemoteQueue INT;
DECLARE @RemoteQueueIP NVARCHAR(32);
DECLARE @Message XML;

DECLARE @RecipientGuid UNIQUEIDENTIFIER;
DECLARE @ZoneGuid UNIQUEIDENTIFIER;
DECLARE @Prefix NVARCHAR(11);
DECLARE @ZipCode NVARCHAR(8);
DECLARE @NumberType INT;
DECLARE @Operator INT;
DECLARE @SendCount INT;
DECLARE @FromIndex INT;
DECLARE @ScopeCount INT;

DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @Where NVARCHAR(MAX)='WHERE [ZoneGuid] IN (SELECT [Guid] FROM  @ZoneGuids )';


SET NOCOUNT ON;

BEGIN TRY

SELECT 
	@Id = [ID],
	@PrivateNumberGuid = [PrivateNumberGuid],
	@SmsText = [SmsText],
	@IsUnicode = CASE 
									WHEN [Encoding] = 2 THEN 1
									ELSE 0
								END,
	@SmsLen = [SmsLen],
	@IsFlash = CASE 
								WHEN [PresentType] = 0 THEN 1
								ELSE 0
							END,
	@UserGuid = [UserGuid]
FROM [dbo].[ScheduledBulkSmses] WITH (NOLOCK) WHERE [Guid] = @Guid;

	
SELECT
		TOP	1
		@RecipientGuid = [Guid],
		@ZoneGuid = [ZoneGuid],
		@Prefix = [Prefix],
		@ZipCode = [ZipCode],
		@NumberType = [Type],
		@Operator = [Operator],
		@FromIndex = [FromIndex],
		@ScopeCount = [ScopeCount],
		@SendCount = [Count],
		@PageNo = ISNULL([SendPageNo],1)
FROM
		[dbo].[BulkRecipient]
WHERE
		[ScheduledBulkSmsGuid] = @Guid AND
    ISNULL([Status],1) IN (1,4)

IF(ISNULL(@RecipientGuid,@EmptyGuid) = @EmptyGuid)
BEGIN
	UPDATE [dbo].[ScheduledBulkSmses] SET [Status] = 6--Completed
	WHERE [Guid] = @Guid;

	RETURN;
END

SET @Where += ' AND [MobileOperator] = ' + CAST(@Operator AS VARCHAR(1));

IF (@NumberType != 0) 
	SET @Where += ' AND [MobileType] = ' + CAST(@NumberType AS VARCHAR(1));

IF (@Prefix != 0) 
	SET @Where += ' AND [Mobile] LIKE ''' + @Prefix +'%''';

IF(ISNULL(@ZipCode,'') != '')
	SET @Where += ' AND [ZipCode] LIKE ''' + @ZipCode +'%''';

SELECT 
	@Number = number.[Number],
	@ServiceId = number.[ServiceID],
	@SmsSenderAgentReference = agent.[SmsSenderAgentReference],
	@QueueLength = agent.[QueueLength],
	@RouteActive = agent.[RouteActive],
	@AgentGuid = agent.[Guid],
	@IsSmpp = agent.[IsSmpp],
	@Username = agent.[Username],
	@Password = agent.[Password],
	@Domain = agent.[Domain],
	@SendLink = agent.[SendLink],
	@ReceiveLink = agent.[ReceiveLink],
	@DeliveryLink = agent.[DeliveryLink]
FROM 
	[dbo].[PrivateNumbers] number  WITH (NOLOCK) INNER JOIN
	[dbo].[SmsSenderAgents] agent  WITH (NOLOCK) ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	number.[Guid] = @PrivateNumberGuid;

SET @QueueLength = ISNULL(@QueueLength,90);

SELECT @TryCount = [Value] FROM [dbo].[Settings] WHERE [Key] = 12--MaximumFailedTryCount
SELECT @IsRemoteQueue = ISNULL([Value],0) FROM [dbo].[Settings] WHERE [Key] = 14 --IsRemoteQueue
SELECT @RemoteQueueIP = [Value] FROM [dbo].[Settings] WHERE [Key] = 15 --RemoteQueueIP

IF(@IsRemoteQueue = 1 AND ISNULL(@RemoteQueueIP,'') = '')
	THROW 62000,'Invalid Remote Server IP',1;

SET @QueueType = '-bulk';
	
SET @TryCount = ISNULL(@TryCount,1);

IF(ISNULL(@RouteActive,0) = 0)
BEGIN
	SET @QueueName =
		CASE
			WHEN @SmsSenderAgentReference = 1 THEN	'Magfa'
			WHEN @SmsSenderAgentReference = 2 THEN	'Asanak'
			WHEN @SmsSenderAgentReference = 3 THEN	'Armaghan'
			WHEN @SmsSenderAgentReference = 4 THEN	'AradBulk'
			WHEN @SmsSenderAgentReference = 5 THEN	'RahyabRG'
			WHEN @SmsSenderAgentReference = 6 THEN	'RahyabPG'
			WHEN @SmsSenderAgentReference = 7 THEN	'SlS'
			WHEN @SmsSenderAgentReference = 8 THEN	'Shreeweb'
			WHEN @SmsSenderAgentReference = 9 THEN	'AradVas'
			WHEN @SmsSenderAgentReference = 10 THEN 'SocialNetworks'
			WHEN @SmsSenderAgentReference = 11 THEN 'FFF'
			WHEN @SmsSenderAgentReference = 12 THEN 'GSM'
			WHEN @SmsSenderAgentReference = 13 THEN 'Avanak'
			WHEN @SmsSenderAgentReference = 14 THEN 'Mobbis'
		END;
		
	SET @QueueName += @QueueType;

	IF(ISNULL(@IsSmpp,0) = 1)
		SET @QueueLength = (@QueueLength / @SmsLen);
	
	EXEC('
	DECLARE @Result TABLE([Mobile] NVARCHAR(16),[Operator] INT,[IsBlackList] BIT);
	DECLARE @RandomTable TABLE([Mobile] NVARCHAR(16),[Operator] INT,[IsBlackList] BIT);
	DECLARE @ZoneGuids TABLE([Guid] UNIQUEIDENTIFIER);
	INSERT INTO @ZoneGuids([Guid]) EXEC [dbo].[Zones_GetAllChildren] '''+ @ZoneGuid +''';
	DECLARE @RecipientGuid UNIQUEIDENTIFIER = '''+ @RecipientGuid +''';
	DECLARE @From INT = '+ @FromIndex +';
	DECLARE @ScopeCount INT = '+ @ScopeCount +';
	DECLARE @SendCount INT = '+ @SendCount +';
	DECLARE @SendPageNo INT = '+ @PageNo +';
	DECLARE @QueueLength INT = '+ @QueueLength +';
	DECLARE @QueueName NVARCHAR(32) = N'''+ @QueueName +''';
	DECLARE @IsComplete INT = 0;
	DECLARE	@Receivers NVARCHAR(MAX) = '''';
	DECLARE	@Ret NVARCHAR(MAX) = '''';
	DECLARE @IsRemoteQueue INT = '+ @IsRemoteQueue +';
	DECLARE @RemoteQueueIP NVARCHAR(32) = '''+@RemoteQueueIP+''';
	DECLARE @Id BIGINT = '+ @Id +';
	DECLARE	@Number NVARCHAR(32) = N'''+ @Number +''';
	DECLARE	@IsUnicode INT = ' + @IsUnicode +';
	DECLARE	@IsFlash INT = '+ @IsFlash +';
	DECLARE	@PrivateNumberGuid UNIQUEIDENTIFIER = '''+ @PrivateNumberGuid +''';
	DECLARE	@ServiceId NVARCHAR(32) = '''+ @ServiceId +''';
	DECLARE	@SmsText NVARCHAR(MAX) = N'''+ @SmsText +''';
	DECLARE	@SmsLen INT = '+ @SmsLen +';
	DECLARE	@TryCount INT = '+ @TryCount +';
	DECLARE @Guid UNIQUEIDENTIFIER = '''+ @Guid +''';
	DECLARE	@Username NVARCHAR(32) = '''+ @Username +''';
	DECLARE	@Password NVARCHAR(32) = '''+ @Password +''';
	DECLARE	@Domain NVARCHAR(32) = '''+ @Domain +''';
	DECLARE	@SendLink NVARCHAR(512) = '''+ @SendLink +''';
	DECLARE	@ReceiveLink NVARCHAR(512) = '''+ @ReceiveLink +''';
	DECLARE	@DeliveryLink NVARCHAR(512) = '''+ @DeliveryLink +''';
	DECLARE	@SmsSenderAgentReference INT = '+ @SmsSenderAgentReference +';
	DECLARE	@UserGuid UNIQUEIDENTIFIER = '''+ @UserGuid +''';
	DECLARE @PageSize INT = 10000;
	DECLARE @SelectedCount INT = 10000;
	DECLARE @PageNo INT = 0;

	IF(@From != -1)
	BEGIN
		IF((@SendPageNo * @PageSize) > @SendCount)
		BEGIN
			SET @SelectedCount = @SendCount - ((@SendPageNo - 1) * @PageSize);
		END

		IF(@SelectedCount <= 0)
		BEGIN
			UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 6 -- Complete
			WHERE [Guid] = @RecipientGuid;

			RETURN;
		END

		INSERT INTO @Result
				([Mobile] ,
				 [Operator] ,
				 [IsBlackList])
		SELECT [Mobile],[MobileOperator],[IsBlackList] FROM [dbo].[PersonsInfo] WITH(NOLOCK) ' + @Where +' ORDER BY [ZoneGuid]
		OFFSET (@From + ((@SendPageNo - 1) * @PageSize)) ROWS
		FETCH NEXT @SelectedCount ROWS ONLY
	END
	ELSE
	BEGIN
		SET @PageSize = @SendCount;
		DECLARE @PageCount INT = @ScopeCount / @PageSize;
		DECLARE @Count INT = @SendCount / @PageCount;
		
		IF( @PageCount = (@SendPageNo - 1))
		BEGIN
			IF( (@PageCount * @Count) < @SendCount)
			BEGIN
				SET @Count = @SendCount - (@PageCount * @Count);
			END
			ELSE
			BEGIN
				UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 6 -- Complete
				WHERE [Guid] = @RecipientGuid;

				RETURN;
			END
		END
		ELSE IF(@PageCount < @SendPageNo)
		BEGIN
			UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 6 -- Complete
			WHERE [Guid] = @RecipientGuid;

			RETURN;
		END

		INSERT INTO @Result
				([Mobile] ,
				 [Operator] ,
				 [IsBlackList])
		SELECT [Mobile],[MobileOperator],[IsBlackList] FROM [dbo].[PersonsInfo] WITH(NOLOCK) ' + @Where +' ORDER BY [ZoneGuid]
		OFFSET ((@SendPageNo - 1) * @PageSize) ROWS
		FETCH NEXT @PageSize ROWS ONLY;

		INSERT INTO @RandomTable
				([Mobile] ,
				 [Operator] ,
				 [IsBlackList])
		SELECT TOP(@Count) [Mobile],[Operator],[IsBlackList] FROM @Result ORDER BY NEWID();

		DELETE FROM @Result;

		INSERT INTO @Result
				([Mobile] ,
				 [Operator] ,
				 [IsBlackList])
		SELECT [Mobile],[Operator],[IsBlackList] FROM @RandomTable;
	END

	WHILE (@IsComplete = 0)
	BEGIN
		SET @Receivers = (SELECT [Mobile],[Operator],[IsBlackList] FROM @Result ORDER BY [Mobile] OFFSET (@PageNo * @QueueLength) ROWS FETCH NEXT @QueueLength ROWS ONLY FOR XML PATH(''Node''), ROOT(''Root''));

		IF(LEN(@Receivers) > 0)
		BEGIN
			EXEC @Ret = dbo.SendSms
					 @Queue = @QueueName,
					 @IsRemoteQueue = @IsRemoteQueue,
					 @RemoteQueueIP = @RemoteQueueIP,
					 @SmsSendType = 7,--bulk
					 @PageNo = @PageNo,
					 @Sender = @Number,
					 @PrivateNumberGuid = @PrivateNumberGuid,
					 @TotalCount = @SendCount,
					 @Receivers = @Receivers,
					 @ServiceId = @ServiceId,
					 @Message = @SmsText,
					 @SmsLen = @SmsLen,
					 @TryCount = @TryCount,
					 @SmsIdentifier = 0,
					 @SmsPartIndex = 0,
					 @IsFlash = @IsFlash,
					 @IsUnicode = @IsUnicode,
					 @Id = @Id,
					 @Guid = @Guid,
					 @Username = @Username,
					 @Password = @Password,
					 @Domain = @Domain,
					 @SendLink = @SendLink,
					 @ReceiveLink = @ReceiveLink,
					 @DeliveryLink = @DeliveryLink,
					 @AgentReference = @SmsSenderAgentReference;

			IF(ISNUMERIC(@Ret) = 1)
				SET @PageNo += 1;
			ELSE
			BEGIN
				 EXEC [dbo].[InsertLog]
							@Type = 2, --Error
							@Source = ''SendSms To MSMQ'',
							@Name = '''' ,
							@Text = @Ret,
							@IP = '''',
							@Browser = '''',
							@ReferenceGuid = @Guid,
							@UserGuid = @UserGuid;
				--BREAK;
			END
		END
		ELSE
		BEGIN
			SET @IsComplete = 1;
			SET @SendPageNo += 1;
		END
	END

	UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 4 --Ready
	WHERE [Guid] = @RecipientGuid;
	');
END
ELSE
BEGIN
	IF(@SmsSenderAgentReference = 9)
		SET @QueueType = 'vas' + @QueueType;

	EXEC('
		DECLARE @Result TABLE([Mobile] NVARCHAR(16),[Operator] INT,[IsBlackList] BIT);
		DECLARE @ZoneGuids TABLE([Guid] UNIQUEIDENTIFIER);
		INSERT INTO @ZoneGuids([Guid]) EXEC [dbo].[Zones_GetAllChildren] '''+ @ZoneGuid +''';
		DECLARE @RecipientGuid UNIQUEIDENTIFIER = '''+ @RecipientGuid +''';
		DECLARE @From INT = '+ @FromIndex +';
		DECLARE @ScopeCount INT = '+ @ScopeCount +';
		DECLARE @SendCount INT = '+ @SendCount +';
		DECLARE @SendPageNo INT = '+ @PageNo +';
		DECLARE @QueueLength INT;
		DECLARE @QueueName NVARCHAR(32) = '''';
		DECLARE @IsComplete INT = 0;
		DECLARE	@Receivers NVARCHAR(MAX) = '''';
		DECLARE	@Ret NVARCHAR(MAX) = '''';
		DECLARE @IsRemoteQueue INT = '+ @IsRemoteQueue +';
		DECLARE @RemoteQueueIP NVARCHAR(32) = '''+@RemoteQueueIP+''';
		DECLARE @Id BIGINT = '+ @Id +';
		DECLARE	@Number NVARCHAR(32) = N'''+ @Number +''';
		DECLARE	@IsUnicode INT = ' + @IsUnicode +';
		DECLARE	@IsFlash INT = '+ @IsFlash +';
		DECLARE	@PrivateNumberGuid UNIQUEIDENTIFIER = '''+ @PrivateNumberGuid +''';
		DECLARE	@ServiceId NVARCHAR(32) = '''+ @ServiceId +''';
		DECLARE	@SmsText NVARCHAR(MAX) = N'''+ @SmsText +''';
		DECLARE	@SmsLen INT = '+ @SmsLen +';
		DECLARE	@TryCount INT = '+ @TryCount +';
		DECLARE @Guid UNIQUEIDENTIFIER = '''+ @Guid +''';
		DECLARE	@Username NVARCHAR(32) = '''';
		DECLARE	@Password NVARCHAR(32) = '''';
		DECLARE	@Domain NVARCHAR(32) = '''';
		DECLARE	@SendLink NVARCHAR(512) = '''';
		DECLARE	@ReceiveLink NVARCHAR(512) = '''';
		DECLARE	@SmsSenderAgentReference INT = '+ @SmsSenderAgentReference +';
		DECLARE	@UserGuid UNIQUEIDENTIFIER = '''+ @UserGuid +''';
		DECLARE @PageSize INT = 10000;
		DECLARE @SelectedCount INT = 10000;
		DECLARE @PageNo INT = 0;
		DECLARE	@OperatorName NVARCHAR(32) = '''';
		DECLARE @QueueType NVARCHAR(32) = '''+@QueueType+''';
		DECLARE @OperatorID INT = '+ @Operator +';
		DECLARE	@IsSmpp BIT = '+ @IsSmpp +';

		IF(@From != -1)
		BEGIN
			IF((@SendPageNo * @PageSize) > @SendCount)
			BEGIN
				SET @SelectedCount = @SendCount - ((@SendPageNo - 1) * @PageSize);
			END

			IF(@SelectedCount <= 0)
			BEGIN
				UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 6 -- Complete
				WHERE [Guid] = @RecipientGuid;

				RETURN;
			END

			INSERT INTO @Result
					([Mobile] ,
					 [Operator] ,
					 [IsBlackList])
			SELECT [Mobile],[MobileOperator],[IsBlackList] FROM [dbo].[PersonsInfo] WITH(NOLOCK) ' + @Where +' ORDER BY [ZoneGuid]
			OFFSET @From + ((@SendPageNo - 1) * @PageSize) ROWS
			FETCH NEXT @SelectedCount ROWS ONLY
		END
		ELSE
		BEGIN
			SET @PageSize = @SendCount;
			DECLARE @PageCount INT = @ScopeCount / @PageSize;
			DECLARE @Count INT = @SendCount / @PageCount;
		
			IF( @PageCount = (@SendPageNo - 1))
			BEGIN
				IF( (@PageCount * @Count) < @SendCount)
				BEGIN
					SET @Count = @SendCount - (@PageCount * @Count);
				END
				ELSE
				BEGIN
					UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 6 -- Complete
					WHERE [Guid] = @RecipientGuid;

					RETURN;
				END
			END
			ELSE IF(@PageCount < @SendPageNo)
			BEGIN
				UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 6 -- Complete
				WHERE [Guid] = @RecipientGuid;

				RETURN;
			END

			INSERT INTO @Result
					([Mobile] ,
					 [Operator] ,
					 [IsBlackList])
			SELECT [Mobile],[MobileOperator],[IsBlackList] FROM [dbo].[PersonsInfo] WITH(NOLOCK) ' + @Where +' ORDER BY [ZoneGuid]
			OFFSET ((@SendPageNo - 1) * @PageSize) ROWS
			FETCH NEXT @PageSize ROWS ONLY;

			INSERT INTO @RandomTable
					([Mobile] ,
					 [Operator] ,
					 [IsBlackList])
			SELECT TOP(@Count) [Mobile],[MobileOperator],[IsBlackList] FROM @Result ORDER BY NEWID();

			DELETE FROM @Result;

			INSERT INTO @Result
					([Mobile] ,
					 [Operator] ,
					 [IsBlackList])
			SELECT [Mobile],[MobileOperator],[IsBlackList] FROM @RandomTable;
		END

		SELECT
			@OperatorName = opt.[Name],
			@Username = [Username],
			@Password = [Password],
			@Domain = [Domain],
			@SendLink = [Link],
			@QueueLength = [QueueLength]
		FROM
			[dbo].[Routes] WITH(NOLOCK) INNER JOIN
			[dbo].[Operators] opt WITH(NOLOCK) ON [dbo].[Routes].[OperatorID] = opt.[ID]
		WHERE
			[SmsSenderAgentGuid] = '''+ @AgentGuid +''' AND
			[OperatorID] = @OperatorID;

	
		IF(ISNULL(@IsSmpp,0) = 1)
			SET @QueueLength = (@QueueLength / @SmsLen);

		SET @QueueName = @OperatorName + @QueueType;

		WHILE (@IsComplete = 0)
		BEGIN
			SET @Receivers = (SELECT [Mobile],[Operator],[IsBlackList] FROM @Result ORDER BY [Mobile] OFFSET (@PageNo * @QueueLength) ROWS FETCH NEXT @QueueLength ROWS ONLY FOR XML PATH(''Node''), ROOT(''Root''));

			IF(LEN(@Receivers) > 0)
			BEGIN
				EXEC @Ret = dbo.SendSms
						 @Queue = @QueueName,
						 @IsRemoteQueue = @IsRemoteQueue,
						 @RemoteQueueIP = @RemoteQueueIP,
						 @SmsSendType = 7,--bulk
						 @PageNo = @PageNo,
						 @Sender = @Number,
						 @PrivateNumberGuid = @PrivateNumberGuid,
						 @TotalCount = @SendCount,
						 @Receivers = @Receivers,
						 @ServiceId = @ServiceId,
						 @Message = @SmsText,
						 @SmsLen = @SmsLen,
						 @TryCount = @TryCount,
						 @SmsIdentifier = 0,
						 @SmsPartIndex = 0,
						 @IsFlash = @IsFlash,
						 @IsUnicode = @IsUnicode,
						 @Id = @Id,
						 @Guid = @Guid,
						 @Username = @Username,
						 @Password = @Password,
						 @Domain = @Domain,
						 @SendLink = @SendLink,
						 @ReceiveLink = @ReceiveLink,
						 @DeliveryLink = '',
						 @AgentReference = @SmsSenderAgentReference;

				IF(ISNUMERIC(@Ret) = 1 AND @Ret != 0)
					SET @PageNo += 1;
				ELSE
				BEGIN
					EXEC [dbo].[InsertLog]
							 @Type = 2, --Error
							 @Source = ''SendSms To MSMQ'',
							 @Name = '''' ,
							 @Text = @Ret,
							 @IP = '''',
							 @Browser = '''',
							 @ReferenceGuid = @Guid,
							 @UserGuid = @UserGuid;
					BREAK;
				END
			END
			ELSE
			BEGIN
				SET @IsComplete = 1;
				SET @SendPageNo += 1;
			END
		END

		UPDATE [dbo].[BulkRecipient] SET [SendPageNo] = @SendPageNo, [Status] = 4 --Ready
		WHERE [Guid] = @RecipientGuid;
	');
END

EXEC [dbo].[InsertLog]
			@Type = 3, --Action
			@Source = 'ScheduledSmses',
			@Name = 'Send Sms' ,
			@Text = N'Send Sms To Queue ' ,
			@IP = '',
			@Browser = '',
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;

END TRY
BEGIN CATCH
 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE())+';@QueueLength:'+CAST(@QueueLength AS	NVARCHAR(8));

 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ScheduledBulkSmses_SendSms',
			@Name = '' ,
			@Text = @Text,
			@IP = '',
			@Browser = '',
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;

 THROW;
END CATCH;

GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_CheckSendSmsException]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScheduledSmses_CheckSendSmsException]
	@BatchId UNIQUEIDENTIFIER
  AS

	DECLARE @UserGuid UNIQUEIDENTIFIER,
					@ParentGuid UNIQUEIDENTIFIER,
					@PrivateNumberGuid UNIQUEIDENTIFIER,
					@IsSendActive BIT,
					@IsSendBulkActive BIT,
					@StartSendTime TIME(0),
					@EndSendTime TIME(0),
					@IsActiveNumber BIT,
					@TimeNow TIME(0) = CAST(GETDATE() as TIME(0)),
					@UserIsActive BIT,
					@ErrorCode INT = 0,
					@SmsText NVARCHAR(MAX	),
					@UserExpireDate DATETIME,
					@ParentExpireDate DATETIME,
					@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
					@DefaultNumber UNIQUEIDENTIFIER,
					@TypeSend INT,
					@SendSmsAlert BIT;

	SELECT 
		@UserGuid = [UserGuid],
		@PrivateNumberGuid = ISNULL([PrivateNumberGuid],@EmptyGuid),
		@SmsText = [SmsText],
		@TypeSend = [TypeSend]
	FROM 
		[dbo].[ScheduledSmses] WITH(NOLOCK) WHERE [Guid] = @BatchId;
	
	IF(@PrivateNumberGuid = @EmptyGuid)
	BEGIN
		SELECT @PrivateNumberGuid = CAST([Value] AS UNIQUEIDENTIFIER) FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 5;--DefaultNumber
		UPDATE [dbo].[ScheduledSmses] SET [PrivateNumberGuid] = @PrivateNumberGuid WHERE [Guid] = @BatchId;
	END

	SELECT 
		@UserIsActive = [IsActive],
		@ParentGuid = [ParentGuid],
		@UserExpireDate = [ExpireDate]
	FROM
		[dbo].[Users] WITH(NOLOCK)
	WHERE
		[Guid] = @UserGuid;

	SELECT @ParentExpireDate = [ExpireDate] FROM [dbo].[Users] WHERE [Guid] = @ParentGuid;

	SELECT 
		@IsSendActive = [IsSendActive],
		@IsSendBulkActive = [IsSendBulkActive], 
		@StartSendTime = [StartSendTime],
		@EndSendTime = [EndSendTime],
		@IsActiveNumber = [IsActive],
		@SendSmsAlert = [SendSmsAlert]
	FROM
		[dbo].[SmsSenderAgents] agent WITH(NOLOCK) INNER JOIN
		[dbo].[PrivateNumbers] number WITH(NOLOCK) ON agent.[Guid] = number.[SmsSenderAgentGuid]
	WHERE
		number.[IsDeleted] = 0 AND
		number.[Guid] =	@PrivateNumberGuid; 
	
	IF(@@ROWCOUNT = 0)
		SET @ErrorCode = 7;--PrivateNumberNotValid

	ELSE IF(@UserIsActive = 0)
		SET @ErrorCode = 5;--UserIsInactive

	ELSE IF([dbo].udfIsUserHierarchyActive(@UserGuid) = 0)
		SET @ErrorCode = 6;--AdminIsInactive
	
	ELSE IF(@IsActiveNumber = 0)
		SET @ErrorCode = 3;--PrivateNumberIsInactive

	ELSE IF(@IsSendActive = 0 AND @TypeSend != 7) --TypeSend = 7 is bulk
		SET @ErrorCode = 11;--SystemIsOutOfService

	ELSE IF(@IsSendBulkActive = 0 AND @TypeSend = 7) --TypeSend = 7 is bulk
		SET @ErrorCode = 11;--SystemIsOutOfService

	ELSE IF (@TypeSend = 7 AND @SendSmsAlert = 0 AND((@TimeNow NOT BETWEEN @StartSendTime AND @EndSendTime) AND @StartSendTime != @EndSendTime))
	 SET @ErrorCode = 12; --SendTimeNotValid

	ELSE IF(CONVERT(DATE,@UserExpireDate) < CONVERT(DATE,GETDATE()))
		SET @ErrorCode = 14; --UserIsExpired

	ELSE IF(CONVERT(DATE,@ParentExpireDate) < CONVERT(DATE,GETDATE()))
		SET @ErrorCode = 15; --AdminIsExpired

	ELSE IF(SELECT COUNT(*) FROM [dbo].[FilterWords] WITH(NOLOCK) WHERE @SmsText LIKE [Title])>0
	 SET @ErrorCode = 13; --SmsTextIsFilter

	RETURN @ErrorCode;



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[BulkRecipient] WHERE [ScheduledBulkSmsGuid] = @Guid;
DELETE FROM [dbo].[ScheduledBulkSmses] WHERE [Guid] = @Guid;
DELETE FROM [dbo].[Recipients] WHERE [ScheduledSmsGuid] = @Guid;
DELETE FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_GarbageCollector]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_GarbageCollector]
  AS


DECLARE @FailedSend TABLE([Guid] UNIQUEIDENTIFIER);

INSERT INTO @FailedSend
				([Guid])
			 SELECT
				[Guid]
			 FROM
				[dbo].[ScheduledSmses]
			 WHERE
				CONVERT(DATE,[DateTimeFuture]) < CONVERT(DATE,GETDATE()-3) AND
				[Status] = 5 AND
				[SmsSendFaildType] != 9

DELETE FROM [dbo].[BulkRecipient] WHERE [ScheduledBulkSmsGuid] IN (SELECT [Guid] FROM @FailedSend);
DELETE FROM [dbo].[ScheduledBulkSmses] WHERE [Guid] IN (SELECT [Guid] FROM @FailedSend);
DELETE FROM [dbo].[Recipients] WHERE [ScheduledSmsGuid] IN (SELECT [Guid] FROM @FailedSend);
DELETE FROM [dbo].[ScheduledSmses] WHERE [Guid] IN (SELECT [Guid] FROM @FailedSend);
GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_GetPagedUsersQueue]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_GetPagedUsersQueue]
	@UserGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

DECLARE @Where NVARCHAR(MAX) = 'number.[IsDeleted] = 0 AND sch.[IsDeleted] = 0';

IF ( @Where != '' ) 
	SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @Statement ='

SELECT * INTO #Children FROM dbo.udfGetAllChildren('''+ CAST(@UserGuid AS VARCHAR(36)) +''');
		
SELECT 
	sch.*,
	number.[Number],
	[UserName]
	INTO #Temp
FROM
	[dbo].[ScheduledSmses] sch WITH(NOLOCK) INNER JOIN
	#Children ON #Children.[UserGuid] = sch.[UserGuid] INNER JOIN 
	[dbo].[PrivateNumbers] number WITH(NOLOCK) ON  sch.[PrivateNumberGuid] = number.[Guid]
	'+ @Where +';

SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';
	
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';DROP TABLE #Temp;DROP TABLE #Children';

EXEC(@Statement);



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_GetQueue]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_GetQueue]
	@Count INT,
	@SmsSenderAgentReference INT
  AS


SELECT 
	TOP (@Count)
	sch.[Guid],
	sch.[ID],
	sch.[TypeSend],
	sch.[Status],
	sch.[PrivateNumberGuid],
	sch.[UserGuid],
	sch.[SmsSenderAgentReference],
	agent.[SendSmsAlert],
	agent.[StartSendTime],
	agent.[EndSendTime]
FROM
	[dbo].[ScheduledSmses] sch INNER JOIN
	[dbo].[PrivateNumbers] number ON sch.[PrivateNumberGuid] = number.[Guid] INNER JOIN
	[dbo].[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	sch.SmsSenderAgentReference = @SmsSenderAgentReference AND
	sch.[IsDeleted] = 0 AND
	sch.[TypeSend] != 7 AND --bulk
	[Status] IN (1,--Stored
							 2,--Extracting
							 --3,--FailedExtract
							 4,--Ready
							 6,--Completed
							 7,--Extracted
							 9--Confirmed
							 )AND
	[DateTimeFuture] <= GETDATE()
ORDER BY 
	[TypeSend],
  [DateTimeFuture]
GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_GetUserQueue]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_GetUserQueue]
	@UserGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS


DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

DECLARE @Where NVARCHAR(MAX) = 'number.[IsDeleted] = 0 AND sch.[IsDeleted] = 0';

IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where += ' sch.[UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + '''';

IF ( @Where != '' ) 
  SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

PRINT @Where;

SET @Statement ='
  SELECT 
		sch.*,
		number.[Number]
		INTO #Temp
	FROM
		[dbo].[ScheduledSmses] sch WITH(NOLOCK) INNER JOIN
		[dbo].[PrivateNumbers] number WITH(NOLOCK) ON  sch.[PrivateNumberGuid] = number.[Guid]'+ @Where +'

	SELECT COUNT(*) [RowCount] FROM #Temp;
	SELECT * FROM #Temp';
	
IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END
	
SET @Statement +=';DROP TABLE #Temp;';

EXEC(@Statement);



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertBulkRequest]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_InsertBulkRequest]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@ReferenceGuid NVARCHAR(MAX),
	@SmsText NVARCHAR(MAX),
	@SmsLen INT,
	@PresentType INT,
	@Encoding INT,
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@RequestXML XML,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64),
	@Recipients [dbo].[BulkRecipient] READONLY
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION

	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [ReferenceGuid],
							 [SmsText],
							 [PresentType],
							 [Encoding],
							 [SmsLen],
							 [TypeSend],
							 [Status],
							 [RequestXML],
							 [DateTimeFuture],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @ReferenceGuid,
							 @SmsText,
							 @PresentType,
							 @Encoding,
							 @SmsLen,
							 @TypeSend,
							 @Status,
							 @RequestXML,
							 @DateTimeFuture,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid)

	INSERT INTO [dbo].[ScheduledBulkSmses]
								([Guid] ,
								 [ID],
								 [PrivateNumberGuid] ,
								 [SmsText] ,
								 [PresentType] ,
								 [Encoding] ,
								 [SmsLen] ,
								 [RequestXML] ,
								 [CreateDate] ,
								 [SendDateTime] ,
								 [SmsSenderAgentReference] ,
								 [Status] ,
								 [UserGuid])
							SELECT
								 [Guid],
								 [ID],
								 [PrivateNumberGuid],
								 [SmsText],
								 [PresentType],
								 [Encoding],
								 [SmsLen],
								 [RequestXML],
								 [CreateDate],
								 [DateTimeFuture],
								 [SmsSenderAgentReference],
								 [Status] ,
								 [UserGuid]
							FROM [ScheduledSmses] WHERE [Guid] = @Guid;

	INSERT INTO [dbo].[BulkRecipient]
					([Guid] ,
					 [Status] ,
					 [Prefix] ,
					 [ZipCode] ,
					 [Type] ,
					 [Operator] ,
					 [FromIndex] ,
					 [Count] ,
					 [ScopeCount] ,
					 [ZoneGuid] ,
					 [ScheduledBulkSmsGuid])
				 SELECT
						NEWID(),
						1,
						[Prefix],
						[ZipCode],
						[Type],
						[Operator],
						[FromIndex],
						[Count],
						[ScopeCount],
						[ZoneGuid],
						@Guid
				 FROM
						@Recipients;
	

	EXEC [dbo].[InsertLog]
			@Type = 3, --Action
			@Source = 'ScheduledSmses',
			@Name = 'Insert bulk Sms' ,
			@Text = N'ثبت درخواست ارسال پیامک انبوه' ,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;
	
	COMMIT TRANSACTION;
END TRY
BEGIN CATCH

	IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ScheduledSmses',
			@Name = 'Insert bulk Sms',
			@Text = @Text,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;
			
END CATCH


GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertFormatSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_InsertFormatSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@ReferenceGuid NVARCHAR(MAX),
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64)
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION

	INSERT INTo [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [ReferenceGuid],
							 [TypeSend],
							 [Status],
							 [DateTimeFuture],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @ReferenceGuid,
							 @TypeSend,
							 @Status,
							 @DateTimeFuture,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid);

	EXEC [dbo].[InsertLog]
			@Type = 3, --Action
			@Source = 'ScheduledSmses',
			@Name = 'Insert Format Sms' ,
			@Text = N'ثبت درخواست ارسال پیامک از قالب' ,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
 
	 IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	 IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ScheduledSmses',
			@Name = 'Insert Format Sms' ,
			@Text = @Text,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;
	 
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertGradualSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_InsertGradualSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Reciever NVARCHAR(MAX),
	@ReferenceGuid NVARCHAR(MAX),
	@SmsText NVARCHAR(MAX),
	@SmsLen INT,
	@PresentType INT,
	@Encoding INT,
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@Period INT,
	@PeriodType INT,
	@SendPageNo INT,
	@SendPageSize INT,
	@UserGuid UNIQUEIDENTIFIER,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64)
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION;

	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [ReferenceGuid],
							 [SmsText],
							 [PresentType],
							 [Encoding],
							 [SmsLen],
							 [TypeSend],
							 [Status],
							 [DateTimeFuture],
							 [Period],
							 [PeriodType],
							 [SendPageNo],
							 [SendPageSize],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @ReferenceGuid,
							 @SmsText,
							 @PresentType,
							 @Encoding,
							 @SmsLen,
							 @TypeSend,
							 @Status,
							 @DateTimeFuture,
							 @Period,
							 @PeriodType,
							 @SendPageNo,
							 @SendPageSize,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid);

	IF(LEN(@Reciever)>0)
	BEGIN
		EXEC [dbo].[Recipients_Insert]
				 @Receivers = @Reciever,
				 @ScheduledSmsGuid = @Guid;
	END
	
	EXEC [dbo].[InsertLog]
			@Type = 3, --Action
			@Source = 'ScheduledSmses',
			@Name = 'Insert Gradual Sms' ,
			@Text = N'ثبت درخواست ارسال پیامک تدریجی' ,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;
				
	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	 IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	 IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ScheduledSmses',
			@Name = 'Insert Gradual Sms' ,
			@Text = @Text,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;	
	 
END CATCH;



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertGroupSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_InsertGroupSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@ReferenceGuid NVARCHAR(MAX),
	@SmsText NVARCHAR(MAX),
	@SmsLen INT,
	@PresentType INT,
	@Encoding INT,
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64)
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION
	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [ReferenceGuid],
							 [SmsText],
							 [PresentType],
							 [Encoding],
							 [SmsLen],
							 [TypeSend],
							 [Status],
							 [DateTimeFuture],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @ReferenceGuid,
							 @SmsText,
							 @PresentType,
							 @Encoding,
							 @SmsLen,
							 @TypeSend,
							 @Status,
							 @DateTimeFuture,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid)

	EXEC [dbo].[InsertLog]
			@Type = 3, --Action
			@Source = 'ScheduledSmses',
			@Name = 'Insert Group Sms' ,
			@Text = N'ثبت درخواست ارسال پیامک گروهی' ,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	 IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	 IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ScheduledSmses',
			@Name = 'Insert Group Sms',
			@Text = @Text,
			@IP = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;
				
END CATCH


GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertP2PSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_InsertP2PSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@FilePath NVARCHAR(128),
	@SmsPattern NVARCHAR(1024),
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64)
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION 
	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [FilePath],
							 [SmsPattern],
							 [PresentType],
							 [TypeSend],
							 [Status],
							 [DateTimeFuture],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @FilePath,
							 @SmsPattern,
							 1,--Normal
							 @TypeSend,
							 @Status,
							 @DateTimeFuture,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid);

	EXEC [dbo].[InsertLog]
		@Type = 3, --Action
		@Source = 'ScheduledSmses',
		@Name = 'Insert P2PSms' ,
		@Text = N'ثبت درخواست ارسال پیامک نظیر به نظیر' ,
	  @Ip = @IPAddress,
	  @Browser = @Browser,
	  @ReferenceGuid = @Guid,
		@UserGuid = @UserGuid;

	COMMIT TRANSACTION
END TRY
BEGIN CATCH 
	 IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	 IF(XACT_STATE() = 1)
	  COMMIT TRANSACTION;

	 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ScheduledSmses',
			@Name = 'Insert P2PSms' ,
			@Text = @Text,
			@Ip = @IPAddress,
			@Browser = @Browser,
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;

	 THROW;
END CATCH;



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertPeriodSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScheduledSmses_InsertPeriodSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Reciever NVARCHAR(MAX),
	@ReferenceGuid NVARCHAR(MAX),
	@SmsText NVARCHAR(MAX),
	@SmsLen INT,
	@PresentType INT,
	@Encoding INT,
	@TypeSend INT,
	@Status INT,
	@Period INT,
	@PeriodType INT,
	@StartDateTime DATETIME,
	@EndDateTime DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64)
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION;

	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [ReferenceGuid],
							 [SmsText],
							 [PresentType],
							 [Encoding],
							 [SmsLen],
							 [TypeSend],
							 [Status],
							 [Period],
							 [PeriodType],
							 [DateTimeFuture],
							 [StartDateTime],
							 [EndDateTime],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @ReferenceGuid,
							 @SmsText,
							 @PresentType,
							 @Encoding,
							 @SmsLen,
							 @TypeSend,
							 @Status,
							 @Period,
							 @PeriodType,
							 @StartDateTime,
							 @StartDateTime,
							 @EndDateTime,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid);

	EXEC [dbo].[Recipients_Insert]
			@Receivers = @Reciever,
	    @ScheduledSmsGuid = @Guid;

	EXEC [dbo].[InsertLog]
			@Type = 3, --Action
	    @Source = 'ScheduledSmses', -- nvarchar(256)
	    @Name = 'Insert Period Sms' ,
			@Text = N'ثبت درخواست ارسال پیامک دوره ای' ,
	    @Ip = @IPAddress,
	    @Browser = @Browser,
	    @ReferenceGuid = @Guid,
	    @UserGuid = @UserGuid;
				 	
	COMMIT TRANSACTION
END TRY
BEGIN CATCH	 
	IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;
	IF(XACT_STATE() = 1)
	 COMMIT TRANSACTION;

	DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	EXEC [dbo].[InsertLog]
			@Type = 2, --Error
	    @Source = 'ScheduledSmses',
			@Name = 'Insert Period Sms' ,
			@Text = @Text,
	    @Ip = @IPAddress,
	    @Browser = @Browser,
	    @ReferenceGuid = @Guid,
	    @UserGuid = @UserGuid;
END CATCH;




GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertRegularContentSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_InsertRegularContentSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Receiver Recipient READONLY,
	@SmsText NVARCHAR(MAX),
	@SmsLen INT,
	@PresentType INT,
	@Encoding INT,
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@UserGuid UNIQUEIDENTIFIER
  AS


BEGIN TRY
	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [SmsText],
							 [PresentType],
							 [Encoding],
							 [SmsLen],
							 [TypeSend],
							 [Status],
							 [DateTimeFuture],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @SmsText,
							 @PresentType,
							 @Encoding,
							 @SmsLen,
							 @TypeSend,
							 @Status,
							 @DateTimeFuture,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 @UserGuid)

	EXEC [dbo].[Recipients_InsertTable]
			@Receivers = @Receiver,
	    @ScheduledSmsGuid = @Guid
	
	EXEC [dbo].[InsertLog]
			@Type = 3, --Action
	    @Source = 'ScheduledSmses',
	    @Name = 'Insert Regular Content Sms' ,
	    @Text = N'ثبت درخواست ارسال پیامک محتوای منظم' ,
	    @Ip = N'',
	    @Browser = N'',
	    @ReferenceGuid = @Guid,
	    @UserGuid = @UserGuid
END TRY
BEGIN CATCH
	 THROW;
END CATCH;



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_InsertSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ScheduledSmses_InsertSms]
	@Guid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Reciever NVARCHAR(MAX),
	@SmsText NVARCHAR(MAX),
	@SmsLen INT,
	@PresentType INT,
	@Encoding INT,
	@TypeSend INT,
	@Status INT,
	@DateTimeFuture DATETIME,
	@UserGuid UNIQUEIDENTIFIER,
	@IPAddress NVARCHAR(32),
	@Browser NVARCHAR(64)--,
	--@VoiceURL NVARCHAR(500),
    --@voiceMessageId INT
  AS


SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION

	INSERT INTO [dbo].[ScheduledSmses]
							([Guid],
							 [PrivateNumberGuid],
							 [SmsText],
							 [PresentType],
							 [Encoding],
							 [SmsLen],
							 [TypeSend],
							 [Status],
							 [DateTimeFuture],
							 [CreateDate],
							 [SmsSenderAgentReference],
							 [IsDeleted],
							 --[VoiceURL],
                             --[voiceMessageId],
							 [UserGuid])
					VALUES
							(@Guid,
							 @PrivateNumberGuid,
							 @SmsText,
							 @PresentType,
							 @Encoding,
							 @SmsLen,
							 @TypeSend,
							 @Status,
							 @DateTimeFuture,
							 GETDATE(),
							 dbo.[GetPrivateNumberAgentReference](@PrivateNumberGuid),
							 0,
							 --@VoiceURL,
							 --@voiceMessageId,
							 @UserGuid)

	EXEC [dbo].[Recipients_Insert]
			 @Receivers = @Reciever,
	     @ScheduledSmsGuid = @Guid;

	EXEC [dbo].[InsertLog]
				@Type = 3, --Action
				@Source = N'ScheduledSmses',
				@Name = N'InsertSms',
				@Text = N'ثبت درخواست ارسال پیامک' ,
				@Ip = @IPAddress,
				@Browser = @Browser,
				@ReferenceGuid = @Guid,
				@UserGuid = @UserGuid;

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	 IF(XACT_STATE() = -1)
		ROLLBACK TRANSACTION;

	 IF(XACT_STATE() = 1)
		COMMIT TRANSACTION;

	 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

	 EXEC [dbo].[InsertLog]
			 @Type = 2, -- Error
	     @Source = N'ScheduledSmses',
	     @Name = N'InsertSms',
	     @Text = @Text,
	     @Ip = @IPAddress,
	     @Browser = @Browser,
	     @ReferenceGuid = @Guid,
	     @UserGuid = @UserGuid;

	 THROW;
END CATCH;



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_ProcessRequest]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScheduledSmses_ProcessRequest]
	@Guid UNIQUEIDENTIFIER,
	@ID BIGINT,
	@Status INT,
	@SendType INT,
	@SmsSenderAgentReference INT,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER,
	@SendSmsAlert BIT,
	@StartSendTime TIME,
	@EndSendTime TIME
  AS


DECLARE @Number nvarchar(32);
DECLARE @IsUnicode INT;
DECLARE @IsFlash INT;
DECLARE @Priority INT;
DECLARE @DeliveryBase BIT;
DECLARE @SmsText NVARCHAR(MAX) = '';
DECLARE @SmsLen INT;
DECLARE	@ErrorCode INT;
DECLARE @trancount INT;
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @RecordXMl XML;
DECLARE @Username NVARCHAR(32);
DECLARE @TimeNow TIME(0) = CAST(GETDATE() as TIME(0));

SET XACT_ABORT ON;

BEGIN TRY
	BEGIN TRANSACTION

	IF(@Status IN (1,3))--Stored OR FailedExtract
	BEGIN
		EXEC @ErrorCode = [dbo].[ScheduledSmses_CheckSendSmsException] @Guid;
		IF(@ErrorCode != 0)
		BEGIN
			UPDATE [dbo].[ScheduledSmses] SET [SmsSendFaildType] = @ErrorCode,[Status] = 5--Failed
			WHERE [Guid] = @Guid;
			COMMIT TRANSACTION;
			RETURN;
		END

		UPDATE [dbo].[ScheduledSmses] SET [Status] = 2--Extracting
		WHERE [Guid] = @Guid;

		IF(@SendType in (1,8))--SendSms,SendSmsFromAPI
		BEGIN
			UPDATE [dbo].[ScheduledSmses] SET [Status] = 7--Extracted
			WHERE [Guid] = @Guid;
		END
		ELSE IF(@SendType in (2,11))--SendGroupSms,SendGroupSmsFromAPI
		BEGIN
			EXEC [dbo].[ScheduledSmses_SendGroupSms] @Guid = @Guid;
		END
		ELSE IF(@SendType = 3) --SendFormatSms
		BEGIN
			EXEC [dbo].[ScheduledSmses_SendFormatSms] @Guid = @Guid;
			UPDATE [dbo].[ScheduledSmses] SET [IsDeleted] = 1 WHERE [Guid] = @Guid;
		END
		ELSE IF(@SendType = 5) --SendPeriodSms
			EXEC [dbo].[ScheduledSmses_SendPeriodSms] @Guid = @Guid;
		ELSE IF(@SendType = 6) --SendGradualSms
			EXEC [dbo].[ScheduledSmses_SendGradualSms] @Guid = @Guid;
		ELSE IF(@SendType = 10) --SendP2PSms
			EXEC [dbo].[ScheduledSmses_SendP2PSms] @Guid = @Guid;
	END
	ELSE IF (@Status = 4)--Ready
	BEGIN
		EXEC [dbo].[ScheduledSmses_SendSms] @Guid = @Guid;
	END
	ELSE IF(@Status = 6)--Completed
	BEGIN
		IF(@SendSmsAlert = 1 AND (@TimeNow NOT BETWEEN @StartSendTime AND @EndSendTime))
		BEGIN
			SELECT @Username = [UserName] FROM [Users] WHERE [Guid] = @UserGuid
			SET @SmsText = N'درخواست ارسال ثبت شد' + CHAR(13)+ N'کاربر'+ @Username + CHAR(13) + N'شناسه:' + CAST(@ID AS NVARCHAR(16));
			EXEC [dbo].[Users_SendSmsForParentOfUser]
						@UserGuid = @UserGuid,
						@SmsText = @SmsText
		END

		DELETE [dbo].[Recipients] WHERE [ScheduledSmsGuid] = @Guid;
		DELETE [dbo].[ScheduledSmses] WHERE [Guid] = @Guid;
	END
	ELSE IF(@Status = 7)--Extracted
	BEGIN
		SELECT 
			@Number = [Number],
			@Priority = [Priority],
			@DeliveryBase = [DeliveryBase]
		FROM 
			[dbo].[PrivateNumbers] WITH (NOLOCK)
		WHERE
			[Guid] = @PrivateNumberGuid;
		
			SET @RecordXML = (SELECT * FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid FOR XML PATH('Request'))

			INSERT INTO [dbo].[Outboxes]
								( [Guid],
									[ID],
									[CheckId],
									[ExportDataStatus],
									[SendStatus],
									[SavedReceiverCount],
									[PrivateNumberGuid],
									[SmsSenderAgentReference],
									[SenderId],
									[SmsPriority],
									[IsUnicode],
									[SmsLen],
									[SmsText],
									[SendingTryCount],
									[SentDateTime],
									[CreateDate],
									[DeliveryNeeded],
									[IsFlash],
									[SmsSendType],
									[RequestXML],
									[UserGuid])
						SELECT 
									@Guid,
									[ID],
									[CheckId],
									1,--None
									2,--WatingForSend
									0,
									[PrivateNumberGuid],
									@SmsSenderAgentReference,
									@Number,
									@Priority,
									CASE 
										WHEN [Encoding] = 2 THEN 1
										ELSE 0
									END,
									[SmsLen],
									[SmsText],
									0,
									GETDATE(),
									GETDATE(),
									@DeliveryBase,
									CASE 
										WHEN [PresentType] = 0 THEN 1
										ELSE 0
									END,
									[TypeSend],
									@RecordXMl,
									[UserGuid]
						FROM 
								[dbo].[ScheduledSmses] WITH (NOLOCK)
						WHERE [Guid] = @Guid;

		EXEC [dbo].[Outboxes_DecreaseSmsSendPrice] @Guid;

		UPDATE [dbo].[ScheduledSmses] SET [Status] = 4 --Ready
		WHERE [Guid] = @Guid;
	END

	COMMIT TRANSACTION
END TRY
BEGIN CATCH

  IF (XACT_STATE() = -1)
    ROLLBACK TRANSACTION;
  IF (XACT_STATE() = 1)
    COMMIT TRANSACTION;

	DECLARE @FailedType INT = 9;--SendError
	DECLARE @State INT = 3;

	IF(ERROR_NUMBER() = 60000)--NotEnoughCredit
	BEGIN
		SET @FailedType = 1;
		SET @State = 5;
	END
	ELSE IF(ERROR_NUMBER() = 61000)--RecipientNotExist
		SET @State = 5;--Failed
	ELSE
		SET @State = 3;--FailedExtract

 UPDATE dbo.ScheduledSmses 
 SET 
 	[SmsSendFaildType] = @FailedType,
 	[SmsSendError] = ERROR_MESSAGE(),
	[Status] = @State
 WHERE [Guid] = @Guid;

 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());
 
 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ProcessRequest',
			@Name = '' ,
			@Text = @Text,
			@IP = '',
			@Browser = '',
			@ReferenceGuid = @Guid,
			@UserGuid = '00000000-0000-0000-0000-000000000000';
END CATCH;
GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_ResendSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_ResendSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE @SendType INT;

SELECT @SendType = [TypeSend] FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid;

UPDATE
	[dbo].[ScheduledSmses]
SET
	[SmsSendFaildType] = 0,
	[DateTimeFuture] = GETDATE(),
	[SmsSendError] = '',
	[Status] = 1
WHERE
	[Guid] = @Guid;

IF(@SendType = 7) --bulk
BEGIN
	UPDATE [dbo].[ScheduledBulkSmses] SET [Status] = 1 WHERE [Guid] = @Guid;
END
GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_SendFormatSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_SendFormatSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE	@ReferenceGuid UNIQUEIDENTIFIER,
				@UserGuid UNIQUEIDENTIFIER,
				@PhoneBookGuid UNIQUEIDENTIFIER,
				@Reciever NVARCHAR(16),
				@Operator TINYINT,
				@SmsBody NVARCHAR(MAX),
				@SmsPartCount TINYINT,
				@Encoding TINYINT,
				@NewGuid UNIQUEIDENTIFIER,
				@IsBlackList BIT;

BEGIN TRY
	
	DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER;

	SELECT
		@ReferenceGuid = CAST([ReferenceGuid] AS UNIQUEIDENTIFIER),
		@UserGuid = [UserGuid],
		@PrivateNumberGuid = [PrivateNumberGuid]
	FROM
		[dbo].[ScheduledSmses] WHERE [Guid] = @Guid;

	SELECT TOP 1 @PhoneBookGuid = [PhoneBookGuid] FROM [dbo].[SmsFormatPhoneBooks] WHERE [SmsFormatGuid] = @ReferenceGuid;

	DECLARE @SmsFormatInfo TABLE([ID] INT IDENTITY (1, 1) Primary key NOT NULL ,[Guid] UNIQUEIDENTIFIER,[Reciever] NVARCHAR(16),[IsBlackList] BIT,[Operator] TINYINT, [SmsBody] NVARCHAR(MAX),[SmsPartCount] TINYINT,[Encoding] TINYINT)
	
	INSERT INTO @SmsFormatInfo ([Guid],Reciever,Operator,SmsBody) SELECT
		NEWID(),
		[CellPhone],
		[Operator],
		dbo.GenerateSmsFromFormat([Guid],@ReferenceGuid) AS SmsBody
	FROM
		[dbo].[PhoneNumbers]
	WHERE
		[IsDeleted] = 0 AND
		[Operator] > 0 AND
		[PhoneBookGuid] = @PhoneBookGuid;

	UPDATE @SmsFormatInfo
	SET
		[SmsPartCount] = [dbo].[GetSmsCount]([SmsBody]),
		[Encoding] = [dbo].[HasUniCodeCharacter]([SmsBody]);

	DECLARE OutboxCursor CURSOR FAST_FORWARD READ_ONLY FOR
	SELECT [Guid],[Reciever],[IsBlackList],[Operator],[SmsBody],[SmsPartCount],[Encoding] FROM @SmsFormatInfo
	 
	OPEN OutboxCursor
	FETCH NEXT FROM OutboxCursor INTO @NewGuid,@Reciever,@IsBlackList,@Operator,@SmsBody,@SmsPartCount,@Encoding
	WHILE @@FETCH_STATUS = 0 
	BEGIN
			INSERT	INTO [dbo].[ScheduledSmses]
			        ([Guid] ,
			         [PrivateNumberGuid] ,
			         [SmsText] ,
			         [PresentType] ,
			         [Encoding] ,
			         [SmsLen] ,
			         [TypeSend] ,
							 [Status],
			         [DateTimeFuture] ,
			         [CreateDate] ,
			         [SmsSenderAgentReference],
							 [IsDeleted],
			         [UserGuid])
							SELECT
								@NewGuid,
								[PrivateNumberGuid] ,
								@SmsBody,
								[PresentType] ,
								@Encoding ,
								@SmsPartCount,
								3,--SendFormatSms
								7,--Extracted
								GETDATE() ,
								GETDATE(),
								[SmsSenderAgentReference] ,
								0,
								[UserGuid]
							FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid

			INSERT INTO [dbo].[Recipients]
			        ([Guid] ,
			         [Mobile] ,
			         [Operator] ,
							 [IsBlackList],
			         [ScheduledSmsGuid])
							SELECT
								NEWID(),
								[Reciever] ,
								[Operator] ,
								[IsBlackList],
								@NewGuid
							FROM
								@SmsFormatInfo
							WHERE
								[Guid] = @NewGuid;


  FETCH NEXT FROM OutboxCursor INTO @NewGuid,@Reciever,@IsBlackList,@Operator,@SmsBody,@SmsPartCount,@Encoding
	END
	CLOSE OutboxCursor
	DEALLOCATE OutboxCursor

	EXEC [dbo].[InsertLog]
				@Type = 3, --Action
				@Source = 'ScheduledSmses',
				@Name = 'Send Format Sms' ,
				@Text = N'Extract Scheduled Pack Numbers Into Outbox' ,
				@IP = '',
				@Browser = '',
				@ReferenceGuid = @Guid,
				@UserGuid = @UserGuid;

END TRY
BEGIN CATCH
	THROW;
END CATCH;


GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_SendGradualSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_SendGradualSms]
	@Guid UNIQUEIDENTIFIER
  AS

	DECLARE @ReferenceGuid NVARCHAR(MAX),
					@Number nvarchar(50),
					@IsUnicode BIT,
					@IsFlash BIT,
					@PrivateNumberGuid UNIQUEIDENTIFIER,
					@Priority INT,
					@DeliveryBase BIT,
					@SmsSenderAgentReference INT,
					@SendPageNo INT,
					@SendPageSize INT,
					@UserGuid UNIQUEIDENTIFIER,
					@MobileCount INT;

DECLARE @Numbers [dbo].[Recipient];
DECLARE @IsComplete BIT	= 0;

BEGIN TRY
	SELECT 
			@PrivateNumberGuid = [PrivateNumberGuid],
			@SendPageNo = ISNULL([SendPageNo],0),
			@SendPageSize = [SendPageSize],
			@ReferenceGuid = [ReferenceGuid],
			@UserGuid = [UserGuid]
	FROM [dbo].[ScheduledSmses] WITH (NOLOCK) WHERE [Guid] = @Guid;

	DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();

	INSERT INTO @Numbers ([Mobile])
	SELECT [Mobile] FROM [dbo].[Recipients]
	WHERE
		[ScheduledSmsGuid] = @Guid
	ORDER BY [ID]
	OFFSET @SendPageNo * @SendPageSize ROWS
	FETCH NEXT @SendPageSize ROWS ONLY;

	SELECT @MobileCount = Count([Mobile]) FROM @Numbers;

	IF(@MobileCount>0)
	BEGIN
		INSERT INTO [dbo].[ScheduledSmses]
			        ([Guid] ,
			         [PrivateNumberGuid] ,
			         [SmsText] ,
			         [PresentType] ,
			         [Encoding] ,
			         [SmsLen] ,
			         [TypeSend] ,
							 [Status],
			         [DateTimeFuture] ,
			         [CreateDate] ,
			         [SmsSenderAgentReference] ,
							 [IsDeleted],
			         [UserGuid])
							SELECT
								@NewGuid,
								[PrivateNumberGuid] ,
								[SmsText],
								[PresentType] ,
								[Encoding] ,
								[SmsLen],
								6,--SendGradualSms
								7,--Extracted
								DATEADD(MINUTE,10,GETDATE()),
								GETDATE(),
								[SmsSenderAgentReference] ,
								0,
								[UserGuid]
							FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid;

		EXEC [dbo].[Recipients_InsertTable]
				 @Receivers =	@Numbers,
				 @ScheduledSmsGuid = @NewGuid;

	END
	ELSE IF(LEN(@ReferenceGuid)>0)
	BEGIN
		SELECT
			[Item]
			INTO #Temp
		FROM
		[dbo].[SplitString](@ReferenceGuid,',')
		OPTION (MAXRECURSION 0)

		INSERT INTO @Numbers ([Mobile])
		SELECT [CellPhone] FROM [dbo].[PhoneNumbers]
		WHERE [IsDeleted] = 0 AND [Operator] > 0 AND [PhoneBookGuid] IN (SELECT * FROM #Temp)
		ORDER BY [CellPhone]
		OFFSET  @SendPageNo * @SendPageSize  ROWS
		FETCH NEXT @SendPageSize ROWS ONLY;

		IF (SELECT COUNT(*) FROM @Numbers) > 0
		BEGIN
			INSERT INTO [dbo].[ScheduledSmses]
			      ([Guid] ,
			        [PrivateNumberGuid] ,
			        [SmsText] ,
			        [PresentType] ,
			        [Encoding] ,
			        [SmsLen] ,
			        [TypeSend] ,
							[Status],
			        [DateTimeFuture] ,
			        [CreateDate] ,
			        [SmsSenderAgentReference] ,
							[IsDeleted],
			        [UserGuid])
						SELECT
							@NewGuid,
							[PrivateNumberGuid] ,
							[SmsText],
							[PresentType] ,
							[Encoding] ,
							[SmsLen],
							6,--SendGradualSms
							7,--Extracted
							DATEADD(MINUTE,10,GETDATE()),
							GETDATE(),
							[SmsSenderAgentReference] ,
							0,
							[UserGuid]
						FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid
					    
			EXEC [dbo].[Recipients_InsertTable]
					 @Receivers =	@Numbers,
					 @ScheduledSmsGuid = @NewGuid;
		END
		ELSE
		BEGIN
			SET @IsComplete = 1;
		END

		DROP TABLE #Temp;
	END
	ELSE
	BEGIN
		SET @IsComplete = 1;
	END

	SET @SendPageNo += 1;
	UPDATE [dbo].[ScheduledSmses]
	SET
		[DateTimeFuture] = DATEADD(MINUTE,[Period],[DateTimeFuture]),
		[SendPageNo] = @SendPageNo,
		[Status] = CASE 
									WHEN @IsComplete = 1 THEN 6
									ELSE 1
							 END
	WHERE [Guid] = @Guid;

	EXEC [dbo].[InsertLog]
				@Type = 3, --Action
				@Source = 'ScheduledSmses',
				@Name = 'Send Gradual Sms' ,
				@Text = N'Extract Scheduled Pack Numbers Into Outbox' ,
				@IP = '',
				@Browser = '',
				@ReferenceGuid = @Guid,
				@UserGuid = @UserGuid;
END TRY
BEGIN CATCH
	THROW;
END CATCH;


GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_SendGroupSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_SendGroupSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE	@ReferenceGuid NVARCHAR(MAX);
DECLARE	@UserGuid UNIQUEIDENTIFIER;

BEGIN TRY
	SELECT 
		@ReferenceGuid = [ReferenceGuid],
		@UserGuid = [UserGuid]
	FROM 
		[dbo].[ScheduledSmses]
	WHERE
		[Guid] = @Guid;

	EXEC [dbo].[Recipients_InsertFromPhonebook]
			 @Groups = @ReferenceGuid,
			 @ScheduledSmsGuid = @Guid

	UPDATE [dbo].[ScheduledSmses] SET [Status] = 7--Extracted
	WHERE [Guid] = @Guid;

	EXEC [dbo].[InsertLog]
				@Type = 3, --Action
				@Source = 'ScheduledSmses',
				@Name = 'Send Group Sms' ,
				@Text = N'Extract Scheduled Pack Numbers' ,
				@IP = '',
				@Browser = '',
				@ReferenceGuid = @Guid,
				@UserGuid = @UserGuid;
	
END TRY
BEGIN CATCH
	THROW;
END CATCH;



GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_SendP2PSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_SendP2PSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE	@IsFlash INT;
DECLARE	@PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE	@SmsSenderAgentReference INT;
DECLARE	@UserGuid UNIQUEIDENTIFIER;
DECLARE	@LogText NVARCHAR(1024);
DECLARE @FilePath NVARCHAR(128) = '';
DECLARE @SmsPattern NVARCHAR(1024) = '';
DECLARE @AppPath NVARCHAR(MAX) = '';

SET NOCOUNT ON;

BEGIN TRY
SELECT 
	@PrivateNumberGuid = [PrivateNumberGuid],
	@IsFlash = CASE 
								WHEN [PresentType] = 0 THEN 1
								ELSE 0
							END,
	@UserGuid = [UserGuid],
	@SmsSenderAgentReference = [SmsSenderAgentReference],
	@FilePath = [FilePath],
	@SmsPattern = [SmsPattern]
FROM [dbo].[ScheduledSmses] WITH (NOLOCK) WHERE [Guid] = @Guid;

SELECT @AppPath = [Value] FROM [dbo].[Settings] WHERE [Key] = 16;--AppPath;
SET @FilePath = @AppPath + @FilePath;

EXEC('
DECLARE @P2PSms TABLE(
	[ID] INT IDENTITY (1, 1) Primary key NOT NULL ,
	[Guid] UNIQUEIDENTIFIER,
	[Receiver] NVARCHAR(16),
	[IsBlackList] BIT,
	[Operator] TINYINT,
	[SmsText] NVARCHAR(1024),
	[SmsLen] TINYINT,
	[Encoding] TINYINT);

select * INTO #temp FROM OPENROWSET(''Microsoft.ACE.OLEDB.12.0'', 
''Excel 12.0;Database='+@FilePath+';HDR=NO'', 
''SELECT * FROM [Sheet1$]'')

DELETE FROM #temp WHERE dbo.GetNumberOperator([F1]) = 0

INSERT INTO @P2PSms([Guid],[Receiver],[SmsText],[IsBlackList])
SELECT NEWID(),[F1],CONCAT('+@SmsPattern+'),0 FROM #temp;

UPDATE @P2PSms
SET
	[SmsLen] = [dbo].[GetSmsCount]([SmsText]),
	[Encoding] = [dbo].[HasUniCodeCharacter]([SmsText]),
	[Operator] = [dbo].[GetNumberOperator]([Receiver]),
	[Receiver] = [dbo].[RepairMobileNumber]([Receiver])

INSERT	INTO [dbo].[ScheduledSmses]
			        ([Guid] ,
			         [PrivateNumberGuid] ,
			         [SmsText] ,
			         [PresentType] ,
			         [Encoding] ,
			         [SmsLen] ,
			         [TypeSend] ,
							 [Status],
			         [DateTimeFuture] ,
			         [CreateDate] ,
			         [SmsSenderAgentReference],
							 [IsDeleted],
			         [UserGuid])
							SELECT
								[Guid],
								'''+ @PrivateNumberGuid +''',
								[SmsText],
								1 ,--Normal
								[Encoding] ,
								[SmsLen],
								10,--SendP2PSms
								7,--Extracted
								GETDATE() ,
								GETDATE(),
								'+ @SmsSenderAgentReference +' ,
								0,
								'''+ @UserGuid +'''
							FROM @P2PSms;

			INSERT INTO [dbo].[Recipients]
			        ([Guid] ,
			         [Mobile] ,
			         [Operator] ,
							 [IsBlackList],
			         [ScheduledSmsGuid])
							SELECT
								NEWID(),
								[Receiver] ,
								[Operator] ,
								[IsBlackList],
								[Guid]
							FROM
								@P2PSms;

DELETE FROM [dbo].[ScheduledSmses] WHERE [Guid] = '''+ @Guid +''';
DROP TABLE #temp;
');

END TRY
BEGIN CATCH
	THROW;
END CATCH;

GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_SendPeriodSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_SendPeriodSms]
	@Guid UNIQUEIDENTIFIER
  AS

	DECLARE 
	@ReferenceGuid NVARCHAR(MAX),
	@EndDateTime DATETIME,
	@DateTimeFuture DATETIME,
	@NextDateTime DATETIME,
	@Period INT,
	@PeriodType INT,
	@UserGuid UNIQUEIDENTIFIER;

BEGIN TRY
	SELECT 
			@DateTimeFuture = [DateTimeFuture],
			@EndDateTime = [EndDateTime],
			@Period = [Period],
			@PeriodType = [PeriodType],
			@ReferenceGuid = [ReferenceGuid],
			@UserGuid = [UserGuid]
	FROM [dbo].[ScheduledSmses] WHERE [Guid] = @Guid;

	DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();

	INSERT INTO [dbo].[ScheduledSmses]
							([Guid] ,
							 [PrivateNumberGuid] ,
							 [SmsText] ,
							 [PresentType] ,
							 [Encoding] ,
							 [SmsLen] ,
							 [TypeSend] ,
							 [Status],
							 [DateTimeFuture] ,
							 [CreateDate] ,
							 [SmsSenderAgentReference] ,
							 [UserGuid])
						SELECT 
								@NewGuid,
								[PrivateNumberGuid],
								[SmsText],
								[PresentType],
								[Encoding],
								[SmsLen],
								5,--SendPeriodSms
								7,--Extracted
								GETDATE(),
								GETDATE(),
								[SmsSenderAgentReference],
								[UserGuid]
						FROM 
								[dbo].[ScheduledSmses]
						WHERE [Guid] = @Guid;
	
	INSERT INTO [dbo].[Recipients]
		      ([Guid] ,
		       [Mobile] ,
		       [Operator] ,
					 [IsBlackList],
		       [ScheduledSmsGuid])
					SELECT
						NEWID(),
						[Mobile],
						[Operator],
						[IsBlackList],
						@NewGuid
					FROM
						[dbo].[Recipients]
					WHERE
						[ScheduledSmsGuid] = @Guid;
		
	IF(ISNULL(@ReferenceGuid,'') != '')
	BEGIN
		EXEC [dbo].[Recipients_InsertFromPhonebook]
				 @Groups = @ReferenceGuid,
				 @ScheduledSmsGuid = @NewGuid;
	END
		
	SET	@NextDateTime = CASE 
										WHEN @PeriodType = 1 THEN DATEADD(MINUTE,@Period,@DateTimeFuture)
										WHEN @PeriodType = 2 THEN DATEADD(HOUR,@Period,@DateTimeFuture)
										WHEN @PeriodType = 3 THEN DATEADD(DAY,@Period,@DateTimeFuture)
										WHEN @PeriodType = 4 THEN DATEADD(WEEK,@Period,@DateTimeFuture)
										WHEN @PeriodType = 5 THEN DATEADD(MONTH,@Period,@DateTimeFuture)
										WHEN @PeriodType = 6 THEN DATEADD(YEAR,@Period,@DateTimeFuture)
									END
		
	IF(@NextDateTime <= @EndDateTime)
	BEGIN
		UPDATE [dbo].[ScheduledSmses]
		SET [DateTimeFuture] = @NextDateTime,[Status] = 1--Stored
		WHERE [Guid] = @Guid;
	END
	ELSE
	BEGIN
		UPDATE [dbo].[ScheduledSmses] SET [IsDeleted] = 1 WHERE [Guid] = @Guid;
	END

	EXEC [dbo].[InsertLog]
				@Type = 3, --Action
				@Source = 'ScheduledSmses',
				@Name = 'Send Period Sms' ,
				@Text = N'Extract Scheduled Pack Numbers Into Outbox' ,
				@IP = '',
				@Browser = '',
				@ReferenceGuid = @Guid,
				@UserGuid = @UserGuid;
END TRY
BEGIN CATCH
	THROW;
END CATCH;


GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_SendSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ScheduledSmses_SendSms]
	@Guid UNIQUEIDENTIFIER
  AS


DECLARE @Id BIGINT;
DECLARE @Number nvarchar(50);
DECLARE	@IsUnicode INT;
DECLARE	@IsFlash INT;
DECLARE	@PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE	@Priority INT;
DECLARE	@DeliveryBase BIT;
DECLARE	@SmsSenderAgentReference INT;
DECLARE	@UserGuid UNIQUEIDENTIFIER;
DECLARE	@QueueName NVARCHAR(32);
DECLARE	@QueueType NVARCHAR(32);
DECLARE	@QueueLength INT;
DECLARE	@ReceiverCount INT = 0;
DECLARE	@SendQueueRecipientAddress NVARCHAR(4000);
DECLARE	@Username NVARCHAR(32);
DECLARE	@Password NVARCHAR(32);
DECLARE	@Domain NVARCHAR(32);
DECLARE	@Link NVARCHAR(512);
DECLARE	@SendLink NVARCHAR(512);
DECLARE	@ReceiveLink NVARCHAR(512);
DECLARE	@DeliveryLink NVARCHAR(512);
DECLARE	@RouteActive BIT;
DECLARE	@SmsText NVARCHAR(MAX) = '';
DECLARE	@AgentGuid UNIQUEIDENTIFIER;
DECLARE	@SmsLen INT;
DECLARE	@IsSmpp BIT;
DECLARE	@Ret NVARCHAR(MAX);
DECLARE	@OperatorName NVARCHAR(32);
DECLARE	@ServiceId NVARCHAR(32);
DECLARE	@PageNo INT;
DECLARE	@ThreadCount INT = 1;
DECLARE	@TryCount INT;
DECLARE	@TypeSend INT;
DECLARE	@LogText NVARCHAR(1024);
DECLARE	@Receivers NVARCHAR(MAX) = '';
DECLARE @IsRemoteQueue INT;
DECLARE @RemoteQueueIP NVARCHAR(32);
--DECLARE @VoiceURL NVARCHAR(500);
--DECLARE @voiceMessageId INT;

SET NOCOUNT ON;

BEGIN TRY
	SELECT
		@Id = [ID],
		@PrivateNumberGuid = [PrivateNumberGuid],
		@SmsText = [SmsText],
		@IsUnicode = CASE 
										WHEN [Encoding] = 2 THEN 1
										ELSE 0
									END,
		@SmsLen = [SmsLen],
		@IsFlash = CASE 
									WHEN [PresentType] = 0 THEN 1
									ELSE 0
								END,
		@UserGuid = [UserGuid],
		@PageNo = ISNULL([SendPageNo],0),
		@TypeSend = [TypeSend]--,
		--@VoiceURL= [VoiceURL],
		--@voiceMessageId= [voiceMessageId]
	FROM [dbo].[ScheduledSmses] WITH (NOLOCK) WHERE [Guid] = @Guid;

	SELECT 
		@Number = number.[Number],
		@Priority = number.[Priority],
		@DeliveryBase = number.[DeliveryBase],
		@ServiceId = number.[ServiceID],
		@SmsSenderAgentReference = agent.[SmsSenderAgentReference],
		@QueueLength = agent.[QueueLength],
		@RouteActive = agent.[RouteActive],
		@AgentGuid = agent.[Guid],
		@IsSmpp = agent.[IsSmpp],
		@Username = agent.[Username],
		@Password = agent.[Password],
		@Domain = agent.[Domain],
		@SendLink = agent.[SendLink],
		@ReceiveLink = agent.[ReceiveLink],
		@DeliveryLink = agent.[DeliveryLink]
	FROM 
		[dbo].[PrivateNumbers] number  WITH (NOLOCK) INNER JOIN
		[dbo].[SmsSenderAgents] agent  WITH (NOLOCK) ON number.[SmsSenderAgentGuid] = agent.[Guid]
	WHERE
		number.[Guid] = @PrivateNumberGuid;

	SET @QueueLength = ISNULL(@QueueLength,90);

	SELECT @TryCount = [Value] FROM [dbo].[Settings] WHERE [Key] = 12 --MaximumFailedTryCount
	SELECT @IsRemoteQueue = ISNULL([Value],0) FROM [dbo].[Settings] WHERE [Key] = 14 --IsRemoteQueue
	SELECT @RemoteQueueIP = [Value] FROM [dbo].[Settings] WHERE [Key] = 15 --RemoteQueueIP

	IF(@IsRemoteQueue = 1 AND ISNULL(@RemoteQueueIP,'') = '')
		THROW 62000,'آی پی سرور ریموت نامعتبر است',1;

	DECLARE @Status INT = 4;

	IF(@TypeSend IN (1,3,8,10))
		SET @QueueType = '-single';
	ELSE IF(@TypeSend IN (2,5,6,11))
		SET @QueueType = '-group';
	
	SET @TryCount = ISNULL(@TryCount,1);

	IF(ISNULL(@RouteActive,0) = 0)
	BEGIN
		SET @QueueName =
			CASE
				WHEN @SmsSenderAgentReference = 1 THEN	'Magfa'
				WHEN @SmsSenderAgentReference = 2 THEN	'Asanak'
				WHEN @SmsSenderAgentReference = 3 THEN	'Armaghan'
				WHEN @SmsSenderAgentReference = 4 THEN	'AradBulk'
				WHEN @SmsSenderAgentReference = 5 THEN	'RahyabRG'
				WHEN @SmsSenderAgentReference = 6 THEN	'RahyabPG'
				WHEN @SmsSenderAgentReference = 7 THEN	'SlS'
				WHEN @SmsSenderAgentReference = 8 THEN	'Shreeweb'
				WHEN @SmsSenderAgentReference = 9 THEN	'AradVas'
				WHEN @SmsSenderAgentReference = 10 THEN 'SocialNetworks'
				WHEN @SmsSenderAgentReference = 11 THEN 'FFF'
				WHEN @SmsSenderAgentReference = 12 THEN 'GSM'
				WHEN @SmsSenderAgentReference = 13 THEN 'Avanak'
				WHEN @SmsSenderAgentReference = 14 THEN 'Mobbis'
			END;
		
		SET @QueueName += @QueueType;

		IF(ISNULL(@IsSmpp,0) = 1)
			SET @QueueLength = (@QueueLength / @SmsLen);
		
		WHILE (@ThreadCount <= 20)
		BEGIN
			SELECT [Mobile],[Operator],[IsBlackList] INTO #numbers FROM  [Arad.SMS.Gateway.DB].[dbo].[Recipients] WITH(NOLOCK)	WHERE [ScheduledSmsGuid] = @Guid ORDER BY [ID] OFFSET (@PageNo * @QueueLength) ROWS FETCH NEXT @QueueLength ROWS ONLY;

			IF((SELECT COUNT(*) FROM #numbers) > 0)
			BEGIN
				SET @Receivers = (SELECT * FROM #numbers FOR XML PATH('Node'), ROOT('Root'));
				
				EXEC @Ret = dbo.SendSms
						@Queue = @QueueName,
						@IsRemoteQueue = @IsRemoteQueue,
						@RemoteQueueIP = @RemoteQueueIP,
						@SmsSendType = @TypeSend,
				    @PageNo = @PageNo,
				    @Sender = @Number,
						@PrivateNumberGuid = @PrivateNumberGuid,
						@TotalCount = 0,
				    @Receivers = @Receivers,
				    @ServiceId = @ServiceId,
				    @Message = @SmsText,
				    @SmsLen = @SmsLen,
				    @TryCount = @TryCount,
				    @SmsIdentifier = 0,
				    @SmsPartIndex = 0,
				    @IsFlash = @IsFlash,
				    @IsUnicode = @IsUnicode,
						@Id = @Id,
				    @Guid = @Guid,
				    @Username = @Username,
						@Password = @Password,
						@Domain = @Domain,
						@SendLink = @SendLink,
						@ReceiveLink = @ReceiveLink,
						@DeliveryLink = @DeliveryLink,
						@AgentReference = @SmsSenderAgentReference;--,
						--@voiceURL = @voiceURL,
					    --@voiceMessageId = @voiceMessageId

				IF(ISNULL(@Ret,'') != '' AND @Ret != 0)
				BEGIN
					SET @PageNo += 1;
					SET @ThreadCount += 1;
				END
			END
			ELSE
			BEGIN
				SET @Status = 6;--completed
			 BREAK;
			END

			DROP TABLE #numbers;
		END
		UPDATE [dbo].[ScheduledSmses] SET [SendPageNo] = @PageNo,[Status] = @Status WHERE [Guid] = @Guid;
	END
	ELSE
	BEGIN
		DECLARE @OperatorID TINYINT;
		DECLARE @IsComplete BIT = 1;
		DECLARE @ItemId NVARCHAR(64)='';

		IF(@SmsSenderAgentReference = 9)
			SET @QueueType = 'vas' + @QueueType;
			
		WHILE (@ThreadCount <= 20)
		BEGIN
			SET @IsComplete = 1;
			DECLARE OperatorCursor CURSOR FAST_FORWARD READ_ONLY FOR
			SELECT
				[OperatorID],
				opt.[Name],
				[Username],
				[Password],
				[Domain],
				[Link],
				[QueueLength]
			FROM
				[dbo].[Routes] WITH(NOLOCK) INNER JOIN
				[dbo].[Operators] opt WITH(NOLOCK) ON [dbo].[Routes].[OperatorID] = opt.[ID]
			WHERE
				[SmsSenderAgentGuid] = @AgentGuid;

			OPEN OperatorCursor
			FETCH NEXT FROM OperatorCursor INTO @OperatorID,@OperatorName,@Username,@Password,@Domain,@Link,@QueueLength
			WHILE @@FETCH_STATUS = 0
			BEGIN
				IF(ISNULL(@IsSmpp,0) = 1)
					SET @QueueLength = (@QueueLength / @SmsLen);

				SET @QueueName = @OperatorName + @QueueType;

				SELECT [Mobile],[Operator],[IsBlackList] INTO #num FROM  [Arad.SMS.Gateway.DB].[dbo].[Recipients] WITH(NOLOCK)	WHERE [ScheduledSmsGuid] = @Guid AND [Operator] = @OperatorID ORDER BY [ID] OFFSET (@PageNo * @QueueLength) ROWS FETCH NEXT @QueueLength ROWS ONLY;
				
				IF((SELECT COUNT(*) FROM #num) > 0)
				BEGIN
					SET @Receivers = (SELECT * FROM #num FOR XML PATH('Node'), ROOT('Root'));

					EXEC @Ret = dbo.SendSms
							@Queue = @QueueName,
							@IsRemoteQueue = @IsRemoteQueue,
							@RemoteQueueIP = @RemoteQueueIP,
							@SmsSendType = @TypeSend,
							@PageNo = @PageNo,
							@Sender = @Number,
							@PrivateNumberGuid = @PrivateNumberGuid,
							@TotalCount = 0,
							@Receivers = @Receivers,
							@ServiceId = @ServiceId,
							@Message = @SmsText,
							@SmsLen = @SmsLen,
							@TryCount = @TryCount,
							@SmsIdentifier = 0,
							@SmsPartIndex = 0,
							@IsFlash = @IsFlash,
							@IsUnicode = @IsUnicode,
							@Id = @Id,
							@Guid = @Guid,
							@Username = @Username,
							@Password = @Password,
							@Domain = @Domain,
							@SendLink = @Link,
							@ReceiveLink = @ReceiveLink,
							@DeliveryLink = '',
							@AgentReference = @SmsSenderAgentReference;--,
						   -- @voiceURL = @voiceURL,
					        --@voiceMessageId = @voiceMessageId;

					SET @IsComplete = 0;
				END
				
				DROP TABLE #num;
				FETCH NEXT FROM OperatorCursor INTO @OperatorID,@OperatorName,@Username,@Password,@Domain,@Link,@QueueLength
			END
			CLOSE OperatorCursor
			DEALLOCATE OperatorCursor

			SET @PageNo += 1;
			IF(@IsComplete = 1)
				SET @Status = 6;--completed

			IF(@Status = 6)
				BREAK;

			SET @ThreadCount += 1;
		END

		UPDATE [dbo].[ScheduledSmses] SET [SendPageNo] = @PageNo,[Status] = @Status WHERE [Guid] = @Guid;
	END

	EXEC [dbo].[InsertLog]
	    	@Type = 3, --Action
				@Source = 'ScheduledSmses',
				@Name = 'Send Sms' ,
				@Text = N'Send Sms To Queue ' ,
				@IP = '',
				@Browser = '',
				@ReferenceGuid = @Guid,
				@UserGuid = @UserGuid;

END TRY
BEGIN CATCH
 DECLARE @Text NVARCHAR(MAX) = 'Error:' + ERROR_MESSAGE() + ';ProcedureName:' + ISNULL(ERROR_PROCEDURE(),'') + ';Line:' +  CONVERT(NVARCHAR,ERROR_LINE());

 EXEC [dbo].[InsertLog]
			@Type = 2, --Error
			@Source = 'ProcessRequest',
			@Name = '' ,
			@Text = @Text,
			@IP = '',
			@Browser = '',
			@ReferenceGuid = @Guid,
			@UserGuid = @UserGuid;
	THROW;
END CATCH;

GO
/****** Object:  StoredProcedure [dbo].[ScheduledSmses_UpdateStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ScheduledSmses_UpdateStatus]
	@Guid UNIQUEIDENTIFIER,
	@Status INT
  AS


UPDATE
	[ScheduledSmses]
SET
	[Status] = @Status
WHERE
	[Guid] = @Guid;

UPDATE
	[ScheduledBulkSmses]
SET
	[Status] = @Status
WHERE
	[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[ServiceGroups_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ServiceGroups_Delete]
    @Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [ServiceGroups]
			SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[ServiceGroups_GetPagedServiceGroup]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[ServiceGroups_GetPagedServiceGroup] 
	@Title NVARCHAR(MAX),
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS
 
   DECLARE @Where NVARCHAR(MAX)='[IsDeleted]=0'
	 
	 IF ( ISNULL(@Title, '') != '' ) 
		BEGIN
				IF ( @Where != '' ) 
						SET @Where += ' AND'
				SET @Where += ' [Title] LIKE N''%' + @Title + '%'''
		END
	
    IF ( @Where != '' ) 
        SET @Where = ' WHERE ' + @Where
---------------------------------------------

EXEC('SELECT
					Menu.[Guid] ,
					Menu.[Order] ,
					Menu.[Title] ,
					Menu.[IconAddress] ,
					Menu.[LargeIcon] ,
					Menu.[CreateDate] ,
					ISNULL(Item.[CountService], 0) AS [CountService]
					INTO #TempServiceGroup
			FROM [ServiceGroups] Menu LEFT JOIN (SELECT  COUNT(ServiceGroupGuid) AS CountService ,
																							ServiceGroupGuid
																			FROM	[Services]
																			GROUP BY [ServiceGroupGuid]) Item
			ON Menu.Guid = Item.ServiceGroupGuid' +	@Where + '
    
    
			SELECT COUNT(*) AS [RowCount] FROM #TempServiceGroup;
	
		WITH expTemp AS
		(
			SELECT
					Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
					*
			FROM
					#TempServiceGroup
		)
		SELECT 
				*
		FROM
			expTemp
		WHERE 
			(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
			([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
		ORDER BY
			 [RowNumber] ;
     
			
		DROP TABLE #TempServiceGroup')


GO
/****** Object:  StoredProcedure [dbo].[ServiceGroups_GetParentGroups]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ServiceGroups_GetParentGroups]
  AS

DECLARE @EmptyGuid UNIQUEIDENTIFIER='00000000-0000-0000-0000-000000000000';
SELECT 
			groups.[Guid] AS [GroupGuid],
			groups.[Title] AS [GroupTitle],
			groups.[TitleFa] AS [GroupTitleFa],
      groups.[IconAddress] AS [GroupIcon],
      groups.[LargeIcon] AS [GroupLargeIcon],
      groups.[Order],
      groups.[ParentGuid] AS [ParentGuid] 
FROM
			[ServiceGroups] groups 
WHERE
			groups.[IsDeleted] = 0 AND
			groups.[ParentGuid] = @EmptyGuid
ORDER BY
			groups.[Order]


GO
/****** Object:  StoredProcedure [dbo].[ServiceGroups_GetUserGroupsWithServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ServiceGroups_GetUserGroupsWithServices]
	@UserGuid UNIQUEIDENTIFIER
  AS

	DECLARE @RoleGuid UNIQUEIDENTIFIER;
	SELECT @RoleGuid = [RoleGuid] FROM [dbo].[Users] WHERE [Guid]=@UserGuid;
	
	SELECT  
				[Services].[Title] AS [ServiceTitle],
				[Services].[TitleFa] AS [ServiceTitleFa],
				[Services].[Guid] As [ServiceGuid],
				[Services].[ServiceGroupGuid],
				[Services].[ReferencePageKey],
				[Services].[IconAddress],
				[Services].[LargeIcon] AS [ServiceLargeIcon],
				[Services].[Order] AS [ServiceOrder],
				groups.[Title] AS [GroupTitle],
				groups.[TitleFa] AS [GroupTitleFa],
				groups.[Guid] AS [GroupGuid],
				groups.[ParentGuid],
				groups.[IconAddress] AS [GroupIcon],
				groups.[LargeIcon] AS [GroupLargeIcon],
				groups.[Order] AS [GroupOrder]				
	FROM
				[Services] INNER JOIN
				[RoleServices] ON [Services].[Guid] = [RoleServices].[ServiceGuid] INNER JOIN
				[ServiceGroups] groups ON [Services].[ServiceGroupGuid] = groups.[Guid]
	WHERE
				[Services].[IsDeleted] = 0 AND
				groups.[IsDeleted] = 0 AND
				[Services].[Presentable] = 1 AND
				[RoleServices].[RoleGuid] = @RoleGuid
	ORDER BY
				groups.[Order],
				[Services].[Order]


GO
/****** Object:  StoredProcedure [dbo].[ServiceGroups_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ServiceGroups_Insert]
    @Guid UNIQUEIDENTIFIER ,
    @Title NVARCHAR(MAX) ,
    @IconAddress NVARCHAR(MAX) ,
    @LargeIcon NVARCHAR(MAX),
    @Order INT ,
    @ParentGuid UNIQUEIDENTIFIER ,
    @CreateDate DATETIME
  AS
 
    INSERT  INTO [ServiceGroups]
            ( [Guid] ,
              [Title] ,
              [IconAddress],
              [LargeIcon],
              [Order] ,
              [ParentGuid],
              [CreateDate],
              [IsDeleted])
              
    VALUES  ( @Guid ,
              @Title ,
              @IconAddress,
              @LargeIcon,
              @Order ,
              @ParentGuid,
              @CreateDate,
              0)


GO
/****** Object:  StoredProcedure [dbo].[ServiceGroups_UpdateServiceGroup]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[ServiceGroups_UpdateServiceGroup]
    @Guid UNIQUEIDENTIFIER ,
    @Title NVARCHAR(MAX) ,
    @IconAddress NVARCHAR(MAX) ,
    @LargeIcon NVARCHAR(MAX),
    @ParentGuid UNIQUEIDENTIFIER ,
    @Order INT
  AS
 
    UPDATE  [ServiceGroups]
    SET     
				[Title] = @Title ,
				[IconAddress]=@IconAddress,
				[LargeIcon] = @LargeIcon,
				[ParentGuid]=@ParentGuid,
        [Order] = @Order
    WHERE   
				[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Services_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Services_Delete]
    (
      @Guid UNIQUEIDENTIFIER
    )
  AS
 
    UPDATE  [Services]
    SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Services_GetPagedService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Services_GetPagedService]
    @Query NVARCHAR(MAX) ,
    @PageNo INT ,
    @PageSize INT ,
    @SortField NVARCHAR(256)
  AS
 
	DECLARE @Where AS NVARCHAR(MAX) ='srvc.[IsDeleted] = 0 AND grp.[IsDeleted] = 0';
	DECLARE	@Statement NVARCHAR(MAX) = '';
	DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
	
SET @Where = ' WHERE ' + @Where;
IF(@Query != '')
	SET @Where = @Where + ' AND ' + @Query;
    
--------------------------------------------------

SET @Statement ='
    SELECT 
			grp.[Title] AS [MenuTitle] ,
			srvc.[Guid] ,
			srvc.[Title],
			srvc.[IconAddress] ,
			srvc.[LargeIcon] ,
			srvc.[Order],
			srvc.[CreateDate] ,
			[Presentable] ,
			[ReferencePageKey] ,
			[ReferenceServiceKey] ,
			[ServiceGroupGuid]
			INTO 
				#TempService
		FROM 
				[ServiceGroups] grp INNER JOIN
				[Services] srvc ON grp.[Guid] = srvc.[ServiceGroupGuid] '+ @Where +';
			
		SELECT COUNT(*) AS [RowCount] FROM #TempService;
		
		SELECT * FROM #TempService';
		
		IF(@PageNo != 0 AND @PageSize != 0)
		BEGIN
			SET @Statement +='
				ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
				OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
		END

		SET @Statement +=';DROP TABLE #TempService;';
     
		EXEC(@Statement);


GO
/****** Object:  StoredProcedure [dbo].[Services_GetService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Services_GetService]
    @Title NVARCHAR(50) ,
    @Price DECIMAL(18, 2) ,
    @IsDefault BIT ,
    @Presentable BIT ,
    @ServiceGroupGuid UNIQUEIDENTIFIER ,
    @PriceRange INT
  AS
 
    SELECT  [Guid] ,
            [Title] ,
            --[Price] ,
            --[IsDefault] ,
            [Presentable] ,
            [ReferencePageKey] ,
            [ReferenceServiceKey] ,
            [ServiceGroupGuid] ,
            ISNULL(Permission.[CountAccess], 0) AS [CountAccess]
    FROM    [Services] Capability
            LEFT JOIN ( SELECT  COUNT(Guid) AS CountAccess ,
                                ServiceGuid
                        FROM    [Accesses]
                        GROUP BY ServiceGuid
                      ) Permission ON Capability.Guid = Permission.ServiceGuid
    WHERE   ( @Title = ''
              OR [Title] LIKE '%' + @Title + '%'
            )
            --AND ( @Price = 0
            --      OR ( ( @PriceRange != 1
            --             OR Price > @Price
            --           )
            --           AND ( @PriceRange != 0
            --                 OR Price = @Price
            --               )
            --           AND ( @PriceRange != -1
            --                 OR Price < @Price
            --               )
            --         )
            --    )
            --AND ( ISNULL(@IsDefault, '') = ''
            --      OR [IsDefault] = @IsDefault
            --    )
            AND ( ISNULL(@Presentable, '') = ''
                  OR [Presentable] = @Presentable
                )
            AND ( @ServiceGroupGuid = '00000000-0000-0000-0000-000000000000'
                  OR [ServiceGroupGuid] = @ServiceGroupGuid
                )


GO
/****** Object:  StoredProcedure [dbo].[Services_GetServiceOfUserForDefineGlobalServicePrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Services_GetServiceOfUserForDefineGlobalServicePrice]
  @UserGuid UNIQUEIDENTIFIER
  AS
 
		SELECT 
					[Services].[Guid],
					[Services].[Title],
					servicePrice.[Price]
		FROM 
					[Services] INNER JOIN
					[UserServices] ON [Services].[Guid] = [UserServices].[ServiceGuid] AND
														[UserServices].[UserGuid] = @UserGuid LEFT JOIN 
					[UserServicePrices] servicePrice ON [Type] = 0 AND
																							[Services].[Guid] = servicePrice.[ServiceGuid] AND
																							servicePrice.[UserGuid]=@UserGuid
		WHERE 
					[Services].[IsDeleted]=0


GO
/****** Object:  StoredProcedure [dbo].[Services_GetServiceOfUserForDeterminePrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Services_GetServiceOfUserForDeterminePrice]
  @UserGuid UNIQUEIDENTIFIER,
  @ParentGuid UNIQUEIDENTIFIER
  AS
 
IF(@ParentGuid != '00000000-0000-0000-0000-000000000000')
	BEGIN
		SELECT 
					[Services].[Guid],
					[Services].[Title],
					servicePrice.[Price]
		FROM 
					[Services] INNER JOIN
					[UserServices] ON [Services].[Guid] = [UserServices].[ServiceGuid] LEFT JOIN 
					[UserServicePrices] servicePrice ON [Type] = 1 AND
																							servicePrice.[UserGuid] = @UserGuid AND
																							[Services].[Guid] = servicePrice.[ServiceGuid]
		WHERE 
					[UserServices].[UserGuid] = @ParentGuid AND
					[Services].[IsDeleted]=0
	END
ELSE IF(@ParentGuid = '00000000-0000-0000-0000-000000000000')
	BEGIN
		SELECT  
					[Services].[Guid],
					[Services].[Title],
					ServicePrice.[Price]
		FROM
					[Services] LEFT JOIN
					[UserServicePrices] ServicePrice ON	ServicePrice.[Type] = 1 AND
																							[UserGuid] = @UserGuid AND
																							[Services].[Guid] = ServicePrice.ServiceGuid
		WHERE
					[IsDeleted] = 0 
	END


GO
/****** Object:  StoredProcedure [dbo].[Services_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Services_Insert]
    (
      @Guid UNIQUEIDENTIFIER ,
      @Title NVARCHAR(50) ,
      @IconAddress NVARCHAR(MAX) ,
      @LargeIcon NVARCHAR(MAX),
      @Presentable BIT ,
      @ReferencePageKey INT ,
      @ReferenceServiceKey INT ,
      @Order INT,
      @CreateDate DATETIME,
			@ServiceGroupGuid UNIQUEIDENTIFIER
    )
  AS
 
    INSERT  INTO [Services]
            ( [Guid] ,
              [Title] ,
              [IconAddress] ,
              [LargeIcon],
              [Presentable] ,
              [IsDeleted],
              [ReferencePageKey] ,
              [ReferenceServiceKey] ,
              [Order],
              [CreateDate],
              [ServiceGroupGuid]
            )
    VALUES  ( @Guid ,
              @Title ,
              @IconAddress ,
              @LargeIcon,
              @Presentable ,
              0,
              @ReferencePageKey ,
              @ReferenceServiceKey ,
              @Order,
              @CreateDate,
              @ServiceGroupGuid
            )


GO
/****** Object:  StoredProcedure [dbo].[Services_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Services_Update]
    @Guid UNIQUEIDENTIFIER ,
    @Title NVARCHAR(50) ,
    @IconAddress NVARCHAR(MAX) ,
    @LargeIcon NVARCHAR(MAX),
    @Presentable BIT ,
    @ReferencePageKey INT ,
    @ReferenceServiceKey INT ,
    @Order INT,
    @ServiceGroupGuid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [Services]
    SET     [Title] = @Title ,
            [IconAddress] = @IconAddress ,
            [LargeIcon] = @LargeIcon,
            [Presentable] = @Presentable ,
            [ReferencePageKey] = @ReferencePageKey ,
            [ReferenceServiceKey] = @ReferenceServiceKey ,
            [Order]=@Order,
            [ServiceGroupGuid] = @ServiceGroupGuid
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Settings_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Settings_Delete]
(
@Guid UNIQUEIDENTIFIER
)
  AS

DELETE FROM
	[Settings]
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Settings_GetSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Settings_GetSetting]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE	@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
SELECT * FROM [dbo].[Settings] WHERE ISNULL([UserGuid],@EmptyGuid) = @UserGuid;



GO
/****** Object:  StoredProcedure [dbo].[Settings_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Settings_Insert]
(
@Guid UNIQUEIDENTIFIER,
@Key INT,
@Value NVARCHAR(MAX)
)
  AS

INSERT INTO [Settings]
	([Guid], [Key], [Value])
VALUES
	(@Guid, @Key, @Value)


GO
/****** Object:  StoredProcedure [dbo].[Settings_InsertSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Settings_InsertSetting]
	@UserGuid UNIQUEIDENTIFIER,
  @Settings [Setting] READONLY
  AS


DECLARE	@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DELETE FROM [dbo].[Settings] WHERE ISNULL([UserGuid],@EmptyGuid) = @UserGuid;

INSERT INTO [dbo].[Settings]
	      ([Guid],
				 [Key],
				 [Value],
				 [UserGuid])
				SELECT 
					NEWID(),
					[Key],
					[Value],
					@UserGuid
				FROM
					@Settings;


GO
/****** Object:  StoredProcedure [dbo].[Settings_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Settings_Update]
(
@Guid UNIQUEIDENTIFIER,
@Key INT,
@Value NVARCHAR(MAX)
)
  AS

UPDATE [Settings] SET
	[Key] = @Key,
	[Value] = @Value
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_Delete]
    @Guid UNIQUEIDENTIFIER
  AS
 
    UPDATE  [SmsFormats]
    SET     [IsDeleted] = 1
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_GenerateSmsFromFormatForNationalCodeParser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_GenerateSmsFromFormatForNationalCodeParser]
	@Number NVARCHAR(16),
	@FormatGuid UNIQUEIDENTIFIER,
	@SmsText NCHAR(10),
	@SmsResult NVARCHAR(MAX) OUTPUT
  AS

BEGIN

DECLARE @Guid UNIQUEIDENTIFIER;
DECLARE @Format NVARCHAR(MAX);
DECLARE	@Prefix NVARCHAR(64);
DECLARE	@Field NVARCHAR(64);
DECLARE	@SmsBody NVARCHAR(MAX) = '';
DECLARE	@UserText NVARCHAR(MAX);
DECLARE	@FirstName NVARCHAR(32);
DECLARE	@LastName NVARCHAR(64);
DECLARE @NationalCode NCHAR(10);
DECLARE	@BirthDate DATETIME;
DECLARE	@Telephone NVARCHAR(16);
DECLARE	@CellPhone NVARCHAR(16);
DECLARE	@FaxNumber NVARCHAR(16);
DECLARE	@Job NVARCHAR(64);
DECLARE	@Address NVARCHAR(MAX);
DECLARE	@Email NVARCHAR(128);
DECLARE	@F1 NVARCHAR(128);
DECLARE	@F2 NVARCHAR(128);
DECLARE	@F3 NVARCHAR(128);
DECLARE	@F4 NVARCHAR(128);
DECLARE	@F5 NVARCHAR(128);
DECLARE	@F6 NVARCHAR(128);
DECLARE	@F7 NVARCHAR(128);
DECLARE	@F8 NVARCHAR(128);
DECLARE	@F9 NVARCHAR(128);
DECLARE	@F10 NVARCHAR(128);
DECLARE	@F11 NVARCHAR(128);
DECLARE	@F12 NVARCHAR(128);
DECLARE	@F13 NVARCHAR(128);
DECLARE	@F14 NVARCHAR(128);
DECLARE	@F15 NVARCHAR(128);
DECLARE	@F16 NVARCHAR(128);
DECLARE	@F17 NVARCHAR(128);
DECLARE	@F18 NVARCHAR(128);
DECLARE	@F19 NVARCHAR(128);
DECLARE	@F20 NVARCHAR(128);
DECLARE	@Sex INT;
DECLARE @GroupGuid UNIQUEIDENTIFIER;

SET @SmsResult = '';

SELECT @Format = [Format],@GroupGuid = [PhoneBookGuid] FROM [dbo].[SmsFormats] WHERE [Guid] = @FormatGuid;

IF(
SELECT COUNT([CellPhone]) FROM [dbo].[PhoneNumbers] 
WHERE
	[IsDeleted] = 0 AND
	[PhoneBookGuid] = @GroupGuid AND
	CONVERT(DATE,[CreateDate]) = CONVERT(DATE,GETDATE()) AND
	([NationalCode] = @SmsText OR	[CellPhone] = @Number)
	) > 0
BEGIN
	SET @SmsResult = N'این سرویس هم اکنون برای شما فعال می باشد';
	RETURN;
END

SELECT
	TOP 1
	@Guid = [Guid],
	@FirstName = [FirstName] ,
	@LastName = [LastName] ,
	@NationalCode = [NationalCode],
	@BirthDate = [BirthDate] ,
	@Telephone = [Telephone] ,
	@CellPhone = [CellPhone],
	@FaxNumber = [FaxNumber] ,
	@Job = [Job] ,
	@Address = [Address] ,
	@Email = [Email] ,
	@F1 = [F1] ,
	@F2 = [F2] ,
	@F3 = [F3] ,
	@F4 = [F4] ,
	@F5 = [F5] ,
	@F6 = [F6] ,
	@F7 = [F7] ,
	@F8 = [F8] ,
	@F9 = [F9] ,
	@F10 = [F10] ,
	@F11 = [F11] ,
	@F12 = [F12] ,
	@F13 = [F13] ,
	@F14 = [F14] ,
	@F15 = [F15] ,
	@F16 = [F16] ,
	@F17 = [F17] ,
	@F18 = [F18] ,
	@F19 = [F19] ,
	@F20 = [F20] ,
	@Sex = [Sex] 
FROM
	[dbo].[PhoneNumbers]
WHERE
	[IsDeleted] = 0 AND
	[PhoneBookGuid] = @GroupGuid AND
	ISNULL([NationalCode],'') = ''

IF(@@ROWCOUNT = 0)
BEGIN
	SET @SmsResult = N'در حال حاضر امکان ارائه سرویس وجود ندارد';
	RETURN;
END

WHILE(LEN(@Format)>0)
BEGIN
	SET @Prefix = SUBSTRING(@Format,1,4)
			
	IF (@Prefix = '<(%$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @Field = SUBSTRING(@Format,1, (CHARINDEX('$%)>',@Format)-1));
		SET @Format = Substring(@Format,LEN(@Field) + 5,LEN(@Format));
				
		IF(CHARINDEX('@$!$@',@Field,0)=0)
		BEGIN
			IF (@Field = 'firstname')
				SET @SmsBody += ISNULL(@FirstName,'') + ' '
			IF (@Field='lastname')
				SET @SmsBody += ISNULL(@LastName,'') + ' '
			IF (@Field='nationalcode')
				SET @SmsBody += @SmsText + ' '
			IF (@Field = 'birthDate')
				SET @SmsBody += ISNULL(dbo.GetSolarDate(@BirthDate),'') + ' ';	
			IF (@Field='telephone')
				SET @SmsBody += ISNULL(@Telephone,'') + ' '
			IF (@Field='cellphone')
				SET @SmsBody += @Number + ' '
			IF (@Field='faxnumber')
				SET @SmsBody += ISNULL(@FaxNumber,'') + ' '
			IF (@Field='job')
				SET @SmsBody += ISNULL(@Job,'') + ' '
			IF (@Field='address')
				SET @SmsBody += ISNULL(@Address,'') + ' '
			IF (@Field='email')
				SET @SmsBody += ISNULL(@email,'') + ' '
			IF(@Field = 'sex')
			BEGIN
				IF (@Sex = 1)
					SET @SmsBody += N'آقای ';
				ELSE IF	(@Sex = 2)
					SET @SmsBody += N'خانم '; 
			END
		END
		ELSE
		BEGIN
			SET @Field= Substring(@Field,1,CHARINDEX('@$!$@',@Field)-1);
			IF (@Field = 'field1')
				SET @SmsBody += ISNULL(@F1,'') + ' '
			IF (@Field = 'field2')
				SET @SmsBody += ISNULL(@F2,'') + ' '
			IF (@Field = 'field3')
				SET @SmsBody += ISNULL(@F3,'') + ' '
			IF (@Field = 'field4')
				SET @SmsBody += ISNULL(@F4,'') + ' '
			IF (@Field = 'field5')
				SET @SmsBody += ISNULL(@F5,'') + ' '
			IF (@Field = 'field6')
				SET @SmsBody += ISNULL(@F6,'') + ' '
			IF (@Field = 'field7')
				SET @SmsBody += ISNULL(@F7,'') + ' '
			IF (@Field = 'field8')
				SET @SmsBody += ISNULL(@F8,'') + ' '
			IF (@Field = 'field9')
				SET @SmsBody += ISNULL(@F9,'') + ' '
			IF (@Field = 'field10')
				SET @SmsBody += ISNULL(@F10,'') + ' '
			IF (@Field = 'field11')
				SET @SmsBody += ISNULL(@F11,'') + ' '
			IF (@Field = 'field12')
				SET @SmsBody += ISNULL(@F12,'') + ' '
			IF (@Field = 'field13')
				SET @SmsBody += ISNULL(@F13,'') + ' '
			IF (@Field = 'field14')
				SET @SmsBody += ISNULL(@F14,'') + ' '
			IF (@Field = 'field15')
				SET @SmsBody += ISNULL(@F15,'') + ' '
			IF (@Field = 'field16')
				SET @SmsBody += ISNULL(@F16,'') + ' '
			IF (@Field = 'field17')
				SET @SmsBody += ISNULL(@F17,'') + ' '
			IF (@Field = 'field18')
				SET @SmsBody += ISNULL(@F18,'') + ' '
			IF (@Field = 'field19')
				SET @SmsBody += ISNULL(@F19,'') + ' '
			IF (@Field = 'field20')
				SET @SmsBody += ISNULL(@F20,'') + ' '
		END
	END
	ELSE IF(@Prefix = '<(*$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @UserText = Substring(@Format,1, (CHARINDEX('$*)>',@Format)-1))
		SET @Format = Substring(@Format,LEN(@UserText)+ 5,LEN(@Format));
				
		SET @SmsBody += @UserText + ' ';
	END
	ELSE IF(@Prefix = '<(!$')
	BEGIN
		SET @Format = SUBSTRING(@Format,5,LEN(@Format));
		SET @Format = Substring(@Format,13,LEN(@Format));
				
		SET @SmsBody += @SmsText + ' ';
  END
END

	EXEC [dbo].[PhoneNumbers_UpdateNationalCodeParser]
				@Guid = @Guid,
				@Mobile = @Number,
				@NationalCode = @SmsText

	SELECT @SmsResult = @SmsBody;
	RETURN;
END


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_GetFormatOfPhoneBook]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsFormats_GetFormatOfPhoneBook]
	@PhoneBookGuid UNIQUEIDENTIFIER
  AS


SELECT
		[Guid],
		[Format],
		[Name] AS [FormatName]
FROM
	[SmsFormats]
WHERE
	[IsDeleted] = 0 AND
	[PhoneBookGuid] = @PhoneBookGuid


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_GetFormatSmsInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_GetFormatSmsInfo]
	@FormatGuid UNIQUEIDENTIFIER
  AS


DECLARE @MessageInfo TABLE
(
ID int IDENTITY (1, 1) Primary key NOT NULL,
Reciever NVARCHAR(50),
Operator INT,
SmsBody NVARCHAR(MAX),
SmsPartCount INT,
Encoding INT
)

INSERT INTO @MessageInfo (Reciever,Operator,SmsBody) SELECT
	[CellPhone],
	[Operator],
	dbo.GenerateSmsFromFormat([Guid],@FormatGuid) AS SmsBody
FROM
	[dbo].[PhoneNumbers]
WHERE
	[IsDeleted] = 0 AND
	[Operator] > 0 AND
	[PhoneBookGuid] = (SELECT [PhoneBookGuid] FROM [dbo].[SmsFormats] WHERE [Guid] = @FormatGuid);

UPDATE @MessageInfo
SET
	[SmsPartCount] = [dbo].[GetSmsCount]([SmsBody]),
	[Encoding] = [dbo].[HasUniCodeCharacter]([SmsBody]);

SELECT 
		COUNT(*) AS [Count],
		[Operator],
		[Encoding],
		[SmsPartCount]
FROM 
		@MessageInfo
GROUP BY
		[Operator],
		[Encoding],
		[SmsPartCount];

				


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_GetFormatsOfUserPhoneBook]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_GetFormatsOfUserPhoneBook]
	@UserGuid UNIQUEIDENTIFIER
  AS


SELECT 
	formt.[Name] AS FormatName,
	formt.[Guid] AS FormatGuid,
	phoneBook.[Name] AS PhoneBookName,
	phoneBook.[Guid] AS PhoneBookGuid
FROM 
	[dbo].[SmsFormats] formt INNER JOIN
	[dbo].[PhoneBooks] phoneBook ON phoneBook.[Guid] = formt.[PhoneBookGuid] 
WHERE 
	formt.[IsDeleted] = 0 AND
	phoneBook.[IsDeleted] = 0 AND
	phoneBook.[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_GetPagedAllSmsFormats]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_GetPagedAllSmsFormats]
	@UserGuid UNIQUEIDENTIFIER,
	@FormatName NVARCHAR(MAX),
	@PhoneBookName NVARCHAR(MAX),
	@PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS

DECLARE @Where NVARCHAR(MAX) = '[SmsFormats].[IsDeleted] = 0 AND [PhoneBooks].[IsDeleted] = 0';

IF ( ISNULL(@FormatName, '') != '' ) 
	BEGIN
    IF ( @Where != '' ) 
			SET @Where += ' AND'
    SET @Where += ' [SmsFormats].[Name] LIKE N''%' + @FormatName + '%'''
  END
  
IF ( ISNULL(@PhoneBookName, '') != '' ) 
	BEGIN
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [PhoneBooks].[Name] LIKE N''%' + @PhoneBookName + '%'''
	END
  
IF ( @Where != '' ) 
	SET @Where += ' AND'
SET @Where += ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
  
IF (@Where != '' ) 
  SET @Where = ' WHERE ' + @Where
-----------------------------------------------------------------------
EXEC('
		SELECT
				COUNT(*) AS [RowCount]
		FROM
				[SmsFormats] INNER JOIN
				[PhoneBooks] ON [PhoneBooks].[Guid] = [SmsFormats].[PhoneBookGuid]' +	@Where + ';
				
		WITH expTemp AS
		(
			SELECT
					Row_Number() OVER (ORDER BY [SmsFormats].' + @SortField + ') AS [RowNumber], 
					[SmsFormats].[Guid],
					[SmsFormats].[Format],
					[SmsFormats].[Name] AS FormatName,
					[PhoneBooks].[Name] AS PhoneBookName,
					[SmsFormats].[CreateDate]
			FROM
				[SmsFormats] INNER JOIN
				[PhoneBooks] ON [PhoneBooks].[Guid] = [SmsFormats].[PhoneBookGuid]' +	@Where + '
		)
		
		SELECT 
				*
		FROM
			expTemp
		WHERE 
			(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
			([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
		ORDER BY
			 [RowNumber] ;')


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_GetRawFormat]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsFormats_GetRawFormat]
	@FormatGuid UNIQUEIDENTIFIER
  AS

 SELECT dbo.GetRawFormat(@FormatGuid) AS SmsBody
	

				


GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_InsertFormat]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_InsertFormat]
	@Guid UNIQUEIDENTIFIER ,
	@Name NVARCHAR(64) ,
	@Format NVARCHAR(MAX) ,
	@CreateDate DATETIME,
	@PhoneBookGuid UNIQUEIDENTIFIER
  AS


INSERT INTO [dbo].[SmsFormats]
					([Guid] ,
					 [Name] ,
					 [Format] ,
					 [IsDeleted],
					 [CreateDate],
					 [PhoneBookGuid])
			 VALUES
					(@Guid ,
					 @Name ,
					 @Format ,
					 0,
					 @CreateDate,
					 @PhoneBookGuid)

GO
/****** Object:  StoredProcedure [dbo].[SmsFormats_UpdateFormat]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsFormats_UpdateFormat]
  @Guid UNIQUEIDENTIFIER ,
  @Name NVARCHAR(100) ,
  @Format NVARCHAR(MAX)
  AS


UPDATE 
	[dbo].[SmsFormats]
SET
	[Name] = @Name ,
  [Format] = @Format
WHERE
	[Guid] = @Guid;

GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[ParserFormulas] WHERE [SmsParserGuid] = @Guid;
DELETE [dbo].[SmsParsers] WHERE [Guid] = @Guid;


GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_GetPagedSmsParsers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsParsers_GetPagedSmsParsers]
	@Type INT,
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256),
	@UserGuid UNIQUEIDENTIFIER
  AS
 
		DECLARE @Where NVARCHAR(MAX) = ' [IsDeleted] = 0 '
		
		IF ( ISNULL(@Type, -1) != -1 ) 
      BEGIN
        IF ( @Where != '' ) 
          SET @Where += ' AND'
        SET @Where += ' [Type] = ' + CAST(@Type AS VARCHAR(1))
      END
			  
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
  
    IF ( @Where != '' ) 
      SET @Where = ' WHERE ' + @Where

--------------------------------------------------
    EXEC('
					SELECT 
								*
								INTO #TempSmsParsers
					FROM 
								[dbo].[SmsParsers]' + @Where + '
    
					SELECT COUNT(*) AS [RowCount] FROM #TempSmsParsers;
				
					WITH expTemp AS
					(
						SELECT
								Row_Number() OVER (ORDER BY [CreateDate] DESC) AS [RowNumber], 
								*
						FROM
								#TempSmsParsers
					)
					SELECT 
							*
					FROM
						expTemp
					WHERE 
						(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
						([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
					ORDER BY
						 [RowNumber] ;
			     
					DROP TABLE #TempSmsParsers')


GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_GetSmsParserKey]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_GetSmsParserKey]
	@ParserGuid UNIQUEIDENTIFIER,
	@NumberGuid UNIQUEIDENTIFIER,
	@Key NVARCHAR(50)
  AS

	IF(@ParserGuid = '00000000-0000-0000-0000-000000000000')
	BEGIN
		SELECT 
			*
		FROM
			[dbo].[SmsParsers] parser INNER JOIN
			[dbo].[ParserFormulas] options ON parser.[Guid] = options.[SmsParserGuid]
		WHERE
			parser.[PrivateNumberGuid] = @NumberGuid AND
			options.[Key] = @Key
	END
	ELSE
	BEGIN
		SELECT 
			*
		FROM
			[dbo].[SmsParsers] parser INNER JOIN
			[dbo].[ParserFormulas] options ON parser.[Guid] = options.[SmsParserGuid]
		WHERE
			parser.[PrivateNumberGuid] = @NumberGuid AND
			options.[Key] = @Key AND
			parser.[Guid] != @ParserGuid
	END
		

GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_InsertCompetition]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsParsers_InsertCompetition]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@Type INT,
	@CreateDate DATETIME,
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@Scope UNIQUEIDENTIFIER,
	@ReplyPrivateNumberGuid UNIQUEIDENTIFIER,
	@ReplySmsText NVARCHAR(MAX),
	@DuplicatePrivateNumberGuid UNIQUEIDENTIFIER,
	@DuplicateUserSmsText NVARCHAR(MAX),
	@UserGuid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Options SmsParserOptions READONLY
  AS


BEGIN TRANSACTION

BEGIN TRY
	INSERT INTO [SmsParsers]
							 ([Guid],
							  [Title],
							  [Type],
							  [CreateDate],
							  [FromDateTime],
							  [ToDateTime],
							  [Scope],
							  [ReplyPrivateNumberGuid],
							  [ReplySmsText],
								[DuplicatePrivateNumberGuid],
							  [DuplicateUserSmsText],
							  [IsActive],
							  [IsDeleted],
							  [UserGuid],
							  [PrivateNumberGuid])
				 VALUES
							 (@Guid,
								@Title,
								@Type,
							  @CreateDate,
							  @FromDateTime,
							  @ToDateTime ,
								@Scope,
								@ReplyPrivateNumberGuid,
								@ReplySmsText,
								@DuplicatePrivateNumberGuid,
								@DuplicateUserSmsText,
								1,
							  0,
								@UserGuid,
							  @PrivateNumberGuid);

	INSERT INTO [dbo].[ParserFormulas]
								( [Guid] ,
									[Title] ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention] ,
									[SmsParserGuid])
					SELECT  NEWID() ,
									Title ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention],
									@Guid
					FROM
									@Options;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
END	CATCH
GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_InsertFilter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_InsertFilter]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@Type INT,
	@CreateDate DATETIME,
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@TypeConditionSender INT,
	@ConditionSender NVARCHAR(50),
	@Scope NVARCHAR(MAX),
	@UserGuid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER
  AS


	INSERT INTO [SmsParsers]
							 ([Guid]
							 ,[Title]
							 ,[Type]
							 ,[CreateDate]
							 ,[FromDateTime]
							 ,[ToDateTime]
							 ,[TypeConditionSender]
							 ,[ConditionSender]
							 ,[Scope]
							 ,[IsActive]
							 ,[IsDeleted]
							 ,[UserGuid]
							 ,[PrivateNumberGuid])
				 VALUES
							 (@Guid,
								@Title,
								@Type,
							  @CreateDate,
							  @FromDateTime,
							  @ToDateTime ,
								@TypeConditionSender,
								@ConditionSender,
								@Scope,
							  1,
							  0,
								@UserGuid,
							  @PrivateNumberGuid);
GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_InsertPoll]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_InsertPoll]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@Type INT,
	@CreateDate DATETIME,
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@Scope NVARCHAR(MAX),
	@ReplyPrivateNumberGuid UNIQUEIDENTIFIER,
	@ReplySmsText NVARCHAR(MAX),
	@DuplicatePrivateNumberGuid UNIQUEIDENTIFIER,
	@DuplicateUserSmsText NVARCHAR(MAX),
	@UserGuid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Options SmsParserOptions READONLY
  AS


BEGIN TRANSACTION

BEGIN TRY
	INSERT INTO [SmsParsers]
							 ([Guid],
							  [Title],
							  [Type],
							  [CreateDate],
							  [FromDateTime],
							  [ToDateTime],
							  [Scope],
							  [ReplyPrivateNumberGuid],
							  [ReplySmsText],
								[DuplicatePrivateNumberGuid],
							  [DuplicateUserSmsText],
							  [IsActive],
							  [IsDeleted],
							  [UserGuid],
							  [PrivateNumberGuid])
				 VALUES
							 (@Guid,
								@Title,
								@Type,
							  @CreateDate,
							  @FromDateTime,
							  @ToDateTime ,
								@Scope,
								@ReplyPrivateNumberGuid,
								@ReplySmsText,
								@DuplicatePrivateNumberGuid,
								@DuplicateUserSmsText,
							  1,
							  0,
								@UserGuid,
							  @PrivateNumberGuid);

	INSERT INTO [dbo].[ParserFormulas]
								( [Guid] ,
									[Title] ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention] ,
									[SmsParserGuid])
					SELECT  NEWID() ,
									Title ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention],
									@Guid
					FROM
									@Options;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
END	CATCH
	


GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_ParseCompetition]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsParsers_ParseCompetition]
	@ReceivedSmsGuid UNIQUEIDENTIFIER,
	@FormulaGuid UNIQUEIDENTIFIER
  AS

	DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER,
					@SmsText NVARCHAR(MAX),
					@SenderNumber NVARCHAR(25),
					@UserGuid UNIQUEIDENTIFIER,
					@ParserGuid UNIQUEIDENTIFIER,
					@Scope UNIQUEIDENTIFIER,
					@ReplyPrivateNumberGuid UNIQUEIDENTIFIER,
					@ReplySmsText NVARCHAR(MAX),
					@DuplicatePrivateNumberGuid UNIQUEIDENTIFIER,
					@DuplicateUserSmsText NVARCHAR(MAX),
					@NewGuid UNIQUEIDENTIFIER = NEWID(),
					@SmsCount INT,
					@IsUnicode BIT,
					@CurrentDate DATETIME = GETDATE(),
					@key NVARCHAR(512),
					@ReactionExtention XML,
					@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

	SELECT
		@PrivateNumberGuid = [PrivateNumberGuid],
		@SmsText = [SmsText],
		@SenderNumber = [Sender],
		@UserGuid = [UserGuid]
	FROM [dbo].[Inboxes] WHERE [Guid] = @ReceivedSmsGuid;

	SELECT
		@Scope = [Scope],
		@ReplyPrivateNumberGuid = [ReplyPrivateNumberGuid],
		@ReplySmsText = [ReplySmsText],
		@DuplicatePrivateNumberGuid = [DuplicatePrivateNumberGuid],
		@DuplicateUserSmsText = [DuplicateUserSmsText],
		@Key = [Key],
		@ReactionExtention = [ReactionExtention]
	FROM
		[dbo].[SmsParsers] parser INNER JOIN
		[dbo].[ParserFormulas] options ON parser.[Guid] = options.[SmsParserGuid]
	WHERE
		options.[Guid] = @FormulaGuid;

	IF(@Scope != @EmptyGuid)
	BEGIN
		SELECT [CellPhone] FROM [dbo].[PhoneNumbers] WHERE [IsDeleted] = 0 AND [PhoneBookGuid] = @Scope AND	[CellPhone] = @SenderNumber;
		IF(@@ROWCOUNT = 0)
			RETURN 0;
  END

	IF((SELECT COUNT(*) FROM [dbo].[Inboxes] WHERE [IsDeleted] = 0 AND ISNULL([ParserFormulaGuid],@EmptyGuid) = @FormulaGuid AND [Sender] = @SenderNumber) > 0)
	BEGIN
		IF(ISNULL(@DuplicateUserSmsText,'') != '' AND @DuplicatePrivateNumberGuid != @EmptyGuid)
		BEGIN
			SET @SmsCount = [dbo].[GetSmsCount](@DuplicateUserSmsText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@DuplicateUserSmsText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertSms]
					@Guid = @NewGuid,
					@PrivateNumberGuid = @DuplicatePrivateNumberGuid,
					@Reciever = @SenderNumber,
					@SmsText = @DuplicateUserSmsText,
					@SmsLen = @SmsCount,
					@PresentType = 1,--normal
					@Encoding = @IsUnicode,
					@TypeSend = 1, -- SendSms,
					@Status = 1,
					@DateTimeFuture = @CurrentDate,
					@UserGuid = @UserGuid,
					@IPAddress = '',
					@Browser = ''
		END
		RETURN 1;
	END

	DECLARE @ReferenceGuid UNIQUEIDENTIFIER;

	IF(@key != @SmsText)
		RETURN 0;

	SELECT  
			@ReferenceGuid = Tbl.Col.value('ReferenceGuid[1]', 'UNIQUEIDENTIFIER')
	FROM
			@reactionExtention.nodes('//ParserFormulaSerialization') Tbl(Col)

	IF(@ReferenceGuid != @EmptyGuid)
		INSERT INTO dbo.PhoneNumbers
						([Guid] ,
							[CreateDate] ,
							[CellPhone] ,
							[Operator] ,
							[IsDeleted] ,
							[PhoneBookGuid])
					VALUES
						(NEWID(),
							GETDATE(),
							@SenderNumber,
							[dbo].[GetNumberOperator](@SenderNumber),
							0,
							@ReferenceGuid)

	IF(ISNULL(@ReplySmsText,'')!='' AND @ReplyPrivateNumberGuid != @EmptyGuid)
	BEGIN
		SET @SmsCount = [dbo].[GetSmsCount](@ReplySmsText);
		SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReplySmsText);

		IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

		EXEC [dbo].[ScheduledSmses_InsertSms]
				@Guid = @NewGuid,
				@PrivateNumberGuid = @ReplyPrivateNumberGuid,
				@Reciever = @SenderNumber,
				@SmsText = @ReplySmsText,
				@SmsLen = @SmsCount,
				@PresentType = 1,--normal
				@Encoding = @IsUnicode,
				@TypeSend = 1, -- SendSms
				@Status = 1,
				@DateTimeFuture = @CurrentDate,
				@UserGuid = @UserGuid,
				@IPAddress = '',
				@Browser = ''
				
	END

	UPDATE [dbo].[Inboxes] SET [ParserFormulaGuid] = @FormulaGuid WHERE [Guid] = @ReceivedSmsGuid;

	RETURN 1;



GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_ParseFilter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SmsParsers_ParseFilter]
	@ReceivedSmsGuid UNIQUEIDENTIFIER,
	@FormulaGuid UNIQUEIDENTIFIER
  AS
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE	@SmsText NVARCHAR(MAX);
DECLARE	@SenderNumber NVARCHAR(25);
DECLARE	@UserGuid UNIQUEIDENTIFIER;
DECLARE	@ParserGuid UNIQUEIDENTIFIER;
DECLARE	@Scope UNIQUEIDENTIFIER;
DECLARE	@Condition INT;
DECLARE	@TypeConditionSender INT;
DECLARE	@ConditionSender NVARCHAR(50);
DECLARE	@NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE	@SmsCount INT;
DECLARE	@IsUnicode INT;
DECLARE	@CurrentDate DATETIME = GETDATE();
DECLARE	@key NVARCHAR(64);
DECLARE	@ReactionExtention XML;
DECLARE	@ReferenceGuid UNIQUEIDENTIFIER;
DECLARE	@ReactionExtentionText NVARCHAR(MAX);
DECLARE	@ReactionExtentionCondition INT;
DECLARE	@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE	@Receiver NVARCHAR(255);
DECLARE	@PrivateNumberForReply UNIQUEIDENTIFIER;
DECLARE	@VasURL NVARCHAR(512) = '';
DECLARE @Accepted BIT = 0;
DECLARE @AcceptFormatGuid UNIQUEIDENTIFIER;
DECLARE	@RejectFormatGuid UNIQUEIDENTIFIER;
DECLARE @ResponseSms NVARCHAR(512);
DECLARE @RowGuid UNIQUEIDENTIFIER;

SELECT
	@PrivateNumberGuid = [PrivateNumberGuid],
	@SmsText = [SmsText],
	@SenderNumber = [Sender],
	@UserGuid = [UserGuid],
	@Receiver = [Receiver]
FROM [dbo].[Inboxes] WITH (NOLOCK) WHERE [Guid] = @ReceivedSmsGuid;

SELECT
	@Scope = [Scope],
	@TypeConditionSender = [TypeConditionSender],
	@ConditionSender = [ConditionSender],
	@Key = [Key],
	@ReactionExtention = [ReactionExtention],
	@Condition = [Condition]
FROM
	[dbo].[SmsParsers] parser WITH (NOLOCK) INNER JOIN
	[dbo].[ParserFormulas] options WITH (NOLOCK) ON parser.[Guid] = options.[SmsParserGuid]
WHERE
	options.[Guid] = @FormulaGuid;

	IF(@Scope != @EmptyGuid)
	BEGIN
		IF((SELECT	COUNT(1) FROM [dbo].[PhoneNumbers] WITH (NOLOCK) WHERE [PhoneBookGuid] = @Scope AND	[IsDeleted] = 0 AND	[CellPhone] = @SenderNumber) = 0)
			RETURN 0;
  END

	IF(ISNULL(@TypeConditionSender,0) != 0 AND ISNULL(@ConditionSender,'') != '')
	BEGIN
		IF(@TypeConditionSender = 1 AND @SenderNumber != @ConditionSender)--Equal
			RETURN 0;
		IF(@TypeConditionSender = 2 AND @SenderNumber NOT LIKE '%'+ @ConditionSender +'%')--Include
			RETURN 0;
		IF(@TypeConditionSender = 3 AND @SenderNumber NOT LIKE @ConditionSender +'%')--StartsWith
			RETURN 0;
		IF(@TypeConditionSender = 4 AND @SenderNumber NOT LIKE '%'+ @ConditionSender)--EndsWith
			RETURN 0;
		IF(@TypeConditionSender = 5 AND @SenderNumber <= @ConditionSender)--GreaterThan
			RETURN 0;
		IF(@TypeConditionSender = 6 AND @SenderNumber >= @ConditionSender)--Smaller
			RETURN 0;
		IF(@TypeConditionSender = 7 AND @SenderNumber = @ConditionSender)--UnEqual
			RETURN 0;

	END

	SELECT  
			@ReferenceGuid = Tbl.Col.value('ReferenceGuid[1]', 'UNIQUEIDENTIFIER'),
			@ReactionExtentionText = Tbl.Col.value('Text[1]', 'NVARCHAR(MAX)'),
			@ReactionExtentionCondition = Tbl.Col.value('Condition[1]', 'INT'),
			@PrivateNumberForReply = Tbl.Col.value('Sender[1]', 'UNIQUEIDENTIFIER'),
			@VasURL = Tbl.Col.value('VasURL[1]', 'NVARCHAR(512)'),
			@AcceptFormatGuid = Tbl.Col.value('AcceptFormatGuid[1]', 'UNIQUEIDENTIFIER'),
			@RejectFormatGuid = Tbl.Col.value('RejectFormatGuid[1]', 'UNIQUEIDENTIFIER')
	FROM
			@ReactionExtention.nodes('//ParserFormulaSerialization') Tbl(Col)

	IF(@@ROWCOUNT = 0)
		RETURN 0;

	IF(@Condition = 1 AND @key = @SmsText) --Equal
		SET @Accepted = 1;
	IF(@Condition = 2 AND @SmsText LIKE @Key + '%') --StartsWith
		SET @Accepted = 1;
	IF(@Condition = 4 AND @SmsText LIKE '%' + @Key) --EndsWith
		SET @Accepted = 1;
	IF(@Condition = 6 AND @SmsText LIKE '%'+ @Key +'%')--Include
		SET @Accepted = 1;
	IF(@Condition = 8)--Everything
		SET @Accepted = 1;
	IF(@Condition = 9 AND @SmsText > @Key)--GreaterThan
		SET @Accepted = 1;
	IF(@Condition = 10 AND @SmsText < @Key)--Smaller
		SET @Accepted = 1;
	IF(@Condition = 11 AND [dbo].[IsValidNationalCode](@SmsText) = 1)--NationalCode
		SET @Accepted = 1;
	IF(@Condition = 12)--EqualWithPhoneBookField
	BEGIN
		DECLARE @Len INT = LEN(@key);
		DECLARE @GroupGuid UNIQUEIDENTIFIER = SUBSTRING(@Key,0,37);
		DECLARE @FieldName NVARCHAR(32) = SUBSTRING(@Key,38,@Len);

		DECLARE @Result TABLE ([Value] NVARCHAR(128));
		INSERT INTO @Result ([Value])
		EXEC ('SELECT [Guid] FROM [PhoneNumbers] WHERE [IsDeleted] = 0 AND [PhoneBookGuid] = '''+ @GroupGuid +''' AND '+ @FieldName + '=N''' + @SmsText+'''');
		IF(SELECT COUNT(*) FROM @Result) > 0
		BEGIN
			SET @Accepted = 1;
			SELECT @RowGuid = [Value] FROM @Result;
		END
  END
	

	IF(@Accepted = 0)
	BEGIN
		IF(@ReactionExtentionCondition = 9)--SendSmsFromFormat
		BEGIN
			IF(@Condition = 11)--NationalCode
			BEGIN
				SET @ResponseSms = '';
				EXEC [dbo].[SmsFormats_GenerateSmsFromFormatForNationalCodeParser]
							@Number = @SenderNumber,
							@FormatGuid = @RejectFormatGuid,
							@SmsText = @SmsText,
							@SmsResult = @ResponseSms OUTPUT

				SET @SmsCount = [dbo].[GetSmsCount](@ResponseSms);
				SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ResponseSms);

				IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

				EXEC [dbo].[ScheduledSmses_InsertSms]
							@Guid = @NewGuid,
							@PrivateNumberGuid = @PrivateNumberForReply,
							@Reciever = @SenderNumber,
							@SmsText = @ResponseSms,
							@SmsLen = @SmsCount,
							@PresentType = 1,--normal
							@Status = 1,
							@Encoding = @IsUnicode,
							@TypeSend = 1, -- SendSms
							@DateTimeFuture = @CurrentDate,
							@UserGuid = @UserGuid,
							@IPAddress = '',
							@Browser = '';

				RETURN 0;
      END
			IF(@Condition = 12)--EqualWithPhoneBookField
			BEGIN
				SET @ResponseSms = '';
				SET @ResponseSms = [dbo].[GenerateSmsFromFormatForParser](@SenderNumber,@RejectFormatGuid,@SmsText,@EmptyGuid)
				SET @SmsCount = [dbo].[GetSmsCount](@ResponseSms);
				SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ResponseSms);

				IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

				EXEC [dbo].[ScheduledSmses_InsertSms]
							@Guid = @NewGuid,
							@PrivateNumberGuid = @PrivateNumberForReply,
							@Reciever = @SenderNumber,
							@SmsText = @ResponseSms,
							@SmsLen = @SmsCount,
							@PresentType = 1,--normal
							@Status = 1,
							@Encoding = @IsUnicode,
							@TypeSend = 1, -- SendSms
							@DateTimeFuture = @CurrentDate,
							@UserGuid = @UserGuid,
							@IPAddress = '',
							@Browser = '';

				RETURN 0;
      END
		END
		ELSE
    RETURN 0;			
  END

	IF(@ReactionExtentionCondition = 1 AND @ReferenceGuid != @EmptyGuid)--AddToGroup
	BEGIN
		IF(SELECT COUNT([CellPhone]) FROM [dbo].[PhoneNumbers] WHERE [IsDeleted] = 0 AND [CellPhone] = @SenderNumber AND PhoneBookGuid = @ReferenceGuid) = 0
		BEGIN
			INSERT INTO dbo.PhoneNumbers
							(Guid ,
								CreateDate ,
								CellPhone ,
								Operator ,
								IsDeleted ,
								PhoneBookGuid)
							VALUES
							(NEWID(),
								GETDATE(),
								@SenderNumber,
								[dbo].[GetNumberOperator](@SenderNumber),
								0,
								@ReferenceGuid);
			
			IF(ISNULL(@ReactionExtentionText,'') != '' AND @PrivateNumberForReply != @EmptyGuid)
			BEGIN
				SET @SmsCount = [dbo].[GetSmsCount](@ReactionExtentionText);
				SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReactionExtentionText);

				IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

				EXEC [dbo].[ScheduledSmses_InsertSms]
							@Guid = @NewGuid,
							@PrivateNumberGuid = @PrivateNumberForReply,
							@Reciever = @SenderNumber,
							@SmsText = @ReactionExtentionText,
							@SmsLen = @SmsCount,
							@PresentType = 1,--normal
							@Status = 1,
							@Encoding = @IsUnicode,
							@TypeSend = 1, -- SendSms
							@DateTimeFuture = @CurrentDate,
							@UserGuid = @UserGuid,
							@IPAddress = '',
							@Browser = '';

				IF(@VasURL != '')
				BEGIN
					SET @VasURL = replace(@VasURL, '$num','0'+SUBSTRING(@SenderNumber,3,LEN(@SenderNumber)));
					SET @VasURL = replace(@VasURL, '$dt', REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(19), CONVERT(DATETIME, getdate(), 112), 126), '-', ''), 'T', ''), ':', ''));
					SET @VasURL = replace(@VasURL,'$SubUnsubMessage',@SmsText);
					EXEC [dbo].[SendRequestToUrl] @Url = @VasURL;
				END
			END
		END
		ELSE
		BEGIN
			SET @ReactionExtentionText = N'کاربر گرامی، شما هم اکنون عضو این سرویس می باشید';
			SET @SmsCount = [dbo].[GetSmsCount](@ReactionExtentionText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReactionExtentionText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertSms]
						@Guid = @NewGuid,
						@PrivateNumberGuid = @PrivateNumberForReply,
						@Reciever = @SenderNumber,
						@SmsText = @ReactionExtentionText,
						@SmsLen = @SmsCount,
						@PresentType = 1,--normal
						@Status = 1,
						@Encoding = @IsUnicode,
						@TypeSend = 1, -- SendSms
						@DateTimeFuture = @CurrentDate,
						@UserGuid = @UserGuid,
						@IPAddress = '',
						@Browser = '';
				
			RETURN 1;
		END
	END

	IF(@ReactionExtentionCondition = 2 AND @ReferenceGuid != @EmptyGuid) --RemoveFromGroup
	BEGIN
		IF(SELECT COUNT([CellPhone]) FROM [dbo].[PhoneNumbers] WHERE [IsDeleted] = 0 AND [CellPhone] = @SenderNumber AND PhoneBookGuid = @ReferenceGuid) = 1
		BEGIN
			UPDATE [dbo].[PhoneNumbers] SET [IsDeleted] = 1 WHERE [PhoneBookGuid] = @ReferenceGuid AND [CellPhone] = @SenderNumber;

			IF(ISNULL(@ReactionExtentionText,'') != '' AND @PrivateNumberForReply != @EmptyGuid)
			BEGIN
				SET @SmsCount = [dbo].[GetSmsCount](@ReactionExtentionText);
				SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReactionExtentionText);

				IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

				EXEC [dbo].[ScheduledSmses_InsertSms]
							@Guid = @NewGuid,
							@PrivateNumberGuid = @PrivateNumberForReply,
							@Reciever = @SenderNumber,
							@SmsText = @ReactionExtentionText,
							@SmsLen = @SmsCount,
							@PresentType = 1,--normal
							@Status = 1,
							@Encoding = @IsUnicode,
							@TypeSend = 1, -- SendSms
							@DateTimeFuture = @CurrentDate,
							@UserGuid = @UserGuid,
							@IPAddress = '',
							@Browser = '';

				IF(@VasURL != '')
				BEGIN
					SET @VasURL = replace(@VasURL, '$num', @SenderNumber);
					SET @VasURL = replace(@VasURL, '$dt', REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(19), CONVERT(DATETIME, getdate(), 112), 126), '-', ''), 'T', ''), ':', ''));
					SET @VasURL = replace(@VasURL,'$SubUnsubMessage',@SmsText);
					EXEC [dbo].[SendRequestToUrl] @Url = @VasURL;
				END
			END
		END
	END

	IF(@ReactionExtentionCondition = 3 AND ISNULL(@ReactionExtentionText,'')!='') --TransferToMobile
	BEGIN
		IF(ISNULL(@SmsText,'') != '' AND @PrivateNumberForReply != @EmptyGuid)
		BEGIN
			SET @SmsCount = [dbo].[GetSmsCount](@SmsText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@SmsText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertSms]
						@Guid = @NewGuid,
						@PrivateNumberGuid = @PrivateNumberForReply,
						@Reciever = @ReactionExtentionText,
						@SmsText = @SmsText,
						@SmsLen = @SmsCount,
						@PresentType = 1,--normal
						@Status = 1,
						@Encoding = @IsUnicode,
						@TypeSend = 1, -- SendSms
						@DateTimeFuture = @CurrentDate,
						@UserGuid = @UserGuid,
						@IPAddress = '',
						@Browser = '';
		END
	END

	IF(@ReactionExtentionCondition = 4 AND ISNULL(@ReactionExtentionText,'')!='') --TransferToUrl
	BEGIN
		DECLARE @Url NVARCHAR(MAX),@Response NVARCHAR(512),@TryCount INT;
		SELECT @Url = [Url] FROM [dbo].[TrafficRelays] WHERE [Guid] = @ReferenceGuid;

		SET @Url = REPLACE(@Url,'$To',@Receiver);
		SET @Url = REPLACE(@Url,'$From',@SenderNumber);
		SET @Url = REPLACE(@Url,'$Text',@SmsText);

		SET @Response = [dbo].[SendRequestToUrl](@Url);

		UPDATE [dbo].[Inboxes] SET [SendSmsToUrlCount] = 1,[ResponseOfRelay] = @Response WHERE [Guid] = @ReceivedSmsGuid;
	END

	IF(@ReactionExtentionCondition = 5 AND ISNULL(@ReactionExtentionText,'')!='') --TransferToEmail
	BEGIN
		PRINT 'Not Implement';
	END

	IF(@ReactionExtentionCondition = 6 AND ISNULL(@ReactionExtentionText,'')!='') --SendSmsToSender
	BEGIN
		IF(ISNULL(@ReactionExtentionText,'') != '' AND @PrivateNumberForReply != @EmptyGuid)
		BEGIN
			SET @SmsCount = [dbo].[GetSmsCount](@ReactionExtentionText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReactionExtentionText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertSms]
						@Guid = @NewGuid,
						@PrivateNumberGuid = @PrivateNumberForReply,
						@Reciever = @SenderNumber,
						@SmsText = @ReactionExtentionText,
						@SmsLen = @SmsCount,
						@PresentType = 1,--normal
						@Status = 1,
						@Encoding = @IsUnicode,
						@TypeSend = 1, -- SendSms
						@DateTimeFuture = @CurrentDate,
						@UserGuid = @UserGuid,
						@IPAddress = '',
						@Browser = '';
		END
	END

	IF(@ReactionExtentionCondition = 7 AND @ReferenceGuid != @EmptyGuid) --SendSmsToGroup
	BEGIN
		IF(ISNULL(@ReactionExtentionText,'') != '' AND @PrivateNumberForReply != @EmptyGuid)
		BEGIN
			SET @SmsCount = [dbo].[GetSmsCount](@ReactionExtentionText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReactionExtentionText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertGroupSms]
						@Guid = @NewGuid,
						@PrivateNumberGuid = @PrivateNumberForReply,
						@ReferenceGuid = @ReferenceGuid,
						@SmsText = @ReactionExtentionText,
						@SmsLen = @SmsCount,
						@PresentType = 1,--normal
						@Status = 1,
						@Encoding = @IsUnicode,
						@TypeSend = 2, -- SendGroupSms
						@DateTimeFuture = @CurrentDate,
						@UserGuid = @UserGuid,
						@IPAddress = N'',
						@Browser = N'';
		END
	END

	IF(@ReactionExtentionCondition = 8 AND @ReferenceGuid != @EmptyGuid) --ForwardSmsToGroup
	BEGIN
		IF(ISNULL(@SmsText,'') != '' AND @PrivateNumberForReply != @EmptyGuid)
		BEGIN
			SET @SmsCount = [dbo].[GetSmsCount](@SmsText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@SmsText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertGroupSms]
						@Guid = @NewGuid,
						@PrivateNumberGuid = @PrivateNumberForReply,
						@ReferenceGuid = @ReferenceGuid,
						@SmsText = @SmsText,
						@SmsLen = @SmsCount,
						@PresentType = 1,--normal
						@Status = 1,
						@Encoding = @IsUnicode,
						@TypeSend = 2, -- SendGroupSms
						@DateTimeFuture = @CurrentDate,
						@UserGuid = @UserGuid,
						@IPAddress = N'',
						@Browser = N'';
		END
	END
    
	IF(@ReactionExtentionCondition = 9)--SendSmsFromFormat
	BEGIN
			IF(@Condition = 11)--NationalCode
			BEGIN
				SET @ResponseSms = '';
				EXEC [dbo].[SmsFormats_GenerateSmsFromFormatForNationalCodeParser]
							@Number = @SenderNumber,
							@FormatGuid = @AcceptFormatGuid,
							@SmsText = @SmsText,
							@SmsResult = @ResponseSms OUTPUT

				SET @SmsCount = [dbo].[GetSmsCount](@ResponseSms);
				SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ResponseSms);

				IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

				EXEC [dbo].[ScheduledSmses_InsertSms]
							@Guid = @NewGuid,
							@PrivateNumberGuid = @PrivateNumberForReply,
							@Reciever = @SenderNumber,
							@SmsText = @ResponseSms,
							@SmsLen = @SmsCount,
							@PresentType = 1,--normal
							@Status = 1,
							@Encoding = @IsUnicode,
							@TypeSend = 1, -- SendSms
							@DateTimeFuture = @CurrentDate,
							@UserGuid = @UserGuid,
							@IPAddress = '',
							@Browser = '';
      END
			ELSE IF(@Condition = 12)--EqualWithPhoneBookField
			BEGIN
				SET @ResponseSms = '';
				SET @ResponseSms = [dbo].[GenerateSmsFromFormatForParser](@SenderNumber,@AcceptFormatGuid,@SmsText,@RowGuid)
				SET @SmsCount = [dbo].[GetSmsCount](@ResponseSms);
				SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ResponseSms);

				IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

				EXEC [dbo].[ScheduledSmses_InsertSms]
							@Guid = @NewGuid,
							@PrivateNumberGuid = @PrivateNumberForReply,
							@Reciever = @SenderNumber,
							@SmsText = @ResponseSms,
							@SmsLen = @SmsCount,
							@PresentType = 1,--normal
							@Status = 1,
							@Encoding = @IsUnicode,
							@TypeSend = 1, -- SendSms
							@DateTimeFuture = @CurrentDate,
							@UserGuid = @UserGuid,
							@IPAddress = '',
							@Browser = '';
      END
	END

	UPDATE [dbo].[Inboxes] SET [ParserFormulaGuid] = @FormulaGuid WHERE [Guid] = @ReceivedSmsGuid;

	RETURN 1;
GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_ParsePoll]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_ParsePoll]
	@ReceivedSmsGuid UNIQUEIDENTIFIER,
	@FormulaGuid UNIQUEIDENTIFIER
  AS


DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER,
				@SmsText NVARCHAR(MAX),
				@SenderNumber NVARCHAR(25),
				@UserGuid UNIQUEIDENTIFIER,
				@ParserGuid UNIQUEIDENTIFIER,
				@Scope UNIQUEIDENTIFIER,
				@ReplyPrivateNumberGuid UNIQUEIDENTIFIER,
				@ReplySmsText NVARCHAR(MAX),
				@DuplicatePrivateNumberGuid UNIQUEIDENTIFIER,
				@DuplicateUserSmsText NVARCHAR(MAX),
				@NewGuid UNIQUEIDENTIFIER = NEWID(),
				@SmsCount INT,
				@IsUnicode BIT,
				@CurrentDate DATETIME = GETDATE(),
				@key NVARCHAR(512),
				@ReactionExtention XML,
				@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

	SELECT
		@PrivateNumberGuid = [PrivateNumberGuid],
		@SmsText = [SmsText],
		@SenderNumber = [Sender],
		@UserGuid = [UserGuid]
	FROM [dbo].[Inboxes] WHERE [Guid] = @ReceivedSmsGuid;

	SELECT
		@Scope = [Scope],
		@ReplyPrivateNumberGuid = [ReplyPrivateNumberGuid],
		@ReplySmsText = [ReplySmsText],
		@DuplicatePrivateNumberGuid = [DuplicatePrivateNumberGuid],
		@DuplicateUserSmsText = [DuplicateUserSmsText],
		@Key = [Key],
		@ReactionExtention = [ReactionExtention]
	FROM
		[dbo].[SmsParsers] parser INNER JOIN
		[dbo].[ParserFormulas] options ON parser.[Guid] = options.[SmsParserGuid]
	WHERE
		options.[Guid] = @FormulaGuid;

	IF(@Scope != @EmptyGuid)
	BEGIN
		IF((SELECT	COUNT(*) FROM [dbo].[PhoneNumbers] WITH (NOLOCK) WHERE [PhoneBookGuid] = @Scope AND	[IsDeleted] = 0 AND	[CellPhone] = @SenderNumber) = 0)
			RETURN 0;
  END

	IF((SELECT	COUNT(*) FROM [dbo].[Inboxes] WHERE [IsDeleted] = 0 AND [ParserFormulaGuid] = @FormulaGuid AND [Sender] = @SenderNumber) > 0)
	BEGIN
		IF(ISNULL(@DuplicateUserSmsText,'') != '' AND @DuplicatePrivateNumberGuid != @EmptyGuid)
		BEGIN
			SET @SmsCount = [dbo].[GetSmsCount](@DuplicateUserSmsText);
			SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@DuplicateUserSmsText);

			IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

			EXEC [dbo].[ScheduledSmses_InsertSms]
					@Guid = @NewGuid,
					@PrivateNumberGuid = @DuplicatePrivateNumberGuid,
					@Reciever = @SenderNumber,
					@SmsText = @DuplicateUserSmsText,
					@SmsLen = @SmsCount,
					@PresentType = 1,--normal
					@Encoding = @IsUnicode,
					@TypeSend = 1, -- SendSms
					@Status = 1,
					@DateTimeFuture = @CurrentDate,
					@UserGuid = @UserGuid,
					@IPAddress = '',
					@Browser = ''
		END
		RETURN 1;
	END

	DECLARE @ReferenceGuid UNIQUEIDENTIFIER;

	IF(@key != @SmsText)
		RETURN 0;

	SELECT  
			@ReferenceGuid = Tbl.Col.value('ReferenceGuid[1]', 'UNIQUEIDENTIFIER')
	FROM
			@reactionExtention.nodes('//ParserFormulaSerialization') Tbl(Col)

	IF(@ReferenceGuid != @EmptyGuid)
	BEGIN
		INSERT INTO dbo.PhoneNumbers
						([Guid] ,
							[CreateDate] ,
							[CellPhone] ,
							[Operator] ,
							[IsDeleted] ,
							[PhoneBookGuid])
						VALUES
						(NEWID(),
							GETDATE(),
							@SenderNumber,
							[dbo].[GetNumberOperator](@SenderNumber),
							0,
							@ReferenceGuid)
	END
	IF(ISNULL(@ReplySmsText,'')!='' AND @ReplyPrivateNumberGuid != @EmptyGuid)
	BEGIN
		SET @SmsCount = [dbo].[GetSmsCount](@ReplySmsText);
		SET	@IsUnicode = [dbo].[HasUniCodeCharacter](@ReplySmsText);

		IF(@IsUnicode=1) SET @IsUnicode = 2 ELSE SET @IsUnicode = 1;

		EXEC [dbo].[ScheduledSmses_InsertSms]
				@Guid = @NewGuid,
				@PrivateNumberGuid = @ReplyPrivateNumberGuid,
				@Reciever = @SenderNumber,
				@SmsText = @ReplySmsText,
				@SmsLen = @SmsCount,
				@PresentType = 1,--normal
				@Encoding = @IsUnicode,
				@TypeSend = 1, -- SendSms
				@Status = 1,
				@DateTimeFuture = @CurrentDate,
				@UserGuid = @UserGuid,
				@IPAddress = '',
				@Browser = ''
				
	END

	UPDATE [dbo].[Inboxes] SET [ParserFormulaGuid] = @FormulaGuid WHERE [Guid] = @ReceivedSmsGuid;

	RETURN 1;



GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_ParseReceiveSms]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_ParseReceiveSms]
	@SmsGuid UNIQUEIDENTIFIER,
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@SmsText NVARCHAR(MAX),
	@Mobile NVARCHAR(16)
  AS


DECLARE	@UserGuid UNIQUEIDENTIFIER;
DECLARE	@ParserType INT;
DECLARE	@FormulaGuid UNIQUEIDENTIFIER;
DECLARE	@AcceptOption BIT = 0;
DECLARE @IsVasNumber BIT;
DECLARE @ServiceId NVARCHAR(32);
DECLARE @IsRoot BIT;
DECLARE @SendSmsText NVARCHAR(MAX) = '';
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Date DATETIME = GETDATE();

SELECT @ServiceId = [ServiceID],@IsRoot = [IsRoot],@UserGuid = [OwnerGuid] FROM [dbo].[PrivateNumbers] WHERE [Guid] = @PrivateNumberGuid;
IF(ISNULL(@ServiceId,'') != '' AND @ServiceId != '0' AND @IsRoot = 1 AND LOWER(@SmsText) = 'off')
BEGIN
	EXEC [SmsParsers_SendSmsForUserActiveServices]
		@PrivateNumberGuid = @PrivateNumberGuid,
		@Mobile = @Mobile,
		@UserGuid = @UserGuid;

	RETURN;
END

SELECT  
  [Type] AS [ParserType] ,
  [UserGuid] ,
  [PrivateNumberGuid] ,
  [ParserFormulas].[Guid] AS [FormulaGuid]
	INTO #options
FROM
	[dbo].[SmsParsers] INNER JOIN
	[dbo].[ParserFormulas] ON [SmsParsers].[Guid] = [ParserFormulas].[SmsParserGuid]
WHERE
	[IsActive] = 1 AND
	[IsDeleted] = 0 AND
	[PrivateNumberGuid] = @PrivateNumberGuid AND
	(
		[FromDateTime] = [ToDateTime] OR
		GETDATE() BETWEEN [FromDateTime] AND [ToDateTime]
	);

IF(@@ROWCOUNT = 0)
	RETURN;

DECLARE ParserCursor CURSOR FAST_FORWARD READ_ONLY FOR
SELECT  
		[ParserType] ,
		[UserGuid] ,
		[PrivateNumberGuid] ,
		[FormulaGuid]
FROM #options

OPEN ParserCursor
FETCH NEXT FROM ParserCursor INTO @ParserType,@UserGuid,@PrivateNumberGuid,@FormulaGuid
WHILE @@FETCH_STATUS = 0
BEGIN
	IF(@ParserType = 1)
	BEGIN
		EXEC @AcceptOption = [dbo].[SmsParsers_ParseCompetition] @ReceivedSmsGuid = @SmsGuid,@FormulaGuid = @FormulaGuid;
	END
	ELSE IF(@ParserType = 2)
	BEGIN
		EXEC @AcceptOption = [dbo].[SmsParsers_ParsePoll] @ReceivedSmsGuid = @SmsGuid,@FormulaGuid = @FormulaGuid;
	END
	ELSE IF(@ParserType = 3)
	BEGIN
		EXEC @AcceptOption = [dbo].[SmsParsers_ParseFilter] @ReceivedSmsGuid = @SmsGuid,@FormulaGuid = @FormulaGuid;
	END

	IF(@AcceptOption = 1)
		BREAK;

	FETCH NEXT FROM ParserCursor INTO @ParserType,@UserGuid,@PrivateNumberGuid,@FormulaGuid
END
CLOSE ParserCursor
DEALLOCATE ParserCursor


DECLARE @MainAdminGuid UNIQUEIDENTIFIER;
EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

IF(@AcceptOption = 0 AND ISNULL(@ServiceId,'') != '' AND @ServiceId != '0' AND @IsRoot = 1)
BEGIN
	SELECT @SendSmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 13 AND [UserGuid] = @MainAdminGuid; --VasRegisterSmsText
	IF(ISNULL(@SendSmsText,'') = '')
		RETURN;

	SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SendSmsText);
	SELECT @SmsLen = [dbo].[GetSmsCount](@SendSmsText);

	EXEC [dbo].[ScheduledSmses_InsertSms]
				@Guid = @NewGuid,
				@PrivateNumberGuid = @PrivateNumberGuid,
				@Reciever = @Mobile,
				@SmsText = @SendSmsText,
				@SmsLen = @SmsLen,
				@PresentType = 1, -- Normal
				@Encoding = @Encoding,
				@TypeSend = 1, -- SendSms
				@Status = 1, -- Stored
				@DateTimeFuture = @Date,
				@UserGuid = @UserGuid,
				@IPAddress = N'',
				@Browser = N''
END

DROP TABLE #options;
GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_SendSmsForUserActiveServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_SendSmsForUserActiveServices]
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Mobile NVARCHAR(16),
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @SmsText NVARCHAR(MAX)='';
DECLARE @ServiceName NVARCHAR(64);
DECLARE @VasRegisterKeys NVARCHAR(512);
DECLARE @VasUnsubscribeKeys NVARCHAR(512);

DECLARE @ActiveServices TABLE([PhoneBookGuid] UNIQUEIDENTIFIER,[Name] NVARCHAR(64),[ServiceId] NVARCHAR(32),[Number] NVARCHAR(32),[VasRegisterKeys] NVARCHAR(512),[VasUnsubscribeKeys] NVARCHAR(512));

INSERT INTO @ActiveServices
EXEC [dbo].[PhoneBooks_GetActiveServices] @Mobile = @Mobile;

IF(SELECT COUNT(*) FROM @ActiveServices) = 0
	RETURN;

DECLARE ServiceCursor CURSOR FAST_FORWARD READ_ONLY FOR
SELECT
		[Name],
		[VasUnsubscribeKeys]
FROM @ActiveServices

OPEN ServiceCursor
FETCH NEXT FROM ServiceCursor INTO @ServiceName,@VasUnsubscribeKeys
WHILE @@FETCH_STATUS = 0
BEGIN
	SET @SmsText += @ServiceName + '-' + N'کلید واژه لغو عضویت ' + @VasUnsubscribeKeys + CHAR(13);

	FETCH NEXT FROM ServiceCursor INTO @ServiceName,@VasUnsubscribeKeys
END
CLOSE ServiceCursor
DEALLOCATE ServiceCursor

IF(@SmsText != '')
BEGIN
	DECLARE @Guid UNIQUEIDENTIFIER = NEWID();
	DECLARE @SmsLen INT;
	DECLARE @Encoding INT;
	DECLARE @Date DATETIME = GETDATE();

	SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SmsText);
	SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

	EXEC [dbo].[ScheduledSmses_InsertSms]
			@Guid = @Guid,
	    @PrivateNumberGuid = @PrivateNumberGuid,
	    @Reciever = @Mobile,
	    @SmsText = @SmsText,
	    @SmsLen = @SmsLen,
	    @PresentType = 1, -- Normal
	    @Encoding = @Encoding,
	    @TypeSend = 1, -- SendSms
	    @Status = 1, -- Stored
	    @DateTimeFuture = @Date,
	    @UserGuid = @UserGuid,
	    @IPAddress = N'',
	    @Browser = N''
	
END

GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_UpdateCompetition]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_UpdateCompetition]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@Scope UNIQUEIDENTIFIER,
	@ReplyPrivateNumberGuid UNIQUEIDENTIFIER,
	@ReplySmsText NVARCHAR(MAX),
	@DuplicatePrivateNumberGuid UNIQUEIDENTIFIER,
	@DuplicateUserSmsText NVARCHAR(MAX),
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Options SmsParserOptions READONLY
  AS


BEGIN TRANSACTION

BEGIN TRY

	DELETE FROM [dbo].[ParserFormulas] WHERE [SmsParserGuid] = @Guid;

	UPDATE [dbo].[SmsParsers]
	SET
			[Title] = @Title,
			[FromDateTime] = @FromDateTime,
			[ToDateTime] = @ToDateTime,
			[Scope] = @Scope,
			[ReplyPrivateNumberGuid] = @ReplyPrivateNumberGuid,
			[ReplySmsText] = @ReplySmsText,
			[DuplicatePrivateNumberGuid] = @DuplicatePrivateNumberGuid,
			[DuplicateUserSmsText] = @DuplicateUserSmsText,
			[PrivateNumberGuid]=@PrivateNumberGuid
	WHERE
			[Guid] = @Guid;

	INSERT INTO [dbo].[ParserFormulas]
								( [Guid] ,
									[Title] ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention] ,
									[SmsParserGuid])
					SELECT  NEWID() ,
									Title ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention],
									@Guid
					FROM
									@Options;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
END	CATCH
	


GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_UpdateFilter]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_UpdateFilter]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@TypeConditionSender INT,
	@ConditionSender NVARCHAR(50),
	@Scope NVARCHAR(MAX),
	@PrivateNumberGuid UNIQUEIDENTIFIER
  AS


	UPDATE [dbo].[SmsParsers]
	SET
			[Title] = @Title,
			[FromDateTime] = @FromDateTime,
			[ToDateTime] = @ToDateTime,
			[TypeConditionSender] = @TypeConditionSender,
			[ConditionSender] = @ConditionSender,
			[Scope] = @Scope,
			[PrivateNumberGuid] = @PrivateNumberGuid
	WHERE
			[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[SmsParsers_UpdatePoll]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsParsers_UpdatePoll]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@FromDateTime DATETIME,
	@ToDateTime DATETIME,
	@Scope NVARCHAR(MAX),
	@ReplyPrivateNumberGuid UNIQUEIDENTIFIER,
	@ReplySmsText NVARCHAR(MAX),
	@DuplicatePrivateNumberGuid UNIQUEIDENTIFIER,
	@DuplicateUserSmsText NVARCHAR(MAX),
	@PrivateNumberGuid UNIQUEIDENTIFIER,
	@Options SmsParserOptions READONLY
  AS


BEGIN TRANSACTION

BEGIN TRY

	DELETE FROM [dbo].[ParserFormulas] WHERE [SmsParserGuid] = @Guid;

	UPDATE [dbo].[SmsParsers]
	SET
			[Title] = @Title,
			[FromDateTime] = @FromDateTime,
			[ToDateTime] = @ToDateTime,
			[Scope] = @Scope,
			[ReplyPrivateNumberGuid] = @ReplyPrivateNumberGuid,
			[ReplySmsText] = @ReplySmsText,
			[DuplicatePrivateNumberGuid] = @DuplicatePrivateNumberGuid,
			[DuplicateUserSmsText] = @DuplicateUserSmsText,
			[PrivateNumberGuid]=@PrivateNumberGuid
	WHERE
			[Guid] = @Guid;

	INSERT INTO [dbo].[ParserFormulas]
								( [Guid] ,
									[Title] ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention] ,
									[SmsParserGuid])
					SELECT  NEWID() ,
									Title ,
									[Key] ,
									[IsCorrect],
									[ReactionExtention],
									@Guid
					FROM
									@Options;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
END	CATCH
	


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsSenderAgents_Delete]
(
@Guid UNIQUEIDENTIFIER
)
  AS

DELETE FROM
	[SmsSenderAgents]
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_GetFirstParentMainAdmin]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsSenderAgents_GetFirstParentMainAdmin]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @ParentGuid UNIQUEIDENTIFIER;

EXEC @ParentGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT @ParentGuid;

GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_GetSmsSenderAgentInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsSenderAgents_GetSmsSenderAgentInfo]
	@SmsSenderAgentRefrence INT
  AS


SELECT
			[IsSendActive],
			[IsRecieveActive],
			[IsSendBulkActive],
			[CheckMessageID]
FROM
			[SmsSenderAgents]
WHERE
			[IsDeleted] = 0 AND
			[SmsSenderAgentReference] = @SmsSenderAgentRefrence
			


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_GetSmsSenderAgentRefrense]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsSenderAgents_GetSmsSenderAgentRefrense]
	@SmsSenderAgentGuid UNIQUEIDENTIFIER
  AS



SELECT
			[SmsSenderAgentReference]
FROM
			[SmsSenderAgents]
WHERE
			[IsDeleted] = 0 AND
			[Guid] = @SmsSenderAgentGuid


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_GetUserAgents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsSenderAgents_GetUserAgents]
	@UserGuid UNIQUEIDENTIFIER
  AS
 
	
SELECT * FROM	[dbo].[SmsSenderAgents] WHERE [IsDeleted] = 0 AND [UserGuid] = @UserGuid;
GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsSenderAgents_Insert]
	@Guid UNIQUEIDENTIFIER,
	@ID INT,
	@Name NVARCHAR(50),
	@SmsSenderAgentReference INT,
	@Type TINYINT,
	@SendSmsAlert BIT,
	@IsSendActive BIT,
	@IsRecieveActive BIT,
	@IsSendBulkActive BIT,
	@SendBulkIsAutomatic BIT,
	@CheckMessageID BIT,
	@CreateDate DATETIME,
	@DefaultNumber NVARCHAR(32),
	@StartSendTime Time(7),
	@EndSendTime Time(7),
	@RouteActive BIT,
	@IsSmpp BIT,
	@Username NVARCHAR(32),
	@Password NVARCHAR(32),
	@SendLink NVARCHAR(512),
	@ReceiveLink NVARCHAR(512),
	@DeliveryLink NVARCHAR(512),
	@Domain NVARCHAR(32),
	@QueueLength INT,
	@UserGuid UNIQUEIDENTIFIER
  AS

INSERT INTO [SmsSenderAgents]
						([Guid],
						 [Name],
						 [SmsSenderAgentReference],
						 [Type],
						 [SendSmsAlert],
						 [IsSendActive],
						 [IsRecieveActive],
						 [IsSendBulkActive],
						 [SendBulkIsAutomatic],
						 [CheckMessageID],
						 [CreateDate],
						 [DefaultNumber],
						 [StartSendTime],
						 [EndSendTime],
						 [IsDeleted],
						 [RouteActive],
						 [IsSmpp],
						 [Username],
						 [Password],
						 [SendLink],
						 [ReceiveLink],
						 [DeliveryLink],
						 [Domain],
						 [QueueLength],
						 [UserGuid])
				VALUES
						(@Guid,
						 @Name,
						 @SmsSenderAgentReference,
						 @Type,
						 @SendSmsAlert,
						 @IsSendActive,
						 @IsRecieveActive,
						 @IsSendBulkActive,
						 @SendBulkIsAutomatic,
						 @CheckMessageID,
						 @CreateDate,
						 @DefaultNumber,
						 @StartSendTime,
						 @EndSendTime,
						 0,
						 @RouteActive,
						 @IsSmpp,
						 @Username,
						 @Password,
						 @SendLink,
						 @ReceiveLink,
						 @DeliveryLink,
						 @Domain,
						 @QueueLength,
						 @UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsSenderAgents_Update]
	@Guid UNIQUEIDENTIFIER,
	@Name NVARCHAR(50),
	@SmsSenderAgentReference INT,
	@IsSendActive BIT,
	@IsRecieveActive BIT,
	@IsSendBulkActive BIT,
	@CreateDate DATETIME
  AS

UPDATE [SmsSenderAgents] SET
	[Name] = @Name,
	[SmsSenderAgentReference] = @SmsSenderAgentReference,
	[IsSendActive] = @IsSendActive,
	[IsRecieveActive] = @IsRecieveActive,
	[IsSendBulkActive] = @IsSendBulkActive,
	[CreateDate] = @CreateDate,
	[IsDeleted] = 0
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_UpdateAgent]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsSenderAgents_UpdateAgent]
  @Guid UNIQUEIDENTIFIER ,
  @Name NVARCHAR(50) ,
  @SmsSenderAgentReference INT,
	@Type TINYINT,
	@DefaultNumber NVARCHAR(32),
	@StartSendTime TIME(5),
	@EndSendTime Time(5),
	@SendSmsAlert BIT,
  @IsSendActive BIT,
  @IsRecieveActive BIT,
  @IsSendBulkActive BIT,
	@SendBulkIsAutomatic BIT,
  @CheckMessageID BIT,
	@RouteActive BIT,
	@IsSmpp BIT,
	@Username NVARCHAR(32),
	@Password NVARCHAR(32),
	@SendLink NVARCHAR(512),
	@ReceiveLink NVARCHAR(512),
	@DeliveryLink NVARCHAR(512),
	@Domain NVARCHAR(32),
	@QueueLength INT
  AS


UPDATE
	[dbo].[SmsSenderAgents]
SET
	[Name] = @Name ,
	[SmsSenderAgentReference] = @SmsSenderAgentReference,
	[Type] = @Type,
	[DefaultNumber] = @DefaultNumber,
	[StartSendTime] = @StartSendTime,
	[EndSendTime]= @EndSendTime ,
	[SendSmsAlert] = @SendSmsAlert,
  [IsSendActive] = @IsSendActive,
  [IsRecieveActive] = @IsRecieveActive,
  [IsSendBulkActive] = @IsSendBulkActive,
	[SendBulkIsAutomatic] = @SendBulkIsAutomatic,
  [CheckMessageID] = @CheckMessageID,
	[RouteActive] = @RouteActive,
	[IsSmpp] = @IsSmpp,
	[Username] = @Username,
	[Password] = @Password,
	[SendLink] = @SendLink,
	[ReceiveLink] = @ReceiveLink,
	[DeliveryLink] = @DeliveryLink,
	[Domain] = @Domain,
	[QueueLength] = @QueueLength
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[SmsSenderAgents_UpdateStatus]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SmsSenderAgents_UpdateStatus]
    @SmsSenderAgentReference INT,
    @IsSendActive BIT,
    @IsRecieveActive BIT,
    @IsSendBulkActive BIT
  AS
 
    UPDATE  [SmsSenderAgents]
    SET     [IsSendActive] = @IsSendActive,
            [IsRecieveActive] = @IsRecieveActive,
            [IsSendBulkActive] = @IsSendBulkActive
    WHERE   
						[SmsSenderAgentReference] = @SmsSenderAgentReference


GO
/****** Object:  StoredProcedure [dbo].[SmsTemplates_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsTemplates_Delete]
	@Guid UNIQUEIDENTIFIER
  AS

	Update [SmsTemplates]
	set 
		[IsDeleted]=1
	WHERE
		[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[SmsTemplates_GetPagedSmsTemplates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsTemplates_GetPagedSmsTemplates]
	@Body NVARCHAR(max),
	@UserGuid UNIQUEIDENTIFIER,
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS

	DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0';
	
	IF ( @Where != '' ) 
		SET @Where += ' AND'
	SET @Where += ' [UserGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
		
	IF ( ISNULL(@Body, '') != '' ) 
	BEGIN
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [Body] LIKE N''%' + @Body + '%'''
	END

  IF ( @Where != '' ) 
		SET @Where = ' WHERE ' + @Where
	
--------------------------------------------------
  EXEC('
	WITH expTemp AS
	(SELECT
			*
		FROM 
			[dbo].[SmsTemplates]' + @Where + ')
    
		SELECT COUNT(*) AS [RowCount] FROM expTemp ;
	
		SELECT 
			*
		FROM
			[dbo].[SmsTemplates]' + @WHERE +'
		ORDER BY ' + @SortField + '
		OFFSET (' + @PageNo + ' - 1) * ' + @PageSize +' ROWS
		FETCH NEXT ' + @PageSize +'ROWS ONLY')


GO
/****** Object:  StoredProcedure [dbo].[SmsTemplates_GetUserSmsTemplates]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsTemplates_GetUserSmsTemplates]
	@UserGuid UNIQUEIDENTIFIER
  AS

SELECT
	[Guid] ,
	[Body] ,
	[CreateDate] ,
	[IsDeleted] ,
	[UserGuid]
FROM 
	[dbo].[SmsTemplates]
WHERE
	[IsDeleted] = 0 AND
	[UserGuid] = @UserGuid



GO
/****** Object:  StoredProcedure [dbo].[SmsTemplates_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsTemplates_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Body NVARCHAR(MAX),
	@CreateDate DATETIME,
	@UserGuid UNIQUEIDENTIFIER
  AS

	INSERT INTO [SmsTemplates]
		([Guid], [Body], [CreateDate], [IsDeleted], [UserGuid])
	VALUES
		(@Guid, @Body, @CreateDate,0, @UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[SmsTemplates_UpdateSmsTemplate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SmsTemplates_UpdateSmsTemplate]
	@Guid UNIQUEIDENTIFIER,
	@Body NVARCHAR(MAX)
  AS

	UPDATE [SmsTemplates]
	SET 
      [Body] = @Body
	WHERE 
			[Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[TrafficRelays_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TrafficRelays_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


UPDATE [dbo].[TrafficRelays]
SET
	[IsDeleted] = 1
WHERE
	[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[TrafficRelays_GetPagedTrafficRelays]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TrafficRelays_GetPagedTrafficRelays]
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256),
	@UserGuid UNIQUEIDENTIFIER
  AS
 
		DECLARE @Where NVARCHAR(MAX) = ' [IsDeleted] = 0 '
		
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
  
    IF ( @Where != '' ) 
      SET @Where = ' WHERE ' + @Where

--------------------------------------------------
    EXEC('
					SELECT 
								*
								INTO #TempTrafficRelays
					FROM 
								[dbo].[TrafficRelays]' + @Where + '
    
					SELECT COUNT(*) AS [RowCount] FROM #TempTrafficRelays;
				
					WITH expTemp AS
					(
						SELECT
								Row_Number() OVER (ORDER BY [CreateDate] DESC) AS [RowNumber], 
								*
						FROM
								#TempTrafficRelays
					)
					SELECT 
							*
					FROM
						expTemp
					WHERE 
						(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
						([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
					ORDER BY
						 [RowNumber] ;
			     
					DROP TABLE #TempTrafficRelays');


GO
/****** Object:  StoredProcedure [dbo].[TrafficRelays_InsertUrl]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TrafficRelays_InsertUrl]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@Url NVARCHAR(MAX),
	@TryCount INT,
	@IsActive BIT,
	@UserGuid UNIQUEIDENTIFIER
  AS


INSERT INTO [dbo].[TrafficRelays]
						([Guid] ,
						 [Title] ,
						 [Url] ,
						 [TryCount] ,
						 [CreateDate] ,
						 [IsActive] ,
						 [IsDeleted] ,
						 [UserGuid])
				VALUES  
						(@Guid ,
						 @Title,
						 @Url,
						 @TryCount,
						 GETDATE(),
						 @IsActive,
						 0,
						 @UserGuid)
GO
/****** Object:  StoredProcedure [dbo].[TrafficRelays_UpdateUrl]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[TrafficRelays_UpdateUrl]
	@Guid UNIQUEIDENTIFIER,
	@Title NVARCHAR(50),
	@Url NVARCHAR(MAX),
	@TryCount INT,
	@IsActive BIT
  AS


UPDATE [dbo].[TrafficRelays]
SET
	[Title] = @Title,
	[Url] =	@Url,
	[TryCount] = @TryCount,
	[IsActive] = @IsActive
WHERE
	[Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[Transactions_CalculateBenefit]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Transactions_CalculateBenefit]
	@UserGuid UNIQUEIDENTIFIER,
	@SmsCount BIGINT,
	@TransactionGuid UNIQUEIDENTIFIER
  AS


DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @GroupGuid UNIQUEIDENTIFIER;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @UserBasePrice DECIMAL(18,2);
DECLARE @ParentBasePrice DECIMAL(18,2);
DECLARE @Benefit DECIMAL(18,2);

SELECT @ParentGuid = [ParentGuid],@GroupGuid = [PriceGroupGuid] FROM [Users] WHERE [Guid] = @UserGuid;
IF(@ParentGuid = @EmptyGuid)
	SET @ParentGuid = @UserGuid;

SELECT @UserBasePrice = [BasePrice] FROM [GroupPrices] WHERE [Guid] = @GroupGuid;

SELECT @GroupGuid = [PriceGroupGuid] FROM [dbo].[Users] WHERE [Guid] = @ParentGuid;
SELECT @ParentBasePrice = [BasePrice] FROM [GroupPrices] WHERE [Guid] = @GroupGuid;

SET @Benefit = (@UserBasePrice - @ParentBasePrice) * @SmsCount;

UPDATE [dbo].[Transactions] SET [Benefit] = @Benefit WHERE [Guid] = @TransactionGuid;
GO
/****** Object:  StoredProcedure [dbo].[Transactions_ChangeCreditForAllParents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Transactions_ChangeCreditForAllParents]
	@UserGuid UNIQUEIDENTIFIER,
	@Description NVARCHAR(MAX),
	@SmsCount INT,
	@SmsPartCount INT,
	@SmsType INT,
	@Operator INT,
	@SmsSenderAgentRefrence INT,
	@TypeCreditChange INT,
	@TypeTransaction INT,
	@SmsSentGuid UNIQUEIDENTIFIER
  AS

BEGIN
	DECLARE @TypeTransactionHandler INT = 1
	DECLARE	@ParentUsers AS TABLE([UserGuid] UNIQUEIDENTIFIER)

	IF(@TypeTransaction = 2)
		SET @TypeTransactionHandler = -1

	INSERT INTO @ParentUsers 
		SELECT [UserGuid] FROM	dbo.udfGetAllParents(@UserGuid);

	DECLARE @SmsSenderAgentGuid UNIQUEIDENTIFIER,@Count INT;
	SELECT @SmsSenderAgentGuid = [Guid] FROM [dbo].[SmsSenderAgents] WHERE [SmsSenderAgentReference] = @SmsSenderAgentRefrence;

	SELECT 
				@Count = COUNT(*)
	FROM
				[Users] LEFT JOIN
				[SmsRates] smsRates ON [Users].[Guid] = smsRates.[UserGuid] INNER JOIN
				@ParentUsers admins ON [Users].[Guid] = admins.[UserGuid]
	WHERE
				smsRates.[Guid] IS NULL AND
				[Users].[Guid] != @UserGuid AND
				smsRates.[IsDeleted] = 0 AND
				[Operator] = @Operator AND
				[SmsSenderAgentGuid] = @SmsSenderAgentGuid

	IF(@Count > 0)
		RETURN 0
		 
	INSERT INTO [Transactions]([Guid],[ReferenceGuid], [TypeTransaction], [TypeCreditChange], [Description], [CreateDate], [CurrentCredit], [Amount], [UserGuid])
		SELECT 
					NEWID(),
					@SmsSentGuid, 
					@TypeTransaction,
					@TypeCreditChange, 
					@Description, 
					GETDATE(), 
					[Credit], 
					@SmsCount * @SmsPartCount *(CASE
																				WHEN @SmsType=1 THEN smsRates.[Farsi]
																				WHEN @SmsType=2 THEN smsRates.[Latin]
																			END),
					[Users].[Guid]
		FROM
					[Users] INNER JOIN
					[SmsRates] smsRates ON [Users].[Guid] = smsRates.[UserGuid] INNER JOIN
					@ParentUsers admins ON [Users].[Guid] = admins.[UserGuid]
		WHERE
					[Users].[Guid] != @UserGuid AND
					smsRates.[IsDeleted] = 0 AND
					[Operator] = @Operator AND
					[SmsSenderAgentGuid] = @SmsSenderAgentGuid

	----------------------------------
	UPDATE [Users]
	SET		[Credit] = CASE
										WHEN @SmsType=1 THEN [Credit] + (@TypeTransactionHandler * @SmsCount * @SmsPartCount * smsRates.[Farsi])
										WHEN @SmsType=2 THEN [Credit] + (@TypeTransactionHandler * @SmsCount * @SmsPartCount * smsRates.[Latin])
									 END
	FROM
				[Users] INNER JOIN
				[SmsRates] smsRates ON [Users].[Guid] = smsRates.[UserGuid] INNER JOIN
				@ParentUsers admins ON [Users].[Guid] = admins.[UserGuid]
	WHERE
				[Users].[Guid] != @UserGuid AND
				smsRates.[IsDeleted] = 0 AND
				[Operator] = @Operator AND
				[SmsSenderAgentGuid] = @SmsSenderAgentGuid
				
	RETURN 1
END


GO
/****** Object:  StoredProcedure [dbo].[Transactions_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Transactions_Delete]
(
@Guid UNIQUEIDENTIFIER
)
  AS

DELETE FROM
	[Transactions]
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Transactions_GetPagedUsersTransactions]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Transactions_GetPagedUsersTransactions]
	@UserGuid UNIQUEIDENTIFIER,
	@DomainGuid UNIQUEIDENTIFIER,
	@UserName NVARCHAR(32),
	@Query NVARCHAR(MAX),
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = ''
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @StartRow INT = (@PageNo - 1) * @PageSize;
DECLARE	@Statement NVARCHAR(MAX) = '';

IF(@UserName != '')
	SET @Where = '[UserName] LIKE ''%' + @UserName + '%''';
IF(@Query != '')
BEGIN
	IF(@Where != '')
		SET @Where += ' AND '
	SET @Where += @Query;
END	
IF (@Where != '' ) 
	SET @Where = ' WHERE ' + @Where;


CREATE TABLE #Children	(
 [UserGuid] UNIQUEIDENTIFIER NULL,
 [Username] [nvarchar](32) NULL	
) 

IF ( @DomainGuid = @EmptyGuid )
BEGIN
	INSERT INTO #Children([UserGuid],[Username])
	SELECT [UserGuid],[Username] FROM dbo.udfGetAllChildren(@UserGuid);
END
ELSE
BEGIN
	INSERT INTO #Children([UserGuid],[Username])
	SELECT [Guid],[UserName] FROM [dbo].[Users] WHERE [DomainGuid] = @DomainGuid AND [IsDeleted] = 0;
END

SET @Statement ='
SELECT 
	[Username],
	[Guid] ,
	[TypeTransaction] ,
	[TypeTransaction] [Type],
	[TypeCreditChange] ,
	[Description] ,
	[CreateDate] ,
	[CurrentCredit] ,
	[Amount] ,
	CASE [TypeTransaction]
		WHEN 1 THEN [CurrentCredit] + [Amount]
		WHEN 2 THEN [CurrentCredit] - [Amount]
	END AS [NextCredit],
	tranx.[UserGuid]
	INTO #temp
FROM
	[dbo].[Transactions] tranx WITH(NOLOCK) INNER JOIN
	#Children ON #Children.[UserGuid] = tranx.[UserGuid] '+ @Where +';
		
SELECT COUNT(*) [RowCount] FROM #Temp;
SELECT * FROM #Temp';

IF(@PageNo != 0 AND @PageSize != 0)
BEGIN
	SET @Statement +='
		ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
		OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT '+ CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
END

SET @Statement +=';

	SELECT
		ISNULL(SUM([Amount]),0) [TotalCount]
	FROM
		#Temp;

	DROP TABLE #Temp;
	DROP TABLE #Children';

EXECUTE SP_EXECUTESQL @Statement;


GO
/****** Object:  StoredProcedure [dbo].[Transactions_GetPagedUserTransactions]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Transactions_GetPagedUserTransactions]
	@UserGuid UNIQUEIDENTIFIER,
	@ReferenceGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
	@PageNo INT ,
	@PageSize INT ,
	@SortField NVARCHAR(256)
  AS


DECLARE @Where NVARCHAR(MAX) = ''
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SET @Where = ' [UserGuid] = ''' + CAST(@UserGuid AS VARCHAR(36)) + '''';

IF ( @ReferenceGuid != @EmptyGuid )
BEGIN
	IF(@Where != '')
		SET @Where += ' AND';
	SET @Where = ' [ReferenceGuid]=''' + CAST(@ReferenceGuid AS VARCHAR(36)) + ''''
END
      
IF ( @Where != '' ) 
	SET @Where = ' WHERE ' + @Where
IF(@Query != '')
	SET @Where += ' AND ' + @Query;

--------------------------------------------------
EXEC('SELECT 
					[Guid] ,
					[TypeTransaction] ,
					[TypeTransaction] [Type],
					[TypeCreditChange] ,
					[Description] ,
					[CreateDate] ,
					[CurrentCredit] ,
					[Amount] ,
					CASE [TypeTransaction]
						WHEN 1 THEN [CurrentCredit] + [Amount]
						WHEN 2 THEN [CurrentCredit] - [Amount]
					END AS [NextCredit] ,
					[UserGuid],
					[ReferenceGuid]
			INTO
					#TempTransaction
			FROM
					[Transactions]' +	@Where + '
    
    
SELECT COUNT(*) AS [RowCount] FROM #TempTransaction;
	
WITH expTemp AS
(
	SELECT
			Row_Number() OVER (ORDER BY [CreateDate] DESC,[TypeTransaction] DESC ) AS [RowNumber], 
			*
	FROM
			#TempTransaction
)
SELECT 
		*
FROM
	expTemp
WHERE 
	(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
	([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
ORDER BY
		[RowNumber] ;
     
			
DROP TABLE #TempTransaction')


GO
/****** Object:  StoredProcedure [dbo].[Transactions_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Transactions_Insert]
	@Guid UNIQUEIDENTIFIER,
	@ReferenceGuid UNIQUEIDENTIFIER,
	@TypeTransaction INT,
	@TypeCreditChange INT,
	@Description NVARCHAR(MAX),
	@CreateDate DATETIME,
	@CurrentCredit DECIMAL(18,2),
	@Amount DECIMAL(18,2),
	@Benefit DECIMAL(18,2),
	@UserGuid UNIQUEIDENTIFIER
  AS

INSERT INTO [Transactions]
						([Guid],
						 [ReferenceGuid],
						 [TypeTransaction],
						 [TypeCreditChange],
						 [Description],
						 [CreateDate],
						 [CurrentCredit],
						 [Amount],
						 [UserGuid])
			VALUES
						 (@Guid,
							@ReferenceGuid,
							@TypeTransaction,
							@TypeCreditChange,
							@Description,
							@CreateDate,
							@CurrentCredit,
							@Amount,
							@UserGuid)


GO
/****** Object:  StoredProcedure [dbo].[Transactions_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Transactions_Update]
(
@Guid UNIQUEIDENTIFIER,
@TypeTransaction INT,
@TypeCreditChange INT,
@Description NVARCHAR(MAX),
@CreateDate DATETIME,
@CurrentCredit DECIMAL(18),
@Amount DECIMAL(18),
@UserGuid UNIQUEIDENTIFIER
)
  AS

UPDATE [Transactions] SET
	[TypeTransaction] = @TypeTransaction,
	[TypeCreditChange] = @TypeCreditChange,
	[Description] = @Description,
	[CreateDate] = @CreateDate,
	[CurrentCredit] = @CurrentCredit,
	[Amount] = @Amount,
	[UserGuid] = @UserGuid
WHERE
	[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[UserDocuments_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDocuments_Delete]
	@Guid UNIQUEIDENTIFIER
  AS


DELETE FROM [dbo].[UserDocuments] WHERE [Guid] = @Guid;
GO
/****** Object:  StoredProcedure [dbo].[UserDocuments_GetUserDocuments]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDocuments_GetUserDocuments]
	@UserGuid UNIQUEIDENTIFIER
  AS


SELECT * FROM [dbo].[UserDocuments] WHERE [UserGuid] = @UserGuid;
GO
/****** Object:  StoredProcedure [dbo].[UserDocuments_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserDocuments_Insert]
	@Guid UNIQUEIDENTIFIER,
	@Type INT,
	@Key INT,
	@Value NVARCHAR(255),
	@Status INT,
	@Description NVARCHAR(512),
	@CreateDate DATETIME,
	@UserGuid UNIQUEIDENTIFIER
  AS


INSERT INTO [dbo].[UserDocuments]
        ([Guid] ,
         [Type] ,
         [Key] ,
         [Value] ,
         [Status] ,
				 [Description],
         [CreateDate] ,
         [UserGuid])
			VALUES
			  (@Guid,
				 @Type,
				 @Key,
				 @Value,
				 @Status,
				 @Description,
				 GETDATE(),
				 @UserGuid)
GO
/****** Object:  StoredProcedure [dbo].[UserFields_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserFields_Delete]
    @PhonebookGuid UNIQUEIDENTIFIER
  AS
 
    DELETE  FROM UserFields
    WHERE   [PhoneBookGuid] = @PhonebookGuid


GO
/****** Object:  StoredProcedure [dbo].[UserFields_DeleteField]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserFields_DeleteField]
    @PhoneBookGuid UNIQUEIDENTIFIER ,
    @Index INT
  AS
 
    DECLARE @Statment NVARCHAR(MAX) = 'UPDATE UserFields SET [Field'
        + CAST(@Index AS VARCHAR(2)) + '] = NULL ,[FieldType'
        + CAST(@Index AS VARCHAR(2)) + ']= NULL WHERE PhoneBookGuid= '''
        + CAST(@PhoneBookGuid AS VARCHAR(36)) + ''''   
    EXEC (@Statment)
   
    IF ( SELECT COUNT(PhoneBookGuid)
         FROM   [UserFields]
         WHERE  [Field1] IS NULL
                AND [Field2] IS NULL
                AND [Field3] IS NULL
                AND [Field4] IS NULL
                AND [Field5] IS NULL
                AND [Field6] IS NULL
                AND [Field7] IS NULL
                AND [Field8] IS NULL
                AND [Field9] IS NULL
                AND [Field10] IS NULL
                AND [Field11] IS NULL
                AND [Field12] IS NULL
                AND [Field13] IS NULL
                AND [Field14] IS NULL
                AND [Field15] IS NULL
                AND [Field16] IS NULL
                AND [Field17] IS NULL
                AND [Field18] IS NULL
                AND [Field19] IS NULL
                AND [Field20] IS NULL
                AND [PhoneBookGuid] = @PhoneBookGuid
       ) = 1 
        BEGIN
            DELETE  [UserFields]
            WHERE   [PhoneBookGuid] = @PhoneBookGuid
        END


GO
/****** Object:  StoredProcedure [dbo].[UserFields_GetPagedAllUserFields]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserFields_GetPagedAllUserFields]
	@UserGuid UNIQUEIDENTIFIER,
	@FieldName NVARCHAR(MAX),
	@PhoneBookName NVARCHAR(MAX),
	@PageNo INT,
	@PageSize INT,
	@SortField NVARCHAR(256)
  AS

DECLARE @Counter INT=1;
DECLARE @Statment NVARCHAR(MAX) ='',@Where NVARCHAR(MAX)='';

IF(ISNULL(@FieldName, '') != '') 
	BEGIN
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [FieldName] LIKE N''%' + @FieldName + '%'''
	END

IF(ISNULL(@PhoneBookName, '') != '') 
	BEGIN
		IF ( @Where != '' ) 
			SET @Where += ' AND'
		SET @Where += ' [PhoneBookName] LIKE N''%' + @PhoneBookName + '%'''
	END

IF (@Where != '' ) 
	SET @Where = ' WHERE ' + @Where	
-----------------------------------------------------------------
CREATE TABLE PhoneBookFields
	(
		[ID] INT NOT NUll Identity(1,1),
		[Guid] UNIQUEIDENTIFIER,
		[FieldName] NVARCHAR(50),
		[FieldType] INT,
		[PhoneBookName] NVARCHAR(50),
		[PhoneBookGuid] UNIQUEIDENTIFIER
		CONSTRAINT [PK_PhoneBookFields] PRIMARY KEY CLUSTERED ([ID] ASC)ON [PRIMARY]
	)
	
WHILE(@Counter<=20)
BEGIN
	SET @Statment+='
	INSERT INTO PhoneBookFields
							([Guid],
							 [FieldName],
							 [FieldType],
							 [PhoneBookName],
							 [PhoneBookGuid]
							)(
						SELECT 
							[UserFields].[Guid],
              [UserFields].[Field'+CAST(@Counter AS VARCHAR(2))+'] ,
              [UserFields].[FieldType'+CAST(@Counter AS VARCHAR(2))+'],
              [PhoneBooks].[Name],
              [UserFields].[PhoneBookGuid]
            FROM      
							[PhoneBooks] INNER JOIN
							[UserFields] ON [PhoneBooks].[Guid]=[UserFields].[PhoneBookGuid]
						WHERE
							[PhoneBooks].[UserGuid]='''+CAST(@UserGuid AS VARCHAR(36))+''' AND
							LEN([UserFields].[Field'+CAST(@Counter AS VARCHAR(2))+'])>0
					)';
	SET @Counter+=1;
END
	
EXEC(@Statment);

EXEC('SELECT COUNT(*) AS [RowCount] FROM [PhoneBookFields]' +	@Where + ';
		WITH expTemp AS
		(
			SELECT
					Row_Number() OVER (ORDER BY ' + @SortField + ') AS [RowNumber], 
					*				 
			FROM
					[PhoneBookFields]' +	@Where + '
		)
		SELECT 
				*
		FROM
			expTemp
		WHERE 
			(' + @PageNo + ' = 0 AND ' + @PageSize + ' = 0) OR
			([RowNumber] > (' + @PageNo + ' - 1) * ' + @PageSize + ' AND [RowNumber] <= ' + @PageNo + ' * ' + @PageSize + ')
		ORDER BY
			 [RowNumber] ;')

DROP TABLE PhoneBookFields;


GO
/****** Object:  StoredProcedure [dbo].[UserFields_GetPhoneBookField]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserFields_GetPhoneBookField]
    @PhoneBookGuid UNIQUEIDENTIFIER
  AS
 
    SELECT  *	
    INTO    #Temp
    FROM    ( SELECT    [UserFields].[Guid],
                        [UserFields].[Field1] ,
                        [UserFields].[Field2] ,
                        [UserFields].[Field3] ,
                        [UserFields].[Field4] ,
                        [UserFields].[Field5] ,
                        [UserFields].[Field6] ,
                        [UserFields].[Field7] ,
                        [UserFields].[Field8] ,
                        [UserFields].[Field9] ,
                        [UserFields].[Field10] ,
                        [UserFields].[Field11] ,
                        [UserFields].[Field12] ,
                        [UserFields].[Field13] ,
                        [UserFields].[Field14] ,
                        [UserFields].[Field15] ,
                        [UserFields].[Field16] ,
                        [UserFields].[Field17] ,
                        [UserFields].[Field18] ,
                        [UserFields].[Field19] ,
                        [UserFields].[Field20] ,
                        [UserFields].[FieldType1],
                        [UserFields].[FieldType2],
                        [UserFields].[FieldType3],
                        [UserFields].[FieldType4],
                        [UserFields].[FieldType5],
                        [UserFields].[FieldType6],
                        [UserFields].[FieldType7],
                        [UserFields].[FieldType8],
                        [UserFields].[FieldType9],
                        [UserFields].[FieldType10],
                        [UserFields].[FieldType11],
                        [UserFields].[FieldType12],
                        [UserFields].[FieldType13],
                        [UserFields].[FieldType14],
                        [UserFields].[FieldType15],
                        [UserFields].[FieldType16],
                        [UserFields].[FieldType17],
                        [UserFields].[FieldType18],
                        [UserFields].[FieldType19],
                        [UserFields].[FieldType20],
                        [UserFields].[PhoneBookGuid] ,
                        [PhoneNumbers].[F1] ,
                        [PhoneNumbers].[F2] ,
                        [PhoneNumbers].[F3] ,
                        [PhoneNumbers].[F4] ,
                        [PhoneNumbers].[F5],
                        [PhoneNumbers].[F6] ,
                        [PhoneNumbers].[F7] ,
                        [PhoneNumbers].[F8] ,
                        [PhoneNumbers].[F9] ,
                        [PhoneNumbers].[F10] ,
                        [PhoneNumbers].[F11] ,
                        [PhoneNumbers].[F12] ,
                        [PhoneNumbers].[F13] ,
                        [PhoneNumbers].[F14] ,
                        [PhoneNumbers].[F15] ,
                        [PhoneNumbers].[F16] ,
                        [PhoneNumbers].[F17] ,
                        [PhoneNumbers].[F18] ,
                        [PhoneNumbers].[F19] ,
                        [PhoneNumbers].[F20]
              FROM      [UserFields] LEFT JOIN [PhoneNumbers] 
												ON [UserFields].[PhoneBookGuid] = [PhoneNumbers].[PhoneBookGuid]
              WHERE     [UserFields].[PhoneBookGuid] = @PhoneBookGuid
            ) AS t

    SELECT  *
    FROM    #Temp

    SELECT  COUNT(CASE WHEN LEN(F1) > 0 THEN 1
                  END) AS CountField1 ,
            COUNT(CASE WHEN LEN(F2) > 0 THEN 1
                  END) AS CountField2 ,
            COUNT(CASE WHEN LEN(F3) > 0 THEN 1
                  END) AS CountField3 ,
            COUNT(CASE WHEN LEN(F4) > 0 THEN 1
                  END) AS CountField4 ,
            COUNT(CASE WHEN LEN(F5) > 0 THEN 1
                  END) AS CountField5 ,
            COUNT(CASE WHEN LEN(F6) > 0 THEN 1
                  END) AS CountField6 ,
            COUNT(CASE WHEN LEN(F7) > 0 THEN 1
                  END) AS CountField7 ,
            COUNT(CASE WHEN LEN(F8) > 0 THEN 1
                  END) AS CountField8 ,
            COUNT(CASE WHEN LEN(F9) > 0 THEN 1
                  END) AS CountField9 ,
            COUNT(CASE WHEN LEN(F10) > 0 THEN 1
                  END) AS CountField10 ,
            COUNT(CASE WHEN LEN(F11) > 0 THEN 1
                  END) AS CountField11 ,
            COUNT(CASE WHEN LEN(F12) > 0 THEN 1
                  END) AS CountField12 ,
            COUNT(CASE WHEN LEN(F13) > 0 THEN 1
                  END) AS CountField13 ,
            COUNT(CASE WHEN LEN(F14) > 0 THEN 1
                  END) AS CountField14 ,
            COUNT(CASE WHEN LEN(F15) > 0 THEN 1
                  END) AS CountField15 ,
            COUNT(CASE WHEN LEN(F16) > 0 THEN 1
                  END) AS CountField16 ,
            COUNT(CASE WHEN LEN(F17) > 0 THEN 1
                  END) AS CountField17 ,
            COUNT(CASE WHEN LEN(F18) > 0 THEN 1
                  END) AS CountField18 ,
            COUNT(CASE WHEN LEN(F19) > 0 THEN 1
                  END) AS CountField19 ,
            COUNT(CASE WHEN LEN(F20) > 0 THEN 1
                  END) AS CountField20
    FROM    #Temp

DROP TABLE #Temp;


GO
/****** Object:  StoredProcedure [dbo].[UserFields_InsertField]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserFields_InsertField]
    @Guid UNIQUEIDENTIFIER ,
    @PhoneBookGuid UNIQUEIDENTIFIER ,
    @Index INT ,
    @FieldValue NVARCHAR(100) ,
    @FieldType INT
  AS
 
    DECLARE @Statement AS NVARCHAR(MAX)= 'INSERT INTO 
		  UserFields([Guid],[PhoneBookGuid],[Field'
        + CAST(@Index AS VARCHAR(2)) + '],[FieldType'
        + CAST(@Index AS VARCHAR(2)) + '])
		  VALUES(''' + CAST(@Guid AS VARCHAR(36)) + ''','''
        + CAST(@PhoneBookGuid AS VARCHAR(36)) + ''',N''' + @FieldValue + ''','
        + CAST(@FieldType AS VARCHAR(2)) + ')'
		  
    EXEC(@Statement)


GO
/****** Object:  StoredProcedure [dbo].[UserFields_UpdateField]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserFields_UpdateField]
    @PhoneBookGuid UNIQUEIDENTIFIER ,
    @Index INT ,
    @FieldValue NVARCHAR(100) ,
    @FieldType INT
  AS
 
    DECLARE @Statment NVARCHAR(MAX) = 'UPDATE UserFields SET [Field'
        + CAST(@Index AS VARCHAR(2)) + '] = N''' + @FieldValue
        + ''' ,[FieldType' + CAST(@Index AS VARCHAR(2)) + ']='''
        + CAST(@FieldType AS VARCHAR(2)) + ''' WHERE PhoneBookGuid= '''
        + CAST(@PhoneBookGuid AS VARCHAR(36)) + ''''   
    EXEC (@Statment)


GO
/****** Object:  StoredProcedure [dbo].[Users_AdvanceUpdate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_AdvanceUpdate]
  @Guid UNIQUEIDENTIFIER,
  @UserName NVARCHAR(32),
  @Password NVARCHAR(512),
	@Type INT,
  @IsActive BIT,
  @MaximumAdmin INT,
  @MaximumUser INT,
	@MaximumPhoneNumber INT,
	@MaximumEmailAddress INT,
	@RoleGuid UNIQUEIDENTIFIER,
	@PriceGroupGuid UNIQUEIDENTIFIER,
	@IsFixPriceGroup BIT,
	@IsAuthenticated BIT,
  @DomainGuid UNIQUEIDENTIFIER,
  @PanelPrice DECIMAL(18, 2),
  @ExpireDate DATETIME 
  AS
 
    UPDATE  [dbo].[Users]
    SET     [UserName] = @UserName ,
            [Password] = @Password ,
						[Type] = @Type,
            [IsActive] = @IsActive ,
            [MaximumAdmin] = @MaximumAdmin ,
            [MaximumUser] = @MaximumUser ,
						[MaximumPhoneNumber] = @MaximumPhoneNumber,
						[MaximumEmailAddress] = @MaximumEmailAddress,
						[RoleGuid] = @RoleGuid,
						[PriceGroupGuid] = @PriceGroupGuid,
						[IsFixPriceGroup] = @IsFixPriceGroup,
						[IsAuthenticated] = @IsAuthenticated,
            [DomainGuid]= @DomainGuid,
            [PanelPrice] = @PanelPrice ,
            [ExpireDate] = @ExpireDate 
    WHERE   [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Users_CheckAllParentsCredit]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_CheckAllParentsCredit]
	@UserGuid UNIQUEIDENTIFIER,
	@SmsType INT,
	@Operator INT,
	@SmsSenderAgentRefrence INT,
	@SmsCount INT,
	@SmsPartCount INT
  AS


DECLARE @SmsSenderAgentGuid UNIQUEIDENTIFIER;
SELECT @SmsSenderAgentGuid=[Guid] FROM [dbo].[SmsSenderAgents] WHERE [SmsSenderAgentReference]=@SmsSenderAgentRefrence;

WITH [ParentUsers]([Guid], [ParentGuid])
	AS
	(
				SELECT 
							[Guid],
							[ParentGuid]
				FROM
							[Users]
				WHERE 
							[Guid] = @UserGuid AND
							[IsDeleted] = 0
							
				UNION ALL
				
				SELECT 
							Parent.[Guid],
							Parent.[ParentGuid]
				FROM 
							[Users] Parent INNER JOIN 
							[ParentUsers] Tree	ON Parent.[Guid] = Tree.[ParentGuid]
				WHERE
							Parent.[IsDeleted] = 0
	)
	SELECT 
				COUNT(*) [NotEnoughParentCountCredit]
	FROM 
				[Users] INNER JOIN 
				[ParentUsers] AS tree	ON [Users].[Guid] = tree.[Guid] LEFT JOIN
				[SmsRates] AS rates ON [Users].[Guid] = rates.[UserGuid]
	WHERE
				 [Users].[Guid] != @UserGuid AND
				 [Users].[IsAdmin] = 1 AND
				 [Users].[Credit] < @SmsCount * @SmsPartCount *(CASE
																													WHEN @SmsType=1 THEN ISNULL(rates.[Farsi],99^1000)
																													WHEN @SmsType=2 THEN ISNULL(rates.[Latin],99^1000)
																												END) AND
				 [Operator]=@Operator AND
				 [SmsSenderAgentGuid]=@SmsSenderAgentGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_Delete]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_Delete]
	@Guid uniqueidentifier
  AS


BEGIN TRANSACTION
BEGIN TRY
DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @UserCredit DECIMAL(18,2);
DECLARE @Username NVARCHAR(50);
DECLARE @ParentCredit DECIMAL(18,2);

SELECT
	@ParentGuid = [ParentGuid],
	@UserCredit = [Credit],
	@Username = [UserName]
FROM
	[dbo].[Users]
WHERE
	[Guid] = @Guid;

UPDATE
	[dbo].[Users]
SET
	[IsDeleted] = 1
WHERE
	[Guid] = @Guid;

IF(@UserCredit > 0)
BEGIN
	DECLARE @Description NVARCHAR(MAX) = N'افزایش اعتبار بدلیل حذف کاربر '+ @Username;
	
	SELECT @ParentCredit = [Credit] FROM [dbo].[Users] WHERE [Guid] = @ParentGuid;

	INSERT INTO [dbo].[Transactions]
						([Guid] ,
						[ReferenceGuid] ,
						[TypeTransaction] ,
						[TypeCreditChange] ,
						[Description] ,
						[CreateDate] ,
						[CurrentCredit] ,
						[Amount] ,
						[UserGuid])
					VALUES 
						(NEWID(),
						'00000000-0000-0000-0000-000000000000',
						1,--Increase
						33,--DeleteUser
						@Description,
						GETDATE(),
						@ParentCredit,
						@UserCredit,
						@ParentGuid);

	UPDATE [dbo].[Users] SET [Credit] += @UserCredit WHERE [Guid] = @ParentGuid;
END

UPDATE [dbo].[PrivateNumbers] SET [OwnerGuid] = @ParentGuid WHERE [OwnerGuid] = @Guid AND [UseForm] != 2;
UPDATE [dbo].[PrivateNumbers] SET [IsDeleted] = 1 WHERE [OwnerGuid] = @Guid AND [UseForm] = 2;

	COMMIT TRANSACTION;
END TRY
BEGIN CATCH
	ROLLBACK TRANSACTION;
END CATCH
GO
/****** Object:  StoredProcedure [dbo].[Users_DeleteAllUserGeneralPhoneBook]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Users_DeleteAllUserGeneralPhoneBook]
    @UserGuid UNIQUEIDENTIFIER 
  AS
 
		DELETE FROM 
				[UserGeneralPhoneBooks]
		WHERE   
				[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_DeleteAllUserService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_DeleteAllUserService]
    @UserGuid UNIQUEIDENTIFIER 
  AS
 
		DELETE FROM 
				[UserServices]
		WHERE   
				[UserGuid] = @UserGuid;
				
		DELETE FROM 
				[UserAccesses] 
		WHERE
				[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_GetAccessOfUserForActivation]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetAccessOfUserForActivation]
  @UserGuid UNIQUEIDENTIFIER,
  @ParentGuid UNIQUEIDENTIFIER
  AS
 

DECLARE @IsAdmin BIT = 0

SELECT @IsAdmin = [IsAdmin] FROM [Users] WHERE [Guid] = @UserGuid

SET @IsAdmin = ISNULL(@IsAdmin,0)

IF(@IsAdmin = 1)
	BEGIN
		SELECT  
				[Accesses].[Guid] ,
				[Accesses].[ReferencePermissionsKey],
				[Services].[Title] AS [ServiceTitle],
				CASE 
					WHEN [UserAccesses].[UserGuid] = @UserGuid THEN 1
					ELSE 0
				END AS [IsActive]
		FROM
				[Accesses] INNER JOIN
				[Services] ON [Accesses].[ServiceGuid] = [Services].[Guid] INNER JOIN
				[UserServices] ON [Services].[Guid] = [UserServices].[ServiceGuid] LEFT JOIN
				[UserAccesses] ON [UserAccesses].[UserGuid] = @UserGuid AND
													[Accesses].[Guid] = [UserAccesses].[AccessGuid]
		WHERE   
				[Accesses].[IsDeleted] = 0 AND
				[Services].[IsDeleted] = 0 AND
				[UserServices].[UserGuid] = @UserGuid

	END
ELSE
	BEGIN  
		SELECT
				[Accesses].[Guid] ,
				[Accesses].[ReferencePermissionsKey],
				[Services].[Title] AS [ServiceTitle],
				CASE 
					WHEN [UserAccesses].[UserGuid] = @UserGuid THEN 1
					ELSE 0
				END AS [IsActive]
		FROM
				[Accesses] INNER JOIN
				[Services] ON [Accesses].[ServiceGuid] = [Services].[Guid] INNER JOIN
				[UserServices] ON [Services].[Guid] = [UserServices].[ServiceGuid] LEFT JOIN
				[UserAccesses] ON [UserAccesses].[UserGuid] = @UserGuid AND
													[Accesses].[Guid] = [UserAccesses].[AccessGuid]
		WHERE   
				[Accesses].[IsDeleted] = 0 AND
				[Services].[IsDeleted] = 0 AND
				[UserServices].[UserGuid] = @ParentGuid
	END


GO
/****** Object:  StoredProcedure [dbo].[Users_GetAllChildren]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetAllChildren]
	@RootGuid AS UNIQUEIDENTIFIER
  AS


WITH [ChildUsers]([Guid], [ParentGuid])
	AS
	(
			SELECT 
						[Guid],
						[ParentGuid]
			FROM 
						[Users]
			WHERE 
						[Guid] = @RootGuid AND
						[IsDeleted] = 0
						
			UNION ALL
			
			SELECT 
						Child.[Guid], 
						Child.[ParentGuid]
			FROM
						[Users] AS Child INNER JOIN 
						[ChildUsers] AS Tree ON Child.[ParentGuid] = Tree.[Guid]
			WHERE
						[Child].[IsDeleted] = 0
	)
	SELECT
				[Users].*
	FROM
				[Users] INNER JOIN 
				[ChildUsers] AS Tree ON [Users].[Guid] = Tree.[Guid];


GO
/****** Object:  StoredProcedure [dbo].[Users_GetAllParents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetAllParents]
	@ChildGuid AS UNIQUEIDENTIFIER
  AS


WITH [ParentUsers]([Guid], [ParentGuid])
	AS
	(
				SELECT 
							[Guid],
							[ParentGuid]
				FROM
							[Users]
				WHERE 
							[Guid] = @ChildGuid AND
							[IsDeleted] = 0
							
				UNION ALL
				
				SELECT 
							Parent.[Guid],
							Parent.[ParentGuid]
				FROM 
							[Users] Parent INNER JOIN 
							[ParentUsers] Tree	ON Parent.[Guid] = Tree.[ParentGuid]
				WHERE
							Parent.[IsDeleted] = 0
	)
	SELECT 
				[Users].*
	FROM 
				[Users] INNER JOIN 
				[ParentUsers] AS Tree	ON [Users].[Guid] = Tree.[Guid]


GO
/****** Object:  StoredProcedure [dbo].[Users_GetChildren]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetChildren]
    @ParentGuid UNIQUEIDENTIFIER ,
    @UserName NVARCHAR(50) ,
    @FirstName NVARCHAR(50) ,
    @LastName NVARCHAR(50) ,
    @Email NVARCHAR(50) ,
    @CellPhone NVARCHAR(50) ,
    @CreateDate DATETIME ,
    @ExpireDate DATETIME ,
    @PanelPrice DECIMAL(18, 2) ,
    @IsActive BIT ,
    @UserType INT
  AS
 
    SELECT  [Guid] ,
            [UserName] ,
            [Password] ,
            [SecondPassword] ,
            [FirstName] ,
            [LastName] ,
            [Email] ,
            [Phone] ,
            [Mobile] ,
            [FaxNumber] ,
            [Address] ,
            [ZoneGuid] ,
            dbo.GetSolarDate([BirthDate]) AS [SolarBirthDate] ,
            dbo.GetSolarDate([CreateDate]) AS [SolarCreateDate] ,
            dbo.GetSolarDate([ExpireDate]) AS [SolarExpireDate] ,
            [BirthDate] ,
            [CreateDate] ,
            [ExpireDate] ,
            [Credit] ,
            [PanelPrice] ,
            [IsActive] ,
            [MaximumAdmin] ,
            [MaximumUser] ,
            [MaximumPhoneNumber] ,
            [ParentGuid] ,
            [PriceGroupGuid] ,
            [IsAdmin] ,
            [IsMainAdmin],
            CASE 
				WHEN [IsAdmin] = 1 AND [MaximumUser] > 0 THEN 'Agent'
				WHEN [IsAdmin] = 1 AND [MaximumUser] = 0 THEN 'NormalUser'	 
				ELSE 'UserNonReal'
			END [UserType]
    FROM    [Users]
    WHERE   [ParentGuid] = @ParentGuid
						AND [IsDeleted] = 0
            AND ( @UserName = ''
                  OR [UserName] LIKE '%' + @UserName + '%'
                )
            AND ( @FirstName = ''
                  OR [FirstName] LIKE '%' + @FirstName + '%'
                )
            AND ( @LastName = ''
                  OR [LastName] LIKE '%' + @LastName + '%'
                )
            AND ( @Email = ''
                  OR [Email] LIKE '%' + @Email + '%'
                )
            AND ( @CellPhone = ''
                  OR [Mobile] LIKE '%' + @CellPhone + '%'
                )
            AND ( ISNULL(@CreateDate, '') = ''
                  OR CONVERT(DATE,[CreateDate]) = CONVERT(DATE,@CreateDate)
                )
            AND ( ISNULL(@ExpireDate, '') = ''
                  OR CONVERT(DATE,[ExpireDate]) = CONVERT(DATE,@ExpireDate)
                )
            AND ( @PanelPrice = 0
                  OR [PanelPrice] = @PanelPrice
                )
            AND ( @UserType = 0
                  OR ( ( @UserType != 1
                         OR ( [IsAdmin] = 1
                              AND [MaximumUser] > 0
                            )
                       )
                       AND ( @UserType != 2
                             OR ( [IsAdmin] = 1
                                  AND [MaximumUser] = 0
                                )
                           )
                       AND ( @UserType != 3
                             OR ( [IsAdmin] = 0 )
                           )
                     )
                )
            AND ( ISNULL(@IsActive, '') = ''
                  OR [IsActive] = @IsActive
                )


GO
/****** Object:  StoredProcedure [dbo].[Users_GetCountChildrenType]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetCountChildrenType]
	@ParentGuid UNIQUEIDENTIFIER
  AS

	
WITH [ChildUsers]([Guid], [ParentGuid])
AS
(
	SELECT 
				[Guid],
				[ParentGuid]
	FROM 
				[Users]
	WHERE 
				[ParentGuid] = @ParentGuid AND
				[IsDeleted] = 0
				
	UNION ALL
	
	SELECT 
				Child.[Guid], 
				Child.[ParentGuid]
	FROM
				[Users] AS Child INNER JOIN 
				[ChildUsers] AS Tree ON Child.[ParentGuid] = Tree.[Guid]
	WHERE
				[Child].[IsDeleted] = 0
)

SELECT
		SUM(CASE WHEN ([IsAdmin]=1 AND [MaximumUser]>0) AND [Users].[Guid]!=@ParentGuid THEN 1 ELSE 0 END) [CountAdmin],
		SUM(CASE WHEN ([MaximumUser]=0) THEN 1 ELSE 0 END) [CountUser]
FROM
		[Users] INNER JOIN 
		[ChildUsers] AS Tree ON [Users].[Guid] = Tree.[Guid]


SELECT 
		[MaximumAdmin],
		[MaximumUser]
FROM 
		[Users]
WHERE 
		[Guid]=@ParentGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_GetCountRoleNumberOfOperators]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetCountRoleNumberOfOperators]
	@RoleGuid UNIQUEIDENTIFIER,
	@UserGuid UNIQUEIDENTIFIER
  AS


		DECLARE @Where NVARCHAR(MAX) = '[IsDeleted] = 0 AND
																		[ParentGuid] ='''+ CAST(@UserGuid AS VARCHAR(36)) +'''';
		
		IF ( @RoleGuid != '00000000-0000-0000-0000-000000000000' )
			BEGIN
				IF ( @Where != '' ) 
					SET @Where += ' AND'
				SET @Where += ' [RoleGuid] = ''' + CAST(@RoleGuid AS VARCHAR(36)) + ''''
			END

	 IF (@Where != '' ) 
			 SET @Where = ' WHERE ' + @Where
--------------------------------------------------       
	EXEC('
		WITH expTemp AS
		(SELECT
				[CellPhone],
				[dbo].GetNumberOperator([CellPhone]) AS [Operator]
		 FROM 
				[dbo].[Users]'+ @Where +')
		
		 SELECT
				COUNT(*) AS [Count],
				[Operator]
		 FROM 
				expTemp
		 GROUP BY 
				[Operator]
	');


GO
/****** Object:  StoredProcedure [dbo].[Users_GetCountUserNumberOfOperators]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetCountUserNumberOfOperators]
	@ParentGuid UNIQUEIDENTIFIER
  AS


WITH expTemp AS
(SELECT
		[Mobile],
		[dbo].GetNumberOperator([Mobile]) AS [Operator]
 FROM 
		[dbo].[Users]
 WHERE
		[IsDeleted] = 0 AND
		[ParentGuid] = @ParentGuid)

 SELECT
		COUNT(*) AS [Count],
		[Operator]
 FROM 
		expTemp
 GROUP BY 
		[Operator]


GO
/****** Object:  StoredProcedure [dbo].[Users_GetPagedUsers]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetPagedUsers]
  @UserGuid UNIQUEIDENTIFIER ,
	@DomainGuid UNIQUEIDENTIFIER,
	@Query NVARCHAR(MAX),
  @PageNo INT ,
  @PageSize INT ,
  @SortField NVARCHAR(256)
  AS
 

DECLARE @Where NVARCHAR(MAX) = 'WHERE [IsDeleted] = 0',
				@IsMainAdmin BIT = 0,
				@IsSuperAdmin BIT = 0,
				@StartRow INT = (@PageNo - 1) * @PageSize;

SELECT @IsMainAdmin= ISNULL([IsMainAdmin],0),@IsSuperAdmin= ISNULL([IsSuperAdmin],0) FROM [Users] WHERE [Guid] = @UserGuid;

IF(@DomainGuid = '00000000-0000-0000-0000-000000000000')
BEGIN
	IF(@IsMainAdmin = 0)
		SET @Where += ' AND [ParentGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''''
	ELSE IF(@IsMainAdmin = 1 AND @IsSuperAdmin = 0)
		SET @Where += ' AND ([ParentGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''' OR [Guid]='''+CAST(@UserGuid AS VARCHAR(36)) + ''')'
	IF(@IsSuperAdmin = 1)
		SET @Where += ' AND ([ParentGuid]=''' + CAST(@UserGuid AS VARCHAR(36)) + ''' OR [Guid]='''+CAST(@UserGuid AS VARCHAR(36)) + ''' OR [ParentGuid] = ''00000000-0000-0000-0000-000000000000'')'
END
ELSE
BEGIN
	SET @Where += ' AND [DomainGuid]=''' + CAST(@DomainGuid AS VARCHAR(36)) + ''''
END

IF(@Query != '')
	SET @Where += ' AND ' + @Query;

SET @SortField = REPLACE(@SortField,'[UserType]','CASE 
																				WHEN [IsAdmin] = 1 AND [MaximumUser] > 0 THEN ''Agent''
																				WHEN [IsAdmin] = 1 AND [MaximumUser] = 0 THEN ''NormalUser''
																				ELSE ''UserNonReal''
																			END');
--------------------------------------------------
DECLARE @Statement NVARCHAR(MAX) = '';

	SET @Statement = '
			SELECT
				*,
				CASE 
					WHEN [IsAdmin] = 1 AND [MaximumUser] > 0 THEN ''Agent''
					WHEN [IsAdmin] = 1 AND [MaximumUser] = 0 THEN ''NormalUser''
					ELSE ''UserNonReal''
				END [UserType],
				CASE
					WHEN CONVERT(DATE,[ExpireDate]) <= CONVERT(DATE,GETDATE()) THEN 1
					ELSE 0
				END [IsExpired]
				INTO #Temp
			FROM
				[dbo].[Users] '+ @Where +';
		
			SELECT COUNT(*) [RowCount] FROM #Temp;
			SELECT * FROM #Temp';

	IF(@PageNo != 0 AND @PageSize != 0)
	BEGIN
		SET @Statement +=' 
			 ORDER BY '+ CAST(@SortField AS NVARCHAR(255)) +'
			 OFFSET '+ CAST(@StartRow AS VARCHAR) +' ROWS FETCH NEXT ' + CAST(@PageSize AS VARCHAR) +' ROWS ONLY';
	END

	SET @Statement +=';DROP TABLE #Temp;';

	EXEC(@Statement);



GO
/****** Object:  StoredProcedure [dbo].[Users_GetServiceOfUserRole]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetServiceOfUserRole]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @UserRoleGuid UNIQUEIDENTIFIER;

SELECT @UserRoleGuid = [RoleGuid] FROM [dbo].[Users] WHERE [Guid]=@UserGuid;

	WITH expTemp AS(
		SELECT 
				[Services].[Guid]
		FROM 
				[Services] INNER JOIN
				[UserServices] ON [Services].[Guid] = [UserServices].[ServiceGuid]
		WHERE 
				[UserServices].[UserGuid] = @UserGuid AND
				[Services].[IsDeleted]=0
		)
	SELECT 
			[Services].[Guid],
			[Services].[Title],
			[RoleServices].[Price],
			[RoleServices].[RoleGuid],
			CASE 
				WHEN [Services].[Guid] IN (SELECT	[Guid] FROM  expTemp) THEN 1 ELSE 0
			END	[IsActive],
			groups.[Title] AS [GroupTitle],
			groups.[Guid] AS [GroupGuid]
	FROM 
			[Services] INNER JOIN
			[RoleServices] ON [Services].[Guid] = [RoleServices].[ServiceGuid] INNER JOIN
		  [ServiceGroups] groups ON [Services].[ServiceGroupGuid] = groups.[Guid]
	WHERE 
			[RoleServices].[RoleGuid] = @UserRoleGuid AND
			[Services].[IsDeleted]=0 AND
			groups.[IsDeleted]=0
	ORDER BY
			[GroupTitle],
			[Services].[Order]
			


GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserAccesses]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserAccesses]
    @UserGuid UNIQUEIDENTIFIER
  AS
 

SELECT 
      [Accesses].[ReferencePermissionsKey] 
FROM    
			[Accesses] INNER JOIN 
			[UserAccesses] ON [Accesses].[Guid] = [UserAccesses].[AccessGuid]
WHERE 
			[Accesses].[IsDeleted] = 0 AND
			[UserAccesses].[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserCredit]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserCredit]
  @UserGuid UNIQUEIDENTIFIER
  AS
 

SELECT [Credit] FROM [Users] WHERE [Guid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserForLogin]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserForLogin]
	@UserName NVARCHAR(32),
	@DomainGuid UNIQUEIDENTIFIER
  AS


DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

IF(@DomainGuid != @EmptyGuid)
BEGIN
	SELECT
		[Guid],
		[Password],
		[IsAuthenticated],
		[IsActive],
		[IsAdmin],
		[IsSuperAdmin],
		[IsMainAdmin],
		[ParentGuid],
		[ExpireDate],
		[FirstName],
		[LastName],
		[Mobile],
		[Email],
		[UserName],
		[NationalCode],
		[Credit],
		[RoleGuid],
		[DomainGuid]
	FROM 
		[dbo].[Users]
	WHERE
		[IsDeleted]=0 AND
		[UserName] = @UserName AND
		[DomainGuid] = @DomainGuid;
END
ELSE
BEGIN
	SELECT
		[Guid],
		[Password],
		[IsAuthenticated],
		[IsActive],
		[IsAdmin],
		[IsSuperAdmin],
		[IsMainAdmin],
		[ParentGuid],
		[ExpireDate],
		[FirstName],
		[LastName],
		[Mobile],
		[Email],
		[UserName],
		[NationalCode],
		[Credit],
		[RoleGuid],
		[DomainGuid]
	FROM 
		[dbo].[Users]
	WHERE
		[IsDeleted]=0 AND
		[UserName] = @UserName
END
GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserGroupPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserGroupPrice]
  @UserGuid UNIQUEIDENTIFIER
  AS
 

SELECT [PriceGroupGuid] FROM [Users] WHERE [Guid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserInfo]
  @UserGuid UNIQUEIDENTIFIER
  AS
 

SELECT [UserName],[FirstName],[LastName] FROM [Users] WHERE [Guid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserServices]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserServices]
	@UserGuid UNIQUEIDENTIFIER
  AS
 
	DECLARE @UserRoleGuid UNIQUEIDENTIFIER;
	SELECT @UserRoleGuid = [RoleGuid] FROM Users WHERE [IsDeleted]=0 AND [Guid] = @UserGuid;
	
SELECT 
		[Services].[ReferenceServiceKey],
		[RoleServices].[IsDefault],
		*
FROM
		[dbo].[Services] INNER JOIN
		[RoleServices] ON [RoleServices].[RoleGuid] = @UserRoleGuid AND
											[Services].[Guid] = [RoleServices].[ServiceGuid]
WHERE 
		[Services].[IsDeleted] = 0


GO
/****** Object:  StoredProcedure [dbo].[Users_GetUserServicesForShortcut]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_GetUserServicesForShortcut]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @RoleGuid UNIQUEIDENTIFIER;

SELECT @RoleGuid = [RoleGuid] FROM [dbo].[Users] WHERE [Guid] = @UserGuid;

SELECT 
			[Services].[Guid],
	    [Title] ,
		[TitleFa],
	    [IconAddress] ,
			[LargeIcon],
	    ReferencePageKey 
FROM    
			[dbo].[Services] services INNER JOIN
			[dbo].[RoleServices] roleservice ON services.[Guid] = roleservice.[ServiceGuid]
WHERE 
			[IsDeleted] = 0 AND
			[Presentable] = 1 AND
			[RoleGuid] = @RoleGuid;


GO
/****** Object:  StoredProcedure [dbo].[Users_Insert]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_Insert]
	@Guid UNIQUEIDENTIFIER,
	@UserName NVARCHAR(32),
	@Password NVARCHAR(512),
	@SecondPassword NVARCHAR(512),
	@FirstName NVARCHAR(32),
	@LastName NVARCHAR(64),
	@Email NVARCHAR(128),
	@Phone NVARCHAR(16),
	@Mobile NVARCHAR(16),
	@FaxNumber NVARCHAR(16),
	@Address NVARCHAR(MAX),
	@ZoneGuid UNIQUEIDENTIFIER,
	@BirthDate DATETIME,
	@CreateDate DATETIME,
	@ExpireDate DATETIME,
	@Credit DECIMAL(18,2),
	@PanelPrice DECIMAL(18,2),
	@IsActive BIT,
	@IsAdmin BIT,
	@MaximumAdmin INT,
	@MaximumUser INT,
	@MaximumPhoneNumber INT,	
	@ParentGuid UNIQUEIDENTIFIER,
	@DomainGroupPriceGuid UNIQUEIDENTIFIER,
	@PriceGroupGuid UNIQUEIDENTIFIER,
	@DomainGuid UNIQUEIDENTIFIER,
	@RoleGuid UNIQUEIDENTIFIER
  AS

   INSERT INTO Users
           ([Guid]
           ,[UserName]
           ,[Password]
           ,[SecondPassword]
           ,[FirstName]
           ,[LastName]
           ,[Email]
           ,[Phone]
           ,[Mobile]
           ,[FaxNumber]
           ,[Address]
           ,[ZoneGuid]
           ,[BirthDate]
           ,[CreateDate]
           ,[ExpireDate]
           ,[Credit]
           ,[PanelPrice]
           ,[IsActive]
           ,[IsAdmin]
           ,[IsMainAdmin]
           ,[MaximumAdmin]
           ,[MaximumUser]
           ,[MaximumPhoneNumber]
           ,[ParentGuid]
           ,[DomainGroupPriceGuid]
           ,[PriceGroupGuid]
           ,[DomainGuid]
           ,[RoleGuid]
           ,[IsDeleted])
     VALUES
           (@Guid
           ,@UserName
           ,@Password
           ,@SecondPassword
           ,@FirstName
           ,@LastName
           ,@Email
           ,@Phone
           ,@Mobile
           ,@FaxNumber
           ,@Address
           ,@ZoneGuid
           ,@BirthDate
           ,@CreateDate
           ,@ExpireDate
           ,@Credit
           ,@PanelPrice
           ,@IsActive
           ,@IsAdmin
           ,0
           ,@MaximumAdmin
           ,@MaximumUser
           ,@MaximumPhoneNumber
           ,@ParentGuid
           ,@DomainGroupPriceGuid
           ,@PriceGroupGuid
           ,@DomainGuid
           ,@RoleGuid
           ,0)


GO
/****** Object:  StoredProcedure [dbo].[Users_InsertAccount]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_InsertAccount]
	@Guid UNIQUEIDENTIFIER,
	@UserName NVARCHAR(32),
	@Password NVARCHAR(512),
	@ParentGuid UNIQUEIDENTIFIER,
	@RoleGuid UNIQUEIDENTIFIER,
	@PriceGroupGuid UNIQUEIDENTIFIER,
	@IsFixPriceGroup BIT,
	@IsAdmin BIT,
	@IsActive BIT,
	@ExpireDate DATETIME,
	@MaximumAdmin INT,
	@MaximumUser INT,
	@MaximumEmailAddress INT,
	@MaximumPhoneNumber INT,
	@PanelPrice DECIMAL(18,2),
	@DomainGuid UNIQUEIDENTIFIER
  AS

	INSERT INTO [dbo].[Users]
							([Guid],
							 [UserName],
							 [Password],
							 [ParentGuid],
							 [RoleGuid],
							 [PriceGroupGuid],
							 [IsFixPriceGroup],
							 [IsAdmin],
							 [IsActive],
							 [ExpireDate],
							 [MaximumAdmin],
							 [MaximumUser],
							 [MaximumEmailAddress],
							 [MaximumPhoneNumber],
							 [PanelPrice],
							 [DomainGuid],
							 [CreateDate],
							 [Credit],
							 [IsMainAdmin],
							 [IsDeleted])
				 VALUES
							(@Guid,
							 @UserName,
							 @Password,
							 @ParentGuid,
							 @RoleGuid,
							 @PriceGroupGuid,
							 @IsFixPriceGroup,
							 @IsAdmin,
							 @IsActive,
							 @ExpireDate,
							 @MaximumAdmin,
							 @MaximumUser,
							 @MaximumEmailAddress,
							 @MaximumPhoneNumber,
							 @PanelPrice,
							 @DomainGuid,
							 GETDATE(),
							 0,
							 0,
							 0)


GO
/****** Object:  StoredProcedure [dbo].[Users_InsertUserAccess]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_InsertUserAccess]
   @UserGuid UNIQUEIDENTIFIER,
   @AccessGuids VARCHAR(MAX)
  AS


DELETE FROM 
		[UserAccesses] 
WHERE
		[UserGuid] = @UserGuid
		
IF(@AccessGuids != '')		
	EXEC('INSERT INTO [UserAccesses]([UserGuid],[AccessGuid])
				SELECT
							'''+ @UserGuid +''',
							[Guid]
				FROM 
							[Accesses]	
				WHERE
							[IsDeleted] = 0 AND
							[Guid] IN ('+ @AccessGuids +')')


GO
/****** Object:  StoredProcedure [dbo].[Users_InsertUserAccessByService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Users_InsertUserAccessByService]
   @UserGuid UNIQUEIDENTIFIER,
   @ServiceGuids VARCHAR(MAX)
  AS


DELETE FROM 
		[UserAccesses] 
WHERE
		[UserGuid] = @UserGuid
		
IF(@ServiceGuids != '')	
	EXEC('INSERT INTO [UserAccesses]([UserGuid],[AccessGuid])
				SELECT
							'''+ @UserGuid +''',
							[Guid]
				FROM 
							[Accesses]	
				WHERE
							[IsDeleted] = 0 AND
							[ServiceGuid] IN ('+ @ServiceGuids +')')


GO
/****** Object:  StoredProcedure [dbo].[Users_InsertUserService]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Users_InsertUserService]
   @UserGuid UNIQUEIDENTIFIER,
   @ServiceGuids VARCHAR(MAX)
  AS


DELETE FROM 
		[UserServices] 
WHERE
		[UserGuid] = @UserGuid
		
IF(@ServiceGuids != '')		
	EXEC('INSERT INTO [UserServices]([UserGuid],[ServiceGuid])
				SELECT
							'''+ @UserGuid +''',
							[Guid]
				FROM 
							[Services]	
				WHERE
							[IsDeleted] = 0 AND
							[Guid] IN ('+ @ServiceGuids +')')


GO
/****** Object:  StoredProcedure [dbo].[Users_RegisterUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_RegisterUser]
	@Guid UNIQUEIDENTIFIER,
	@Type INT,
	@UserName NVARCHAR(32),
	@Password NVARCHAR(512),
	@Email NVARCHAR(128),
	@Mobile NVARCHAR(16),
	@ParentGuid UNIQUEIDENTIFIER,
	@RoleGuid UNIQUEIDENTIFIER,
	@PriceGroupGuid UNIQUEIDENTIFIER,
	@IsFixPriceGroup BIT,
	@IsAdmin BIT,
	@IsActive BIT,
	@ExpireDate DATETIME,
	@MaximumAdmin INT,
	@MaximumUser INT,
	@PanelPrice DECIMAL(18,2),
	@DomainGuid UNIQUEIDENTIFIER
  AS


INSERT INTO [dbo].[Users]
						([Guid],
							[Type],
							[UserName],
							[Password],
							[Email],
							[Mobile],
							[ParentGuid],
							[RoleGuid],
							[PriceGroupGuid],
							[IsFixPriceGroup],
							[IsAdmin],
							[IsActive],
							[ExpireDate],
							[MaximumAdmin],
							[MaximumUser],
							[PanelPrice],
							[DomainGuid],
							[CreateDate],
							[Credit],
							[IsMainAdmin],
							[IsDeleted])
				VALUES
						(@Guid,
							@Type,
							@UserName,
							@Password,
							@Email,
							@Mobile,
							@ParentGuid,
							@RoleGuid,
							@PriceGroupGuid,
							@IsFixPriceGroup,
							@IsAdmin,
							@IsActive,
							@ExpireDate,
							@MaximumAdmin,
							@MaximumUser,
							@PanelPrice,
							@DomainGuid,
							GETDATE(),
							0,
							0,
							0);

EXEC [dbo].[Users_SendSmsForRegisterUser]
	@UserGuid = @Guid,
	@UserName = @UserName,
	@ParentGuid = @ParentGuid,
	@DomainGuid = @DomainGuid,
	@Mobile = @Mobile



GO
/****** Object:  StoredProcedure [dbo].[Users_RetrievePassword]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_RetrievePassword]
	@UserName NVARCHAR(32),
	@RawPassword NVARCHAR(32),
	@NewPassword NVARCHAR(512)
  AS


DECLARE @UserGuid UNIQUEIDENTIFIER;
DECLARE @Mobile NVARCHAR(16);
DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000'

SELECT @UserGuid = [Guid],@Mobile = [Mobile],@ParentGuid = [ParentGuid] FROM [dbo].[Users] WHERE [UserName] = @UserName;

IF(@@ROWCOUNT = 0)
	RETURN;

UPDATE [dbo].[Users] SET [Password] = @NewPassword WHERE [Guid] = @UserGuid;

IF(@ParentGuid = @EmptyGuid)
	SET @ParentGuid = @UserGuid;

EXEC [dbo].[Users_SendSmsForRetrievePassword]
	@UserGuid = @UserGuid,
  @ParentGuid = @ParentGuid,
  @Password = @RawPassword,
  @Mobile = @Mobile

GO
/****** Object:  StoredProcedure [dbo].[Users_SendSmsForLogin]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_SendSmsForLogin]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @Receivers NVARCHAR(MAX);
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000' ;
DECLARE @NewGuid UNIQUEIDENTIFIER;
DECLARE @SmsText NVARCHAR(MAX)='';
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @UserName NVARCHAR(32);
DECLARE @FirstName NVARCHAR(32);
DECLARE @LastName NVARCHAR(64);
DECLARE @Mobile NVARCHAR(16);
DECLARE @Date CHAR(10);
DECLARE @Time CHAR(5);
DECLARE @Domain NVARCHAR(64);
DECLARE @DomainGuid UNIQUEIDENTIFIER;
DECLARE @SmsSenderAgentReference INT;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @MainAdminGuid UNIQUEIDENTIFIER;

SELECT 
	@UserName = [UserName] ,
	@FirstName = [FirstName] ,
	@LastName = [LastName],
	@Mobile = [Mobile],
	@DomainGuid = [DomainGuid]
FROM
	[dbo].[Users]
WHERE
	[Guid] = @UserGuid;

EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT @Domain = [Name] FROM [dbo].[Domains] WHERE [Guid] = @DomainGuid;
SELECT @Receivers = [Value] FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 3; --LoginWarning
SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 5 AND [UserGuid] = @MainAdminGuid; --LoginSmsText
SELECT @PrivateNumberGuid = CAST([Value] AS UNIQUEIDENTIFIER) FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 5--DefaultNumber
SELECT @Date = dbo.GetSolarDate(CONVERT(DATE,GETDATE()));
select @Time = CONVERT(char(5), GETDATE(), 108)

SET @SmsText = REPLACE(@SmsText,'#firstname#',@FirstName);
SET @SmsText = REPLACE(@SmsText,'#lastname#',@LastName);
SET @SmsText = REPLACE(@SmsText,'#date#',@Date);
SET @SmsText = REPLACE(@SmsText,'#time#',@Time);
SET @SmsText = REPLACE(@SmsText,'#username#',@UserName);
SET @SmsText = REPLACE(@SmsText,'#domain#',@domain);

IF(ISNULL(@SmsText,'')='' OR @PrivateNumberGuid = @EmptyGuid)
	RETURN;

SELECT 
	@SmsSenderAgentReference = [SmsSenderAgentReference]
FROM 
	[dbo].[PrivateNumbers] number INNER JOIN
	[dbo].[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	number.[Guid] = @PrivateNumberGuid;

SELECT
	NEWID() [Guid],
	[Item] [Mobile],
	0 [Operator],
	0 [IsBlackList]
	INTO #Receivers
FROM
	[dbo].[SplitString](@Receivers,',')
OPTION (MAXRECURSION 0)

UPDATE #Receivers SET [Operator] = dbo.GetNumberOperator([Mobile]);

IF(SELECT COUNT(*) FROM #Receivers WHERE [Operator] > 0) = 0
	RETURN;

SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SmsText);
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

INSERT INTO dbo.ScheduledSmses
				([Guid] ,
					[PrivateNumberGuid],
					[SmsText] ,
					[PresentType] ,
					[Encoding] ,
					[SmsLen] ,
					[TypeSend] ,
					[DateTimeFuture] ,
					[CreateDate] ,
					[Status] ,
					[SmsSenderAgentReference],
					[IsDeleted] ,
					[UserGuid]
				)
			SELECT
					[Guid],
					@PrivateNumberGuid,
					@SmsText,
					1,--Normal
					CASE WHEN [dbo].[HasUniCodeCharacter](@SmsText) = 1 THEN 2
							 ELSE 1 
					END,
					[dbo].[GetSmsCount](@SmsText),
					1,--SendSms
					GETDATE(),
					GETDATE(),
					1,--Stored
					@SmsSenderAgentReference,
					0,
					@UserGuid
			FROM
					#Receivers

INSERT INTO [dbo].[Recipients]
	      ([Guid] ,
	        [Mobile] ,
	        [Operator] ,
	        [IsBlackList] ,
	        [ScheduledSmsGuid])
				SELECT
					NEWID(),
					[Mobile],
					dbo.GetNumberOperator([Mobile]),
					[IsBlackList],
					[Guid]
				FROM
					#Receivers

DROP TABLE #Receivers;

GO
/****** Object:  StoredProcedure [dbo].[Users_SendSmsForParentOfUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_SendSmsForParentOfUser]
	@UserGuid UNIQUEIDENTIFIER,
	@SmsText NVARCHAR(MAX)
  AS


DECLARE @Mobile NVARCHAR(16);
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Date DATETIME = GETDATE();
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SELECT @ParentGuid = [ParentGuid] FROM	[dbo].[Users] WHERE [Guid] = @UserGuid;
SELECT @Mobile = [Mobile] FROM [dbo].[Users] WHERE [Guid] = @ParentGuid;

SELECT 
	@PrivateNumberGuid = CASE LEN([Value]) WHEN  0 THEN @EmptyGuid
																				 WHEN 36 THEN CAST([Value] AS UNIQUEIDENTIFIER)
																				 ELSE @EmptyGuid END
FROM [dbo].[UserSettings] WHERE [UserGuid] = @ParentGuid AND [Key] = 5--DefaultNumber

IF(@@ROWCOUNT=0)
	RETURN;

IF(ISNULL(@SmsText,'') = '' OR @PrivateNumberGuid = @EmptyGuid)
	RETURN;

SELECT @Encoding = 	CASE WHEN [dbo].[HasUniCodeCharacter](@SmsText) = 1 THEN 2
												 ELSE 1 
										END;

SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

IF(ISNULL(@Mobile,'') != '')
BEGIN
	EXEC [dbo].[ScheduledSmses_InsertSms]
			@Guid = @NewGuid,
	    @PrivateNumberGuid = @PrivateNumberGuid,
	    @Reciever = @Mobile,
	    @SmsText = @SmsText,
	    @SmsLen = @SmsLen,
	    @PresentType = 1, -- Normal
	    @Encoding = @Encoding,
	    @TypeSend = 1, -- SendSms
	    @Status = 1, -- Stored
	    @DateTimeFuture = @Date,
	    @UserGuid = @ParentGuid,
	    @IPAddress = N'',
	    @Browser = N''
END

GO
/****** Object:  StoredProcedure [dbo].[Users_SendSmsForRegisterUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_SendSmsForRegisterUser]
	@UserGuid UNIQUEIDENTIFIER,
	@UserName NVARCHAR(32),
	@ParentGuid UNIQUEIDENTIFIER,
	@DomainGuid UNIQUEIDENTIFIER,
	@Mobile NVARCHAR(16)
  AS


DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsText NVARCHAR(MAX)='';
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @DomainName NVARCHAR(32);
DECLARE @ParentMobile NVARCHAR(16);
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @SmsSenderAgentReference INT;
DECLARE @MainAdminGuid UNIQUEIDENTIFIER;

EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 7 AND [UserGuid] = @MainAdminGuid; --RegisterUserSmsText
SELECT @PrivateNumberGuid = CAST([Value] AS UNIQUEIDENTIFIER) FROM [dbo].[UserSettings] WHERE [UserGuid] = @ParentGuid AND [Key] = 5--DefaultNumber
SELECT @DomainName = [Name] FROM [dbo].[Domains] WITH(NOLOCK) WHERE [Guid] = @DomainGuid;
SELECT @ParentMobile = [Mobile] FROM [dbo].[Users] WHERE [Guid] = @ParentGuid;

SET @SmsText = REPLACE(@SmsText,'#username#',@UserName);
SET @SmsText = REPLACE(@SmsText,'#usermobile#',@Mobile);
SET @SmsText = REPLACE(@SmsText,'#domain#',@DomainName);

IF(ISNULL(@SmsText,'')='' OR @PrivateNumberGuid = @EmptyGuid)
	RETURN;

SELECT @SmsSenderAgentReference = [agent].[SmsSenderAgentReference]
FROM
	[dbo].[PrivateNumbers] number INNER JOIN
	[dbo].[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	number.[Guid] = @PrivateNumberGuid;

IF(dbo.GetNumberOperator(@ParentMobile) = 0)
	RETURN;

SELECT @Encoding = 	CASE WHEN [dbo].[HasUniCodeCharacter](@SmsText) = 1 THEN 2
												 ELSE 1 
										END;
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

INSERT INTO dbo.ScheduledSmses
				([Guid] ,
				 [PrivateNumberGuid] ,
				 [SmsText] ,
				 [PresentType] ,
				 [Encoding] ,
				 [SmsLen] ,
				 [TypeSend] ,
				 [DateTimeFuture] ,
				 [CreateDate] ,
				 [Status] ,
				 [SmsSenderAgentReference],
				 [IsDeleted] ,
				 [UserGuid])
			SELECT
					@NewGuid,
					@PrivateNumberGuid,
					@SmsText,
					1,--Normal
					@Encoding,
					[dbo].[GetSmsCount](@SmsText),
					1,--SendSms
					GETDATE(),
					GETDATE(),
					1,--Stored
					@SmsSenderAgentReference,
					0,
					@ParentGuid;

INSERT INTO [dbo].[Recipients]
	      ([Guid] ,
	       [Mobile] ,
	       [Operator] ,
	       [IsBlackList] ,
	       [ScheduledSmsGuid])
				SELECT
					NEWID(),
					@ParentMobile,
					dbo.GetNumberOperator(@ParentMobile),
					0,
					@NewGuid;

GO
/****** Object:  StoredProcedure [dbo].[Users_SendSmsForRetrievePassword]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_SendSmsForRetrievePassword]
	@UserGuid UNIQUEIDENTIFIER,
	@ParentGuid UNIQUEIDENTIFIER,
	@Password NVARCHAR(32),
	@Mobile NVARCHAR(16)
  AS


DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsText NVARCHAR(MAX)='';
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @SmsSenderAgentReference INT;
DECLARE @MainAdminGuid UNIQUEIDENTIFIER;

EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 11 AND [UserGuid] = @MainAdminGuid; --RetrievePasswordSmsText
SELECT @PrivateNumberGuid = CAST([Value] AS UNIQUEIDENTIFIER) FROM [dbo].[UserSettings] WHERE [UserGuid] = @ParentGuid AND [Key] = 5--DefaultNumber

SELECT 
	@SmsSenderAgentReference = [SmsSenderAgentReference]
FROM 
	[dbo].[PrivateNumbers] number INNER JOIN
	[dbo].[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	number.[Guid] = @PrivateNumberGuid;

SET @SmsText = REPLACE(@SmsText,'#password#',@Password);

IF(ISNULL(@SmsText,'') ='' OR @PrivateNumberGuid = @EmptyGuid)
	RETURN;

IF(dbo.GetNumberOperator(@Mobile) = 0)
	RETURN;

SELECT @Encoding = 	CASE WHEN [dbo].[HasUniCodeCharacter](@SmsText) = 1 THEN 2
												 ELSE 1 
										END;
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

INSERT INTO dbo.ScheduledSmses
				([Guid] ,
				 [PrivateNumberGuid] ,
				 [SmsSenderAgentReference],
				 [SmsText] ,
				 [PresentType] ,
				 [Encoding] ,
				 [SmsLen] ,
				 [TypeSend] ,
				 [DateTimeFuture] ,
				 [CreateDate] ,
				 [Status] ,
				 [IsDeleted] ,
				 [UserGuid])
			SELECT
					@NewGuid,
					@PrivateNumberGuid,
					@SmsSenderAgentReference,
					@SmsText,
					1,--Normal
					@Encoding,
					[dbo].[GetSmsCount](@SmsText),
					1,--SendSms
					GETDATE(),
					GETDATE(),
					1,--Stored
					0,
					@ParentGuid;

INSERT INTO [dbo].[Recipients]
	      ([Guid] ,
	       [Mobile] ,
	       [Operator] ,
	       [IsBlackList] ,
	       [ScheduledSmsGuid])
				SELECT
					NEWID(),
					@Mobile,
					dbo.GetNumberOperator(@Mobile),
					0,
					@NewGuid;

GO
/****** Object:  StoredProcedure [dbo].[Users_SendSmsForUserExpired]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_SendSmsForUserExpired]
  AS


RETURN;
DECLARE @SmsText NVARCHAR(MAX)='';

SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 9;--UserExpireSmsText

IF(ISNULL(@SmsText,'') = '')
	RETURN;

WITH expTemp AS(
	SELECT
		userSetting.[UserGuid],
		users.[Mobile]
	FROM 
			(SELECT 
				[UserGuid],
				
				[Key],
				[Value]
			FROM
				[UserSettings]
			WHERE
				[Key]= 2  AND--ExpireWarning
				CONVERT(INT,[Value])>0) userSetting INNER JOIN
																[Users] users ON [userSetting].[UserGuid]=[Users].[Guid] AND
																CONVERT(VARCHAR(10),DATEADD(DAY,CONVERT(INT,[Value]),GETDATE()),111)=CONVERT(VARCHAR(10),[ExpireDate],111) 
)
	
SELECT
	TOP 1
	NEWID() [Guid],
	number.[Guid] [NumberGuid],
	number.[UserGuid] [UserGuid],
	expTemp.[Mobile] [Mobile],
	0 [IsBlackList]
	INTO #UserExpired
FROM [dbo].[PrivateNumbers] number
INNER JOIN expTemp	ON number.[OwnerGuid] = expTemp.[UserGuid]
WHERE
	[IsDeleted] = 0 AND
	[IsActive] = 1 AND
	[IsDefault] = 1 AND
	[UseForm] != 2--Range

IF(SELECT COUNT(*) FROM #UserExpired)>0
BEGIN
	INSERT INTO dbo.ScheduledSmses
					([Guid] ,
					 [PrivateNumberGuid] ,
					 [SmsText] ,
					 [PresentType] ,
					 [Encoding] ,
					 [SmsLen] ,
					 [TypeSend] ,
					 [DateTimeFuture] ,
					 [CreateDate] ,
					 [Status] ,
					 [IsDeleted] ,
					 [UserGuid]
					)
				SELECT
					 [Guid],
					 [NumberGuid],
					 @SmsText,
					 1,--Normal
					 CASE WHEN [dbo].[HasUniCodeCharacter](@SmsText) = 1 THEN 2
								ELSE 1 
					 END,
					 [dbo].[GetSmsCount](@SmsText),
					 1,--SendSms
					 GETDATE(),
					 GETDATE(),
					 1,--Stored
					 0,
					 [UserGuid]
				FROM
						#UserExpired

	INSERT INTO [dbo].[Recipients]
	        ([Guid] ,
	         [Mobile] ,
	         [Operator] ,
	         [IsBlackList] ,
	         [ScheduledSmsGuid])
				 SELECT
					 NEWID(),
					 [Mobile],
					 dbo.GetNumberOperator([Mobile]),
					 [IsBlackList],
					 [Guid]
				 FROM
						#UserExpired
END

DROP TABLE #UserExpired;
GO
/****** Object:  StoredProcedure [dbo].[Users_SendUserAccountInfo]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_SendUserAccountInfo]
	@Password NVARCHAR(32),
	@Domain NVARCHAR(32),
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsText NVARCHAR(MAX)='';
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Mobile NVARCHAR(16);
DECLARE @UserName NVARCHAR(32);
DECLARE @ParentGuid UNIQUEIDENTIFIER;
DECLARE @FirstName NVARCHAR(32);
DECLARE @LastName NVARCHAR(64);
DECLARE @SmsSenderAgentReference INT;
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';
DECLARE @MainAdminGuid UNIQUEIDENTIFIER;

EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT
	@Mobile = [Mobile],
	@UserName = [UserName],
	@ParentGuid = [ParentGuid],
	@FirstName = [FirstName],
	@LastName = [LastName]
FROM [dbo].[Users] WITH(NOLOCK)
WHERE [Guid] = @UserGuid;

SELECT @SmsText = [value] FROM [dbo].[Settings] WHERE [Key] = 8 AND [UserGuid] = @MainAdminGuid; --UserAccountSmsText

SELECT 
	@PrivateNumberGuid = CASE LEN([Value]) WHEN  0 THEN @EmptyGuid
																				 WHEN 36 THEN CAST([Value] AS UNIQUEIDENTIFIER)
																				 ELSE @EmptyGuid END
FROM [dbo].[UserSettings] WHERE [UserGuid] = @ParentGuid AND [Key] = 5--DefaultNumber

IF(@@ROWCOUNT=0)
	RETURN;

SET @SmsText = REPLACE(@SmsText,'#firstname#',@FirstName);
SET @SmsText = REPLACE(@SmsText,'#lastname#',@LastName);
SET @SmsText = REPLACE(@SmsText,'#username#',@UserName);
SET @SmsText = REPLACE(@SmsText,'#password#',@Password);
SET @SmsText = REPLACE(@SmsText,'#domain#',@domain);

IF(ISNULL(@SmsText,'') = '' OR @PrivateNumberGuid = @EmptyGuid)
	RETURN;

IF( dbo.GetNumberOperator(@Mobile) =  0)
	RETURN;

SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SmsText);
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

SELECT 
	@SmsSenderAgentReference = [SmsSenderAgentReference]
FROM 
	[dbo].[PrivateNumbers] number INNER JOIN
	[dbo].[SmsSenderAgents] agent ON number.[SmsSenderAgentGuid] = agent.[Guid]
WHERE
	number.[Guid] = @PrivateNumberGuid;

INSERT INTO dbo.ScheduledSmses
				([Guid] ,
					[PrivateNumberGuid] ,
					[SmsText] ,
					[PresentType] ,
					[Encoding] ,
					[SmsLen] ,
					[TypeSend] ,
					[DateTimeFuture] ,
					[CreateDate] ,
					[Status] ,
					[SmsSenderAgentReference],
					[IsDeleted] ,
					[UserGuid]
				)
			SELECT
					@NewGuid,
					@PrivateNumberGuid,
					@SmsText,
					1,--Normal
					[dbo].[HasUniCodeCharacter](@SmsText),
					[dbo].[GetSmsCount](@SmsText),
					1,--SendSms
					GETDATE(),
					GETDATE(),
					1,--Stored
					@SmsSenderAgentReference,
					0,
					@ParentGuid;

INSERT INTO [dbo].[Recipients]
	      ([Guid] ,
	        [Mobile] ,
	        [Operator] ,
	        [IsBlackList] ,
	        [ScheduledSmsGuid])
				SELECT
					NEWID(),
					@Mobile,
					dbo.GetNumberOperator(@Mobile),
					0,
					@NewGuid;
GO
/****** Object:  StoredProcedure [dbo].[Users_Update]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_Update]
   @Guid UNIQUEIDENTIFIER
  ,@FirstName NVARCHAR(50)
  ,@LastName NVARCHAR(50)
  ,@BirthDate DATETIME
  ,@Email NVARCHAR(50)
  ,@ZoneGuid UNIQUEIDENTIFIER
  ,@Address NVARCHAR(MAX)
  ,@Mobile NVARCHAR(50)
  ,@Phone NVARCHAR(50)
  ,@FaxNumber NVARCHAR(50)
  AS

UPDATE Users
SET 
   [FirstName] = @FirstName
  ,[LastName] = @LastName
  ,[BirthDate] = @BirthDate
  ,[Email] = @Email
  ,[ZoneGuid] = @ZoneGuid
  ,[Address] = @Address
  ,[Mobile] = @Mobile
  ,[Phone] = @Phone
  ,[FaxNumber] = @FaxNumber
 
 WHERE [Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateCreditAndGroupPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_UpdateCreditAndGroupPrice]
	@UserGuid UNIQUEIDENTIFIER,
	@Credit DECIMAL(18,2),
	@SmsCount BIGINT
  AS


DECLARE @ParentGuid UNIQUEIDENTIFIER,
				@EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000',
				@GroupPriceGuid UNIQUEIDENTIFIER;

SELECT 
		@ParentGuid = [ParentGuid]
FROM 
		[Users]
WHERE
		[Guid] = @UserGuid AND
		[IsDeleted] = 0

IF (@ParentGuid = @EmptyGuid)
	SET @ParentGuid = @UserGuid

SELECT @GroupPriceGuid = [Guid] FROM [GroupPrices]
WHERE 
			[UserGuid] = @ParentGuid AND
			@Credit BETWEEN [MinimumMessage] AND [MaximumMessage]

UPDATE
	[dbo].[Users]
SET
	[Credit] = @Credit,
	[PriceGroupGuid] = @GroupPriceGuid
WHERE
	[Guid] = @UserGuid;


GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateDomainGroupPrice]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Users_UpdateDomainGroupPrice]
	@UserGuid UNIQUEIDENTIFIER,
	@DomainGroupPriceGuid UNIQUEIDENTIFIER
  AS

	UPDATE
		[dbo].[Users]
	SET 
		[DomainGroupPriceGuid] = @DomainGroupPriceGuid
	WHERE 
		[Guid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateExpireDate]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_UpdateExpireDate]
	@UserGuid UNIQUEIDENTIFIER,
	@ExpireDate DATETIME
  AS


UPDATE [dbo].[Users] SET [ExpireDate] = @ExpireDate WHERE [Guid] = @UserGuid;
GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateGroupPriceOfUser]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_UpdateGroupPriceOfUser]
	@Guid UNIQUEIDENTIFIER,
	@GroupPriceGuid UNIQUEIDENTIFIER
  AS

	UPDATE [Users] 
	SET [PriceGroupGuid]=@GroupPriceGuid
	WHERE [Guid]=@Guid


GO
/****** Object:  StoredProcedure [dbo].[Users_UpdatePassword]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Users_UpdatePassword]
	@Guid UNIQUEIDENTIFIER,
	@Password NVARCHAR(512)
  AS

UPDATE [dbo].[Users] SET [Password] = @Password WHERE [Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[Users_UpdateProfile]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Users_UpdateProfile]
	@Guid UNIQUEIDENTIFIER,
	@FirstName NVARCHAR(32),
	@LastName NVARCHAR(64),
	@FatherName NVARCHAR(32),
	@NationalCode NCHAR(10),
	@ShCode NVARCHAR(16),
	@Email NVARCHAR(50),
	@Phone NVARCHAR(16),
	@Mobile NVARCHAR(16),
	@FaxNumber NVARCHAR(16),
	@Address NVARCHAR(MAX),
	@ZoneGuid UNIQUEIDENTIFIER,
	@Type INT,
	@BirthDate DATETIME,
	@ZipCode NCHAR(10),
	@CompanyName NVARCHAR(64),
	@CompanyNationalId NVARCHAR(32),
	@EconomicCode NVARCHAR(32),
	@CompanyCEOName NVARCHAR(32),
	@CompanyCEONationalCode NCHAR(10),
	@CompanyCEOMobile NVARCHAR(16),
	@CompanyPhone NVARCHAR(16),
	@CompanyZipCode NCHAR(10),
	@CompanyAddress NVARCHAR(255)
  AS

	UPDATE [dbo].[Users]
  SET 
		[FirstName] = @FirstName,
		[LastName] = @LastName,
		[FatherName] = @FatherName,
		[NationalCode] = @NationalCode,
		[ShCode] = @ShCode,
		[Email] = @Email,
		[Phone] = @Phone,
		[Mobile] = @Mobile,
		[FaxNumber] = @FaxNumber,
		[Address] = @Address,
		[ZoneGuid] = @ZoneGuid,
		[BirthDate] = @BirthDate,
		[ZipCode] = @ZipCode,
		[Type] = @Type,
		[CompanyName] = @CompanyName,
		[CompanyNationalId] = @CompanyNationalId,
		[EconomicCode] = @EconomicCode,
		[CompanyCEOName] = @CompanyCEOName,
		[CompanyCEONationalCode] = @CompanyCEONationalCode,
		[CompanyCEOMobile] = @CompanyCEOMobile,
		[CompanyPhone] = @CompanyPhone,
		[CompanyZipCode] = @CompanyZipCode,
		[CompanyAddress] = @CompanyAddress
	WHERE
		[Guid] = @Guid


GO
/****** Object:  StoredProcedure [dbo].[UserSettings_CheckCreditNotification]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserSettings_CheckCreditNotification]
	@UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @CreditWarning DECIMAL(18,2) = 999999999999999;
DECLARE @CreditWarningStatus INT;
DECLARE @CreditGuid UNIQUEIDENTIFIER;
DECLARE @UserCredit DECIMAL(18,2);
DECLARE @UserMobile NVARCHAR(16);
DECLARE @PrivateNumberGuid UNIQUEIDENTIFIER;
DECLARE @NewGuid UNIQUEIDENTIFIER = NEWID();
DECLARE @SmsText NVARCHAR(MAX) = '';
DECLARE @SmsLen INT;
DECLARE @Encoding INT;
DECLARE @Date DATETIME = GETDATE();
DECLARE @EmptyGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SELECT
	@UserCredit = [Credit],
	@UserMobile = [Mobile]
FROM
	[dbo].[Users] WHERE [Guid] = @UserGuid;

SELECT
	@CreditGuid = [Guid],
	@CreditWarning = CAST([Value] AS DECIMAL(18,2)),
	@CreditWarningStatus = ISNULL([Status],0)
FROM
	[dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 1--CreditWarning

IF(@@ROWCOUNT = 0)
	RETURN;
IF(@CreditWarningStatus = 1 AND @UserCredit < @CreditWarning)
	RETURN;
IF(@CreditWarningStatus = 0 AND @UserCredit > @CreditWarning)
	RETURN;
ELSE IF(@CreditWarningStatus = 1 AND @UserCredit > @CreditWarning)
 SET @CreditWarningStatus = 0;
ELSE
 SET @CreditWarningStatus = 1;

SELECT 
	@PrivateNumberGuid = CASE LEN([Value]) WHEN  0 THEN @EmptyGuid
																				 WHEN 36 THEN CAST([Value] AS UNIQUEIDENTIFIER)
																				 ELSE @EmptyGuid END
	FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 5--DefaultNumber

IF(@@ROWCOUNT=0)
	RETURN;

DECLARE @MainAdminGuid UNIQUEIDENTIFIER;
EXEC @MainAdminGuid = [dbo].[udfGetFirstParentMainAdmin] @UserGuid = @UserGuid;

SELECT @SmsText = [Value] FROM [dbo].[Settings] WHERE [Key] = 6 AND [UserGuid] = @MainAdminGuid; --LowCreditSmsText

IF(ISNULL(@SmsText,'') = '')
	RETURN;

SELECT @Encoding = [dbo].[HasUniCodeCharacter](@SmsText);
SELECT @SmsLen = [dbo].[GetSmsCount](@SmsText);

IF(@UserCredit < @CreditWarning AND ISNULL(@UserMobile,'') != '')
BEGIN
	EXEC [dbo].[ScheduledSmses_InsertSms]
			@Guid = @NewGuid,
	    @PrivateNumberGuid = @PrivateNumberGuid,
	    @Reciever = @UserMobile,
	    @SmsText = @SmsText,
	    @SmsLen = @SmsLen,
	    @PresentType = 1, -- Normal
	    @Encoding = @Encoding,
	    @TypeSend = 1, -- SendSms
	    @Status = 1, -- Stored
	    @DateTimeFuture = @Date,
	    @UserGuid = @UserGuid,
	    @IPAddress = N'',
	    @Browser = N'';

	UPDATE [UserSettings] SET [Status] = @CreditWarningStatus WHERE [Guid] = @CreditGuid;
END




GO
/****** Object:  StoredProcedure [dbo].[UserSettings_GetSettingValue]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UserSettings_GetSettingValue]
  @UserGuid UNIQUEIDENTIFIER,
  @Key INT
  AS


SELECT  
	  [Value]
FROM  
		[dbo].[UserSettings]
WHERE 
		[UserGuid] = @UserGuid AND
		[Key] = @Key


GO
/****** Object:  StoredProcedure [dbo].[UserSettings_GetUserExpired]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UserSettings_GetUserExpired]
  AS

WITH expTemp AS
(
		SELECT 
			userSetting.[UserGuid],
			userSetting.[Value] AS [Expiration]
		FROM 
			 (SELECT 
					[UserGuid],
					[Key],
					[Value]
				FROM
					[UserSettings]
				WHERE
					[Key]= 2  AND--ExpireWarning
					CONVERT(INT,[Value])>0) userSetting INNER JOIN
																	[Users] users ON [userSetting].[UserGuid]=[Users].[Guid] AND
																	CONVERT(VARCHAR(10),DATEADD(DAY,CONVERT(INT,[Value]),GETDATE()),111)=CONVERT(VARCHAR(10),[ExpireDate],111) 
)

SELECT * FROM expTemp


GO
/****** Object:  StoredProcedure [dbo].[UserSettings_GetUserSettings]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[UserSettings_GetUserSettings]
	@UserGuid UNIQUEIDENTIFIER
  AS

	Select 
				*
	From
				[dbo].[UserSettings]
	Where
				[UserGuid] = @UserGuid


GO
/****** Object:  StoredProcedure [dbo].[UserSettings_GetUserShortcutForLoad]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UserSettings_GetUserShortcutForLoad]
  @UserGuid UNIQUEIDENTIFIER
  AS


DECLARE @Value NVARCHAR(MAX);

SELECT @Value = [Value] FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 4; --Shortcut

SELECT
	[Item] AS [Guid]
	INTO #ServiceGuid
FROM
	[dbo].[SplitString](@Value,',')
OPTION (MAXRECURSION 0)

SELECT * FROM [dbo].[Services] WHERE [Guid] IN (SELECT [Guid] FROM #ServiceGuid)

DROP TABLE #ServiceGuid;


GO
/****** Object:  StoredProcedure [dbo].[UserSettings_GetUserWebAPIPassword]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserSettings_GetUserWebAPIPassword]
	@UserName NVARCHAR(32)
  AS


DECLARE @UserGuid UNIQUEIDENTIFIER;
DECLARE @AccountPassword NVARCHAR(512);
DECLARE @APIPassword NVARCHAR(512) = '';

SELECT @UserGuid = [Guid],@AccountPassword = [Password] FROM [dbo].[Users] WHERE [UserName] = @UserName;
SELECT @APIPassword = ISNULL([Value],'') FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = 6 --ApiPassword

SELECT 
	@AccountPassword [AccountPassword],
	@APIPassword [APIPassword];
GO
/****** Object:  StoredProcedure [dbo].[UserSettings_InsertSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserSettings_InsertSetting]
  @UserGuid UNIQUEIDENTIFIER ,
  @Settings [UserSetting] READONLY
  AS


DELETE FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid;
	
DECLARE @DefaultNumberGuid UNIQUEIDENTIFIER = '00000000-0000-0000-0000-000000000000';

SELECT @DefaultNumberGuid = CAST([Value] AS UNIQUEIDENTIFIER) FROM @Settings WHERE [Key] = 5--DefaultNumber

INSERT INTO [dbo].[UserSettings]
	      ([Guid],
					[UserGuid],
					[Key],
					[Value],
					[Status])
				SELECT 
					NEWID(),
					@UserGuid,
					[Key],
					[Value],
					[Status]
				FROM
					@Settings;

IF(@DefaultNumberGuid != '00000000-0000-0000-0000-000000000000')
BEGIN
	UPDATE [dbo].[PrivateNumbers] SET [IsDefault] = 0 WHERE [OwnerGuid] = @UserGuid;
	UPDATE [dbo].[PrivateNumbers] SET [IsDefault] = 1 WHERE [Guid] = @DefaultNumberGuid;
END

GO
/****** Object:  StoredProcedure [dbo].[UserSettings_UpdateSetting]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UserSettings_UpdateSetting]
	@UserGuid UNIQUEIDENTIFIER ,
	@Key INT,
	@Value NVARCHAR(MAX)
  AS
 
  DELETE FROM [dbo].[UserSettings] WHERE [UserGuid] = @UserGuid AND [Key] = @Key;

	INSERT INTO [dbo].[UserSettings]
							([Guid],
							 [UserGuid],
							 [Key],
							 [Value])
					VALUES
							(NEWID(),
							 @UserGuid,
							 @Key,
							 @Value);


GO
/****** Object:  StoredProcedure [dbo].[Zones_GetAllChildren]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Zones_GetAllChildren]
	@RootGuid AS UNIQUEIDENTIFIER
  AS


WITH [ChildZone]([Guid], [ParentGuid])
	AS
	(
		SELECT 
					[Guid],
					[ParentGuid]
		FROM 
					[dbo].[Zones]
		WHERE 
					[Guid] = @RootGuid
						
		UNION ALL
			
		SELECT 
					Child.[Guid], 
					Child.[ParentGuid]
		FROM
					[dbo].[Zones] AS Child INNER JOIN 
					[ChildZone] AS Tree ON Child.[ParentGuid] = Tree.[Guid]
	)
	SELECT
				[dbo].[Zones].[Guid]
	FROM
				[dbo].[Zones] INNER JOIN 
				[ChildZone] AS Tree ON [Zones].[Guid] = Tree.[Guid];


GO
/****** Object:  StoredProcedure [dbo].[Zones_GetAllParents]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Zones_GetAllParents]
	@ChildGuid AS UNIQUEIDENTIFIER
  AS


WITH [ParentZones]([Guid], [ParentGuid])
	AS
	(
		SELECT 
					[Guid],
					[ParentGuid]
		FROM
					[dbo].[Zones]
		WHERE 
					[Guid] = @ChildGuid
							
		UNION ALL
				
		SELECT 
					Parent.[Guid],
					Parent.[ParentGuid]
		FROM 
					[dbo].[Zones] Parent INNER JOIN 
					[ParentZones] Tree	ON Parent.[Guid] = Tree.[ParentGuid]
	)
	SELECT 
				[Zones].*
	FROM 
				[dbo].[Zones] INNER JOIN 
				[ParentZones] AS Tree	ON [Zones].[Guid] = Tree.[Guid]


GO
/****** Object:  StoredProcedure [dbo].[Zones_GetZones]    Script Date: 1/1/2021 1:40:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Zones_GetZones]
	@ParentGuid UNIQUEIDENTIFIER
  AS

	SELECT 
		*
	FROM
		[dbo].[Zones]
	WHERE
		[ParentGuid] = @ParentGuid
	ORDER BY
		[Name]
GO
USE [master]
GO
ALTER DATABASE [Arad.SMS.Gateway.DB] SET  READ_WRITE 
GO
