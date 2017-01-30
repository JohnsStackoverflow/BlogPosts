using Crud.Abstract;
using Crud.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Crud.Concrete;

namespace Crud.Controllers
{
    public class HomeController : Controller
    {
        private EFDbContext db = new EFDbContext();
        // GET: Admin
        private IRepository repository;

        public HomeController(IRepository repo)
        {
            repository = repo;
        }
        public ViewResult Display()
        {
            return View(repository.Posts);
        }
        public ViewResult PublicPostDisplay()
        {
            return View(repository.Posts);
        }


        public ActionResult Create()
        {
            PostVM model = new PostVM();
            return View("Edit", model);
        }

        public ActionResult Edit(int id)
        {
            // Get your data model, for example
            PostModel post = db.Posts.Find(id);
            // Initialize view model and map properties
            PostVM model = new PostVM()
            {
                ID = post.ID,
                Heading = post.Heading,
                Body = post.Body,
                Images = post.Images.Select(x => new ImageVM()
                {
                    ID = x.ID,
                    Path = x.Path,

                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(PostVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            PostModel post = new PostModel();
            if (model.ID.HasValue)
            {
                // We are editing an existing Post, so get the original from the database, for example
                post = db.Posts.Find(model.ID);
            }
            // Map properties
            post.Heading = model.Heading;
            post.Body = model.Body;

            foreach (HttpPostedFileBase file in model.Files)
            {
                if (file.ContentLength > 0)
                {
                    string displayName = file.FileName;
                    string fileExtension = Path.GetExtension(displayName);
                    string fileName = string.Format("{0}.{1}", Guid.NewGuid(), fileExtension);
                    string path = Path.Combine(Server.MapPath("~/Images/"), fileName);
                    file.SaveAs(path);
                    ImageModel image = new ImageModel()
                    {
                        Path = path,
                        DisplayName = displayName
                    };
                    post.Images.Add(image);
                }
            }
            IEnumerable<ImageVM> deleted = model.Images.Where(x => x.IsDeleted);
            foreach (ImageVM image in deleted)
            {
                // delete the file from the server an remove from the database
            }
            repository.Save(post);
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

    }

}
