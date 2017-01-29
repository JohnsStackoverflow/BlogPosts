using Crud.Abstract;
using Crud.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }
        

        //Images

        public ViewResult Display()
        {
            return View(repository.Posts);
        }
        public ViewResult Create()
        {
            return View("Edit", new PostModel());

        }

        public ViewResult Edit(int PostID)
        {
            PostModel editedItem = repository.Posts
            .FirstOrDefault(p => p.PostID == PostID);
            return View(editedItem);
        }
        [HttpPost]
        public ActionResult Edit(PostModel Post, IEnumerable<HttpPostedFileBase> files)
        {
            if (ModelState.IsValid)
            {
                foreach (var file in files)
                {
                    PostModel post = new PostModel();
                    if (file != null && file.ContentLength > 0)
                    {
                        string displayName = file.FileName;
                        string fileExtension = Path.GetExtension(displayName);
                        string fileName = string.Format("{0}.{1}", Guid.NewGuid(), fileExtension);
                        string path = Path.Combine(Server.MapPath("~/Img/"), fileName);
                        file.SaveAs(path);
                        post.ImageDisplayName = displayName;
                        post.ImagePath = fileName;
                        post.PostBody = Post.PostBody;
                        post.Heading = Post.Heading;
                    }
                    repository.Save(post);

                }
            }
            return RedirectToAction("display");
        }
        
        [HttpPost]
        public ActionResult DeletePosts(int PostID)
        {
            PostModel deletePost = repository.DeletePosts(PostID);
            if (deletePost != null)
            {
                TempData["message"] = string.Format("{0} was deleted", deletePost.Heading);
            }
            return RedirectToAction("display");
        }

        //[HttpPost]
        //public ActionResult Create(PostModel Post, IEnumerable<HttpPostedFileBase> files)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(Post);
        //    }
        //    foreach (var file in files)
        //    {
        //        PostModel post = new PostModel();
        //        if (file.ContentLength > 0)
        //        {
        //            string displayName = file.FileName;
        //            string fileExtension = Path.GetExtension(displayName);
        //            string fileName = string.Format("{0}.{1}", Guid.NewGuid(), fileExtension);
        //            string path = Path.Combine(Server.MapPath("~/Img/"), fileName);
        //            file.SaveAs(path);

        //            ImageModel image = new ImageModel()
        //            {
        //                ImagePath = fileName
        //            };
        //            post.Images.Add(image);
        //            post.ImageDisplayName = displayName;
        //            post.PostBody = Post.PostBody;
        //            post.Heading = Post.Heading;
        //        }
        //    }
        //    repository.Save(Post);
        //    return RedirectToAction("display");
        //}

        public ViewResult PublicPostDisplay()
        {
            return View(repository.Posts);
        }


    }
}