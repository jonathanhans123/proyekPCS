/*
SQLyog Ultimate v13.1.1 (64 bit)
MySQL - 10.4.21-MariaDB : Database - proyekpcs
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`proyekpcs` /*!40100 DEFAULT CHARACTER SET utf8mb4 */;

USE `proyekpcs`;

/*Table structure for table `discount` */

DROP TABLE IF EXISTS `discount`;

CREATE TABLE `discount` (
  `di_id` int(100) NOT NULL AUTO_INCREMENT,
  `di_name` varchar(100) NOT NULL,
  `di_category` varchar(100) DEFAULT NULL,
  `di_value` int(10) NOT NULL,
  `di_type` varchar(100) DEFAULT NULL,
  `di_status` int(10) NOT NULL,
  PRIMARY KEY (`di_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

/*Data for the table `discount` */

insert  into `discount`(`di_id`,`di_name`,`di_category`,`di_value`,`di_type`,`di_status`) values 
(1,'Sneakers','Tipe',10,'Diskon Percentage',1),
(8,'576 Classic Sneakers','Name',10,'Diskon Percentage',1),
(9,'576 Classic Sneakers','Name',40,'Diskon Percentage',1),
(10,'Sneakers','Tipe',0,'Buy 1 Get 1 Free',1);

/*Table structure for table `item` */

DROP TABLE IF EXISTS `item`;

CREATE TABLE `item` (
  `it_id` int(100) NOT NULL AUTO_INCREMENT,
  `it_nama` varchar(100) NOT NULL,
  `it_stock` varchar(100) NOT NULL,
  `it_price` varchar(100) NOT NULL,
  `it_size` varchar(100) NOT NULL,
  `ti_id` int(100) NOT NULL,
  `me_id` int(100) NOT NULL,
  `it_status` int(100) NOT NULL,
  PRIMARY KEY (`it_id`),
  KEY `fk1` (`ti_id`),
  KEY `fk2` (`me_id`),
  CONSTRAINT `fk1` FOREIGN KEY (`ti_id`) REFERENCES `tipe` (`ti_id`),
  CONSTRAINT `fk2` FOREIGN KEY (`me_id`) REFERENCES `merk` (`me_id`)
) ENGINE=InnoDB AUTO_INCREMENT=53 DEFAULT CHARSET=utf8mb4;

/*Data for the table `item` */

insert  into `item`(`it_id`,`it_nama`,`it_stock`,`it_price`,`it_size`,`ti_id`,`me_id`,`it_status`) values 
(11,'574 Classic Sneakers','23','1599000','38',1,1,1),
(12,'575 Classic Sneakers','15','1599000','39',1,1,1),
(13,'576 Classic Sneakers','10','1599000','38',1,1,1),
(14,'577 Classic Sneakers','30','1599000','38',1,1,1),
(15,'ADIDAS Originals ZX 2K Flux Sneakers','15','1597050','39',1,10,1),
(16,'ADIDAS originals superstar junior sneakers','15','1020000','39',1,10,1),
(17,'ADIDAS nite jogger sneakers','10','2164050','39',1,10,1),
(19,'Converse Jack Purcell Gold Standard 1st In Class Ox Sneakers','10','943000','40',1,1,1),
(20,'Converse Chuck Taylor All Star 70 Core Ox Sneakers','5','1099000','39',1,2,1),
(21,'Converse Jack Purcell Gold Standard Ox Sneakers','7','1177900','38',1,2,1),
(23,'The North Face Men Thermoball Boot Zip-Up-NF0A4OAI9T3','10','1645000','39',2,3,1),
(24,'The North Face Men M Thermoball Boot-NF0A4OAIV75','14','1645000','39',2,3,1),
(25,'The North Face Men Thermoball Boot Zip Up Black Grey-NF0A4OAIKZ2','2','3290000','38',2,3,1),
(26,'Timberland Men White Ledge Mid Waterproof Ankle Boot','11','1294455','39',2,4,1),
(27,'Timberland Men Classic Boot Ankle','10','2158625','38',2,4,1),
(28,'Timberland Men Flume Mid Waterproof Hiking Boot','15','1295175','40',2,4,1),
(29,'Timberland 6 Premium Boot','5','1599522','41',2,4,1),
(30,'Red Wing Heritage Men Classic Moc 6 Boot','24','3813426','38',2,5,1),
(31,'Red Wing Men Iron Ranger 6 Boot','10','4605066','38',2,5,1),
(32,'Red Wing Heritage Men Blacksmith Vibram Boot','10','4604922','41',2,5,1),
(33,'ADIDAS nizza rf slip shoes','15','1000000','39',3,10,1),
(34,'ADIDAS 3MC Slip x Disney Sport Goofy Shoes','5','1199999','39',3,10,1),
(35,'ADIDAS Originals womens Superstar Slip-On Shoes','16','2445722','39',3,10,1),
(36,'Vans Classic Slip-On','10','999000','38',3,6,1),
(37,'Vans Classic Slip-On 98 Dx','5','1099000','39',3,6,1),
(38,'Vans Comfycush Slip-On','15','1229000','39',3,6,1),
(39,'Vans Ua Comfycush Slip-On','10','1149000','39',3,6,1),
(40,'Rubi HOLLY SLIP ON','10','199.900','41',3,7,1),
(41,'Rubi HOLLY SLIP ON','10','199.900','41',3,7,1),
(42,'Rubi PEYTON SLIP ON','11','199900','38',3,7,1),
(43,'Columbia Men Redmond V2 Waterproof Hiking Boot','10','2629349','38',4,8,1),
(44,'Columbia Men Newton Ridge Plus Ii Suede Waterproof Hiking Shoe','10','1031247','38',4,8,1),
(45,'Columbia Women Newton Ridge Plus Hiking Boot','13','3425018','38',4,8,1),
(48,'ADIDAS Men Terrex Trailmaker Gore-tex Hiking Walking Shoe','10','1095430','39',4,10,1),
(49,'ADIDAS Men Terrex Eastrail Hiking Shoes','10','2014572','38',4,10,1),
(50,'ADIDAS Men Terrex Free Hiker Hiking Boot','11','2414062','38',4,10,1),
(51,'test1','10','10','10',1,1,1);

/*Table structure for table `merk` */

DROP TABLE IF EXISTS `merk`;

CREATE TABLE `merk` (
  `me_id` int(100) NOT NULL AUTO_INCREMENT,
  `me_name` varchar(1000) NOT NULL,
  PRIMARY KEY (`me_id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4;

/*Data for the table `merk` */

insert  into `merk`(`me_id`,`me_name`) values 
(1,'New Balance'),
(2,'Converse'),
(3,'The North Face'),
(4,'Timberland'),
(5,'Red Wing'),
(6,'Vans'),
(7,'Rubi'),
(8,'Columbia'),
(10,'Adidas');

/*Table structure for table `order` */

DROP TABLE IF EXISTS `order`;

CREATE TABLE `order` (
  `or_id` int(100) NOT NULL AUTO_INCREMENT,
  `us_id` int(100) DEFAULT NULL,
  `or_hargatotal` varchar(1000) NOT NULL,
  `or_tanggalorder` date NOT NULL,
  PRIMARY KEY (`or_id`),
  KEY `fk3` (`us_id`),
  CONSTRAINT `fk3` FOREIGN KEY (`us_id`) REFERENCES `user` (`us_id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

/*Data for the table `order` */

insert  into `order`(`or_id`,`us_id`,`or_hargatotal`,`or_tanggalorder`) values 
(1,1,'6396000','0000-00-00'),
(2,1,'6396000','0000-00-00');

/*Table structure for table `ordered_item` */

DROP TABLE IF EXISTS `ordered_item`;

CREATE TABLE `ordered_item` (
  `or_id` int(100) NOT NULL,
  `it_id` int(100) NOT NULL,
  `oi_itemprice` varchar(100) NOT NULL,
  `oi_quantity` int(100) NOT NULL,
  PRIMARY KEY (`or_id`,`it_id`),
  KEY `fk5` (`it_id`),
  CONSTRAINT `fk4` FOREIGN KEY (`or_id`) REFERENCES `order` (`or_id`),
  CONSTRAINT `fk5` FOREIGN KEY (`it_id`) REFERENCES `item` (`it_id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

/*Data for the table `ordered_item` */

insert  into `ordered_item`(`or_id`,`it_id`,`oi_itemprice`,`oi_quantity`) values 
(1,11,'1599000',4),
(2,11,'1599000',4);

/*Table structure for table `tipe` */

DROP TABLE IF EXISTS `tipe`;

CREATE TABLE `tipe` (
  `ti_id` int(100) NOT NULL AUTO_INCREMENT,
  `ti_name` varchar(1000) NOT NULL,
  PRIMARY KEY (`ti_id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4;

/*Data for the table `tipe` */

insert  into `tipe`(`ti_id`,`ti_name`) values 
(1,'Sneakers'),
(2,'Boots'),
(3,'Slip-on'),
(4,'Hiking');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `us_id` int(100) NOT NULL AUTO_INCREMENT,
  `us_name` varchar(100) NOT NULL,
  `us_password` varchar(100) NOT NULL,
  `us_email` varchar(100) NOT NULL,
  `us_phone` varchar(100) NOT NULL,
  `us_rank` varchar(500) NOT NULL,
  PRIMARY KEY (`us_id`),
  UNIQUE KEY `user_email` (`us_email`),
  UNIQUE KEY `user_phone` (`us_phone`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4;

/*Data for the table `user` */

insert  into `user`(`us_id`,`us_name`,`us_password`,`us_email`,`us_phone`,`us_rank`) values 
(1,'jonathanhans1234','jojo123','jojo@gmail.com','joji2','Bronze'),
(2,'jojojojo','jojojojojo','jojjojjojoj','jojojojojojojojo','');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
