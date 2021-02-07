
/*************************Select options for question types 1 and 3.****************************/
var optionList = [];
$('div.answers').on('click', function () {
    var id = $(this).attr("id");
    $(this).toggleClass("answers").toggleClass("selectAnswer");
    if ($(this).hasClass("border-0")) {
        $(this).toggleClass("border-0");
    }

    var index = optionList.indexOf(+id);
    if (index !== -1) {
        optionList.splice(index, 1);
    } else {
        optionList.push(+id);
    }
});
/************************************************************************************************/







/***********************Draggable options for question types 2 and 4.***************************/
$(function () {
    $('#rhs').sortable({ connectWith: '#lhs' });
    $('#lhs').sortable({ connectWith: '#rhs' });
});
/************************************************************************************************/








/***************************************Send Optins (Ok button)**************************************/
$(".sendOptions").on("click", function () {
    var questionType = $("#questionType").val();
    var questionId = $("#questionId").val();
    var minSelect;
    var maxSelect;
    switch (+questionType) {
        case 1:
        case 3:
            minSelect = $("#type1and3MinSelect").val();
            maxSelect = $("#type1and3MaxSelect").val();
            if ((optionList.length < minSelect) || (optionList.length > maxSelect)) {
                notification("error", "Error!", `Min ${minSelect} - Max ${maxSelect} items must be selected!`);
                return;
            }

            break;
        case 2:
        case 4:
            $('ul#rhs li').each(function () {
                var id = $(this).attr("id");
                optionList.push(+id);
            });
            minSelect = $("#type2and4MinSelect").val();
            maxSelect = $("#type2and4MaxSelect").val();
            if ((optionList.length < minSelect) || (optionList.length > maxSelect)) {
                notification("error", "Error!", `Min ${minSelect} - Max ${maxSelect} items must be selected!`);
                optionList = [];
                return;
            }
            break;
        case 5:
            var value = +$("#sliderValue").val();
            var min = +$("#type5MinSelect").val();
            var max = +$("#type5MaxSelect").val();
            if ((value < min) || (value > max)) {
                notification("error", "Error!", `Your choice must be between  ${min} and ${max}!`);
                optionList = [];
                return;
            }
            optionList.push(value);
        default:
            break;
    }

    var options = { questionId: +questionId, optionIds: JSON.stringify(optionList) };
    $.ajax({
        url: "/Questions/SaveOptions",
        type: "POST",
        dataType: "json",
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(options)
    }).done(function () {
        notification("success", "Success!", "Your options send.", 1000);
        location.reload();
        optionList = [];

        getNewQuestion();

    }).fail(function (err) {
        var errorMessage = err.responseText ? err.responseText : err.status;
        notification("error", "Error! Something went wrong.", `${errorMessage}`);
        if (err.status === 401) {
            $(location).prop('href', "/Identity/Account/Login?returnUrl=" + window.location.pathname + "&&questionId=" + questionId);
        }
        optionList = [];
    });
});

function getNewQuestion() {
    setTimeout(function () {
        $.ajax({
            url: "/Questions",
            type: "GET"
        }).done(function (newQuestion) {
            $("body").html(newQuestion);
        }).fail(function (err) {
            var errorMessage = err.responseText ? err.responseText : err.status;
            notification("error", "Error! Something went wrong.", `${errorMessage}`);
        }).then(setButtonsPosition());
    }, 1000);
}

function setButtonsPosition() {
    setTimeout(function () {
        cardButtonsFunction();
        ResizeCardButtonsWidth();
    }, 1000);
}
/************************************************************************************************/






/*****************************When touch to options locks the scroll.****************************/
$(".options").on("touchstart", function () { e.preventDefault() }, false);
/************************************************************************************************/







/***************************Skip question show loader. Don't remove.*****************************/
$(".skipQuestion").on("click", function () {
    $.ajax({});
});
/************************************************************************************************/



/********************Slider option for Question type 5 ********************/
var $range = $("#sliderValue"),
    $input = $(".js-input"),
    instance,
    min = +$("#type5MinSelect").val(),
    max = +$("#type5MaxSelect").val(),
    step = +$("#type5Increment").val();

$range.ionRangeSlider({
    skin: "big",
    type: "single",
    min: min,
    max: max,
    from: 0,
    step: step,
    grid: true,
    grid_snap: true,
    //onStart: function (data) {
    //    $input.prop("value", data.from);
    //},
    onChange: function (data) {
        $input.prop("value", data.from);
    }
});

instance = $range.data("ionRangeSlider");

var sliderInput = document.getElementById("slider-input");

if (sliderInput) {
    sliderInput.addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();
            document.getElementById("enterInputChoiceBtn").click();
        }
    });
}


$("#enterInputChoiceBtn").on("click", function () {
    var val = $input.prop("value");
    if (val % step !== 0) {
        notification("error", "Error", `Your choice must be a multiple of ${step}`);
        return;
    }

    // validate
    if (val < min) {
        val = min;
    } else if (val > max) {
        val = max;
    }

    instance.update({
        from: val
    });

});
/************************************************************************************************/