$(document).ready(function () {
    debugger;
    getProductCategoryList();
});

function getProductCategoryList() {
    $.ajax({
        type: 'Get',
        url: '/Admin/GetProductCategoryList',
        dataType: 'json',
        success: function (response) {

            if (response) {
                let tblProductCategoryData = "";
                let productCount = 1;
                $.each(response, function (i, value) {

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
        let productId = $(this).data("product-id");
        deleteProduct(productId);
    });

    // Event listener for the "Edit" buttons
    $(document).on('click', '.edit-product', function (e) {
        e.preventDefault();
        debugger;
        var productCategoryId = $(this).data('product-id');

        window.location.href = "/Admin/AddProductCategory?productCategoryId=" + productCategoryId;
    })
}

function deleteProduct(productId) {

    $('#Delete-Product-Category-Modal').modal("show");

    debugger;
    $.ajax({
        type: 'POST',
        url: '/Admin/DeleteProductCategory',
        data: { productCategoryId: productId },
        success: function (response) {
            debugger;
            getProductCategoryList();
        },
        error: function (xhr, status, error) {
            // Optionally handle error response
            console.error("Error deleting product:", error);
        }
    });
}


