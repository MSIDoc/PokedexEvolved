// Write your Javascript code.


$(document).ready(function () {
    console.log('fuck');
    $('#pokemon-list a').on("click", function () {
        var PokedexID = $(this).data('dex-id');
        console.log(PokedexID);

        $.get('/Pokemon/Get/' + PokedexID                  
        ).success(function(data){
            console.log(data);
        });

    });
});
