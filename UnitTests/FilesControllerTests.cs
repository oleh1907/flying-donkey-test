using API.Controllers;
using BLL.FileService;
using DAL.FileRep;
using DBL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTests
{
    public class FilesControllerTests
    {
        [Fact]
        public async Task GetAllAsync_Success_ReturnsOkObjectResultWithProperValue()
        {
            //Arrange
            var filesRepoMock = new Mock<IFileRepository>();
            filesRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetFileInstances());

            var filesServiceMock = new Mock<IFileService>();

            var configMock = new Mock<IConfiguration>();

            var controller = new FilesController(filesRepoMock.Object, filesServiceMock.Object, configMock.Object);

            //Act
            var result = await controller.GetAllAsync();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<FileInstance>>(actionResult.Value);
            Assert.Equal(GetFileInstances().Count, model.Count());
        }
        private List<FileInstance> GetFileInstances()
        {
            var files = new List<FileInstance>
            { 
                new FileInstance { Id=1, Name="Name1", SizeInMb=1 },
                new FileInstance { Id=1, Name="Name2", SizeInMb=2 },
                new FileInstance { Id=1, Name="Name3", SizeInMb=3 }
            };
            return files;
        }

        [Fact]
        public async Task GetAllAsync_Fail_ReturnsNoContentResult()
        {
            //Arrange
            var filesRepoMock = new Mock<IFileRepository>();
            filesRepoMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(GetFileInstancesEmpty());

            var filesServiceMock = new Mock<IFileService>();

            var configMock = new Mock<IConfiguration>();

            var controller = new FilesController(filesRepoMock.Object, filesServiceMock.Object, configMock.Object);

            //Act
            var result = await controller.GetAllAsync();

            //Assert
            var actionResult = Assert.IsType<NoContentResult>(result);
        }

        private List<FileInstance> GetFileInstancesEmpty()
        {
            return new List<FileInstance> {};
        }

        [Fact]
        public void GetAllowedFileExtensions_Success_ReturnsOkObjectResultWithProperValue()
        {
            //Arrange
            var filesRepoMock = new Mock<IFileRepository>();

            var filesServiceMock = new Mock<IFileService>();

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(conf => conf["AppDetails:AllowedFileTypes"]).Returns(GetExtensions());

            var controller = new FilesController(filesRepoMock.Object, filesServiceMock.Object, configMock.Object);

            //Act
            var result = controller.GetAllowedFileExtensions();

            //Assert
            var actionResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsType<string[]>(actionResult.Value);
            Assert.Equal(GetExtensions().Split(","), model);
        }

        private string GetExtensions()
        {
            return ".txt,.docx,.pdf";
        }

        [Fact]
        public void GetAllowedFileExtensions_Fail_ThrowsException()
        {
            //Arrange
            var filesRepoMock = new Mock<IFileRepository>();

            var filesServiceMock = new Mock<IFileService>();

            var configMock = new Mock<IConfiguration>();
            configMock.Setup(conf => conf["AppDetails:AllowedFileTypes"]).Returns(GetExtensionsNull());

            var controller = new FilesController(filesRepoMock.Object, filesServiceMock.Object, configMock.Object);

            //Assert
            Assert.Throws<NullReferenceException>(() => controller.GetAllowedFileExtensions());
        }
        private string GetExtensionsNull()
        {
            return null;
        }
    }
}
