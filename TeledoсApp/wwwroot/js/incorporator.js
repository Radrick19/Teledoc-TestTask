let tableHandler;
document.addEventListener("DOMContentLoaded", function () {
	tableHandler = document.querySelector('.content-table');
	OpenIncorporatorsTable();
})

function OpenIncorporatorsTable() {
	$.get("/get/incorporatorstable", function (data) {
		tableHandler.innerHTML = data
	});
}
