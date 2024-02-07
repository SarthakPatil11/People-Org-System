using System.Security.Claims;
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
    /// Class PersonalDetails for managing personal details.
    /// </summary>
    [Authorize(Roles = Constants.ROLE_USER)]
    public class PersonalDetails : Controller
    {
        #region Private Data Members

        /// <summary>
        /// To store the user DAO.
        /// </summary>
        private readonly UserDetailsDAO m_objUserDetailsDAO;

        /// <summary>
        /// To store the user details DAO.
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
        /// To initialize the user details controller.
        /// </summary>
        /// <param name="objUserDAOService"> To take the user DAO sevice. </param>
        /// <param name="objUserDetailsDAOService"> To take the user details DAO sevice. </param>
        /// <param name="objWebHostEnvironment"> To take the hosed environment information. </param>
        /// <param name="objLogger"> To take the log </param>
        public PersonalDetails(UserDAO objUserDAOService, UserDetailsDAO objUserDetailsDAOService, IWebHostEnvironment objWebHostEnvironment, ILogger<ManageUsers> objLogger)
        {
            m_objUserDetailsDAO = objUserDetailsDAOService;
            m_objUserDAO = objUserDAOService;
            m_objWebHostEnvironment = objWebHostEnvironment;
            m_objLogger = new(objLogger);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// To show personal details view.
        /// </summary>
        /// <returns> personal details page. </returns>
        public IActionResult Index()
        {
            try
            {
                int nUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));

                var UserWholeData = m_objUserDAO.GetUserByUserIDIncludingUserDetails(nUserID) 
                    ?? throw new CustomException(ErrorCodes.UserNotFound, Constants.MSG_USER_NOT_FOUND);

                var UserInfo = UserWholeData.UserDetails;

                UserDetailsViewModel objUserDetailsViewModel = new()
                {
                    UserID = nUserID,
                    UserName = UserWholeData.UserName,
                    PhotoName = Constants.ALTERNET_IMG_PATH,
                };

                if (UserInfo != null) //To check the user is not null.
                {
                    string path = UserInfo.PhotoName != string.Empty ? $"{Constants.ROOT_FOLDER_PATH}{Constants.IMG_FOLDER_PATH}{UserInfo.PhotoName}" : string.Empty;
                    objUserDetailsViewModel.Sex = (UserInfo.Sex != null) ? (UserInfo.Sex == true) ? Constants.STR_ONE : Constants.STR_ZERO : string.Empty;
                    objUserDetailsViewModel.DateOfBirth = UserInfo.DateOfBirth;
                    objUserDetailsViewModel.JoiningDate = UserInfo.JoiningDate;
                    objUserDetailsViewModel.WorkedInJapan = (UserInfo.WorkedInJapan != null) ? (UserInfo.WorkedInJapan == true) ? Constants.STR_ONE : Constants.STR_ZERO : string.Empty;
                    objUserDetailsViewModel.Qualification = UserInfo.Qualification;
                    objUserDetailsViewModel.Languages = UserInfo.Languages;
                    objUserDetailsViewModel.Database = UserInfo.Database;
                    objUserDetailsViewModel.PhotoName = path;
                }

                if (TempData.ContainsKey(Constants.RESPONSE_DATA_VARIABLE_NAME))
                {
                    ViewBag.responseData = TempData[Constants.RESPONSE_DATA_VARIABLE_NAME];
                }

                m_objLogger.LogInfo(Constants.USERS_FETHED);
                return View(objUserDetailsViewModel);
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                var responseData = new ResponseData { ErrorCode = (int)objException.ErrorCode, ErrorMessage = objException.Message, Severity = Constants.SEVERITY_DANGER };
                ViewBag.responseData = responseData;
                return View();
            }
        }

        /// <summary>
        /// To update or create user details.
        /// </summary>
        /// <param name="objUserDetailsViewModel"> To take teh user details. </param>
        /// <returns> Updated view of the personal details. </returns>
        [HttpPost]
        public IActionResult CreateOrUpdate(UserDetailsViewModel objUserDetailsViewModel)
        {
            try
            {
                var responseData = null as dynamic;
                if (!ModelState.IsValid) //To check the model state is valid or not.
                {
                    m_objLogger.LogError((int)ErrorCodes.ModelValidationFaild, Constants.MSG_INVALID_MODEL);
                    return View(nameof(Index), objUserDetailsViewModel);
                }

                int nUserID = Convert.ToInt32(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var UserBaseData = m_objUserDAO.GetUserByUserIDIncludingUserDetails(nUserID);

                if (UserBaseData?.UserDetails == null) //To check the user has personal details or not.
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    UserBaseData.UserDetails = new()
                    {
                        UserID = nUserID,
                    };
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }

                UserBaseData.UserName = objUserDetailsViewModel.UserName;
                UserBaseData.UserDetails.DateOfBirth = objUserDetailsViewModel.DateOfBirth;
                UserBaseData.UserDetails.JoiningDate = objUserDetailsViewModel.JoiningDate;
                UserBaseData.UserDetails.Qualification = objUserDetailsViewModel.Qualification ?? string.Empty;
                UserBaseData.UserDetails.Languages = objUserDetailsViewModel.Languages ?? string.Empty;
                UserBaseData.UserDetails.Database = objUserDetailsViewModel.Database ?? string.Empty;

                if (objUserDetailsViewModel.Sex != string.Empty) //To check the sex field is updated.
                {
                    UserBaseData.UserDetails.Sex = objUserDetailsViewModel.Sex == Constants.STR_ONE;
                }

                if (objUserDetailsViewModel.WorkedInJapan != string.Empty) //To check the worked in japan field is updated.
                {
                    UserBaseData.UserDetails.WorkedInJapan = objUserDetailsViewModel.WorkedInJapan == Constants.STR_ONE;
                }

                if (objUserDetailsViewModel.Photo != null) //To check the photo field is updated.
                {
                    string strWWWRootPath = m_objWebHostEnvironment.WebRootPath;
                    string strFileName = Path.GetFileName(objUserDetailsViewModel.Photo.FileName);

                    if (UserBaseData.UserDetails.PhotoName != string.Empty) //To check the photo is present or not.
                    {
                        string strSearchFileURL = Path.Combine(strWWWRootPath, Constants.IMG_FOLDER_PATH, UserBaseData.UserDetails.PhotoName);
                        FileManager.DeleteFile(strSearchFileURL);
                    }

                    UserBaseData.UserDetails.PhotoName = strFileName = $"{UserBaseData.UserName}_{strFileName}";

                    string strStorePath = Path.Combine(strWWWRootPath, Constants.IMG_FOLDER_PATH, strFileName);
                    FileManager.CopyFile(objUserDetailsViewModel.Photo, strStorePath);
                }

                m_objUserDAO.Update(UserBaseData);

                m_objUserDetailsDAO.SaveChanges();

                m_objLogger.LogInfo(Constants.MSG_USER_UPDATED);
                responseData = new ResponseData { ErrorMessage = Constants.MSG_USER_UPDATED, Severity = Constants.SEVERITY_SUCCESS };
                TempData[Constants.RESPONSE_DATA_VARIABLE_NAME] = JsonConvert.SerializeObject(responseData);
                return RedirectToAction(nameof(Index));
            }
            catch (CustomException objException) //To handle the custom exception.
            {
                m_objLogger.LogError((int)objException.ErrorCode, objException.Message);
                var responseData = new ResponseData { ErrorCode = (int)objException.ErrorCode, ErrorMessage = objException.Message, Severity = Constants.SEVERITY_DANGER };
                TempData[Constants.RESPONSE_DATA_VARIABLE_NAME] = JsonConvert.SerializeObject(responseData);
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion
    }
}