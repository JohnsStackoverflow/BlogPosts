using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Crud.Models;

namespace Crud.Abstract
{
    public interface IRepository
    {
        IEnumerable<PostModel> Posts { get; }
        void Save(PostModel posts);
        PostModel DeletePosts(int ID);
       

    }
}