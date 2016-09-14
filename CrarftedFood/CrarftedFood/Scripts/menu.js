$(document).ready(function($) {

    var days = ["pon", "uto", "sre", "cet", "pet"];
    var today = new Date().getDay() -1;
    for(var i=0; today + i < 5 ; i++)
    {
      $('.card__social').append('<a class="share-icon" href="#"' + 'attr-day="' + (today+i) + '">' + days[today + i] + '</span></a>');
    }
      $('.card__social').append('<a class="share-icon" href="#">' + '<i class="fa fa-calendar" aria-hidden="true"></i>' + '</span></a>');

    var dialog = document.getElementById('order_dialog');

    $('.card__social > a').on('click', function(e){
        dialog.querySelector('span').innerHTML += "datum je" + this.attributes['attr-day'].value;
       dialog.showModal();
    });

    if (! dialog.showModal) {
      dialogPolyfill.registerDialog(dialog);
    }
    dialog.querySelector('button:not([disabled])')
    .addEventListener('click', function() {
      dialog.close();

      //collapse order button
      $('.card__social').toggleClass( 'card__social--active' );
      $('#share').toggleClass('share-expanded');

    });

    $('.description').on('click', function(e){
      $('.description').toggleClass("short");
    });

    $('.comments_button').on('click', function(e){
      $('.comments').toggleClass("hide");
      $('.card__article').toggleClass("hide");
    });

    $('.card__share > a').on('click', function(e){
        e.preventDefault() // prevent default action - hash doesn't appear in url
        $(this).parent().find( 'div' ).toggleClass( 'card__social--active' );
        $(this).toggleClass('share-expanded');
    });







});
