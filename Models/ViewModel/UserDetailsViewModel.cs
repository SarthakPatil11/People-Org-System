using SkillSheetManager.Utility;
using System.ComponentModel.DataAnnotations;

namespace SkillSheetManager.Models.ViewModel
{
    /// <summary>
    /// Used to store the user details for viewing the data.
    /// </summary>
    public class UserDetailsViewModel
    {
        /// <summary>
        /// Used to store the user id.
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Used to store the user name.
        /// </summary>
        [Required(ErrorMessage = Constants.MSG_NAME_REQUIRED)]
        [Display(Name = Constants.MSG_NAME)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the sex.
        /// </summary>
        [Required(ErrorMessage = Constants.MSG_SEX_REQUIRED)]
        public string Sex { get; set; } = string.Empty;

        /// <summary>
        /// Used to store date of birth.
        /// </summary>
        [Required(ErrorMessage = Constants.MSG_BIRTH_DATE_REQUIRED)]
        [Display(Name = Constants.MSG_DATE_OF_BIRTH)]
        [CustomDateRange(ErrorMessage = Constants.MSG_DATE_RANGE)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Used to store the joining date.
        /// </summary>
        [Required(ErrorMessage = Constants.MSG_JOIN_DATE_REQUIRED)]
        [Display(Name = Constants.MSG_JOIN_DATE)]
        [CustomDateRange(ErrorMessage = Constants.MSG_DATE_RANGE)]
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Used to store worked in japan.
        /// </summary>
        [Display(Name = Constants.MSG_WORKED_IN_JAP)]
        public string? WorkedInJapan { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the qualification.
        /// </summary>
        public string? Qualification { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the languages.
        /// </summary>
        public string? Languages { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the database.
        /// </summary>
        public string? Database { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the photo name.
        /// </summary>
        [Display(Name = Constants.MSG_UPLOAD_PHOTO)]
        public string? PhotoName { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the photo.
        /// </summary>
        public IFormFile? Photo { get; set; }
    }
}