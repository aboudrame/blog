$(function () {
    var custom_codemirror = {
        init: function () {
            custom_codemirror.getCodeMirrorInstance();
            custom_codemirror.restoreDefault();
            custom_codemirror.runTheCode();
        },
        getCodeMirrorInstance: function () {
            var myModeSpec = {
                name: "htmlmixed",
                tags: {
                    style: [["type", /^text\/(x-)?scss$/, "text/x-scss"],
                    [null, null, "css"]],
                    custom: [[null, null, "customMode"]]
                }
            };
            //html editor instance
             htmleditor = CodeMirror.fromTextArea($(".html")[0], {
                lineNumbers: true,
                mode: myModeSpec,
                matchBrackets: true,
                closeBrackets: true,
                matchtags: true,
                closetags: true

            });

            //css editor instance
             csseditor = CodeMirror.fromTextArea($(".css")[0], {
                lineNumbers: true,
                mode: "htmlmixed",
                matchBrackets: true

             });

            //javascript editor instance
             jseditor = CodeMirror.fromTextArea($(".javascript")[0], {
                lineNumbers: true,
                mode: "text/javascript",
                matchBrackets: true

            });

            obj = {//global variable object to store the 3 editor instances
                html: htmleditor,
                css: csseditor,
                javascript: jseditor
            };

            //document.querySelectorAll(".CodeMirror").forEach(function (node) {
            //    node.onkeyup = function () {
            //        alert();
            //    };
            //});
        },
        restoreDefault: function () {
            setDefault();
            $('.CodeMirror').off('keyup').on('keyup', function () {
                setTimeout(function () {
                    setDefault();
                }, 3000);
                
            });

            //Run the code on page/iframe load
            $('#Result').off('load').on('load', function () {
               // custom_codemirror.runTheCode();
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
        }
        
    };

    custom_codemirror.init();

});