$(document).ready(function () {

});
document.addEventListener("DOMContentLoaded", function () {
    const stars = document.querySelectorAll(".fa-star");
    let currentRating = 0; 
    stars.forEach(function (star, index) {
        star.addEventListener("click", function () {
            const rating = parseInt(this.getAttribute("data-rating"));
            currentRating = rating; 
            updateRatingStars(rating);
        });

        star.addEventListener("mouseover", function () {
            const rating = parseInt(this.getAttribute("data-rating"));
            updateRatingStars(rating);
        });

        star.addEventListener("mouseout", function () {
            updateRatingStars(currentRating); 
        });
    });

    $("#submitReviewBtn").click(function () {
        debugger;
        const reviewText = $('#reviewdescription').val();
        debugger;
        if (currentRating !== 0) {
                    debugger;
                    submitReview(currentRating, reviewText);
                } else {
                    showErrorMessage("Invalid Rating", "Rating should not be Empty");
                }
    });

    function updateRatingStars(rating) {
        const feedbackEnd = document.getElementById("myRatingCustomFeedbackStart");
        const stars = feedbackEnd.querySelectorAll("span");

        stars.forEach(function (star, index) {
            if (index < rating) {
                star.classList.remove("far");
                star.classList.add("fas", "chosen-star");
            } else {
                star.classList.remove("fas", "chosen-star");
                star.classList.add("far");
            }
        });
    }
});

function submitReview(rating, reviewText) {
    debugger;
    var orderId = document.getElementById("hdnOrderId").value;

    var reviewModel = {
        Rating: {
            Rate: rating
        },
        ReviewText: reviewText,
        ProductOrderId: orderId
    };

    $.ajax({
        type: 'Post',
        url: '/Customer/AddReview',
        data: { reviewModel: reviewModel },
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                debugger;
                showSuccessMessage("Review Added", "Review Added Successfully");
            }
        }
    });
}
