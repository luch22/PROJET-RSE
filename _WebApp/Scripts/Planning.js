$('document').ready(function () {

    // page is now ready, initialize the calendar...
    $('#calendar').fullCalendar({
        // put your options 
        defaultView: 'month',
        themeSystem: 'bootstrap3',
        nowIndicator: true,
        header: {
            left: 'prev,next today',
            center: 'title',
            right: 'month,agendaWeek,agendaDay,listWeek'
        },
        weekNumbers: true,
        eventLimit: true, // allow "more" link when too many events
        events: function (start, end, timezone, callback) {
            //console.log(moment(start).format("YYYY-MM-DD HH:mm") + " " + moment(end).format("YYYY-MM-DD HH:mm"));
            $.ajax({
                type: "POST",
                url: '/Planning/GetEvent',
                data: { start: moment(start).format("YYYY-MM-DD HH:mm"), end: moment(end).format("YYYY-MM-DD HH:mm") },
                dataType: "JSON",
                success: function (result) {
                    var eventsList = [];
                    $(result).each(function () {
                        var eventTitle = $(this).attr('Nom');
                        var eventStart = moment($(this).attr('DateDebut')).format("YYYY-MM-DDTHH:mm");
                        var eventEnd = moment($(this).attr('DateFin')).format("YYYY-MM-DDTHH:mm");
                        var eventColor = $(this).attr('Color');
                        var fullDay = $(this).attr('FullDay');
                        var urlLink = $(this).attr('Url');
                        //console.log(eventTitle+' '+urlLink);
                        eventsList.push(
                            {
                                title: eventTitle,
                                start: eventStart,
                                end: eventEnd,
                                allDay: fullDay,
                                color: eventColor,
                                url: urlLink
                            }
                        );
                    });
                    //console.log(eventsList);
                    callback(eventsList);
                },
                error: function (e) {
                    debugger;
                }
            });
        },
        eventClick: function (event) {
            if (event.url) {
                //console.log(event.url);
                window.open(event.url, "_self");
                return false;
            }
        }
    })
});