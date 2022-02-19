using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DotNetExam.Models
{
    public class Products
    {

        [Required(ErrorMessage ="Please enter the Product Id")]
        [Display(Name ="Product Id")]
        public int ProductId { get; set; }




        [Required(ErrorMessage ="Please enter the Product Name")]
        [Display(Name ="Product Name")]
        public string ProductName { get; set; }



        [Required(ErrorMessage = "Please enter the Product Rate")]
        public decimal Rate { get; set; }



        [Required(ErrorMessage = "Please enter the Product Description")]
        public string Description { get; set; }



        [Required(ErrorMessage = "Please enter the Category Name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
    }
}

