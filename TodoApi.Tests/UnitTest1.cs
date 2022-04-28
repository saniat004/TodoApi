using Xunit;
using Moq;
using TodoApiPractise.Services;
using System;
using TodoApiPractise.DTOs;
using TodoApiPractise.Controllers;
using TodoApiPractise.Entities;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoApi.Tests
{
    public class UnitTest1
    {
        private readonly Mock<IMapper> mapper = new Mock<IMapper>();
        private readonly Random Rand = new();
        private ToDoList GenerateRandomItem()
        {
            return new()
            {
                Id = Rand.Next(),
                Description= Guid.NewGuid().ToString(),
                Title = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                Done = false
            };
        }
        [Fact]
        //checking if it returns not found when item does not exist 
        public async Task GetItemAsync_WithUnexistingItem_ReturnsNotFound()
        {
            //Arrange
            
            Mock<IToDoListRepository> repositoryStub = new();
            repositoryStub.Setup(repo => repo.GetSpecificTodoAsync(It.IsAny<int>())).ReturnsAsync((ToDoList)null);

            var controller = new ToDoController(repositoryStub.Object, mapper.Object);
            //Act
            var result = await controller.GetTodList(It.IsAny<int>());

            //Assert
            
            result.Result.Should().BeOfType<NotFoundResult>();
        }
        //checking if the items get deleted
        [Fact]
        public async Task DeleteToDoList_Should_RemoveEntry()
        {
            //arrange
            ToDoList existingItem = GenerateRandomItem();
            var itemID = existingItem.Id;
            
            Mock<IToDoListRepository> repositoryStub = new();

            //Setup expected behavior of mock
            repositoryStub
                .Setup(_ => _.GetSpecificTodoAsync(itemID))
                .ReturnsAsync(existingItem);

            var controller = new ToDoController(repositoryStub.Object, mapper.Object);

            //act
            await controller.DeleteToDoList(itemID);

            //assert
            repositoryStub.Verify(_ => _.DeleteToDoList(existingItem));
        }
        [Fact]
        public async Task DeleteToDoList_WithExistingItem_Should_ReturnNoContent()
        {
            //Arrange
            ToDoList existingItem = GenerateRandomItem();
            var itemID = existingItem.Id;

            Mock<IToDoListRepository> repositoryStub = new();

            //Setup expected behavior of mock
            repositoryStub
                .Setup(_ => _.GetSpecificTodoAsync(itemID))
                .ReturnsAsync(existingItem);
            repositoryStub.Setup(_ => _.SaveChangesAsync()).ReturnsAsync(true);

            var controller = new ToDoController(repositoryStub.Object, mapper.Object);

            //Act
            ActionResult result = await controller.DeleteToDoList(itemID);

            //assert
            result.Should().BeOfType<NoContentResult>();
        }

    }

}