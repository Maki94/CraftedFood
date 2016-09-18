//rating
$('.rating-list > li').on('click', function (e) {
    sendData = {
        mealId: this.parentElement.parentElement.parentElement.parentElement.getElementsByClassName('meal-id')[0].innerHTML,
        rating: parseInt(this.innerHTML.charAt(0))
    };

    $.ajax({
        url: url.rateMeal,
        method: "POST",
        data: sendData,
        success: function (result) {
            var inner = document.getElementById(sendData.mealId).getElementsByClassName('rating')[0].innerHTML;
            document.getElementById(sendData.mealId).getElementsByClassName('rating')[0].innerHTML = result.newRating + " " + inner.substr(inner.indexOf("<"));
            var snackbarContainer = document.querySelector('#demo-toast-example');
            var data = { message: 'Rate submited' };
            snackbarContainer.MaterialSnackbar.showSnackbar(data);
        }
    });
});

// postavljanje komentara na enter
var ENTER_CODE = 13;
function eventFire(el, etype) {
    if (el.fireEvent) {
        el.fireEvent('on' + etype);
    } else {
        var evObj = document.createEvent('Events');
        evObj.initEvent(etype, true, false);
        el.dispatchEvent(evObj);
    }
}

$('#menu-comment').bind('keypress', function (event) {
    var x = event.keyCode;
    if (x === ENTER_CODE) {
        eventFire(document.getElementById('menu-comment-share'), 'click');
    }
});
// END

function formatDate(date) {
    return date.getDate() + "." + (date.getMonth() + 1) + "." + date.getFullYear();
}

//show comments
$('.comments_button :not(.disabled)').on('click', function (e) {
    self = this;
    mealId = this.parentElement.parentElement.parentElement.parentElement.getElementsByClassName('meal-id')[0].innerHTML;

    if (this.parentElement.parentElement.parentElement.getElementsByClassName('comments')[0].classList.contains('hide')) {
        //get comments
        $.ajax({
            url: url.getComments,
            method: "POST",
            data: { mealId: mealId },
            success: function (result) {
                if (result.success === true) {
                    var container = self.parentElement.parentElement.parentElement.querySelector('.comments :first-child');
                    container.innerHTML = "";
                    for (var i = 0; i < result.comments.length; i++) {
                        container.innerHTML += '<div><h6>' + result.comments[i].CommenterName + '</h6><span class="date">' + formatDate(new Date("" + result.comments[i].Date)) + '</span><p>' + result.comments[i].Comment + '</p></div>';
                    }
                }
            }
        });
    }

    ///hide immage
    this.parentElement.parentElement.parentElement.parentElement.getElementsByClassName('card__image')[0].classList.toggle('shrink');

    //toggle content
    this.parentElement.parentElement.parentElement.getElementsByClassName('comments')[0].classList.toggle('hide');
    this.parentElement.parentElement.parentElement.getElementsByClassName('card__article')[0].classList.toggle('hide');

    //toggle icon
    $(this).toggleClass('fa-reply fa-comments');
});

//add comment
$('.add-comment-wrapper button').on('click', function (e) {
    input = this.parentElement.getElementsByTagName('textarea')[0];
    sendData = {
        mealId: this.parentElement.parentElement.parentElement.parentElement.getElementsByClassName('meal-id')[0].innerHTML,
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
            url: url.commentMeal,
            method: "POST",
            data: sendData,
            success: function (result) {
                var now = new Date();
                document.querySelector('*[data-id="' + sendData.mealId + '"]').parentElement.querySelector('.comments :first-child').innerHTML +=
                    '<div><h6>'+userName+'</h6><span class="date">' + formatDate(now) + '</span><p>' + sendData.comment + '</p></div>';
            }
        });
        //obrisi text polje
        input.value = "";
    }
});