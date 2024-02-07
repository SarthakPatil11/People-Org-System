namespace SkillSheetManager.Models
{
    /// <summary>
    /// Class used for sending response date.
    /// </summary>
    [Serializable]
    public class ResponseData
    {
        /// <summary>
        /// To store the error code.
        /// </summary>
        public int ErrorCode { get; set; } = 0;

        /// <summary>
        /// To store the error message.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string ErrorMessage { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// To store the severity.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Severity { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}