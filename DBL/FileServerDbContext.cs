using DBL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DBL
{
    public class FileServerDbContext : DbContext
    {
        public FileServerDbContext(DbContextOptions<FileServerDbContext> options) : base(options) { }

        public DbSet<FileInstance> Files { get; set; }
    }
}
