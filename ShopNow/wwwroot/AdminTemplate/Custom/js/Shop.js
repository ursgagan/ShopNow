var pricefiltervalue = {};

var arrSelectedColors = [];
$(document).ready(function () {
    debugger;
    getProductList();
});

function getProductList(rating) {
    debugger;
    let filterProductModel = {};

    filterProductModel.ProductCategoryId = $("#hdnProductCategoryId").val();                       
    
    filterProductModel.Rating = rating;
    let colorString = arrSelectedColors.join(', ');
    debugger;
    filterProductModel.Color = colorString;
   
    filterProductModel.PriceFiltervalue = getfilteredProductByPrice();
    debugger;
    $.ajax({
        type: 'GET',
        url: '/Home/GetProductByFilters',
        dataType: 'json',
        data: filterProductModel,

        success: function (response) {
            debugger;
            let tblProductData = "";
            $.each(response, function (i, value) {
                debugger;
                if (value.productImages != null && value.productImages.length > 0) {

                    if (isPriceSelected(value)) {

                        let averageRatingData = calculateAverageRating(value);

                        tblProductData += `
                    <div class="col-lg-4 col-md-6 col-sm-6 pb-1">
                        <div class="product-item bg-light mb-4">
                            <div class="product-img position-relative overflow-hidden" style="cursor: pointer;">
                                <img class="img-fluid w-100" style="width: 150px; height: 230px;" src="data:image/png;base64,${value.productImages[0].image.imageData}" alt="">
                                <div class="product-action" onclick="ProductDetail('${value.id}')">
                                    <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-shopping-cart"></i></a>
                                    <a class="btn btn-outline-dark btn-square" href=""><i class="far fa-heart"></i></a>
                                    <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-sync-alt"></i></a>
                                    <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-search"></i></a>
                                </div>
                            </div>
                            <div class="text-center py-4">
                                <a class="h6 text-decoration-none text-truncate" href="">${value.name}</a>
                                <div class="d-flex align-items-center justify-content-center mt-2">
                                    <h5>${value.price}</h5>
                                </div>
                                 ${averageRatingData}       
                            </div>
                        </div>
                    </div>
                `;
                    }
                }
            });

            $("#tblProductList").html(tblProductData);  
        },
        error: function (xhr, status, error) {
            console.error("Error fetching product data:", error);  
        }
    });
}
function isPriceSelected(product) {
    let selectedPrices = [];

    $(".price-checkbox:checked").each(function () {
        let checkboxClass = $(this).attr('class').split(' ')[1];

        switch (checkboxClass) {
            case 'price-low':
                selectedPrices.push(product.price >= 0 && product.price <= 500);
                break;
            case 'price-medium':
                selectedPrices.push(product.price > 500 && product.price <= 10000);
                break;
            case 'price-high':
                selectedPrices.push(product.price > 10000);
                break;
            case 'price-all':
                selectedPrices.push(true);
                break;
            default:

                break;
        }
    });

    return selectedPrices.length === 0 || selectedPrices.some(price => price);
}

$(".price-checkbox").change(function () {
    debugger;
    let pricefilter = {};
    $('.price-checkbox:checked').each(function () {
        debugger;
        let checkboxId = $(this).attr('id');
        let labelValue = $(this).siblings('label').text().trim();

        let checkboxdata = labelValue.split('-');
        pricefilter['minValue'] = checkboxdata[0];
        pricefilter['maxValue'] = checkboxdata[1];
    });

    pricefiltervalue = pricefilter;

    getProductList(0, pricefilter);

    return pricefiltervalue;
});

function getfilteredProductByPrice()
{
    debugger;
    let pricefilter = {};
    $('.price-checkbox:checked').each(function () {
        debugger;
        let checkboxId = $(this).attr('id');
        let labelValue = $(this).siblings('label').text().trim();

        let checkboxdata = labelValue.split('-');
        pricefilter['minValue'] = checkboxdata[0];
        pricefilter['maxValue'] = checkboxdata[1];
    });

    pricefiltervalue = pricefilter;
    return pricefiltervalue;
}

function ProductDetail(productId) {
    var productDetailsUrl = "/Home/ProductDetails?productId=" + productId;
    window.location.href = productDetailsUrl;
}

function getProductByRating(rate) {
    let rating = rate;
    
    debugger;

    $.ajax({
        type: 'GET',
        url: '/Home/GetProductsByRating',
        data: {
            rating: rating
        },
        
        success: function (response) {
            debugger;
            if (response) {
                debugger;

            }
        }
    });
}

$(".color-checkbox").change(function () {
    debugger;
    //let selectedColors = [];

    $('.color-checkbox:checked').each(function () {
        debugger;
        let checkboxId = $(this).attr('id');
        arrSelectedColors.push(checkboxId);

        let uniqueColors = [...new Set(arrSelectedColors)];

        arrSelectedColors = uniqueColors;
        
    });

    getProductList(); 
});

function applyColorFilter(selectedColors) {
    debugger;
    $('.product-item').hide(); 

    if (selectedColors.length === 0) {
        $('.product-item').show(); 
    } else {
        selectedColors.forEach(function (color) {
            debugger;
            $('.product-item[data-color="' + color + '"]').show(); 
        });
    }
}
function calculateAverageRating(product) {
    let averageRatingData = "";

    if (product.ratings.length > 0) {
        var totalRatings = product.ratings.length;
        var sumOfRatings = 0;

        $.each(product.ratings, function (index, rating) {
            if (rating.rate != null) {
                sumOfRatings += rating.rate;
            }
        });

        var averageRating = sumOfRatings / totalRatings;
        var fullStars = Math.floor(averageRating);

        for (var i = 0; i < fullStars; i++) {
            averageRatingData += '<small class="fa fa-star text-primary mr-1"></small>';
        }

        if (averageRating % 1 !== 0) {
            averageRatingData += '<small class="fa fa-star-half-alt text-primary mr-1"></small>';
            fullStars++;
        }

        var emptyStars = 5 - fullStars;
        for (var j = 0; j < emptyStars; j++) {
            averageRatingData += '<small class="far fa-star" ></small>';
        }

        averageRatingData += " " + totalRatings + " Ratings";
    }

    return averageRatingData;
}