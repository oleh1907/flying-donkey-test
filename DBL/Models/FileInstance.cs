using System;
using System.Collections.Generic;
using System.Text;

namespace DBL.Models
{
    public class FileInstance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double SizeInMb { get; set; }
        public string Path { get; set; }
        public DateTime UploadDate { get; set; }
        public string UserWhoUploaded { get; set; }
    }
}
