USE [master]
GO
/****** Object:  Database [ACSdatabase]    Script Date: 2014/12/10 18:30:50 ******/
CREATE DATABASE [ACSdatabase]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ACSdatabase_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ACSdatabase_Data.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 10%)
 LOG ON 
( NAME = N'ACSdatabase_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\ACSdatabase_Log.mdf' , SIZE = 5120KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ACSdatabase] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ACSdatabase].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ACSdatabase] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ACSdatabase] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ACSdatabase] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ACSdatabase] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ACSdatabase] SET ARITHABORT OFF 
GO
ALTER DATABASE [ACSdatabase] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ACSdatabase] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [ACSdatabase] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ACSdatabase] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ACSdatabase] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ACSdatabase] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ACSdatabase] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ACSdatabase] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ACSdatabase] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ACSdatabase] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ACSdatabase] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ACSdatabase] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ACSdatabase] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ACSdatabase] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ACSdatabase] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ACSdatabase] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ACSdatabase] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ACSdatabase] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ACSdatabase] SET RECOVERY FULL 
GO
ALTER DATABASE [ACSdatabase] SET  MULTI_USER 
GO
ALTER DATABASE [ACSdatabase] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ACSdatabase] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ACSdatabase] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ACSdatabase] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ACSdatabase', N'ON'
GO
USE [ACSdatabase]
GO
/****** Object:  Table [dbo].[t_access_detail]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_access_detail](
	[ACCESS_DETAIL_ID] [int] IDENTITY(1,1) NOT NULL,
	[ACCESS_ID] [int] NULL,
	[VALUE_ID] [int] NULL,
	[TYPE] [nvarchar](20) NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[ACCESS_NAME] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ACCESS_DETAIL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_alarm_record]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_alarm_record](
	[ALARM_ID] [int] NOT NULL,
	[ALARM_TIME] [datetime] NULL,
	[CONTROL_ID] [int] NULL,
	[DOOR_ID] [int] NULL,
	[ALARM_TYPE] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[ALARM_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_control]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_control](
	[CONTROL_ID] [int] IDENTITY(1,1) NOT NULL,
	[NET_ID] [int] NULL,
	[CODE] [nvarchar](50) NULL,
	[CONTROL_NAME] [nvarchar](50) NULL,
	[ADDRESS] [nvarchar](50) NULL,
	[SERIAL] [nvarchar](50) NULL,
	[TYPE_ID] [int] NULL,
	[ENABLE] [bit] NULL,
	[DURESS_PIN] [nvarchar](50) NULL,
	[AREA_ID] [int] NULL,
	[WWW] [nvarchar](255) NULL,
	[IP] [nvarchar](255) NULL,
	[PORT] [int] NULL,
	[BY_IP] [bit] NULL,
	[LOCK_EACH] [int] NULL,
	[FIRE_TIME] [int] NULL,
	[ALARM_TIME] [int] NULL,
	[ALARM_MAST] [int] NULL,
	[CONTROL_GROUP] [int] NULL,
	[TIME_TAMP] [datetime] NULL,
	[LOCAL_SETUP] [nvarchar](50) NULL,
	[MAP_ID] [int] NULL,
	[X_POINT] [int] NULL,
	[Y_POINT] [int] NULL,
	[MAP_VISIBLE] [bit] NULL,
	[ONLINE] [bit] NULL,
	[DOOR_OPEN] [int] NULL,
	[FUNCTION_MAST] [int] NULL,
	[OUTPUT] [nvarchar](50) NULL,
	[INPUT] [nvarchar](50) NULL,
	[IS16] [bit] NULL,
	[OPEN_TIME] [int] NULL,
	[CLOSE_TIME] [int] NULL,
	[FLOOR_DELAY] [int] NULL,
	[OTHRE_FUNCTION] [int] NULL,
	[MX_OUT_PORT] [int] NULL,
	[AES_PIN] [nvarchar](50) NULL,
	[USE_AES] [bit] NULL,
	[IC_ADDRESS] [int] NULL,
	[IC_IS_ST] [bit] NULL,
	[TYPE] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[CONTROL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_dept]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_dept](
	[DEPT_ID] [int] IDENTITY(1,1) NOT NULL,
	[DEPT_NAME] [nvarchar](50) NULL,
	[FATHER_DEPT_ID] [int] NULL,
	[DEPT_CODE] [nvarchar](50) NULL,
	[NOTE] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[DEPT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_door]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_door](
	[DOOR_ID] [int] IDENTITY(1,1) NOT NULL,
	[CONTROL_ID] [int] NULL,
	[DOOR_NAME] [nvarchar](50) NULL,
	[DOOR_SERIAL] [nvarchar](50) NULL,
	[DOOR_ADDRESS] [int] NULL,
	[OPEN_TIME] [int] NULL,
	[CLOSE_OUT_TIME] [int] NULL,
	[DOOR_ALERM2_LONG] [bit] NULL,
	[PASS_BACK] [bit] NULL,
	[DOOR_ENABLE] [bit] NULL,
	[DOOR_VISIBLE] [bit] NULL,
	[MAP_ID] [int] NULL,
	[X_POINT] [int] NULL,
	[Y_POINT] [int] NULL,
	[MAP_VISIBLE] [bit] NULL,
	[IS_KQ] [bit] NULL,
	[IS_REPAST] [bit] NULL,
	[ALARM_MAST] [int] NULL,
	[ALARM_TIME] [int] NULL,
	[IS_MAIN_DOOR] [bit] NULL,
	[READER_IN] [nvarchar](50) NULL,
	[READER_OUT] [nvarchar](50) NULL,
	[M_CARDS_OPEN] [int] NULL,
	[M_CARDS_OPEN_IN_OUT] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DOOR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_door_time]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_door_time](
	[DOOR_TIME_ID] [int] IDENTITY(1,1) NOT NULL,
	[DOOR_ID] [int] NULL,
	[DOOR_TIME_NAME] [nvarchar](255) NULL,
	[ENABLE] [nvarchar](25) NULL,
	[START_TIME] [nvarchar](255) NULL,
	[END_TIME] [nvarchar](255) NULL,
	[MONDAY] [bit] NULL,
	[TUESDAY] [bit] NULL,
	[WEDNESDAY] [bit] NULL,
	[THURSDAY] [bit] NULL,
	[FRIDAY] [bit] NULL,
	[SATURDAY] [bit] NULL,
	[SUNDAY] [bit] NULL,
	[HOLIDAY] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[DOOR_TIME_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_employee]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_employee](
	[EMPLOYEE_ID] [int] IDENTITY(1,1) NOT NULL,
	[EMPLOYEE_NAME] [nvarchar](50) NULL,
	[EMPLOYEE_CODE] [nvarchar](24) NULL,
	[ENGLISH_NAME] [nvarchar](50) NULL,
	[CARD_NO] [nvarchar](11) NULL,
	[PIN] [int] NULL,
	[EMP_ENABLE] [bit] NULL,
	[SEX] [bit] NULL,
	[BIRTHDAY] [datetime] NULL,
	[PERSON_CODE] [nvarchar](18) NULL,
	[HOME] [nvarchar](60) NULL,
	[PHONE] [nvarchar](50) NULL,
	[EMAIL] [nvarchar](50) NULL,
	[CAR] [nvarchar](50) NULL,
	[JOB_ID] [int] NULL,
	[DEPT_ID] [int] NULL,
	[PHOTO1] [nvarchar](255) NULL,
	[PHOTO2] [nvarchar](255) NULL,
	[REG_DATE] [datetime] NULL,
	[END_DATE] [datetime] NULL,
	[DELETED] [bit] NULL,
	[LEAVE] [bit] NULL,
	[LEAVE_DATE] [datetime] NULL,
	[BE_KQ] [bit] NULL,
	[PSWD] [nvarchar](20) NULL,
	[MAP_ID] [int] NULL,
	[X_POINT] [int] NULL,
	[Y_POINT] [int] NULL,
	[MAP_VISIBLE] [bit] NULL,
	[OWNER_DOOR] [int] NULL,
	[LAST_EVENT_ID] [int] NULL,
	[EVENT_2EMO_ID] [int] NULL,
	[STATUS] [int] NULL,
	[TIME_STAMP] [datetime] NULL,
	[SHOW_CARD_NO] [bit] NULL,
	[NOTE1] [nvarchar](50) NULL,
	[NOTE2] [nvarchar](50) NULL,
	[NOTE3] [nvarchar](50) NULL,
	[NOTE4] [nvarchar](50) NULL,
	[NOTE5] [nvarchar](50) NULL,
	[TIME_STAMPX] [datetime] NULL,
	[IS_BLACK_CARD] [bit] NULL,
	[ASC_STRING] [nvarchar](255) NULL,
	[PHOTO] [varbinary](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[EMPLOYEE_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_event_record]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_event_record](
	[EVENT_ID] [int] IDENTITY(1,1) NOT NULL,
	[EMPLOYEE_ID] [int] NULL,
	[EVENT_TIME] [datetime] NULL,
	[CONTROL_ID] [int] NULL,
	[DOOR_ID] [int] NULL,
	[EVENT_TYPE] [int] NULL,
	[MODIFY] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[EVENT_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_event_type]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_event_type](
	[EVENT_ID] [int] NULL,
	[EVENT_NAME] [varchar](25) NULL,
	[LEVEL] [int] NULL,
	[VISIBLE] [int] NULL,
	[AFFIRM] [int] NULL,
	[ALARM] [int] NULL,
	[EVENT0] [int] NULL,
	[EVENT1] [int] NULL,
	[EVENT2] [int] NULL,
	[EVENT3] [int] NULL,
	[EVENT4] [int] NULL,
	[EVENT5] [int] NULL,
	[EVENT_TYPE_ID] [int] NULL,
	[EVENT_TYPE_NAME] [nvarchar](50) NULL,
	[FOREGROUND_COLOR] [int] NULL,
	[BACKGROUND_COLOR] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_holiday]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_holiday](
	[HOLIDAY_ID] [int] IDENTITY(1,1) NOT NULL,
	[HOLIDAY_NAME] [nvarchar](50) NULL,
	[START_TIME] [datetime] NULL,
	[END_TIME] [datetime] NULL,
	[HOLIDAY_NOTE] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[HOLIDAY_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_job]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_job](
	[JOB_ID] [int] IDENTITY(1,1) NOT NULL,
	[JOB_NAME] [nvarchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[JOB_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[t_privilege]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_privilege](
	[PRIVILEGE_ID] [int] NULL,
	[PRIVILEGE_MASTER] [varchar](255) NULL,
	[PRIVILEGE_MASTER_VALUE] [varchar](255) NULL,
	[PRIVILEGE_ACCESS] [varchar](255) NULL,
	[PRIVILEGE_ACCESS_VALUE] [varchar](255) NULL,
	[PRIVILEGE_OPERATION] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_role]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_role](
	[ROLE_ID] [int] NULL,
	[ROLE_NAME] [varchar](25) NULL,
	[ROLE_DESC] [varchar](25) NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[MODIFY_USER_ID] [int] NULL,
	[MODIFY_DATE] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_sys_application]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_sys_application](
	[APPLICATION_ID] [int] NULL,
	[APPLICATION_CODE] [varchar](25) NULL,
	[APPLICATION_NAME] [varchar](25) NULL,
	[APPLICATION_DESC] [varchar](25) NULL,
	[SHOW_IN_MENU] [varchar](25) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_sys_button]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_sys_button](
	[BTN_ID] [int] NULL,
	[BTN_NAME] [varchar](25) NULL,
	[BTN_NO] [varchar](25) NULL,
	[BTN_CLASS] [varchar](25) NULL,
	[BTN_ICON] [varchar](25) NULL,
	[BTN_SCRIPT] [varchar](25) NULL,
	[INIT_STATUS] [varchar](25) NULL,
	[SEQ_NO] [int] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_sys_menu]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_sys_menu](
	[MENU_ID] [int] NULL,
	[MENU_NO] [varchar](25) NULL,
	[APPLICATION_CODE] [varchar](25) NULL,
	[MENU_PARENT_NO] [varchar](25) NULL,
	[MENU_NAME] [varchar](25) NULL,
	[MENU_ICON] [varchar](25) NULL,
	[IS_VISIBLE] [varchar](25) NULL,
	[IS_LEAF] [varchar](25) NULL,
	[MENU_URL] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_system_log]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_system_log](
	[LOG_ID] [int] NULL,
	[LOG_DATE_TIME] [datetime] NULL,
	[USER_CODE] [int] NULL,
	[LOG_EVENT] [varchar](25) NULL,
	[MSG] [varchar](25) NULL,
	[RESULT] [varchar](25) NULL,
	[COMPUTER] [varchar](25) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_user]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[t_user](
	[USER_ID] [int] NULL,
	[USER_NAME] [varchar](255) NULL,
	[USER_DESC] [varchar](255) NULL,
	[PSWD] [varchar](255) NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[MODIFY_USER_ID] [int] NULL,
	[MODIFY_DATE] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[t_user_role]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[t_user_role](
	[USER_ROLE_ID] [int] NULL,
	[USER_ID] [int] NULL,
	[ROLE_ID] [int] NOT NULL,
	[CREATE_USER_ID] [int] NULL,
	[CREATE_DATE] [datetime] NULL,
	[MODIFY_USER_ID] [int] NULL,
	[MODIFY_DATE] [datetime] NULL
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[t_access_detail_view]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[t_access_detail_view]
AS 
SELECT
[t_access_detail].[ACCESS_DETAIL_ID] AS [ACCESS_DETAIL_ID],
[t_access_detail].[ACCESS_ID] AS [ACCESS_ID],
[t_access_detail].[VALUE_ID] AS [VALUE_ID],
[t_access_detail].[TYPE] AS [TYPE],
[t_access_detail].[CREATE_USER_ID] AS [CREATE_USER_ID],
[t_access_detail].[CREATE_DATE] AS [CREATE_DATE],
[t_access_detail].[ACCESS_NAME] AS [ACCESS_NAME],
[t_control].[CONTROL_NAME] AS [CONTROL_NAME],
[t_door].[CONTROL_ID] AS [CONTROL_ID],
[t_door].[DOOR_NAME] AS [DOOR_NAME],
[t_door_time].[DOOR_ID] AS [DOOR_ID],
[t_door_time].[DOOR_TIME_NAME] AS [DOOR_TIME_NAME],
[t_door_time].[START_TIME] AS [START_TIME],
[t_door_time].[END_TIME] AS [END_TIME]
FROM
[dbo].[t_access_detail]

LEFT JOIN
[dbo].[t_door_time]
ON
[dbo].[t_door_time].[DOOR_TIME_ID] = [dbo].[t_access_detail].[VALUE_ID]
LEFT JOIN
[dbo].[t_door]
ON
[dbo].[t_door].[DOOR_ID] = [dbo].[t_door_time].[DOOR_ID]
LEFT JOIN
[dbo].[t_control]
ON
[dbo].[t_control].[CONTROL_ID] = [dbo].[t_door].[CONTROL_ID];
GO
/****** Object:  View [dbo].[t_alarm_record_view]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[t_alarm_record_view]
AS 
SELECT
[t_alarm_record].[ALARM_ID] AS [ALARM_ID],
[t_alarm_record].[ALARM_TIME] AS [ALARM_TIME],
[t_alarm_record].[CONTROL_ID] AS [CONTROL_ID],
[t_alarm_record].[DOOR_ID] AS [DOOR_ID],
[t_alarm_record].[ALARM_TYPE] AS [ALARM_TYPE],
[t_control].[CONTROL_NAME] AS [CONTROL_NAME],
[t_door].[DOOR_NAME] AS [DOOR_NAME]
FROM
[dbo].[t_alarm_record]

LEFT JOIN
[dbo].[t_control]
ON
[dbo].[t_alarm_record].[CONTROL_ID] = [dbo].[t_control].[CONTROL_ID]
LEFT JOIN
[dbo].[t_door]
ON
[dbo].[t_alarm_record].[DOOR_ID] = [dbo].[t_door].[DOOR_ID];
GO
/****** Object:  View [dbo].[t_door_time_view]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[t_door_time_view]
AS 
SELECT
[t_door_time].[DOOR_TIME_ID] AS [DOOR_TIME_ID],
[t_door_time].[DOOR_ID] AS [DOOR_ID],
[t_door_time].[DOOR_TIME_NAME] AS [DOOR_TIME_NAME],
[t_door_time].[ENABLE] AS [ENABLE],
[t_door_time].[START_TIME] AS [START_TIME],
[t_door_time].[END_TIME] AS [END_TIME],
[t_door].[DOOR_NAME] AS [DOOR_NAME],
[t_control].[CONTROL_ID] AS [CONTROL_ID],
[t_control].[CONTROL_NAME] AS [CONTROL_NAME]
FROM
[dbo].[t_door_time]

LEFT JOIN
[dbo].[t_door]
ON
[dbo].[t_door_time].[DOOR_ID] = [dbo].[t_door].[DOOR_ID]
LEFT JOIN
[dbo].[t_control]
ON
[dbo].[t_door].[CONTROL_ID] = [dbo].[t_control].[CONTROL_ID];
GO
/****** Object:  View [dbo].[t_employee_view]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[t_employee_view]
AS 
SELECT
[t_employee].[EMPLOYEE_ID] AS [EMPLOYEE_ID],
[t_employee].[EMPLOYEE_NAME] AS [EMPLOYEE_NAME],
[t_employee].[EMPLOYEE_CODE] AS [EMPLOYEE_CODE],
[t_employee].[CARD_NO] AS [CARD_NO],
[t_employee].[EMP_ENABLE] AS [EMP_ENABLE],
[t_employee].[SEX] AS [SEX],
[t_employee].[PERSON_CODE] AS [PERSON_CODE],
[t_employee].[PHONE] AS [PHONE],
[t_employee].[JOB_ID] AS [JOB_ID],
[t_employee].[DEPT_ID] AS [DEPT_ID],
[t_employee].[PHOTO1] AS [PHOTO1],
[t_employee].[PHOTO2] AS [PHOTO2],
[t_employee].[END_DATE] AS [END_DATE],
[t_employee].[LEAVE] AS [LEAVE],
[t_employee].[LAST_EVENT_ID] AS [LAST_EVENT_ID],
[t_job].[JOB_NAME] AS [JOB_NAME],
[t_dept].[DEPT_NAME] AS [DEPT_NAME]
FROM
[dbo].[t_employee]

LEFT JOIN
[dbo].[t_job]
ON
[dbo].[t_employee].[JOB_ID] = [dbo].[t_job].[JOB_ID]
LEFT JOIN
[dbo].[t_dept]
ON
[dbo].[t_employee].[DEPT_ID] = [dbo].[t_dept].[DEPT_ID];
GO
/****** Object:  View [dbo].[t_event_record_view]    Script Date: 2014/12/10 18:30:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[t_event_record_view]
AS 
SELECT
[t_event_record].[EVENT_ID] AS [EVENT_ID],
[t_event_record].[EMPLOYEE_ID] AS [EMPLOYEE_ID],
[t_event_record].[EVENT_TIME] AS [EVENT_TIME],
[t_event_record].[CONTROL_ID] AS [CONTROL_ID],
[t_event_record].[DOOR_ID] AS [DOOR_ID],
[t_event_record].[EVENT_TYPE] AS [EVENT_TYPE],
[t_event_record].[MODIFY] AS [MODIFY],
[t_employee].[EMPLOYEE_CODE] AS [EMPLOYEE_CODE],
[t_employee].[EMPLOYEE_NAME] AS [EMPLOYEE_NAME],
[t_employee].[ENGLISH_NAME] AS [ENGLISH_NAME],
[t_employee].[CARD_NO] AS [CARD_NO],
[t_employee].[EMP_ENABLE] AS [EMP_ENABLE],
[t_employee].[SEX] AS [SEX],
[t_job].[JOB_NAME] AS [JOB_NAME],
[t_dept].[DEPT_NAME] AS [DEPT_NAME],
[t_employee].[LEAVE] AS [LEAVE],
[t_employee].[LEAVE_DATE] AS [LEAVE_DATE],
[t_employee].[BE_KQ] AS [BE_KQ],
[t_employee].[REG_DATE] AS [REG_DATE],
[t_employee].[BIRTHDAY] AS [BIRTHDAY],
[t_employee].[PERSON_CODE] AS [PERSON_CODE],
[t_employee].[HOME] AS [HOME],
[t_employee].[PHONE] AS [PHONE],
[t_employee].[EMAIL] AS [EMAIL],
[t_employee].[PHOTO1] AS [PHOTO1],
[t_employee].[PHOTO2] AS [PHOTO2],
[t_employee].[CAR] AS [CAR],
[t_employee].[NOTE1] AS [NOTE1],
[t_employee].[NOTE2] AS [NOTE2],
[t_employee].[NOTE3] AS [NOTE3],
[t_employee].[NOTE4] AS [NOTE4],
[t_door].[DOOR_NAME] AS [DOOR_NAME],
[t_control].[CONTROL_NAME] AS [CONTROL_NAME]
FROM
[dbo].[t_event_record]
JOIN
[dbo].[t_employee]
ON
[dbo].[t_employee].[EMPLOYEE_ID] = [dbo].[t_event_record].[EMPLOYEE_ID]
JOIN
[dbo].[t_dept]
ON
[dbo].[t_employee].[DEPT_ID] = [dbo].[t_dept].[DEPT_ID]
JOIN
[dbo].[t_job]
ON
[dbo].[t_employee].[JOB_ID] = [dbo].[t_job].[JOB_ID]
JOIN
[dbo].[t_door]
ON
[dbo].[t_event_record].[DOOR_ID] = [dbo].[t_door].[DOOR_ID]
JOIN
[dbo].[t_control]
ON
[dbo].[t_event_record].[CONTROL_ID] = [dbo].[t_control].[CONTROL_ID];
GO
SET IDENTITY_INSERT [dbo].[t_access_detail] ON 

INSERT [dbo].[t_access_detail] ([ACCESS_DETAIL_ID], [ACCESS_ID], [VALUE_ID], [TYPE], [CREATE_USER_ID], [CREATE_DATE], [ACCESS_NAME]) VALUES (53, 0, 53, N'ACCESS', 1, CAST(0x0000A3ED010B1AE8 AS DateTime), N'门禁权限1')
INSERT [dbo].[t_access_detail] ([ACCESS_DETAIL_ID], [ACCESS_ID], [VALUE_ID], [TYPE], [CREATE_USER_ID], [CREATE_DATE], [ACCESS_NAME]) VALUES (54, 0, 54, N'ACCESS', 1, CAST(0x0000A3ED010B4C20 AS DateTime), N'门禁权限2')
INSERT [dbo].[t_access_detail] ([ACCESS_DETAIL_ID], [ACCESS_ID], [VALUE_ID], [TYPE], [CREATE_USER_ID], [CREATE_DATE], [ACCESS_NAME]) VALUES (93, 0, 93, N'ACCESS', 1, CAST(0x0000A3F50111FD68 AS DateTime), N'门禁权限4')
INSERT [dbo].[t_access_detail] ([ACCESS_DETAIL_ID], [ACCESS_ID], [VALUE_ID], [TYPE], [CREATE_USER_ID], [CREATE_DATE], [ACCESS_NAME]) VALUES (98, 0, 98, N'ACCESS', 1, CAST(0x0000A3F700C2CE50 AS DateTime), N'门禁权限3')
INSERT [dbo].[t_access_detail] ([ACCESS_DETAIL_ID], [ACCESS_ID], [VALUE_ID], [TYPE], [CREATE_USER_ID], [CREATE_DATE], [ACCESS_NAME]) VALUES (103, 53, 93, N'ACCESS', 1, CAST(0x0000A3F50111FD68 AS DateTime), N'门禁权限4')
SET IDENTITY_INSERT [dbo].[t_access_detail] OFF
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (1, CAST(0x0000A3D000B786D0 AS DateTime), 1, 1, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (2, CAST(0x0000A3D000B786D0 AS DateTime), 1, 2, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (3, CAST(0x0000A3D000B786D0 AS DateTime), 1, 3, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (4, CAST(0x0000A3D000B786D0 AS DateTime), 1, 4, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (5, CAST(0x0000A3EF00B786D0 AS DateTime), 1, 1, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (6, CAST(0x0000A3EF00B786D0 AS DateTime), 1, 2, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (7, CAST(0x0000A3EF00B786D0 AS DateTime), 1, 3, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (8, CAST(0x0000A3EF00B786D0 AS DateTime), 1, 4, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (9, CAST(0x0000A3F600B786D0 AS DateTime), 1, 1, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (10, CAST(0x0000A3F600B786D0 AS DateTime), 1, 2, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (11, CAST(0x0000A3F600B786D0 AS DateTime), 1, 3, N'Broken')
INSERT [dbo].[t_alarm_record] ([ALARM_ID], [ALARM_TIME], [CONTROL_ID], [DOOR_ID], [ALARM_TYPE]) VALUES (12, CAST(0x0000A3F600B786D0 AS DateTime), 1, 4, N'Broken')
SET IDENTITY_INSERT [dbo].[t_control] ON 

INSERT [dbo].[t_control] ([CONTROL_ID], [NET_ID], [CODE], [CONTROL_NAME], [ADDRESS], [SERIAL], [TYPE_ID], [ENABLE], [DURESS_PIN], [AREA_ID], [WWW], [IP], [PORT], [BY_IP], [LOCK_EACH], [FIRE_TIME], [ALARM_TIME], [ALARM_MAST], [CONTROL_GROUP], [TIME_TAMP], [LOCAL_SETUP], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [ONLINE], [DOOR_OPEN], [FUNCTION_MAST], [OUTPUT], [INPUT], [IS16], [OPEN_TIME], [CLOSE_TIME], [FLOOR_DELAY], [OTHRE_FUNCTION], [MX_OUT_PORT], [AES_PIN], [USE_AES], [IC_ADDRESS], [IC_IS_ST], [TYPE]) VALUES (6, 0, NULL, N'测试控制器', NULL, NULL, 1, 0, NULL, 0, NULL, N'192.168.0.149', 0, 0, 0, 0, 0, 0, 0, CAST(0x0000A2A600000000 AS DateTime), NULL, 0, 0, 0, 0, 1, 0, 0, NULL, NULL, 0, 0, 0, 0, 0, 0, NULL, 0, 0, 0, NULL)
SET IDENTITY_INSERT [dbo].[t_control] OFF
SET IDENTITY_INSERT [dbo].[t_dept] ON 

INSERT [dbo].[t_dept] ([DEPT_ID], [DEPT_NAME], [FATHER_DEPT_ID], [DEPT_CODE], [NOTE]) VALUES (1, N'总公司', NULL, N'111', NULL)
INSERT [dbo].[t_dept] ([DEPT_ID], [DEPT_NAME], [FATHER_DEPT_ID], [DEPT_CODE], [NOTE]) VALUES (2, N'销售部', 1, N'123', N'')
INSERT [dbo].[t_dept] ([DEPT_ID], [DEPT_NAME], [FATHER_DEPT_ID], [DEPT_CODE], [NOTE]) VALUES (3, N'采购部', 1, N'1234', N'')
INSERT [dbo].[t_dept] ([DEPT_ID], [DEPT_NAME], [FATHER_DEPT_ID], [DEPT_CODE], [NOTE]) VALUES (4, N'新事业部', 1, N'55544', N'')
SET IDENTITY_INSERT [dbo].[t_dept] OFF
SET IDENTITY_INSERT [dbo].[t_door] ON 

INSERT [dbo].[t_door] ([DOOR_ID], [CONTROL_ID], [DOOR_NAME], [DOOR_SERIAL], [DOOR_ADDRESS], [OPEN_TIME], [CLOSE_OUT_TIME], [DOOR_ALERM2_LONG], [PASS_BACK], [DOOR_ENABLE], [DOOR_VISIBLE], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [IS_KQ], [IS_REPAST], [ALARM_MAST], [ALARM_TIME], [IS_MAIN_DOOR], [READER_IN], [READER_OUT], [M_CARDS_OPEN], [M_CARDS_OPEN_IN_OUT]) VALUES (9, 5, N'测试门', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[t_door] ([DOOR_ID], [CONTROL_ID], [DOOR_NAME], [DOOR_SERIAL], [DOOR_ADDRESS], [OPEN_TIME], [CLOSE_OUT_TIME], [DOOR_ALERM2_LONG], [PASS_BACK], [DOOR_ENABLE], [DOOR_VISIBLE], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [IS_KQ], [IS_REPAST], [ALARM_MAST], [ALARM_TIME], [IS_MAIN_DOOR], [READER_IN], [READER_OUT], [M_CARDS_OPEN], [M_CARDS_OPEN_IN_OUT]) VALUES (10, 6, N'Door0', NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, 0, 0)
INSERT [dbo].[t_door] ([DOOR_ID], [CONTROL_ID], [DOOR_NAME], [DOOR_SERIAL], [DOOR_ADDRESS], [OPEN_TIME], [CLOSE_OUT_TIME], [DOOR_ALERM2_LONG], [PASS_BACK], [DOOR_ENABLE], [DOOR_VISIBLE], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [IS_KQ], [IS_REPAST], [ALARM_MAST], [ALARM_TIME], [IS_MAIN_DOOR], [READER_IN], [READER_OUT], [M_CARDS_OPEN], [M_CARDS_OPEN_IN_OUT]) VALUES (11, 6, N'Door1', NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, 0, 0)
INSERT [dbo].[t_door] ([DOOR_ID], [CONTROL_ID], [DOOR_NAME], [DOOR_SERIAL], [DOOR_ADDRESS], [OPEN_TIME], [CLOSE_OUT_TIME], [DOOR_ALERM2_LONG], [PASS_BACK], [DOOR_ENABLE], [DOOR_VISIBLE], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [IS_KQ], [IS_REPAST], [ALARM_MAST], [ALARM_TIME], [IS_MAIN_DOOR], [READER_IN], [READER_OUT], [M_CARDS_OPEN], [M_CARDS_OPEN_IN_OUT]) VALUES (12, 6, N'Door2', NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, 0, 0)
INSERT [dbo].[t_door] ([DOOR_ID], [CONTROL_ID], [DOOR_NAME], [DOOR_SERIAL], [DOOR_ADDRESS], [OPEN_TIME], [CLOSE_OUT_TIME], [DOOR_ALERM2_LONG], [PASS_BACK], [DOOR_ENABLE], [DOOR_VISIBLE], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [IS_KQ], [IS_REPAST], [ALARM_MAST], [ALARM_TIME], [IS_MAIN_DOOR], [READER_IN], [READER_OUT], [M_CARDS_OPEN], [M_CARDS_OPEN_IN_OUT]) VALUES (13, 6, N'Door3', NULL, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, NULL, NULL, 0, 0)
SET IDENTITY_INSERT [dbo].[t_door] OFF
SET IDENTITY_INSERT [dbo].[t_door_time] ON 

INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (129, 10, N'Time0', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (130, 10, N'Time1', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (131, 10, N'Time2', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (132, 10, N'Time3', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (133, 10, N'Time4', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (134, 10, N'Time5', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (135, 10, N'Time6', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (136, 10, N'Time7', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (137, 11, N'Time0', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (138, 11, N'Time1', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (139, 11, N'Time2', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (140, 11, N'Time3', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (141, 11, N'Time4', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (142, 11, N'Time5', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (143, 11, N'Time6', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (144, 11, N'Time7', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (145, 12, N'Time0', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (146, 12, N'Time1', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (147, 12, N'Time2', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (148, 12, N'Time3', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (149, 12, N'Time4', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (150, 12, N'Time5', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (151, 12, N'Time6', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (152, 12, N'Time7', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (153, 13, N'Time0', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (154, 13, N'Time1', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (155, 13, N'Time2', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (156, 13, N'Time3', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (157, 13, N'Time4', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (158, 13, N'Time5', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (159, 13, N'Time6', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT [dbo].[t_door_time] ([DOOR_TIME_ID], [DOOR_ID], [DOOR_TIME_NAME], [ENABLE], [START_TIME], [END_TIME], [MONDAY], [TUESDAY], [WEDNESDAY], [THURSDAY], [FRIDAY], [SATURDAY], [SUNDAY], [HOLIDAY]) VALUES (160, 13, N'Time7', N'disable', NULL, NULL, 0, 0, 0, 0, 0, 0, 0, 0)
SET IDENTITY_INSERT [dbo].[t_door_time] OFF
SET IDENTITY_INSERT [dbo].[t_employee] ON 

INSERT [dbo].[t_employee] ([EMPLOYEE_ID], [EMPLOYEE_NAME], [EMPLOYEE_CODE], [ENGLISH_NAME], [CARD_NO], [PIN], [EMP_ENABLE], [SEX], [BIRTHDAY], [PERSON_CODE], [HOME], [PHONE], [EMAIL], [CAR], [JOB_ID], [DEPT_ID], [PHOTO1], [PHOTO2], [REG_DATE], [END_DATE], [DELETED], [LEAVE], [LEAVE_DATE], [BE_KQ], [PSWD], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [OWNER_DOOR], [LAST_EVENT_ID], [EVENT_2EMO_ID], [STATUS], [TIME_STAMP], [SHOW_CARD_NO], [NOTE1], [NOTE2], [NOTE3], [NOTE4], [NOTE5], [TIME_STAMPX], [IS_BLACK_CARD], [ASC_STRING], [PHOTO]) VALUES (1, N'萌妹子', N'1234555', N'Meizi Meng ', N'16170444', 0, 0, 1, CAST(0x0000A3FD00AF2198 AS DateTime), N'', N'', N'', N'', N'', 8, 2, N'"~/Test/test.jpg"', N'/upload/EmployeePhoto/a4d311a1-af25-470b-88b3-f9d68d9c9b72.jpg', CAST(0x0000A3FD00AF2198 AS DateTime), CAST(0x0000A3FD00AF2198 AS DateTime), 0, 0, CAST(0x0000A3FD00AF4BC8 AS DateTime), 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(0x0000A3FD00AF4BC8 AS DateTime), 0, N'', N'', N'', N'', NULL, CAST(0x0000A3FD00AF4BC8 AS DateTime), 0, NULL, NULL)
INSERT [dbo].[t_employee] ([EMPLOYEE_ID], [EMPLOYEE_NAME], [EMPLOYEE_CODE], [ENGLISH_NAME], [CARD_NO], [PIN], [EMP_ENABLE], [SEX], [BIRTHDAY], [PERSON_CODE], [HOME], [PHONE], [EMAIL], [CAR], [JOB_ID], [DEPT_ID], [PHOTO1], [PHOTO2], [REG_DATE], [END_DATE], [DELETED], [LEAVE], [LEAVE_DATE], [BE_KQ], [PSWD], [MAP_ID], [X_POINT], [Y_POINT], [MAP_VISIBLE], [OWNER_DOOR], [LAST_EVENT_ID], [EVENT_2EMO_ID], [STATUS], [TIME_STAMP], [SHOW_CARD_NO], [NOTE1], [NOTE2], [NOTE3], [NOTE4], [NOTE5], [TIME_STAMPX], [IS_BLACK_CARD], [ASC_STRING], [PHOTO]) VALUES (2, N'大美女', N'123332111', N'Meinv Da', NULL, 0, 0, 0, CAST(0x0000A3FD00B025C0 AS DateTime), N'', N'', N'', N'', N'', 7, 2, N'"~/Test/test1.jpg"', N'/upload/EmployeePhoto/c9c5d082-31ec-4f10-9098-433ea50a2e47.jpg', CAST(0x0000A3FD00B025C0 AS DateTime), CAST(0x0000A3FD00B025C0 AS DateTime), 0, 0, CAST(0x0000A3FD00B04C6C AS DateTime), 0, NULL, 0, 0, 0, 0, 0, 0, 0, 0, CAST(0x0000A3FD00B04C6C AS DateTime), 0, N'', N'', N'', N'', NULL, CAST(0x0000A3FD00B04C6C AS DateTime), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[t_employee] OFF
SET IDENTITY_INSERT [dbo].[t_event_record] ON 

INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (1, 0, CAST(0x0000A3FD012A8450 AS DateTime), 6, 9, 0, 0)
INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (2, 0, CAST(0x0000A3FD012A8450 AS DateTime), 6, 9, 0, 0)
INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (3, 0, CAST(0x0000A3FD012A8450 AS DateTime), 6, 9, 0, 0)
INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (4, 16170444, CAST(0x0000A3FD012B61CC AS DateTime), 6, 9, 11, 0)
INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (5, 16170444, CAST(0x0000A3FD012B89A4 AS DateTime), 6, 9, 11, 0)
INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (6, 16170444, CAST(0x0000A3FD012B89A4 AS DateTime), 6, 9, 11, 0)
INSERT [dbo].[t_event_record] ([EVENT_ID], [EMPLOYEE_ID], [EVENT_TIME], [CONTROL_ID], [DOOR_ID], [EVENT_TYPE], [MODIFY]) VALUES (7, 16170444, CAST(0x0000A3FD012B89A4 AS DateTime), 6, 9, 11, 0)
SET IDENTITY_INSERT [dbo].[t_event_record] OFF
SET IDENTITY_INSERT [dbo].[t_job] ON 

INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (2, N'职位_2')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (3, N'职位_3')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (4, N'职位_4')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (5, N'职位_5')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (6, N'职位_6')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (7, N'职位_7')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (8, N'职位_8')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (9, N'职位_9')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (10, N'职位_10')
INSERT [dbo].[t_job] ([JOB_ID], [JOB_NAME]) VALUES (11, N'职位_11')
SET IDENTITY_INSERT [dbo].[t_job] OFF
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (1, N'ROLE', N'0', N'APP', N'1', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (2, N'ROLE', N'0', N'APP', N'2', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (3, N'ROLE', N'0', N'APP', N'3', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (4, N'ROLE', N'0', N'APP', N'4', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (6, N'ROLE', N'0', N'APP', N'6', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (7, N'ROLE', N'0', N'APP', N'7', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (8, N'ROLE', N'0', N'APP', N'5', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (9, N'ROLE', N'0', N'APP', N'8', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (45, N'USER', N'3', N'APP', N'2000', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (46, N'USER', N'3', N'APP', N'2', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (47, N'USER', N'3', N'APP', N'3', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (48, N'USER', N'3', N'APP', N'4', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (56, N'USER', N'1', N'DEVICE_DOMAIN', N'1', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (57, N'USER', N'1', N'APP', N'3000', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (58, N'USER', N'1', N'APP', N'1', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (59, N'USER', N'1', N'APP', N'11', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (60, N'USER', N'1', N'APP', N'13', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (61, N'USER', N'1', N'APP', N'2000', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (62, N'USER', N'1', N'APP', N'2', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (63, N'USER', N'1', N'APP', N'3', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (49, N'USER', N'3', N'APP', N'14', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (64, N'USER', N'1', N'APP', N'4', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (65, N'USER', N'1', N'APP', N'14', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (66, N'USER', N'1', N'APP', N'6', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (67, N'USER', N'1', N'APP', N'10', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (68, N'USER', N'1', N'APP', N'12', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (69, N'USER', N'1', N'APP', N'1000', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (50, N'USER', N'3', N'APP', N'6', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (51, N'USER', N'3', N'APP', N'10', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (52, N'USER', N'3', N'APP', N'12', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (70, N'USER', N'1', N'APP', N'7', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (71, N'USER', N'1', N'APP', N'8', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (72, N'USER', N'1', N'APP', N'9', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (74, N'USER', N'1', N'DEVICE_DOMAIN', N'6', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (41, N'ROLE', N'0', N'APP', N'10', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (42, N'ROLE', N'0', N'APP', N'9', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (43, N'ROLE', N'0', N'APP', N'14', N'OP')
INSERT [dbo].[t_privilege] ([PRIVILEGE_ID], [PRIVILEGE_MASTER], [PRIVILEGE_MASTER_VALUE], [PRIVILEGE_ACCESS], [PRIVILEGE_ACCESS_VALUE], [PRIVILEGE_OPERATION]) VALUES (73, N'ROLE', N'0', N'APP', N'12', N'OP')
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (0, N'ADMIN', N'系统管理员', 0, CAST(0x0000A3D500A504EC AS DateTime), 0, CAST(0x0000A3D500A504EC AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (1, N'admin0', NULL, 0, CAST(0x0000A3D801242510 AS DateTime), 0, CAST(0x0000A3D801242510 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (2, N'admin1', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (3, N'admin2', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (4, N'admin3', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (5, N'admin4', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (6, N'admin5', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (7, N'admin6', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (8, N'admin7', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (9, N'admin8', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_role] ([ROLE_ID], [ROLE_NAME], [ROLE_DESC], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (10, N'admin9', NULL, 0, CAST(0x0000A3D8012429C0 AS DateTime), 0, CAST(0x0000A3D8012429C0 AS DateTime))
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (5, N'AccessManage', N'门禁权限管理', N'门禁权限管理', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (6, N'DeviceManage', N'控制器管理', N'控制器管理', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (7, N'DeptManage', N'部门管理', N'部门管理', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (8, N'EventRecord', N'刷卡记录', N'刷卡记录', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (1, N'Monitor', N'设备监控', N'设备监控', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (2, N'UserManage', N'用户管理', N'用户管理', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (3, N'PrivilegeManage', N'权限管理', N'权限管理', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (9, N'AlarmRecord', N'报警记录', N'报警记录', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (4, N'EmployeeManage', N'员工管理', N'员工管理', N'TRUE')
INSERT [dbo].[t_sys_application] ([APPLICATION_ID], [APPLICATION_CODE], [APPLICATION_NAME], [APPLICATION_DESC], [SHOW_IN_MENU]) VALUES (10, N'HolidayManage', N'假日管理', N'假日管理', N'TRUE')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (1, N'UserManage', N'2', N'3000', N'用户管理', NULL, N'visible', NULL, N'UserManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (2, N'DeptManage', N'7', N'2000', N'部门管理', NULL, N'visible', NULL, N'DeptManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (3, N'DeviceManage', N'6', N'2000', N'设备管理', NULL, N'visible', NULL, N'DeviceManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (4, N'AccessManage', N'5', N'2000', N'门禁权限管理', NULL, N'visible', NULL, N'AccessManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (14, N'JobManage', N'14', N'2000', N'职位管理', NULL, N'visible', NULL, N'JobManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (6, N'EmployeeManage', N'4', N'2000', N'员工管理', NULL, N'visible', NULL, N'EmployeeManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (7, N'Monitor', N'1', N'1000', N'设备监控', NULL, N'visible', NULL, N'Monitor/Monitor')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (8, N'EventRecord', N'8', N'1000', N'刷卡记录', NULL, N'visible', NULL, N'EventRecord/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (9, N'AlarmRecord', N'9', N'1000', N'报警记录', NULL, N'visible', NULL, N'AlarmRecord/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (10, N'HolidayManage', N'10', N'2000', N'假日管理', NULL, N'visible', NULL, N'HolidayManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (11, N'LogManage', NULL, N'3000', N'系统日志', NULL, N'visible', NULL, N'LogManage/LogManage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (12, N'EventTypeManage', N'12', N'2000', N'事件类型管理', NULL, N'visible', NULL, N'EventTypeManage/IndexPage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (13, N'Communication', NULL, N'3000', N'后台通信', NULL, N'visible', NULL, N'Communication/Communication')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (3000, N'sysmanage', NULL, NULL, N'系统管理', NULL, N'visible', N'TRUE', N'sysmanage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (2000, N'DataManage', NULL, NULL, N'数据配置', NULL, N'visible', N'TRUE', N'DataManage')
INSERT [dbo].[t_sys_menu] ([MENU_ID], [MENU_NO], [APPLICATION_CODE], [MENU_PARENT_NO], [MENU_NAME], [MENU_ICON], [IS_VISIBLE], [IS_LEAF], [MENU_URL]) VALUES (1000, N'App', N'', N'', N'基本功能', NULL, N'visible', N'TRUE', N'App')
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (1, N'admin', NULL, N'1234', 0, CAST(0x0000A3D100000000 AS DateTime), 0, CAST(0x0000A3D100000000 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (3, N'deptuser', N'顶顶', N'1234', 0, CAST(0x0000A3D2011DAADC AS DateTime), 1, CAST(0x0000A3F7012AA520 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (31, N'admin29', N'testdata', NULL, 0, CAST(0x0000A3D2011DAC08 AS DateTime), 0, CAST(0x0000A3D2011DAC08 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (32, N'admin30', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (33, N'admin31', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (34, N'admin32', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (39, N'admin37', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (43, N'admin41', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (44, N'admin42', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (45, N'admin43', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (46, N'admin44', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (47, N'admin45', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (48, N'admin46', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (49, N'admin47', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (50, N'admin48', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (51, N'admin49', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (52, N'admin50', N'testdata', NULL, 0, CAST(0x0000A3D2011DAD34 AS DateTime), 0, CAST(0x0000A3D2011DAD34 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (53, N'admin51', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (54, N'admin52', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (55, N'admin53', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (56, N'admin54', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (57, N'admin55', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (58, N'admin56', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (59, N'admin57', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (60, N'admin58', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (61, N'admin59', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (62, N'admin60', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (63, N'admin61', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (64, N'admin62', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (65, N'admin63', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (66, N'admin64', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (67, N'admin65', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (68, N'admin66', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (69, N'admin67', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (70, N'admin68', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (71, N'admin69', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (72, N'admin70', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (73, N'admin71', N'testdata', NULL, 0, CAST(0x0000A3D2011DAE60 AS DateTime), 0, CAST(0x0000A3D2011DAE60 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (74, N'admin72', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (75, N'admin73', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (76, N'admin74', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (77, N'admin75', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (78, N'admin76', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (79, N'admin77', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (80, N'admin78', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (81, N'admin79', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (82, N'admin80', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (83, N'admin81', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (84, N'admin82', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (85, N'admin83', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (86, N'admin84', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (87, N'admin85', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (88, N'admin86', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (89, N'admin87', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (90, N'admin88', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (91, N'admin89', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (92, N'admin90', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (93, N'admin91', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (94, N'admin92', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (95, N'admin93', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (96, N'admin94', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (97, N'admin95', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (98, N'admin96', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (99, N'admin97', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (100, N'admin98', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (101, N'admin99', N'testdata', NULL, 0, CAST(0x0000A3D2011DAF8C AS DateTime), 0, CAST(0x0000A3D2011DAF8C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (102, N'admin100', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (103, N'admin101', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (104, N'admin102', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (105, N'admin103', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (106, N'admin104', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (107, N'admin105', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (108, N'admin106', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (109, N'admin107', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (110, N'admin108', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (111, N'admin109', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (112, N'admin110', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (113, N'admin111', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (114, N'admin112', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (115, N'admin113', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (116, N'admin114', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (117, N'admin115', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (118, N'admin116', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (119, N'admin117', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (120, N'admin118', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (121, N'admin119', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (122, N'admin120', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (123, N'admin121', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (124, N'admin122', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (125, N'admin123', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (126, N'admin124', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (127, N'admin125', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (128, N'admin126', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (129, N'admin127', N'testdata', NULL, 0, CAST(0x0000A3D2011DB0B8 AS DateTime), 0, CAST(0x0000A3D2011DB0B8 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (130, N'admin128', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (131, N'admin129', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (132, N'admin130', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (133, N'admin131', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (134, N'admin132', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (135, N'admin133', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
GO
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (136, N'admin134', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (137, N'admin135', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (138, N'admin136', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (139, N'admin137', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (140, N'admin138', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (141, N'admin139', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (142, N'admin140', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (143, N'admin141', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (144, N'admin142', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (145, N'admin143', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (146, N'admin144', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (147, N'admin145', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (148, N'admin146', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (149, N'admin147', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (150, N'admin148', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (151, N'admin149', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (152, N'admin150', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (153, N'admin151', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (154, N'admin152', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (155, N'admin153', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (156, N'admin154', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (157, N'admin155', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (158, N'admin156', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (159, N'admin157', N'testdata', NULL, 0, CAST(0x0000A3D2011DB1E4 AS DateTime), 0, CAST(0x0000A3D2011DB1E4 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (160, N'admin158', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (161, N'admin159', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (162, N'admin160', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (163, N'admin161', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (164, N'admin162', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (165, N'admin163', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (166, N'admin164', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (167, N'admin165', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (168, N'admin166', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (169, N'admin167', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (170, N'admin168', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (171, N'admin169', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (172, N'admin170', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (173, N'admin171', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (174, N'admin172', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (175, N'admin173', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (176, N'admin174', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (177, N'admin175', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (178, N'admin176', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (179, N'admin177', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (180, N'admin178', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (181, N'admin179', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (182, N'admin180', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (183, N'admin181', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (184, N'admin182', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (185, N'admin183', N'testdata', NULL, 0, CAST(0x0000A3D2011DB310 AS DateTime), 0, CAST(0x0000A3D2011DB310 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (186, N'admin184', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (187, N'admin185', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (188, N'admin186', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (189, N'admin187', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (190, N'admin188', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (191, N'admin189', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (192, N'admin190', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (193, N'admin191', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (194, N'admin192', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (195, N'admin193', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (196, N'admin194', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (197, N'admin195', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (198, N'admin196', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (199, N'admin197', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (200, N'admin198', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (201, N'admin199', N'testdata', NULL, 0, CAST(0x0000A3D2011DB43C AS DateTime), 0, CAST(0x0000A3D2011DB43C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (203, N'user1', NULL, N'1111', 0, CAST(0x0000A3D3015B8794 AS DateTime), 0, CAST(0x0000A3D3015B8794 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (204, N'user2', NULL, N'1233', 0, CAST(0x0000A3D3015C63E4 AS DateTime), 0, CAST(0x0000A3D3015C663C AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (205, N'测试用户1', N'顶顶顶', N'888888', 1, CAST(0x0000A3F7012807FC AS DateTime), 1, CAST(0x0000A3F7012807FC AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (206, N'测试用户1', N'顶顶顶', N'888888', 1, CAST(0x0000A3F701282B24 AS DateTime), 1, CAST(0x0000A3F701282B24 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (207, N'测试用户2', N'订单', N'888888', 1, CAST(0x0000A3F70128D060 AS DateTime), 1, CAST(0x0000A3F70128D060 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (208, N'测试测试', N'是', N'888888', 1, CAST(0x0000A3F800B5BB70 AS DateTime), 1, CAST(0x0000A3F800B5BB70 AS DateTime))
INSERT [dbo].[t_user] ([USER_ID], [USER_NAME], [USER_DESC], [PSWD], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (209, NULL, NULL, N'888888', 1, CAST(0x0000A3F8011CE854 AS DateTime), 1, CAST(0x0000A3F8011CE854 AS DateTime))
INSERT [dbo].[t_user_role] ([USER_ROLE_ID], [USER_ID], [ROLE_ID], [CREATE_USER_ID], [CREATE_DATE], [MODIFY_USER_ID], [MODIFY_DATE]) VALUES (0, 1, 0, 0, CAST(0x0000A3D500A6FE00 AS DateTime), 0, CAST(0x0000A3D500A6FE00 AS DateTime))
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__t_employ__ACCFDE0B3EF7C626]    Script Date: 2014/12/10 18:30:50 ******/
ALTER TABLE [dbo].[t_employee] ADD UNIQUE NONCLUSTERED 
(
	[EMPLOYEE_NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ACSdatabase] SET  READ_WRITE 
GO
