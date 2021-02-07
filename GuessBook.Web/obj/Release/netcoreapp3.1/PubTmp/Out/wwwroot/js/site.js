

$(window).on("load", function () {
    // Profile page categories hamburger menu button height settings
    setCategoriesBtnPosition();

    //if these element not exits on document return.
    var stickyCardButtons = $("#stickyCardButtons").length;
    var cardButtons = $("#cardButtons").length;
    if (stickyCardButtons === 0 || cardButtons === 0) { return; }

    cardButtonsFunction();
    ResizeCardButtonsWidth();
});

//when page refresh scrool top
window.onbeforeunload = function () { window.scrollTo(0, 0); }

$(window).on("resize", function () {

    setCategoriesBtnPosition();
    var stickyCardButtons = $("#stickyCardButtons").length;
    var cardButtons = $("#cardButtons").length;
    if (stickyCardButtons === 0 || cardButtons === 0) { return; }

    cardButtonsFunction();
    ResizeCardButtonsWidth();

});

$(".options").on("touchend mouseenter", function () {
    var stickyCardButtons = $("#stickyCardButtons").length;
    var cardButtons = $("#cardButtons").length;
    if (stickyCardButtons === 0 || cardButtons === 0) { return; }

    cardButtonsFunction();
    ResizeCardButtonsWidth();
});

function cardButtonsFunction() {
    //when stickyCardButtons.height > cardButtons.height it will be hidden
    var stickyCardButtonsPosYTop = $("#stickyCardButtons").offset().top;  //get the position from top
    var cardButtonsPosYTop = $("#cardButtons").offset().top;  //get the position from top
    if (stickyCardButtonsPosYTop <= cardButtonsPosYTop) {
        $("#stickyCardButtons").css("visibility", "visible");
        $("#cardButtons").css("visibility", "hidden");

    } else {
        $("#stickyCardButtons").css("visibility", "hidden");
        $("#cardButtons").css("visibility", "visible");
    }

}

function ResizeCardButtonsWidth() {
    var cardButtonsWidth = $("#cardButtons").width();
    $("#stickyCardButtons").css("width", cardButtonsWidth);
}

function setCategoriesBtnPosition() {
    // Profile page categories hamburger menu button height settings
    var headerHeight = $('#header').height();
    $(".profileCategoriesBtn").css("top", headerHeight + 4 + "px");
    $(".profileCategoriesBtn").css("visibility", "visible");
}


/****************************Profile page toggle menu*******************************/
function openNav() {
    //$(".sideBar").removeClass("d-none");
    $(".profileCategoriesBtn").fadeOut(0);
    $("#profileCategoriesMenu").width(250);

    //If wanted bacground opacity open this code
    //$("body").css("backgroundColor", "rgba(0,0,0,0.4)");
}

function closeNav() {
    $("#profileCategoriesMenu").width(0);
    $(".profileCategoriesBtn").fadeIn("slow");

    //If wanted bacground opacity open this code
    //$("body").css("backgroundColor", "white");
}

/************************************************************************************/




/******************************Back to top button************************************/
$(window).on("scroll", function () {

    if ($(this).scrollTop() > 100) {
        $('.back-to-top').fadeIn('slow');
    } else {
        $('.back-to-top').fadeOut('slow');
    }

    var stickyCardButtons = $("#stickyCardButtons").length;
    var cardButtons = $("#cardButtons").length;
    if (stickyCardButtons === 0 || cardButtons === 0) { return; }

    cardButtonsFunction();
    ResizeCardButtonsWidth();

});
$('.back-to-top').click(function () {
    $('html, body').animate({ scrollTop: 0 }, 800);
    return false;
});

/************************************************************************************/


/**************************Profile page Question Type 5 *****************************/
var $range = $("#profilePageSlider"),
    instance,
    min = +$("#type5MinSelect").val(),
    max = +$("#type5MaxSelect").val(),
    step = +$("#type5Increment").val(),
    from = +$("#profilePageSlider").val();

$range.ionRangeSlider({
    skin: "big",
    type: "single",
    min: min,
    max: max,
    from: from,
    step: step,
    grid: true,
    grid_snap: true,
    disable: true
});
/************************************************************************************/


/***********************When scrool page header sticky top***************************/

// When the user scrolls the page, execute myFunction
window.onscroll = function () { stickyHeader() };

// Get the header
var header = document.getElementById("header");

// Get the offset position of the navbar
var sticky = header.offsetTop;

// Add the sticky class to the header when you reach its scroll position. Remove "sticky" when you leave the scroll position
function stickyHeader() {
    if (window.pageYOffset > sticky) {
        header.classList.add("sticky");
    } else {
        header.classList.remove("sticky");
    }
}

/************************************************************************************/



/***************When click header set position of profileCategoriesBtn*****************/
$("#header, #userDropdown").on("click", function () {
    $(".profileCategoriesBtn").fadeOut(0);
    setTimeout(function () {
        setCategoriesBtnPosition();
        $(".profileCategoriesBtn").fadeIn(0);
    }, 400);
});
/************************************************************************************/