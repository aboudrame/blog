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
            main.codeEditorNew();
            main.codeeditor_nav();
            main.placeholder();
            main.TextareaResize();
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

                    var getHTML = $('.HTML  #HTML').val();
                    var getCSS = $('.CSS  #CSS').val();
                    var getJavaScript = '$(function () {' + $('.JavaScript  #JavaScript').val() + '});';

                    var IFRAME_HEAD = IFRAME.contents().find('head');
                    var IFRAME_BODY = IFRAME.contents().find('body');

                    var js = document.createElement('script');
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
                    IFRAME_BODY.append(getHTML);
                });
            });

        },

        codeEditorNew: function () {

                $('.RUN').off('click').on('click', function () {
                    var IFRAME = $('#Result');

                    var getHTML = $('.HTML  #HTML').val();
                    var getCSS = $('.CSS  #CSS').val();
                    var getJavaScript = '$(function () {' + $('.JavaScript  #JavaScript').val() + '});';

                    var IFRAME_HEAD = IFRAME.contents().find('head');
                    var IFRAME_BODY = IFRAME.contents().find('body');

                    var js = document.createElement('script');
                    var cjquery = document.createElement('script');
                    var css = document.createElement('style');

                        css.type = "text/css";
                         js.type = "text/javascript";
                    cjquery.type = "text/javascript";
                     cjquery.src = "https://localhost:44347/lib/jquery/dist/jquery.js";

                    $(css).html(getCSS);
                    $(js).html(getJavaScript);

                    IFRAME_HEAD.html('');
                    IFRAME_HEAD
                        .append(css)
                        .append(cjquery)
                        .append(js);

                    IFRAME_BODY.html('');
                    IFRAME_BODY.append(getHTML);
                });


        },


        codeeditor_nav: function () {
            $('.codeeditor > div').each(function () {
                if (!$(this).is('.HTML') && !$(this).is('.Result')) {
                    $(this).hide();
                }
            });

            $('.codeeditor_nav li:first-child').addClass('active');

            $('.codeeditor_nav li').off('click').on('click', function () {
                var el = $(this);
                $('.codeeditor_nav li').removeClass('active');
                el.addClass('active');

                $('.codeeditor > div').each(function () {
                    $(this).hide();

                    if ( $(this).is('.' + $.trim(el.find($('h3')).text())) || $(this).is('.Result')) {
                        $(this).show();
                    }

                });
            });
        },

        placeholder: function () {
            function pholder(x) {
                $(x).each(function () {
                    if ($(x).val().length === 0) {
                        $(x).closest('div').find($('.placeholder')).show();
                    }
                    else {
                        $(x).closest('div').find($('.placeholder')).hide();
                    }
                });
            }

            $('.codeeditor textarea').off('keyup').on('keyup', function () {
                pholder(this);
            });
        },

        TextareaResize: function () {

            $('.codeeditor textarea').off('mousedown').on('mousedown', function () {
                var el = $(this);

                repeat = setInterval(function () {
                    //$('.test').remove();
                    //el.closest('div').find($('.placeholder')).html($('<span class="test">' + repeat + '</span>'));

                    el.closest('div').find($('.placeholder')).css('width', el.outerWidth(true));
                }, 100);
                
            });
            
            $('.codeeditor textarea').off('mouseup').on('mouseup', function () {
               // alert('before= ' + repeat);
                clearInterval(repeat);uk
               // alert('after= ' + repeat);
            });
        }

    };

    main.init();

});