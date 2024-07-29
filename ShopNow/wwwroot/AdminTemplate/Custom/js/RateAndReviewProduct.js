$(document).ready(function () {

});
document.addEventListener("DOMContentLoaded", function () {
    const stars = document.querySelectorAll(".fa-star");
    let currentRating = 0; 
    stars.forEach(function (star, index) {
        star.addEventListener("click", function () {
            const rating = parseInt(this.getAttribute("data-rating"));
            currentRating = rating; // Update the current rating
            updateRatingStars(rating);
        });

        star.addEventListener("mouseover", function () {
            const rating = parseInt(this.getAttribute("data-rating"));
            updateRatingStars(rating);
        });

        star.addEventListener("mouseout", function () {
            updateRatingStars(currentRating); // Revert to the current rating on mouseout
        });
    });

    const submitReviewBtn = document.getElementById("submitReviewBtn");
    if (submitReviewBtn) {
        submitReviewBtn.addEventListener("click", function () {
            if (currentRating !== 0) {
                const reviewText = document.getElementById("reviewdescription").value;
                submitReview(currentRating, reviewText);
            } else {
                alert("Please select a rating before submitting your review.");
            }
        });
    }

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
