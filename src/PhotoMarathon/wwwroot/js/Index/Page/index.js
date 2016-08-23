Main = {};

Main.InitCalendar = function () {
    var currentMonth = moment().format('YYYY-MM');
    var nextMonth = moment().add('month', 1).format('YYYY-MM');
    var events = [
      { date: '2016-09-21', title: 'Start înscrieri' },
      { date: '2016-10-15', title: 'Stabilirea temelor pentru fiecare categorie, competiție' },
      { date: '2016-10-16', title: 'Jurizare' },
      { date: '2016-10-17', title: 'Premierea câştigătorilor' },
      { date: '2016-10-21', title: 'Expoziție Photo Marathon’16' }
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