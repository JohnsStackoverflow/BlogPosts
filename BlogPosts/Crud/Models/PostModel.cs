using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud.Models
{
    public partial class PostModel
    {
        [Key]
        [HiddenInput(DisplayValue = false)]
        public int PostID { get; set; }
        [Required(ErrorMessage = "Heading is Required")]
        [Display(Name = "Heading")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Body is Required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Body")]
        public string PostBody { get; set; }
        public string ImageDisplayName { get; set; }
        public string ImagePath { get; set; } //Temporarly here until I can get the ImageModel Method Working
        //public virtual ICollection<ImageModel> Images { get; set; }
        public IEnumerable<HttpPostedFileBase> File { get; set; }
    }

    //public class ImageModel
    //{
    //    [Key]
    //    public int ImageId { get; set; }
    //    public string ImagePath { get; set; }
    //    public virtual PostModel Post { get; set; }
    //}

    //public class CreatePostViewModel
    //{
    //    [Key]
    //    public int CreatePostId { get; set; }

    //    [Required(ErrorMessage = "Heading is Required")]
    //    public string Heading { get; set; }
    //    [Required(ErrorMessage = "Body is Required")]
    //    public string PostBody { get; set; }
    //    public string ImagePath { get; set; }

    //    public virtual ICollection<ImageModel> Images { get; set; }
    //    public virtual PostModel Post { get; set; }
    //}
}



