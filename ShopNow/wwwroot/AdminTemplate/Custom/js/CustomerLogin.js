$(document).ready(function () {

    if ($("#hdnResetPwdData").val() != null && $("#hdnResetPwdData").val() != '') {
        debugger;
        toastr.success("Password Reset", "Password Reset Succcessfull");
    }

    if ($("#hdnIncorrectPwd").val() != null && $("#hdnIncorrectPwd").val() != '') {
        debugger;
        toastr.error("Password is Incorrect", "Login Failed");
    }
}
);