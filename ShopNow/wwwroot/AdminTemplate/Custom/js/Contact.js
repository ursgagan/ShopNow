$("#submitContactButton").click(function () {
    debugger;
    var firstName = $("#FirstName").val();
    var lastName = $("#LastName").val();
    var emailId = $("#Email").val();
    var phoneNumber = $("#PhoneNumber").val();
    var message = $("#Message").val();
    var isValid = true;
    
    if (firstName == "") {
        iziToast.error({
            title: 'Invalid First Name',
            message: 'Please fill First Name',
            position: 'topRight'
        });
        $("#FirstName").focus();
        isValid = false;
        return false;
    }

    if (emailId == "") {
        iziToast.error({
            title: 'Invalid EmailId',
            message: 'Please fill EmailId',
            position: 'topRight'
        });
        $("#Email").focus();
        isValid = false;
        return false;
    }

    if (phoneNumber == "") {
        iziToast.error({
            title: 'Invalid PhoneNumber',
            message: 'Please fill PhoneNumber',
            position: 'topRight'
        });
        $("#PhoneNumber").focus();
        isValid = false;
        return false;
    }

    if (message == "") {
        iziToast.error({
            title: 'Invalid Message',
            message: 'Please fill Message',
            position: 'topRight'
        });
        $("#Message").focus();
        isValid = false;
        return false;
    }

    else { 
    var contact = {};
    debugger
    contact.firstName = firstName;
    contact.lastName = lastName;
    contact.emailId = emailId;
    contact.phoneNumber = phoneNumber;
    contact.message = message;
    }
    if (isValid) { 
    $.ajax({
        url: '/Home/Contact',
        type: 'POST',
        datatype: 'json',
        data: contact,
        success: function (response) {
            debugger;
            if (response == true) {
                debugger;

                showSuccessMessage("Contact Added", "Contact Added Successfully");
               $("#FirstName").val('');
               $("#LastName").val('');
               $("#Email").val('');
               $("#PhoneNumber").val('');
               $("#Message").val('');
            }
            else {
                showErrorMessage("Invalid Contact", "Please enter valid Contact");
            }

        }
    })
    }
});