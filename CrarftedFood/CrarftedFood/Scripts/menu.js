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



    //table view

    var tableContent = "";
    var cardContent = document.getElementById('menu-content').innerHTML;

    document.querySelector('.radio #table-view').onclick = function () {
        if (tableContent.length == 0) {
            $.ajax({
                url: "/Menu/TableView",
                method: "GET",
                data: null,
                success: function (result) {
                    tableContent = result.HTMLString;
                    document.getElementById('menu-content-table').innerHTML = tableContent;
                    tableViewInit();
                }
        });
        }


        document.getElementById('menu-content-table').classList.toggle('hide');
        document.getElementById('menu-content').classList.toggle('hide');

    };

    function formatDate(date) {
        return date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear();
    }

    document.querySelector('.radio #cards-view').onclick = function () {
        document.getElementById('menu-content').classList.toggle('hide');
        document.getElementById('menu-content-table').classList.toggle('hide');

    };

    var globalMealId = -1;
    function tableViewInit() {

        $('.comments_button-table').on('click', function (e) {
            self = this;
            mealId = this.parentElement.parentElement.querySelector('#item_MealId').value;
            globalMealId = mealId;
            //get comments
            $.ajax({
                url: "Menu/GetComments",
                method: "POST",
                data: { mealId: mealId },
                success: function (result) {
                    if (result.success === true) {
                        var container = document.querySelector('.comments-table :first-child');
                        container.innerHTML = "";
                        for (var i = 0; i < result.comments.length; i++) {
                            container.innerHTML += '<div><h6>' + result.comments[i].CommenterName + '</h6><span class="date">' + formatDate(new Date("" + result.comments[i].Date)) + '</span><p>' + result.comments[i].Comment + '</p></div>';
                        }
                    }
                }
            });


            $('.comments-table-dialog').removeClass('hide');
        });

        $('.comments-table-dialog [data-dismiss="modal"]').on('click', function(e) {
            $('.comments-table-dialog').addClass('hide');
        });


        $('#menu-comment-table').on('click', function (e) {
            input = this.parentElement.getElementsByTagName('textarea')[0];
            sendData = {
                mealId: globalMealId,
                comment: this.parentElement.getElementsByTagName('textarea')[0].value
            }

            if (sendData.comment == null || sendData.comment.length < 1) {
                //prikazi poruku 
                var snackbarContainer = document.querySelector('#demo-toast-example');
                var data = { message: 'No comment to submit' };
                snackbarContainer.MaterialSnackbar.showSnackbar(data);
            } else {
                //posalji komentar
                $.ajax({
                    url: "/Menu/CommentMeal",
                    method: "POST",
                data: sendData,
                success: function(result) {
                    var now = new Date();
                    document.querySelector('.comments-table :first-child').innerHTML +=
                        '<div><h6>@UserSession.GetUser().Name</h6><span class="date">' + formatDate(now) + '</span><p>' + sendData.comment + '</p></div>';
                }
            });
            //obrisi text polje
            input.value = "";
        }
        });
    }

    


});
