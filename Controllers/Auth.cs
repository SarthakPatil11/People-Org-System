using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SkillSheetManager.DAO;
using SkillSheetManager.EnumHolder;
using SkillSheetManager.ExceptionHolder;
using SkillSheetManager.Models;
using SkillSheetManager.Models.ViewModel;
using SkillSheetManager.Utility;

namespace SkillSheetManager.Controllers
{
    /// <summary>
    /// Class Auth for managing authentication.
    /// </summary>
    public class Auth : Controller
    {
        #region Private Data Members

        /// <summary>
        /// To store the DAO.
        /// </summary>
        private readonly UserDAO m_objUserDAO;

        /// <summary>
        /// To store the instance of the LogHandler for Auth logger.
        /// </summary>
        private readonly LogHandler<Auth> m_objLogger;

        #endregion

        #region Public Constructor

        /// <summary>
        /// To initialize the Auth controller.
        /// </summary>
        /// <param name="objUserDAOService"> To take the user DAO sevice. </param>
        public Auth(UserDAO objUserDAOService, ILogger<Auth> objLogger)
        {
            m_objUserDAO = objUserDAOService;
            m_objLogger = new(objLogger);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// To show the login page.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// Method to get the users by group name.
        /// </summary>
        /// <param name="groupName"> To take the group name. </param>
        /// <returns> List of users. </returns>
        [HttpGet]
        public JsonResult GetUsers(string? groupName)
        {
            dynamic objResult;
            try
            {
                if (groupName == null || !m_objUserDAO.HasRecords()) //To check the group name and context is not null.
                {
                    m_objLogger.LogError((int)ErrorCodes.SelectProperUser, Constants.MSG_SELECT_PROPER_USER);
                    objResult = Responsehandler.GetObject(false, (int)ErrorCodes.SelectProperUser, Constants.MSG_SELECT_PROPER_USER);
                    return Json(objResult);
                }

                //To get user details.
                var UserDetails = m_objUserDAO.GetUsersByGroupName(groupName);

                if (UserDetails == null) //If user details are null.
                {
                    m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    objResult = Responsehandler.GetObject(false, (int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    return Json(objResult);
                }

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
        /// Method to get the user by it's user name.
        /// </summary>
        /// <param name="userName"> To take the user name. </param>
        /// <returns> User details. </returns>
        [HttpGet]
        public JsonResult GetUser(string? userName)
        {
            dynamic objResult;
            try
            {
                if (userName == null || !m_objUserDAO.HasRecords()) //To check the group name and context is not null.
                {
                    m_objLogger.LogError((int)ErrorCodes.SelectProperUser, Constants.MSG_SELECT_PROPER_USER);
                    objResult = Responsehandler.GetObject(false, (int)ErrorCodes.SelectProperUser, Constants.MSG_SELECT_PROPER_USER);
                    return Json(objResult);
                }

                //To get user details.
                var UserDetails = m_objUserDAO.GetUserByUserName(userName);
                if (UserDetails == null) //If user details are null.
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
        /// To view login page.
        /// </summary>
        /// <returns> login view. </returns>
        public IActionResult Login()
        {
            if (TempData.ContainsKey(Constants.RESPONSE_DATA_VARIABLE_NAME))
            {
                ViewBag.responseData = TempData[Constants.RESPONSE_DATA_VARIABLE_NAME];
            }

            return View();
        }

        /// <summary>
        /// To login the user and authenticate.
        /// </summary>
        /// <param name="objUserViewModel"> To take user credentials. </param>
        /// <returns> If authenticated then redirect to appropriate view.  </returns>
        [HttpPost]
        public IActionResult Login(UserViewModel objUserViewModel)
        {
            try
            {
                var responseData = null as dynamic;
                if (!ModelState.IsValid) //To check the model state.
                {
                    m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    responseData = new ResponseData { ErrorCode = (int)ErrorCodes.UserNotFound, ErrorMessage = Constants.MSG_USER_NOT_FOUND, Severity = Constants.SEVERITY_DANGER };
                    responseData = JsonConvert.SerializeObject(responseData);
                    ViewBag.responseData = responseData;
                    return View(objUserViewModel);
                }

                var User = m_objUserDAO.GetUserByUserName(objUserViewModel.UserName);
                if (User == null) //To check the user is null.
                {
                    m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    responseData = new ResponseData { ErrorCode = (int)ErrorCodes.UserNotFound, ErrorMessage = Constants.MSG_USER_NOT_FOUND, Severity = Constants.SEVERITY_DANGER };
                    ViewBag.responseData = JsonConvert.SerializeObject(responseData);
                    return View(objUserViewModel);
                }

                string strUserPassword = PasswordManager.Decrypt(User.Password);
                if (strUserPassword != objUserViewModel.Password) //To check password is matching.
                {
                    m_objLogger.LogError((int)ErrorCodes.PasswordNotCorrect, Constants.MSG_PASSWORD_NOT_CORRECT);
                    ModelState.AddModelError(nameof(UserViewModel.Password), Constants.MSG_PASSWORD_NOT_CORRECT);
                    return View(objUserViewModel);
                }

                var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(User.UserID)),
                    new Claim(ClaimTypes.Name, objUserViewModel.UserName),
                    new Claim(ClaimTypes.Role, objUserViewModel.GroupName)
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                var principle = new ClaimsPrincipal(identity);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principle);

                string strMsg = string.Format(Constants.MSG_LOGIN_SUCCESSFUL, objUserViewModel.UserName, objUserViewModel.GroupName);
                m_objLogger.LogInfo(strMsg);

                if (objUserViewModel.GroupName == Constants.ROLE_ADMIN) //To check the logged in user role is admin.
                {
                    return RedirectToAction(nameof(Index), nameof(ManageUsers));
                }
                else //If user role is not admin.
                {
                    return RedirectToAction(nameof(Index), nameof(PersonalDetails));
                }
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, Constants.SEVERITY_DANGER);
                var responseData = new ResponseData { ErrorCode = (int)objException.ErrorCode, ErrorMessage = objException.Message, Severity = Constants.SEVERITY_DANGER };
                ViewBag.responseData = JsonConvert.SerializeObject(responseData);
                return View(objUserViewModel);
            }
        }

        /// <summary>
        /// To logout the user.
        /// </summary>
        /// <returns> Show login view. </returns>
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            //To delete if some cookies are created.
            var StoredCookies = Request.Cookies.Keys;
            foreach (var Cookie in StoredCookies)
            {
                Response.Cookies.Delete(Cookie);
            }

            m_objLogger.LogInfo(Constants.MSG_LOGOUT_SUCCESSFUL);
            TempData.Keep(Constants.RESPONSE_DATA_VARIABLE_NAME);
            return RedirectToAction(nameof(Login));
        }

        /// <summary>
        /// To show change password view.
        /// </summary>
        /// <returns> xhange password view. </returns>
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// To change the password.
        /// </summary>
        /// <param name="objFormCollection"> To the user credentials. </param>
        /// <returns> Redirect to login view. </returns>
        [HttpPost]
        [Authorize]
        public IActionResult ChangePassword(IFormCollection objFormCollection)
        {
            try
            {
                var responseData = null as dynamic;
                string? strNewPassword = objFormCollection[Constants.NEW_PASSWORD_ID];

                if (strNewPassword != null) //To check the new password is not null.
                {
                    int nUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

                    var user = m_objUserDAO.GetUserByUserID(nUserID);

                    if (user != null) //To check the user is note null.
                    {
                        user.Password = PasswordManager.Encrypt(strNewPassword);
                        m_objUserDAO.SaveChanges();

                        m_objLogger.LogInfo(Constants.MSG_PASSWORD_CHANGED);
                        responseData = new ResponseData { ErrorMessage = Constants.MSG_PASSWORD_CHANGED, Severity = Constants.SEVERITY_SUCCESS };

                        TempData[Constants.RESPONSE_DATA_VARIABLE_NAME] = JsonConvert.SerializeObject(responseData);
                        return RedirectToAction(nameof(Logout));
                    }

                    m_objLogger.LogError((int)ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);
                    responseData = new ResponseData { ErrorCode = (int)ErrorCodes.UserNotFound, ErrorMessage = Constants.MSG_USER_NOT_FOUND, Severity = Constants.SEVERITY_DANGER };
                    ViewBag.responseData = JsonConvert.SerializeObject(responseData);
                    return View(objFormCollection);
                }

                m_objLogger.LogError((int)ErrorCodes.PasswordEmpty, Constants.MSG_PASSWORD_EMPTY);
                responseData = new ResponseData { ErrorCode = (int)ErrorCodes.PasswordEmpty, ErrorMessage = Constants.MSG_PASSWORD_EMPTY, Severity = Constants.SEVERITY_DANGER };
                ViewBag.responseData = JsonConvert.SerializeObject(responseData);
                return View(objFormCollection);
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, Constants.SEVERITY_DANGER);
                var responseData = new ResponseData { ErrorCode = (int)objException.ErrorCode, ErrorMessage = objException.Message, Severity = Constants.SEVERITY_DANGER };
                ViewBag.responseData = JsonConvert.SerializeObject(responseData);
                return View(objFormCollection);
            }
        }

        #endregion
    }
}