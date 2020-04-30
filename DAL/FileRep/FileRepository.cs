using DBL;
using DBL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.FileRep
{
    public class FileRepository : IFileRepository
    {
        private readonly FileServerDbContext context;

        public FileRepository(FileServerDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<FileInstance>> GetAllAsync()
        {
            return await context.Files.ToListAsync();
        }

        public async Task<FileInstance> AddAsync(FileInstance file)
        {
            var savedFile = await context.Files.AddAsync(file);
            await context.SaveChangesAsync();

            return file;
        }
    }
}
