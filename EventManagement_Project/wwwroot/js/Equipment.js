document.addEventListener('DOMContentLoaded', function (event) {

	LoadEquipments();
	$('#EquipmentImage').hide();

});
function Equipmentsave() {

    var EquipmentModel = [];
    EquipmentModel.push($("#txtEquipmentID").val());

	var EquipmentName = $("#txtEquipmentName").val();
	if (EquipmentName == "") {
		alert("Please Enter EquipmentName !");
		return false;
	} else {
		EquipmentModel.push(EquipmentName);
	}

	EquipmentModel.push("");

	EquipmentModel.push($("#txtEquipmentFilepath").val());

    EquipmentModel.push($("#txtCreatedby").val());

	var Equipmentcost = $("#txtEquipmentcost").val();
	if (Equipmentcost == "") {
		alert("Please Enter Equipmentcost !");
		return false;
	} else {
		EquipmentModel.push(Equipmentcost);
	}


    var fileInput = document.getElementById("txtEquipmentFilepath");
    var files = fileInput.files;

    var formData = new FormData();
    formData.append("txtEquipmentFilepath", files[0]);

    $.each(EquipmentModel, function (index, value) {
        formData.append("EquipmentModel[]", value);
    });

    $.ajax({
        type: "POST",
        url: '/Equipment/SaveEquipment',
        data: formData,
        contentType: false,
        processData: false,
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
				Swal.fire({
					position: "top-end",
					icon: "success",
					title: data.message,
					showConfirmButton: false,
					timer: 2500,
				})
				LoadEquipments();

				$('#displaySection').show();
				$('#editSection').hide();
				$('#showAllEquipmentsLink').hide();

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
		url: "/Equipment/Getdataforedit",
		data: { EquipmentId: Data },
		success: function (data) {
			if (!data.isSuccess) {
				alert(data.message)
			} else {
				$('._CustomMessage').text(data.message);
				if (data.dataList) {
					$('#txtEquipmentID').val(data.dataList.equipmentId);
					$('#txtEquipmentName').val(data.dataList.equipmentName);
					$('#txtEquipmentcost').val(data.dataList.equipmentCost);
					$('#EquipmentImage').attr('src', data.dataList.equipmentFilepath);
					$('#txtEquipmentFilepath').attr(data.dataList.equipmentFilename);

					$('#editSection').show();
					$('#Image').hide();

					$('#EquipmentImage').show();

				}
			}
		},
		error: function () {
			alert("An error occurred while updating details.");
		}
	});
}


function LoadEquipments() {
	$.ajax({
		type: "POST",
		url: "/Equipment/LoadallEquipment",
		data: {},
		success: function (Obj) {
			if (!Obj.isSuccess) {
				$('._CustomMessage').text(Obj.message);
				$('#successPopup').modal('show');
			}
			else {
				LoadEquipmentDeatils(Obj.list);
			}
		},
		error: function () {

		}
	});
}
function LoadEquipmentDeatils(List) {
	$('#tblEquipmentdetails').empty();
	$('#tblEquipmentdetails').dataTable({
		"pageLength": 10,
		"Processing": true,
		"destroy": true,
		"aaData": List,
		"columns": [


			{ "data": "equipmentName", "title": "Equipment Name", "width": "70px" },
			{ "data": "equipmentCost", "title": "Equipment Cost", "width": "70px" },
			{
				"data": "createdate", "title": "Createdate", "width": "70px",
				"render": function (data) {
					return formatDate(data);
				}
			},

			{
				"data": "equipmentId",
				"width": "70px",
				"title": "Action",
				"class": "text-center",
				"orderable": false,
				"render": function (data) {
					return `<button type="button" class="dt-btn-approve" style="background-color: green; color: white;" onclick="editdata('` + data.toString() + `')"> Edit </button>` +
						`&nbsp;&nbsp;` +
						`<button type="button" class="dt-btn-reject" style="background-color: red; color: white;" onclick="EquipmentDeleteData('` + data.toString() + `')"> Delete </button>`;
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

function EquipmentDeleteData(deleteEquipment) {
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
				url: "/Equipment/DeleteEquipmentData",
				data: { deleteEquipment },
				success: function (data) {
					if (!data.isSuccess) {
						$('._CustomMessage').text(data.message);
						$('#errorPopup').modal('show');
					} else {
						$('._CustomMessage').text(data.message);
						$('#successPopup').modal('show');
						window.location.replace('/Equipment/EquipmentDetails');

						LoadEquipments();
					}
				},
				error: function () {
				}
			});
		}
	});
}
