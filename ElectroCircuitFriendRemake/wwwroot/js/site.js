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

$(document).on("submit","#CreateComponent", function(e) {
    
})
