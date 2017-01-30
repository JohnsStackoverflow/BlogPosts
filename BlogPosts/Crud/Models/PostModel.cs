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
        public int ID { get; set; }
        [Required(ErrorMessage = "Heading is Required")]
        [Display(Name = "Heading")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "Body is Required")]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Body")]
        public string Body { get; set; }
      //  public string ImageDisplayName { get; set; }
       // public string ImagePath { get; set; } //Temporarly here until I can get the ImageModel Method Working
        public virtual ICollection<ImageModel> Images { get; set; }
        public IEnumerable<HttpPostedFileBase> File { get; set; }
    }

    public class ImageModel
    {
        [Key]
        public int ID { get; set; }
        public string Path { get; set; }
        public virtual PostModel Post { get; set; }
        public string DisplayName { get; set; }
    }
    public class ImageVM
    {
        public int? ID { get; set; }
        public string Path { get; set; }
        public string DisplayName { get; set; }
        public bool IsDeleted { get; set; }
    }
    public partial class PostVM
    {
        public PostVM()
        {
           Images = new List<ImageVM>();
        }

        public int? ID { get; set; }
        public string Heading { get; set; }
        public string Body { get; set; }
        public IEnumerable<HttpPostedFileBase> Files { get; set; }
        public List<ImageVM> Images { get; set; }
  
    }
}



