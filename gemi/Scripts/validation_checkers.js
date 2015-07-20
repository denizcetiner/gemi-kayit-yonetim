function ValidateEditShip(object)
{
    var result = true;
    $("photoerror").css("display", "none");
    var numberOfPhotos = $(".photo").length;
    var obj = $(object).parent().parent().parent().parent().parent();
    var numberOfFiles = obj.find("input:eq(2)").prop("files").length;
    if (obj.find("input:eq(0)").val() == "")
    {
        CannotBeEmpty(obj.find("input:eq(0)"));
        result = false;
    }
    else if (obj.find("input:eq(0)").val().length > 10)
    {
        TooLong(obj.find("input:eq(0)"), 10);
        result = false;
    }
    if (numberOfPhotos == 0 && numberOfFiles == 0)
    {
        $("#photoerror").css("display", "block");
        result = false;
    }
    return result;

}

function ValidateAddUser(object) {
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
        CannotBeEmptyPass(frm.find("input:eq(1)"));
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
        TooLong(frm.find("input:eq(2)"), 20)
    }
    return result;
}

function ValidateSearchString(object)
{
    var result;
    var frm = $(object).parent().parent();
    if (frm.find("input:eq(0)").val() == "")
    {
        CannotBeEmpty(frm.find("input:eq(0)"));
        result = false;
    }
    else if (frm.find("input:eq(0)").val() == "")
    {
        TooLong(frm.find("input:eq(0)"), 10);
        result = false;
    }
    return result;
}

function ValidateDates(object)
{
    var frm = $(object).parent();
    var result = true;
    if (frm.find("input:eq(0)").val() == "" || frm.find("input:eq(0)").val() == "Başlangıç tarihi" || frm.find("input:eq(0)").val() == "Boş bırakılamaz")
    {
        CannotBeEmptyDate(frm.find("input:eq(0)"));
        result = false;
    }
    if (frm.find("input:eq(1)").val() == "" || frm.find("input:eq(1)").val() == "Bitiş tarihi" || frm.find("input:eq(1)").val() == "Boş bırakılamaz")
    {
        CannotBeEmptyDate(frm.find("input:eq(1)"));
        result = false;
    }
    return result;
}

function CannotBeEmptyDate(object)
{
    $(object).val("Bir tarih seçmek için tıklayın");
}

function ValidateNewUser(object)
{
    var result = true;
    var frm = $(object).parent();
    if (frm.find("input:eq(0)").val() == "")
    {
        CannotBeEmpty(frm.find("input:eq(0)"));
        result = false;
    }
    else if (frm.find("input:eq(0)").val().length > 20)
    {
        TooLong(frm.find("input:eq(0)"), 20);
        result = false;
    }
    if (frm.find("input:eq(1)").val() == "")
    {
        CannotBeEmpty(frm.find("input:eq(1)"));
        result = false;
    }
    else if (frm.find("input:eq(1)").val().length > 20)
    {
        TooLongPass(frm.find("input:eq(1)"), 20);
        result = false;
    }
    if (frm.find("input:eq(2)").val() == "")
    {
        CannotBeEmpty(frm.find("input:eq(2)"));
        result = false;
    }
    else if (frm.find("input:eq(2)").val().length > 20)
    {
        TooLong(frm.find("input:eq(2)"), 20);
        result = false;
    }
    return result;
}

function ValidateLogin(object)
{
    result = true;
    var obj = $(object).parents("fieldset");
    var input0 = obj.find("input:eq(0)");
    var input1 = obj.find("input:eq(1)");
    if (input0.val() == "")
    {
        input0.val("Boş bırakılamaz");
        input0.css("border", "red solid 1px");
        input0.css("color", "red");
        input0.attr("onclick", "ClickToEmpty(this)");
        result = false;
    }
    if (input1.val() == "")
    {
        input1.prop("type", "text");
        input1.val("Boş bırakılamaz");
        input1.css("border", "red solid 1px");
        input1.css("color", "red");
        input1.attr("onclick", "ClickToEmptyPass(this)");
        result = false;
    }
    return result;
}


function ValidateSearch(object)
{
    var result = true;
    var obj = $(object).parents("form");
    if (obj.find("input:eq(0)").val() == "")
    {
        obj.find("input:eq(0)").css("border", "solid red 1px");
        obj.find("input:eq(0)").val("Boş bırakılamaz");
        obj.find("input:eq(0)").css("color", "red");
        obj.find("input:eq(0)").attr("onclick", "ClickToEmpty(this)");
        result = false;
    }
    else if (!$.isNumeric(obj.find("input:eq(0)").val()))
    {
        obj.find("input:eq(0)").css("border", "solid red 1px");
        obj.find("input:eq(0)").val("Sadece sayı girin");
        obj.find("input:eq(0)").css("color", "red");
        obj.find("input:eq(0)").attr("onclick", "ClickToEmpty(this)");
        result = false;
    }
    else if (obj.find("input:eq(0)").val().length > 10)
    {
        TooLong(obj.find("input:eq(0)"), 10);
        result = false;
    }
    return result;
}