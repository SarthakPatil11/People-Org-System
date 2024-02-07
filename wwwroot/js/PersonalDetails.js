//Function to get file and lode on image tag.
function js_GetFile(inputId, imageboxId, spanId)
{
    var uploaded_image = "";
    let input = document.getElementById(inputId);
    var imagebox = document.getElementById(imageboxId);

    let isValid = js_IsValidFile(input, imagebox, spanId);

    if (isValid) //To check the file is valid.
    {
        const reader = new FileReader();
        reader.onload = function (e) {
            uploaded_image = e.target.result;
            imagebox.src = uploaded_image;
        };
        reader.readAsDataURL(input.files[0]);
    }
}

//Function to validate the personal details form.
function js_ValidateFormPersonalDetails(event) {
    let validUserName = js_IsValidUserName('input_Name', 'span_Name');

    if (validUserName) //To check valid fields.
    {
        return validUserName;
    }
    else //If fields are not valid.
    {
        event.preventDefault();
        return validUserName;
    }
}