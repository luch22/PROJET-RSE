//
//
var li = document.getElementById("liNotifs");
var a = document.getElementById("aNotifs");
var span = document.getElementById("spanNotifs");
var notifications = document.getElementById("notifications");
notifications.setAttribute("class", "divnotif");
var count = 0;

li.style.display = "block";

$.ajax({
    type: "POST",
    url: '/Script/Notifications',
    dataType: "JSON",
    success: function (result) {
        var notifs = [];
        $(result).each(function () {
            var contenu = $(this).attr('Contenu');
            var date = moment($(this).attr('Date')).format("DD/MM/YYYY HH:mm");
            var url = $(this).attr('Lien');
            var lu = $(this).attr('Lu');
            notifs.push(
                {
                    Contenu: contenu,
                    Date: date,
                    Url: url,
                    Lu: lu
                }
            );
        });
        for (var i = 0; i < notifs.length; i++) {
            var elem = document.createElement("div");
            var date = document.createElement("div");
            //Notif
            elem.setAttribute("onclick", "link('" + notifs[i].Url + "');");
            elem.setAttribute("class", "hoverdivblue contenunotif");
            elem.setAttribute("onmouseover", "change(this)");
            elem.style.background = notifs[i].Lu;
            elem.innerHTML = notifs[i].Contenu;
            notifications.appendChild(elem);
            date.setAttribute("class", "datenotif");
            date.innerHTML = notifs[i].Date;
            notifications.appendChild(date);


            if (notifs[i].Lu !== "#FFFFFF") {
                count++;
            }
        }
        span.textContent = count;
    },
    error: function (e) {
        console.log(e);
    }
});

function link(url) {
    window.location.href = url;
};

function change(elem) {
    elem.style.background = "#FFFFFF";
};

$(document).ready(function () {

    $('#aNotifs').click(function () {

        if (count < 1) {
            document.getElementById("nonotifs").style.display = "block";
        }
        span.textContent = count;

        // TOGGLE (SHOW OR HIDE) NOTIFICATION WINDOW.
        $('#notifications').fadeToggle('fast', 'linear', function () {
            if ($('#notifications').is(':hidden')) {
                //
            }
            else {
                //$('#spanNotifs').fadeOut('slow');
                //MARK AS READ
                if (count > 0) {
                    $.ajax({
                        type: "POST",
                        url: '/Script/MarkRead',
                        success: function (result) {
                            //console.log("Notification(s) marquée(s) comme lue(s)");
                        },
                        error: function (e) {
                            console.log(e);
                        }
                    });
                    count = 0;
                }
            }
        });
        return false;
    });

    // HIDE NOTIFICATIONS WHEN CLICKED ANYWHERE ON THE PAGE.
    $(document).click(function () {
        $('#notifications').hide();
    });
});