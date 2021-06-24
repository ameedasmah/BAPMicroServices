using System;
using Xunit;
using Moq;
using Domain.mangers;
using WebApplication1.Repositories;
using Contract.Resourse;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Publisher.Domin.Test
{
    public class PublisherTest
    {
        private readonly Mock<IPublisherRepositories> _PublisherRepositoriesMock;
        private readonly Mock<IPublisherManger> _PublisherMangerMock;
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
            _PublisherRepositoriesMock.Setup(c => c.GetPublishers()).ReturnsAsync(new List<Contract.Entities.Publisher>
            {
                new Contract.Entities.Publisher
                {
                    Id = 1,
                    Name = "",
                    Salery = 1234,
                    DateOfBirth = new DateTime(),
                }
            });
        }
    }
}
