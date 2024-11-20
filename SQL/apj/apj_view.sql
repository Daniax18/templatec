CREATE VIEW v_utilisateur_magasin_lib
AS
SELECT 
    utilisateur.id,
    utilisateur.refuser,
    utilisateur.nomuser,
    utilisateur.pwduser,
    utilisateur_magasin.id AS magasin_id
FROM utilisateur_magasin
JOIN utilisateur ON utilisateur_magasin.idutilisateur = utilisateur.id
WHERE utilisateur_magasin.datefin IS NULL;

CREATE VIEW v_utilisateur_role
AS
SELECT 
    utilisateur.id,
    utilisateur.refuser,
    utilisateur.nomuser,
    roles.rang
FROM utilisateur
JOIN roles ON roles.idrole = utilisateur.idrole;