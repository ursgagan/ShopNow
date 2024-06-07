$(document).ready(function () {
    getProductList();
});

function getProductList() {

    $.ajax({
        type: 'Get',
        url: '/Admin/GetProductWithImageList',
        dataType: 'json',

        success: function (response) {
            debugger;
            if (response) {
                let tblProductData = "";

                $.each(response, function (i, value) {
                    if (value.productImages != null && value.productImages.length > 0) {
                        tblProductData += `
            <div class="col-lg-3 col-md-4 col-sm-6 pb-1">
                <div class="product-item bg-light mb-4">
                   <div class="product-img position-relative overflow-hidden" style="cursor: pointer;">
                    
                            <img class="img-fluid w-100 product-image" style="width: 150px; height: 200px;" src="data:image/png;base64,${value.productImages[0].image.imageData}">
                        <div class="product-action" onclick="ProductDetail('${value.id}')">
                            <a class="btn btn-outline-dark btn-square"><i class="fa fa-shopping-cart"></i></a>
                            <a class="btn btn-outline-dark btn-square" onclick="addToWishlist('${value.id}')"><i class="far fa-heart"></i></a>
                            <a class="btn btn-outline-dark btn-square" href=""><i class="fa fa-search"></i></a>
                            <a class="btn btn-outline-dark btn-square view-product-link"><i class="fa fa-folder"></i></a>
                        </div>
                    </div>  
                    <div class="text-center py-4">
                        <a class="h6 text-decoration-none text-truncate" href="">${value.name}</a>
                        <div class="d-flex align-items-center justify-content-center mt-2">
                            <h5>${value.price}</h5><h6 class="text-muted ml-2"><del>₹100000</del></h6>
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
    });
}

function addToWishlist(productId) {
    debugger;
    $.ajax({
        url: '/Customer/AddProductToWishList',
        type: 'POST',
        data: { productId: productId },
        success: function (response) {
            if (response) {
                debugger;
                showSuccessMessage("Product Added", "Product Added To WishList")

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

function addToCart(productId) {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Home/AddProductToShoppingCart',
        data: { productId: productId },

        success: function (response) {
            debugger;
            if (response) {
                debugger;

                var productDetailsUrl = "/Home/AddProductToShoppingCart";
                // Redirect the page to the product details URL
                window.location.href = productDetailsUrl;
            }
        }
    });
}