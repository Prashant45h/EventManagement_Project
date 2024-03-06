document.addEventListener('DOMContentLoaded', function (event) {

    $('#Image').hide();

});
function bookindishes() {
    debugger;
    var foodbooking = [];
    var selectedFoods = []; 

    foodbooking.push($("#txtFoodID").val());

    var selectedfoodtype = $("input[name='FoodType']:checked").val();
    foodbooking.push(selectedfoodtype);

    var selectedmealtype = $("input[name='MealType']:checked").val();
    foodbooking.push(selectedmealtype);

    foodbooking.push($("#txtDishType").val());
    $("input[name='selectedfood']:checked").each(function () {
        selectedFoods.push($(this).val()); 
    });
    if (selectedFoods.length === 0) {
        alert("Please select a Dish....!");
        return false;
    }

    foodbooking.push($("#txtcreatedby").val());

    $.ajax({
        type: "post",
        url: '/bookingfood/foodbookings',
        data: { "foodbooking": JSON.stringify(foodbooking), "selectedFoods": JSON.stringify(selectedFoods) },

       
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
                        window.location.replace('/BooK_Light/LightBooking');
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
        console.log("ImagePath:", imagePath);
        document.getElementById("Image").src = imagePath ? imagePath : "/FoodImages/default.jpg";
    }
}
