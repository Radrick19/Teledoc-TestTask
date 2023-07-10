let tableHandler;
let selectedClientTypeHandler;
document.addEventListener("DOMContentLoaded", function () {
	tableHandler = document.querySelector('.content-table');
	OpenIndividualPersonsTable(document.querySelectorAll('.client-type-link')[0]); 
})

function OpenIndividualPersonsTable(self) {
	$.get("/get/iptable", function (data) {
		tableHandler.innerHTML = data
	});
	if (selectedClientTypeHandler != null) {
		selectedClientTypeHandler.style.border = "0px solid #000000"
	}
	if (self != null) {
		selectedClientTypeHandler = self;
		selectedClientTypeHandler.style.border = "1px solid #000000"
	}
}

function OpenLegalEntitiesTable(self) {
	$.get("/get/letable", function (data) {
		tableHandler.innerHTML = data
	});
	if (selectedClientTypeHandler != null) {
		selectedClientTypeHandler.style.border = "0px solid #000000"
	}
	if (self != null) {
		selectedClientTypeHandler = self;
		selectedClientTypeHandler.style.border = "1px solid #000000"
	}
}