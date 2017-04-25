using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Cookery.WebUI.Models.Identity;

namespace Cookery.WebUI.Models.Entities
{
    public class Recipe
    {

        public Recipe()
        {
            this.InstructionSteps = new HashSet<InstructionStep>();
            this.Ingredients = new HashSet<Ingredient>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


        public string AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }

        public virtual ICollection<InstructionStep> InstructionSteps { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }


    }
}
