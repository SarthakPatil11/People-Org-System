using SkillSheetManager.EnumHolder;
using SkillSheetManager.ExceptionHolder;

namespace SkillSheetManager.Utility
{
    /// <summary>
    /// Class is used to manage the file related opretions.
    /// </summary>
    public class FileManager
    {
        #region Public Methods

        /// <summary>
        /// Method used to copy the file.
        /// </summary>
        /// <param name="objFile"> To take the file. </param>
        /// <param name="strStorePath"> To take the store location path. </param>
        /// <exception cref="CustomException"> If thre is an error it will throw customized exception. </exception>
        public static void CopyFile(IFormFile objFile, string strStorePath)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(CopyFile)}{Constants.MSG_METHOD}";
            try
            {
                using (var fileStream = new FileStream(strStorePath, FileMode.Create))
                {
                    objFile.CopyTo(fileStream);
                }
            }
            catch (IOException objException) //If file has some IO opretion on going.
            {
                throw new CustomException(ErrorCodes.IOException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If file has no write permission.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If Write method throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (ObjectDisposedException objException) //If Write method throws not supported exception.
            {
                throw new CustomException(ErrorCodes.ObjectDisposedException, $"{objException.Message}{strMethodName}");
            }
            catch (InvalidOperationException objException) //If Write method throws not supported exception.
            {
                throw new CustomException(ErrorCodes.InvalidOperationException, $"{objException.Message}{strMethodName}");
            }
            catch (Exception objException) //If thre is nay other exception occured.
            {
                throw new CustomException(ErrorCodes.UndefinedException, $"{objException.Message}{strMethodName}", objException);
            }
        }

        /// <summary>
        /// Method is used to delete the file fromspecific location.
        /// </summary>
        /// <param name="strFilePath"> To take the file path. </param>
        /// <exception cref="CustomException"> If thre is an error it will throw customized exception. </exception>
        public static void DeleteFile(string strFilePath)
        {
            string strMethodName = $"{Constants.MSG_OCCUR_IN}{nameof(DeleteFile)}{Constants.MSG_METHOD}";
            try
            {
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
            }
            catch (NotSupportedException objException) //If Write method throws not supported exception.
            {
                throw new CustomException(ErrorCodes.NotSupportedException, $"{objException.Message}{strMethodName}");
            }
            catch (DirectoryNotFoundException objException) //If Write method throws not supported exception.
            {
                throw new CustomException(ErrorCodes.DirectoryNotFoundException, $"{objException.Message}{strMethodName}");
            }
            catch (PathTooLongException objException) //If Write method throws not supported exception.
            {
                throw new CustomException(ErrorCodes.PathTooLongException, $"{objException.Message}{strMethodName}");
            }
            catch (IOException objException) //If file has some IO opretion on going.
            {
                throw new CustomException(ErrorCodes.IOException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentNullException objException) //If file has no write permission.
            {
                throw new CustomException(ErrorCodes.ArgumentNullException, $"{objException.Message}{strMethodName}");
            }
            catch (ArgumentException objException) //If Write method throws argument exception.
            {
                throw new CustomException(ErrorCodes.ArgumentException, $"{objException.Message}{strMethodName}");
            }
            catch (UnauthorizedAccessException objException) //If Write method throws not supported exception.
            {
                throw new CustomException(ErrorCodes.UnauthorizedAccessException, $"{objException.Message}{strMethodName}");
            }
            catch (Exception objException) //If thre is nay other exception occured.
            {
                throw new CustomException(ErrorCodes.UndefinedException, $"{objException.Message}{strMethodName}", objException);
            }
        }

        #endregion
    }
}