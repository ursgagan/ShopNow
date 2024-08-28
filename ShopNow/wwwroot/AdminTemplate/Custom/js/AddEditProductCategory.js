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

        var fileInput = $('#CImage')[0];
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
            processData: false, // Important for FormData
            contentType: false, // Important for FormData
            success: function (data) {
                debugger;
                if (data = true) {
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