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
            url: '/Admin/AdminLogin',
            data: { customer: customer }, 
            dataType: 'json',
            success: function (data) {
                debugger;
                if (data) {
                    debugger;
                    window.location.href = "/Admin/Dashboard";
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