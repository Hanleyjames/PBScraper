# PrettyBasicScraper
------------------------
## By Hanleyjames
* github.com/Hanleyjames
* hanley.doth@outlook.com

----------------------

### Description

The scope of this repository (Pretty Basic Scraper) is a proof of concept in scraping contact information from Google's Custom Search API. The future live version will be publicly available with an expanded set of tools and features.

-----

### Running the project

Run the following SQL commands to create the DB and table
```
CREATE DATABASE IF NOT EXISTS `pbscraper` DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;
USE `pbscraper`;

DROP TABLE IF EXISTS `pbscrape`;
CREATE TABLE `pbscraper`.`pbscrape` ( `id` int(11) NOT NULL AUTO_INCREMENT, `keyword` varchar(255) NOT NULL, `url` varchar(255) NOT NULL, `phone` varchar(255) NOT NULL, `email` varchar(255) NOT NULL PRIMARY KEY (`id`)) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COLLATE utf8_general_ci;
```

 ---

 ### All remaining project task are in the project MVP kabana.
