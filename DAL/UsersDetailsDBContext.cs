using Microsoft.EntityFrameworkCore;
using SkillSheetManager.Models.DBEntities;

namespace SkillSheetManager.DAL
{
    /// <summary>
    /// Class for database context.
    /// </summary>
    public class UsersDetailsDBContext : DbContext
    {
        #region Public Constructor

        /// <summary>
        /// To initialize the database context.
        /// </summary>
        /// <param name="options"></param>
        public UsersDetailsDBContext(DbContextOptions options) : base(options) { }


        #endregion

        #region Public Properties

        /// <summary>
        /// To store the User dataset.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// TO store the UserDetails dataset.
        /// </summary>
        public DbSet<UserDetails> UsersDetails { get; set; }

        #endregion

        #region Protected Methods

        /// <summary>
        /// To set database constraint.
        /// </summary>
        /// <param name="builder"> To take the model builder. </param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }

        #endregion
    }
}