// Write your Javascript code.
$(document).on('change', ':file', function(e) {
    var reg = /(.*)\.(jpg|gif|png|jpeg|pdf)$/i;
    var ext = $(this).val().match(reg);
    if (ext != null) {
        var label = $(this).val().replace(/\\/g, '/').replace(/.*\//, '');
        var textInputElement = $(this).parent().parent().find(":text")[0];
        console.log(textInputElement);
        $(textInputElement).val(label);
    }
});

$(document).on("click",
    ".subtractItem",
    function () {
        var itemId = $(this).parent().parent().prop("class").split('-')[1];
        var object = $(this).parent();
        console.log(object);
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: ComponentUrl,
            data: JSON.stringify({
                ItemId: itemId,
                ItemType: object.prop("class"),
                ChangeAction: "subtract"
            }),
            dataType: "json",
            success: function (data) {
                $(object).find(".value").text(data);
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            }

        });
    });
$(document).on("click",
    ".addItem",
    function () {
        var itemId = $(this).parent().parent().prop("class").split('-')[1];
        var object = $(this).parent();
        console.log($(object).find(".value"));
        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: ComponentUrl,
            data: JSON.stringify({
                ItemId: itemId,
                ItemType: object.prop("class"),
                ChangeAction: "add"
            }),
            dataType: "json",
            success: function (data) {
                $(object).find(".value").text(data);
            },
            error: function (xhr, err) {
                alert("readyState: " + xhr.readyState + "\nstatus: " + xhr.status);
                alert("responseText: " + xhr.responseText);
            }

        });
    });