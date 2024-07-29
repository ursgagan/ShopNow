function PlaceOrder() {
    var placeOrdersUrl = "/Customer/PlaceOrder";
    // Redirect the page to the product details URL
    window.location.href = placeOrdersUrl;
}

function addProductToPlaceOrder() {
   
    var productOrderList = [];

    var productCount = $('#hdnProductCount').val();
    /*var quantities = $('#hdnQuantityId');*/
    if (productCount != null) {

        for (var i = 1; i <= productCount; i++) {
          
            var productId = $("#hdnProductId_" + i).val();
            var quantity = $("#hdnQuantity_" + i).val();
            /*var Price = $("#hdnPrice_" + i).val();*/
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
