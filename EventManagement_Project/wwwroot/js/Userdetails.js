document.addEventListener('DOMContentLoaded', function (event) {

   Loaduserdetails() 
});
function Loaduserdetails() {
	$.ajax({
		type: "POST",
		url: "/SignIn/LoadUsers",
		data: {},
		success: function (Obj) {
			if (!Obj.isSuccess) {
				
			}
			else {
				LoadAllUsers(Obj.list);
			}
		},
		error: function () {

		}
	});
}
function LoadAllUsers(List) {
	$('#tbluserdetails').empty();
	$('#tbluserdetails').dataTable({
		"pageLength": 10,
		"Processing": true,
		"destroy": true,
		"aaData": List,
		"columns": [

			{ "data": "name", "title": "Name", "width": "70px" },
			{ "data": "username", "title": "User Name", "width": "70px" },
			{ "data": "mobileno", "title": "Mobile No", "width": "70px" },
			{ "data": "emailID", "title": "Email ID", "width": "70px" },
			{ "data": "gender", "title": "Gender", "width": "70px" },
			{
				"data": "birthdate", "title": "BirthDate", "width": "70px",

				"render": function (data) {
					return formatDate(data);
				}
			},


			{ "data": "countryName", "title": "Country", "width": "70px" },
			{ "data": "stateName", "title": "State", "width": "70px" },
			{ "data": "cityName", "title": "City", "width": "70px" },
			{ "data": "address", "title": "Address", "width": "70px" },
			{
				"data": "createdOn", "title": "CreatedOn", "width": "70px",
				"render": function (data) {
					return formatDate(data);
				}
			},
		]
	});
}

function formatDate(dateString) {
	if (!dateString) return "";

	var date = new Date(dateString);

	var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
		"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
	var month = monthNames[date.getMonth()];

	var day = date.getDate();
	var year = date.getFullYear();

	return day + "-" + month + "-" + year;
}

