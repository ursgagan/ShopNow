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
        var productCategory = {};

        productCategory.CategoryName = $("#ProductCategory").val();
        productCategory.Id = $("#hdnProductCategoryId").val();

        $.ajax({
            url: '/Admin/AddProductCategory',
            type: 'POST',
            data: productCategory, 
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