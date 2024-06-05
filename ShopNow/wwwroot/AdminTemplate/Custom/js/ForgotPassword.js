$(document).ready(function () {

    $("#btnResetPassword").click(function () {
        debugger;
       
        var newPassword = $("#NewPassword").val();
        var confirmPassword = $("#ConfirmPassword").val();
        var resetCodeId = $("#ResetCode").val();

   
        if (newPassword !== confirmPassword) {
            
            alert("New Password and Confirm Password do not match");
            return false; // Prevent form submission
        }


        $.ajax({
            type: 'POST',
            url: '/Customer/ForgotPassword',
            data: { password: newPassword, resetCodeId: resetCodeId }, // Pass the emailId as data to the server
            dataType: 'json',
            success: function (data) {
                debugger;
                if (data) {
                    debugger;
                    window.location.href = '/Customer/Login';

                }
                else {
                    alert('Email not send!');
                }
            },
            error: function (error) {
                // Handle errors, e.g., show an error message
                console.error('Error sending email:', error);
            }
        });

    });
});
