document.addEventListener('DOMContentLoaded', function (event) {

	Loaddetails()
});
function Loaddetails() {
	$.ajax({
		type: "POST",
		url: "/AllBookings/LoadBookingdetails",
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
				"title": "Show Order",
				"class": "text-center",
				"orderable": false,
				"render": function (data, bookingApprovel, row) {
					if (row.bookingApprovel === "Approved") {
						return `<button type="button" class="dt-btn-approve" style="background-color: #45b3e7; color: white;" onclick="ShowOrder('` + data.toString() + `')"> Show Order </button>`

					} else if (row.bookingApprovel === "Pending") {
						return `<a type="text" > Pending </a>`

					} else if (row.bookingApprovel === "Cancelled") {
						return `<a type="text" > Cancelled </a>`

					} else
						if (row.bookingApprovel === "Reject") {
							return `<a type="text"> Reject </a>`
						}
				}
			},

			{
				"data": "bookingID",
				"width": "70px",
				"title": "Cancle Booking",
				"class": "text-center",
				"orderable": false,
				"render": function (data, bookingApprovel, row) {
					if (row.bookingApprovel === "Approved") {
						return `<button type="button" class="dt-btn-approve" style="background-color: #e30022 ; color: white;" onclick="Cancel('` + data.toString() + `')"> Cancel Booking </button>`

					} else if (row.bookingApprovel === "Pending") {
						return `<a type="text" >.......</a>`

					} else if (row.bookingApprovel === "Cancelled") {
						return `<a type="text" >.......</a>`

					} else
						if (row.bookingApprovel === "Reject") {
							return `<a type="text">.......</a>`
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


function ShowOrder(Data) {
	$('#displaySection').hide();

	$.ajax({
		type: "GET",
		url: "/AllBookings/Getdataforedit",
		data: { BookingNo: Data },

		success: function (data) {
			if (!data.isSuccess) {
				$('._CustomMessage').text(data.message);
			} else {
				$('._CustomMessage').text(data.message);
				if (data) {
					$('#txtvenuename').val(data.venuName);
     				$('#txtvenucost').val(data.venuCost);

					$('#txtEquipmentname').val(data.equipmentNames);
					$('#txtequipmentprice').val(data.equipmentCosts);

					$('#txtFoodType').val(data.foodType);
					$('#txtMealType').val(data.mealType);
					$('#txtFoodName').val(data.foodNames);
					$('#txtfoodprice').val(data.foodCosts);

					$('#txtLightType').val(data.lightType);
					$('#txtLightName').val(data.lightName);
					$('#txtLightCost').val(data.lightCost);

					$('#txtFlowerName').val(data.flowerName);
					$('#txtFlowerCost').val(data.flowerCost);


					$('#txtbookingID').val(data.bookingID);
					$('#txtbookingno').val(data.bookingNo);
					$('#txtbookingdare').val(formatDate(data.bookingDate)); 

					debugger;
					var venuCost = parseFloat(data.venuCost);
					var equipmentCosts = parseAndReplace(data.equipmentCosts);
					var foodCosts = parseAndReplace(data.foodCosts);
					var lightCost = parseAndReplace(data.lightCost);
					var flowerCost = parseAndReplace(data.flowerCost);

					
					var totalAmount = venuCost + equipmentCosts + foodCosts + lightCost + flowerCost;

					if (typeof totalAmount === 'number' && !isNaN(totalAmount)) {
						$('#txttotalamount').val(totalAmount.toFixed(2));
					} else {
						console.error(error);
					}

					$('#editSection').show();
				}
			}
		},
		error: function (xhr, status, error) {
			console.error(xhr.responseText);
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



function Cancel(Data) {
	Swal.fire({
		title: "Are you sure?",
		text: "You won't be able to revert this!",
		icon: "warning",
		showCancelButton: true,
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		confirmButtonText: "Yes, Cancel it!"
	}).then((result) => {
		if (result.isConfirmed) {
			var DATA = [];
			DATA.push($("#txtbookingid").val());
			$.ajax({
				type: "POST",
				url: '/AllBookings/CancelOrder',
				data: { "BookingId": Data },
				success: function (data) {
					if (!data.isSuccess) {
						$('._CustomMessage').text(data.message);
						$('#errorPopup').modal('show');
					} else {
						Swal.fire({
							position: "top-end",
							icon: "success",
							title: data.message,
							showConfirmButton: false,
							timer: 2500,
						}).then((result) => {
							if (result.dismiss === Swal.DismissReason.timer) {
								window.location.replace('/AllBookings/ShowAllBookings');
							}

						});
					}
				},
				error: function () {
				}
			});
		}
	});

}

