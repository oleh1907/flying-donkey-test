using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.File
{
    public class FileDataDTO
    {
        [Required]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "You must specify username between 1 and 20 characters")]
        public string UserWhoUploaded { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
