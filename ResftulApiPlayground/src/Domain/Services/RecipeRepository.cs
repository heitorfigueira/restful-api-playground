using ResftulApiPlayground.Entities;
using ResftulApiPlayground.Exceptions;
using RestfulApiPlayground.src.Application.Contracts;
using System.Collections.Generic;

namespace ResftulApiPlayground.Service;

public class RecipeDictionaryRepository : IRecipeRepository
{
    private RecipeDictionary  _recipes;
    public RecipeDictionaryRepository()
    {
        _recipes = new RecipeDictionary();
    }

    public Recipe DeleteById(int id)
    {
        return _recipes.DeleteById(id);
    }

    public Recipe GetById(int id)
    {
        return _recipes.GetById(id);
    }

    public int Insert(Recipe recipe)
    {
        return _recipes.Insert(recipe);
    }

    public Recipe Update(Recipe newRecipe)
    {
        return _recipes.Update(newRecipe);
    }
}
