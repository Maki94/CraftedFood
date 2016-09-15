
/* staro
function search(containerId, text, ignoreExcludes = true, containersAttribute = 'data-id'){

  $(".show-search").removeClass("show-search");
  $(".targetfound").removeClass("targetfound");
  originalHtml = document.getElementById(containerId).innerHTML;
  newHtml = originalHtml.replace(new RegExp(text + "(?![^<>]*>)", "ig"), function(e){
      return "<span class='targetfound'></span>" + e;
   });
   document.getElementById(containerId).innerHTML = newHtml;

 debugger;
   var targets = document.getElementsByClassName("targetfound");
   for(var i=0; i<targets.length; i++){
     target = targets[i];

      if(ignoreExcludes && target.parentElement.classList.contains('search-exclude')){
        continue;
      }

     while(target.parentElement && !target.parentElement.hasAttribute(containersAttribute)) {

       target = target.parentElement;
     }
     target.parentElement.classList.add('show-search');

   }
   $(".targetfound").remove();
}*/

$( document ).ready(function() {
  //case insensitive search
      $.expr[":"].contains = $.expr.createPseudo(function (arg) {
          return function (elem) {
              return $(elem).text().toUpperCase().indexOf(arg.toUpperCase()) >= 0;
          };
      });
});

//unutar containera trazi text koji je u textFieldu i prikazuje elemente u okviru containerSelector (koji jos ima i atribut containersAttribute) gde je nesto nadjeno
//potreban css: (ContainerSelector).emp-container:not( .show-search ) {   display: none;}
function searchPage(textFieldSelector, container, containerSelector,  containersAttribute = 'data-id') {
  $(textFieldSelector).on('change keydown paste input', function () {
        if (this.value.length > 0) {
            $(".show-search").removeClass("show-search");
            var results = $(container).children(containerSelector).find(':contains("' + $(textFieldSelector).val() + '")').filter(function () {
                return $(this).attr('data-id');
            });
            results.parent().addClass("show-search");
        } else {
            $(containerSelector).addClass("show-search");
        }
    });
}
