
using Contract.Entities;
using Contract.models;
using Contract.Resourse;
using Domins.mangers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Helper;
using WebApplication1.Repositories;

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

        public AuthorManger(IAuthorRepositories reposotiry)
        {
            _reposotiry = reposotiry;

        }

        public async Task<AuthorResource> CreateAuthor(AuthorModel newAuthor)
        {
            var AuthEntitiy = new Author()
            {
                FullName = newAuthor.FullName,
                Email = newAuthor.Email,
                Age = newAuthor.Age,
            };
            var AuthortOEntities = await _reposotiry.CreateAuthor(AuthEntitiy);
            return AuthortOEntities.ToResource();

        }

        public async Task Delete(int id)
        {
            var bookToDelete = await _reposotiry.GetAuthor(id);
            if (bookToDelete == null)
                throw new Exception("Id is not correct");

            if (bookToDelete.Books.Count == 0)
            {
                await _reposotiry.Delete(bookToDelete.Id);
                throw new Exception("NO Data");
            }

        }

        public async Task<AuthorResource> GetAuthor(int id)
        {
            var AuthorEntitiy = await _reposotiry.GetAuthor(id);

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
            var existingEntitiy = await _reposotiry.GetAuthor(Id);
            if (existingEntitiy is null)
                throw new Exception("there is a wrong Id");

            existingEntitiy.FullName = model.FullName;
            existingEntitiy.Email = model.Email;
            existingEntitiy.Age = model.Age;

            var UpdateEntitiy = await _reposotiry.Update(existingEntitiy);
            return UpdateEntitiy.ToResource();
        }
        
    }
}
