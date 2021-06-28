using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace DataAccessLayer.Repositories
{
    public interface IPublisherRepositories
    {
        Task<IEnumerable<Publisher>> GetPublishers();
        Task<Publisher> GetPublisher(int Id);

        Task<Publisher> CreatePublisher(Publisher publisher);

        Task<Publisher> updatePublisher(Publisher publisher);

        Task deletePublisher(int Id);


    }
    public class PublisherReposoitories : IPublisherRepositories
    {
        private readonly BookContext _Context;
        public PublisherReposoitories(BookContext Context)
        {
            _Context = Context;
        }
        public async Task<Publisher> CreatePublisher(Publisher publisher)
        {
            if (publisher is null)
            {
                throw new ArgumentNullException($"{nameof(CreatePublisher)} should not be null");
            }
            _Context.Add(publisher);
            await _Context.SaveChangesAsync();
            return await _Context.publishers.Include(item => item.Books).FirstOrDefaultAsync(x => x.Id == publisher.Id);
        }

        public async Task deletePublisher(int Id)
        {
            var bookToDelete = await _Context.publishers.FirstOrDefaultAsync(x => x.Id == Id);
            _Context.Remove(bookToDelete);
            await _Context.SaveChangesAsync();
        }

        public async Task<Publisher> GetPublisher(int Id)
        {
            return await _Context.publishers.Include(item => item.Books).FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await _Context.publishers.Include(item => item.Books).ToListAsync();
        }

        public async Task<Publisher> updatePublisher(Publisher publisher)
        {
            if (publisher is null)
            {
                throw new ArgumentNullException($"{nameof(updatePublisher)}publisher must not be null");
            }
            _Context.publishers.Update(publisher);
            await _Context.SaveChangesAsync();
            return publisher;
        }
    }
}
