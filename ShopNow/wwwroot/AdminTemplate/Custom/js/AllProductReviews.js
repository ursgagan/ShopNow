$(document).ready(function () {
    getProductReviewList();
});

function getProductReviewList() {
    $.ajax({
        type: 'Get',
        url: '/Admin/getProductReviews',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                let tblProductReviewData = "";

                $.each(response, function (i, value) {
                    debugger;
                    if (value.productOrder != null) {

                        tblProductReviewData += `
                 <tr>
                   <td>${value.productOrder?.product.name != null ? value.productOrder?.product.name : ' '}</td>               
                   <td>${value.productOrder?.product.productDescription != null ? value.productOrder?.product.productDescription : ' '}</td>
               
                   <td>${value.reviewText != null ? value.reviewText : ' '}</td>
                   <td>${value.ratings.rate != null ? value.ratings.rate : ' '}</td>   
                   `
                    }
                })



                $("#tblProductReviewList").html(tblProductReviewData);

            }
        },
    });
}
