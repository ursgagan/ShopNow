$(document).ready(function () {
    debugger;
    getMyOrderList();
});

function getMyOrderList() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/MyOrderList',
        dataType: 'json',

        success: function (response) {
            debugger;
            if (response) {
                let tblMyOrderData = "";

                $.each(response, function (i, value) {

                    tblMyOrderData += `
                        <tr>       
                        <td>${value.product.name != null ? value.product.name : ' '}</td>               
                        <td>${value.product.price != null ? value.product.price : ' '}</td>

                        <td>${value.product.quantity != null ? value.product.quantity : ' '}</td>
                                        
                        <td><a href="#" class="btn btn-primary edit-MyOrder" data-MyOrder-id="${value.id}"> Edit</a>
                        &nbsp &nbsp
                        <a href="#" class="btn btn-danger delete-MyOrder" data-MyOrder-id="${value.id}">Delete</a>
                    </tr> `
                })

                $("#tblMyOrderList").html(tblMyOrderData);

            }
        },
    });

//    $(document).on('click', '.edit-product', function (e) {
//        e.preventDefault();
//        debugger;
//        var productId = $(this).data('product-id');

//        window.location.href = "/Admin/Product?productId=" + productId;

//    })
//}

//$("#tblProductList").on("click", ".delete-product", function (e) {
//    debugger;
//    e.preventDefault();
//    console.log($(this).data("products-id"));
//    var productId = $(this).data("products-id");
//    $("#hdnProductId").val(productId);

//    jQuery('#Delete-Product-Modal').show();
//});

//function deleteProduct() {

//    var productId = $("#hdnProductId").val();

//    $.ajax({
//        type: 'Get',
//        url: '/Admin/DeleteProduct',
//        data: { productId: productId },
//        success: function (response) {
//            debugger;
//            iziToast.success({
//                title: 'Product Deleted',
//                message: 'Product Deleted Successfully',
//                position: 'topRight'
//            });

//            jQuery('#Delete-Product-Modal').hide();
//            getProductList();
//        },

//    });
//}
//function CloseModal() {
//    debugger;
//    var modal = document.getElementById("Delete-Product-Modal");

//    modal.style.display = 'none';
//}
