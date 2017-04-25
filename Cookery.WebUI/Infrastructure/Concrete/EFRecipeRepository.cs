using System.Collections.Generic;
using System.Data;
using System.Linq;
using Cookery.WebUI.Infrastructure.Abstract;
using Cookery.WebUI.Infrastructure.Identity;
using Cookery.WebUI.Models.Entities;

namespace Cookery.WebUI.Infrastructure.Concrete
{
    public class EFRecipeRepository : IRecipeRepository
    {
        AppIdentityEntitiesContext context = new AppIdentityEntitiesContext();

        public IEnumerable<Recipe> Recipes
        {
            get { return context.Recipes; }
        }
        public IEnumerable<Ingredient> Ingredients
        {
            get { return context.Ingredients; }
        }

        public void SaveRecipe(Recipe recipe)
        {
            if (recipe.RecipeID == 0)
            {
                var ingrsPack = recipe.Ingredients.ToList();
                recipe.Ingredients.Clear();
                context.Recipes.Add(recipe);
                context.SaveChanges();
                Recipe dbEntry = context.Recipes.OrderByDescending(i => i.RecipeID).FirstOrDefault();
                foreach (var ingr in ingrsPack)
                {
                    if (context.Ingredients.Any(i => i.Name == ingr.Name))
                    {
                        context.Ingredients.FirstOrDefault(x => x.Name == ingr.Name)?.Recipes.Add(dbEntry);
                    }
                    else
                    {
                        dbEntry.Ingredients.Add(ingr);
                    }
                }
            }
            else
            {
                Recipe dbEntry = context.Recipes.Find(recipe.RecipeID);
                if (dbEntry != null)
                {
                    dbEntry.Name = recipe.Name;
                    dbEntry.Description = recipe.Description;
                    dbEntry.Category = recipe.Category;
                    dbEntry.ImageData = recipe.ImageData;
                    dbEntry.ImageMimeType = recipe.ImageMimeType;
                    dbEntry.Ingredients.Clear();
                    foreach (var istp in dbEntry.InstructionSteps.ToList())
                        context.InstructionSteps.Remove(istp);
                    foreach (var ingr in recipe.Ingredients)
                    {
                        if (context.Ingredients.Any(i => i.Name == ingr.Name))
                        {
                            context.Ingredients.FirstOrDefault(x => x.Name == ingr.Name)?.Recipes.Add(dbEntry);
                        }
                        else
                        {
                            dbEntry.Ingredients.Add(ingr); 
                        }
                    }

                    foreach (var iStep in recipe.InstructionSteps)
                    {                  
                        dbEntry.InstructionSteps.Add(iStep);
                    }
                }
            }
            context.SaveChanges();
        }

        public Recipe DeleteRecipe(int recipeId)
        {
            Recipe dbEntry = context.Recipes.Find(recipeId);
            if (dbEntry != null)
            {
                dbEntry.Ingredients.Clear();
                context.Recipes.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
