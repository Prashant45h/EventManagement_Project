document.addEventListener('DOMContentLoaded', function (event) {

	$('#Image').hide();

});
function SaveVenus() {
	var BookingModel = [];
	BookingModel.push($("#txtID").val());
	BookingModel.push($("#txtevent").val());


	BookingModel.push($("#txtvenu").val());


	BookingModel.push($("#txtguest").val());
	BookingModel.push($("#txtCreatedby").val());
	BookingModel.push($("#txtdate").val());
	BookingModel.push($("#txtBookID").val());


	$.ajax({
		type: "POST",
		url: '/BookingVenu/Bookingsave',
		data: { "BookingModel": JSON.stringify(BookingModel) },
		success: function (data) {
			if (!data.isSuccess) {
				Swal.fire({
					icon: "error",
					title: data.message,
					showConfirmButton: false,
					timer: 1500,
				}).then((result) => {
					if (result.dismiss === Swal.DismissReason.timer) {
						window.location.replace('/BookingVenu/VenuBooking');
					}
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
						window.location.replace('/BookingEquipment/Success');
					}
				});
			}
		},
		error: function () {
		}
	});
}

