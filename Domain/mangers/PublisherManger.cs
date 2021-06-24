
using Contract.Entities;
using Contract.models;
using Contract.Resourse;
using Domain.mangers.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Helper;
using WebApplication1.Repositories;

namespace Domain.mangers
{
    public interface IPublisherManger
    {
        Task<IEnumerable<PublisherResource>> GetPublishers();
        Task<PublisherResource> GetPublisher(int id);
        Task<PublisherResource> CreatePublisher(PublisherModel newPublisherModel);
        Task<PublisherResource> PutPublisher(int id, PublisherModel model);
        Task DeleteResource(int Id);
    }
    public class publishermanger : IPublisherManger
    {
        private readonly IPublisherRepositories _repository;
        private readonly IPublisherSend _publisherSend;
        public publishermanger(IPublisherRepositories repository, IPublisherSend publisherSend)
        {
            _repository = repository;
            _publisherSend = publisherSend;
        }
        public async Task<PublisherResource> CreatePublisher(PublisherModel newPublisherModel)
        {
            var newPublisherEntity = new Publisher()
            {
                Name = newPublisherModel.Name,
                Email = newPublisherModel.Email,
                DateOfBirth = newPublisherModel.DateOfBirth,
                Salery = newPublisherModel.Salery,
            };
            var newPublisherResource = await _repository.CreatePublisher(newPublisherEntity);
            _publisherSend.sendPublisher(new SendArgument()
            {
                Id = newPublisherEntity.Id,
                OperationType = "Publisher",
                Type = "Create"
            });
            return newPublisherResource.ToResource();
        }
        public async Task DeleteResource(int Id)
        {
            var BookToDelete = await _repository.GetPublisher(Id);
            if (BookToDelete == null) throw new Exception("Id not Found");
            if (BookToDelete.Books.Count == 0)
            {
                await _repository.deletePublisher(BookToDelete.Id);
                _publisherSend.sendPublisher(new SendArgument()
                {
                    Id = BookToDelete.Id,
                    OperationType = "Publisher",
                    Type = "Delete"
                });
            }
            else
            {
                throw new Exception("Cant Delete A Publisher That has A book");
            }
        }
        public async Task<PublisherResource> GetPublisher(int id)
        {
            var PublisherEntitiy = await _repository.GetPublisher(id);
            if (PublisherEntitiy is null)
            {
                throw new Exception($"this {id} is not found");
            }
            return PublisherEntitiy.ToResource();
        }

        public async Task<IEnumerable<PublisherResource>> GetPublishers()
        {
            var PublisherEntities = await _repository.GetPublishers();

            var publisherResource = new List<PublisherResource>();

            foreach (var item in PublisherEntities)
            {
                publisherResource.Add(item.ToResource());
            }
            return publisherResource;
        }
        public async Task<PublisherResource> PutPublisher(int id, PublisherModel model)
        {
            var existingEntity = await _repository.GetPublisher(id);
            if (existingEntity == null) throw new Exception("Id not Found");
            existingEntity.Name = model.Name;
            existingEntity.Email = model.Email;
            existingEntity.Salery = model.Salery;
            existingEntity.DateOfBirth = model.DateOfBirth;
            var updatedEntity = await _repository.updatePublisher(existingEntity);
            _publisherSend.sendPublisher(new SendArgument()
            {
                Id = existingEntity.Id,
                OperationType = "Publisher",
                Type = "Update"
            });
            return updatedEntity.ToResource();
        }
    }
}

