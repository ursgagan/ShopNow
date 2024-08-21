//$(document).ready(function () {
//    debugger;
//    if ($("#hdnResetPwdData").val() != null && $("#hdnResetPwdData").val() != '') {
//        debugger;
//        toastr.success("Password Reset", "Password Reset Succcessfull");
//    }

//    if ($("#hdnIncorrectPwd").val() != null && $("#hdnIncorrectPwd").val() != '') {
//        debugger;
//        toastr.error("Password is Incorrect", "Login Failed");
//    }
//}
//);

$('#btnCustomerLogin').click(function () {
    debugger;
    var emailId = $('#EmailId').val();
    var password = $('#Password').val();
    var isValid = true;

    if (emailId == "") {
        iziToast.error({
            title: 'Invalid EmailId',
            message: 'Please fill EmailId',
            position: 'topRight'
        });
        $("#EmailId").focus();
        isValid = false;
        return false;
    }
    else {
        var customer = {};

        customer.emailId = emailId;
        customer.password = password;
    }
    if (isValid) {
        $.ajax({
            type: 'POST',
            url: '/Customer/CustomerLogin',
            data: { customer: customer },
            dataType: 'json',
            success: function (data) {
                if (data) {
                    debugger;
                    window.Location.href = "/Home/Index";
                }
                else {
                    showErrorMessage("Invalid Login", "Login Unsuccessfully");
                }
            },
            error: function (error) {
                console.error('Error sending email:', error);
            }
        });
    }

});
