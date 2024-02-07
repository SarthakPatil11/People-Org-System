namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Used to manage constants.
    /// </summary>
    public class Constants
    {
        #region String Constants

        /// <summary>
        /// Used for user created message.
        /// </summary>
        public const string MSG_USER_CREATED = "User Created.";

        /// <summary>
        /// Used for user updated message.
        /// </summary>
        public const string MSG_USER_UPDATED = "User Updated.";

        /// <summary>
        /// Used for user deleted message.
        /// </summary>
        public const string MSG_USER_DELETED = "User(s) Deleted.";

        /// <summary>
        /// Used for model validetion message.
        /// </summary>
        public const string MSG_MODEL_VALIDETION_FAILD = "Model vaidation failed!!!";

        /// <summary>
        /// Used for user not found message.
        /// </summary>
        public const string MSG_USER_NOT_FOUND = "User not found.";
        
        /// <summary>
        /// Used for user not found message.
        /// </summary>
        public const string RESPONSE_DATA_VARIABLE_NAME = "responseData";
        
        /// <summary>
        /// Used for user already exist.
        /// </summary>
        public const string MSG_USER_NAME_EXIST = "Username already Exist. ";
        
        /// <summary>
        /// Used for password has problem message.
        /// </summary>
        public const string MSG_PASSWORD_PROBLEM = "Password field has problem. ";
        
        /// <summary>
        /// Used for email has problem message.
        /// </summary>
        public const string MSG_EMAIL_PROBLEM = "Email field has problem. ";
        
        /// <summary>
        /// Used for date of birth field has problem message.
        /// </summary>
        public const string MSG_DATE_OF_BIRTH_PROBLEM = "Date of birth field has problem. ";
        
        /// <summary>
        /// Used for joinig date field has problem message.
        /// </summary>
        public const string MSG_JOINING_DATE_PROBLEM = "Joining date field has problem. ";
        
        /// <summary>
        /// Used for worked in japan field has problem message.
        /// </summary>
        public const string MSG_WORKED_IN_JAPAN_PROBLEM = "Worked in japan field has problem. ";
        
        /// <summary>
        /// Used for sex field has problem message.
        /// </summary>
        public const string MSG_SEX_PROBLEM = "Sex field has problem. ";

        /// <summary>
        /// Used for Qualification field has problem message.
        /// </summary>
        public const string MSG_QUALIFICATION_PROBLEM = "Qualification field has problem. ";

        /// <summary>
        /// Used for Languages field has problem message.
        /// </summary>
        public const string MSG_LANGUAGES_PROBLEM = "Languages field has problem. ";

        /// <summary>
        /// Used for Database field has problem message.
        /// </summary>
        public const string MSG_DATABASE_PROBLEM = "Database field has problem. ";

        /// <summary>
        /// Used for PhotoName field has problem message.
        /// </summary>
        public const string MSG_PHOTONAME_PROBLEM = "Photo name field has problem. ";

        /// <summary>
        /// Used for select proper user message.
        /// </summary>
        public const string MSG_SELECT_PROPER_USER = "Select proper username.";

        /// <summary>
        /// Used for password not correct message.
        /// </summary>
        public const string MSG_PASSWORD_NOT_CORRECT = "Enter correct password.";

        /// <summary>
        /// Used for password changed message.
        /// </summary>
        public const string MSG_PASSWORD_CHANGED = "Password has been changed.";

        /// <summary>
        /// Used for password empty message.
        /// </summary>
        public const string MSG_PASSWORD_EMPTY = "Password can not be empty.";

        /// <summary>
        /// Used for name field required message.
        /// </summary>
        public const string MSG_NAME_REQUIRED = "Name field is required.";

        /// <summary>
        /// Used for sex field required message.
        /// </summary>
        public const string MSG_SEX_REQUIRED = "Sex field is required.";

        /// <summary>
        /// Used for birth date field required message.
        /// </summary>
        public const string MSG_BIRTH_DATE_REQUIRED = "Date of birth field is required.";

        /// <summary>
        /// Used for date field range message.
        /// </summary>
        public const string MSG_DATE_RANGE = "Date should between ({0}) to ({1}).";

        /// <summary>
        /// Used for join date is required message.
        /// </summary>
        public const string MSG_JOIN_DATE_REQUIRED = "Joining date field is required.";

        /// <summary>
        /// Used for name field lable.
        /// </summary>
        public const string MSG_NAME = "Name";

        /// <summary>
        /// Used for date of birth field lable.
        /// </summary>
        public const string MSG_DATE_OF_BIRTH = "Date Of Birth";

        /// <summary>
        /// Used for joining date field lable.
        /// </summary>
        public const string MSG_JOIN_DATE = "Joining Date";

        /// <summary>
        /// Used for worked in japan field lable.
        /// </summary>
        public const string MSG_WORKED_IN_JAP = "Worked In Japan";

        /// <summary>
        /// Used for upload photo field lable.
        /// </summary>
        public const string MSG_UPLOAD_PHOTO = "Upload Photo";

        /// <summary>
        /// Used for root folder path.
        /// </summary>
        public const string ROOT_FOLDER_PATH = "./";

        /// <summary>
        /// Used for image folder path.
        /// </summary>
        public const string IMG_FOLDER_PATH = "img/UserProfilePhotos/";

        /// <summary>
        /// Used for alternate photo path.
        /// </summary>
        public const string ALTERNET_IMG_PATH = "./img/ProfileImage.png";

        /// <summary>
        /// Used for table user.
        /// </summary>
        public const string TABLE_USER = "User";

        /// <summary>
        /// Used for role user.
        /// </summary>
        public const string ROLE_USER = "User";

        /// <summary>
        /// Used for role admin.
        /// </summary>
        public const string ROLE_ADMIN = "Admin";

        /// <summary>
        /// Used for digit true value.
        /// </summary>
        public const string STR_ONE = "1";

        /// <summary>
        /// Used for digit false value.
        /// </summary>
        public const string STR_ZERO = "0";

        /// <summary>
        /// Used for new password input element ID.
        /// </summary>
        public const string NEW_PASSWORD_ID = "input_NewPassword";

        /// <summary>
        /// Used for varchar type size 255.
        /// </summary>
        public const string VARCHAR_255 = "varchar(255)";

        /// <summary>
        /// Used for varchar type size 20.
        /// </summary>
        public const string VARCHAR_20 = "varchar(20)";

        /// <summary>
        /// Used for varchar type size 50.
        /// </summary>
        public const string VARCHAR_50 = "varchar(50)";

        /// <summary>
        /// Used for show occured in.
        /// </summary>
        public const string MSG_OCCUR_IN = " This exception occured in ";

        /// <summary>
        /// Used for show occured in.
        /// </summary>
        public const string MSG_METHOD = " Method.";

        /// <summary>
        /// Used for show empty field message.
        /// </summary>
        public const string MSG_FIELDS_EMPTY = "Fields are empty.";

        /// <summary>
        /// Used for severity danger.
        /// </summary>
        public const string SEVERITY_DANGER = "danger";

        /// <summary>
        /// Used for severity success.
        /// </summary>
        public const string SEVERITY_SUCCESS = "success";

        /// <summary>
        /// Used for user fetched.
        /// </summary>
        public const string USERS_FETHED = "Users are fetched.";

        /// <summary>
        /// Used for user invalid model state.
        /// </summary>
        public const string MSG_INVALID_MODEL = "Invalid model state.";

        /// <summary>
        /// Used for user login successful.
        /// </summary>
        public const string MSG_LOGIN_SUCCESSFUL = "Login successful to {0} with role {1}.";

        /// <summary>
        /// Used for user login successful.
        /// </summary>
        public const string MSG_LOGOUT_SUCCESSFUL = "Log out successful.";

        /// <summary>
        /// Used for stop host exception.
        /// </summary>
        public const string STOP_HOST_EXCEPTION = "StopTheHostException";

        /// <summary>
        /// Used for show stop host exception massege.
        /// </summary>
        public const string MSG_STOP_HOST_EXCEPTION = "Stopped program because of exception";

        /// <summary>
        /// Used for show log massege.
        /// </summary>
        public const string MSG_LOG_MSG = "Error {0}:{1}";
        
        /// <summary>
        /// Used for filter which is serialized.
        /// </summary>
        public const string FILTER_SERIALIZED = "[\"{0}\", \"{1}\", \"{2}\"]";
        
        /// <summary>
        /// Used for equal to opretor.
        /// </summary>
        public const string EQUAL_TO_OPRETOR = "=";

        #endregion

        #region Integer Constants

        /// <summary>
        /// Used for declare second frame.
        /// </summary>
        public const int SECOND_FRAME = 1;
        
        /// <summary>
        /// Used for count zero.
        /// </summary>
        public const int COUNT_ZERO = 0;

        #endregion
    }
}