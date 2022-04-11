using System;

namespace ResftulApiPlayground.Models.DTO
{
    public class GetRecipeResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public DateTime DateIncluded { get; set; }
    }
}
