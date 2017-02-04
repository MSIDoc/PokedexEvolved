// Write your Javascript code.

function setPokelistHeight() {
    var realHeight = $('#body-content-menu').height() - 180;
    $('#pokemon-list').css('max-height', realHeight + 'px');
}

//function used to determine what size we are in.
function findBootstrapEnvironment() {
    var envs = ['xs', 'sm', 'md', 'lg'];

    var $el = $('<div>');
    $el.appendTo($('body'));

    for (var i = envs.length - 1; i >= 0; i--) {
        var env = envs[i];

        $el.addClass('hidden-' + env);
        if ($el.is(':hidden')) {
            $el.remove();
            return env;
        }
    }
}

$(document).ready(function () {
    //partial rendering for pokemon content
    $('#pokemon-list a').on("click", function () {
        var PokedexID = $(this).data('dex-id');
       
        var url = '/Pokemon/Get/' + PokedexID;
        $('#pokemon-content').load(url);

    });

    /* responsive stuff*/
    setPokelistHeight();

    $(window).on('resize', function () {
        if (findBootstrapEnvironment() !== 'lg') {

        }
        setPokelistHeight();
    });
   

    /* end responsive stuff*/
    
   
});
