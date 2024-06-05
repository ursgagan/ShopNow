$("#btnSaveCustomer").click(function () {
    debugger;
  
    var FirstName = $("#FirstName").val();
    var AddressId = $("#AddressId").val(); 
    var Address1 = $("#Address1").val();
    var Address2 = $("#Address2").val();
    var ZipCode = $("#ZipCode").val();
    var PhoneNumber = $("#PhoneNumber").val();
    var isValid = true;

    // Validation checks
    if (FirstName.trim() === "") {
        iziToast.error({
            title: 'Invalid First Name',
            message: 'Please fill in the First Name',
            position: 'topRight'
        });
        $("#FirstName").focus();
        isValid = false; 
    }

    if (Address1.trim() === "") {
        iziToast.error({
            title: 'Invalid Address',
            message: 'Please fill in the Address Line 1',
            position: 'topRight'
        });
        $("#Address1").focus();
        isValid = false; 
    }

    if (Address2.trim() === "") {
        iziToast.error({
            title: 'Invalid Address',
            message: 'Please fill in the Address Line 2',
            position: 'topRight'
        });
        $("#Address2").focus();
        isValid = false; 
    }

    if (ZipCode.trim() === "") {
        iziToast.error({
            title: 'Invalid Zip Code',
            message: 'Please fill in the Zip Code',
            position: 'topRight'
        });
        $("#ZipCode").focus();
        isValid = false; 
    }

    if (PhoneNumber.trim() === "") {
        iziToast.error({
            title: 'Invalid PhoneNumber',
            message: 'Please fill in the PhoneNumber',
            position: 'topRight'
        });
        $("#PhoneNumber").focus();
        isValid = false;
    }
  
    if (isValid) {
         $.ajax({
            url: '/Customer/SignUp',
            type: 'POST',
            data: {
                Id: $("#hdnCustomerId").val(),
                FirstName: FirstName,
                LastName: $("#LastName").val(),
                EmailId: $("#EmailId").val(),
                PhoneNumber: $("#PhoneNumber").val(),
                AddressId: AddressId,
                Address1: Address1,
                Address2: Address2,
                ZipCode: ZipCode
            },
            success: function(data) {
                debugger;
                if (data === true) {
                    showSuccessMessage("Customer Added", "Customer Added Successfully");
                    setTimeout(function () {
                        window.location.href = "/Customer/SignUp";
                    }, 4000);
                } else {
                    iziToast.error({
                        title: 'Error',
                        message: 'Failed to add customer',
                        position: 'topRight'
                    });
                }
            },
            error: function(jqXHR, textStatus, errorThrown) {
                console.error('AJAX Error:', errorThrown);
            }
        });
    }
});
