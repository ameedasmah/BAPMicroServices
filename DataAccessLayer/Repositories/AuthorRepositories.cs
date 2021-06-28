using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{

    public interface IAuthorRepositories
    {
        Task<ICollection<Author>> GetAuthors(Func<Author, bool> predicate = null);
        Task<Author> GetAuthor(int Id);
        Task<Author> CreateAuthor(Author author);
        Task<Author> Update(Author author);
        Task Delete(int Id);
    }

    public class AuthorRepositories : IAuthorRepositories
    {
        private readonly BookContext _bookContext;

        public AuthorRepositories(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            _bookContext.Add(author);
            await _bookContext.SaveChangesAsync();
            return await _bookContext.Authors.Include(item => item.Books).FirstOrDefaultAsync(x => x.Id == author.Id);
        }
        public async Task Delete(int Id)
        {
            var BookToDelelte = await _bookContext.Authors.FirstOrDefaultAsync(x => x.Id == Id);
            _bookContext.Remove(BookToDelelte);
            await _bookContext.SaveChangesAsync();
        }
        public async Task<Author> GetAuthor(int Id)
        {

            return await _bookContext.Authors.Include(x => x.Books).FirstOrDefaultAsync(X => X.Id == Id);
        }
        public async Task<ICollection<Author>> GetAuthors(Func<Author, bool> predicate)
        {
            if (predicate != null)
            {
                return _bookContext.Authors.Include(x => x.Books).Where(predicate).ToList();
            }
            return await _bookContext.Authors.Include(x => x.Books).ToListAsync();
        }
        public async Task<Author> Update(Author author)
        {
            _bookContext.Authors.Update(author);
            await _bookContext.SaveChangesAsync();
            return author;
        }
    }
}
