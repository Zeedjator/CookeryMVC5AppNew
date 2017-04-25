//using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cookery.WebUI.Models.Entities
{
    public class InstructionStep
    {
        [Key]
        [Column("StepID")]
        public int StepID { get; set; }
        public string Description { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        [Column("RecipeID")]
        public int RecipeID { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
