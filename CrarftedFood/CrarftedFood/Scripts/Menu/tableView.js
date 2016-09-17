﻿var tableContent = "";
var cardContent = document.getElementById('menu-content').innerHTML;


//show table view
document.querySelector('.radio #table-view').onclick = function () {
    if (tableContent.length == 0) {
        $.ajax({
            url: url.tableView,
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


//show cards view
document.querySelector('.radio #cards-view').onclick = function () {
    document.getElementById('menu-content').classList.toggle('hide');
    document.getElementById('menu-content-table').classList.toggle('hide');

};

var globalMealId = -1;
//add listeners
function tableViewInit() {

    //get commnets for meal
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

    //close dialog(doesn't work properly)
    $('.comments-table-dialog [data-dismiss="modal"]').on('click', function (e) {
        $('.comments-table-dialog').addClass('hide');
    });


    //add comment
    $('#add-comment-table').on('click', function (e) {
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
                url: url.commentMeal,
                method: "POST",
                data: sendData,
                success: function (result) {
                    var now = new Date();
                    document.querySelector('.comments-table :first-child').innerHTML +=
                        '<div><h6>' + userName + '</h6><span class="date">' + formatDate(now) + '</span><p>' + sendData.comment + '</p></div>';
                }
            });
            //obrisi text polje
            input.value = "";
        }
    });
}