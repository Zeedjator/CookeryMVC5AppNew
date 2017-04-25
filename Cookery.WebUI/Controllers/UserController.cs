using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cookery.WebUI.Infrastructure.Abstract;
using Cookery.WebUI.Infrastructure.Identity;
using Cookery.WebUI.Models;
using Cookery.WebUI.Models.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Cookery.WebUI.Controllers
{
    public class UserController : Controller
    {
        IRecipeRepository repository;

        public UserController(IRecipeRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            //Выводим только рецепты, принадлежащие текущему пользовтаелю
            return View(repository.Recipes.Where(r => r.AppUser.Id == User.Identity.GetUserId()));
            //return View(repository.Recipes);
        }

        public ViewResult Edit(int recipeId)
        {
            Recipe recipe = repository.Recipes
                .FirstOrDefault(r => r.RecipeID == recipeId);
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe = recipe;
            foreach (var ingr in recipe.Ingredients)
            {
                recipeViewModel.IngredientsList += ingr.Name + '\n';
            }
            return View(recipeViewModel);
        }

        [HttpPost]
        public ActionResult Edit(RecipeViewModel recipeViewModel, ICollection<string> instrStepDesc, ICollection<HttpPostedFileBase> insrStepPhotos, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    recipeViewModel.Recipe.ImageMimeType = image.ContentType;
                    recipeViewModel.Recipe.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(recipeViewModel.Recipe.ImageData, 0, image.ContentLength);
                }
                Recipe recipe = new Recipe();
                recipe = recipeViewModel.Recipe;
                List<string> ingredients = recipeViewModel.IngredientsList.ToLower().Trim('\n', ' ').Split('\n').ToList();
                foreach (var ingr in ingredients)
                {
                    var ingred = new Ingredient();
                    ingred.Name = ingr.Trim();
                    if (ingred.Name != "")
                        recipe.Ingredients.Add(ingred);
                }
                if (instrStepDesc != null)
                {
                    for (int i = 0; i < instrStepDesc.Count; i++)
                    {
                        if (insrStepPhotos.ToList()[i] != null)
                        {
                            var tempImageMimeType = insrStepPhotos.ToList()[i].ContentType;
                            var tempImageData = new byte[insrStepPhotos.ToList()[i].ContentLength];
                            insrStepPhotos.ToList()[i].InputStream.Read(tempImageData, 0,
                            insrStepPhotos.ToList()[i].ContentLength);
                            recipe.InstructionSteps.Add(new InstructionStep()
                            {
                                Description = instrStepDesc.ToList()[i],
                                ImageMimeType = tempImageMimeType,
                                ImageData = tempImageData
                            });
                        }
                        else
                        {
                            recipe.InstructionSteps.Add(new InstructionStep()
                            {
                                Description = instrStepDesc.ToList()[i]
                            });
                        }
                    }
                }

                //Устанавливаем автора рецепта
                //recipe.AppUser = UserManager.FindById(User.Identity.GetUserId());
                recipe.AppUserId = User.Identity.GetUserId();
                repository.SaveRecipe(recipe);

                TempData["message"] = string.Format("Изменения в рецепте \"{0}\" были сохранены", recipe.Name);
                return RedirectToAction("Index");
            }
            else
            {
                // Что-то не так со значениями данных
                return View(recipeViewModel);
            }
        }

        public ViewResult Create()
        {
            RecipeViewModel recipeViewModel = new RecipeViewModel();
            recipeViewModel.Recipe = new Recipe();
            return View("Edit", recipeViewModel);
        }

        [HttpPost]
        public ActionResult Delete(int recipeId)
        {
            Recipe deletedRecipe = repository.DeleteRecipe(recipeId);
            if (deletedRecipe != null)
            {
                TempData["message"] = string.Format("Рецепт \"{0}\" был удален",
                    deletedRecipe.Name);
            }
            return RedirectToAction("Index");
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}