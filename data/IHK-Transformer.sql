-- Datenbank löschen, falls sie existiert
DROP DATABASE IF EXISTS ihk_transformer;

-- Neue Datenbank erstellen
CREATE DATABASE ihk_transformer;

-- Nutzung der Datenbank
USE ihk_transformer;

-- Tabelle: Ausbildungen
CREATE TABLE Ausbildungen(
    ausbildung_id varchar(4) PRIMARY KEY NOT NULL,
    bezeichnung varchar(50) NOT NULL
);

-- Tabelle: Ausbilder
CREATE TABLE Ausbilder(
    ausbilder_id int PRIMARY KEY AUTO_INCREMENT NOT NULL,
    vorname varchar(50) NOT NULL,
    nachname varchar(50) NOT NULL
    );

-- Tabelle: Azubis
CREATE TABLE Azubis(
	azubi_id int PRIMARY KEY AUTO_INCREMENT NOT NULL,
    vorname varchar(50) NOT NULL,
    nachname varchar(50) NOT NULL,
    ausbildungsbeginn DATE NOT NULL,
    ausbildung_id varchar(4) NOT NULL,
    ausbilder_id int NOT NULL,
    
    
    FOREIGN KEY (ausbildung_id) REFERENCES Ausbildungen(ausbildung_id),
    FOREIGN KEY (ausbilder_id) REFERENCES Ausbilder(ausbilder_id)
);

-- Daten einfügen in Ausbilder
INSERT INTO `ausbilder` (`ausbilder_id`, `vorname`, `nachname`) VALUES
(101, 'Christian', 'Böttcher');

-- Daten einfügen in Ausbildungen
INSERT INTO `ausbildungen` (`ausbildung_id`, `bezeichnung`) VALUES
('FIAN', 'Fachfinformatiker für Anwendungsentwicklung');

-- Daten einfügen in Azubis
INSERT INTO `azubis` (`azubi_id`, `vorname`, `nachname`, `ausbildungsbeginn`, `ausbildung_id`, `ausbilder_id`) VALUES
(1, 'Mert', 'Kuyumcuoglu', '2023-09-01', 'FIAN', 101),
(2, 'Michel', 'Pörschke', '2023-09-01', 'FIAN', 101),
(3, 'Henner', 'Wittek', '2023-09-01', 'FIAN', 101),
(4, 'Tobias', 'Hausmann', '2023-09-01', 'FIAN', 101),
(5, 'Alexander', 'Geffe', '2023-09-01', 'FIAN', 101),
(6, 'Marcel', 'Oelschläger', '2023-09-01', 'FIAN', 101),
(7, 'Konrad', 'Friedrichson', '2023-09-01', 'FIAN', 101),
(8, 'Christopher', 'Mieske', '2023-09-01', 'FIAN', 101),
(9, 'Justus', 'Habig', '2023-09-01', 'FIAN', 101),
(10, 'Erik', 'Boinski', '2023-09-01', 'FIAN', 101),
(11, 'Niels', 'Reichel', '2023-09-01', 'FIAN', 101),
(12, 'Birger', 'Weidemann', '2023-09-01', 'FIAN', 101);