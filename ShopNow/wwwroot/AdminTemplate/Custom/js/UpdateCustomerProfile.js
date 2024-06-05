$("#btnUpdateCustomer").click(function () {
    debugger;

    var Address = {};

    Address.Address1 = $("#Address1").val();
    Address.Address2 = $("#Address2").val();
    Address.PhoneNumber = $("#PhoneNumber").val();
    Address.ZipCode = $("#ZipCode").val();

    var customer = {};

    customer.Id = $("#hdnCustomerId").val();
    customer.FirstName = $("#FirstName").val();
    customer.LastName = $("#LastName").val();
    customer.Address = Address; 
    
        $.ajax({
            url: '/Home/UpdateCustomerProfile',
            type: 'POST',
            data: { customer : customer
                
            },
            success: function (data) {
                debugger;
                if (data == true) {
                    debugger;
                    iziToast.success({
                        title: "Customer Profile Updated",
                        message: "Customer Profile Updated Successfully",
                        position: 'topRight'
                    });
                    /*showSuccessMessage("Customer Profile Updated", "Customer Profile Updated Successfully");*/
                    setTimeout(function () {
                        window.location.href = "/Home/MyProfile";
                    }, 7000);
                } else {
                    iziToast.error({
                        title: 'Error',
                        message: 'Failed to Update Customer Profile',
                        position: 'topRight'
                    });
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.error('AJAX Error:', errorThrown);
            }
        });
   
});
