$(document).ready(function () {
    $('#regform').submit(function (e) {
        e.preventDefault();
        var Name = $('#Name').val();
        var Email = $('#Email').val();
        


        $(".error").remove();
        var check = true;
        if (Name.length < 1) {
            $('#Name').after('<span class="error"> Это поле обязательное к заполнению</span>');
            check = false;
        }

        if (Email.length < 1) {
            $('#Email').after('<span class="error">Это поле обязательное к заполнению</span>')
            check = false;
        } else {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            var address = document.forms[form_id].elements[email].value;
            if (reg.test(address) == false) {
                alert('Введите корректный e-mail');
                return false;

            }
            

    });
});