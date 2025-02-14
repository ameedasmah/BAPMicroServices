﻿using Contract.Exceptions;
using Contract.models;
using Contract.Resourse;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Domain.Helper;
using Domain.mangers.Producer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.mangers
{
    public interface IAuthorMangers
    {
        Task<IEnumerable<AuthorResource>> GetAuthors();
        Task<AuthorResource> GetAuthor(int id);
        Task<AuthorResource> CreateAuthor(AuthorModel newAuthor);
        Task<AuthorResource> PutAuthor(int Id, AuthorModel model);
        Task Delete(int id);
    }
    public class AuthorManger : IAuthorMangers
    {
        private readonly IAuthorRepositories _reposotiry;
        private readonly IAuthor _AuthorSend;
        public AuthorManger(IAuthorRepositories reposotiry, IAuthor AuthorSend)
        {
            _reposotiry = reposotiry;
            _AuthorSend = AuthorSend;
        }
        public async Task<AuthorResource> CreateAuthor(AuthorModel newAuthor)
        {
            if (newAuthor == null)
            {
                throw new ArgumentNullException($"{nameof(CreateAuthor)} entity musn't to be null ");
            }
            var AuthEntitiy = new Author()
            {
                FullName = newAuthor.FullName,
                Email = newAuthor.Email,
                Age = newAuthor.Age,
            };
            var AuthortOEntities = await _reposotiry.CreateAuthor(AuthEntitiy);
            _AuthorSend.SendAuthor(new AuthorToSend()
            {
                Id = AuthEntitiy.Id,
                OperationType = "Author",
                Type = "Create",
            });
            return AuthortOEntities.ToResource();
        }
        public async Task Delete(int id)
        {
            var bookToDelete = await _reposotiry.GetAuthor(id);
            if (bookToDelete == null)
                throw new KeyNotFoundException($"Id is not correct");
            if (bookToDelete.Books.Count == 0)
            {
                _AuthorSend.SendAuthor(new AuthorToSend()
                {
                    Id = bookToDelete.Id,
                    OperationType = "Author",
                    Type = "Delete"
                });

                await _reposotiry.Delete(bookToDelete.Id);
            }
            else
            {
                throw new ErrorException("Cant Delete Author that has a Book");
            }
        }
        public async Task<AuthorResource> GetAuthor(int id)
        {
            var AuthorEntitiy = await _reposotiry.GetAuthor(id);
            if (AuthorEntitiy is null)
            {
                throw new KeyNotFoundException($"this {id} is not found");
            }
            return AuthorEntitiy.ToResource(); ;
        }
        public async Task<IEnumerable<AuthorResource>> GetAuthors()
        {
            var AuthorEntities = await _reposotiry.GetAuthors();

            var ResponseAuthor = new List<AuthorResource>();

            foreach (var item in AuthorEntities)
            {
                ResponseAuthor.Add(item.ToResource());
            }
            return ResponseAuthor;
        }
        public async Task<AuthorResource> PutAuthor(int Id, AuthorModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException($"{nameof(CreateAuthor)} entity musn't to be null ");
            }
            var existingEntitiy = await _reposotiry.GetAuthor(Id);
            if (existingEntitiy is null)
                throw new KeyNotFoundException("there is a wrong Id");
            existingEntitiy.FullName = model.FullName;
            existingEntitiy.Email = model.Email;
            existingEntitiy.Age = model.Age;
            var UpdateEntitiy = await _reposotiry.Update(existingEntitiy);
            return UpdateEntitiy.ToResource();
        }
    }
}
