using Contract.Entities;
using Contract.Resourse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace WebApplication1.Helper
{
    public static class Extinsion
    {
        public static BookResource ToResource(this Book entitiy)
        {
            return new BookResource()
            {
                Id = entitiy.Id,
                PublisherId = entitiy.PublisherId,
                Title = entitiy.Title,
                discraptions = entitiy.Discraptions,
                publisher = entitiy.Publisher.ToResourceNew()
            };
        }

        public static PublisherBookCreate ToLightResource(this Book entitiy)
        {
            return new PublisherBookCreate()
            {
                Id = entitiy.Id,
                Title = entitiy.Title,
                discraptions = entitiy.Discraptions
            };
        }


        public static PublisherResource ToResource(this Publisher entities)
        {
            return new PublisherResource()
            {
                Id = entities.Id,
                Name = entities.Name,
                Email = entities.Email,
                Salery = entities.Salery,
                DateOfBirth = entities.DateOfBirth,
                Books = entities.Books.Select(x => x.ToLightResource()).ToList()
            };
        }

        public static BookPublisherResource ToResourceNew(this Book entitiy)
        {
            return new BookPublisherResource()


            {
                Id = entitiy.Id,
                Title = entitiy.Title,
                Discraptions = entitiy.Discraptions,
                PublisherId = entitiy.PublisherId,
                Newpublisher = entitiy.Publisher.ToResourceNew(),
                BookAuthorResources = entitiy.Authors.Select(x => x.ToResourceNew()).ToList()

            };
        }

        public static BookAuthorResource ToResourceNew(this Author entitiy)
        {
            return new BookAuthorResource()
            {
                Id = entitiy.Id,
                FullName = entitiy.FullName
            };
        }

        public static PublisherBookResource ToResourceNew(this Publisher entities)
        {
            return new PublisherBookResource()
            {
                Id = entities.Id,
                Name = entities.Name,
            };
        }
        public static AuthorResource ToResource(this Author entitiy)
        {
            return new AuthorResource()
            {
                Id = entitiy.Id,
                FullName = entitiy.FullName,
                Email = entitiy.Email,
                Age = entitiy.Age,
                Books = entitiy.Books.Select(x => x.ToResourceNEw()).ToList()
            };
        }


        public static AuthorBookCreateResource ToResourceNEw(this Author entitiy)
        {
            return new AuthorBookCreateResource()
            {
                Id = entitiy.Id,
                FullName = entitiy.FullName,
                Email = entitiy.Email,
                Age = entitiy.Age,
            };
        }

        public static AuthorBookResource ToResourceNEw(this Book entitiy)
        {
            return new AuthorBookResource()
            {
                Id = entitiy.Id,
                Title = entitiy.Title,
                Discraptions = entitiy.Discraptions
            };
        }

    }
}
