document.addEventListener('DOMContentLoaded', function (event) {

	Loaddetails()
});
function Loaddetails() {
	$.ajax({
		type: "POST",
		url: "/BookingDetails/LoadBookingdetail",
		data: {},
		success: function (Obj) {
			if (!Obj.isSuccess) {
				$('._CustomMessage').text(Obj.message);
				$('#successPopup').modal('show');
			}
			else {
				LoadAlldata(Obj.list);
			}
		},
		error: function () {

		}
	});
}

function LoadAlldata(List) {
	$('#tblbookingdetails').empty();
	$('#tblbookingdetails').dataTable({
		"pageLength": 10,
		"Processing": true,
		"destroy": true,
		"aaData": List,
		"columns": [

			{ "data": "name", "title": "Customer Name", "width": "70px" },

			{ "data": "bookingNo", "title": "Booking Number", "width": "70px" },
			

			{
				"data": "bookingDate", "title": "Booking Date", "width": "70px",
				"render": function (data) {
					return formatDate(data);
				}
			},
			{ "data": "bookingApprovel", "title": "Booking Approvel", "width": "70px" },
			{
				"data": "bookingApprovelDate", "title": "Booking Approvel Date", "width": "70px",
				"render": function (data) {
					return formatDate(data);
				}
			
			},
		
				
			{
				"data": "bookingNo",
				"width": "70px",
				"title": "Process",
				"class": "text-center",
				"orderable": false,
				"render": function (data, bookingApprovel,row) {
					if (row.bookingApprovel === "Pending") {
						return `<button type="button" class="dt-btn-approve" style="background-color: #45b3e7; color: white;" onclick="Viewdata('` + data.toString() + `')"> Process </button>` 

					} else if (row.bookingApprovel === "Approved") {
						return `<a type="text" > Approve </a>`

					} else if (row.bookingApprovel === "Cancelled") {
						return `<a type="text" > Cancelled </a>`
					} else
						if (row.bookingApprovel === "Reject") {
							return `<a type="text"> Reject </a>`
						}
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



function Approve() {

	var DATA = [];
	DATA.push($("#bookingno").val());
	DATA.push($("#bookingdate").val());
	$.ajax({
		type: "POST",
		url: '/BookingDetails/Approved',
		data: { "DATA": JSON.stringify(DATA) },
		success: function (data) {
			if (!data.isSuccess) {
				alert(data.message)

			} else {
				Swal.fire({
					position: "top-end",
					icon: "success",
					title: data.message,
					showConfirmButton: false,
					timer: 2500,
				}).then((result) => {
					if (result.dismiss === Swal.DismissReason.timer) {
						window.location.replace('/BookingDetails/BookingDetails');
					}

				});
			}
		},
		error: function () {
		}
	});

}

function REJECT() {
	var DATA = [];
	DATA.push($("#bookingno").val());
	DATA.push($("#bookingdate").val());
	$.ajax({
		type: "POST",
		url: '/BookingDetails/Rejected',
		data: { "DATA": JSON.stringify(DATA) },
		success: function (data) {
			if (!data.isSuccess) {
				alert(data.message)

			} else {
				Swal.fire({
					position: "top-end",
					icon: "success",
					title: data.message,
					showConfirmButton: false,
					timer: 2500,
				}).then((result) => {
					if (result.dismiss === Swal.DismissReason.timer) {
						window.location.replace('/BookingDetails/BookingDetails');
					}

				});
			}
		},
		error: function () {
		}
	});
}

function Viewdata(Data) {
	$('#displaySection').hide();

	$.ajax({
		type: "GET",
		url: "/BookingDetails/Getdataforedit",
		data: { BookingNo: Data },


		success: function (data) {
			if (!data.isSuccess) {
				$('._CustomMessage').text(data.message);
			} else {
				$('._CustomMessage').text(data.message);
				if (data) {
					$('#txtid').val(data.bookingID);

					$('#venucost').val(data.venueCost);
					$('#equipmentcost').val(data.equipmentCost);
					$('#foodcost').val(data.foodCost);
					$('#lightcost').val(data.lightCost);
					$('#flowercost').val(data.flowerCost);

					

					var venuCost = parseFloat(data.venueCost);
					var equipmentCosts = parseAndReplace(data.equipmentCost);
					var foodCosts = parseAndReplace(data.foodCost);
					var lightCost = parseAndReplace(data.lightCost);
					var flowerCost = parseAndReplace(data.flowerCost);

					var totalAmount = venuCost + equipmentCosts + foodCosts + lightCost + flowerCost;


					if (typeof totalAmount === 'number' && !isNaN(totalAmount)) {
						$('#totalamount').val(totalAmount.toFixed(2));
					} else {
						console.error(error);
					}
					$('#bookingno').val(data.bookingNo);
					$('#bookingdate').val(formatDate(data.bookingDate));



					$('#editSection').show();
				}
			}
		},
		error: function () {
			alert("An error occurred while updating details.");
		}
	});
}
function parseAndReplace(value) {
	debugger;

	if (Array.isArray(value)) {
		return value.reduce((sum, part) => sum + parseFloat(part), 0);
	} else if (typeof value === 'number') {
		return value;
	} else {
		return value;
	}
}
