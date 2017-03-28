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

function loadPokemonDetails(url)
{
    var side = $(card).data('current-side');

    //if a flip has already occurred    
    if (side === 'front')
    {
        //load back and flip
        $(card).find('.back').load(url);
        $(card).data('current-side', 'back');
    }
    else  
    { 
        $(card).find('.front').load(url);
        $(card).data('current-side', 'front');
    }
    $(card).flip('toggle');
}

function SAP(p, kw, a)
{
    
    var url = "/Pokemon/SearchAll/?keywords=" + encodeURIComponent(kw) + "&page=" + encodeURIComponent(p) + '&inmod=' + a;
    $('#pokemon-tbl').load(url);
    
}

$(document).ready(function () {    

    //partial rendering for pokemon content
    $('#pokemon-list a').on("click", '.pokemon', function () {
        var PokedexID = $(this).data('dex-id');
        var card = $('#card');
        
        var url = '/Pokemon/Get/' + PokedexID;
        //var flip = $("#card").data("flip-model");

        // e.g. to see currect flip state
        // debugger;

        loadPokemonDetails(url);
        $('#pokemon-tbl').load(url);
        
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
            if ($(this) !== undefined && $(this)[0].multiple && $(this)[0].files.length > 0) {

                for (var i = 0; i < $(this)[0].files.length; i++) {

                    var reader = new FileReader();

                    reader.onload = function (e) {
                            
                            $('#img-preview').html('<img id="preview-img-' + i + '" /> ');

                            $('#preview-img-' + i)
                            .attr('src', e.target.result)
                            .width(150)
                            .height(200);
                    };
                    
                    reader.readAsDataURL($(this)[0].files[i]);
                }
            }
        });
    
    /* end file upload stuff */
   
        $('#admin-pokemon-search').on('keyup', function () {
                        
            var kw = $(this).val();
            var p = 0;
            var a = $('#filter-active-only').is(':checked');
            debugger;
            SAP(p, kw, a);
            
        });
    
        $('#filter-active-only').on('change', function () {

            var kw = $('#admin-pokemon-search').val();
            var p = 0;
            var a = $('#filter-active-only').is(':checked');
            debugger;
            SAP(p, kw, a);
        });
    
        $('#card').flip({
             trigger: "manual"
        });

        $('#poke-search-btn').on('click', function () {

            var pokesearch = $("#pokemon-search");
            var keywords = $(pokesearch).val();
            var url = '';

            //allow for filter by pokemon number if they know it
            if ($.isNumeric(keywords))
            {
                //this calls a method to figure out which side of the card is active, and load the details
                url = '/Pokemon/Get/' + PokedexID;

                if (url.length > 0)
                    loadPokemonDetails(url);
            }
            else //searching for pokemon by name
            {                
                if (keywords.length > 0)                
                    url = '/Pokemon/Search/?keywords=' + keywords; 
                else
                    location.href = "/Pokemon";
            }
        });

        $('#pokemon-search').on('keyup', function () {
            
            var keywords = $(this).val();            
            var url = '/Pokemon/Search/?keywords=' + keywords;
            $('#pokemon-list').load(url);            

        });

        $('.pagination > li > a').click(function() {
            alert('');
            var kw = $('#admin-pokemon-search').val();
            var a = $('#filter-active-only').is(':checked');
            var p = $(this).html();

            debugger;
            SAP(p, kw, a);

        });
});
