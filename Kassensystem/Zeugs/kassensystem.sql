-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Erstellungszeit: 09. Dez 2017 um 16:27
-- Server Version: 5.5.27
-- PHP-Version: 5.4.7

SET SQL_MODE="NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Datenbank: `kassensystem`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bestellposition`
--

CREATE TABLE IF NOT EXISTS `bestellposition` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Menge` int(11) NOT NULL,
  `FK_Produkt_ID` int(11) NOT NULL,
  `FK_Bestellung_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=26 ;

--
-- Daten für Tabelle `bestellposition`
--

INSERT INTO `bestellposition` (`ID`, `Menge`, `FK_Produkt_ID`, `FK_Bestellung_ID`) VALUES
(1, 4, 2, 2),
(2, 1, 1, 3),
(3, 5, 1, 4),
(4, 4, 7, 5),
(5, 2, 8, 5),
(6, 1, 9, 5),
(7, 1, 10, 5),
(8, 1, 11, 5),
(9, 1, 12, 5),
(10, 1, 13, 5),
(11, 1, 14, 5),
(12, 1, 15, 5),
(13, 1, 16, 5),
(14, 1, 17, 5),
(15, 1, 18, 5),
(16, 1, 19, 5),
(17, 1, 20, 5),
(18, 1, 21, 5),
(19, 1, 14, 6),
(20, 1, 15, 6),
(21, 1, 16, 6),
(22, 2, 14, 8),
(23, 2, 15, 8),
(24, 1, 13, 9),
(25, 1, 14, 9);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bestellung`
--

CREATE TABLE IF NOT EXISTS `bestellung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Datum` date NOT NULL,
  `FK_Mitarbeiter_ID` int(11) NOT NULL,
  `MwSt` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=10 ;

--
-- Daten für Tabelle `bestellung`
--

INSERT INTO `bestellung` (`ID`, `Datum`, `FK_Mitarbeiter_ID`, `MwSt`) VALUES
(1, '2017-12-09', 1, 0),
(2, '2017-12-09', 1, 0),
(3, '2017-12-09', 1, 0),
(4, '2017-12-09', 1, 0),
(5, '2017-12-09', 1, 0),
(6, '2017-12-09', 1, 0),
(7, '2017-12-09', 1, 0),
(8, '2017-12-09', 1, 19),
(9, '2017-12-09', 1, 7);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `mitarbeiter`
--

CREATE TABLE IF NOT EXISTS `mitarbeiter` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Vorname` varchar(255) NOT NULL,
  `Nachname` varchar(255) NOT NULL,
  `Vorgesetzter` int(11) DEFAULT NULL,
  `PIN` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=2 ;

--
-- Daten für Tabelle `mitarbeiter`
--

INSERT INTO `mitarbeiter` (`ID`, `Vorname`, `Nachname`, `Vorgesetzter`, `PIN`) VALUES
(1, 'Eric', 'Rameil', NULL, 1234);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `produkt`
--

CREATE TABLE IF NOT EXISTS `produkt` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` varchar(500) NOT NULL,
  `Preis` decimal(10,2) NOT NULL,
  `FK_Produktgruppe_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=22 ;

--
-- Daten für Tabelle `produkt`
--

INSERT INTO `produkt` (`ID`, `Bezeichnung`, `Preis`, `FK_Produktgruppe_ID`) VALUES
(7, 'Hamburger', 1.99, 1),
(8, 'SFC Burger', 2.99, 1),
(9, 'BBQ Burger', 5.95, 1),
(10, 'EinBurgerUng', 4.50, 1),
(11, 'Burgerwehr', 6.95, 1),
(12, 'Duis Burger', 2.99, 1),
(13, 'Wasser 0,2L', 1.50, 2),
(14, 'Wasser 0,3L', 2.00, 2),
(15, 'Wasser 0,5L', 2.50, 2),
(16, 'Clubmate 0,2L', 2.00, 2),
(17, 'Clubmate 0,3L', 2.50, 2),
(18, 'Clubmate 0,5L', 3.50, 2),
(19, 'Pommes Klein', 2.00, 3),
(20, 'Pommes Mittel', 3.00, 3),
(21, 'Pommes Groß', 4.50, 3);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `produktgruppe`
--

CREATE TABLE IF NOT EXISTS `produktgruppe` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=4 ;

--
-- Daten für Tabelle `produktgruppe`
--

INSERT INTO `produktgruppe` (`ID`, `Bezeichnung`) VALUES
(1, 'Burger'),
(2, 'Getränke'),
(3, 'Beilage');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `produkt_has_rabatt`
--

CREATE TABLE IF NOT EXISTS `produkt_has_rabatt` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `FK_Rabatt_ID` int(11) NOT NULL,
  `FK_Produkt_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `rabatt`
--

CREATE TABLE IF NOT EXISTS `rabatt` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Prozent` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
