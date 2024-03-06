document.addEventListener('DOMContentLoaded', function (event) {

	$('#Image').hide();

});
function SaveFLowers() {
	var FLOWERS = [];
	debugger;
	var Selectedflowers = [];

	FLOWERS.push($("#txtID").val());

	$("input[name='selectedflowers']:checked").each(function () {
		Selectedflowers.push($(this).val());
    });
    if (Selectedflowers.length === 0) {
        alert("Please select a Flowers....!");
        return false;
    }


	FLOWERS.push($("#txtCreatedby").val());


    $.ajax({
        type: "POST",
        url: '/BookingFlowers/Flowersbooking',
        data: { "FLOWERS": JSON.stringify(FLOWERS), "Selectedflowers": JSON.stringify(Selectedflowers) },
        success: function (data) {
            if (!data.isSuccess) {
                Swal.fire({
                    position: "top-end",
                    icon: "error",
                    title: data.message,
                    showConfirmButton: false,
                    timer: 2000,
                });
            } else {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: data.message,
                    showConfirmButton: false,
                    timer: 1000,
                }).then((result) => {
                    if (result.dismiss === Swal.DismissReason.timer) {
                        window.location.replace('/CustomerDashboard/Dashboard');
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
		document.getElementById("Image").src = imagePath ? imagePath : "/FlowerImages/default.jpg";
	}
}
