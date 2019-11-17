$(function () {
    var main = {
        init: function () {
            main.banner();
            main.profile();
            main.codeEditor();
            main.codeEditorNav();
            main.placeholder();
            main.ToolsRotation();
           // main.tabtrap();
            main.instruction();
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

            $('.RUN').off('click').on('click', function () {
                var IFRAME = $('#Result');

                var getHTML = $('textarea[name="HTML"]').val();
                var getCSS = $('textarea[name="CSS"]').val();
                var getJavaScript = $('textarea[name="JavaScript"]').val();

                var IFRAME_HEAD = IFRAME.contents().find('head');
                var IFRAME_BODY = IFRAME.contents().find('body');

                var js = document.createElement('script');
                var css = document.createElement('style');


                css.type = "text/css";
                js.type = "text/javascript";

                $(css).html(getCSS);
                $(js).html(getJavaScript);

                IFRAME_HEAD.html('');
                IFRAME_HEAD
                    .append(css)
                    .append(js);

                IFRAME_BODY.html('');
                IFRAME_BODY.append(getHTML);

            });

        },


        codeEditorNav: function () {
            $('.codeeditor > div').each(function () {
                if (!$(this).is('.HTML') && !$(this).is('.Result')) {
                    $(this).hide();
                }
            });

            $('.codeeditor li:first-child').addClass('active');

            $('.codeeditor li').off('click').on('click', function () {
                var el = $(this);
                $('.codeeditor li').removeClass('active');
                el.addClass('active');

                $('.codeeditor > div').each(function () {
                    $(this).hide();

                    if ($(this).is('.' + $.trim(el.find($('h3')).text())) || $(this).is('.Result')) {
                        $(this).show();
                    }

                });
            });
        },

        placeholder: function () {

            $('.code-content').each(function () {
                if ($(this).val().length === 0) {
                    Showpholder(this);
                }
            });

            $('.code-content').off('mouseleave').on('mouseleave', function () {
                if ($(this).val().length === 0) {
                    Showpholder(this);
                }
                else {
                    Hidepholder(this);
                }
            });

            function Showpholder(p) {
                $(p).closest($('.keepspace')).find($('.placeholder')).addClass("on");
            }

            function Hidepholder(p) {
                $(p).closest($('.keepspace')).find($('.placeholder')).removeClass("on");
            }

        },

        ToolsRotation: function () {
            setInterval(Toollist, 5000);

            function Toollist() {

                $('.tools li.active').each(function (index, element) {
                    if ($(element).next().length) {
                        $(element).removeClass('active');
                        $(element).next().addClass('active');
                    }
                    else {
                        $(element).removeClass('active');
                        $('.tools li:first-child').addClass('active');
                    }

                });

                $('.tools li').each(function () {
                    if ($(this).hasClass('active')) {
                        activeIndex = $(this).index();
                    }
                });

                $('.tools').css({ 'background-image': 'url("../images/bkg' + activeIndex + '.jpg")', 'background-position': 'center', 'background- clip': 'content - box' });

            }
        },
        tabtrap: function () {
            $('.tab').on('keydown', function (e) {
                if (e.keyCode === 9) {
                    e.preventDefault();
                    document.execCommand('insertHTML', false, '&#009');
                }
            });
        },
        instruction: function () {
            $('.btn-instruction').off('click').on('click', function () {
                $(this).closest($('.instruction')).find($('.code-instruction')).toggle();
            });
        }

    };

    main.init();

});