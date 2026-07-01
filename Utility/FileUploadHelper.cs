using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text;

namespace Utility
{
    public class FileUploadHelper
    {
        public static string UploadBase64StringTofile(string base64String, string fileDirectory, string uniqueFileName, string returnUrl)
        {
            // Convert base64 to byte array
            int res = 0;
            string filepathstr = "";

            try
            {
                //exception if missing prefix like "data:image/jpeg;base64,", then add in prefix to base64 string//
                string mimePart = base64String.Split(',')[0]; // "data:image/png;base64"
                string mimeType = mimePart.Split(':')[1].Split(';')[0]; // "image/png"
                base64String = base64String.Split(',')[1];
                // Optional: Map MIME type to file extension
                string extension = mimeType switch
                {
                    "image/png" => ".png",
                    "image/jpeg" => ".jpg",
                    "image/gif" => ".gif",
                    "application/pdf" => ".pdf",
                    // add more as needed
                    _ => ""
                };

                byte[] fileBytes = Convert.FromBase64String(base64String);

                // Save path
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                string filePath = Path.Combine(folderPath, uniqueFileName + extension);
                System.IO.File.WriteAllBytes(filePath, fileBytes);
                res = 1;
                if (res > 0)
                {
                    filepathstr = returnUrl + uniqueFileName + extension;
                    return filepathstr;
                }

            }
            catch (Exception ex)
            {
                filepathstr = ex.Message;
                
            }
            return filepathstr;
        }
        public static string UploadBase64StringToVideofile(string base64String, string fileDirectory, string uniqueFileName, string returnUrl)
        {
            string filepathstr = "";
            try
            {
                // Handle cases with or without prefix
                string extension = ".bin"; // default if no match
                string base64Data = base64String;

                if (base64String.Contains(","))
                {
                    string mimePart = base64String.Split(',')[0]; // e.g. "data:video/mp4;base64"
                    string mimeType = mimePart.Split(':')[1].Split(';')[0]; // e.g. "video/mp4"
                    base64Data = base64String.Split(',')[1];

                    // Map MIME type to extension
                    extension = mimeType switch
                    {
                        "image/png" => ".png",
                        "image/jpeg" => ".jpg",
                        "image/gif" => ".gif",
                        "application/pdf" => ".pdf",
                        "video/mp4" => ".mp4",
                        "video/webm" => ".webm",
                        "video/ogg" => ".ogg",
                        _ => ".bin"
                    };
                }

                byte[] fileBytes = Convert.FromBase64String(base64Data);

                // Optional: Size validation (e.g., max 50 MB)
                if (fileBytes.Length > 50 * 1024 * 1024)
                    throw new Exception("File too large. Max allowed size is 50MB.");

                // Ensure folder exists
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, uniqueFileName + extension);

                File.WriteAllBytes(filePath, fileBytes);

                filepathstr = returnUrl + uniqueFileName + extension;
            }
            catch (Exception ex)
            {
                filepathstr = ex.Message;
            }

            return filepathstr;
        }

        public static string UploadBase64FileToFtp(string base64String, string folderPath, string uniqueFileName)
        {
            try
            {

                string mimePart = base64String.Split(',')[0]; // "data:image/png;base64"
                string mimeType = mimePart.Split(':')[1].Split(';')[0]; // "image/png"
                base64String = base64String.Split(',')[1];
                // Optional: Map MIME type to file extension
                string extension = mimeType switch
                {
                    "image/png" => ".png",
                    "image/jpeg" => ".jpg",
                    "image/gif" => ".gif",
                    "application/pdf" => ".pdf",
                    "video/mp4" => ".mp4",
                    // add more as needed
                    _ => ""
                };

                byte[] fileBytes = Convert.FromBase64String(base64String);

                // 2️⃣ FTP credentials & base URL
                string ftpBaseUrl = "ftp://hmsfileupload.kdsgroup.co.in/";
                string ftpUser = "hmsfileupload";
                string ftpPass = "KDSGroup@123";

                // 3️⃣ Create folder structure: HandpumpImage/{handpumpId}/
                string _folderPath = folderPath;
                string ftpFolderUrl = ftpBaseUrl + _folderPath;

                // Try to create folder (ignore if already exists)
                try
                {
                    if (!FtpFolderExistsAsync(ftpFolderUrl, ftpUser, ftpPass))
                    {
                        FtpWebRequest createFolderRequest = (FtpWebRequest)WebRequest.Create(ftpFolderUrl);
                        createFolderRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                        createFolderRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                        createFolderRequest.UsePassive = true;
                        createFolderRequest.UseBinary = true;
                        createFolderRequest.KeepAlive = false;

                        using var folderResponse = (FtpWebResponse)createFolderRequest.GetResponse();
                    }
                    //else
                    //{
                    //    // 4️⃣ Upload file into the created folder
                    //    string ftpFileUrl = ftpFolderUrl + fileName;
                    //    FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(ftpFileUrl);
                    //    uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    //    uploadRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                    //    uploadRequest.UsePassive = true;
                    //    uploadRequest.UseBinary = true;
                    //    uploadRequest.KeepAlive = false;
                    //}

                   
                }
                catch (WebException ex)
                {
                    // Ignore "550 Folder already exists"
                    if (ex.Response is FtpWebResponse ftpResp &&
                        ftpResp.StatusCode != FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        throw; // rethrow other errors
                    }
                }

                // 4️⃣ Upload file into the created folder
                string ftpFileUrl = ftpFolderUrl + uniqueFileName + extension;
                FtpWebRequest uploadRequest = (FtpWebRequest)WebRequest.Create(ftpFileUrl);
                uploadRequest.Method = WebRequestMethods.Ftp.UploadFile;
                uploadRequest.Credentials = new NetworkCredential(ftpUser, ftpPass);
                uploadRequest.UsePassive = true;
                uploadRequest.UseBinary = true;
                uploadRequest.KeepAlive = false;

                using (var reqStream = uploadRequest.GetRequestStream())
                {
                     reqStream.WriteAsync(fileBytes, 0, fileBytes.Length);
                }

                using var uploadResponse = (FtpWebResponse) uploadRequest.GetResponse();

                // 5️⃣ Return HTTP URL for public access http://hmsfileupload.kdsgroup.co.in/
                return $"{folderPath}{uniqueFileName + extension}";
            
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private static bool FtpFolderExistsAsync(string folderUrl, string user, string pass)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(folderUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(user, pass);
                request.UsePassive = true;
                request.UseBinary = true;
                request.KeepAlive = false;

                using var response = (FtpWebResponse) request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                if (ex.Response is FtpWebResponse ftpResp &&
                    ftpResp.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false; // folder does not exist
                }
                throw; // some other error (network issue, wrong credentials, etc.)
            }
        }

        public static async Task<string> UploadBase64StringToVideofileAsync(string base64String, string fileDirectory, string uniqueFileName, string returnUrl)
        {
            string filepathstr = "";
            try
            {
                // Handle cases with or without prefix
                string extension = ".bin"; // default if no match
                string base64Data = base64String;

                if (base64String.Contains(","))
                {
                    string mimePart = base64String.Split(',')[0]; // e.g. "data:video/mp4;base64"
                    string mimeType = mimePart.Split(':')[1].Split(';')[0]; // e.g. "video/mp4"
                    base64Data = base64String.Split(',')[1];

                    // Map MIME type to extension
                    extension = mimeType switch
                    {
                        "image/png" => ".png",
                        "image/jpeg" => ".jpg",
                        "image/gif" => ".gif",
                        "application/pdf" => ".pdf",
                        "video/mp4" => ".mp4",
                        "video/webm" => ".webm",
                        "video/ogg" => ".ogg",
                        _ => ".bin"
                    };
                }

                byte[] fileBytes = Convert.FromBase64String(base64Data);

                // Optional: Size validation (e.g., max 50 MB)
                if (fileBytes.Length > 50 * 1024 * 1024)
                    throw new Exception("File too large. Max allowed size is 50MB.");

                // Ensure folder exists
                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, uniqueFileName + extension);

               await File.WriteAllBytesAsync(filePath, fileBytes);

                filepathstr = returnUrl + uniqueFileName + extension;
            }
            catch (Exception ex)
            {
                filepathstr = ex.Message;
            }

            return filepathstr;
        }

        public static async Task<string> UploadBase64StringToFileAsync(string base64String, string fileDirectory, string uniqueFileName, string returnUrl)
        {
            string filepathstr = string.Empty;

            try
            {
                // Ensure Base64 has proper prefix
                string[] parts = base64String.Split(',');
                string mimeType = "application/octet-stream";
                string base64Data = base64String;

                if (parts.Length > 1)
                {
                    string mimePart = parts[0]; // e.g. "data:image/png;base64"
                    mimeType = mimePart.Split(':')[1].Split(';')[0]; // "image/png"
                    base64Data = parts[1];
                }

                string extension = mimeType switch
                {
                    "image/png" => ".png",
                    "image/jpeg" => ".jpg",
                    "image/jpg" => ".jpg",
                    "image/gif" => ".gif",
                    "application/pdf" => ".pdf",
                    _ => "" // fallback (no extension)
                };

                byte[] fileBytes = Convert.FromBase64String(base64Data);

                string folderPath = Path.Combine(Directory.GetCurrentDirectory(), fileDirectory);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                string filePath = Path.Combine(folderPath, uniqueFileName + extension);

                // Async write
                await File.WriteAllBytesAsync(filePath, fileBytes);

                filepathstr = returnUrl + uniqueFileName + extension;
            }
            catch (Exception ex)
            {
                filepathstr = ex.Message;
            }

            return filepathstr;
        }

    }
}

