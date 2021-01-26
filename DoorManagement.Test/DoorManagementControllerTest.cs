using NUnit.Framework;
using DoorManagementServer.Controllers;
using FakeItEasy;
using DoorManagementServer;
using Microsoft.Extensions.Logging;
using GateManagement.Domain;
using System;

namespace DoorManagement.Test
{
    public class DoorManagementControllerTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddDoor_ThrowsException_When_AddRepository_Failed()
        {
            var mockService = A.Fake<IDoorManagementRepository>();
            A.CallTo(() => mockService.AddDoor(A<Door>.Ignored)).WithAnyArguments().Throws(() => new DoorManagementException("Invalid data"));
            var mockLoging = A.Fake<ILogger<DoorManagementController>>();
            DoorManagementController doorManagementController = new DoorManagementController(mockLoging, mockService);

            try
            {
                var _ = doorManagementController.AddDoor(null).Result;
                Assert.Fail("Controller not failed");
            }
            catch(Exception ex)
            {

            }
        }


        [Test]
        public void AddDoor_ReturnsCreated_When_RepositoryServiceCallSucceeded()
        {
            var mockService = A.Fake<IDoorManagementRepository>();
            A.CallTo(() => mockService.AddDoor(A<Door>.Ignored)).Returns(new Door());
            var mockLoging = A.Fake<ILogger<DoorManagementController>>();
            DoorManagementController doorManagementController = new DoorManagementController(mockLoging, mockService);
            var res = doorManagementController.AddDoor(null).Result;
            var statusCode = ((Microsoft.AspNetCore.Mvc.ObjectResult)res).StatusCode;
            if (!(statusCode.HasValue) || statusCode.Value !=201)
            {
                Assert.Fail("Add Door functionality not returning created when service call succeeded");
            }
        }


    }
}