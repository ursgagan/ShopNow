$(document).ready(function () {
    bindWishListByCustomerId();
});
function bindWishListByCustomerId() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Customer/GetWishListByCustomerId',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                let tblWishListDataByCustomerId = "";
               
                $.each(response, function (i, value) {

                    tblWishListDataByCustomerId += `

                    <input type="hidden" id="hdnProductId_${value.id}" class="form-control form-control-sm bg-secondary border-0 text-center" value="${value.productId}">
                            
                        <tr>
                        <td class="align-middle">${value.product.name}</td>
                          <td class="align-middle"> ${value.product.productDescription}</td>
                        <td class="align-middle"> ${value.product.price}</td>
                 
                        <td class="align-middle"><button class="btn btn-sm btn-danger" data-products-id="${value.id}"><i class="fa fa-times"></i></button></td>
                    </tr>`
                })

                $("#tblWishListByCustomerId").html(tblWishListDataByCustomerId);
            }
        }
    });
}

$("#tblWishListByCustomerId").on("click", ".btn-danger", function (e) {
    debugger;
    e.preventDefault();
    console.log($(this).data("products-id"));
    var wishListId = $(this).data("products-id");
    $("#hdnWishListId").val(wishListId);

    jQuery('#Delete-WishList-Modal').show();
});

function deleteWishList()
{
    var WishListId = $("#hdnWishListId").val();

    $.ajax({
        type: 'Get',
        url: '/Customer/DeleteProductFromWishlist',
        data: { WishListId: WishListId },
        async: true,
        success: function (response) {
            debugger;
            jQuery('#Delete-WishList-Modal').hide();
            if (response) {
                debugger;
                showSuccessMessage("Product Removed", "Product Removed From WishList")
                bindWishListByCustomerId();
                GetAllCount();

            } else {
                iziToast.error({
                    title: 'Error',
                    message: 'Product Failed to Removed from WishList',
                    position: 'topRight'
                });
            }
        },
    });
}
function CloseModal() {
    debugger;
    var modal = document.getElementById("Delete-WishList-Modal");

    modal.style.display = 'none';
}
 