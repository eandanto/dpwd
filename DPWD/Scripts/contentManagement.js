function showModal(action, modalContainerId, modalBodyId, id) {
    if (action == 'EditBankDeposit' || action == 'EditPromotion' || action == 'EditGame') {
        var url = '/ContentManagement/' + action;
        $.get(url, { id: id }, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
    else {
        var url = '/ContentManagement/' + action;
        $.get(url, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
}

$(document).ready(function () {

});