document.getElementById("txtCountry").addEventListener("change", function () {

    var Countryid = this.value;
	getStatesByCountryId(Countryid);

});
document.getElementById("txtState").addEventListener("change", function () {
	var Stateid = this.value;
	getcitiesBystateId(Stateid);
});

function Registration() {

	var RegistrationModel = [];

	RegistrationModel.push($("#txtId").val());


	var txtName = $("#txtName").val();
	if (txtName == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Enter A Name',
			text: '',
		});		return false;
	} else {
		RegistrationModel.push(txtName);
	}

	var txtAddress = $("#txtAddress").val();
	if (txtAddress == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Enter A Address',
			text: '',
		}); return false;
	} else {
		RegistrationModel.push(txtAddress);
	}

	var txtCountry = $("#txtCountry").val();
	if (txtCountry == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Choose A Country',
			text: '',
		}); return false;
	} else {
		RegistrationModel.push(txtCountry);
	}

	var txtState = $("#txtState").val();
	if (txtState == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Choose A State',
			text: '',
		}); return false;
	} else {
		RegistrationModel.push(txtState);
	}

	var txtCity = $("#txtCity").val();
	if (txtCity == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Choose A City',
			text: '',
		}); return false;
	} else {
		RegistrationModel.push(txtCity);
	}

	var txtMobile_No = $("#txtMobile_No").val();
	if (txtMobile_No == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Entr A Mobile Number',
			text: '',
		}); return false;
	} else {
		var mobileRegex = /^\d{10}$/;

		if (!mobileRegex.test(txtMobile_No)) {
			Swal.fire({
				icon: 'error',
				title: 'Please enter a valid 10-digit Mobile Number!',
				text: '',
			}); return false;
		}

		RegistrationModel.push(txtMobile_No);
	}


	var txtEmail_ID = $("#txtEmail_ID").val();
	if (txtEmail_ID == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Entr A Email ID!',
			text: '',
		}); return false;
	} else {
		var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
		if (!emailRegex.test(txtEmail_ID)) {
			Swal.fire({
				icon: 'error',
				title: '"Please enter a valid Email ID!',
				text: '',
			}); return false;
		}
		RegistrationModel.push(txtEmail_ID);
	}



	var txtUsername = $("#txtUsername").val();
	if (txtUsername == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Entr A  UserName',
			text: '',
		}); return false;
	} else {
		var usernameRegex = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$/;
		if (!usernameRegex.test(txtUsername)) {
			Swal.fire({
				icon: 'error',
				title: 'Username must contain at least one letter, one number, and one special character!',
				text: '',
			}); return false;
		}
		RegistrationModel.push(txtUsername);
	}



	var txtPassword = $("#txtPassword").val();
	if (txtPassword == "") {
		Swal.fire({
			icon: 'error',
			title: 'please Entr A Password!',
			text: '',
		}); return false;
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
		RegistrationModel.push(txtPassword);
	}

	var txtConfirmPassword = $("#txtConfirmPassword").val();
	if (txtConfirmPassword == "") {
		Swal.fire({
			icon: 'error',
			title: 'Invalid Credentials',
			text: 'Please Confirm Your Password.'
		}); return false;
	} else if (txtConfirmPassword !== $("#txtPassword").val()) {
		Swal.fire({
			icon: 'error',
			title: 'Invalid Credentials',
			text: 'Password Does Not Match.'
		}); return false;
	} else {
		RegistrationModel.push(txtConfirmPassword);
	}

	var txtGender = $("#txtGender").val();
	if (txtGender == "") {
		Swal.fire({
			icon: 'error',
			title: 'Please Select A Gender',
			text: ''
		}); return false;
	} else {
		RegistrationModel.push(txtGender);
	}

	var txtBirthDate = $("#txtBirthDate").val();
	if (txtBirthDate == "") {
		Swal.fire({
			icon: 'error',
			title: 'Select DOB',
			text: ''
		}); return false;
	} else {
		RegistrationModel.push(txtBirthDate);
	}

	var txtRoleID = $("#txtRoleID").val();
	if (txtRoleID == "") {
		Swal.fire({
			icon: 'error',
			title: 'Select Role',
			text: ''
		}); return false;
	} else {
		RegistrationModel.push(txtRoleID);
	}

	


	$.ajax({
		type: "POST",
		url: '/SignIn/Register',
		data: { "RegistrationModel": JSON.stringify(RegistrationModel) },
		success: function (data) {
			if (!data.isSuccess) {
				Swal.fire({
					icon: 'error',
					title: 'Registration Failed',
					text: data.message
				});
			} else {
				Swal.fire({
					icon: 'success',
					title: 'Registration Success',
					text: 'Your registration was successful.',
					timer: 2000,
					timerProgressBar: true
				
				}).then((result) => {
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


function getStatesByCountryId(Countryid) {
    $.ajax({
		type: "GET",
        url: '/SignIn/Getstates',	
        data: { "Countryid": Countryid },
		success: function (data) {
			if (!data.isSuccess) {
			}
			else {
				if (data.stateList) {
                    $("#txtState").empty().append('<option selected="selected" value="">Select State</option>');
                    $.each(data.stateList, function (key, value) {
                        $("#txtState").append('<option value="' + value.stateID + '">' + value.stateName + '</option>');
					});
				}
			}
		},
		error: function (errormessage) {
		}
	});
}

function getcitiesBystateId(stateid) {
	$.ajax({
		type: "GET",
		url: '/SignIn/Getcities',
		data: { "stateid": stateid },
		success: function (data) {
			if (!data.isSuccess) {
			}
			else {
				if (data.citylist) {
					$("#txtCity").empty().append('<option selected="selected" value="">Select City</option>');
					$.each(data.citylist, function (key, value) {
						$("#txtCity").append('<option value="' + value.cityID + '">' + value.cityName + '</option>');
					});
				}
			}
		},
		error: function (errormessage) {
		}
	});
}