document.addEventListener('DOMContentLoaded', function (event) {
    LoadDishes();
    $('#foodImage').hide();

});
function Foodsave() {

    var foodmodel = [];
    foodmodel.push($("#txtFoodID").val());

    var selectedFoodType = $("input[name='FoodType']:checked").val();
    foodmodel.push(selectedFoodType);

    var selectedMealType = $("input[name='MealType']:checked").val();
    foodmodel.push(selectedMealType);

    foodmodel.push($("#txtDishType").val());


    var foodName = $("#txtfoodname").val();
    if (foodName == "") {
        alert("Please Enter foodName !");
        return false;
    } else {
        foodmodel.push(foodName);
    }

    foodmodel.push("");

    foodmodel.push($("#txtfoodFilepath").val());

    foodmodel.push($("#txtCreatedBy").val());


    var foodcost = $("#txtfoodcost").val();
    if (foodcost == "") {
        alert("Please Enter foodcost !");
        return false;
    } else {
        foodmodel.push(foodcost);
    }

    var fileInput = document.getElementById("txtfoodFilepath");
    var files = fileInput.files;

    var formData = new FormData();
    formData.append("txtfoodFilepath", files[0]);

    $.each(foodmodel, function (index, value) {
        formData.append("foodmodel[]", value);
    });

    $.ajax({
        type: "POST",
        url: '/Food/SaveFood',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            if (!data.isSuccess) {
                Swal.fire({
                    position: "top-end",
                    icon: "error",
                    title: data.message,
                    showConfirmButton: false,
                    timer: 2500,
                })
            } else {
                Swal.fire({
                    position: "top-end",
                    icon: "success",
                    title: data.message,
                    showConfirmButton: false,
                    timer: 2500,
                })
                LoadDishes();

                $('#displaySection').show();
                $('#editSection').hide();
                $('#showAlldishesLink').hide();

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
        url: "/Food/Getdataforedit",
        data: { FoodID: Data },
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
                $('._CustomMessage').text(data.message);
                if (data.dataList) {

                    var foodTypeValue = data.dataList.foodType;

                    if (foodTypeValue === "VEG") {
                        $('#vegRadio').prop('checked', true);
                    } else if (foodTypeValue === "NON-VEG") {
                        $('#nonVegRadio').prop('checked', true);
                    }

                    var mealTypeValue = data.dataList.mealType;

                    if (mealTypeValue === "Lunch") {
                        $('#lunchRadio').prop('checked', true);
                    } else if (mealTypeValue === "Dinner") {
                        $('#dinnerRadio').prop('checked', true);
                    }

                    $('#txtDishType').val(data.dataList.dishType);


                    $('#txtFoodID').val(data.dataList.foodID);
                    $('#txtfoodname').val(data.dataList.foodName);
                    $('#txtfoodcost').val(data.dataList.foodCost);
                    $('#foodImage').attr('src', data.dataList.foodFilepath);
                    $('#editSection').show();
                    $('#Image').hide();

                    $('#foodImage').show();

                }
            }
        },
        error: function () {
            alert("An error occurred while updating details.");
        }
    });
}


function LoadDishes() {
    $.ajax({
        type: "POST",
        url: "/Food/LoadallDishes",
        data: {},
        success: function (Obj) {
            if (!Obj.isSuccess) {
                $('._CustomMessage').text(Obj.message);
                $('#successPopup').modal('show');
            }
            else {
                LoadFoodDeatils(Obj.list);
            }
        },
        error: function () {

        }
    });
}
function LoadFoodDeatils(List) {
    $('#tblFooddetails').empty();
    $('#tblFooddetails').dataTable({
        "pageLength": 10,
        "Processing": true,
        "destroy": true,
        "aaData": List,
        "columns": [

            { "data": "foodType", "title": "Food Type", "width": "70px" },
            { "data": "foodName", "title": "Food Name", "width": "70px" },
            { "data": "mealType", "title": "Meal Type", "width": "70px" },
            { "data": "dishType", "title": "Dish Type", "width": "70px" },
            { "data": "foodCost", "title": "Food Cost", "width": "70px" },
            {
                "data": "createdate", "title": "Createdate", "width": "70px",
                "render": function (data) {
                    return formatDate(data);
                }

            },

            {
                "data": "foodID",
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

function DeleteData(deletedish) {
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
                url: "/Food/Deletefood",
                data: { deletedish },
                success: function (data) {
                    if (!data.isSuccess) {
                        $('._CustomMessage').text(data.message);
                        $('#errorPopup').modal('show');
                    } else {
                        $('._CustomMessage').text(data.message);
                        $('#successPopup').modal('show');
                        window.location.replace('/Food/FoodDetails');

                        LoadDishes();
                    }
                },
                error: function () {
                }
            });
        }
    });
}
