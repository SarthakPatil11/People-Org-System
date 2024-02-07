using System.ComponentModel.DataAnnotations;

namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Class is used to validate the date.
    /// </summary>
    public class CustomDateRangeAttribute : ValidationAttribute
    {
        #region Private Data Members

        /// <summary>
        /// Used to store the minimum date.
        /// </summary>
        private readonly DateTime m_minDate;

        /// <summary>
        /// Used to store the maximum date.
        /// </summary>
        private readonly DateTime m_maxDate;

        #endregion

        #region Public Constructor

        /// <summary>
        /// To initialize the attribute.
        /// </summary>
        public CustomDateRangeAttribute()
        {
            m_minDate = DateTime.Now.AddYears(-100);
            m_maxDate = DateTime.Now;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// To check the date range.
        /// </summary>
        /// <param name="Value"> To take the date value. </param>
        /// <param name="ValidationContext"> To take teh validetion context. </param>
        /// <returns> Validation success or error. </returns>
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        protected override ValidationResult IsValid(object Value, ValidationContext ValidationContext)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            string strMinDate = m_minDate.ToShortDateString();
            string strMaxDate = m_maxDate.ToShortDateString();
#pragma warning disable CS8604 // Possible null reference argument.
            string strErrorMsg = string.Format(ErrorMessage, strMinDate, strMaxDate);
#pragma warning restore CS8604 // Possible null reference argument.

            if (Value != null && Value is DateTime dateValue) //To check the date is not null.
            {
                if (dateValue >= m_minDate && dateValue <= m_maxDate) //To check the date is in range.
                {
#pragma warning disable CS8603 // Possible null reference return.
                    return ValidationResult.Success;
#pragma warning restore CS8603 // Possible null reference return.
                }
                else
                {
                    return new ValidationResult(strErrorMsg);
                }
            }

            return new ValidationResult(strErrorMsg);
        }

        #endregion
    }
}
