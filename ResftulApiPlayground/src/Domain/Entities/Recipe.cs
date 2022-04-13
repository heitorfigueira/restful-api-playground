using System;
using System.ComponentModel.DataAnnotations;

namespace ResftulApiPlayground.Entities
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public DateTime IncludedAt { get; set; }
    }
}
