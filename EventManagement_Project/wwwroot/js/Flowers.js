document.addEventListener('DOMContentLoaded', function (event) {

	LoadFlowers();
	$('#FlowerImage').hide();

});
function Flowersave() {

	var flowermodel = [];
	flowermodel.push($("#txtFlowerID").val());

	var FlowerName = $("#txtFlowerName").val();
	if (FlowerName == "") {
		alert("Please Enter FlowerName !");
		return false;
	} else {
		flowermodel.push(FlowerName);
	}

	flowermodel.push("");

	flowermodel.push($("#txtFlowerFilepath").val());

	flowermodel.push($("#txtCreatedby").val());


	var Flowercost = $("#txtFlowercost").val();
	if (Flowercost == "") {
		alert("Please Enter FlowerName !");
		return false;
	} else {
		flowermodel.push(Flowercost);
	}

	var fileInput = document.getElementById("txtFlowerFilepath");
	var files = fileInput.files;

	var formData = new FormData();
	formData.append("txtFlowerFilepath", files[0]);

	$.each(flowermodel, function (index, value) {
		formData.append("flowermodel[]", value);
	});

	$.ajax({
		type: "POST",
		url: '/Flower/SaveFlower',
		data: formData,
		contentType: false,
		processData: false,
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
				})
				LoadFlowers();
				$('#showAllFlowersLink').hide();
				$('#displaySection').show();
				$('#editSection').hide();

			}
		},
		error: function () {
		}
	});
}
function editdata(Data) {
	$('#displaySection').hide();

	$.ajax({
		type: "GET",
		url: "/Flower/Getdataforedit",
		data: { FlowerID: Data },
		success: function (data) {
			if (!data.isSuccess) {
				Swal.fire({
					position: "top-end",
					icon: "success",
					title: data.message,
					showConfirmButton: false,
					timer: 1000,
				})
			} else {
				$('._CustomMessage').text(data.message);
				if (data.dataList) {
					$('#txtFlowerID').val(data.dataList.flowerID);
					$('#txtFlowerName').val(data.dataList.flowerName);
					$('#txtFlowercost').val(data.dataList.flowerCost);
					$('#FlowerImage').attr('src', data.dataList.flowerFilepath);
					$('#Image').hide();

					$('#editSection').show();
					$('#FlowerImage').show();


				}
			}
		},
		error: function () {
			alert("An error occurred while updating details.");
		}
	});
}


function LoadFlowers() {
	$.ajax({
		type: "POST",
		url: "/Flower/LoadallFlowers",
		data: {},
		success: function (Obj) {
			if (!Obj.isSuccess) {
				$('._CustomMessage').text(Obj.message);
				$('#successPopup').modal('show');
			}
			else {
				LoadFlowerDeatils(Obj.list);
			}
		},
		error: function () {

		}
	});
}
function LoadFlowerDeatils(List) {
	$('#tblFlowerdetails').empty();
	$('#tblFlowerdetails').dataTable({
		"pageLength": 10,
		"Processing": true,
		"destroy": true,
		"aaData": List,
		"columns": [


			{ "data": "flowerName", "title": "Flower Name", "width": "70px" },
			{ "data": "flowerCost", "title": "Flower Cost", "width": "70px" },
			{
				"data": "createdate", "title": "Createdate", "width": "70px",
				"render": function (data) {
					return formatDate(data);
				}

			},

			{
				"data": "flowerID",
				"width": "70px",
				"title": "Action",
				"class": "text-center",
				"orderable": false,
				"render": function (data) {
					return `<button type="button" class="dt-btn-approve" style="background-color: green; color: white;" onclick="editdata('` + data.toString() + `')"> Edit </button>` +
						`&nbsp;&nbsp;` +
						`<button type="button" class="dt-btn-reject" style="background-color: red; color: white;" onclick="FlowerDeleteData('` + data.toString() + `')"> Delete </button>`;
				}
			},
		]
	});
}

function formatDate(dateString) {
	var date = new Date(dateString);

	var monthNames = ["Jan", "Feb", "Mar", "Apr", "May", "Jun",
		"Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
	var month = monthNames[date.getMonth()];

	var day = date.getDate();
	var year = date.getFullYear();

	return day + "-" + month + "-" + year;
}
function FlowerDeleteData(deleteFlower) {
	Swal.fire({
		title: "Are you sure?",
		text: "You won't be able to revert this!",
		icon: "warning",
		showCancelButton: true,
		confirmButtonColor: "#3085d6",
		cancelButtonColor: "#d33",
		confirmButtonText: "Yes, delete it!"
	}).then((result) => {
		if (result.isConfirmed) {
			$.ajax({
				type: "POST",
				url: "/Flower/DeleteFlowerData",
				data: { deleteFlower },
				success: function (data) {
					if (!data.isSuccess) {
						$('._CustomMessage').text(data.message);
						$('#errorPopup').modal('show');
					} else {
						$('._CustomMessage').text(data.message);
						$('#successPopup').modal('show');
						window.location.replace('/Flower/FlowerDetails');

						LoadFlowers();
					}
				},
				error: function () {
				}
			});
		}
	});
}
