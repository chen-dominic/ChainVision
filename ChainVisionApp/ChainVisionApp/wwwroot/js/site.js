// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function timer() {
    let counter = 0;

    setInterval(() => {
        console.log("Value: " + counter);
        populateAlerts(counter);
    }, 60000); // Every minute
}

function populateAlerts(alert) {
    let alertItem = document.createElement('li');
    alertItem.innerText = alert;
    document.getElementById("Alerts").appendChild(alertItem);
}

function populateInventoryGraph() {
    var inv = document.getElementById('Canvas-Main').getContext('2d');
    myChart = new Chart(inv, {
        type: 'bar',
        data: {
            labels: ['Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'June'],
            datasets: [
                {
                    label: 'Coco',
                    data: [21.57, 20.27, 19.58, 39.42, 19.55, 18.54, 18.06],

                    backgroundColor: 'brown',
                    borderColor: 'brown',
                    borderWidth: 4
                },
                {
                    label: 'Flour',
                    data: [23.57, 23.27, 24.58, 25.42, 27.55, 29.54, 33.06],
                    backgroundColor: 'black',
                    borderColor: 'black',
                    borderWidth: 1
                },
                {
                    label: 'Blue Berries',
                    data: [22, 34, 21, 12, 33, 22, 2, 2, 22, 22, 22, 22, 22],
                    backgroundColor: 'blue',
                    borderColor: 'blue',
                    borderWidth: 8
                }
            ]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: 'Live Inventory',
                    font: {
                        size: 15 // Font size for the title
                    },
                    padding: {
                        bottom: 3
                    }
                }
            },
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });
}

function populatePriceGraph() {
    var ctx = document.getElementById('Canvas-Main').getContext('2d');
    myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'June'], // X-axis labels
            datasets: [{
                label: 'Chocolate',
                data: [21.57, 20.27, 19.58, 30.42, 19.55, 18.54, 18.06],
                backgroundColor: 'brown',
                borderColor: 'brown',
                borderWidth: 4
            },
            {
                label: 'Flour',
                data: [23.57, 23.27, 24.58, 25.42, 27.55, 29.54, 33.06],
                backgroundColor: 'black',
                borderColor: 'black',
                borderWidth: 1
            },
            {
                label: 'Blue Berries',
                data: [22, 34, 21, 12, 33, 22, 2, 2, 22, 22, 22, 22, 22],
                backgroundColor: 'blue',
                borderColor: 'blue',
                borderWidth: 8
            }
            ]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: 'High Severity Price Per Unit',
                    font: {
                        size: 15
                    },
                }
            },
            options: {
                plugins: {
                    title: {
                        display: true,
                        text: 'Tariff Percentage by Country',
                        font: {
                            size: 15
                        },
                        padding: {
                            bottom: 3
                        }
                    }
                },
                scales: {
                    x: {
                        barPercentage: 0.8,
                        categoryPercentage: 0.9
                    },
                    legend: {
                        position: 'top',
                    },
                    y: {
                        beginAtZero: true,
                    }
                }
            }
        },
    });
}

function populateTariffGraph() {
    var tar = document.getElementById('Canvas-Main').getContext('2d');

    myChart = new Chart(tar, {
        type: 'pie',
        data: {
            labels: ['CAD', 'USA', 'MEX'],
            datasets: [{
                label: 'Tariff Distribution', // You can add a label if needed
                data: [0.33, 0.55, 0.1],
                backgroundColor: [
                    'red',
                    'blue',
                    'yellow'
                ],
                borderWidth: 1
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                    labels: {
                        font: {
                            size: 12
                        }
                    }
                },
                title: {
                    display: true,
                    text: 'Tariff Percentage by Country',
                    font: {
                        size: 15
                    },
                    padding: {
                        bottom: 3
                    }
                }
            },
            layout: {
                padding: {
                    top: 10,
                    right: 10,
                    bottom: 10,
                    left: 10
                }
            }
        }
    });
}


document.getElementById("Inventory").addEventListener('click', function () {
    myChart.destroy();

    populateInventoryGraph();
});

document.getElementById("Price").addEventListener('click', function () {
    myChart.destroy();

    populatePriceGraph();
});

document.getElementById("Tariff").addEventListener('click', function () {
    myChart.destroy();

    populateTariffGraph();
});


window.onload = function () {
    for (let i = 0; i < 50; i++) {
        populateAlerts(i); // TODO put data here
        if (i % 20 === 0) { //Clear the Alerts every 11 alerts generated
            console.log("called");
            document.getElementById("Alerts").innerHTML = '';
        }

        populatePriceGraph(); // For a default graph on load

    }
}