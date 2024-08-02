$(document).ready(function () {
   
});

$("#submitProductComplaintBtn").click(function () {
    debugger;
    var orderId = document.getElementById("hdnOrderId").value; 

    var complaintHeadLine = $("#productComplaintHeadLine").val();
    var complaintDescription = $("#productComplaintDescription").val();

    var productComplaint = { 
        ComplaintHeadLine: complaintHeadLine,
        ComplaintDescription: complaintDescription,
        ProductOrderId: orderId
    };

    $.ajax({
            type: 'Post',
            url: '/Home/AddComplaint',
            data: { productComplaint : productComplaint },
            dataType: 'json',
            success: function (response) {
                debugger;
                if (response.complaintDescription != null && response.complaintHeadLine != null) { 
                    debugger;
                    showSuccessMessage("Complaint Added", "Product Complaint Added Successfully");
                }
                else {
                    showErrorMessage("Invalid Complaint", "Please enter Product Complaint");
                }
                setTimeout(function () {
                    window.location.href = "/Customer/MyOrders";

               }, 7000);
            }
        });

});
//function submitReview(rating, reviewText) {
//    debugger;

//    var orderId = document.getElementById("hdnOrderId").value;
//    var reviewModel = {
//        Rating: {
//            Rate: rating
//        },
//        ReviewText: reviewText,
//        ProductOrderId: orderId
//    };

//    $.ajax({
//        type: 'Get',
//        url: '/Customer/AddReview',
//        data: { reviewModel: reviewModel },
//        dataType: 'json',
//        success: function (response) {
//            debugger;
//            if (response) {
//                debugger;
//                showSuccessMessage("Review Added", "Review Added Successfully");
//            }
//        }
//    });
//}