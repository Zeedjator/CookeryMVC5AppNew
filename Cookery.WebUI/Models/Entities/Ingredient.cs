using System.Collections.Generic;

namespace Cookery.WebUI.Models.Entities
{
    public class Ingredient
    {
        public Ingredient()
        {
            this.Recipes = new HashSet<Recipe>();
        }

        public int IngredientID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}
