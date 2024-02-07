using System.Text;

namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Used to encryp or decrypt password.
    /// </summary>
    public class PasswordManager
    {
        #region Public Methods

        /// <summary>
        /// Method to encrypt password.
        /// </summary>
        /// <param name="strPassword"> To take password. </param>
        /// <returns> Encripted password. </returns>
        public static string Encrypt(string strPassword)
        {

            if (string.IsNullOrEmpty(strPassword)) //To check passwor is null or empty.
            {
                return string.Empty;
            }
            else //If password is not empty or null.
            {
                byte[] btarrBtorePassword = Encoding.ASCII.GetBytes(strPassword);
                string strEncryptedPassword = Convert.ToBase64String(btarrBtorePassword);
                return strEncryptedPassword;
            }
        }

        /// <summary>
        /// Method to decrypt password.
        /// </summary>
        /// <param name="strPassword"> To take the password. </param>
        /// <returns> Decrypted mpassword. </returns>
        public static string Decrypt(string strPassword)
        {
            if (string.IsNullOrEmpty(strPassword)) //To check passwor is null or empty.
            {
                return string.Empty;
            }
            else //If password is not empty or null.
            {
                byte[] btarrEncryptedPassword = Convert.FromBase64String(strPassword);
                string strDecryptedPassword = Encoding.ASCII.GetString(btarrEncryptedPassword);
                return strDecryptedPassword;
            }
        }

        #endregion
    }
}