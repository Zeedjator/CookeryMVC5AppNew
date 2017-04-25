using System.Collections.Generic;
using Cookery.WebUI.Models.Entities;

namespace Cookery.WebUI.Infrastructure.Abstract
{
    public interface IRecipeRepository
    {
        IEnumerable<Recipe> Recipes { get; }
        IEnumerable<Ingredient> Ingredients { get; }
        void SaveRecipe(Recipe recipe);

        Recipe DeleteRecipe(int recipeId);
    }
}
