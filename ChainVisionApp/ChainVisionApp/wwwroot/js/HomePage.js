
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var myChart;
var sugar; // To store each global instance of the data on start up
var wheat;
var cocoa;
var alerts;
var flour;
var sugar;
var milk;
var butter;
var springWheat;
var myArray = [];

window.onload = function () {
    extractPreviousData(alertData);
    console.log("Array here: " + myArray);
    populateAlerts(); 
    sugar = extractPreviousData(SugarData);
    wheat = extractPreviousData(WheatData);
    cocoa = extractPreviousData(CocoaData);
    springWheat = extractPreviousData(SpringWheatData);

    populatePriceGraph();
}

function extractPreviousData(ingredient) {
    myArray = [];
    
    const prev = ingredient.map(item => item.previous);
    const alert = ingredient.map(item => String(item.title));
    console.log("Here are the alerts: " + alert);
    var previousData;
    if (ingredient == alertData) {
        previousData = alert;
        console.log("Making alert");
    } else {
        previousData = prev;
    }

    var dataSetPrevious = Object.keys(previousData).map(key => ({
        label: key,
        data: previousData[key],
    }));

    dataSetPrevious.forEach(function (data) {
        myArray = myArray.concat(data.data);
    });

    myArray = myArray.filter(item => item !== null && item !== undefined);
    console.log("Data from function: " + myArray);
    return myArray;
}

function populateAlerts() {
    const alertList = document.getElementById("Alerts");
    alertList.innerHTML = ''; 

    myArray.forEach(alertMessage => {
        let alertItem = document.createElement('li');
        alertItem.textContent = alertMessage;
        alertList.appendChild(alertItem);
    });
}


document.getElementById("Inventory").addEventListener('click', function () {
    if (myChart) {
        myChart.destroy();
    }
    console.log("Inventory");
    populateInventoryGraph();
});

document.getElementById("Price").addEventListener('click', function () {
    if (myChart) {
        myChart.destroy();
    }
    console.log("price")
    populatePriceGraph();
});

document.getElementById("Tariff").addEventListener('click', function () {
    if (myChart) {
        myChart.destroy();
    }
    console.log("tariff")
    populateTariffGraph();
});
function populateInventoryGraph() {
    if (myChart) {
        myChart.destroy();
    }

    var inv = document.getElementById('Canvas-Main').getContext('2d');
    myChart = new Chart(inv, {
        type: 'bar',
        data: {
            labels: ['Mar 25','May 25','Jul 25','Oct 25','Mar 26','May 26'],
            datasets: [
                {
                    label: 'Cocoa',
                    data: [21.57, 20.27, 19.58, 39.42, 19.55, 18.54, 18.06],
                    backgroundColor: 'brown',
                    borderColor: 'brown',
                    borderWidth: 4
                },
                {
                    label: 'Wheat',
                    data: [23.57, 23.27, 24.58, 25.42, 27.55, 29.54, 33.06],
                    backgroundColor: 'black',
                    borderColor: 'black',
                    borderWidth: 1
                },
                {
                    label: 'Sugar',
                    data: [22, 34, 21, 12, 33, 22, 2, 2, 22, 22, 22, 22, 22],
                    backgroundColor: 'blue',
                    borderColor: 'blue',
                    borderWidth: 8
                },
                {
                    label: 'Spring Wheat',
                    data: [22, 34, 21, 12, 33, 22, 2, 2, 22, 22, 22, 22, 22],
                    backgroundColor: 'Purple',
                    borderColor: 'Purple',
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
                        size: 18,
                        weight: 'bold'
                    },
                    padding: {
                        bottom: 3
                    }
                }
            },
            scales: {
                x: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Month',
                        font: {
                            size: 12
                        }
                    },
                    grid: {
                        display: true,
                    }
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Amount of Inentory in Units',
                        font: {
                            size: 14
                        }
                    },
                    grid: {
                        display: true,
                    }
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
            labels: ['Mar 25', 'May 25', 'Jul 25', 'Oct 25', 'Mar 26', 'May 26'],
            datasets: [{
                label: 'Cocoa', 
                data: cocoa,  
                backgroundColor: 'brown',
                borderColor: 'brown',
                borderWidth: 4,
                borderRadius: 5,
            },
            {
                label: 'Wheat', 
                data: wheat,  
                backgroundColor: 'black',
                borderColor: 'black',
                borderWidth: 4,
                borderRadius: 5,
            },
            {
                label: 'Sugar', 
                data: sugar,  
                backgroundColor: 'blue',
                borderColor: 'blue',
                borderWidth: 4,
                borderRadius: 5,
            },
            {
                label: 'Spring Wheat',
                data: springWheat,  
                backgroundColor: 'purple',
                borderColor: 'purple',
                borderWidth: 4,
                borderRadius: 5,
            }
        ]
        },
        options: {
            plugins: {
                title: {
                    display: true,
                    text: 'High Severity Price Per Unit',
                    font: {
                        size: 18, 
                        weight: 'bold'  
                    },
                }
            },
            scales: {
                x: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Month',
                        font: {
                            size: 12
                        }
                    },
                    grid: {
                        display: true,
                    }
                },
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Price Per Unit in cents',
                        font: {
                            size: 14
                        }
                    },
                    grid: {
                        display: true,
                    }
                }
            },
            legend: {
                position: 'bottom', 
                labels: {
                    boxWidth: 20,  
                    padding: 15,  
                }
            }
        }
    });
}

function populateTariffGraph() {
    if (myChart) {
        myChart.destroy();
    }
    var tar = document.getElementById('Canvas-Main').getContext('2d');

    myChart = new Chart(tar, {
        type: 'line',
        data: {
            labels: ['CAD', 'USA', 'MEX'],
            datasets: [
                {
                    label: 'Canada',
                    data: [0.33, 0.34, 0.23],  
                    backgroundColor: 'rgba(255, 99, 132, 0.2)', 
                    borderColor: 'rgba(255, 99, 132, 1)',  
                    borderWidth: 2,
                    fill: false,
                },
                {
                    label: 'USA',
                    data: [0.44, 0.55, 0.56],  
                    backgroundColor: 'rgba(54, 162, 235, 0.2)',  
                    borderColor: 'rgba(54, 162, 235, 1)', 
                    borderWidth: 2,
                    fill: false,
                },
                {
                    label: 'Mexico',
                    data: [0.23, 0.12, 0.1],  
                    backgroundColor: 'rgba(75, 192, 192, 0.2)',  
                    borderColor: 'rgba(75, 192, 192, 1)',  
                    borderWidth: 2,
                    fill: false,
                }
            ]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'top',
                    labels: {
                        font: {
                            size: 12,
                            weight: 'bold'
                        }
                    }
                },
                title: {
                    display: true,
                    text: 'Tariff Percentage by Country',
                    font: {
                        size: 18,
                        weight: 'bold'
                    },
                    padding: {
                        bottom: 20
                    }
                }
            },
        }
    });


}







