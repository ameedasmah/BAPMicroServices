using Domain.Interface;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Helper;
using WebApplication1.models;
using WebApplication1.Repositories;
using WebApplication1.Resourse;

namespace Domain.mangers
{
    public class PublisherManger : IManger
    {
        private readonly IPublisherRepositories _repository;

        public PublisherManger(IPublisherRepositories repository)
        {
            _repository = repository;
        }

        public Task<PublisherResourse> CreatePublisher(PublisherModel newPublisherModel)
        {
            throw new NotImplementedException();
        }

        public Task DeleteResource(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Publisher> GetPublisher(int id)
        {
            return await _repository.GetPublisher(id);
        }

        public async Task<IEnumerable<PublisherResourse>> GetPublishers()
        {
            var PublisherEntities = await _repository.GetPublishers();

            var publisherResource = new List<PublisherResourse>();

            foreach (var item in PublisherEntities)
            {
                publisherResource.Add(item.ToResource());
            }
            return publisherResource;
        }

        public Task<PublisherResourse> PutPublisher(int id, PublisherModel model)
        {
            throw new NotImplementedException();
        }
    }
    }

