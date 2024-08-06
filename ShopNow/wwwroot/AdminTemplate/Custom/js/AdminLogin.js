$('#btnAdminLogin').click(function () {
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
    var admin = {};

        admin.emailId = emailId;
        admin.password = password;
    }
    if (isValid) {
        $.ajax({
            type: 'POST',
            url: '/Admin/AdminLogin',
            data: { admin: admin }, 
            dataType: 'json',
            success: function (data) {
                if (data) {
                    showSuccessMessage("Login", "Login Successfully");
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