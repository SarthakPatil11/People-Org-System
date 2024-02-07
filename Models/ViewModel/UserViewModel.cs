using System.ComponentModel.DataAnnotations;

namespace SkillSheetManager.Models.ViewModel
{
    /// <summary>
    /// Class is used to store the user view modal
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// To store the username of user. 
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// To store the group name of the user.
        /// </summary>
        [Required]
        public string GroupName { get; set; } = string.Empty;

        /// <summary>
        /// TO store the password of the user.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}