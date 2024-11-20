  

-- Table magasin
CREATE TABLE magasin (
    id VARCHAR(50) PRIMARY KEY,
    nom VARCHAR(200)
);

INSERT INTO magasin (id, nom)
VALUES ('MAG1', 'Magasin Central'),
       ('MAG2', 'Magasin Secondaire');

-- -------------------------------------------------------------------------------------------------  
-- Table utilisateur_magasin
CREATE TABLE utilisateur_magasin (
    id VARCHAR(50) PRIMARY KEY,
    idutilisateur VARCHAR(50),
    idmagasin VARCHAR(50),
    datedebut DATE,
    datefin DATE,
    FOREIGN KEY (idutilisateur) REFERENCES utilisateur(id),
    FOREIGN KEY (idmagasin) REFERENCES magasin(id)
);

-- Données de test pour utilisateur_magasin
INSERT INTO utilisateur_magasin (id, idutilisateur, idmagasin, datedebut, datefin)
VALUES ('UM1', 'EMP1', 'MAG1', '2024-01-01', '2021-12-31'),
        ('UM3', 'EMP1', 'MAG2', '2022-01-01', null),
       ('UM2', 'EMP2', 'MAG2', '2024-01-01', null);

-- -------------------------------------------------------------------------------------------------  
-- Création de la table unite
CREATE TABLE unite (
    id VARCHAR(50) PRIMARY KEY,
    nom VARCHAR(200)
);

INSERT INTO unite (id, nom)
VALUES ('UNIT1', 'Unite'),
       ('UNIT2', 'Plaquette');

-- ------------------------------------------------------------------------------------------------- 
-- Création de la table categorie
CREATE TABLE categorie (
    id VARCHAR(50) PRIMARY KEY,
    nom VARCHAR(200)
);

INSERT INTO categorie (id, nom)
VALUES ('CAT1', 'Antibiotique'),
        ('CAT3', 'Anti-inflammatoire'),
       ('CAT2', 'Antalgique');

-- -------------------------------------------------------------------------------------------------

-- Création de la table produit
CREATE TABLE produit (
    id VARCHAR(50) PRIMARY KEY,
    nom VARCHAR(200),
    idunite VARCHAR(50),
    idcategorie VARCHAR(50),
    pu DECIMAL(10, 2),
    pv DECIMAL(10, 2),
    FOREIGN KEY (idunite) REFERENCES unite(id),
    FOREIGN KEY (idcategorie) REFERENCES categorie(id)
);

INSERT INTO produit (id, nom, idunite, idcategorie, pu, pv)
VALUES ('PROD1', 'PARACETAMOL', 'UNIT2', 'CAT1', 10.5, 12.00),
       ('PROD2', 'VOLTINAX', 'UNIT1', 'CAT3', 10, 15);

-- -------------------------------------------------------------------------------------------------
-- Création de la table boncommande
CREATE TABLE boncommande (
    id VARCHAR(50) PRIMARY KEY,
    ref VARCHAR(200)
);

INSERT INTO boncommande (id, ref)
VALUES ('BC1', 'Ref-001'),
       ('BC2', 'Ref-002');

-- -------------------------------------------------------------------------------------------------
-- Création de la table fournisseur
CREATE TABLE fournisseur (
    id VARCHAR(50) PRIMARY KEY,
    nom VARCHAR(200)
);

INSERT INTO fournisseur (id, nom)
VALUES ('FR1', 'Floreal'),
       ('FR2', 'HOMEOPHARMA');

-- -------------------------------------------------------------------------------------------------
-- Création de la table livraiso
CREATE TABLE livraison (
    id VARCHAR(50) PRIMARY KEY,
    date DATE,
    idmagasin VARCHAR(50),
    idboncommande VARCHAR(50),
    FOREIGN KEY (idmagasin) REFERENCES magasin(id),
    FOREIGN KEY (idboncommande) REFERENCES boncommande(id)
);

ALTER TABLE livraison
ADD idfournisseur VARCHAR(50);

ALTER TABLE livraison
ADD CONSTRAINT FK_Livraison_Fournisseur
FOREIGN KEY (idfournisseur) REFERENCES fournisseur(id);



-- -------------------------------------------------------------------------------------------------
CREATE TABLE detaillivraison (
    id VARCHAR(50) PRIMARY KEY,
    idlivraison VARCHAR(50),
    idproduit VARCHAR(50),
    pu_produit DECIMAL(10, 2),
    qte INTEGER,
    total_achat DECIMAL(10, 2),
    FOREIGN KEY (idlivraison) REFERENCES livraison(id),
    FOREIGN KEY (idproduit) REFERENCES produit(id)
);

-- ---------------------------------------------------------------------------------------------------
-- Création de la table CLIENT
CREATE TABLE client (
    id VARCHAR(50) PRIMARY KEY,
    nom VARCHAR(200),
    email VARCHAR(200),
    contact VARCHAR(200)
);

INSERT INTO client (id, nom, email, contact) VALUES
('1', 'Alice Dupont', 'alice.dupont@example.com', '0612345678'),
('2', 'Jean Martin', 'jean.martin@example.com', '0623456789'),
('3', 'Marie Durand', 'marie.durand@example.com', '0634567890');


-- ----------------------------------------------------------------------------------------------
CREATE TABLE facture(
    id varchar(50) PRIMARY KEY,
    date_f DATE,
    idclient varchar(50),
    idmagasin varchar(50),
    remarque varchar(200),
    total_facture decimal(10, 2),
    FOREIGN KEY (idclient) REFERENCES client(id),
    FOREIGN KEY (idmagasin) REFERENCES magasin(id)
);

-- ---------------------------------------------------------------------------------------------
CREATE TABLE detailfacture(
    id varchar(50) PRIMARY KEY,
    idfacture varchar(50),
    idproduit varchar(50),
    pu_produit DECIMAL(10, 2),
    qte INTEGER,
    total_vente DECIMAL(10, 2),
    FOREIGN KEY (idfacture) REFERENCES facture(id),
    FOREIGN KEY (idproduit) REFERENCES produit(id)
);