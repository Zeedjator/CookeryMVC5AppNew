using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cookery.WebUI.Models.Entities;

namespace Cookery.WebUI.Models
{
    public class RecipeViewModel
    {
        public Recipe Recipe { get; set; }
        public string IngredientsList { get; set; }
    }
}