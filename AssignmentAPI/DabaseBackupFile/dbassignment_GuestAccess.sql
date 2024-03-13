-- MySQL dump 10.13  Distrib 8.0.34, for macos13 (arm64)
--
-- Host: dbassignment.cf6yy06uqhhu.ap-southeast-1.rds.amazonaws.com    Database: dbassignment
-- ------------------------------------------------------
-- Server version	8.0.35

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
SET @MYSQLDUMP_TEMP_LOG_BIN = @@SESSION.SQL_LOG_BIN;
SET @@SESSION.SQL_LOG_BIN= 0;

--
-- GTID state at the beginning of the backup 
--

SET @@GLOBAL.GTID_PURGED=/*!80000 '+'*/ '';

--
-- Table structure for table `GuestAccess`
--

DROP TABLE IF EXISTS `GuestAccess`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `GuestAccess` (
  `GuestAccessId` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Path` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `isGetAccess` tinyint(1) DEFAULT NULL,
  `isPostAccess` tinyint(1) DEFAULT NULL,
  `isPutAccess` tinyint(1) DEFAULT NULL,
  `isDeleteAccess` tinyint(1) DEFAULT NULL,
  `CreatedDate` datetime(6) DEFAULT NULL,
  `UpdatedDate` datetime(6) DEFAULT NULL,
  `CreatedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `UpdatedBy` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`GuestAccessId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `GuestAccess`
--

LOCK TABLES `GuestAccess` WRITE;
/*!40000 ALTER TABLE `GuestAccess` DISABLE KEYS */;
INSERT INTO `GuestAccess` VALUES ('','api/Rooms/GetRoomsByLevel',1,0,0,0,NULL,NULL,NULL,NULL),('1','/swagger/index.html',1,0,0,0,NULL,NULL,NULL,NULL),('10','/api/User',0,1,0,1,NULL,NULL,NULL,NULL),('15','api/Levels/GetLevelsByBuilding',1,0,0,0,NULL,NULL,NULL,NULL),('2','/api/Visitors',0,1,0,0,NULL,NULL,NULL,NULL),('3','/api/Rooms',1,0,0,0,NULL,NULL,NULL,NULL),('4','/api/Levels',1,0,0,0,NULL,NULL,NULL,NULL),('5','/api/Building',1,0,0,0,NULL,NULL,NULL,NULL),('6','/swagger/v1/swagger.json',1,0,0,0,NULL,NULL,NULL,NULL),('7','/api/User/UserLogin',1,1,0,0,NULL,NULL,NULL,NULL),('8','/swagger/index.html',1,0,0,0,NULL,NULL,NULL,NULL),('9','api/Levels/GetLevelsByBuilding',1,0,0,0,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `GuestAccess` ENABLE KEYS */;
UNLOCK TABLES;
SET @@SESSION.SQL_LOG_BIN = @MYSQLDUMP_TEMP_LOG_BIN;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-03-13 16:32:02
