$(function () {
    var main = {
        init: function () {
            main.banner();
            main.profile();
            main.codeEditor();
            main.codeEditorNav();
            main.placeholder();
            main.ToolsRotation();
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
                var $T = $(this);

                var getHTML = $(this).closest($('form')).find($('textarea[name="HTML"]')).val();
                var getCSS = $(this).closest($('form')).find($('textarea[name="CSS"]')).val();
                var getJavaScript =  $(this).closest($('form')).find($('textarea[name="JavaScript"]')).val();

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
                reset(this);
            });

            $('.Code_content').each(function (i, e) {
               vAlign(e);
            });

            $('.Code_content').off('mouseup').on('mouseup', function () {
                vAlign(this);
            });

            $('.Code_content').off('keyup').on('keyup', function () {
                vAlign(this);
            });

            $('.Code_content').off('keypress').on('keypress', function () {
                $(this).css({ 'padding': 10, 'text-align': 'left' });
            });

            $('.Code_content').off('mouseenter').on('mouseenter', function () {
                if ($(this).val() === $(this).closest($('.Code_container')).find($('.placeholder')).text()) {
                    $(this).val('');
                }
            });

            $('.Code_content').off('mouseleave').on('mouseleave', function () {
                if ($(this).val().length === 0) {
                    $(this).val($(this).closest($('.Code_container')).find($('.placeholder')).text());
                    vAlign(this);
                }
                if ($(this).val() !== $(this).closest($('.Code_container')).find($('.placeholder')).text()) {
                    $(this).css({ 'padding': 10, 'text-align': 'left' });
                }
            });

            function reset(p) {
                if ($(p).val() === $(p).closest($('.Code_container')).find($('.placeholder')).text()) {
                    $(p).val('');
                }

                if ($(p).val().length === 0) {
                    $(p).val($(p).closest($('.Code_container')).find($('.placeholder')).text());
                }
            }

            //vertically align the placeholder
            function vAlign(code) {
                
                var PlaceHolderBox = $(code).closest($('.Code_container')).find($('.placeholder'));
                var PlaceHolderText = PlaceHolderBox.text();
                var PlaceHolderHeight = PlaceHolderBox.outerHeight(true);
                var TextAreaHeight = $(code).outerHeight(true);
                var TextAreaPaddingTop = (TextAreaHeight - 2 * PlaceHolderHeight) * 0.5;

                if ($(code).val() === PlaceHolderText) {
                    $(code).css({ 'padding-top': TextAreaPaddingTop, 'text-align': 'center' });
                }
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
        }

    };

    main.init();

});