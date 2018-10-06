var x = document.getElementById("myAudio");

function playAudio() {
    x.play();
}

function getNotifications(played) {
    $.ajax({
        url: "/Notification/GetNotifications",
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            if (res) {
                var countM = 0;
                var countD = 0;
                var countW = 0;
                for (var i = 0; i < Object.keys(res).length; i++) {
                    if (res[i]['Status'] == 1) {
                        if (res[i]['Type'] == 'member') {
                            countM = countM + 1;
                        }
                        else if (res[i]['Type'] == 'deposit') {
                            countD = countD + 1;
                        }
                        else if (res[i]['Type'] == 'withdrawal') {
                            countW = countW + 1;
                        }
                    }
                }
                if (countM != 0) {
                    $('#notifMember').show();
                    $("#notifMember").text(countM);
                }
                if (countD != 0) {
                    $('#notifDeposit').show();
                    $("#notifDeposit").text(countD);
                }
                if (countW != 0) {
                    $('#notifWithdrawal').show();
                    $("#notifWithdrawal").text(countW);
                }
                if (countM != 0 || countD != 0 || countW != 0) {
                    if (played == false) {
                        playAudio();
                        played = true;
                    }
                }
            }
        }
    });
    return played;
}

$(document).ready(function () {
    $('#notifMember').hide();
    $('#notifDeposit').hide();
    $('#notifWithdrawal').hide();

    var played = false;

    setInterval(function () {
        $.ajax({
            url: "/Notification/GetNotifications",
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res) {
                    var countM = 0;
                    var countD = 0;
                    var countW = 0;
                    for (var i = 0; i < Object.keys(res).length; i++) {
                        if (res[i]['Status'] == 1) {
                            if (res[i]['Type'] == 'member') {
                                countM = countM + 1;
                            }
                            else if (res[i]['Type'] == 'deposit') {
                                countD = countD + 1;
                            }
                            else if (res[i]['Type'] == 'withdrawal') {
                                countW = countW + 1;
                            }
                        }
                    }
                    if (countM != 0) {
                        $('#notifMember').show();
                        $("#notifMember").text(countM);
                    }
                    if (countD != 0) {
                        $('#notifDeposit').show();
                        $("#notifDeposit").text(countD);
                    }
                    if (countW != 0) {
                        $('#notifWithdrawal').show();
                        $("#notifWithdrawal").text(countW);
                    }
                    if (countM != 0 || countD != 0 || countW != 0) {
                        if (played == false) {
                            playAudio();
                            played = true;
                        }
                    }
                }
            }
        })
    }
        , 5000
    );
});




//var x = document.getElementById("myAudio");

//function playAudio() {
//    x.play();
//} 

//function getNotifications() {
//    $.ajax({
//        url: "/Notification/GetTopFiveNotifications",
//        type: 'GET',
//        dataType: 'json',
//        success: function (res) {
//            if (res) {
//                for (var i = 0; i < Object.keys(res).length; i++) {
//                    if (res[i]['Type'] == 'member') {
//                        $("#notifList").append('<li class="notification"><a href="/Member/Index"><div><i class="fa fa-user fa-fw"></i>  New User</div></a></li>');
//                        exist = true;
//                    }
//                    else if (res[i]['Type'] == 'deposit') {
//                        $("#notifList").append('<li class="notification"><a href="/Deposit/Index"><div><i class="fa fa-download fa-fw"></i>  New Deposit</div></a></li>');
//                        exist = true;
//                    }
//                    else if (res[i]['Type'] == 'withdrawal') {
//                        $("#notifList").append('<li class="notification"><a href="/Withdrawal/Index"><div><i class="fa fa-upload fa-fw"></i>  New Withdrawal</div></a></li>');
//                        exist = true;
//                    }
//                }
//            }
//        }
//    });
//}

//function updateNotifications() {
//    $.ajax({
//        url: "/Notification/UpdateNotifications",
//        type: 'POST',
//        dataType: 'json',
//        success: function (res) {
//        }
//    });
//}

//$(document).ready(function () {
//    getNotifications();

//    var played = false;

//    $('#notif').click(function () {
//        updateNotifications();
//        if (('#notifNum').length) {
//            $("#notifNum").remove();
//        }
//        played = false;
//    });

//    setInterval(function () {
//        $.ajax({
//            url: "/Notification/GetNotifications",
//            type: 'GET',
//            dataType: 'json',
//            success: function (res) {
//                if (res) {
//                    $("#notifIcon").append('<span id="notifNum" class="num"></span>');
//                    var count = 0;
//                    for (var i = 0; i < Object.keys(res).length; i++) {
//                        if (res[i]['Status'] == 1) {
//                            count = count + 1;
//                        }
//                        if (res[i]['Pulled'] != 1) {
//                            if (res[i]['Type'] == 'member') {
//                                $("#notifList").prepend()('<li class="notification"><a href="/Member/Index"><div><i class="fa fa-user fa-fw"></i> New User </div></a></li>');
//                            }
//                            else if (res[i]['Type'] == 'deposit') {
//                                $("#notifList").prepend('<li class="notification"><a href="/Deposit/Index"><div><i class="fa fa-download fa-fw"></i> New Deposit </div></a></li>');
//                            }
//                            else if (res[i]['Type'] == 'withdrawal') {
//                                $("#notifList").prepend('<li class="notification"><a href="/Withdrawal/Index"><div><i class="fa fa-upload fa-fw"></i> New Withdrawal </div></a></li>');
//                            }
//                        }
//                    }
//                    if (count != 0) {
//                        $("#notifNum").text(count);

//                        if (played == false) {
//                            playAudio();
//                            played = true;
//                        }
//                    }
//                }
//            }
//        });
//    }, 5000);
//});