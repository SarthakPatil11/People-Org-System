//Function to validate the password.
function js_IsValidPassword(elementId, spanId) {
    let password = document.getElementById(elementId).value;
    let errorSpan = document.getElementById(spanId);
    let elementStyle = document.getElementById(elementId).style;
    // /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/

    if (password.match(/^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,16}$/)) //To check password is valid.
    {
        elementStyle.border = "none";
        errorSpan.innerText = "";
        errorSpan.style.display = "none";
        return true;
    }
    else //If it is not valid.
    {
        elementStyle.border = "1px solid red";
        errorSpan.innerText = "Password should have upper and lower case letters followed with digits and special charecter also length should be (8 - 16) digits."
        errorSpan.style.display = "block";
        return false;
    }
}

//Function to validate the password.
function js_ValidatePassword(newPasswordElementId, confirmPasswordElementId, spanId) {
    let newPassword = document.getElementById(newPasswordElementId).value;
    let confirmPassword = document.getElementById(confirmPasswordElementId).value;
    let elementStyle = document.getElementById(confirmPasswordElementId).style;
    let errorSpan = document.getElementById(spanId);

    let isValid = js_IsValidPassword(confirmPasswordElementId, spanId);

    if (isValid && newPassword === confirmPassword) //To check teh passwords matches.
    {
        elementStyle.border = "none";
        errorSpan.innerText = "";
        errorSpan.style.display = "none";
        return true;
    }
    else //If password not match.
    {
        elementStyle.border = "1px solid red";
        errorSpan.innerText = "Password should match with new password.";
        errorSpan.style.display = "block";
        return false;
    }
}

//Function to validate the email.
function js_IsValidEmail(elementId, spanId) {
    let email = document.getElementById(elementId).value;
    let errorSpan = document.getElementById(spanId);
    let elementStyle = document.getElementById(elementId).style;

    if (email.match(/^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/)) //To check the email is valid.
    {
        elementStyle.border = "none";
        errorSpan.innerText = "";
        errorSpan.style.display = "none";
        return true;
    }
    else //If email is not valid.
    {
        elementStyle.border = "1px solid red";
        errorSpan.innerText = "Enter poper email ID.";
        errorSpan.style.display = "block";
        return false;
    }
}

//Function to validate the username.
function js_IsValidUserName(elementId, spanId) {
    let userName = document.getElementById(elementId).value;
    let errorSpan = document.getElementById(spanId);
    let elementStyle = document.getElementById(elementId).style;

    if (userName.match(/^(?=.+[a-zA-Z])[a-zA-Z0-9]{8,20}$/)) //To check the username is valid.
    {
        elementStyle.border = "none";
        errorSpan.innerText = "";
        errorSpan.style.display = "none";
        return true;
    }
    else //If username is not valid.
    {
        elementStyle.border = "1px solid red";
        errorSpan.innerText = "Username should have upper and lower case letters followed with digits also length should be (8 - 20) digits.";
        errorSpan.style.display = "block";
        return false;
    }
}

//Function to validate the file.
function js_IsValidFile(file, imageBox, spanId) {
    let errorSpan = document.getElementById(spanId);

    let filename = file.value;
    let idxDot = filename.lastIndexOf(".") + 1;
    let extFile = filename.substr(idxDot, filename.length).toLowerCase();

    if (extFile != "jpg" && extFile != "jpeg" && extFile != "png") //To check the file is image file.
    {
        errorSpan.innerText = "Only jpg/jpeg and png files are allowed.";
        errorSpan.style.display = "block";
        file.value = "";
        imageBox.src = "./img/ProfileImage.png";
        return false;
    }
    else //If file is not image file.
    {
        errorSpan.innerText = "";
        errorSpan.style.display = "none";
        return true;
    }
}