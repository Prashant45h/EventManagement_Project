$(document).ready(function () {
	
	
});
function Login() {

    var RegistrationModel = [];
    RegistrationModel.push($("#txUserName").val());
    RegistrationModel.push($("#txtpassword").val());

	$.ajax({
		type: "POST",
		url: '/SignIn/UserLogin',
		data: { RegistrationModel: JSON.stringify(RegistrationModel) },
		success: function (data) {
			if (!data.isSuccess) {
				Swal.fire({
					icon: 'error',
					title: 'Invalid Credentials',
					text: 'Please enter valid credentials.'
				});
			} else {
				Swal.fire({
					icon: 'success',
					title: 'Login Success',
					text: 'You have successfully logged in.',
					timer: 1000, 
					timerProgressBar: true 
				}).then((result) => {
					debugger;
					if (result.dismiss === Swal.DismissReason.timer) {
						window.location.replace(data.redirectUrl);
					}
				});
			}
		},
		error: function () {
		}
	});


}

function savepassword() {

	var DATA = [];
	DATA.push($("#txtusername").val());

	var txtPassword = $("#newPasswordInput").val();
	if (txtPassword == "") {
		Swal.fire({
			icon: 'error',
			title: 'Invalid Credentials',
			text: 'Please enter credentials.'
		});
		return false;
	} else {
		const strongPasswordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/;
		if (!strongPasswordRegex.test(txtPassword)) {
			Swal.fire({
				icon: 'error',
				title: 'Invalid Credentials',
				text: "Password must meet the following criteria:\n" +
					"- At least 8 characters long\n" +
					"- At least one uppercase letter\n" +
					"- At least one lowercase letter\n" +
					"- At least one number\n" +
					"- At least one special character (@$!%*?&)"
			});

			return false;
		}
		DATA.push(txtPassword);
	}

	var txtConfirmPassword = $("#confirmPassword").val();
	if (txtConfirmPassword == "") {
		Swal.fire({
			icon: 'error',
			title: 'Invalid Credentials',
			text: 'Please Confirm Your Password.'
		});		return false;
	} else if (txtConfirmPassword !== $("#newPasswordInput").val()) {
		Swal.fire({
			icon: 'error',
			title: 'Invalid Credentials',
			text: 'Password Does Not Match.'
		});		return false;
	} else {
		DATA.push(txtConfirmPassword);
	}

	$.ajax({
		type: "POST",
		url: '/SignIn/updatappassword',
		data: { "DATA": JSON.stringify(DATA) },
		success: function (data) {
			if (!data.isSuccess) {
				Swal.fire({
					icon: 'error',
					title: data.message,
					text: 'Please enter Valid Username.'
				});
			} else {

				Swal.fire({
					icon: 'success',
					title: data.message,
					text: 'You Password Change Successfully .',
					timer: 1000,
					timerProgressBar: true
				}).then((result) => {
					debugger;
					if (result.dismiss === Swal.DismissReason.timer) {
						window.location.replace('/SignIn/Login');
					}
				});
			}
		},
		error: function () {
		}
	});

}