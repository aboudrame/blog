$(function () {
    var main = {
        init: function () {
            main.banner();
            main.profile();
            main.codeEditor();
            main.codeEditorNav();
            main.placeholder();
            main.ToolsRotation();
            main.codeFormating();
            main.codeInstruction();
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

            $('.Code_content').each(function () {
                if ($(this).val().length === 0) {
                    Showpholder(this);
                }
            });

            $('.Code_content').off('mouseleave').on('mouseleave', function () {
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

        codeFormating: function () {

            $('.get-preview').each(function () {
                format(this);
            });

            $('.get-preview').off('keyup').on('keyup', function () {
                format(this);
            });

            function format(txt) {
                var regInlineOpen = /<inlinecode>/gi;
                var regInlineClose = /<\/inlinecode>/gi;
                var regBlockOpen = /<blockcode>/gi;
                var regBlockClose = /<\/blockcode>/gi;

                var bodyTxt = '<pre>' + $(txt).val() + '<pre>';

                cleancode = bodyTxt
                    .replace(regInlineOpen, '<div class="inline-code">')
                    .replace(regInlineClose, '</div>')
                    .replace(regBlockOpen, '<div class="block-code">')
                    .replace(regBlockClose, '</div>');

                $(txt).closest($('.row')).find($('.preview-container')).html('');
                $(txt).closest($('.row')).find($('.preview-container')).append($(cleancode));

                if ($(txt).val().length === 0) {
                    $(txt).closest($('.row')).find($('.preview')).hide();
                }
                else {
                    $(txt).closest($('.row')).find($('.preview')).show();
                }
            }

         
        },

        codeInstruction: function () {
            $('.instruction').off('click').on('click', function () {
                $('.code-instruction').toggleClass('on');
            });
        }
    };

    main.init();

});