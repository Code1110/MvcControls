function initListView(lvName) {
    $("#" + lvName + " .listviewul li").click(function () {
        var current = $(this);

        if (current.data("checked")) {
            uncheckItem(current);
        } else {
            checkItem(current);
        }

    });

    var searchField = $("#" + lvName + "_SearchField");

    if (searchField !== null) {

        searchField.keyup(function () {
            var search = searchField.val().toLowerCase();

            $("#" + lvName + " li").each(function () {
                if (search === "") {
                    $(this).show();
                }
                else if ($(this).html().toLowerCase().indexOf(search) !== -1) {
                    $(this).show();
                }
                else {
                    $(this).hide();
                }
            });
        });
    }
}

function checkItem(item) {
    item.data("checked", "true");
    item.addClass("selectedlistviewitem");
    //item.next("input").val(item.data("id"));
    item.next("input").val("True");
}

function uncheckItem(item) {
    item.data("checked", "");
    item.removeClass("selectedlistviewitem");
    //item.next("input").val("");
    item.next("input").val("False");
}

function checkAllItems(lvName) {
    $("#" + lvName + " li").each(function() { checkItem($(this)) });
}

function uncheckAllItems(lvName) {
    $("#" + lvName + " li").each(function () { uncheckItem($(this)) });
}