using ResftulApiPlayground.Entities;
using ResftulApiPlayground.Exceptions;
using System.Collections.Generic;

namespace ResftulApiPlayground.Service;

public class RecipeDictionaryRepository : IRecipeRepository
{
    private Dictionary<int, Recipe> _recipes { get; set; }

    public RecipeDictionaryRepository()
    {
        _recipes = new Dictionary<int, Recipe>();
    }

    public Recipe GetById(int id)
    {
        Recipe recipe;
        if (_recipes.TryGetValue(id, out recipe))
            return recipe;
        else
            throw new IdNotFoundError();
    }

    public int Insert(Recipe recipe)
    {
        var newId = _recipes.Count + 1;

        recipe.Id = newId;

        _recipes.Add(newId, recipe);
        return newId;
    }

    public Recipe DeleteById(int id)
    {
        Recipe removedRecipe;
        if (_recipes.TryGetValue(id, out removedRecipe))
            _recipes.Remove(id);
        else
            throw new IdNotFoundError();

        return removedRecipe;
    }

    public Recipe Update(Recipe updatedRecipe)
    {
        Recipe oldRecipe;
        if (_recipes.TryGetValue(updatedRecipe.Id, out oldRecipe))
            _recipes.Remove(updatedRecipe.Id);
        else
            throw new IdNotFoundError();

        _recipes.Add(updatedRecipe.Id, updatedRecipe);

        return oldRecipe;
    }
}
