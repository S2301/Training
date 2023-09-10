// Set fixed prices for each vehicle category
const prices = {
    "Bike": 100,
    "LMV": 200,
    "HMV": 300,
    "Heavy Truck": 400
};

// Initialize statistics object
let statistics = {
    "Bike": 0,
    "LMV": 0,
    "HMV": 0,
    "Heavy Truck": 0
};

// Initialize entries array
let entries = [];

// Check if there is data in localStorage
if (localStorage.getItem("statistics") !== null) {
    statistics = JSON.parse(localStorage.getItem("statistics"));
}

if (localStorage.getItem("entries") !== null) {
    entries = JSON.parse(localStorage.getItem("entries"));
}

// Update statistics table
function updateStatisticsTable() {
    $("#statisticsTable tr").not(":first").remove();
    $.each(statistics, function(key, value) {
        $("#statisticsTable").append("<tr><td>" + key + "</td><td>" + value + "</td></tr>");
    });
}
updateStatisticsTable();

// Handle form submission
$("#entryForm").submit(function(event) {
    event.preventDefault();
    let vehicleCategory = $("#vehicleCategory").val();
    let vehicleRegNo = $("#vehicleRegNo").val();
    let tollAmount = prices[vehicleCategory];
    statistics[vehicleCategory] += tollAmount;

    entries.push({
        vehicleCategory: vehicleCategory,
        vehicleRegNo: vehicleRegNo,
        tollAmount: tollAmount
    });

    localStorage.setItem("statistics", JSON.stringify(statistics));
    localStorage.setItem("entries", JSON.stringify(entries));

    updateStatisticsTable();

    $("#entryForm")[0].reset();
});



// Handle search form submission
$("#searchForm").submit(function(event) {
    event.preventDefault();

    let searchCategory = $("#searchCategory").val();
    let searchRegNo = $("#searchRegNo").val();

    let filteredEntries = entries.filter(function(entry) {
        return (searchCategory === "" || entry.vehicleCategory === searchCategory) && (searchRegNo === "" || entry.vehicleRegNo === searchRegNo);
    });

    $("#searchResultsTable tr").not(":first").remove();

    $.each(filteredEntries, function(index, entry) {
        $("#searchResultsTable").append("<tr><td>" + entry.vehicleCategory + "</td><td>" + entry.vehicleRegNo + "</td><td>" + entry.tollAmount + "</td></tr>");
    });
});