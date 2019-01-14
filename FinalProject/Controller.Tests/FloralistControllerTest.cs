using FinalProject.Controllers;
using FinalProject.Models;
using FinalProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Web.Http;
using System.Collections.Generic;
using FluentEmail.Core;

namespace Controller.Tests
{
    public class FloralistControllerTest
    {
        private readonly Mock<IRepository> _mockRepo;
        private readonly Mock<EmailSender> _mockService;
        private readonly Mock<IFluentEmail> _mockFluentEmail;
        private readonly ImagesController _imagesController;
        private readonly ProposalsController _proposalController;
        private readonly ProposalItemsController _proposalItemsController;
        private readonly TagsController _tagsController;


        public FloralistControllerTest()
        {
            _mockRepo = new Mock<IRepository>();
            _mockService = new Mock<EmailSender>();
            _mockFluentEmail = new Mock<IFluentEmail>();
            _imagesController = new ImagesController(_mockRepo.Object);
            _proposalController = new ProposalsController(_mockRepo.Object, _mockService.Object);
            _proposalItemsController = new ProposalItemsController(_mockRepo.Object);
            _tagsController = new TagsController(_mockRepo.Object);

        }

        //Helper Method to create Mocking for Controller Context
        public ControllerContext CreateMockControllerContext()
        {
            var claimsPrincipal = new Mock<ClaimsPrincipal>();
            claimsPrincipal.Setup(m => m.HasClaim(It.IsAny<string>(), It.IsAny<string>())).Returns(true);


            var mockHttpContext = new Mock<HttpContext>();
            mockHttpContext.Setup(m => m.User).Returns(claimsPrincipal.Object);


            var context = new ControllerContext()
            {
                HttpContext = mockHttpContext.Object
            };

            return context;
        }


        [Fact]
        public async void ImagesController_Create_Test()
        {
            //Arrange
            var images = new Image() { };
            var file = new Mock<IFormFile>();
            var ms = new MemoryStream();
            var fileName = "kelly-sikkema-250501-unsplash.jpg";

            //Instance of the Controller Context
            _imagesController.ControllerContext = CreateMockControllerContext();

            //Setup to create File object
            file.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
            .Returns((Stream stream, CancellationToken token) => ms.CopyToAsync(stream)).Verifiable();
            file.Setup(f => f.FileName).Returns(fileName);
            file.Setup(f => f.Length).Returns(1);

            var inputFile = file.Object;

            _mockRepo.Setup(c => c.AddImageAsync(It.IsAny<Image>())).Returns(Task.CompletedTask);
            _mockRepo.Setup(c => c.GetDesignerIdForUserId(It.IsAny<string>())).Returns(1);

            //Act
           
            var result = await _imagesController.Create(images, inputFile);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);


        }

        [Fact]
        public async Task ProposalsController_Create_Test()
        {
            var proposal = new Proposal();

            // Arrange
            
            _proposalController.ControllerContext = CreateMockControllerContext();
            _mockRepo.Setup(c => c.GetDesignerIdForUserId(It.IsAny<string>())).Returns(1);
            _mockRepo.Setup(c => c.AddProposalAsync(It.IsAny<Proposal>())).Returns(Task.CompletedTask);


            // Act
            var result = await _proposalController.Create(proposal) as IActionResult;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("ImageSearch", redirectToActionResult.ActionName);
        }

        

        [Fact]
        public async void ProposalItemsController_Delete_Test()
        {
            // Arrange
            var proposalitem = new ProposalItem();
            int Id = 0;

            _mockRepo.Setup(c => c.AddProposalItemAsync(Id, proposalitem));

            // Act

            var result = await _proposalItemsController.Delete(Id, proposalitem.Id);

            //Assert

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void TagsController_Create_test()
        {
            //Arrange
            var tag = new Tag();

            _mockRepo.Setup(c => c.AddTagAsync(It.IsAny<Tag>())).Returns(Task.CompletedTask);

            //Act
            // Act
            var result = await _tagsController.Create(tag) as IActionResult;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);

        }

        

        [Fact]
        public async Task ProposalsController_Share_Test()
        {
            // Arrange
            
            var proposal = new Proposal() { Id = 1, Title = "Hello", DesignerId = 1};
            var Id = 1;

            _proposalController.ControllerContext = CreateMockControllerContext();

            _mockRepo.Setup(c => c.GetProposalAsync(Id)).Returns(Task.FromResult(proposal));
            _mockService.Setup(c => c.sendProposalEmail(It.IsAny<Proposal>()));
            _mockRepo.Setup(c => c.ShareProposalAsync(Id)).Returns(Task.CompletedTask);

            // Act
            var result = await _proposalController.Share(Id, proposal) as IActionResult;

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);


        }

    }

 }
    