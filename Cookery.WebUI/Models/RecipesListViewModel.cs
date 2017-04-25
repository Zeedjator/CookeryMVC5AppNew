using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cookery.WebUI.Models.Entities;


namespace Cookery.WebUI.Models
{
    public class RecipesListViewModel
    {
        public IEnumerable<Recipe> Recipes { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public string CurrentCategory { get; set; }
    }
}