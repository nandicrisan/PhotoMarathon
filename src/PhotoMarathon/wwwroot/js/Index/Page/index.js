Main = {};

Main.InitCalendar = function () {
    var currentMonth = moment().format('YYYY-MM');
    var nextMonth = moment().add('month', 1).format('YYYY-MM');
    var lotsOfEvents = [
        {
            end: '2016-11-08',
            start: '2016-12-04',
            title: 'Monday to Friday Event'
        }, {
            end: '2016-11-20',
            start: '2016-12-15',
            title: 'Another Long Event'
        },
        {
            date: '2016-09-20',
            title: 'Another Long Event'
        }
    ];

    $('#mini-clndr').clndr({
        daysOfTheWeek: ['D', 'L', 'M', 'M', 'J', 'V', 'S'],
        numberOfRows: 5,
        template: $('#calendar-template').html(),
        events: lotsOfEvents,
        weekOffset: 1,
        clickEvents: {
            click: function (target) {
                console.log(target);
            },
            onMonthChange: function (month) {
                console.log('you just went to ' + month.format('MMMM, YYYY'));
            }
        },
        doneRendering: function () {
            console.log('this would be a fine place to attach custom event handlers.');
        },
        dateParameter: 'date',
        //multiDayEvents: {
        //    endDate: 'end',
        //    startDate: 'start'
        //},
        adjacentDaysChangeMonth: true
    });
}