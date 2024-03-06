document.addEventListener('DOMContentLoaded', function (event) {
    LoadLightings();
    $('#LightImage').hide();

});
function Lightingssave() {

    var lightingmodel = [];
    lightingmodel.push($("#txtLightid").val());

 
    var selectedLightType = $("input[name='LightType']:checked").val();
    lightingmodel.push(selectedLightType);


    var LightingName = $("#txtLightname").val();
    if (LightingName == "") {
        alert("Please Enter LightingName !");
        return false;
    } else {
        lightingmodel.push(LightingName);
    }

    lightingmodel.push("");

    lightingmodel.push($("#txtLightFilepath").val());

    lightingmodel.push($("#txtCreatedBy").val());


    var Lightingcost = $("#txtLightcost").val();
    if (Lightingcost == "") {
        alert("Please Enter Lightingcost !");
        return false;
    } else {
        lightingmodel.push(Lightingcost);
    }

    var fileInput = document.getElementById("txtLightFilepath");
    var files = fileInput.files;

    var formData = new FormData();
    formData.append("txtLightFilepath", files[0]);

    $.each(lightingmodel, function (index, value) {
        formData.append("lightingmodel[]", value);
    });

    $.ajax({
        type: "POST",
        url: '/Lightings/Lightingsave',
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
                LoadLightings();

                $('#displaySection').show();
                $('#editSection').hide();
                $('#showAlllightsLink').hide();

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
        url: "/Lightings/Getdataforedit",
        data: { Light: Data },
        success: function (data) {
            if (!data.isSuccess) {
                alert(data.message)
            } else {
                $('._CustomMessage').text(data.message);
                if (data.dataList) {

                    var lightTypeValue = data.dataList.lightType;

                    if (lightTypeValue === "IN DOOR") {
                        $('#INDOORRadio').prop('checked', true);
                    } else if (lightTypeValue === "OUT DOOR") {
                        $('#OUTDOORRadio').prop('checked', true);
                    }

                    $('#txtLightid').val(data.dataList.lightID);
                    $('#txtLightname').val(data.dataList.lightName);
                    $('#txtLightcost').val(data.dataList.lightCost);
                    $('#LightImage').attr('src', data.dataList.lightFilepath);
                    $('#editSection').show();
                    $('#Image').hide();

                    $('#LightImage').show();

                }
            }
        },
        error: function () {
            alert("An error occurred while updating details.");
        }
    });
}


function LoadLightings() {
    $.ajax({
        type: "POST",
        url: '/Lightings/LoadLightings',
        data: {},
        success: function (Obj) {
            if (!Obj.isSuccess) {
                $('._CustomMessage').text(Obj.message);
                $('#successPopup').modal('show');
            }
            else {
                LoadLightingDeatils(Obj.list);
            }
        },
        error: function () {

        }
    });
}
function LoadLightingDeatils(List) {
    $('#tblLightingdetails').empty();
    $('#tblLightingdetails').dataTable({
        "pageLength": 10,
        "Processing": true,
        "destroy": true,
        "aaData": List,
        "columns": [

         
            { "data": "lightName", "title": "light Name", "width": "70px" },
            { "data": "lightType", "title": "Light Type", "width": "70px" },
            { "data": "lightCost", "title": "Light Cost", "width": "70px" },
            {
                "data": "createdate", "title": "Createdate", "width": "70px",
                "render": function (data) {
                    return formatDate(data);
                }

            },

            {
                "data": "lightID",
                "width": "70px",
                "title": "Action",
                "class": "text-center",
                "orderable": false,
                "render": function (data) {
                    return `<button type="button" class="dt-btn-approve" style="background-color: green; color: white;" onclick="editdata('` + data.toString() + `')"> Edit </button>` +
                        `&nbsp;&nbsp;` +
                        `<button type="button" class="dt-btn-reject" style="background-color: red; color: white;" onclick="DeleteData('` + data.toString() + `')"> Delete </button>`;
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

function DeleteData(deleteid) {
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
                url: "/Lightings/DeleteLightsData",
                data: { deleteid },
                success: function (data) {
                    if (!data.isSuccess) {
                        $('._CustomMessage').text(data.message);
                        $('#errorPopup').modal('show');
                    } else {
                        $('._CustomMessage').text(data.message);
                        $('#successPopup').modal('show');
                        window.location.replace('/Lightings/LightingsDetails');

                        LoadLightings();
                    }
                },
                error: function () {
                }
            });
        }
    });
}
