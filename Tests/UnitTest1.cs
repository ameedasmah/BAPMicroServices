using System;
using Xunit;
using Moq;
using Domain.mangers;
using Contract.Resourse;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.mangers.Producer;
using System.Management.Automation;
using Contract.models;
using DataAccessLayer.Repositories;
using DataAccessLayer.Entities;

namespace Publisherss.Domin.Test
{
    public class PublisherTest
    {
        private readonly Mock<IPublisherRepositories> _PublisherRepositoriesMock;
        private readonly Mock<IPublisherSend> _PublisherSendiesMock;
        private readonly IPublisherManger _PublisherManger;
        private readonly List<PublisherResource> PublisherResourceToTest;

        public PublisherTest()
        {
            _PublisherRepositoriesMock = new Mock<IPublisherRepositories>();
            _PublisherSendiesMock = new Mock<IPublisherSend>();
            _PublisherManger = new publishermanger(_PublisherRepositoriesMock.Object, _PublisherSendiesMock.Object);
            PublisherResourceToTest = new List<PublisherResource>
            {
                new PublisherResource
                {
                    Id=1,
                    Name="test1",
                    Email="test@test.test",
                    Salery=2340,
                    DateOfBirth = new DateTime(),
            }
          };
            DataAccessLayer.Entities.Publisher NewPublisher = new DataAccessLayer.Entities.Publisher
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
            };
        }
        [Fact]
        public async void GetAll_PublisherList()
        {
            //Arrange
            _PublisherRepositoriesMock.Setup(c => c.GetPublishers()).ReturnsAsync(new List<DataAccessLayer.Entities.Publisher>
            {
                new DataAccessLayer.Entities.Publisher
                {
                    Id = 1,
                    Name = "test2",
                    Email="test2@test.test",
                    Salery = 4325,
                    DateOfBirth = new DateTime(),
                }
            });
            //act
            var result = await _PublisherManger.GetPublishers();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<PublisherResource>>(result);

        }
        [Fact]
        public async void GetById_Publisher()
        {
            //Arrange

            var PublisherId = 1;

            DataAccessLayer.Entities.Publisher newPublisher = new DataAccessLayer.Entities.Publisher
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
            };

            _PublisherRepositoriesMock.Setup(c => c.GetPublisher(newPublisher.Id)).ReturnsAsync(newPublisher);
            //act
            PublisherResource newPublisherResource = new PublisherResource()
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
            };
            var result = await _PublisherManger.GetPublisher(newPublisherResource.Id);

            //Assert
            Assert.Equal(PublisherId, newPublisher.Id);
        }
        [Fact]
        public async void Create_Publisher()
        {
            //Arrange
            DataAccessLayer.Entities.Publisher newPublisher = new DataAccessLayer.Entities.Publisher
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
            };

            _PublisherRepositoriesMock.Setup(c => c.CreatePublisher(It.IsAny<DataAccessLayer.Entities.Publisher>())).ReturnsAsync(newPublisher);

            //act
            var result = await _PublisherManger.CreatePublisher(new Contract.models.PublisherModel
            {
                Name = "test3",
                Email = "test3@test.test",
                DateOfBirth = new DateTime(),
                Salery = 4530,
            });
            //Assert
            Assert.NotNull(result);
            Assert.IsType<PublisherResource>(result);
        }
        [Fact]
        public async void Remove_PublisherWithNotExisitingId()
        {
            //Arrange
            _PublisherRepositoriesMock.Setup(c => c.GetPublisher(It.IsAny<int>())).Returns(Task.FromResult<DataAccessLayer.Entities.Publisher>(null));
            //act
            var exception = await Assert.ThrowsAsync<Exception>(() =>
                _PublisherManger.DeletePublisher(int.MaxValue)
             );
            //Assert
            exception.Message.Equals("Id not Found");
        }
        //Happy case
        [Fact]
        public async void Remove_Publisher()
        {
            //Arrange
            DataAccessLayer.Entities.Publisher newPublisher = new DataAccessLayer.Entities.Publisher
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
                Books = new List<Book>() { }
            };
            //Arrange
            _PublisherRepositoriesMock.Setup(c => c.GetPublisher(It.IsAny<int>())).ReturnsAsync(newPublisher);
            //act
            PublisherResource newPublisherResource = new PublisherResource()
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
                Books = new List<PublisherBookCreateResource>() { }
            };
            await _PublisherManger.DeletePublisher(newPublisherResource.Id);
            //Assert
            _PublisherRepositoriesMock.Verify(x => x.deletePublisher(newPublisher.Id));
        }
        [Fact]
        public async void Remove_PublisherThatHasBooks()
        {
            //Arrange
            DataAccessLayer.Entities.Publisher newPublisher = new DataAccessLayer.Entities.Publisher
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
                Books = new List<Book>() { new Book { }, new Book { } }
            };
            _PublisherRepositoriesMock.Setup(c => c.GetPublisher(newPublisher.Id)).ReturnsAsync(newPublisher);

            //Arrange
            //_PublisherRepositoriesMock.Setup(c => c.GetPublisher(It.IsAny<int>())).Returns(Task.FromResult<Contract.Entities.Publisher>(null));
            var exception = await Assert.ThrowsAsync<Exception>(() =>
               _PublisherManger.DeletePublisher(newPublisher.Id));
            //Assert
            Assert.Equal(exception.Message, "Cant Delete A Publisher That has A book");
        }
    }
}
