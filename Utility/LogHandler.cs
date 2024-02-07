namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Class for handling log massages.
    /// </summary>
    /// <typeparam name="T"> To take the type. </typeparam>
    public class LogHandler<T>
    {
        #region Private Data Members

        /// <summary>
        /// To store the ILogger with T type.
        /// </summary>
        private ILogger<T> m_objLogger;

        #endregion

        #region Public Constructor

        /// <summary>
        /// To initialize the LogHandler.
        /// </summary>
        /// <param name="objLogger"> To take the log handler object. </param>
        public LogHandler(ILogger<T> objLogger)
        {
            m_objLogger = objLogger;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// To log with level info.
        /// </summary>
        /// <param name="strMsg"> To take the log masseage. </param>
        public void LogInfo(string strMsg)
        {
            m_objLogger.LogInformation(message: strMsg);
        }

        /// <summary>
        /// To log with level error.
        /// </summary>
        /// <param name="strMsg"> To take the log masseage. </param>
        public void LogError(int nErrorCode, string strMsg)
        {
            string strError = string.Format(Constants.MSG_LOG_MSG, nErrorCode, strMsg);
            m_objLogger.LogError(message: strError);
        }

        #endregion
    }
}