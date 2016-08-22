About = {};
About.InitCarousel = function () {
    jQuery(".owl-carousel.owl-services").owlCarousel({
        items: 4,
        margin: 30,
        nav: false,
        navText: ['', ''],
        dots: true,
        loop: true,
        responsive: {
            0: {
                items: 1
            },
            768: {
                items: 2
            },
            992: {
                items: 3
            }
        }
    });

    jQuery(".owl-carousel.owl-testimonials2").owlCarousel({
        items: 1,
        nav: false,
        navText: ['', ''],
        dots: true,
        loop: true
    });
}