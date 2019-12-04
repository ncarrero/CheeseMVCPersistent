using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddCategoryViewModel
    {
        //don't need an id property because it will be generated when the object is stored in the database
        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }
    }
}
