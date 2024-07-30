$(document).ready(function () {
    getProductComplaintList();
});

function getProductComplaintList() {
    debugger;
    $.ajax({
        type: 'Get',
        url: '/Admin/getProductComplaints',
        dataType: 'json',
        success: function (response) {
            debugger;
            if (response) {
                let tblProductComplaintData = "";

                $.each(response, function (i, value) {

                    if (value.productOrder != null) {

                        tblProductComplaintData += `
                 <tr>
                   <td>${value.productOrder?.product.name != null ? value.productOrder?.product.name : ' '}</td>               
                   <td>${value.productOrder?.product.productDescription != null ? value.productOrder?.product.productDescription : ' '}</td>
               
                   <td>${value.complaintHeadLine != null ? value.complaintHeadLine : ' '}</td>
                   <td>${value.complaintDescription != null ? value.complaintDescription : ' '}</td>
                   `
                    }
                })



                $("#tblProductComplaintList").html(tblProductComplaintData);

            }
        },
    });
}
