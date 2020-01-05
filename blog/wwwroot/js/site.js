$(function () {
    var main = {
        init: function () {
            main.banner();
            main.profile();
           // main.codeEditorNav(); to be deleted later
            main.placeholder();
            main.ToolsRotation();
            main.instruction();
            // main.dragE();
            main.dragbar();
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
        instruction: function () {
            $('.btn-instruction').off('click').on('click', function () {
                $(this).closest($('.instruction')).find($('.code-instruction')).toggle();
            });
        },
        dragE: function () {
            dragElement($(".dragbar")[0]);

            function dragElement(elmnt) {
                var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
                if (document.getElementById(elmnt.id + "header")) {
                    // if present, the header is where you move the DIV from:
                    document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
                } else {
                    // otherwise, move the DIV from anywhere inside the DIV:
                    elmnt.onmousedown = dragMouseDown;
                }

                function dragMouseDown(e) {
                    e = e || window.event;
                    e.preventDefault();
                    // get the mouse cursor position at startup:
                    pos3 = e.clientX;
                    pos4 = e.clientY;
                    document.onmouseup = closeDragElement;
                    // call a function whenever the cursor moves:
                    document.onmousemove = elementDrag;
                }

                function elementDrag(e) {
                    e = e || window.event;
                    e.preventDefault();
                    // calculate the new cursor position:
                    pos1 = pos3 - e.clientX;
                    pos2 = pos4 - e.clientY;
                    pos3 = e.clientX;
                    pos4 = e.clientY;
                    // set the element's new position:
                   // elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
                    elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
                }

                function closeDragElement() {
                    // stop moving when mouse button is released:
                    document.onmouseup = null;
                    document.onmousemove = null;
                }
            }
        },

        dragbar: function () {
            $('.dragbar').on('mousedown', function (e) {
                e = e || window.event;
                e.preventDefault();

                $this = $(this);
                posX0 = e.clientX;

                $('.snippets-wrap-container').off('mouseup').on('mouseup', function () {
                    $(this).off('mousedown');
                    $(this).off('mousemove');
                });

                $('.snippets-wrap-container').on('mousemove', function (e) {
                    e = e || window.event;
                    e.preventDefault();
                    dist = window.posX0 - e.clientX;
                    posX0 = e.clientX;
                    $this.css('left', $($this)[0].offsetLeft - dist + "px");
                    
                });

            });




        }

    };

    main.init();

});