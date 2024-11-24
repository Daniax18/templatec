function BarChart(url, date1, date2) {
    $(document).ready(function () {
        // Requête AJAX pour obtenir les données
        $.ajax({
            url: url,
            type: 'GET',
            data: { startDate: date1, endDate: date2 },
            success: function (response) {
                // Initialisez le graphique en utilisant les données reçues
                var barChartCanvas = $("#barChart").get(0).getContext("2d");

                var barChartOptions = {
                    scaleBeginAtZero: true,
                    scaleShowGridLines: true,
                    scaleGridLineColor: "rgba(0,0,0,.05)",
                    scaleGridLineWidth: 1,
                    scaleShowHorizontalLines: true,
                    scaleShowVerticalLines: true,
                    barShowStroke: true,
                    barStrokeWidth: 2,
                    barValueSpacing: 5,
                    barDatasetSpacing: 1,
                    responsive: true,
                    maintainAspectRatio: true
                };

                var barChart = new Chart(barChartCanvas);
                barChartOptions.datasetFill = false;
                barChart.Bar(response, barChartOptions);
            },
            error: function () {
                alert("Une erreur s'est produite lors du chargement des données.");
            }
        });
    });
}