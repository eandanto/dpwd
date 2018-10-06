function submitForm() {
    var username;
    var usernameId;
    var status;
    if ($('#userNameList').is(':checked')) {
        username = $('#ddlUserNameList option:selected').html();
        usernameId = $('#ddlUserNameList').val();
        status = 'list';
    }
    else if ($('#userNameManual').is(':checked')) {
        username = $("#txtUserNameManual").val();
        status = 'manual';
    }

    $.get('/Member/ReAssignMember', { Id: $("#memberId").val(), UserName: username, UserNameId: usernameId, Password: $("#password").val(), Status: status },
        function (returnedData) {
            window.location.href = '/Member/Index';
            
        });
}

$(document).ready(function () {
    $('#userNameList').click(function () {
        $('#ddlUserNameList').prop("disabled", false);
        $('#txtUserNameManual').prop("disabled", true);
    });

    $('#userNameManual').click(function () {
        $('#ddlUserNameList').prop("disabled", true);
        $('#txtUserNameManual').prop("disabled", false);
    });
});