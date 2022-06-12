using homecoming.api.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homecoming.api.Abstraction
{
    public interface IFileUpload<T>:IMultiFileUpload<T>
    {
        public string FileUpload(IFormFile file);

        public Task<string> FileUploadAsync(IFormFile file);
    }
}
