
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var myChart;
var sugar; // To store each global instance of the data on start up
var wheat;
var cocoa;
var springWheat;

window.onload = function () {
        populateAlerts(); // TODO put data here
            //console.log("called");
            //document.getElementById("Alerts").innerHTML = '';
    sugar = extractPreviousData(SugarData);
    wheat = extractPreviousData(WheatData);
    cocoa = extractPreviousData(CocoaData);
    springWheat = extractPreviousData(SpringWheatData);

    populatePriceGraph();
}

function extractPreviousData(ingredient) {
    let myArray = [];
    const prev = ingredient.map(item => item.previous);
    var previousData = prev;

    var dataSetPrevious = Object.keys(previousData).map(key => ({
        label: key,
        data: previousData[key],
    }));

    dataSetPrevious.forEach(function (data) {
        myArray = myArray.concat(data.data);
    });

    myArray = myArray.map(item => parseFloat(item));
    console.log("Data from function: " + myArray);
    return myArray;
}

function populateAlerts() {
    //let alertItem = document.createElement('li');
    //document.getElementById("Alerts").appendChild(alertItem);
    /* /To implement when data is in the backend
    console.log("Alerts: ", window.alert);

    const prev = window.alert.map(item => item.previous);
    var previousData = prev;
    console.log("Data for Previous Column: " + previousData);

    var dataSetPrevious = Object.keys(previousData).map(key => ({
        label: key,
        data: previousData[key],
    }));

    console.log("Data from function", dataSetPrevious);

    dataSetPrevious.forEach(function (data) {
        myArray = myArray.concat(data.data);
    });

    myArray = myArray.map(item => parseFloat(item));
    
    
    */
}

function timer() {
    let counter = 0;

    setInterval(() => {
        console.log("Value: " + counter);
        populateAlerts(counter);
    }, 60000); // Every minute
}

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
                label: 'Cocoa', // For Cocoa
                data: cocoa,  
                backgroundColor: 'brown',
                borderColor: 'brown',
                borderWidth: 4,
                borderRadius: 5,
            },
            {
                label: 'Wheat', // For Wheat
                data: wheat,  
                backgroundColor: 'black',
                borderColor: 'black',
                borderWidth: 4,
                borderRadius: 5,
            },
            {
                label: 'Sugar', // For sugar
                data: sugar,  
                backgroundColor: 'blue',
                borderColor: 'blue',
                borderWidth: 4,
                borderRadius: 5,
            },
            {
                label: 'Spring Wheat', // For sugar
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
                        text: 'Price Per Unit ($)',
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
                            size: 12,
                            weight: 'bold'
                        }
                    }
                },
                title: {
                    display: true,
                    text: 'Tariff Percentage by Country',
                    font: {
                        size: 18
                    },
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
    if (myChart) {
        myChart.destroy();
    }

    populateInventoryGraph();
});

document.getElementById("Price").addEventListener('click', function () {
    if (myChart) {
        myChart.destroy();
    }

    populatePriceGraph();
});

document.getElementById("Tariff").addEventListener('click', function () {
    if (myChart) {
        myChart.destroy();
    }

    populateTariffGraph();
});




