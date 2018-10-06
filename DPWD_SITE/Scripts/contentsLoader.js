$(document).ready(function () {
    $.ajax({
        url: "/ContentManagement/GetMainContents",
        type: 'GET',
        dataType: 'json',
        success: function (res) {
            if (res) {
                for (var i = 0; i < Object.keys(res).length; i++) {
                    if (res[i]['Type'] == 0) {
                        $('#runningText').html(res[i]['Contents'])
                    }
                    if (res[i]['Type'] == 1) {
                        $("#disclaimerText").append(res[i]['Contents']);
                    }
                    if (res[i]['Type'] == 26) {
                        $("#runningTextDate").append(res[i]['Contents']);
                    }
                    if (res[i]['Type'] == 25) {
                        $("#bankOfflineText").append(res[i]['Contents']);
                    }
                    if (res[i]['Type'] == 30) {
                        if (res[i]['Contents'] != null) {
                            $("head").append(res[i]['Contents'].replace("</script>", "<\/script>"));
                        }
                    }
                    if (res[i]['Type'] == 31) {
                        if (res[i]['Contents'] != null || res[i]['Contents'] != "") {
                            $("body").append(res[i]['Contents'].replace("</script>", "<\/script>"));
                        }
                    }
                }
            }
        }
    });
});