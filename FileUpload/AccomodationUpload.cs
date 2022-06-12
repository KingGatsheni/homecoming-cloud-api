using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Repo
{
    public class AccomodationUpload : IFileUpload<Accomodation>
    {
        IWebHostEnvironment web;
        private bool IsSuccess { get; set; } = false;
        public AccomodationUpload(IWebHostEnvironment webHost)
        {
            web = webHost;
        }
        public bool MultiFileUpload(Accomodation objectFile)
        {
            if (objectFile.ImageList != null)
            {
                objectFile.AccomodationGallary = new List<ListingImage>();
                foreach (IFormFile file in objectFile.ImageList)
                {
                    objectFile.AccomodationGallary.Add(new ListingImage { ImageUrl =FileUpload(file)});
                }

                IsSuccess = true;
                return IsSuccess;
            }
            else
            {
                IsSuccess = false;
                return IsSuccess;
            }
        }

        public string FileUpload(IFormFile file)
        {
            string fileName = null;
            try
            {
                if (file != null)
                {
                    string uploadDir = Path.Combine(web.WebRootPath, "Uploads\\Accomodations");
                    fileName = Guid.NewGuid().ToString() + "-" + file.FileName;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                }
            }
            catch (Exception si)
            {

                Debug.WriteLine(si.Message);
            }
            return fileName;
        }

        public Task<string> FileUploadAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<bool> MultiFileUploadAsync(Accomodation files)
        {
            throw new NotImplementedException();
        }
    }
}
