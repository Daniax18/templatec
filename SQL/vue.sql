CREATE VIEW v_produit_complet
AS
select
	produit.*,
	unite.nom as unite_nom
from produit
join unite on unite.id = produit.idunite;


CREATE VIEW v_detail_livraison_complet
AS
select
	d.*,
	vp.nom,
	vp.unite_nom
from detaillivraison as d
join v_produit_complet vp on vp.id = d.idproduit;

CREATE VIEW v_livraison_complet
AS
select
	l.*,
	m.nom as m_nom,
	f.nom as f_nom
from livraison l
join magasin m on m.id = l.idmagasin
join fournisseur f on f.id = l.idfournisseur;

CREATE VIEW v_facture_complet
AS
select
	l.*,
	m.nom as m_nom,
	f.nom as c_nom
from facture l
join magasin m on m.id = l.idmagasin
join client f on f.id = l.idclient;

CREATE VIEW v_detail_facture_complet
AS
select
	d.*,
	vp.nom,
	vp.unite_nom
from detailfacture as d
join v_produit_complet vp on vp.id = d.idproduit;