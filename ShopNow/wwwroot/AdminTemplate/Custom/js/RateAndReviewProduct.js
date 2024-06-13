document.addEventListener("DOMContentLoaded", function () {
    const stars = document.querySelectorAll(".fa-star");

    stars.forEach(function (star, index) {
        star.addEventListener("click", function () {
            const rating = parseInt(this.getAttribute("data-rating"));
            updateRatingText(rating);
            highlightStars(rating);
            // Toggle chosen-star class for all stars up to the clicked star
            for (let i = 0; i <= index; i++) {
                stars[i].classList.add("chosen-star");
            }
        });

        star.addEventListener("mouseover", function () {
            const rating = parseInt(this.getAttribute("data-rating"));
            highlightStars(rating);
        });

        star.addEventListener("mouseout", function () {
            const currentRating = document.querySelector(".chosen-star")
                ? parseInt(document.querySelector(".chosen-star").getAttribute("data-rating"))
                : 0;
            highlightStars(currentRating);
        });
    });

    // Handle review submission
    const submitReviewBtn = document.getElementById("submitReviewBtn");
    if (submitReviewBtn) {
        submitReviewBtn.addEventListener("click", function () {
            const rating = parseInt(document.querySelector(".chosen-star").getAttribute("data-rating"));
            const reviewText = document.getElementById("reviewDescription").value;
            submitReview(rating, reviewText);
        });
    }

    function updateRatingText(rating) {
        const feedbackEnd = document.getElementById("myRatingCustomFeedbackEnd");
        const ratingsText = ["Meh", "Poor", "Fair", "Good", "Excellent"];
        feedbackEnd.textContent = ratingsText[rating - 1];
    }

    function highlightStars(rating) {
        stars.forEach(function (star, index) {
            if (index < rating) {
                star.classList.add("chosen-star");
            } else {
                star.classList.remove("chosen-star");
            }
        });
    }

    function submitReview(rating, reviewText) {
        debugger;
        $.ajax({
            type: 'Post',
            url: '/Customer/AddReview',
            dataType: { rating : rating, reviewText: reviewText },
            dataType: 'json',
            success: function (response) {
                debugger;
                if (response) {
                    debugger;
                    showSuccessMessage("Review Added", "Review Added Successfully")
                }
            }
        });
    }
});
