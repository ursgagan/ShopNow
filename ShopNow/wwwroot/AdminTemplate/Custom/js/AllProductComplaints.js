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
                   <td>`

                        if (value.status == "completed") {
                            tblProductComplaintData += `
                        <span class="btn btn-success" style="width: 115px;">Completed</span>
                        </td>`
                        }

                        else if (value.status == "resolved") {
                            tblProductComplaintData += `
                        <span class="btn btn-success" style="width: 115px;">Resolved</span>
                        </td>`
                        }

                        else if (value.status == "cancelled") {
                            tblProductComplaintData += `
                        <span class="btn btn-danger" style="width: 115px;">Cancelled</span>
                        </td>`
                        }

                        else {
                            tblProductComplaintData += `
                              <select class="btn btn-primary dropdown-toggle Product-Complaint" data-complaint-id="${value.id}">
                              <option value="pending" style="color:black; background-color:white; " ${value.status === 'pending' ? 'selected' : ''}>Pending</option>
                              <option value="processing" style="color:black; background-color:white;" ${value.status === 'processing' ? 'selected' : ''}>Processing</option>
                              <option value="resolved" style="color:black; background-color:white;" ${value.status === 'Resolved' ? 'selected' : ''} > Resolved </option>
                              <option value="unresolved" style="color:black; background-color:white;" ${value.status === 'UnResolved' ? 'selected' : ''} > UnResolved </option>
                              <option value="cancelled" style="color:black; background-color:white;" ${value.status === 'Cancelled' ? 'selected' : ''}>Cancelled</option>
                              </select>    
                            </td>`
                        }
                    }
                })



                $("#tblProductComplaintList").html(tblProductComplaintData);

            }
        },
    });
}

$(document).on('change', '.Product-Complaint', function () {
    debugger;

    var complaintId = $(this).data("complaint-id");
    var complaintStatus = $(this).val();

    $.ajax({
        type: 'Get',
        url: '/Admin/UpdateComplaintStatus',
        data: { complaintId: complaintId, complaintStatus: complaintStatus },
        success: function (response) {
            debugger;
            showSuccessMessage("Status Updated", "Status Updated Successfully");


        },
    })
});  