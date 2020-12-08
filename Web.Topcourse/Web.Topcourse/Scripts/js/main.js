$(document).ready(function() {
	function StickyMenu() {
        var mainmenu = $('header');
        var sidebar = $('.sidebar');
        var socialbox = $('.social-box');
        $(window).scroll(function() {
            var st = $(this).scrollTop();
            if ( st < 350 ){
                mainmenu.removeClass('fixed');
                //sidebar.removeClass('fixed');
                socialbox.removeClass('fixed');
            } else {
                if(st > 350){
                    mainmenu.addClass('fixed');
                    //sidebar.addClass('fixed');
                    socialbox.addClass('fixed');
                }
            }
        });
    }

    function SliderCaroulsel() {
        $('.slider-banner .owl-carousel').owlCarousel({
            loop: true,
            margin:0,
            items:1,
            nav:true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots:true,
            autoplay:true,
            autoplayTimeout:5000,
        });
        $('.slider-daotao-image .owl-carousel').owlCarousel({
            loop: true,
            margin:0,
            items:1,
            nav:true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots:false,
            autoplay:true,
            autoplayTimeout:5000
        });
        $('.ct-hocbong .owl-carousel').owlCarousel({
            loop:false,
            margin:20,
            items:3,
            nav:true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots:false,
            autoplay:true,
            autoplayTimeout:5000,
            responsive:{
                0:{
                    items:1
                },
                600:{
                    items:1
                },
                1000:{
                    items:2
                },
                1300:{
                    items:3
                }
            }
        });

        $('.slider-truong .owl-carousel').owlCarousel({
            loop: true,
            margin: 0,
            items: 2,
            nav: true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots: false,
            autoplay: true,
            autoplayTimeout: 5000,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 2
                }
            }
        });

        $('.wishlist-login .owl-carousel').owlCarousel({
            loop:false,
            margin:20,
            nav:true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots:false,
            autoplay:true,
            autoplayTimeout:5000,
            responsive:{
                0:{
                    items:1
                },
                600:{
                    items:2
                },
                1000:{
                    items:4
                }
            }
        });
        $('.related-articles-truong .owl-carousel').owlCarousel({
            loop: true,
            margin:20,
            nav:true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots:false,
            autoplay:true,
            autoplayTimeout:5000,
            responsive:{
                0:{
                    items:1
                },
                600:{
                    items:2
                },
                1000:{
                    items:4
                }
            }
        });
        $('.testimonials .owl-carousel').owlCarousel({
            loop: true,
            margin:60,
            items:2,
            nav:true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots:false,
            autoplay:true,
            autoplayTimeout:5000,
            responsive:{
                0:{
                    items:1
                },
                600:{
                    items:1
                },
                1000:{
                    items:2
                }
            }
        });
        
        $('.memberships .owl-carousel').owlCarousel({
            loop: true,
            margin: 0,
            items: 6,
            nav: false,
            dots: false,
            autoplay: true,
            autoplayTimeout: 5000,
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 6
                }
            }
        });

        $('.slider-course .owl-carousel').owlCarousel({
            loop: true,
            margin: 0,
            items: 1,
            nav: true,
            navText: ['<span class="icon-arrow-left"></span>', '<span class="icon-arrow-right"></span>'],
            dots: false,
            autoplay: true,
            autoplayTimeout: 5000,
            responsive: {
                0: {
                    items: 1
                }
            }
        });
    }

    function ShowShortlist() {
        $('.btn-sosanh').click(function(){
            $('.box-shortlist').show('400');
        });
        $('.btn-close').click(function(){
            $('.box-shortlist').hide('400');
        })
    }
    function ShowDanhmuc() {
        $('a.btn-danhmuc').hover(function() {
            $('.danhmuc').show('400');
        });
        $('.v3_chonloc').click(function(){
            $('.sidebar').show();
        });
        $('.sidebar .btn_close').click(function(){
            $('.sidebar').hide();
        });
        $('.v3_filter_ngang').click(function(){
            $('.filter-top').show();
        });
        $('.filter-top .btn_close').click(function(){
            $('.filter-top').hide();
        });
        $('#m_search .btn-close').click(function () {
            $('#m_search').hide();
        });
    };
    function ShowSocialInfo() {

        $('[id^=social-action-]').click( function(){
            var id_selector = $(this).attr("id");
            var id = id_selector.substr(id_selector.length -1);
            id = parseInt(id);
            var goTo = $(this).data('index');
            $('.social-action ul li').each(function(index){
                if(index == goTo && !$('#social-action-'+(index + 1 )).hasClass("active")){
                    $('#social-action-'+(index + 1 )).addClass('active');
                    $('[data-in^='+(index + 1 )+']').show();
                }else {
                    $('#social-action-'+(index + 1 )).removeClass('active');
                    $('[data-in^='+(index + 1 )+']').hide();
                }
            });
        });


    }
    function SortByList() {
        $('.sortby .sortby-list').click(function(){
            $(this).addClass('active');
            $('.sortby .sortby-grid').removeClass('active');
            $('.box-khoahoc').removeClass('box-grid');
            $('.list-khoahoc').removeClass('row');
            $('.item-khoahoc-grid ').removeClass('col-md-4');
            $("#hdTypeView").val(1);
        });
        $('.sortby .sortby-grid').click(function(){
            $(this).addClass('active');
            $('.sortby .sortby-list').removeClass('active');
            $('.box-khoahoc').addClass('box-grid');
            $('.list-khoahoc').addClass('row');
            $('.item-khoahoc-grid').addClass('col-md-4');
            $("#hdTypeView").val(2);
        });
    };
     $( ".danhmuc-content" ).mouseleave(function() {
        $(".danhmuc-content").hide('400');
    });
    $( ".danhmuc" ).mouseleave(function() {
        $(".danhmuc").hide('400');
    });

    function SortCourse() {
        $("#kh-nganhan .btn-sort i,#kh-daihan .btn-sort i").click(function () {
            $("#kh-nganhan .btn-sort i,#kh-daihan .btn-sort i").removeClass("active");
            $(this).addClass("active");
        });
    }
    function dataList() {
        $("#text_search_2").on('click', 'li', function () {
            $("#course_name_2").val($(this).text());
        });
        $("#text_search_1").on('click', 'li', function () {
            $("#course_name_1").val($(this).text());

        });
        $(document).on("click", function (event) {
            if (!$(event.target).is(".sf-input-text")) {
                $(".data-list").css("display", "none");
            }

        });
    }
    function findOffset(element) {
        var top = 0, left = 0;
        do {
            top += element.offsetTop || 0;
            left += element.offsetLeft || 0;
            element = element.offsetParent;
        } while (element);
        return {
            top: top,
            left: left
        };
    }
    function scrollTop() {
        var stickyHeader = document.getElementById('header_fix');
        if (stickyHeader !== null) {
             var headerOffset = findOffset(stickyHeader);
            window.onscroll = function () {
                var bodyScrollTop = document.documentElement.scrollTop || document.body.scrollTop;
                if (bodyScrollTop > headerOffset.top) {
                    stickyHeader.classList.add('search_fix');
                } else {
                    stickyHeader.classList.remove('search_fix');
                }
            };
        }
       
    }

    function scrollTopSideBar() {
        $(window).bind("scroll", function (e) {
            var stickyHeader = document.getElementById('sidebar-right');
            if (stickyHeader !== null) {
                var headerOffset = findOffset(stickyHeader);
                var top = $(window).scrollTop();
                if (top > headerOffset.top && top < $("#page").height()) {
                    $(".sidebar-right").addClass("sidebar_fix");
                } else {
                    $(".sidebar-right").removeClass("sidebar_fix");
                }
            }
            
        });
    }

    function clickTab() {
        $("#header_fix .dropdown-menu a").click(function () {
            //$("#header_fix .dropdown-menu li").removeClass("active");
            //$(this).parent().addClass("active");
            $(this).parent().parent().parent().find(".title-item").html($(this).text());
        });
    }
    function SearchMobile() {
        $("#menu-button").click(function() {
            $("#m_search").toggle();
        });
    }
    window.onload = function() {
        //Animate
        new WOW().init();
        StickyMenu();
        SliderCaroulsel();
        ShowDanhmuc();
        //ShowShortlist();
        SortByList();
        ShowSocialInfo();
        SortCourse();
        dataList();
        scrollTop();
        scrollTopSideBar();
        
        
        clickTab();
        SearchMobile();
    };
   
    
});