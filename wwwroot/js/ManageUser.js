let _selectedUsersID = [];
let _btnAddUser = document.getElementById("button_AddUser")
let _btnEditUser = document.getElementById("button_EditUser")
let _btnDeleteUser = document.getElementById("button_DeleteUser")

function selection_changed(selectedItems) {
    var data = selectedItems.selectedRowsData;

    console.log(data);

    data.forEach((user) => {
        _selectedUsersID.push(user.UserID);
    });

    if (data.length == 1) {
        _btnDeleteUser.disabled = false
        _btnEditUser.disabled = false
    }
    else if (data.length > 1) {
        _btnDeleteUser.disabled = false
        _btnEditUser.disabled = true
    }
    else {
        _btnDeleteUser.disabled = true
        _btnEditUser.disabled = true
    }
}

//Funtion for cratin the user.
function js_CreateUser() {
    let validUserName = js_IsValidUserName('input_AddUserName', 'span_AddUserName');
    let validPassword = js_IsValidPassword('input_AddPassword', 'span_AddPassword');
    let validEmail = js_IsValidEmail('input_AddEmail', 'span_AddEmail');

    if (!validUserName || !validPassword || !validEmail) //To check valid fields.
    {
        return;
    }

    var formData = new Object();
    
    formData.userName = $('#input_AddUserName').val();
    formData.groupName = "User";
    formData.password = $('#input_AddPassword').val();
    formData.email = $('#input_AddEmail').val();

    console.log(formData);

    $.ajax({
        url: 'ManageUsers/CreateUser',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0 || !response.bIsSuccess) //To check the response is not correct.
            {
                alert(`Error ${response.nErrorCode}: Unable to create user, ${response.strErrorMsg}`);
            }
            else //If response is correct
            {
                location.reload();
                alert(response.strMsg);
            }
        },
        error: function (error) {
            alert("Unable to save data " + error);
        }
    })
}

//To get the user infor for update purpose.
function js_EditUser() {
    //js_SetSelectedUsers()

    let selectedUserID = _selectedUsersID[0];
    console.log(_selectedUsersID)

    let id = selectedUserID;

    $.ajax({
        url: 'ManageUsers/Edit?id=' + id,
        type: 'get',
        datatype: 'json',
        contentType: 'application/json;chaset=utf-8',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0 || !response.bIsSuccess) //To check the response is not correct.
            {
                alert(`Error ${response.nErrorCode}: Unable to get user, ${response.strErrorMsg}`);
            }
            else //If response is correct
            {
                console.log("Edit res:", response);
                $('#div_EditModal').modal('show');
                $('#input_EditUserName').val(response.objData.UserName);
                $('#input_EditPassword').val(response.objData.Password);
                $('#input_EditEmail').val(response.objData.Email);
                $('#button_ModalEditUser').click(() => { js_UpdateUser(response.objData) });
            }
        },
        error: function (error) {
            alert("Unable to save data " + error);  
        }
    });
}

//Function to update the user.
function js_UpdateUser(user) {
    let validUserName = js_IsValidUserName('input_EditUserName', 'span_EditUserName');
    let validPassword = js_IsValidPassword('input_EditPassword', 'span_EditPassword');
    let validEmail = js_IsValidEmail('input_EditEmail', 'span_EditEmail');

    if (!validUserName || !validPassword || !validEmail)  //To check valid fields.
    {
        return;
    }

    var formData = new Object();

    formData.userID = user.UserID;
    formData.groupName = user.GroupName;
    formData.userName = $('#input_EditUserName').val();
    formData.password = $('#input_EditPassword').val();
    formData.email = $('#input_EditEmail').val();

    console.log("Update formData:", formData);

    $.ajax({
        url: 'ManageUsers/Update',
        data: formData,
        type: 'post',
        success: function (response) {
            if (response == null || response == undefined || response.length == 0 || !response.bIsSuccess) //To check the response is not correct.
            {
                alert(`Error ${response.nErrorCode}: Unable to update user, ${response.strErrorMsg}`);
            }
            else  //If response is correct
            {
                location.reload();
                alert(response.strMsg);
            }
        },
        error: function (error) {
            alert("Unable to update user " + error);
        }
    });
}

//Function to delete the user.
function js_DeleteUser() {
    let delUserIDs = _selectedUsersID;

    $.ajax({
        url: 'ManageUsers/DeleteUser',
        type: 'post',
        data: { lstIDs: delUserIDs },
        success: function (response) {
            if (response == null || response == undefined || response.length == 0 || !response.bIsSuccess)  //To check the response is not correct.
            {
                alert(`Error ${response.nErrorCode}: Unable to delete user, ${response.strErrorMsg}`);
            }
            else //If response is correct
            {
                location.reload();
                alert(response.strMsg);
            }
        },
        error: function (error) {
            alert("Unable to save data " + error);
        }
    });
}

//Function for closing the spesific model.
function js_CloseModal(modalName, clear) {
    if (clear === true) {
        $(`#input_${modalName}UserName`).val('');
        $(`#input_${modalName}Password`).val('');
        $(`#input_${modalName}Email`).val('');
    }

    $(`#div_${modalName}Modal`).modal('hide');
    $('#button_EditUser').attr("disabled", true);
    $('#button_DeleteUser').attr("disabled", true);
}