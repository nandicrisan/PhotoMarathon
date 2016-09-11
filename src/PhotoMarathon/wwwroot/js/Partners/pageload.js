Partners = {};

Partners.InitPage = function () {
    jQuery(".owl-carousel.owl-testimonials").owlCarousel({
        items: 1,
        nav: false,
        navText: ['', ''],
        loop: true
    });
    jQuery(".owl-carousel.owl-clients").owlCarousel({
        center: true,
        autoPlay: 3000,
        items: 7,
        margin: 30,
        nav: true,
        navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'],
        dots: true,
        loop: true,
        responsive: {
            0: {
                items: 2
            },
            768: {
                items: 3
            },
            992: {
                items: 4
            }
        },
        autoWidth: true,
    });
}