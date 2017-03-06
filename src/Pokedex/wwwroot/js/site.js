// Write your Javascript code.

function setPokelistHeight() {
    var realHeight = $('#body-content-menu').height() - 170;
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
        var card = $('#card');
        var side = $(card).data('current-side');
        var url = '/Pokemon/Get/' + PokedexID;
        var flip = $("#card").data("flip-model");

        // e.g. to see currect flip state
        debugger;


        //if a flip has already occurred    
        if ( side === 'front' ) {

            //load back and flip
            $(card).find('.back').load(url);
            $(card).data('current-side','back');
        }
        else { //else

            //load front and flip
            $(card).find('.front').load(url);
            $(card).data('current-side', 'front');
           // $('#pokemon-content').load(url);
        }
        
        $(card).flip('toggle');
        
    });

    /* responsive stuff*/
    setPokelistHeight();

    $(window).on('resize', function () {
        if (findBootstrapEnvironment() !== 'lg') {
            setPokelistHeight();
        }
    });
   

    /* end responsive stuff*/


    /* Begin file Upload stuff */
        $('.file-upload').change(function (event) {

            console.log(event);
            console.log($(this));

            if ($(this) !== undefined && $(this)[0].multiple && $(this)[0].files.length > 0) {

                for (var i = 0; i < $(this)[0].files.length; i++) {

                    var reader = new FileReader();

                    reader.onload = function (e) {
                            
                            //console.log(e);

                            $('#img-preview').html('<img id="preview-img-' + i + '" /> ');

                            $('#preview-img-' + i)
                            .attr('src', e.target.result)
                            .width(150)
                            .height(200);
                    };

                   // console.log($(this)[0].files[i]);

                    reader.readAsDataURL($(this)[0].files[i]);
                }
            }

        });
    
    /* end file upload stuff */
   

        $('#admin-pokemon-search').on('keyup', function () {

            
            var keywords = $(this).val();
            if (keywords.length > 0) {
                var url = '/Pokemon/SearchPokemon/?keywords=' + keywords;
                $('#pokemon-tbl').load(url);
            } else {
                location.href = "/Pokemon";
            }

        });


        $('#filter-active-only').on('change', function () {

            if ($(this).is(':checked')) {
                
                //var url = '/Pokemon/InModOnly/?inmod="' + $(this).is(':checked') + '"';
                var url = '/Pokemon/InModOnly/' + $(this).is(':checked');
                console.log(url);
                $('#pokemon-tbl').load(url);
            } else {
                location.href = "/Pokemon";
            }


        });


        $('#card').flip({
             trigger: "manual"
        });


});
