$(function () {
    $("#datepicker").datepicker({ dateFormat: 'dd/mm/yy' }, $.datepicker.regional['tr'])
    $(".datepick").each(function () {
        $(this).datepicker();
    });
});

function TooLong(object, length) {
    $(object).val("En fazla " + length + " karakter girin");
    $(object).css("border", "solid red 1px");
    $(object).css("color", "red");
    $(object).attr("onclick", "ClickToEmpty(this)");
}
function TooLongPass(object, length) {
    $(object).val("");
    $(object).prop("type", "text")
    $(object).val("En fazla " + length + " karakter girin");
    $(object).css("border", "solid red 1px");
    $(object).css("color", "red");
    $(object).attr("onclick", "ClickToEmptyPass(this)");
}

function ValidateSearch(object) {
    var result = true;
    var obj = $(object).parents("form");
    if (obj.find("input:eq(0)").val() == "") {
        obj.find("input:eq(0)").css("border", "solid red 1px");
        obj.find("input:eq(0)").val("Boş bırakılamaz");
        obj.find("input:eq(0)").css("color", "red");
        obj.find("input:eq(0)").attr("onclick", "ClickToEmpty(this)");
        result = false;
    }
    else if (!$.isNumeric(obj.find("input:eq(0)").val())) {
        obj.find("input:eq(0)").css("border", "solid red 1px");
        obj.find("input:eq(0)").val("Sadece sayı girin");
        obj.find("input:eq(0)").css("color", "red");
        obj.find("input:eq(0)").attr("onclick", "ClickToEmpty(this)");
        result = false;
    }
    else if (obj.find("input:eq(0)").val().length > 10) {
        TooLong(obj.find("input:eq(0)"), 10);
        result = false;
    }
    return result;
}

function CannotBeEmpty(object) {
    $(object).css("border", "red solid 1px");
    $(object).css("color", "red");
    $(object).val("Boş bırakılamaz");
    $(object).attr("onclick", "ClickToEmpty(this)");
}
function CannotBeEmptyPass(object) {
    $(object).val("");
    $(object).prop("type", "text");
    $(object).css("border", "red solid 1px");
    $(object).css("color", "red");
    $(object).val("Boş bırakılamaz");
    $(object).attr("onclick", "ClickToEmptyPass(this)");
}
function ClickToEmpty(object) {
    $(object).val("");
    $(object).css("color", "");
    $(object).css("border", "");
    $(object).attr("onclick", "")
}

function ClickToEmptyPass(object) {
    $(object).val("");
    $(object).css("color", "");
    $(object).css("border", "");
    $(object).prop("type", "password");
    $(object).attr("onclick", "")
}

function ValidateLogin(object) {
    result = true;
    var obj = $(object).parents("fieldset");
    var input0 = obj.find("input:eq(0)");
    var input1 = obj.find("input:eq(1)");
    if (input0.val() == "") {
        input0.val("Boş bırakılamaz");
        input0.css("border", "red solid 1px");
        input0.css("color", "red");
        input0.attr("onclick", "ClickToEmpty(this)");
        result = false;
    }
    if (input1.val() == "") {
        input1.prop("type", "text");
        input1.val("Boş bırakılamaz");
        input1.css("border", "red solid 1px");
        input1.css("color", "red");
        input1.attr("onclick", "ClickToEmptyPass(this)");
        result = false;
    }
    return result;
}
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

function ValidateNewUser(object) {
    var result = true;
    var frm = $(object).parent();
    if (frm.find("input:eq(0)").val() == "") {
        CannotBeEmpty(frm.find("input:eq(0)"));
        result = false;
    }
    else if (frm.find("input:eq(0)").val().length > 20) {
        TooLong(frm.find("input:eq(0)"), 20);
        result = false;
    }
    if (frm.find("input:eq(1)").val() == "") {
        CannotBeEmpty(frm.find("input:eq(1)"));
        result = false;
    }
    else if (frm.find("input:eq(1)").val().length > 20) {
        TooLongPass(frm.find("input:eq(1)"), 20);
        result = false;
    }
    if (frm.find("input:eq(2)").val() == "") {
        CannotBeEmpty(frm.find("input:eq(2)"));
        result = false;
    }
    else if (frm.find("input:eq(2)").val().length > 20) {
        TooLong(frm.find("input:eq(2)"), 20);
        result = false;
    }
    return result;
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

function ValidateEditShip(object) {
    var result = true;
    $("photoerror").css("display", "none");
    var numberOfPhotos = $(".photo").length;
    var obj = $(object).parent().parent().parent().parent().parent();
    var numberOfFiles = obj.find("input:eq(2)").prop("files").length;
    if (obj.find("input:eq(0)").val() == "") {
        CannotBeEmpty(obj.find("input:eq(0)"));
        result = false;
    }
    else if (obj.find("input:eq(0)").val().length > 10) {
        TooLong(obj.find("input:eq(0)"), 10);
        result = false;
    }
    if (numberOfPhotos == 0 && numberOfFiles == 0) {
        $("#photoerror").css("display", "block");
        result = false;
    }
    return result;

}