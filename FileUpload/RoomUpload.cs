using homecoming.api.Abstraction;
using homecoming.api.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace homecoming.api.Repo
{
    public class RoomUpload:IFileUpload<Room>
    {
        IWebHostEnvironment web;
        private bool IsSuccess { get; set; } = false;

        public RoomUpload(IWebHostEnvironment webHost)
        {
            web = webHost;
        }
        public bool MultiFileUpload(Room objectFile)
        {
            if (objectFile.ImageList != null)
            {
                objectFile.RoomGallary = new List<RoomImage>();
                foreach (IFormFile file in objectFile.ImageList)
                {
                    objectFile.RoomGallary.Add(new RoomImage { ImageUrl = FileUpload(file) });
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
                    string uploadDir = Path.Combine(web.WebRootPath, "Uploads\\Rooms");
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
    }
}
