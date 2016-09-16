$(document).ready(function($) {

    var days = ["pon", "uto", "sre", "cet", "pet"];
    var today = new Date().getDay() -1;
    for (var i = 0; today + i < 5 || i < 2; i++) {
        $('.card__social').append('<a class="share-icon order-btn" href="#" data-toggle="modal" data-target="#order-dialog" ' + 'attr-day="' + ((today + i) % 5) + '">' + days[(today + i) % 5] + '</span></a>');
    }
      $('.card__social').append('<a class="share-icon order-btn" href="#" data-toggle="modal" data-target="#order-dialog" >' + '<i class="fa fa-calendar" aria-hidden="true"></i>' + '</span></a>');

      $('.card__share > a').on('click', function (e) {
          e.preventDefault() // prevent default action - hash doesn't appear in url
          $(this).parent().find('div').toggleClass('card__social--active');
          $(this).toggleClass('share-expanded');
      });

      var dialog = document.getElementById('order-dialog');

      

      $('.order-btn').on('click', function(e) {
          var body = "";
          var mealId = this.parentElement.parentElement.parentElement.getElementsByClassName('meal-id')[0].innerHTML;
          var fullDate = new Date();
          if (this.attributes['attr-day']) {
              var date = this.attributes['attr-day'].value - today;
              if (date < 0)
                  date = date + 7;
              fullDate.setDate(new Date().getDate() + date);
          }

          var dateString = fullDate.getDate() + "/" + (fullDate.getMonth() + 1) + "/" + fullDate.getFullYear();

          body += '<div class="form-horizontal">';

          body += '<div class="form-group"> \
                        <label class="control-label col-md-3" for="quantity">Quantity</label> \
                        <div class="col-md-9"> \
                           <input class="quantity" type="number" id="quantity" value="1"/> \
                        </div> \
                    </div>';

          body += '<div class="form-group"> \
                        <label class="control-label col-md-3" for="Date">Choose date</label> \
                        <div class="col-md-9"> \
                           <input class="date" size="16" type="text" value="' + dateString + '"> \
                           <span class="add-on"><i class="icon-th"></i></span></div> \
                        </div>';

        
          body += '<div class="form-group"> \
                        <label class="control-label col-md-3" for="add-note">Add note</label> \
                        <div class="col-md-9"> \
                           <textarea type="text" id="add-note"></textarea> \
                        </div> \
                    </div>';
          body += '</div>';

          body += '<span class="meal-id" hidden data-id="' + mealId + '"></span>';

          dialog.querySelector('.modal-body').innerHTML = body;
          $('.modal-body input.date').datepicker({
              minDate: new Date(),
              autosize: true,
              firstDay: 1,
              prevText: "",
          });
      });



    $('.description').on('click', function(e){
      this.classList.toggle('short');
    });


    $('.mdl-chip').on('click', function (e) {
        var val = this.getElementsByTagName('span')[0].innerHTML;
        $('#category-filter option:contains("' + val + '")').attr('selected', 'selected');
        $('#category-filter').trigger('change');
    });







});
