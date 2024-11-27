// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
//import Chart from 'chart.js';

function timer() {
    let counter = 0; 

    setInterval(() => {
        console.log("Value: " + counter);
        populateAlerts(counter);
    }, 60000); // Every minute
}

function populateAlerts(alert) {
    let alertItem = document.createElement('li');
    alertItem.innerText = "test"
    document.getElementById("Alerts").appendChild(alertItem);
}



window.onload = function () {
    var ctx = document.getElementById('Price-Chart').getContext('2d');

    var Price = new Chart(ctx, {
        type: 'line',
        data: {
            labels: ['Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'June'], // X-axis labels
            datasets: [{
                label: 'Chocolate',
                data: [21.57, 20.27, 19.58, 30.42, 19.55, 18.54, 18.06], 
                backgroundColor: 'brown', 
                borderColor: 'brown', 
                borderWidth: 1
            },
                {
                    label: 'Flour', 
                    data: [23.57, 23.27, 24.58, 25.42, 27.55, 29.54, 33.06], 
                    backgroundColor: 'black',
                    borderColor: 'black',
                    borderWidth: 1
                }
            ]
        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    });

    var inv = document.getElementById('Inventory-Chart').getContext('2d');

    var Inv = new Chart(inv, {
        type: 'bar', 
        data: {
            labels: ['Dec', 'Jan', 'Feb', 'Mar', 'Apr', 'May', 'June'], 
            datasets: [
                {
                    label: 'Coco', 
                    data: [21.57, 20.27, 19.58, 30.42, 19.55, 18.54, 18.06], 
                    backgroundColor: 'purple', 
                    borderColor: 'purple', 
                    borderWidth: 1 
                },
                {
                    label: 'Bananas', 
                    data: [23.57, 23.27, 24.58, 25.42, 27.55, 29.54, 33.06],
                    backgroundColor: 'orange', 
                    borderColor: 'orange', 
                    borderWidth: 1 
                }
            ]
        },
        options: {
            scales: {
                x: {
                    barPercentage: 0.8, 
                    categoryPercentage: 0.9
                },
                y: {
                    beginAtZero: true 
                }
            }
        }
    });

    var tar = document.getElementById('Tarrif-Chart').getContext('2d');

    var tarrif = new Chart(tar, {
        type: 'pie',
        data: {
            labels: ['CAD','USA','MEX'],
            datasets: [
                {
                    label: '',
                    data: [12,2,3],
                    backgroundColor: [
                        'red',
                        'blue', 
                        'yellow'
                    ],
                    borderWidth: 1
                },
            ]
        },
        options: {
            scales: {
                x: {
                    barPercentage: 0.8,
                    categoryPercentage: 0.9
                },
                legend: {
                    position: 'top',
                },
                y: {
                    beginAtZero: true
                }
            }
        }
    });

};