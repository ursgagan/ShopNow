﻿function createPagination(pagination) {
  
    var paginationData = `  <li class="page-item"><a class="page-link" onclick="getProductList(1)" href="#">First Page</a></li>`

    if (pagination.currentPage <= pagination.totalPages) {
        paginationData += `  <li class="page-item"><a class="page-link" onClick="getProductList(${pagination.currentPage - 1})" href="#">Previous</a></li>`
    }

    if (pagination.currentPage <= pagination.totalPages) {
        paginationData += `  <li class="page-item active"><a class="page-link" onClick="getProductList(${pagination.currentPage})" href="#">${pagination.currentPage}</a></li> `
    }

    if (pagination.currentPage + 1 <= pagination.totalPages) {

        paginationData += `  <li class="page-item"><a class="page-link" onClick="getProductList(${pagination.currentPage + 1})" href="#">${pagination.currentPage + 1}</a></li> `
    }

    if (pagination.currentPage + 2 <= pagination.totalPages) {

        paginationData += ` <li class="page-item"><a class="page-link" onClick="getProductList(${pagination.currentPage + 2})" href="#">${pagination.currentPage + 2}</a></li> `
    }

    if (pagination.currentPage + 1 <= pagination.totalPages) {

        paginationData += `  <li class="page-item"><a class="page-link" onClick="getProductList(${pagination.currentPage + 1})" href="#">Next</a></li> `
    }

    if (pagination.currentPage != pagination.totalPages) {

        paginationData += ` <li class="page-item"><a class="page-link" onClick="getProductList(${pagination.endPage})" href="#">Last Page</a></li> `
    }
    else {
        paginationData += ` <li class="page-item"><a class="page-link disabled" onClick="getProductList(${pagination.endPage})" href="#">Last Page</a></li> `

    }
    $("#divPagination").html(paginationData);
}

function showSuccessMessage(title, message) {
    iziToast.success({
        title: title,
        message: message,
        position: 'topRight'
    });
}

function showErrorMessage(title, message) {
    iziToast.warning({
        title: title,
        message: message,
        position: 'topRight'
    });
}

function GetAllCount() {
    $.ajax({
        url: '/Customer/GetAllCount',
        type: 'GET',
        async: true,
        success: function (response) {
            if (response) {
                var shoppingCartCount = $('#ShoppingCartCount').text(response.shoppingCartCount);
                var wishListCount = $('#WishListCount').text(response.wishListCount);
            }
        },
    });
}