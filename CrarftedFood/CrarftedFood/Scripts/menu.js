$(document).ready(function ($) {

    //search
    searchPage('#searchMealsText', '#menu-wrapper', '.meal-wrapper', containersAttribute = 'data-id');

    //menu description expanding
    $('.description').on('click', function (e) {
        this.classList.toggle('short');
    });

    //filter by category when clicked on one
    $('.mdl-chip').on('click', function (e) {
        var val = this.getElementsByTagName('span')[0].innerHTML;
        $('#category-filter option:contains("' + val + '")').attr('selected', 'selected');
        $('#category-filter').trigger('change');
    });

    //delete (open delete dialog)
    $('.delete-btn').on('click', function (e) {
        idNum = this.parentElement.attributes['data-id'].value;
        $('#delete-dialog .modal-footer .delete-confirm').attr("href", url.del + "/" + idNum);
    });

    //category filter
    $('#category-filter').change(function () {
        $(".show-category").removeClass("show-category");

        if ($('#category-filter option:selected').val() == -1) {
            $('.meal-wrapper').addClass("show-category");
        } else {
            var pom = $('#menu-wrapper .category').parent().find(':contains("' + $('#category-filter option:selected').text() + '")');
            var results = $('.meal-wrapper').has(pom);
            results.addClass("show-category");
        }
    });

});

function showSnackbar(message) {
    var snackbarContainer = document.querySelector('#demo-toast-example');
    var data = { message: message };
    snackbarContainer.MaterialSnackbar.showSnackbar(data);
}
