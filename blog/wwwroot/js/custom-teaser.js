$(function () {
    $('.teaser').each(function () {
        var el = $(this);
        var long_text = $.trim(el.html());
        var n_sentences = el.attr("data-teaser-length") || 2;
        var short_text = $.trim(el.html()).split(/([\.\?\!])\s/, n_sentences * 2).map(function (d, i) { return i % 2 === 0 ? d : d + " "; }).join("");

        if (long_text !== short_text) {
            el.html('');
            el.append(
                "<div class='teaser-long'>" +
                    long_text +
                    ' <span class="teaser-see-less text-info" style="cursor:pointer;margin-top:5px;">See less</span>...' +
                "</div>" +
                '<div class="teaser-short">' +
                    short_text +
                    ' <span class="teaser-see-more text-info" style="cursor:pointer;margin-top:5px;">Read more</span>...' +
                '</div>'
            );
            el.children('.teaser-long').hide();
        }

        $('.teaser-see-more').off('click').on('click', function () {
            var element = $(this);
            element.closest($('.teaser-short')).prev($('.teaser-long')).slideDown(2000);
            element.closest($('.teaser-short')).slideUp(2000);
        });

        $('.teaser-see-less').off('click').on('click', function () {
            var element = $(this);
            element.closest($('.teaser-long')).next($('.teaser-short')).slideDown(2000);
            element.closest($('.teaser-long')).slideUp(2000);  

        });

    });

    $('.teaser-see-more, .teaser-see-less')
        .mouseenter(function () { $(this).css("text-decoration", "underline"); })
        .mouseleave(function () { $(this).css("text-decoration", "none"); });

});