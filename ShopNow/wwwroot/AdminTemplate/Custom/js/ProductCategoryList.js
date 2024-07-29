$(document).ready(function () {
    debugger;
    getProductCategoryList();
});

function getProductCategoryList() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/GetProductCategoryList',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                let tblProductCategoryData = "";
                let productCount = 1;
                $.each(response, function (i, value) {
                    debugger;
                    tblProductCategoryData += `
                        <tr>
                        <td> ${productCount++}</td>
                        <td> ${value.categoryName}</td>
                        <td>${value.createdOn}</td>
                        <td>${value.updatedOn}</td>                    
                        <td><a href="#" class="btn btn-primary edit-product" data-product-id="${value.id}" > Edit</a>
                        &nbsp &nbsp
                        <a href="#" class="btn btn-danger delete-product" data-product-id="${value.id}">Delete</a>
                    </tr> `
                })

                $("#tblProductCategoryList").html(tblProductCategoryData);
            }
        },
    });


    // Event listener for delete button click
    $("#tblProductCategoryList").on("click", ".delete-product", function (e) {
        debugger;
        e.preventDefault();
        let productCategoryId = $(this).data("product-id");
        $("#hdnProductId").val(productCategoryId);

        jQuery('#Delete-Product-Category-Modal').show();
        
    });

    // Event listener for the "Edit" buttons
    $(document).on('click', '.edit-product', function (e) {
        e.preventDefault();
        debugger;
        var productCategoryId = $(this).data('product-id');

        window.location.href = "/Admin/AddProductCategory?productCategoryId=" + productCategoryId;
    })
}

function deleteProductCategory() {
    debugger;
    var productCategoryId = $("#hdnProductId").val();
   
    
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/DeleteProductCategory',
        data: { productCategoryId: productCategoryId },
        success: function (response) {
            debugger;
            iziToast.success({
                title: 'Product Category Deleted',
                message: 'Product Category Deleted Successfully',
                position: 'topRight'
            });
            jQuery('#Delete-Product-Category-Modal').hide();
            getProductCategoryList();
        },
        error: function (xhr, status, error) {
            // Optionally handle error response
            console.error("Error deleting product:", error);
        }
    });
}


