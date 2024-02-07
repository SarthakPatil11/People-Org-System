using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSheetManager.Models.DBEntities
{
    /// <summary>
    /// Used to handle user table data.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Used to store user ID.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        /// <summary>
        /// Used to store User name.
        /// </summary>
        [Required]
        [Column(TypeName = Utility.Constants.VARCHAR_20)]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Used to store group name.
        /// </summary>
        [Required]
        [Column(TypeName = Utility.Constants.VARCHAR_20)]
        public string GroupName { get; set; } = string.Empty;

        /// <summary>
        /// Used to store password
        /// </summary>
        [Required]
        [Column(TypeName = Utility.Constants.VARCHAR_50)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Useed to store the email.
        /// </summary>
        [Required]
        [Column(TypeName = Utility.Constants.VARCHAR_50)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Useed to store the delete flag.
        /// </summary>
        [Required]
        [DefaultValue(false)]
        public bool DeleteFlag { get; set; }

        /// <summary>
        /// Used to store the user details.
        /// </summary>
        public virtual UserDetails? UserDetails { get; set; }
    }
}