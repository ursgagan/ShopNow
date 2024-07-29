$(document).ready(function () {
    debugger;
    getOrderList();
});

function getOrderList() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/getOrderList',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                let tblOrderData = "";

                $.each(response, function (i, value) {

                    tblOrderData += `
                 <tr>
                   <td>${value.product?.name != null ? value.product?.name : ' '}</td>               
                   <td>${value.product?.price != null ? value.product?.price : ' '}</td>
               
                   <td>${value.quantity != null ? value.quantity : ' '}</td>
            
                   <td>`

                    if (value.status == "completed") {
                        tblOrderData += `
                        <span class="btn btn-success" style="width: 115px;">Completed</span>
                        </td>`
                    }

                    else if (value.status == "cancelled")
                    {
                        tblOrderData += `
                        <span class="btn btn-danger" style="width: 115px;">Cancelled</span>
                        </td>`
                    }

                    else {
                         tblOrderData += `
                              <select class="btn btn-primary dropdown-toggle Orders-Status" data-myorder-id="${value.id}">
                              <option value="pending" style="color:black; background-color:white; " ${value.status === 'pending' ? 'selected' : ''}>Pending</option>
                              <option value="processing" style="color:black; background-color:white;" ${value.status === 'processing' ? 'selected' : ''}>Processing</option>
                              <option value="completed" style="color:black; background-color:white;" ${value.status === 'completed' ? 'selected' : 'Completed'} > Completed </option>
                              <option value="cancelled" style="color:black; background-color:white;" ${value.status === 'cancelled' ? 'selected' : ''}>Cancelled</option>
                              </select>    
                            </td>`
                         }
                })
                


    $("#tblOrderList").html(tblOrderData);

}
        },
    });
}

$(document).on('change', '.Orders-Status', function () {
    debugger;

    var orderId = $(this).data("myorder-id");
    var orderStatus = $(this).val();

    $.ajax({
        type: 'Get',
        url: '/Admin/UpdateProductOrderStatus',
        data: { orderId: orderId, orderStatus: orderStatus },
        success: function (response) {
            debugger;
            showSuccessMessage("Status Updated", "Status Updated Successfully");


        },
    })
});  