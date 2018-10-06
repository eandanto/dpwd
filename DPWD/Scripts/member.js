function showModal(action, modalContainerId, modalBodyId, id) {
    var url = '/Member/' + action;

    if (action == 'EditMember' || action == 'ReAssign') {
        $.get(url, { id: id }, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
    else if (action == 'EditNotes' || action == 'EditNotes') {
        $.get(url, { id: id }, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
    else {
        $.get(url, function (data) {
            $(modalBodyId).html(data);
            $(modalContainerId).modal('show');
        });
    }
}

$(document).ready(function () {
    setTimeout(function () {
        $(".alert").slideUp(500);
    }, 3000);

    $('#tbl-members').DataTable({
        responsive: true,
        "columnDefs": [
            { type: 'any-number', targets: 4 },
            { type: 'any-number', targets: 7 }
        ],
        "order": [[8, "desc"]]
    });

    $('[data-toggle="tooltip"]').tooltip(); 
});