using System.Collections;
using System.Data;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.ResponseModel;
using DevExtreme.AspNet.Mvc;
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
    public class UserDAO
    {
        #region Private Data Members

        /// <summary>
        /// To store the database context.
        /// </summary>
        private readonly UsersDetailsDBContext m_objContext;

        #endregion

        #region Public Constructor

        /// <summary>
        /// Constructor for initializing UserDAO.
        /// </summary>
        /// <param name="objContext"> To take the database context. </param>
        public UserDAO(UsersDetailsDBContext objContext)
        {
            m_objContext = objContext;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Method to check the user has records.
        /// </summary>
        /// <returns> True if has records. </returns>
        public bool HasRecords()
        {
            if (m_objContext.Users == null) //To check context is null.
            {
                return false;
            }
            else //If it is not null.
            {
                return true;
            }
        }

        /// <summary>
        /// Method to add user.
        /// </summary>
        /// <param name="objUser"> To take user data. </param>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public void Add(User objUser)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.Add)}{Constants.MSG_METHOD}";
            try
            {
                m_objContext.Users.Add(objUser);
            }
            catch (DbUpdateException objException) //If it throws DbUpdateException exception.
            {
                throw new CustomException(ErrorCodes.DbUpdateException, $"{objException.Message}{strMethodName}");
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

        /// <summary>
        /// Method to update user.
        /// </summary>
        /// <param name="objUser"> To take user data. </param>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public void Update(User objUser)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.Update)}{Constants.MSG_METHOD}";
            try
            {
                m_objContext.Users.Update(objUser);
            }
            catch (DbUpdateException objException) //If it throws DbUpdateException exception.
            {
                throw new CustomException(ErrorCodes.DbUpdateException, $"{objException.Message}{strMethodName}");
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
                    throw new CustomException(ErrorCodes.DbUpdateException, $"{objException.Message}");
                }

                if (objException.InnerException.Message.Contains(nameof(User.UserName)))
                {
                    throw new CustomException(ErrorCodes.UserNameExist, $"{Constants.MSG_USER_NAME_EXIST}");
                }
                else if (objException.InnerException.Message.Contains(nameof(User.Password)))
                {
                    throw new CustomException(ErrorCodes.PasswordFieldHasProblem, $"{Constants.MSG_PASSWORD_PROBLEM}");
                }
                else if (objException.InnerException.Message.Contains(nameof(User.Email)))
                {
                    throw new CustomException(ErrorCodes.EmailFieldHasProblem, $"{Constants.MSG_EMAIL_PROBLEM}{strMethodName}");
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

        /// <summary>
        /// To load users as per the load options..
        /// </summary>
        /// <param name="strGroupName"> To take the group name. </param>
        /// <param name="objOptions"> To take the load options. </param>
        /// <returns> List of users. </returns>
        /// <exception cref="CustomException"> If there is any exception. </exception>
        public object LoadUsers(string strGroupName, DataSourceLoadOptions objOptions)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.LoadUsers)}{Constants.MSG_METHOD}";
            try
            {
                var filterGroupName = FilterHandler.GenerateFilter(nameof(User.GroupName), Constants.EQUAL_TO_OPRETOR, strGroupName);
                var filterDeleteFlag = FilterHandler.GenerateFilter(nameof(User.DeleteFlag), Constants.EQUAL_TO_OPRETOR, false);

                objOptions.Filter ??= new List<IList>();

                objOptions.Filter.Insert(0, filterGroupName);
                objOptions.Filter.Insert(1, filterDeleteFlag);

                LoadResult data = DataSourceLoader.Load(m_objContext.Users, objOptions);
                return data;
            }
            catch (SqlException objException) //If it throws SqlException.
            {
                throw new CustomException(ErrorCodes.SqlException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If it throws argument null exception.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If it throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (ObjectDisposedException objException) //If it throws ObjectDisposedException exception.
            {
                throw new CustomException(ErrorCodes.ObjectDisposedException, $"{objException.Message}{strMethodName}");
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

        /// <summary>
        /// Method to get user by group name.
        /// </summary>
        /// <param name="strGroupName"> To take group name. </param>
        /// <returns> User list. </returns>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public List<User> GetUsersByGroupName(string strGroupName)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.GetUsersByGroupName)}{Constants.MSG_METHOD}";
            try
            {
                List<User> lstUserDetails = m_objContext.Users.Where(u => u.GroupName == strGroupName && u.DeleteFlag == false).ToList();
                return lstUserDetails;
            }
            catch (SqlException objException) //If it throws SqlException.
            {
                throw new CustomException(ErrorCodes.SqlException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If it throws argument null exception.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If it throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (ObjectDisposedException objException) //If it throws ObjectDisposedException exception.
            {
                throw new CustomException(ErrorCodes.ObjectDisposedException, $"{objException.Message}{strMethodName}");
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

        /// <summary>
        /// Method to get the user data.
        /// </summary>
        /// <param name="strUserName"> To take the username. </param>
        /// <returns> User data </returns>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public User? GetUserByUserName(string strUserName)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.GetUserByUserName)}{Constants.MSG_METHOD}";
            try
            {
                var User = m_objContext.Users.SingleOrDefault(u => u.UserName == strUserName && u.DeleteFlag == false);
                return User;
            }
            catch (SqlException objException) //If it throws SqlException.
            {
                throw new CustomException(ErrorCodes.SqlException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If it throws argument null exception.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If it throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (ObjectDisposedException objException) //If it throws ObjectDisposedException exception.
            {
                throw new CustomException(ErrorCodes.ObjectDisposedException, $"{objException.Message}{strMethodName}");
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

        /// <summary>
        /// Method to get the user by user ID.
        /// </summary>
        /// <param name="nUserID"> To get the user ID. </param>
        /// <returns> User data. </returns>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public User? GetUserByUserID(int nUserID)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.GetUserByUserID)}{Constants.MSG_METHOD}";
            try
            {
                var User = m_objContext.Users.SingleOrDefault(u => u.UserID == nUserID && u.DeleteFlag == false);
                return User;
            }
            catch (SqlException objException) //If it throws SqlException.
            {
                throw new CustomException(ErrorCodes.SqlException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If it throws argument null exception.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If it throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (ObjectDisposedException objException) //If it throws ObjectDisposedException exception.
            {
                throw new CustomException(ErrorCodes.ObjectDisposedException, $"{objException.Message}{strMethodName}");
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

        /// <summary>
        /// Method to get the use by ID including user details.
        /// </summary>
        /// <param name="nUserID"> TO take the user ID. </param>
        /// <returns> User data. </returns>
        /// <exception cref="CustomException"> If there is some error wile adding user. </exception>
        public User? GetUserByUserIDIncludingUserDetails(int nUserID)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(this.GetUserByUserIDIncludingUserDetails)}{Constants.MSG_METHOD}";
            try
            {
                var User = m_objContext.Users.Include(u => u.UserDetails).FirstOrDefault(u => u.UserID == nUserID && u.DeleteFlag == false);
                return User;
            }
            catch (SqlException objException) //If it throws SqlException.
            {
                throw new CustomException(ErrorCodes.SqlException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If it throws argument null exception.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If it throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (ObjectDisposedException objException) //If it throws ObjectDisposedException exception.
            {
                throw new CustomException(ErrorCodes.ObjectDisposedException, $"{objException.Message}{strMethodName}");
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