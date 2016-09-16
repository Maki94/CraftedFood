$(document).ready(function($) {

    var days = ["pon", "uto", "sre", "cet", "pet"];
    var today = new Date().getDay() -1;
    for(var i=0; today + i < 5 ; i++)
    {
      $('.card__social').append('<a class="share-icon" href="#" data-toggle="modal" data-target="#order-dialog" ' + 'attr-day="' + (today+i) + '">' + days[today + i] + '</span></a>');
    }
      $('.card__social').append('<a class="share-icon" href="#" data-toggle="modal" data-target="#order-dialog" >' + '<i class="fa fa-calendar" aria-hidden="true"></i>' + '</span></a>');



    $('.description').on('click', function(e){
      this.classList.toggle('short');
    });









});
