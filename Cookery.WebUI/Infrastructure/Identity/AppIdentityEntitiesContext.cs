using System.Data.Entity;
using Cookery.WebUI.Models.Entities;
using Cookery.WebUI.Models.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cookery.WebUI.Infrastructure.Identity
{
    public class AppIdentityEntitiesContext : IdentityDbContext<AppUser>
    {
        public AppIdentityEntitiesContext() : base("name=IdentityEntities") { }

        static AppIdentityEntitiesContext()
        {
            Database.SetInitializer<AppIdentityEntitiesContext>(new IdentityEntitiesInit());
        }

        public static AppIdentityEntitiesContext Create()
        {
            return new AppIdentityEntitiesContext();
        }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<InstructionStep> InstructionSteps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Recipe>()
               .HasMany(r => r.Ingredients)
               .WithMany(i => i.Recipes)
               .Map(mc =>
               {
                   mc.MapLeftKey("FkRecipesId");
                   mc.MapRightKey("FkIngredientsId");
                   mc.ToTable("RecipesIngredients");
               });

            modelBuilder.Entity<Recipe>()
                    .HasOptional<AppUser>(s => s.AppUser)
                    .WithMany(s => s.Recipes)
                    .HasForeignKey(s => s.AppUserId);
        }
    }

    public class IdentityEntitiesInit : NullDatabaseInitializer<AppIdentityEntitiesContext>
    { }
}