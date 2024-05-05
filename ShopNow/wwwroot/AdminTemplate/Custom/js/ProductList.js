let pageNumber = 1;
$(document).ready(function () {
    debugger;
    getProductList(pageNumber);
});

function getProductList(pageNumber) {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/GetProductListByPagination?pageNumber=' + pageNumber,
        dataType: 'json',

        success: function (response) {
            debugger;
            if (response) {
                let tblProductData = "";
              
                $.each(response.paginationData, function (i, value) {

                    tblProductData += `
                        <tr>       
                        <td>${value.name != null ? value.name : ' '}</td>               
                        <td>${value.productDescription != null ? value.productDescription : ' '}</td>

                        <td>${value.createdOn != null ? value.createdOn : ' '}</td>
                        <td>${value.updatedOn != null ? value.updatedOn : ' '}</td>                    
                        <td><a href="#" class="btn btn-primary edit-product" data-product-id="${value.id}"> Edit</a>
                        &nbsp &nbsp
                        <a href="#" class="btn btn-danger delete-product" data-products-id="${value.id}">Delete</a>
                    </tr> `
                })

                $("#tblProductList").html(tblProductData);

                if (response.pager != null) {
                    createPagination(response.pager);
                }
            }
        },
    });



    $(document).on('click', '.edit-product', function (e) {
        e.preventDefault();
        debugger;
        var productId = $(this).data('product-id');

        window.location.href = "/Admin/Product?productId=" + productId;
    })
}

$("#tblProductList").on("click", ".delete-product", function (e) {
    debugger;
    e.preventDefault();
    console.log($(this).data("products-id"));
    var productId = $(this).data("products-id");
    $("#hdnProductId").val(productId);

    jQuery('#Delete-Product-Modal').show();

});

function deleteProduct() {
    debugger;
    var productId = $("#hdnProductId").val();

    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/DeleteProduct',
        data: { productId: productId },
        success: function (response) {
            debugger;
            iziToast.success({
                title: 'Product Deleted',
                message: 'Product Deleted Successfully',
                position: 'topRight'
            });

            jQuery('#Delete-Product-Modal').hide();
            getProductList();
        },

    });


}