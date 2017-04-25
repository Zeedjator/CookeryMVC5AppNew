using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookery.WebUI.Infrastructure.Abstract;

namespace Cookery.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IRecipeRepository repository;

        public NavController(IRecipeRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null, string searchValue = null)
        {
            ViewBag.SelectedCategory = category;
            ViewBag.SearchValue = searchValue;
            IEnumerable<string> categories = repository.Recipes
                .Select(recipe => recipe.Category)
                .Distinct()
                .OrderBy(x => x);
            return PartialView("FlexMenu", categories);
        }
    }
}