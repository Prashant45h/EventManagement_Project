document.addEventListener('DOMContentLoaded', function (event) {

    $('#Image').hide();

});
function BOOKINGLIGHTS() {

    var LIGHTS = [];
    var selectedlights = [];

    $("input[name='selectedlightings']:checked").each(function () {
        selectedlights.push($(this).val());
    });
    if (selectedlights.length === 0) {
        alert("Please select a Lightings....!");
        return false;
    }

    LIGHTS.push($("#txtLightid").val());

    var selectedlighting = $("input[name='LightType']:checked").val();
    LIGHTS.push(selectedlighting);

 


    LIGHTS.push($("#txtCreatedBy").val());




    $.ajax({
        type: "POST",
        url: '/BooK_Light/LightingsBook',
        data: { "LIGHTS": JSON.stringify(LIGHTS), "Selectedlights": JSON.stringify(selectedlights) },

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
                        window.location.replace('/BookingFlowers/BookFlower');
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
        document.getElementById("Image").src = imagePath ? imagePath : "/default.jpg";
    }
}
