//自定义js
$(document).ready(function () {
    // MetsiMenu
    $('#side-menu').metisMenu();

    (function() {
        //for ie8
        var h = 0;
        //除IE浏览器外
        if (window.innerWidth && window.innerHeight) {
            h = window.innerHeight;
        } else if (document.documentElement) { //IE FF 等
            h = document.documentElement.clientHeight;
        } else if (document.body.clientWidth && document.body.clientHeight) { //IE FireFox 等等
            h = document.body.clientHeight;
        }
        if (window.navigator.userAgent.indexOf("MSIE 8") !== -1) {
            $("#content-main").height(h - 140);
        }
    })();

    // 右侧边栏使用slimscroll
    $('.sidebar-container').slimScroll({
        height: '100%',
        railOpacity: 0.4,
        wheelStep: 10
    });

    // Small todo handler
    $('.check-link').click(function () {
        var button = $(this).find('i');
        var label = $(this).next('span');
        button.toggleClass('fa-check-square').toggleClass('fa-square-o');
        label.toggleClass('todo-completed');
        return false;
    });

    //固定菜单栏
    $(function () {
        $('.sidebar-collapse').slimScroll({
            height: '100%',
            railOpacity: 0.9,
            alwaysVisible: false
        });
    });

    // 菜单切换
    $('#btnChangeMenu').click(function () {
        $("body").toggleClass("mini-navbar");
        SmoothlyMenu();
    });


    // 侧边栏高度
    function fixHeight() {
        var heightWithoutNavbar = $("body > #wrapper").height() - 61;
        $(".sidebard-panel").css("min-height", heightWithoutNavbar + "px");
    }
    fixHeight();

    $(window).bind("load resize click scroll", function () {
        if (!$("body").hasClass('body-small')) {
            fixHeight();
        }
    });

    //侧边栏滚动
    $(window).scroll(function () {
        if ($(window).scrollTop() > 0 && !$('body').hasClass('fixed-nav')) {
            $('#right-sidebar').addClass('sidebar-top');
        } else {
            $('#right-sidebar').removeClass('sidebar-top');
        }
    });

    $('.full-height-scroll').slimScroll({
        height: '100%'
    });

    $('#side-menu>li').click(function () {
        if ($('body').hasClass('mini-navbar')) {
            NavToggle();
        }
        if ($(this).hasClass("active")) {
            $(this).find("ul").addClass("in").attr("aria-expanded", true);
        } else {
            $(this).find("ul").removeClass("in").attr("aria-expanded", false);
        }
    });
    $('#side-menu>li li a').click(function () {
        if ($(window).width() < 769) {
            NavToggle();
        }
    });

    $('.nav-close').click(NavToggle);

    //ios浏览器兼容性处理
    if (/(iPhone|iPad|iPod|iOS)/i.test(navigator.userAgent)) {
        $('#content-main').css('overflow-y', 'auto');
    }

});

$(window).bind("load resize", function () {
    if ($(this).width() < 769) {
        $('body').addClass('mini-navbar');
        $('.navbar-static-side').fadeIn();
    }
});

function NavToggle() {
    $('#btnChangeMenu').trigger('click');
}

function SmoothlyMenu() {
    if (!$('body').hasClass('mini-navbar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(500);
            }, 100);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(500);
            }, 300);
    } else {
        $('#side-menu').removeAttr('style');
    }
}

//判断浏览器是否支持html5本地存储
function localStorageSupport() {
    return (('localStorage' in window) && window['localStorage'] !== null);
}
