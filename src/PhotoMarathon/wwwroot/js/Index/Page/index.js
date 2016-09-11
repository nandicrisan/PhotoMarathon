Main = {};

Main.InitCalendar = function () {
    var currentMonth = moment().format('YYYY-MM');
    var nextMonth = moment().add('month', 1).format('YYYY-MM');
    var lotsOfEvents = [
        {
            date: '2016-10-13',
            title: 'Expoziții'
        },
        {
            date: '2016-10-14',
            title: 'Expoziții'
        },
        {
            date: '2016-10-14',
            title: 'Workshop-uri'
        },
        {
            date: '2016-10-15',
            title: 'Expoziții'
        },
        {
            date: '2016-10-15',
            title: 'Maraton Foto'
        },
        {
            date: '2016-10-16',
            title: 'Expoziții'
        },
        {
            date: '2016-10-16',
            title: 'Jurizare & premiere'
        },
    ];

    $('#mini-clndr').clndr({
        daysOfTheWeek: ['D', 'L', 'M', 'M', 'J', 'V', 'S'],
        numberOfRows: 5,
        template: $('#calendar-template').html(),
        events: lotsOfEvents,
        weekOffset: 1,
        startWithMonth: "2016-10-1",
        clickEvents: {
            click: function (target) {
                $("#events").removeClass("animated slideOutRight");
                $("#mini-clndr").removeClass("animated slideInLeft");

                $("#mini-clndr").addClass("animated slideOutLeft");
                $("#events").css("display", "inline-block");
                $("#events").addClass("animated slideInRight");
            },
        },
        doneRendering: function () {
            console.log('this would be a fine place to attach custom event handlers.');
        },
        dateParameter: 'date',
        adjacentDaysChangeMonth: true
    });
}

Main.BackToCalendar = function () {
    $("#events").removeClass("animated slideInRight");
    $("#events").addClass("animated slideOutRight");

    $("#mini-clndr").removeClass("animated slideOutLeft");
    $("#mini-clndr").addClass("animated slideInLeft");
}