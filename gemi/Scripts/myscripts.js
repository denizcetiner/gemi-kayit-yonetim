$(function () {
    $("#datepicker").datepicker({ dateFormat: 'dd/mm/yy' }, $.datepicker.regional['tr'])
    $(".datepick").each(function () {
        $(this).datepicker();
    });
});




function NewUser(object) {
    $("#newuser").html("<div id='newuser'>" +
            "<p>Yeni kullanıcı ekle:</p>" +
            "<form method='post' action='/Admin/AddUser'>" +
            "<input type='text' value='Kullanıcı adı:' name='username' onclick='ClickToEmpty(this)'/></br>" +
            "<input value='Şifresi:' type='text' name='password' onclick='ClickToEmptyPass(this)'/></br>" +
            "<input type='text' value='Rolü:' name='role' onclick='ClickToEmpty(this)'/></br>" +
            "<input type='submit' onclick='return ValidateNewUser(this)'/></br>" +
            "</form>" +
            "</div>");
}



function DeletePicture(e) {

    var answer = confirm("Bu fotoğrafı silmek istediğinize emin misiniz?");
    if (answer) {
        $.ajax({
            url: "/Gemi/DeletePicture",
            type: "POST",
            cache: false,
            async: false,
            data:
                {
                    photo_id: $(e).data("photoid"),
                    ref_id: $(e).data("refid")
                },
            success: function (result) {
                if (result == true) {
                    $(e).parent().parent().remove()
                }
            }
        })
    }
    return false;
}

