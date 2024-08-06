function PlaceOrder() {
    var placeOrdersUrl = "/Customer/PlaceOrder";
    window.location.href = placeOrdersUrl;
}

function addProductToPlaceOrder() {
    debugger;
    var productOrderList = [];

    var productCount = $('#hdnProductCount').val();
    if (productCount != null) {

        for (var i = 1; i <= productCount; i++) {
          
            var productId = $("#hdnProductId_" + i).val();
            var quantity = $("#hdnQuantity_" + i).val();
            productOrderList.push({ ProductId: productId, Quantity: quantity })
        }
    }

    $.ajax({
        type: 'Post',
        url: '/Customer/PlaceOrder',
        data: { productOrderList: productOrderList },
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                debugger;
                showSuccessMessage("Order Placed", "Product Order is Placed Successfully")
                GetAllCount();
            }
        }
    });
}
