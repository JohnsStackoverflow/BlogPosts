using Crud.Abstract;
using Crud.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace Crud.Concrete
{
    public class EFRepository : IRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<PostModel> Posts
        {
            get { return context.Posts; }
        }

        public void Save(PostModel Post)
        {
            try
            {
                if (Post.ID == 0)
                {
                    context.Posts.Add(Post);
                }
                else
                {
                    PostModel dbEntry = context.Posts.Find(Post.ID);
                    if (dbEntry != null)
                    {
                        dbEntry.Heading = Post.Heading;
                        dbEntry.Body = Post.Body;
                        dbEntry.Date = Post.Date;
                    }
                }
                context.SaveChanges();

            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
        }

        public PostModel DeletePosts(int ID)
        {
            PostModel dbEntry = context.Posts.Find(ID);
            ImageModel dbEntryForeign = context.Images.Find(ID);
            if (dbEntry != null)
            {
                context.Posts.Remove(dbEntry);
                context.Images.Remove(dbEntryForeign);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}

