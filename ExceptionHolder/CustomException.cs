using SkillSheetManager.EnumHolder;

namespace SkillSheetManager.ExceptionHolder
{
    /// <summary>
    /// Class used for custom exception.
    /// </summary>
    internal class CustomException : Exception
    {
        #region Private Data Members

        /// <summary>
        /// To store the error code of exception.
        /// </summary>
        private ErrorCodes m_objErrorCode;

        #endregion

        #region Public Properties

        /// <summary>
        /// Property to get the info of error code of exception.
        /// </summary>
        public ErrorCodes ErrorCode
        {
            get { return m_objErrorCode; }
        }

        #endregion

        #region Public Constructors

        /// <summary>
        /// Constructor to initialize the exception.
        /// </summary>
        /// <param name="nErrorCode"> To take the error code. </param>
        /// <param name="strMassege"> To take the exception massage. </param>
        public CustomException(ErrorCodes nErrorCode, string strMassege) : base(strMassege)
        {
            m_objErrorCode = nErrorCode;
        }

        /// <summary>
        /// Constructor to initialize the exception.
        /// </summary>
        /// <param name="nErrorCode"> To take the error code. </param>
        /// <param name="strMassege"> To take the exception massage. </param>
        /// <param name="objException"> To take the exception object. </param>
        public CustomException(ErrorCodes nErrorCode, string strMassege, Exception objException) : base(strMassege, objException)
        {
            m_objErrorCode = nErrorCode;
        }

        #endregion
    }
}
