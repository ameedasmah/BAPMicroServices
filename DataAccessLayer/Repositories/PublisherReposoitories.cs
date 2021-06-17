using Contract.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace WebApplication1.Repositories
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
            if(publisher is null)
            {
                throw new ArgumentNullException($"{nameof(CreatePublisher)} should not be null");
            }
            try
            {

            _Context.Add(publisher);
            await _Context.SaveChangesAsync();
                return await _Context.publishers.Include(item => item.Books).FirstOrDefaultAsync(x => x.Id == publisher.Id);
            }
            catch (Exception exiption)
            {
                throw new Exception($"there is an error in Create Publiser, Check It please : {exiption.Message}");
            }
        }

        public async Task deletePublisher(int Id)
        {
            var bookToDelete = await _Context.publishers.FirstOrDefaultAsync(x => x.Id == Id);
            _Context.Remove(bookToDelete);
            await _Context.SaveChangesAsync();
        }

        public async Task<Publisher> GetPublisher(int Id)
        {
            try
            {
            return await _Context.publishers.Include(item=>item.Books).FirstOrDefaultAsync(x=>x.Id==Id);
            }
            catch(Exception exception)
            {
                throw new Exception($"handel Your Id method broo :D:D:D:D:D:D : {exception.Message}");
            }
        }

        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            try
            {

            return await _Context.publishers.Include(item=>item.Books).ToListAsync();
            }
            catch(Exception exception)
            {
                throw new Exception($"handel your get method :D:D:D:C : {exception.Message}");
            }
        }

        public async Task<Publisher> updatePublisher(Publisher publisher)
        {
            if(publisher is null) {
                throw new ArgumentNullException($"{nameof(updatePublisher)}publisher must not be null"); 
            }
            try
            {
                _Context.publishers.Update(publisher);
                await _Context.SaveChangesAsync();
                return publisher;
            }
            catch(Exception ex)
            {
                throw new Exception($"hii cheack your Error in updatePublisher Mehtod: {ex.Message} ");
            }
        }

    }
}
