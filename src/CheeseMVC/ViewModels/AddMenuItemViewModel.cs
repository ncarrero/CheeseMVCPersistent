using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public int MenuID { get; set; }
        [Display(Name = "Menu Item")]
        public int CheeseID { get; set; }
        public Menu Menu { get; set; }
        [Required]
        public List<SelectListItem> Cheeses { get; set; }

        public AddMenuItemViewModel() { }

        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Menu = menu;
            Cheeses = new List<SelectListItem>();

            foreach (var cheese in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = cheese.ID.ToString(),
                    Text = cheese.Name
                });
            }
        }
    }  
}
