Main = {};

Main.InitCalendar = function () {
    var currentMonth = moment().format('YYYY-MM');
    var nextMonth = moment().add('month', 1).format('YYYY-MM');
    var events = [
      { date: '2016-08-22', title: 'CLNDR GitHub Page Finished', url: 'http://github.com/kylestetz/CLNDR' }
    ];

    $('#mini-clndr').clndr({
        daysOfTheWeek: ['D', 'L', 'M', 'M', 'J', 'V', 'S'],
        numberOfRows: 5,
        template: $('#calendar-template').html(),
        events: events,
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
        adjacentDaysChangeMonth: true
    });
}