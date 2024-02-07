namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Used to send the json object response.
    /// </summary>
    public class Responsehandler
    {
        /// <summary>
        /// TO create an send the response object for json.
        /// </summary>
        /// <param name="bIsSuccess"> To get the status of result. </param>
        /// <param name="nErrorCode"> To get error code. </param>
        /// <param name="strErrorMsg"> To get the error message. </param>
        /// <param name="strMsg"> To get the inform message. </param>
        /// <param name="objData"> To get the data. </param>
        /// <returns></returns>
        public static object GetObject(bool bIsSuccess, int? nErrorCode = null, string? strErrorMsg = null, string? strMsg = null, object? objData = null)
        {
            var data = new
            {
                bIsSuccess,
                nErrorCode,
                strErrorMsg,
                strMsg,
                objData
            };

            return data;
        }
    }
}