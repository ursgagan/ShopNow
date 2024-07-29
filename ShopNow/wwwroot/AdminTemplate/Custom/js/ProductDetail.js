
$(document).ready(function () {
    getProductRating();
});
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
        url: '/Customer/AddProductToShoppingCart',
        data: shoppingCart,
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                debugger;
               
                var productDetailsUrl = "/Customer/ShoppingCart";
                // Redirect the page to the product details URL
                window.location.href = productDetailsUrl;
                GetAllCount();
            }
        }
    });
}


function getProductRating() {
    var productId = $("#hdnProductId").val();
    $.ajax({
        type: 'POST',
        url: '/Home/GetRatingsByProductId',
        data: { productId: productId },
        dataType: 'json',
        success: function (response) {
            debugger;

            let averageRatingData = "";

            if (response.length > 0) {
                var totalRatings = response.length;
                var sumOfRatings = 0;
               
                $.each(response, function (i, value) {
                    if (value.rate != null) {
                        sumOfRatings += value.rate;
                    }
                });

                if (totalRatings > 0) {
                    debugger;
                    var averageRating = sumOfRatings / totalRatings;
                    $('#RatingCount').text(totalRatings + " Ratings");

                    averageRatingData += '<div class="average-rating">';

                    var fullStars = Math.floor(averageRating);
                    for (var i = 0; i < fullStars; i++) {
                        debugger;
                        averageRatingData += '<small class="fas fa-star"></small>';
                    }

                    if (averageRating % 1 !== 0) {
                        debugger;
                        averageRatingData += '<small class="fas fa-star-half-alt"></small>';
                        fullStars++; 
                    }
   
                    var emptyStars = 5 - fullStars;
                    for (var j = 0; j < emptyStars; j++) {
                        averageRatingData += '<small class="far fa-star"></small>';
                    }

                    averageRatingData += '</div>'; 
                }
            }

            $("#averageRatingDataList").html(averageRatingData);
        }

    });
}

