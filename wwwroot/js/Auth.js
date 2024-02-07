//Function for geting the users.
function js_GetUsers() {
    let groupName = $('#select_GroupName').val();

    $.ajax({
        url: 'GetUsers?groupName=' + groupName,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;chaset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0 || !response.bIsSuccess) //To check the response is not correct.
            {
                alert(`Error ${response.nErrorCode}: ${response.strErrorMsg}`);
            }
            else //If response is correct.
            {
                console.log("GetUsers res:", response);

                let object = "<option selected hidden>--Select UserNames--</option>";

                let data = response.objData;
                console.log(data);
                data.forEach((item) => {
                    object += `<option value="${item.UserName}">${item.UserName}</option>`;
                });

                $('#select_UserName').html(object);
                $('#select_UserName').removeAttr('disabled');
                $('#input_Password').removeAttr('disabled');
            }
        },
        error: function (error) {
            alert("Select proper username " + error);
        }
    });
}

//Function to check the old password.
function js_CheckOldPassword(userName) {
    $.ajax({
        url: 'GetUser?userName=' + userName,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;chaset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0 || !response.bIsSuccess) //To check the response is not correct.
            {
                alert(`Error ${response.nErrorCode}: ${response.strErrorMsg}`);
            }
            else //If response is correct.
            {
                let oldPassword = $('#input_OldPassword').val();

                if (oldPassword == response.objData.Password) //To check the passwords are matching.
                {
                    alert("Password matched no need to change.");
                }
            }
        },
        error: function (error) {
            alert("Unable to get username" + error);
        }
    });
}

//Function to perform form validations on login form.
function js_ValidateFormLogin(event) {
    let validPassword = js_IsValidPassword('input_Password', 'span_Password')
    if (validPassword) //To check valid field.
    {
        return validPassword;
    }
    else //If fields are not valid.
    {
        event.preventDefault();
        return validPassword;
    }
}

//Function to perform form validations on change password form.
function js_ValidateFormChangePass(event) {
    let validNewPassword = js_IsValidPassword('input_NewPassword', 'span_NewPassword');
    let validReenteredPassword = js_ValidatePassword('input_NewPassword', 'input_ReenterPassword', 'span_ReenterPassword');
    if (validNewPassword && validReenteredPassword) //To check valid fields.
    {
        return true;
    }
    else //If fields are not valid.
    {
        event.preventDefault();
        return false;
    }
}