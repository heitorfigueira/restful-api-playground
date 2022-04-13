using ResftulApiPlayground.Entities;

namespace RestfulApiPlayground.src.Application.Contracts
{
    public interface IRecipeRepository
    {
        Recipe GetById(int id);
        int Insert(Recipe recipe);
        Recipe DeleteById(int id);
        Recipe Update(Recipe newRecipe);
    }
}