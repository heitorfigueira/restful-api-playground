using System;
using System.ComponentModel.DataAnnotations;

namespace RestfulApiPlayground.Models.DTO
{
    public class UpdateRecipePayload
    {
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
    }
}
