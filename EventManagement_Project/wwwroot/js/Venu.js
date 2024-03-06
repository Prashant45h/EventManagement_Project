document.addEventListener('DOMContentLoaded', function (event) {

	LoadVenus();
	$('#venueImage').hide();


});
function Venusave() {

    var venumodel = [];
    venumodel.push($("#txtVenuID").val());

	var VenuName = $("#txtVenuName").val();
	if (VenuName == "") {
		alert("Please Enter VenuName !");
		return false;
	} else {
		venumodel.push(VenuName);
	}

	venumodel.push("");
	venumodel.push(txtvenuFilepath);

	venumodel.push($("#txtCreatedby").val());



	var Venucost = $("#txtVenucost").val();
	if (Venucost == "") {
		alert("Please Enter Venucost !");
		return false;
	} else {
		venumodel.push(Venucost);
	}

	var fileInput = document.getElementById("txtvenuFilepath"); 

	var formData = new FormData();
	formData.append("txtvenuFilepath", fileInput.files[0]);



    $.each(venumodel, function (index, value) {
        formData.append("venumodel[]", value);
	});

    $.ajax({
        type: "POST",
		url: '/Venu/SaveVenu',
        data: formData,
        contentType: false,
		processData: false,


		success: function (data)
		{

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
					timer: 1000,
				})



				LoadVenus();
				$('#displaySection').show();
				$('#editSection').hide();
				$('#showAllVenuesLink').hide();

            }
        },
        error: function () {
        }
    });
}

var txtvenuFilepath;

function editdata(Data) {
	$('#displaySection').hide();

	$.ajax({
		type: "GET",
		url: "/Venu/Getdataforedit",
		data: { VenuID: Data },
		success: function (data) {
			if (!data.isSuccess) {
				$('._CustomMessage').text(data.message);
			} else {
				$('._CustomMessage').text(data.message);
				if (data.dataList) {
					$('#txtVenuID').val(data.dataList.venuID);
					$('#txtVenuName').val(data.dataList.venuName);
					$('#txtVenucost').val(data.dataList.venuCost);
					$('#venueImage').attr('src', data.dataList.venuFilepath);
					$('#txtvenuFilepath').text(data.dataList.venuFilepath);

					$('#filenameDisplay').text(data.dataList.venuFilename);
					txtvenuFilepath = (data.dataList.venuFilepath);
					$('#editSection').show();
					$('#Image').hide();
					$('#venueImage').show();
					$('#abcds').hide();

				}
			}
		},
		error: function () {
			alert("An error occurred while updating details.");
		}
	});
}


function LoadVenus() {
	$.ajax({
		type: "POST",
		url: "/Venu/LoadallVenus",
		data: {},
		success: function (Obj) {
			if (!Obj.isSuccess) {
				$('._CustomMessage').text(Obj.message);
				$('#successPopup').modal('show');
			}
			else {
				LoadVenuDeatils(Obj.list);
			}
		},
		error: function () {

		}
	});
}
function LoadVenuDeatils(List) {
	$('#tblVenudetails').empty();
	$('#tblVenudetails').dataTable({
		"pageLength": 10,
		"Processing": true,
		"destroy": true,
		"aaData": List,
		"columns": [

			
			{ "data": "venuName", "title": "Venu Name", "width": "70px" },
			{ "data": "venuCost", "title": "Venu Cost", "width": "70px" },
			{
				"data": "createdate", "title": "Createdate", "width": "70px",
				"render": function (data) {
					return formatDate(data);
				}

			},

			{
				"data": "venuID",
				"width": "70px",
				"title": "Action",
				"class": "text-center",
				"orderable": false,
				"render": function (data) {
					return `<button type="button" class="dt-btn-approve" style="background-color: green; color: white;" onclick="editdata('` + data.toString() + `')"> Edit </button>` +
						`&nbsp;&nbsp;` +
						`<button type="button" class="dt-btn-reject" style="background-color: red; color: white;" onclick="VenuDeleteData('` + data.toString() + `')"> Delete </button>`;
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

function VenuDeleteData(deletevenu) {
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
				url: "/Venu/DeleteVenuData",
				data: { deletevenu },
				success: function (data) {
					if (!data.isSuccess) {
						$('._CustomMessage').text(data.message);
						$('#errorPopup').modal('show');
					} else {
						$('._CustomMessage').text(data.message);
						$('#successPopup').modal('show');
						window.location.replace('/venu/VenuDetails');
						LoadVenus();
						$('#editSection').hide();
						$('#displaySection').show();
					

					}
				},
				error: function () {
				}
			});
		}
	});
}
