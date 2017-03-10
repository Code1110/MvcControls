function initListView(lvName) {
    $("#" + lvName + ".dielis li").click(function () {
        var current = $(this);

        if (current.data("checked")) {
            uncheckItem(current);
        } else {
            checkItem(current);
        }

    });

    var searchField = $("#" + lvName + "_SearchField");
    //console.log(searchField);
    if (searchField !== null) {
        //console.log("searchfield found")
        searchField.keyup(function () {
            var search = searchField.val().toLowerCase();
            //console.log("#" + lvName + " li");
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
    } else {
        //console.log("searchfield not found")
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