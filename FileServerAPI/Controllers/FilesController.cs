using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.File;
using DAL.FileRep;
using BLL.FileService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using DBL.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IFileRepository fileRepository;
        private readonly IFileService fileService;
        private readonly IConfiguration config;

        public FilesController(IFileRepository fileRepository, IFileService fileService, IConfiguration config)
        {
            this.fileRepository = fileRepository;
            this.fileService = fileService;
            this.config = config;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var files = await fileRepository.GetAllAsync();

            if (files.Count() == 0)
                return NoContent();

            return Ok(files);
        }

        [HttpGet("extensions")]
        public IActionResult GetAllowedFileExtensions()
        {
            string[] extensions = config["AppDetails:AllowedFileTypes"].Split(",");

            return Ok(extensions);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadAsync([FromForm]FileDataDTO fileData)
        {
            try
            {
                int maxSize = Convert.ToInt32(config["AppDetails:AllowedFileSizeInMb"]) * 1000000;

                if (fileData.File.Length > maxSize)
                    return BadRequest("File is too big");

                string[] alloweFileTypes = config["AppDetails:AllowedFileTypes"].Split(",");

                string extension = Path.GetExtension(fileData.File.FileName);

                if (!alloweFileTypes.Contains(extension))
                    return BadRequest("File extension is not allowed");

                string path = await fileService.SaveOnDriveAsync(fileData.File);
                if(path == null)
                {
                    // Such file already exists
                    return BadRequest("File with such name already exists");
                }

                var fileInstance = await fileRepository.AddAsync(new FileInstance()
                {
                    Name = fileData.File.FileName,
                    SizeInMb = (double)fileData.File.Length / 1000000,
                    Path = path,
                    UploadDate = DateTime.Now,
                    UserWhoUploaded = fileData.UserWhoUploaded
                });

                return Ok(fileInstance);
            }
            catch(Exception e)
            {
                // place to add logging
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}