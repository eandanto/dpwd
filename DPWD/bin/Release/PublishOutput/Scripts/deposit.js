function showModal(action, modalContainerId, modalBodyId, id) {
    if (action == 'EditNotes') {
        var url = '/Deposit/EditNotes'
        $.get(url, { id: id }, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
    else {
        var url = '/Deposit/Insert';
        $.get(url, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
}

function checkUserName() {
    $.ajax({
        url: "/Deposit/CheckUserName",
        type: "get", //send it through get method
        data: { userName: $("#addOrEditUserName").val() },
        success: function (response) {
            if (response != null && response != '') {
                $("#addOrEditGameType").val(response.GameType);
                $("#addOrEditBankAccount").val(response.BankAccount);
                $("#addOrEditBankAccountName").val(response.BankAccountName);
                $("#addOrEditBankAccountNumber").val(response.BankAccountNumber);
            }
            else {
                $("#addOrEditGameType").val(1);
                $("#addOrEditBankAccount").val(1);
                $("#addOrEditBankAccountName").val("");
                $("#addOrEditBankAccountNumber").val("");
            }
        },
        error: function (response) {
            $("#addOrEditGameType").val(1);
            $("#addOrEditBankAccount").val(1);
            $("#addOrEditBankAccountName").val("");
            $("#addOrEditBankAccountNumber").val("");
        }
    });
}

$(document).ready(function () {
    setTimeout(function () {
        $(".alert").slideUp(500);
    }, 3000);

    $('#tbl-deposit').DataTable({
        responsive: true,
        "order": [[6, "desc"]]
    });

    $('#startDate').datepicker({ dateFormat: 'dd-mm-yy' });
    $('#endDate').datepicker({ dateFormat: 'dd-mm-yy' });

    $('[data-toggle="tooltip"]').tooltip(); 
});