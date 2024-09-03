$("#btnSaveCategory").click(function () {
    var ProductCategory = $("#ProductCategory").val();
    var isValid = false;

    if (ProductCategory == "") {
        iziToast.error({
            title: 'Invalid Category Name',
            message: 'Please fill Category Name',
            position: 'topRight'
        });
        $("#ProductCategory").focus();
        isValid = false;
        return false;
    }

    else {
        isValid = true;
    }

    if (isValid) {
        debugger;
        event.preventDefault();
        var productCategory = new FormData();

        productCategory.append('CategoryName', $("#ProductCategory").val());
        productCategory.append('Id', $("#hdnProductCategoryId").val());

        var fileInput = $('#Image')[0];
        if (fileInput.files.length > 0) {
            for (var i = 0; i < fileInput.files.length; i++) {
                var file = fileInput.files[i];
                productCategory.append('imageFile', file);
            }
        } else {
            console.error('No file selected.');
        }

        $.ajax({
            url: '/Admin/AddProductCategory',
            type: 'POST',
            data: productCategory,
            processData: false, 
            contentType: false, 
            success: function (data) {
                debugger;
                if (data = true) {
                    debugger;
                    iziToast.success({
                        title: 'Product Category',
                        message: 'Product Category Added Successfully',
                        position: 'topRight'
                    });
                    setTimeout(function () {
                        window.location.href = "/Admin/ProductCategoryList";
                    }, 4000);
                }
            },
        })
    }
});


function showDeleteProductCategoryImageModal(id) {
    debugger;

    $("#hdnProductCategoryImageId").val(id);
    jQuery('#Delete-ProductCategoryImage-Modal').show();

}

function DeleteProductCategoryImage() {
    debugger;
    var productCategoryImageId = $("#hdnProductCategoryImageId").val();
    $.ajax({
        type: 'Get',
        url: '/Admin/DeleteProductCategoryImage',
        data: { productCategoryImageId: productCategoryImageId },
        success: function (response) {
            debugger;
            showSuccessMessage("Product Category Image Deleted", "Product Category Image Deleted Successfully")

            $("#" + productCategoryImageId).remove();

            $("#productCategoryImage-" + productCategoryImageId).remove();

            jQuery('#Delete-ProductCategoryImage-Modal').hide();
        },
    });
}

function CloseModal() {
    debugger;
    var modal = document.getElementById("Delete-ProductCategoryImage-Modal");

    modal.style.display = 'none';
}