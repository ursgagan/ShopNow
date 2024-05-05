$(document).ready(function () {
    debugger;
    // Function to fetch product categories

    // Call the function to fetch product categories
    bindProductCategoriesDropdown();


    debugger;
});

function bindProductCategoriesDropdown() {
    debugger;
    $.ajax({
        url: '/Admin/GetProductCategoryList', // Replace ControllerName with your actual controller name
        type: 'GET',
        success: function (response) {
            debugger;
            var categories = response;
            var dropdown = $('#ddlProductCategory');
            dropdown.empty(); // Clear existing options
            dropdown.append('<option value="">Select Category</option>'); // Add default option
            $.each(categories, function (index, category) {
                dropdown.append($('<option></option>').attr('value', category.id).text(category.categoryName));
            });
        },
        error: function (xhr, status, error) {
            // Handle errors
            console.error(xhr.responseText);
        }
    });
}




$("#btnSaveProduct").click(function () {
    debugger;
    var Product = $("#ProductName").val();
    var isValid = false;

    if (Product == "") {
        iziToast.error({
            title: 'Invalid Product Name',
            message: 'Please fill Product Name',
            position: 'topRight'
        });
        $("#ProductName").focus();
        isValid = false;
        return false;
    }

    else {
        isValid = true;
    }

    if (isValid) {
        debugger;
        event.preventDefault();
        var product = {};
        var formData = new FormData();

        //formData.Name = $("#ProductName").val();
        //formData.ProductId = $("#hdnProductId").val();
        //formData.Price = $("#Price").val();

        formData.append('Name', $("#ProductName").val());
        formData.append('ProductId', $("#hdnProductId").val());
        formData.append('ProductDescription', $("#ProductDescription").val());
        formData.append('Price', $("#Price").val());
        formData.append('ProductCategoryId', $("#ddlProductCategory").val())

        var fileInput = $('#Image')[0];
        if (fileInput.files.length > 0) {
            for (var i = 0; i < fileInput.files.length; i++) {
                var file = fileInput.files[i];
                formData.append('imageFile', file);
                
            }
        }
        else {
            console.error('No file selected.');
        }


        $.ajax({
            url: '/Admin/AddProduct',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            async: false,
            cache: false,
            //datatype: 'json',
        }).done(function (data) {
            debugger;
            if (data === true) {
                iziToast.success({
                    title: 'Product',
                    message: 'Product Added Successfully',
                    position: 'topRight'
                });

                setTimeout(function () {
                    window.location.href = "/Admin/ProductList";
                }, 4000);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error('AJAX Error:', errorThrown);
        });

    }
});

