CREATE DATABASE `db_matricula` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `db_matricula`;
CREATE TABLE `matricula` (
  `Id` char(36) NOT NULL,
  `DataHora` datetime NOT NULL,
  `Cpf` char(11) NOT NULL,
  `Ra` int(11) NOT NULL AUTO_INCREMENT,
  `IsAtivo` bit(1) NOT NULL,
  `IdUsuarioEmail` char(60) NOT NULL,
  PRIMARY KEY (`Ra`),
  UNIQUE KEY `Cpf_UNIQUE` (`Cpf`),
  UNIQUE KEY `Id_UNIQUE` (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=178554 DEFAULT CHARSET=latin1;
CREATE DATABASE `db_security` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `db_security`;
CREATE TABLE `usuario` (
  `Id` char(36) NOT NULL,
  `Email` char(60) NOT NULL,
  `Nome` char(120) NOT NULL,
  `Senha` char(56) NOT NULL,
  `Salt` char(56) NOT NULL,
  `DataCriacao` datetime NOT NULL,
  `IsAtivo` bit(1) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Email_UNIQUE` (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;