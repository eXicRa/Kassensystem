-- phpMyAdmin SQL Dump
-- version 3.5.2.2
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Erstellungszeit: 25. Nov 2017 um 12:50
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
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `bestellung`
--

CREATE TABLE IF NOT EXISTS `bestellung` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Datum` date NOT NULL,
  `FK_Mitarbeiter_ID` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `mitarbeiter`
--

CREATE TABLE IF NOT EXISTS `mitarbeiter` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Vorname` varchar(255) NOT NULL,
  `Nachname` varchar(255) NOT NULL,
  `Vorgesetzter` int(11) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1 AUTO_INCREMENT=1 ;

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
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=5 ;

--
-- Daten für Tabelle `produkt`
--

INSERT INTO `produkt` (`ID`, `Bezeichnung`, `Preis`, `FK_Produktgruppe_ID`) VALUES
(1, 'Hamburger', 1.00, 0),
(2, 'Chessburger', 2.00, 0),
(3, 'Hamburger', 1.99, 0),
(4, 'Chessburger', 2.99, 0);

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `produktgruppe`
--

CREATE TABLE IF NOT EXISTS `produktgruppe` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Daten für Tabelle `produktgruppe`
--

INSERT INTO `produktgruppe` (`ID`, `Bezeichnung`) VALUES
(1, 'Burger'),
(2, 'Getränke');

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
