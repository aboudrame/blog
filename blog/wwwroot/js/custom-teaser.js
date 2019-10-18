$(function () {
    $('.teaser').each(function () {
        var el = $(this);
        var long_text = $.trim(el.html().replace(/<pre>/, '').replace(/<\/pre>/, ''));
       // alert(long_text);
        var n_sentences = el.attr("data-teaser-length") || 2;
        var short_text = long_text.split(/([\.\?\!])\s/, n_sentences * 2).map(function (d, i) { return i % 2 === 0 ? d : d + " "; }).join("");

      //  alert(short_text);

        el.html('');
        if (long_text !== short_text) {
            el.append(
                $('<div class="teaser-long">' +
                    '<pre>' +
                    long_text +
                    ' <span class="teaser-see-less text-info" style="cursor:pointer;margin-top:5px;">See less</span>...' +
                    '<\/pre>' +
                  '</div>')
                ,
                $('<div class="teaser-short">' +
                    '<pre>' +
                    short_text +
                    ' <span class="teaser-see-more text-info" style="cursor:pointer;margin-top:5px;">Read more</span>...' +
                    '<\/pre>' +
                  '</div>')
            );

            el.children('.teaser-long').hide();
        }

        //alert($(this).html());

        $('.teaser-see-more').off('click').on('click', function () {
            var element = $(this);
            element.closest($('.teaser')).find($('.teaser-long')).slideDown(2000);
            element.closest($('.teaser')).find($('.teaser-short')).slideUp(2000);
        });

        $('.teaser-see-less').off('click').on('click', function () {
            var element = $(this);
            element.closest($('.teaser')).find($('.teaser-short')).slideDown(2000);
            element.closest($('.teaser')).find($('.teaser-long')).slideUp(2000);  

        });

    });

    $('.teaser-see-more, .teaser-see-less')
        .mouseenter(function () { $(this).css("text-decoration", "underline"); })
        .mouseleave(function () { $(this).css("text-decoration", "none"); });

});