using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkillSheetManager.Models.DBEntities
{
    /// <summary>
    /// Used to handle UserDetails table.
    /// </summary>
    public class UserDetails
    {
        /// <summary>
        /// Used to store teh user ID.
        /// </summary>
        [Key]
        [ForeignKey(Utility.Constants.TABLE_USER)]
        public int UserID { get; set; }

        /// <summary>
        /// Used to store the sex.
        /// </summary>
        public bool? Sex { get; set; }

        /// <summary>
        /// Used to store the date of birth.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = Utility.Constants.MSG_DATE_OF_BIRTH)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Used to store the joining date.
        /// </summary>
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = Utility.Constants.MSG_JOIN_DATE)]
        public DateTime JoiningDate { get; set; }

        /// <summary>
        /// Used to store the worked in japan field.
        /// </summary>
        [Display(Name = Utility.Constants.MSG_WORKED_IN_JAP)]
        public bool? WorkedInJapan { get; set; }

        /// <summary>
        /// Used to store the qualification.
        /// </summary>
        [Column(TypeName = Utility.Constants.VARCHAR_255)]
        public string Qualification { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the languages.
        /// </summary>
        [Column(TypeName = Utility.Constants.VARCHAR_255)]
        public string Languages { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the database.
        /// </summary>
        [Column(TypeName = Utility.Constants.VARCHAR_255)]
        public string Database { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the photo name.
        /// </summary>
        [Display(Name = Utility.Constants.MSG_UPLOAD_PHOTO)]
        public string PhotoName { get; set; } = string.Empty;

        /// <summary>
        /// Used to store the user.
        /// </summary>
        [Required]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual User User { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}