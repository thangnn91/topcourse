$(document).ready(function () {
    //Include Template
    var include = $('div[data-include]');

    include.each(function () {
        $(this).load($(this).attr('data-include'), function () {
            ewmodal();
        });
    });

    $('.img-star').mouseover(function () {
        var hasvote = $(this).parent('span').data('hasvote');
        if (hasvote)
            return;
        $('.img-star').attr('src', utils.rootUrl() + 'Content/images/rating_off.png');
        var dataStar = parseInt($(this).data('star'));
        for (var i = dataStar; i > 0; i--) {
            $('#rating_5095_' + i).attr('src', utils.rootUrl() + 'Content/images/rating_on.png');
        }
    });

    $('.img-star').mouseout(function () {
        var hasvote = $(this).parent('span').data('hasvote');
        if (hasvote)
            return;
        $('.img-star').attr('src', utils.rootUrl() + 'Content/images/rating_off.png');
    });

    $('.img-star').on('click', function () {
        var $that = $(this);
        var hasvote = $that.parent('span').data('hasvote');
        if (hasvote)
            return;
        $('.img-star').attr('src', utils.rootUrl() + 'Content/images/rating_off.png');
        var dataStar = parseInt($(this).data('star'));
        for (var i = dataStar; i > 0; i--) {
            $('#rating_5095_' + i).attr('src', utils.rootUrl() + 'Content/images/rating_on.png');
        }
        //Neu da dang nhap thi moi cho vote
        if (userInfoData.UserName) {
            var targetId = $(this).data('targetid');
            var type = $that.data('targettype');
            if (!targetId || !type)
                return;
            var param = {
                TargetID: targetId,
                Type: type,
                NumRate: dataStar
            };
            $.post(utils.rootUrl() + "Home/UserRated", param).done(function (data) {
                console.log(data);
                $that.parent('span').data('hasvote', 1);
            });
        }
        
    });

    

    function owlcarousel() {
        //$('#slider-topsearch').owlCarousel({
        //    loop: true,
        //    margin: 10,
        //    nav: true,
        //    responsive: {
        //        0: {
        //            items: 3
        //        },
        //        600: {
        //            items: 3
        //        },
        //        1000: {
        //            items: 5
        //        }
        //    }
        //});
        $('#dc_doitac').owlCarousel({
            loop: true,
            margin: 30,
            nav: false,
            autoplay: true,
            autoplayTimeout: 2000,
            responsive: {
                0: {
                    items: 3
                },
                600: {
                    items: 3
                },
                1000: {
                    items: 6
                }
            }
        });
        //$('#slider-lastview').owlCarousel({
        //    loop: false,
        //    margin: 30,
        //    nav: true,
        //    dot: false,
        //    autoplay: true,
        //    autoplayTimeout: 5000,
        //    responsive: {
        //        0: {
        //            items: 3
        //        },
        //        600: {
        //            items: 3
        //        },
        //        1000: {
        //            items: 3
        //        }
        //    }
        //});
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
    function showmoreMenu() {
        $('.sf-field-tag ul').each(function () {
            var max = 4
            if ($(this).find('li').length > max) {
                $(this).find('li:gt(' + max + ')').hide().end().append('<li class="tag_more"><span class="tag_show">Mở rộng</span></li>');
                $('.tag_more').click(function () {
                    $(this).siblings(':gt(' + max + ')').toggle();
                    if ($('.tag_show').length) {
                        $(this).html('<span class="tag_less">Thu gọn</span>');
                    } else {
                        $(this).html('<span class="tag_show">Mở rộng</span>');
                    };
                });
            };
        });
    }
    function showformsearch() {
        $('#search-button').click(function () {
            $('.header_search').toggle(300);
        })
    }
    function ewmodal() {
        $('[data-modal]').click(function () {
            var templatemodal = $(this).attr('data-modal');
            $('.ew-modal').find('.modal-dialog').load('template/modal/' + templatemodal + '.html', function () {
                ewmodal();
            });
        });
    }

    function filtersearch() {
        var numberElements = document.querySelectorAll(".sf-text-number");
        for (var i = 0; i < numberElements.length; i++) {
            var elem = numberElements[i];
            // console.log("The number: " + elem.innerText);
            elem.innerText = elem.innerText.replace(/(\d)0*$/g, "$1");
        }
    }

    function showComment() {
        $('#parent_comment').on('click',
            '.btn-show',
            function() {
                $(this).siblings('.replay-form').toggle('300');
            });
    }

    window.onload = function () {
        //Animate
        new WOW().init();
        dataList();
        ewmodal();
        owlcarousel();
        scrollTop();
        showformsearch();
        showmoreMenu();
        filtersearch();
        showComment();
    };
});