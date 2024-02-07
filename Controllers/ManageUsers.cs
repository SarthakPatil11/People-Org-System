using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkillSheetManager.DAO;
using SkillSheetManager.EnumHolder;
using SkillSheetManager.ExceptionHolder;
using SkillSheetManager.Models.DBEntities;
using SkillSheetManager.Utility;

namespace SkillSheetManager.Controllers
{
    /// <summary>
    /// Class ManageUser for managing the users.
    /// </summary>
    [Authorize(Roles = Constants.ROLE_ADMIN)]
    public class ManageUsers : Controller
    {
        #region Private Data Members

        /// <summary>
        /// To store the DAO.
        /// </summary>
        private readonly UserDAO m_objUserDAO;

        /// <summary>
        /// To store the instance of the LogHandler for Auth logger.
        /// </summary>
        private readonly LogHandler<ManageUsers> m_objLogger;

        /// <summary>
        /// To store the host environment details.
        /// </summary>
        private readonly IWebHostEnvironment m_objWebHostEnvironment;

        #endregion

        #region Public Constructor

        /// <summary>
        /// To initialize the manage user controller.
        /// </summary>
        /// <param name="objUserDAOService"> To take the user DAO sevice. </param>
        /// <param name="objWebHostEnvironment"> To take the hosed environment information. </param>
        /// <param name="objLogger"> TO take the LogHandler for ManageUsers. </param>
        public ManageUsers(UserDAO objUserDAOService, IWebHostEnvironment objWebHostEnvironment, ILogger<ManageUsers> objLogger)
        {
            m_objUserDAO = objUserDAOService;
            m_objWebHostEnvironment = objWebHostEnvironment;
            m_objLogger = new(objLogger);
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Method to show manage user view.
        /// </summary>
        /// <returns> Show mange user view. </returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// To get the users.
        /// </summary>
        /// <returns> Users. </returns>
        [HttpGet]
        public object GetUsers(DataSourceLoadOptions objOptions)
        {
            dynamic objResult;
            try
            {
                var Users = m_objUserDAO.LoadUsers(Constants.ROLE_USER, objOptions);
                return Users;
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                objResult = Responsehandler.GetObject(false, (int)objException.ErrorCode, objException.Message);
                return Json(objResult);
            }
        }

        /// <summary>
        /// To create the user.
        /// </summary>
        /// <param name="objUser"> To take the user deatils. </param>
        /// <returns> Response JSON. </returns>
        [HttpPost]
        public JsonResult CreateUser(User objUser)
        {
            dynamic objResult;
            try
            {
                if (ModelState.IsValid) //To check the model is valid.
                {
                    objUser.Password = PasswordManager.Encrypt(objUser.Password);
                    m_objUserDAO.Add(objUser);
                    m_objUserDAO.SaveChanges();

                    m_objLogger.LogInfo(Constants.MSG_USER_CREATED);
                    objResult = Responsehandler.GetObject(true, strMsg: Constants.MSG_USER_CREATED);
                    return Json(objResult);
                }

                m_objLogger.LogError((int)ErrorCodes.ModelValidationFaild, Constants.MSG_MODEL_VALIDETION_FAILD);
                objResult = Responsehandler.GetObject(false, (int)ErrorCodes.ModelValidationFaild, Constants.MSG_MODEL_VALIDETION_FAILD);
                return Json(objResult);
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                objResult = Responsehandler.GetObject(false, (int)objException.ErrorCode, objException.Message);
                return Json(objResult);
            }
        }

        /// <summary>
        /// To get the user details by User ID.
        /// </summary>
        /// <param name="id"> To take the use ID. </param>
        /// <returns> Response JSON. </returns>
        [HttpGet]
        public JsonResult Edit(int id)
        {
            dynamic objResult;
            try
            {
                if (!m_objUserDAO.HasRecords()) //To check the user has records.
                {
                    m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    objResult = Responsehandler.GetObject(false, (int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    return Json(objResult);
                }

                var UserDetails = m_objUserDAO.GetUserByUserID(id);
                if (UserDetails == null) //To check the user deatils.
                {
                    m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    objResult = Responsehandler.GetObject(false, (int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    return Json(objResult);
                }

                UserDetails.Password = PasswordManager.Decrypt(UserDetails.Password);

                m_objLogger.LogInfo(Constants.USERS_FETHED);
                objResult = Responsehandler.GetObject(true, objData: UserDetails);
                return Json(objResult);
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                objResult = Responsehandler.GetObject(false, (int)objException.ErrorCode, objException.Message);
                return Json(objResult);
            }
        }

        /// <summary>
        /// To update the user details.
        /// </summary>
        /// <param name="objUser"> To take the use details. </param>
        /// <returns> Response JSON. </returns>
        [HttpPost]
        public JsonResult Update(User objUser)
        {
            dynamic objResult;
            try
            {
                if (ModelState.IsValid) //To check the model is valid.
                {
                    objUser.Password = PasswordManager.Encrypt(objUser.Password);
                    m_objUserDAO.Update(objUser);
                    m_objUserDAO.SaveChanges();

                    m_objLogger.LogInfo(Constants.MSG_USER_UPDATED);
                    objResult = Responsehandler.GetObject(true, strMsg: Constants.MSG_USER_UPDATED);
                    return Json(objResult);
                }

                m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                objResult = Responsehandler.GetObject(false, (int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                return Json(objResult);
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                objResult = Responsehandler.GetObject(false, (int)objException.ErrorCode, objException.Message);
                return Json(objResult);
            }
        }

        /// <summary>
        /// To delete the user by user ID.
        /// </summary>
        /// <param name="lstIDs"> To get the user ID <param>
        /// <returns> Response JSON. </returns>
        [HttpPost]
        public JsonResult DeleteUser(List<int> lstIDs)
        {
            dynamic objResult;
            try
            {
                //To delete the users.
                foreach (int nId in lstIDs)
                {
                    var User = m_objUserDAO.GetUserByUserID(nId);

                    if (User != null)
                    {
                        User.DeleteFlag = true;

                        m_objUserDAO.Update(User);
                        m_objUserDAO.SaveChanges();
                    }
                }

                m_objLogger.LogInfo(Constants.MSG_USER_DELETED);
                objResult = Responsehandler.GetObject(true, strMsg: Constants.MSG_USER_DELETED);
                return Json(objResult);
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                objResult = Responsehandler.GetObject(false, (int)objException.ErrorCode, objException.Message);
                return Json(objResult);
            }
        }

        #endregion
    }
}