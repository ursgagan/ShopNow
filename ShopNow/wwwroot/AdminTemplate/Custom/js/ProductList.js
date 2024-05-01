$(document).ready(function () {
    debugger;
    getProductList();
});

function getProductList() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/GetProductList',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                let tblProductData = "";
                let productCount = 1;
                $.each(response, function (i, value) {

                    tblProductData += `
                        <tr>
                        <td> ${productCount++}</td>
                        <td> ${value.name}</td>
                        <td>${value.createdOn}</td>
                        <td>${value.updatedOn}</td>                    
                        <td><a href="#" class="btn btn-primary edit-product" data-product-id="${value.productId}" > Edit</a>
                        &nbsp &nbsp
                        <a href="#" class="btn btn-danger delete-product" data-product-id="${value.productId}">Delete</a>
                    </tr> `
                })

                $("#tblProductList").html(tblProductData);
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
    let productId = $(this).data("product-id");
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