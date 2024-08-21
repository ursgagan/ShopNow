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
                    showSuccessMessage("Login", "Login Successfully");

                    window.Location.href("DashBoard","Admin");
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