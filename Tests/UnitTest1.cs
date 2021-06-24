using System;
using Xunit;
using Moq;
using Domain.mangers;
using WebApplication1.Repositories;
using Contract.Resourse;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.mangers.Producer;

namespace Publisherss.Domin.Test
{
    public class PublisherTest
    {
        private readonly Mock<IPublisherRepositories> _PublisherRepositoriesMock;
        private readonly Mock<IPublisherSend> _PublisherSendiesMock;
        private readonly IPublisherManger _PublisherMangerMock;
        private readonly List<PublisherResource> PublisherResourceToTest;

        public PublisherTest()
        {
            _PublisherRepositoriesMock = new Mock<IPublisherRepositories>();
            _PublisherSendiesMock = new Mock<IPublisherSend>();
            _PublisherMangerMock = new publishermanger(_PublisherRepositoriesMock.Object, _PublisherSendiesMock.Object);
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
            var result = await _PublisherMangerMock.GetPublishers();
            //Assert
            Assert.NotNull(result);
            Assert.IsType<List<PublisherResource>>(result);

        }
        [Fact]
        public async void GetById_Publisher()
        {
            //Arrange
            var PublisherId = 1;

            Contract.Entities.Publisher newPublisher = new Contract.Entities.Publisher
            {
                Id = 1,
                Name = "test2",
                Email = "test2@test.test",
                Salery = 4325,
                DateOfBirth = new DateTime(),
            };

            _PublisherRepositoriesMock.Setup(c => c.GetPublisher(newPublisher.Id)).ReturnsAsync(newPublisher);
            //act
            var result = await _PublisherMangerMock.GetPublisher(newPublisher.Id);

            //Assert
            Assert.Equal(PublisherId, newPublisher.Id);
        }
    }
}
