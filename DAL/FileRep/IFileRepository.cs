using DBL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FileRep
{
    public interface IFileRepository
    {
        Task<IEnumerable<FileInstance>> GetAllAsync();
        Task<FileInstance> AddAsync(FileInstance file);
    }
}
