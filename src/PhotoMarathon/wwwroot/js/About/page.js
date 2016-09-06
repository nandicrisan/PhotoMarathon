About = {};
About.InitCarousel = function () {
    jQuery(".owl-carousel.owl-testimonials2").owlCarousel({
        items: 1,
        nav: false,
        navText: ['', ''],
        dots: true,
        loop: true
    });
}

About.InitArrow = function () {
    $(document).scroll(function () {
        var y = $(this).scrollTop();
        if (y < 300) {
            $('.arrow').fadeIn();
        } else {
            $('.arrow').fadeOut();
        }
    });
}