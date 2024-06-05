function DecreaseProductQuantity() {

    var inputElement = document.getElementById("quantityInput");
    var currentValue = parseInt(inputElement.value);
    if (currentValue > 0) {
        inputElement.value = currentValue - 1;
    }
}

function IncreaseProductQuantity() {

    var inputElement = document.getElementById("quantityInput");
    var currentValue = parseInt(inputElement.value);
    inputElement.value = currentValue + 1 ;
}

function addToCart() {
    debugger;
    var shoppingCart = {};
    shoppingCart.ProductId = $("#hdnProductId").val();
    shoppingCart.Quantity = $("#quantityInput").val();

    $.ajax({
        type: 'Post',
        url: '/Home/AddProductToShoppingCart',
        data: shoppingCart,
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                debugger;

                var productDetailsUrl = "/Home/ShoppingCart";
                // Redirect the page to the product details URL
                window.location.href = productDetailsUrl;
            }
        }
    });
}