using DBL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.FileService
{
    public class FileService : IFileService
    {
        private readonly IHostingEnvironment environment;

        public FileService(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public async Task<string> SaveOnDriveAsync(IFormFile file)
        {
            try
            {
                var uploads = Path.Combine(environment.ContentRootPath, "uploads");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var filePath = Path.Combine(uploads, file.FileName);

                if (File.Exists(filePath))
                    return null;


                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return filePath;
            }
            catch(Exception e)
            {
                // place to add logging
                throw e;
            }
        }
    }
}
