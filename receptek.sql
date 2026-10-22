-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2026. Okt 22. 08:45
-- Kiszolgáló verziója: 10.4.32-MariaDB
-- PHP verzió: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `receptek`
--
CREATE DATABASE IF NOT EXISTS `receptek` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE `receptek`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `hozzavalo`
--

CREATE TABLE `hozzavalo` (
  `id` int(11) NOT NULL,
  `ingredientName` varchar(40) NOT NULL,
  `quantity` varchar(20) NOT NULL,
  `description` text DEFAULT NULL,
  `addedTime` datetime NOT NULL,
  `updateTime` datetime NOT NULL,
  `szakacsId` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `hozzavalo`
--

INSERT INTO `hozzavalo` (`id`, `ingredientName`, `quantity`, `description`, `addedTime`, `updateTime`, `szakacsId`) VALUES
(1, 'Liszt', '500 g', 'Finomliszt, sütéshez.', '2025-05-05 09:00:00', '2025-05-05 09:00:00', 1),
(2, 'Paradicsom', '1 kg', 'Érett, házi kertből.', '2025-05-16 10:30:00', '2025-05-16 10:30:00', 2),
(3, 'Fokhagyma', '3 gerezd', 'Frissen pucolva.', '2025-05-27 11:15:00', '2025-05-27 11:15:00', 3),
(4, 'Vaj', '250 g', 'Sótlan, hűtőből.', '2025-06-07 08:20:00', '2025-06-07 08:20:00', 5),
(5, 'Bazsalikom', '1 csokor', 'Friss, tésztaszószhoz.', '2025-06-18 12:40:00', '2025-06-18 12:40:00', 6);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `szakacs`
--

CREATE TABLE `szakacs` (
  `id` int(11) NOT NULL,
  `name` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `age` int(11) NOT NULL,
  `password` varchar(100) NOT NULL,
  `registrationTime` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- A tábla adatainak kiíratása `szakacs`
--

INSERT INTO `szakacs` (`id`, `name`, `email`, `age`, `password`, `registrationTime`) VALUES
(1, 'Bíró Emma', 'biro.emma@example.com', 29, 'receptes1', '2025-01-09 08:45:00'),
(2, 'Székely Zalán', 'szekely.zalan@example.com', 34, 'konyha2025', '2025-02-02 10:20:00'),
(3, 'Antal Flóra', 'antal.flora@example.com', 27, 'fuszeres99', '2025-02-21 09:00:00'),
(4, 'Pintér Marcell', 'pinter.marcell@example.com', 31, 'sutes321', '2025-03-11 14:30:00'),
(5, 'Nagy Alíz', 'nagy.aliz@example.com', 25, 'recept2025', '2025-03-30 11:15:00'),
(6, 'Váradi Botond', 'varadi.botond@example.com', 36, 'grillmester', '2025-04-17 15:50:00');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `hozzavalo`
--
ALTER TABLE `hozzavalo`
  ADD PRIMARY KEY (`id`),
  ADD KEY `szakacsId` (`szakacsId`);

--
-- A tábla indexei `szakacs`
--
ALTER TABLE `szakacs`
  ADD PRIMARY KEY (`id`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `hozzavalo`
--
ALTER TABLE `hozzavalo`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT a táblához `szakacs`
--
ALTER TABLE `szakacs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `hozzavalo`
--
ALTER TABLE `hozzavalo`
  ADD CONSTRAINT `hozzavalo_ibfk_1` FOREIGN KEY (`szakacsId`) REFERENCES `szakacs` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
