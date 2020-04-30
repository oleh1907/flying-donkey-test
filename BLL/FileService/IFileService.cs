using DBL.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BLL.FileService
{
    public interface IFileService
    {
        Task<string> SaveOnDriveAsync(IFormFile file);
    }
}
