
$(document).ready(function () {
    bindProductOrderByCustomerId();
});
function bindProductOrderByCustomerId() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Customer/GetProductOrderByCustomerId',
        dataType: 'json',
        async: true,
        success: function (response) {
            debugger;
            if (response) {
                let tblProductOrderDataByCustomerId = "";
                let subTotal = 0;
                let total = 0;
                $.each(response, function (i, value) {
                    let totalPriceForProduct = parseFloat(value.product.price) * parseInt(value.quantity);

                    subTotal = subTotal + totalPriceForProduct;
                    total = subTotal + 10;

                    tblProductOrderDataByCustomerId += `

                    <input type="hidden" id="hdnProductId_${value.id}" class="form-control form-control-sm bg-secondary border-0 text-center" value="${value.productId}">
                               
                        <tr>
                        <td class="align-middle">${value.product.name}</td>
                        <td class="align-middle"> ${value.product.price}</td>
                  
                             <td class="align-middle"> <div class="input-group quantity mx-auto" style="width: 100px;">
                                <div class="input-group-btn">
                                    <button id="${value.id}" class="btn btn-sm btn-primary btn-minus" onclick="DecreaseProductQuantity(this.id)">
                                        <i class="fa fa-minus"></i>
                                    </button>
                                </div>
                                 
                              <input id="quantityInput_${value.id}" type="text" class="form-control form-control-sm bg-secondary border-0 text-center" value="${value.quantity}">
                                 <div class="input-group-btn">
                                    <button class="btn btn-sm btn-primary btn-plus" id="${value.id}" onclick="IncreaseProductQuantity(this.id)">
                                        <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                              </td>
                            </div>
                        </td>
                        <td class="align-middle">${value.totalPrice}</td>
                        <td class="align-middle"><button class="btn btn-sm btn-danger" data-products-id="${value.id}"><i class="fa fa-times"></i></button></td>
                    </tr>`
                })

                $("#tblProductOrderListByCustomerId").html(tblProductOrderDataByCustomerId);

                $("#tblProductOrderSubTotalId").html(subTotal);

                $("#tblProductOrderTotalId").html(total);
            }
        }
    });

}

function DecreaseProductQuantity(id) {
    debugger;
    var currentQuantity = $("#quantityInput_" + id).val();
    var productId = $("#hdnProductId_" + id).val();
    var currentQuantityInt = parseInt(currentQuantity) - 1;
    updateShoppingCart(currentQuantityInt, id, productId);
}

function IncreaseProductQuantity(id) {
    var currentQuantity = $("#quantityInput_" + id).val();
    var productId = $("#hdnProductId_" + id).val();
    var currentQuantityInt = parseInt(currentQuantity) + 1;
    updateShoppingCart(currentQuantityInt, id, productId);
}

function updateShoppingCart(quantity, id, productId) {
    debugger;
    var shoppingCart = {
        ProductId: productId,
        Id: id,
        Quantity: quantity
    };

    $.ajax({
        url: '/Customer/UpdateProductToShoppingCart',
        type: 'POST',
        dataType: 'json',
        data: { shoppingCart: shoppingCart },
        success: function (response) {
            bindShoppingCartByCustomerId();
        },
        error: function (xhr, textStatus, errorThrown) {
            console.error('Error: ' + errorThrown);
        }
    });
}

$("#tblShoppingCartListByCustomerId").on("click", ".btn-danger", function (e) {
    debugger;
    e.preventDefault();
    console.log($(this).data("products-id"));
    var shoppingCartId = $(this).data("products-id");
    $("#hdnShoppingCartId").val(shoppingCartId);

    jQuery('#Delete-ShoppingCart-Modal').show();
});

function deleteShoppingCart() {
    debugger;
    var shoppingCartId = $("#hdnShoppingCartId").val();

    $.ajax({
        type: 'Get',
        url: '/Customer/DeleteProductFromShoppingCart',
        data: { shoppingCartId: shoppingCartId },
        async: true,
        success: function (response) {
            debugger;
            jQuery('#Delete-ShoppingCart-Modal').hide();
            if (response) {
                debugger;
                showSuccessMessage("Product Removed", "Product Removed From Cart")
                GetAllCount();
                bindShoppingCartByCustomerId();

            } else {
                iziToast.error({
                    title: 'Error',
                    message: 'Product Failed to Removed from Cart',
                    position: 'topRight'
                });
            }
        },
    });
}
function CloseModal() {
    debugger;
    var modal = document.getElementById("Delete-ShoppingCart-Modal");

    modal.style.display = 'none';
}

function ProceedToCheckOut() {
    debugger;
    var CheckOutUrl = "/Customer/CheckOut"

    window.location.href = CheckOutUrl;

}
