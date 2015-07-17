function TooLong(object, length)
{
    $(object).val("En fazla " + length + " karakter girin");
    $(object).css("border", "solid red 1px");
    $(object).css("color", "red");
    $(object).attr("onclick", "ClickToEmpty(this)");
}
function TooLongPass(object, length)
{
    $(object).val("");
    $(object).prop("type", "text")
    $(object).val("En fazla " + length + " karakter girin");
    $(object).css("border", "solid red 1px");
    $(object).css("color", "red");
    $(object).attr("onclick", "ClickToEmptyPass(this)");
}


function CannotBeEmpty(object)
{
    $(object).css("border", "red solid 1px");
    $(object).css("color", "red");
    $(object).val("Boş bırakılamaz");
    $(object).attr("onclick", "ClickToEmpty(this)");
}
function CannotBeEmptyPass(object)
{
    $(object).val("");
    $(object).prop("type", "text");
    $(object).css("border", "red solid 1px");
    $(object).css("color", "red");
    $(object).val("Boş bırakılamaz");
    $(object).attr("onclick", "ClickToEmptyPass(this)");
}
function ClickToEmpty(object)
{
    $(object).val("");
    $(object).css("color", "");
    $(object).css("border", "");
    $(object).attr("onclick", "")
}

function ClickToEmptyPass(object)
{
    $(object).val("");
    $(object).css("color", "");
    $(object).css("border", "");
    $(object).prop("type", "password");
    $(object).attr("onclick", "")
}