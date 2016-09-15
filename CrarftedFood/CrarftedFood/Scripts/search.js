

function search(containerId, text, ignoreExcludes = true, containersAttribute = 'data-id'){

  $(".show-search").removeClass("show-search");
  $(".targetfound").removeClass("targetfound");
  originalHtml = document.getElementById(containerId).innerHTML;
  newHtml = originalHtml.replace(new RegExp(text + "(?![^<>]*>)", "ig"), function(e){
      return "<span class='targetfound'></span>" + e;
   });
   document.getElementById(containerId).innerHTML = newHtml;


   var targets = document.getElementsByClassName("targetfound");
   for(var i=0; i<targets.length; i++){
     target = targets[i];

      if(ignoreExcludes && target.parentElement.classList.contains('search-exclude')){
        continue;
      }

     while(!target.parentElement.hasAttribute(containersAttribute)) {
       target = target.parentElement;
     }
     target.parentElement.classList.add('show-search');

   }
   $(".targetfound").remove();
}

//unutar containera trazi text koji je u textFieldu i prikazuje elemente u okviru containerSelector (koji jos ima i atribut containersAttribute) gde je nesto nadjeno
//zaobilaze se sadrzaji tagova sa klasom search-exclude ukoliko je ignoreExcludes ture
//potreban css: (ContainerSelector).emp-container:not( .show-search ) {   display: none;}
function searchPage(textFieldSelector, container, containerSelector, ignoreExcludes = true, containersAttribute = 'data-id') {
    $(textFieldSelector).on('change keydown paste input', function(){
      if(this.value.length > 0) {
        search(container, this.value, ignoreExcludes, containersAttribute);
      }else {
        $(containerSelector).addClass("show-serch");
      }
    });
}


$(document).ready(function($) {
  searchPage("#searchMembersText","members", ".emp-container");

});
