var pricefiltervalue = {};

$(document).ready(function () {
    debugger;
    getProductList();
});

function getProductList(rating = null) {
    debugger;

    $.ajax({
        type: 'GET',
        url: '/Home/GetProductByFilters',
        dataType: 'json', 
        data: {
            pricefiltervalue: pricefiltervalue,
            rating: rating,
            
        },

        success: function (response) {
            debugger;
            if (response) {
                let tblProductData = "";
                $.each(response, function (i, value) {
                    if (value.productImages != null && value.productImages.length > 0) {
                        
                            tblProductData += `
                                <div class="col-lg-4 col-md-6 col-sm-6 pb-1">
                                    <div class="product-item bg-light mb-4">
                                        <div class="product-img position-relative overflow-hidden" style="cursor: pointer;">
                                            <img class="img-fluid w-100" style="width: 150px; height: 200px;" src="data:image/png;base64,${value.productImages[0].image.imageData}" alt="">
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
                                            <div class="d-flex align-items-center justify-content-center mb-1">
                                                <small class="fa fa-star text-primary mr-1"></small>
                                                <small class="fa fa-star text-primary mr-1"></small>
                                                <small class="fa fa-star text-primary mr-1"></small>
                                                <small class="fa fa-star text-primary mr-1"></small>
                                                <small class="fa fa-star text-primary mr-1"></small>
                                                <small>(99)</small>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            `;
                        
                    }
                });

                $("#tblProductList").html(tblProductData);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error fetching product data:", error);
        }
    });
}
//function isPriceSelected(product) {
//    let selectedPrices = [];

//    $(".price-checkbox:checked").each(function () {
//        let checkboxClass = $(this).attr('class').split(' ')[1]; // Get the second class of the checkbox

//        switch (checkboxClass) {
//            case 'price-low':
//                selectedPrices.push(product.price >= 0 && product.price <= 500);
//                break;
//            case 'price-medium':
//                selectedPrices.push(product.price > 500 && product.price <= 10000);
//                break;
//            case 'price-high':
//                selectedPrices.push(product.price > 10000);
//                break;
//            case 'price-all':
//                selectedPrices.push(true); // Include all products when 'All Price' is selected
//                break;
//            default:
//                // Handle other cases if needed
//                break;
//        }
//    });

//    // If no prices are selected, return true (show all products)
//    return selectedPrices.length === 0 || selectedPrices.some(price => price);
//}


//$(".price-checkbox").change(function () {
//    let pricefilter = {};
//    debugger;
//    $('.price-checkbox:checked').each(function () {
//        let checkboxId = $(this).attr('id');
//        let labelValue = $(this).siblings('label').text().trim();

//        let [min, max] = labelValue.split(' - ').map(Number);
//        pricefilter['minValue'] = min;
//        pricefilter['maxValue'] = max;
//    });

//    pricefiltervalue = pricefilter;

//    getProductList();

//    return pricefiltervalue;
//});


//function ProductDetail(productId) {
//    debugger;

//    var productDetailsUrl = "/Home/ProductDetails?productId=" + productId;
//    window.location.href = productDetailsUrl;
//} 


