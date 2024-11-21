function pieChart(url) {
    $(document).ready(function () {
        // Requête AJAX pour obtenir les données
        $.ajax({
            url: url,
            type: 'GET',
            success: function (response) {
                // Transformez les données en format compatible avec Chart.js
                var pieData = response.map(function (item) {
                    return {
                        value: item.value,
                        color: item.color,
                        highlight: item.color,
                        label: item.label
                    };
                });

                // Options du graphique
                var pieOptions = {
                    segmentShowStroke: true,
                    segmentStrokeColor: "#fff",
                    segmentStrokeWidth: 2,
                    percentageInnerCutout: 50,
                    animationSteps: 100,
                    animationEasing: "easeOutBounce",
                    animateRotate: true,
                    animateScale: false,
                    responsive: true,
                    maintainAspectRatio: true,
                };

                // Initialisation du graphique
                var pieChartCanvas = $("#pieChart").get(0).getContext("2d");
                var pieChart = new Chart(pieChartCanvas);
                pieChart.Doughnut(pieData, pieOptions);
            },
            error: function () {
                alert("Une erreur s'est produite lors du chargement des données.");
            }
        });
    });
}