using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {
        //add private field context
        private readonly CheeseDbContext context;
        //set up constructor to initialize the field
        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();
            return View(menus);
        }

        public IActionResult Add()
        {
            AddMenuViewModel addMenuViewModel = new AddMenuViewModel();

            return View(addMenuViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel addMenuViewModel)
        {
            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu
                {
                    Name = addMenuViewModel.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu");
                //return Redirect(String.Format("/Menu/ViewMenu/{0}", newMenu.ID));
            }

            return View(addMenuViewModel);
        }

public IActionResult ViewMenu(int id)
        {

            List<CheeseMenu> items = context
                .CheeseMenus
                .Include(item => item.Cheese)
                .Where(cm => cm.MenuID == id)
                .ToList();

            Menu menu = context.Menus.Single(m => m.ID == id);

            ViewMenuViewModel viewMenuViewModel = new ViewMenuViewModel
            {
                Menu = menu,
                Items = items
            };

            return View(viewMenuViewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            List<Cheese> cheeses = context.Cheeses.ToList();
            AddMenuItemViewModel addMenuItemViewModel = new AddMenuItemViewModel(menu, cheeses);

            return View(addMenuItemViewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel addMenuItemViewModel)
        {
            if (ModelState.IsValid)
            {
                //var cheeseID = addMenuItemViewModel.CheeseID;
                //var menuID = addMenuItemViewModel.MenuID;

                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == addMenuItemViewModel.CheeseID)
                    .Where(cm => cm.MenuID == addMenuItemViewModel.MenuID).ToList();

                if (existingItems.Count == 0)
                {
                    CheeseMenu menuItem = new CheeseMenu
                    {
                        //Cheese = context.Cheeses.Single(c => c.ID == cheeseID),
                        //Menu = context.Menus.Single(m => m.ID == menuID)
                        CheeseID = addMenuItemViewModel.CheeseID,
                        MenuID = addMenuItemViewModel.MenuID
                    };

                    context.CheeseMenus.Add(menuItem);
                    context.SaveChanges();
                }
            return Redirect(string.Format("/Menu/ViewMenu/{0}", addMenuItemViewModel.MenuID));
            }

            return View(addMenuItemViewModel);
        }
    }
}