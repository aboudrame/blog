$(function () {
    var custom_codemirror = {
        init: function () {
            custom_codemirror.renderTextareaContent();
            custom_codemirror.getCodeMirrorInstance();
            custom_codemirror.restoreDefault();
            custom_codemirror.runTheCode();
            custom_codemirror.CreateEdit();
            custom_codemirror.CodePreview();
            custom_codemirror.indexPageCode();
        },
        getCodeMirrorInstance: function () {
            myModeSpec = {
                name: "htmlmixed",
                tags: {
                    style: [["type", /^text\/(x-)?scss$/, "text/x-scss"],
                    [null, null, "css"]],
                    custom: [[null, null, "customMode"]]
                }
            };

                //html editor instance
            if ($('.html').length > 0 || $('.code').length > 0) {

                $('.html, .code10').each(function () {
                    htmleditor = CodeMirror.fromTextArea($(this)[0], {
                        lineNumbers: true,
                        mode: myModeSpec,
                        matchBrackets: true,
                        closeBrackets: true,
                        matchtags: true,
                        closetags: true
                    });
                });
            }
                 //css editor instance
            if ($(".css").length > 0) {
                csseditor = CodeMirror.fromTextArea($(".css")[0], {
                    lineNumbers: true,
                    mode: "htmlmixed",
                    matchBrackets: true

                });
            }

            if ($(".javascript").length > 0) {
                //javascript editor instance
                jseditor = CodeMirror.fromTextArea($(".javascript")[0], {
                    lineNumbers: true,
                    mode: "text/javascript",
                    matchBrackets: true

                });
            }

            if ($(".javascript").length > 0 && $(".css").length > 0 && $(".html").length > 0) {
                obj = {//global variable object to store the 3 editor instances
                    html: htmleditor,
                    css: csseditor,
                    javascript: jseditor
                };
            }
            else {
                obj = "nodata";
            }

        },
        restoreDefault: function () {

            if (Object.keys(obj).length > 0) {
                setDefault();
                $('.CodeMirror').off('keyup').on('keyup', function () {
                    setTimeout(function () {
                        setDefault();
                    }, 3000);

                });
            }

            //Run the code on page/iframe load
            $('#Result').off('load').on('load', function () {
                custom_codemirror.runTheCode();
                $(this).off('load');
            });

            function setDefault() {
                for (key in obj) {
                    switch (key) {

                        case "html":

                            if (htmleditor.doc.getValue().length === 0) {
                                var html = '<h4 style="text-align: center;">CODE SAMPLE</h4>\n\n';
                                html += '<div class = "sampleHTML">\n';
                                html += ' <p>Hello world!</p>\n';
                                html += '</div>';
                                htmleditor.doc.setValue(html);
                            }
                            break;
                        case "css":
                            if (csseditor.doc.getValue().length === 0) {
                                var css = '\/\/ SAMPLE CSS3 CODE\n\n';
                                css += '<style type="text/css">\n';
                                css += '.sampleHTML {\n';
                                css += 'color: red; \n';
                                css += 'font-size: 36px;\n';
                                css += '}\n';
                                css += '</style>';

                                csseditor.doc.setValue(css);
                            }
                            break;
                        case "javascript":
                            if (jseditor.doc.getValue().length === 0) {
                                var js = '\/\/ SAMPLE JavaScript/jQuery code\n\n';
                                js += '\<script type ="text/javascript">\n';
                                js += '$(function () {\n';
                                js += '$(".sampleHTML > p").html($(".sampleHTML > p").text()' + " + ' from <a href=" + '"/"' + ">aboudrame</a>'" + ');\n';
                                js += '});\n';
                                js += '<\/script>';

                                jseditor.doc.setValue(js);
                            }
                            break;
                        default:
                    }
                }
            }
        },
        runTheCode: function () {
            exec();
            $('.RUN').off('click').on('click', function () {
                exec();
            });

            $(".CodeTester .CodeMirror").off("keyup").on("keyup", function () {
                exec();
            });

            function exec() {
                iframeHead = $('#Result').contents().find('head');
                iframeBody = $('#Result').contents().find('body');
                iframeHead.html('');
                iframeBody.html('');

                for (key in obj) {
                    if (key === 'css' || key === 'javascript') {
                        iframeHead.append(obj[key].doc.getValue());
                    }
                    if (key === 'html') {
                        iframeBody.append(obj[key].doc.getValue());
                    }

                }
            }

        },
        renderTextareaContent: function () {
            $('.highlightcodes').each(function () {
                //this will render the textarea content, removing the parent
                $(this).replaceWith($(this).clone().val());
            });
        },
        CreateEdit: function () {
            assignEditors();

            function assignEditors() {
                myModeSpec = {
                    name: "htmlmixed",
                    tags: {
                        style: [["type", /^text\/(x-)?scss$/, "text/x-scss"],
                        [null, null, "css"]],
                        custom: [[null, null, "customMode"]]
                    }
                };

                $('.code').each(function () {
                    htmleditor = CodeMirror.fromTextArea($(this)[0], {
                        lineNumbers: true,
                        mode: myModeSpec,
                        matchBrackets: true,
                        closeBrackets: true,
                        matchtags: true,
                        closetags: true
                    });
                });
            }
        },

        CodePreview: function () {
            //$('.custom-highlighter .preview').off('click').on('click', function () {
            //    codesnippet($(this));
            //});

            $('.CodeMirror').off("keyup").on("keyup", function () {
                codesnippet($(this));
            });

            function codesnippet(el) {
                var editorval = htmleditor.getValue();
                x = '<pre>' +
                    editorval
                    .replace(/\[\[/g, '<textarea class="code1">')
                    .replace(/]]/g, '</textarea>')
                    + '</pre>';
                $(el).closest($('.custom-highlighter')).find($('.preview-container')).html(x);

                $(".code1").each(function () {
                    $(this).html($(this).html().replace(/\[{/g, "").replace(/\}]/g, ""));
                });

                $('.preview-container').each(function () {
                    $(this).html($(this).html().replace(/\[{/g, '<span class="highlight">').replace(/}]/g, "</span>"));
                });

                $('.code1').each(function () {
                    $(this).html($.trim($(this).html())); //this will remore the empty line at the bottom of the codeMirror instance
                    htmleditor1 = CodeMirror.fromTextArea($(this)[0], {
                        lineNumbers: true,
                        mode: myModeSpec,
                        matchBrackets: true,
                        closeBrackets: true,
                        matchtags: true,
                        closetags: true
                    });
                });

                var cm = $(el).closest($('.custom-highlighter')).find($('.included-snippets')).next($('.CodeMirror'));
                var cm_preview = $(el).closest($('.custom-highlighter')).find($('.preview-container'));

                //if ($(el).val() === 'Edit') {
                //    $(el).val('Preview');
                //    cm_preview.hide();
                //    cm.show();
                //}
                //else {
                //    $(el).val('Edit');
                //    cm_preview.show();
                //    cm.hide();
                //}
            }
        },

        indexPageCode: function () {
            $('.codeIndex').each(function () {
                x = '<pre>' + $(this).val().replace(/\[\[/g, '<textarea class="codeIdx">').replace(/\]\]/g, '</textarea>') + '</pre>';
                $(this).closest($('.custom-highlighter')).find($('.preview-container')).css('display', 'block').html(x);
                $(this).hide();
            });

            $('.codeIdx').each(function () {
                $(this).html($.trim($(this).html())); //this will remore the empty line at the bottom of the codeMirror instance
                htmleditorIndex = CodeMirror.fromTextArea($(this)[0], {
                    lineNumbers: true,
                    mode: myModeSpec,
                    matchBrackets: true,
                    closeBrackets: true,
                    matchtags: true,
                    closetags: true
                });
            });
        }
    };

    custom_codemirror.init();

});