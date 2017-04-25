using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookery.WebUI.Infrastructure.Abstract;
using Cookery.WebUI.Models;
using Cookery.WebUI.Models.Entities;


namespace Cookery.WebUI.Controllers
{
    public class RecipeController : Controller
    {
        private IRecipeRepository repository;
        public int pageSize = 4;

        public RecipeController(IRecipeRepository repo)
        {
            repository = repo;
        }


        public ViewResult List(string category, string searchValue = "", int page = 1)
        {
            if (searchValue != null)
            {
                @ViewBag.SearchValue = searchValue;
            }

            RecipesListViewModel model = new RecipesListViewModel
            {
                
                Recipes = repository.Recipes
                    .Where(p => (category == null || p.Category == category) && (String.IsNullOrEmpty(searchValue)?true:((p.Name.Contains(searchValue) || p.Ingredients.Any(c=>c.Name.Contains(searchValue))))))
                    .OrderBy(recipe => recipe.RecipeID)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = 0
                },

                CurrentCategory = category
            };
            model.PagingInfo.TotalItems = repository.Recipes.Count(p => (category == null || p.Category == category) &&
                                                                        (String.IsNullOrEmpty(searchValue) || ((p.Name.Contains(searchValue) || p.Ingredients.Any(c => c.Name.Contains(searchValue))))));
            return View(model);
        }

        public ViewResult Info(int recipeId)
        {
            Recipe recipe = repository.Recipes
                .FirstOrDefault(r => r.RecipeID == recipeId);
            return View(recipe);
        }

        public FileContentResult GetImage(int recipeId)
        {
            Recipe recipe = repository.Recipes
                .FirstOrDefault(r => r.RecipeID == recipeId);

            if (recipe != null)
            {
                return File(recipe.ImageData, recipe.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public FileContentResult GetInstrStepPhoto(int recipeId, int stepId)
        {
            Recipe recipe = repository.Recipes
                .FirstOrDefault(r => r.RecipeID == recipeId);
            InstructionStep instrStep = recipe.InstructionSteps.FirstOrDefault(istp => istp.StepID == stepId);
            if (instrStep != null)
            {
                return File(instrStep.ImageData, instrStep.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
    }

}