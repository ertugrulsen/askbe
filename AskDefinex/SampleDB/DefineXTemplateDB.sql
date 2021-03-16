-- --------------------------------------------------------
-- Host:                         127.0.0.1
-- Server version:               8.0.20 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             11.0.0.5919
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


-- Dumping database structure for askdefinex
DROP DATABASE IF EXISTS `askdefinex`;
CREATE DATABASE IF NOT EXISTS `askdefinex` /*!40100 DEFAULT CHARACTER SET latin5 */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `askdefinex`;

-- Dumping structure for table askdefinex.company
DROP TABLE IF EXISTS `company`;
CREATE TABLE IF NOT EXISTS `company` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(200) NOT NULL,
  `Description` varchar(255) DEFAULT NULL,
  `Address` varchar(200) NOT NULL,
  `Country` varchar(100) DEFAULT NULL,
  `City` varchar(100) DEFAULT NULL,
  `ContactName` varchar(150) DEFAULT NULL,
  `Phone` varchar(150) DEFAULT NULL,
  `Email` varchar(100) NOT NULL,
  `Industry` varchar(150) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) NOT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UNQ_Email` (`Email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.company: ~0 rows (approximately)
/*!40000 ALTER TABLE `company` DISABLE KEYS */;
INSERT INTO `company` (`Id`, `Name`, `Description`, `Address`, `Country`, `City`, `ContactName`, `Phone`, `Email`, `Industry`, `IsActive`, `CreateDate`, `CreateUser`, `LastUpdateDate`, `LastUpdateUser`) VALUES
	(1, 'Definex', 'Teknoloji Danismanlik Sirketiniz', 'Kurucesme Mah. Kirbac Sok. 17/4, 34345, Besiktas', 'Turkey', 'Istanbul', 'Ates Ulas', '02327505050', 'teamdefinex@teamdefinex.com', 'Bilisim', 1, '2020-09-23 00:00:00', '', NULL, NULL);
/*!40000 ALTER TABLE `company` ENABLE KEYS */;

-- Dumping structure for table askdefinex.integrationkeys
DROP TABLE IF EXISTS `integrationkeys`;
CREATE TABLE IF NOT EXISTS `integrationkeys` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned NOT NULL,
  `FacebookAccessToken` text,
  `FacebookId` varchar(50) DEFAULT NULL,
  `CreateUser` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.integrationkeys: ~0 rows (approximately)
/*!40000 ALTER TABLE `integrationkeys` DISABLE KEYS */;
/*!40000 ALTER TABLE `integrationkeys` ENABLE KEYS */;

-- Dumping structure for table askdefinex.log
DROP TABLE IF EXISTS `log`;
CREATE TABLE IF NOT EXISTS `log` (
  `Source` varchar(200) NOT NULL,
  `Message` varchar(21845) NOT NULL,
  `Severity` varchar(50) NOT NULL,
  `TransactionId` varchar(50) DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.log: ~3 rows (approximately)
/*!40000 ALTER TABLE `log` DISABLE KEYS */;
INSERT INTO `log` (`Source`, `Message`, `Severity`, `TransactionId`, `CreateDate`, `CreateUser`) VALUES
	('Template.Api', 'Message : Login Failed. User:dad | Exception :  ', 'Info', NULL, '2020-09-26 23:42:54', NULL),
	('Template.Api', 'Message : Log Out. User: | Exception :  ', 'Info', NULL, '2020-09-26 23:51:50', NULL),
	('Template.Api', 'Message : Log Out. User:testuser | Exception :  ', 'Info', NULL, '2020-09-26 23:54:32', NULL);
/*!40000 ALTER TABLE `log` ENABLE KEYS */;

-- Dumping structure for table askdefinex.role
DROP TABLE IF EXISTS `role`;
CREATE TABLE IF NOT EXISTS `role` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(250) NOT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) NOT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.role: ~2 rows (approximately)
/*!40000 ALTER TABLE `role` DISABLE KEYS */;
INSERT INTO `role` (`Id`, `Name`, `Description`, `IsActive`, `CreateDate`, `CreateUser`, `LastUpdateDate`, `LastUpdateUser`) VALUES
	(1, 'Site Yönetimi', 'Site Yönetimi', 1, '2020-09-23 00:00:00', '', NULL, NULL),
	(2, 'Şirket Yönetimi', 'Şirket Yönetimi', 1, '2020-09-23 00:00:00', '', NULL, NULL),
	(3, 'Kullanıcı Yönetimi', 'Kullanıcı Yönetimi', 1, '2020-09-23 00:00:00', '1', NULL, NULL);
/*!40000 ALTER TABLE `role` ENABLE KEYS */;

-- Dumping structure for table askdefinex.setting
DROP TABLE IF EXISTS `setting`;
CREATE TABLE IF NOT EXISTS `setting` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Domain` varchar(50) NOT NULL,
  `Section` varchar(50) NOT NULL,
  `Key` varchar(50) NOT NULL,
  `Value` varchar(500) NOT NULL,
  `Definition` varchar(250) DEFAULT NULL,
  `CacheDuration` int DEFAULT NULL,
  `IsActive` tinyint(1) NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) NOT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.setting: ~0 rows (approximately)
/*!40000 ALTER TABLE `setting` DISABLE KEYS */;
/*!40000 ALTER TABLE `setting` ENABLE KEYS */;

-- Dumping structure for table askdefinex.user
DROP TABLE IF EXISTS `user`;
CREATE TABLE IF NOT EXISTS `user` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserName` varchar(100) NOT NULL,
  `Password` varchar(250) NOT NULL,
  `CompanyId` int DEFAULT NULL,
  `Name` varchar(250) DEFAULT NULL,
  `Surname` varchar(250) DEFAULT NULL,
  `RefreshToken` varchar(250) DEFAULT NULL,
  `RefreshTokenCreateDate` datetime DEFAULT NULL,
  `UserType` varchar(20) NOT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `IsDeleted` tinyint(1) DEFAULT '0',
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) NOT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.user: ~0 rows (approximately)
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` (`Id`, `UserName`, `Password`, `CompanyId`, `Name`, `Surname`, `RefreshToken`, `RefreshTokenCreateDate`, `UserType`, `IsActive`, `IsDeleted`, `CreateDate`, `CreateUser`, `LastUpdateDate`, `LastUpdateUser`) VALUES
	(1, 'testuser@teamdefinex.com', '7HcGIRYCrSEXdUmiEPoW4jq8H7BJPxQKwugg9aRsBQA=', 1, 'Test', 'User', '', '2020-11-17 10:06:26', 'client', 1, 0, '2020-09-23 00:00:00', '', NULL, NULL);
/*!40000 ALTER TABLE `user` ENABLE KEYS */;

-- Dumping structure for table askdefinex.userrole
DROP TABLE IF EXISTS `userrole`;
CREATE TABLE IF NOT EXISTS `userrole` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `RoleId` int NOT NULL,
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) NOT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_UserRoleUserId` (`UserId`),
  KEY `FK_UserRoleRoleId` (`RoleId`),
  CONSTRAINT `FK_UserRoleRoleId` FOREIGN KEY (`RoleId`) REFERENCES `role` (`Id`),
  CONSTRAINT `FK_UserRoleUserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.userrole: ~0 rows (approximately)
/*!40000 ALTER TABLE `userrole` DISABLE KEYS */;
INSERT INTO `userrole` (`Id`, `UserId`, `RoleId`, `CreateDate`, `CreateUser`, `LastUpdateDate`, `LastUpdateUser`) VALUES
	(1, 1, 1, '2020-09-23 00:00:00', '', NULL, NULL),
	(2, 1, 3, '2020-09-23 00:00:00', '1', NULL, NULL),
	(3, 1, 2, '2020-09-23 00:00:00', '1', NULL, NULL);
/*!40000 ALTER TABLE `userrole` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;

-- Dumping structure for table askdefinex.ask_user
DROP TABLE IF EXISTS `ask_user`;
CREATE TABLE IF NOT EXISTS `ask_user` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) DEFAULT NULL,
  `Surname` varchar(50) DEFAULT NULL,
  `Username` varchar(50) DEFAULT NULL,
  `Email` varchar(50) DEFAULT NULL,
  `Password` varchar(50) NOT NULL,
  `RefreshToken` varchar(250) DEFAULT NULL,
  `RefreshTokenCreateDate` datetime DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreateDate` datetime NOT NULL,
  `CreateUser` varchar(50) NOT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.user: ~0 rows (approximately)
/*!40000 ALTER TABLE `ask_user` DISABLE KEYS */;
INSERT INTO `ask_user` (`Id`, `Name`, `Surname`,`Username`, `Email`, `Password`,`RefreshToken`,`RefreshTokenCreateDate`, `IsActive`, `CreateDate`, `CreateUser`, `LastUpdateDate`, `LastUpdateUser`) VALUES
	(1, 'Test', 'User','test', 'testuser@teamdefinex.com', '7HcGIRYCrSEXdUmiEPoW4jq8H7BJPxQKwugg9aRsBQA=','','2020-11-17 10:06:26', 1, '2020-11-17 10:06:26', 'client', '2020-09-23 00:00:00', '');
/*!40000 ALTER TABLE `ask_user` ENABLE KEYS */;

-- Dumping structure for table askdefinex.ask_badge
DROP TABLE IF EXISTS `ask_badge`;
CREATE TABLE IF NOT EXISTS `ask_badge` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Type` varchar(50) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreateUser` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.ask_badge: ~0 rows (approximately)
/*!40000 ALTER TABLE `ask_badge` DISABLE KEYS */;
/*!40000 ALTER TABLE `ask_badge` ENABLE KEYS */;

-- Dumping structure for table askdefinex.ask_tag
DROP TABLE IF EXISTS `ask_tag`;
CREATE TABLE IF NOT EXISTS `ask_tag` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `QuestionId` int unsigned NOT NULL,
  `Name` varchar(50) DEFAULT NULL,
  `Type` varchar(50) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreateUser` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.ask_tag: ~0 rows (approximately)
/*!40000 ALTER TABLE `ask_tag` DISABLE KEYS */;
/*!40000 ALTER TABLE `ask_tag` ENABLE KEYS */;

-- Dumping structure for table askdefinex.ask_question
DROP TABLE IF EXISTS `ask_question`;
CREATE TABLE IF NOT EXISTS `ask_question` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned NOT NULL,
  `Header` varchar(150) DEFAULT NULL,
  `Detail` varchar(1000) DEFAULT NULL,
  `UpVotes` int DEFAULT NULL,
  `DownVotes` int DEFAULT NULL,
  `IsAccepted` tinyint(1) DEFAULT '1',
  `IsActive` tinyint(1) DEFAULT '1',
  `IsClose` tinyint(1) DEFAULT '1',
  `CreateUser` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.ask_question: ~0 rows (approximately)
/*!40000 ALTER TABLE `ask_question` DISABLE KEYS */;
/*!40000 ALTER TABLE `ask_question` ENABLE KEYS */;

-- Dumping structure for table askdefinex.ask_answer
DROP TABLE IF EXISTS `ask_answer`;
CREATE TABLE IF NOT EXISTS `ask_answer` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `QuestionId` int unsigned NOT NULL,
  `UserId` int unsigned NOT NULL,
  `Answer` varchar(4000) DEFAULT NULL,
  `UpVotes` int DEFAULT NULL,
  `DownVotes` int DEFAULT NULL,
  `IsAccepted` tinyint(1) DEFAULT '1',
  `IsActive` tinyint(1) DEFAULT '1',
  `CreateUser` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.ask_answer: ~0 rows (approximately)
/*!40000 ALTER TABLE `ask_answer` DISABLE KEYS */;
/*!40000 ALTER TABLE `ask_answer` ENABLE KEYS */;

-- Dumping structure for table askdefinex.ask_comment
DROP TABLE IF EXISTS `ask_comment`;
CREATE TABLE IF NOT EXISTS `ask_comment` (
  `Id` int unsigned NOT NULL AUTO_INCREMENT,
  `UserId` int unsigned NOT NULL,
  `Question_Answer_Id` int unsigned NOT NULL,
  `Comment` varchar(1000) DEFAULT NULL,
  `Type` varchar(36) DEFAULT NULL,
  `IsActive` tinyint(1) DEFAULT '1',
  `CreateUser` varchar(50) DEFAULT NULL,
  `CreateDate` datetime DEFAULT NULL,
  `LastUpdateUser` varchar(50) DEFAULT NULL,
  `LastUpdateDate` datetime DEFAULT NULL,
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin5;

-- Dumping data for table askdefinex.ask_comment: ~0 rows (approximately)
/*!40000 ALTER TABLE `ask_comment` DISABLE KEYS */;
/*!40000 ALTER TABLE `ask_comment` ENABLE KEYS */;



