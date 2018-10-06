$(document).ready(function () {
    if (window.location.pathname == '/Registration/Index/') {
        $('#navRegistration').addClass('active');
    }
    else if (window.location.pathname == '/Deposit/Index/') {
        $('#navDeposit').addClass('active');
    }
    else if (window.location.pathname == '/Withdrawal/Index/') {
        $('#navWithdrawal').addClass('active');
    }
    else if (window.location.pathname == '/Promotion/Index/') {
        $('#navPromotion').addClass('active');
    }
    else if (window.location.pathname == '/ContactUs/Index/') {
        $('#navContact').addClass('active');
    }
    else if (window.location.pathname == '/Dashboard/Index/'){
        $('#navDashboard').addClass('active');
    }
});