/// <reference path="test.js" />
/// <reference path="../lib/jquery/dist/jquery.js" />
/// <reference path="../lib/jquery/dist/jquery.js" />
/// <reference path="../lib/jquery/dist/jquery.js" />
$(function () {
    var main = {
        init: function () {
            main.banner();
            main.profile();
            main.codeEditor();
        },

        banner: function () {
            $('.banner li:first-child').addClass("active");

            myTimer = setInterval(function () {

                $('.banner li.active').each(function (index, element) {

                    $(element).removeClass("active");

                    if ($(element).next($("li")).length) {
                        $(element).next($("li")).addClass("active");
                    }
                    else {
                        $('.banner li:first-child').addClass("active");
                    }
                });

            }, 3000);

        },

        profile: function () {
            $('.profile img').show(5000);
        },

        codeEditor: function () {
            $('.Result #Result').off('load').on('load', function () {
                var IFRAME = $(this);
                
                $(this).contents().find('.RUN').off('click').on('click', function () {

                var getHTML       = $('.HTML  #HTML').val();
                var getCSS        = $('.CSS  #CSS').val();
                var getJavaScript = '$(function () {' + $('.JavaScript  #JavaScript').val() + '});';

                var IFRAME_HEAD = IFRAME.contents().find('head');
                var IFRAME_BODY = IFRAME.contents().find('body');

                var  js = document.createElement('script');
                var css = document.createElement('style');

                    css.type = "text/css";
                     js.type = "text/javascript";

                     $(js).html(getJavaScript);
                    $(css).html(getCSS);

                    IFRAME_HEAD.find('.temp').remove();
                    IFRAME_HEAD.append('<div class="temp"></div>')
                               .append(css)
                               .append(js);

                    IFRAME_BODY.find('>*:not(.RUN_container)').remove();
                    IFRAME_BODY.prepend(getHTML);
                });
            });

        }

    };

    main.init();

});