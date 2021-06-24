using System;
using Xunit;
using Moq;
using Domain.mangers;
using WebApplication1.Repositories;
using Contract.Resourse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publisherss.Domin.Test
{
    public class PublisherTest
    {
        private readonly Mock<IPublisherRepositories> _PublisherRepositoriesMock;
        private readonly Mock<IPublisherManger> _PublisherMangerMock = new Mock<IPublisherManger>();
        private readonly List<PublisherResource> PublisherResourceToTest;

        public PublisherTest()
        {
            _PublisherRepositoriesMock = new Mock<IPublisherRepositories>();
            _PublisherMangerMock = new Mock<IPublisherManger>();
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
        }
        [Fact]
        public async void GetAll_PublisherList()
        {
            //Arrange
            _PublisherRepositoriesMock.Setup(c => c.GetPublishers()).ReturnsAsync(new List<Contract.Entities.Publisher>
            {
                new Contract.Entities.Publisher
                {
                    Id = 1,
                    Name = "test2",
                    Email="test2@test.test",
                    Salery = 4325,
                    DateOfBirth = new DateTime(),
                }
            });
            //act
            var result = await _PublisherMangerMock.Object.GetPublishers();

            //Assert
            Assert.Null(result);
        }
        [Fact]
        public async void GetById_Publisher()
        {
            //Arrange
            var PublisherId = 1;

            Contract.Entities.Publisher newPublisher = new Contract.Entities.Publisher
            {
                Id = 1,
            };

            _PublisherRepositoriesMock.Setup(c => c.GetPublisher(1)).ReturnsAsync(newPublisher);
            //act
            var result = await _PublisherMangerMock.Object.GetPublisher(1);

            //Assert
            Assert.Equal(PublisherId, newPublisher.Id);
        }
    }
}
