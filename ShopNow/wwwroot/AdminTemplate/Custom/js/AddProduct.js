$(document).ready(function () {
    debugger;

    bindProductCategoriesDropdown();

});

function bindProductCategoriesDropdown() {

    debugger;
    $.ajax({
        url: '/Admin/GetProductCategoryList',
        type: 'GET',
        success: function (response) {
            debugger;
            var categories = response;
            var dropdown = $('#ddlProductCategory');
            dropdown.empty();

            dropdown.append('<option value="">Select Category</option>');
            $.each(categories, function (index, category) {
                dropdown.append($('<option></option>').attr('value', category.id).text(category.categoryName));
            });
            debugger;
            var existingCategoryId = $('#hdnProductCategoryId').val();
            if (existingCategoryId != null && existingCategoryId != "") {
                dropdown.val(existingCategoryId);
            }
        },
        error: function (xhr, status, error) {
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

        formData.append('Id', $("#hdnProductId").val());

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
                if ($("#hdnProductId").val() != null && $("#hdnProductId").val() != "") {
                    showSuccessMessage("Product Updated", "Product Updated Successfully");
                }

                else {
                    showSuccessMessage("Product Added", "Product Added Successfully")
                }

                setTimeout(function () {
                    window.location.href = "/Admin/ProductList";
                }, 7000);
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            console.error('AJAX Error:', errorThrown);
        });
    }
});

function showDeleteProductImageModal(id) {
    debugger;

    $("#hdnProductImageId").val(id);
    jQuery('#Delete-ProductImage-Modal').show();

}

function DeleteProductImage() {
    debugger;
    var productImageId = $("#hdnProductImageId").val();
    $.ajax({
        type: 'Get',
        url: '/Admin/DeleteProductImage',
        data: { productImageId: productImageId },
        success: function (response) {
            debugger;
            showSuccessMessage("Product Image Deleted", "Product Image Deleted Successfully")

            $("#" + productImageId).remove();

            $("#productImage-" + productImageId).remove();


            jQuery('#Delete-ProductImage-Modal').hide();
        },
    });
}

function CloseModal() {
    debugger;
    var modal = document.getElementById("Delete-ProductImage-Modal");

    modal.style.display = 'none';
}