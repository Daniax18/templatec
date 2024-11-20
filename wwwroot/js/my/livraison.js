function setupAutocomplete(inputSelector, hiddenFieldSelector, suggestionListSelector, ajaxUrl) {
    $(document).ready(function () {
        // Gestion de l'autocomplétion
        $(inputSelector).on("keyup", function () {
            var term = $(this).val(); // Récupère la valeur saisie

            // Ne fait rien si moins de 1 caractère
            if (term.length < 1) {
                $(suggestionListSelector).empty(); // Vide les suggestions
                return;
            }

            // Requête AJAX pour récupérer les suggestions
            $.ajax({
                url: ajaxUrl, // URL de l'action
                type: "GET",
                data: { term: term }, // Envoie le terme à chercher
                success: function (data) {
                    // Vide les anciennes suggestions
                    $(suggestionListSelector).empty();
                    console.log(data);
                    // Ajouter chaque suggestion reçue
                    data.forEach(function (item) {
                        $(suggestionListSelector).append(
                            "<li class='list-group-item' data-id='" + item.id + "'>" + item.id + " - "  + item.nom + "</li>"
                        );
                    });
                },
                error: function () {
                    console.error("Erreur lors de la recherche.");
                }
            });
        });

        // Remplir le champ lors du clic sur une suggestion
        $(document).on("click", suggestionListSelector + " li", function () {
            var nom = $(this).text(); // Texte de la suggestion sélectionnée
            var id = $(this).data("id"); // ID de la suggestion

            $(inputSelector).val(nom); // Remplit le champ texte
            $(hiddenFieldSelector).val(id); // Remplit le champ caché
            $(suggestionListSelector).empty(); // Vide les suggestions
        });
    });
}

//// Appel de la fonction pour le champ fournisseur
//setupAutocomplete(
//    "#fournisseurInput",          // Champ de texte
//    "#fournisseurId",             // Champ caché pour l'ID
//    "#suggestions",               // Liste des suggestions
//    "/Fournisseur/AutocompleteFournisseur" // URL de l'API
//);
