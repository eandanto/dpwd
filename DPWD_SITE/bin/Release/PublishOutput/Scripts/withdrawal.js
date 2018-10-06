function checkUserName() {
    $.ajax({
        url: "/Withdrawal/CheckUserName",
        type: "get", //send it through get method
        data: { userName: $("#txt-username").val()},
        success: function (response) {
            if (response != null && response != '') {
                $("#containerUserName").removeClass('has-error');
                $("#error-message").hide();
                $("#addOrEditGameType").val(response.GameType);
                $("#addOrEditBankAccount").val(response.BankAccount);
                $("#txtGameType").val(response.GameType);
                $("#txtBankAccount").val(response.BankAccount);
                
                var addOrEditBankAccountName = response.BankAccountName;
                var addOrEditBankAccountNumber = response.BankAccountNumber;
                var i;
                for (i = 0; i < addOrEditBankAccountName.length; i++) {
                    if (i % 2 != 0) {
                        addOrEditBankAccountName = addOrEditBankAccountName.substring(0, i - 1) + "*" + addOrEditBankAccountName.substring(i, addOrEditBankAccountName.length);
                    }
                }
                $("#addOrEditBankAccountName").val(addOrEditBankAccountName);
                $("#addOrEditBankAccountNameSubmit").val(response.BankAccountName);
                var i;
                for (i = 0; i < addOrEditBankAccountNumber.length; i++) {
                    if (i % 2 != 0) {
                        addOrEditBankAccountNumber = addOrEditBankAccountNumber.substring(0, i - 1) + "*" + addOrEditBankAccountNumber.substring(i, addOrEditBankAccountNumber.length);
                    }
                }
                $("#addOrEditBankAccountNumber").val(addOrEditBankAccountNumber);
                $("#addOrEditBankAccountNumberSubmit").val(response.BankAccountNumber);
            }
            else {
                if ($("#txt-username").val().length > 0) {
                    $("#containerUserName").addClass('has-error');
                    $("#error-message").show();
                }
                else {
                    $("#containerUserName").removeClass('has-error');
                    $("#error-message").hide();
                }
                $("#addOrEditGameType").val(1);
                $("#addOrEditBankAccount").val(1);
                $("#addOrEditBankAccountName").val("");
                $("#addOrEditBankAccountNumber").val("");
                $("#addOrEditBankAccountNumber").val("");
                $("#addOrEditBankAccountNumberSubmit").val("");
            }
        },
        error: function (response) {
            $("#containerUserName").addClass('has-error');
            $("#error-message").show();
            $("#addOrEditGameType").val(1);
            $("#addOrEditBankAccount").val(1);
            $("#addOrEditBankAccountName").val("");
            $("#addOrEditBankAccountNumber").val("");
            $("#addOrEditBankAccountNumber").val("");
            $("#addOrEditBankAccountNumberSubmit").val("");
        }
    });
}

function checkIsFormValid() {
    if ($("#addOrEditGameType").prop('selectedIndex') != 0 && $("#addOrEditBankAccount").prop('selectedIndex') != 0) {
        $("#submit").removeAttr('disabled');
    }
    else {
        $("#submit").removeAttr('disabled');
        $("#submit").attr('disabled', 'disabled');
    }
}

$(document).ready(function () {
    $("#error-message").hide();

    //$("#submit").attr('disabled', 'disabled');

    //$('#addOrEditGameType').on('change', function () {
    //    checkIsFormValid();
    //})
    //$('#addOrEditBankAccount').on('change', function () {
    //    checkIsFormValid();
    //})
});