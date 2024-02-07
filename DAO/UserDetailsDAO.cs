using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SkillSheetManager.DAL;
using SkillSheetManager.EnumHolder;
using SkillSheetManager.ExceptionHolder;
using SkillSheetManager.Models.DBEntities;
using SkillSheetManager.Utility;

namespace SkillSheetManager.DAO
{
    /// <summary>
    /// Class UserDAO for managing the database interactions.
    /// </summary>
    public class UserDetailsDAO
    {
        #region Private Data Members

        /// <summary>
        /// To store the database context.
        /// </summary>
        private readonly UsersDetailsDBContext m_objContext;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Constructor for initializing UserDetailsDAO.
        /// </summary>
        /// <param name="objContext"> To take the database context. </param>
        public UserDetailsDAO(UsersDetailsDBContext objContext)
        {
            m_objContext = objContext;
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Method to save changes.
        /// </summary>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public void SaveChanges()
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.SaveChanges)}{Constants.MSG_METHOD}";
            try
            {
                m_objContext.SaveChanges();
            }
            catch (DbUpdateException objException) //If it throws DbUpdateException exception.
            {
                if (objException.InnerException == null)
                {
                    throw new CustomException(ErrorCodes.DbUpdateException, $"{objException.Message}{strMethodName}");
                }

                if (objException.InnerException.Message.Contains(nameof(User.UserName)))
                {
                    throw new CustomException(ErrorCodes.UserNameExist, $"{Constants.MSG_USER_NAME_EXIST}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.DateOfBirth)))
                {
                    throw new CustomException(ErrorCodes.DateOfBirthFieldHasProblem, $"{Constants.MSG_DATE_OF_BIRTH_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.WorkedInJapan)))
                {
                    throw new CustomException(ErrorCodes.WorkedInJapanFieldHasProblem, $"{Constants.MSG_WORKED_IN_JAPAN_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.Sex)))
                {
                    throw new CustomException(ErrorCodes.SexFieldHasProblem, $"{Constants.MSG_SEX_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.Qualification)))
                {
                    throw new CustomException(ErrorCodes.QualificationFieldHasProblem, $"{Constants.MSG_QUALIFICATION_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.Languages)))
                {
                    throw new CustomException(ErrorCodes.LanguagesFieldHasProblem, $"{Constants.MSG_LANGUAGES_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.Database)))
                {
                    throw new CustomException(ErrorCodes.DatabaseFieldHasProblem, $"{Constants.MSG_DATABASE_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(UserDetails.PhotoName)))
                {
                    throw new CustomException(ErrorCodes.PhotoNameFieldHasProblem, $"{Constants.MSG_PHOTONAME_PROBLEM}");
                }
                else
                {
                    throw new CustomException(ErrorCodes.DbUpdateException, $"${objException.InnerException.Message}{strMethodName}");
                }
            }
            catch (SqlException objException) //If it throws SqlException exception.
            {
                throw new CustomException(ErrorCodes.SqlException, $"{objException.Message}{strMethodName}");
            }
            catch (NotSupportedException objException) //If it throws NotSupportedException exception.
            {
                throw new CustomException(ErrorCodes.NotSupportedException, $"{objException.Message}{strMethodName}");
            }
            catch (TimeoutException objException) //If it throws TimeoutException exception.
            {
                throw new CustomException(ErrorCodes.TimeoutException, $"{objException.Message}{strMethodName}");
            }
            catch (DataException objException) //If it throws DataException exception.
            {
                throw new CustomException(ErrorCodes.DataException, $"{objException.Message}{strMethodName}");
            }
            catch (InvalidOperationException objException) //If it throws InvalidOperationException exception.
            {
                throw new CustomException(ErrorCodes.InvalidOperationException, $"{objException.Message}{strMethodName}");
            }
            catch (Exception objException) //If thre is nay other exception occured.
            {
                throw new CustomException(ErrorCodes.UndefinedException, $"{objException.Message}{strMethodName}", objException);
            }
        }

        #endregion
    }
}