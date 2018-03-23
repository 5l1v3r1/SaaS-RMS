using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Services
{
    public class FileUploader
    {
        private readonly IHostingEnvironment _environment;

        #region Constructor

        public FileUploader(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public FileUploader()
        {
        }

        #endregion

        #region UploadFile

        public string UploadFile(IFormFile file, UploadType uploadType)
        {
            var filename = DateTime.Now.ToFileTime().ToString();

            if (file != null)
            {
                var fileInfo = new FileInfo(file.Name);

                if ((fileInfo.Extension.ToLower() == ".jpg") || (fileInfo.Extension.ToLower() == ".jpeg") || 
                    (fileInfo.Extension.ToLower() == ".png") || (fileInfo.Extension.ToLower() == ".gif") || 
                    (fileInfo.Extension.ToLower() == ".pdf") || (fileInfo.Extension.ToLower() == ".docx") || 
                    (fileInfo.Extension.ToLower() == ".doc"))
                {
                    try
                    {
                        filename = DateTime.Now.ToFileTime() + fileInfo.Extension;

                        var uploadPath = Path.Combine(_environment.WebRootPath, "~/wwwroot/uploads/" + uploadType);

                        //check to see if the directory exists else, create directory
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }


                        if (file.Length > 0)
                        {
                            using (var fileStream = new FileStream(Path.Combine(uploadPath, filename), FileMode.Create))
                            {
                                file.CopyTo(fileStream);
                            }
                        }
                    }
                    catch (Exception)
                    {

                    }
                }   
            }
            return filename;
        }

        #endregion

        public bool ThumbnailCallback()
        {
            return false;
        }

    }
}
