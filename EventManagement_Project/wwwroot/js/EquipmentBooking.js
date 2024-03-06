document.addEventListener('DOMContentLoaded', function (event) {

    $('#Image').hide();

});

function Savedata() {

    var dataToSend = {
        txtID: $("#txtID").val(),
        selectedEquipments: [],
        txtCreatedby: $("#txtCreatedby").val(),
        txtdate: $("#txtdate").val(),
        txtBookID: $("#txtBookID").val()
    };

    $("input[name='selectedEquipments']:checked").each(function () {
        dataToSend.selectedEquipments.push($(this).val());
    });


    $.ajax({
        type: "POST",
        url: '/BookingEquipment/EqipmentBookings',
        data: { "MODEL": JSON.stringify(dataToSend) },
        success: function (data) {
            if (!data.isSuccess) {
                Swal.fire({
                    icon: "error",
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
                }).then((result) => {
                    if (result.dismiss === Swal.DismissReason.timer) {
                        window.location.replace('/BookingFood/BookDish');
                    }
                });
            }
        },
        error: function () {
        }
    });
}


function changeImage(checkbox) {

    $('#Image').show();

    if (checkbox.checked) {
        var imagePath = checkbox.getAttribute('data-img');
        console.log("Image Path:", imagePath); 
        document.getElementById("Image").src = imagePath ? imagePath : "/FoodImages/default.jpg";
    }
}
