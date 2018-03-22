using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SaaS_RMS.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SaaS_RMS.Extensions
{
    public class FileUploader
    {
        private readonly IHostingEnvironment _environment;

        #region Constructor

        public FileUploader(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        #endregion

        #region UploadFile

        public string UploadFile(IFormFile file, UploadType uploadType)
        {
            var fileinfo = new FileInfo(file.Name);
            var filename = DateTime.Now.ToFileTime() + fileinfo.Extension;
            

            var uploads = Path.Combine(_environment.WebRootPath, "uploads" + uploadType);



            if (file.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, filename), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
        }

        #endregion

    }
}
