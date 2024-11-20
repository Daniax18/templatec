-- Création de la table roles
CREATE TABLE roles (
    idrole VARCHAR(50) PRIMARY KEY,
    descrole VARCHAR(200),
    rang INTEGER
);

-- Insertion de données de test dans la table roles
INSERT INTO roles (idrole, descrole, rang)
VALUES ('Dg', 'Directeur Général', 10),
       ('Emp', 'Employé', 1);

-- -------------------------------------------------------------------------------------------------      

-- Création de la table utilisateur
CREATE TABLE utilisateur (
    id  VARCHAR(50) PRIMARY KEY,
    refuser INTEGER,
    loginuser VARCHAR(200),
    pwduser VARCHAR(200),
    nomuser VARCHAR(200),
    adruser VARCHAR(200),
    teluser VARCHAR(100),
    idrole VARCHAR(100),
    FOREIGN KEY (idrole) REFERENCES roles(idrole)
);

-- Insertion de données de test dans la table utilisateur
INSERT INTO utilisateur (id, refuser, loginuser, pwduser, nomuser, adruser, teluser, idrole)
VALUES ('EMP1', 1, 'jdupont', 'password123', 'Jean Dupont', '123 Rue Principale', '0601020304', 'Dg'),
       ('EMP2', 2, 'mleclerc', 'mdp456', 'Marie Leclerc', '456 Rue Secondaire', '0605060708', 'Emp'),
       ('EMP3', 3, 'rbernard', 'monpass789', 'René Bernard', '789 Rue Tertiaire', '0611223344', 'Emp');

-- -------------------------------------------------------------------------------------------------   

CREATE TABLE historique (
    idhistorique VARCHAR(50) NOT NULL PRIMARY KEY,
    datehistorique DATE NOT NULL,
    heure VARCHAR(50) NOT NULL,
    objet VARCHAR(100) NOT NULL,
    action VARCHAR(50) NOT NULL,
    idutilisateur VARCHAR(50) NOT NULL,
    refobjet VARCHAR(50) NOT NULL
);

CREATE TABLE historique_valeur (
    id VARCHAR(50) NOT NULL PRIMARY KEY,
    idhisto VARCHAR(50) NOT NULL,
    refhisto VARCHAR(255) NOT NULL,
    nom_table VARCHAR(255) NOT NULL,
    nom_classe VARCHAR(255) NOT NULL,
    val1 VARCHAR(255) DEFAULT NULL,
    val2 VARCHAR(255) DEFAULT NULL,
    val3 VARCHAR(255) DEFAULT NULL,
    val4 VARCHAR(255) DEFAULT NULL,
    val5 VARCHAR(255) DEFAULT NULL,
    val6 VARCHAR(255) DEFAULT NULL,
    val7 VARCHAR(255) DEFAULT NULL,
    val8 VARCHAR(255) DEFAULT NULL,
    val9 VARCHAR(255) DEFAULT NULL,
    val10 VARCHAR(255) DEFAULT NULL,
    FOREIGN KEY (idhisto) REFERENCES historique(idhistorique)
);
