using Crud.Abstract;
using Crud.Models;
using System;
using System.Collections.Generic;
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
            if (Post.PostID == 0)
            {
                context.Posts.Add(Post);
            }
            else
            {
                PostModel dbEntry = context.Posts.Find(Post.PostID);
                if (dbEntry != null)
                {
                    dbEntry.ImagePath = Post.ImagePath;
                    dbEntry.Heading = Post.Heading;
                    //dbEntry.PostBody = Image.PostBody;
                    //dbEntry.PostDate = Image.PostDate;

                }
            }
            context.SaveChanges();
        }
    }
}

