
$(document).ready(function () {
    bindMyOrderByCustomerId();
});
function bindMyOrderByCustomerId() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Customer/GetMyOrders',
        dataType: 'json',
        async: true,
        success: function (response) {
            debugger;
            if (response) {
                let tblMyOrderDataByCustomerId = "";
                
                $.each(response, function (i, value) {
                    debugger;
                    tblMyOrderDataByCustomerId += `

                    <input type="hidden" id="hdnProductId_${value.id}" class="form-control form-control-sm bg-secondary border-0 text-center" value="${value.productId}">
                               
                        <tr>
                        <td class="align-middle">${value.product?.name}</td>
                        <td class="align-middle"> ${value.product?.price}</td>
                  
                             <td class="align-middle">
                             <div class="input-group quantity mx-auto" style="width: 100px;">      
                                 
                              <input id="quantityInput_${value.id}" type="text" class="form-control form-control-sm bg-secondary border-0 text-center" value="${value.quantity}">
                            </div>
                        </td>
                         <td class="align-middle"><button class="btn btn-sm btn-success" onClick="rateAndReviewProduct('${value.productId}')"> Rate & Review Product </button></td>
                        <td class="align-middle"><button class="btn btn-sm btn-success" onClick="addToCart('${value.id}')"> Order Again</button></td>
                        <td class="align-middle"><button class="btn btn-sm btn-danger" onClick="AddProductComplaint()">Add a Complaint</button></td>
               
                        </tr>`
                })
                $("#tblMyOrderListByCustomerId").html(tblMyOrderDataByCustomerId);
            }
        }
    });
}

function addToCart(id) {
    debugger;
    var productInput = $("#hdnProductId_" + id).val();
    var quantityInput = $("#quantityInput_" + id).val();

    var shoppingCart = {};

    shoppingCart.ProductId = productInput;
    shoppingCart.Quantity = quantityInput;

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
                window.location.href = productDetailsUrl;
                GetAllCount();
            }
        }
    });
}

function rateAndReviewProduct(productId)
{
    var rateAndReviewProductUrl = "/Customer/RateAndReviewProduct?productId=" + productId;
    window.location.href = rateAndReviewProductUrl;
}

function addToCart(AddProductComplaint) {
   
    $.ajax({
        type: 'Post',
        url: '/Customer/AddProductToShoppingCart',
        data: shoppingCart,
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
            
            }
        }
    });
}