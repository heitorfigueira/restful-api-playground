using ResftulApiPlayground.Entities;
using ResftulApiPlayground.Exceptions;
using System.Collections.Generic;

namespace ResftulApiPlayground.Service;

public class RecipeRepository
{
    private Dictionary<int, Recipe> _recipes { get; set; }

    public RecipeRepository()
    {
        _recipes = new Dictionary<int, Recipe>();
    }

    public Recipe GetById(int id)
    {
        Recipe recipe;
        if (_recipes.TryGetValue(id, out recipe))
            return recipe;
        else
            throw new IdNotFoundException();
    }

    public int InsertRecipe(Recipe recipe)
    {
        var newId = _recipes.Count + 1;

        recipe.Id = newId;

        _recipes.Add(newId, recipe);
        return newId;
    }

    public Recipe DeleteById(int id)
    {
        Recipe recipe;
        if (_recipes.TryGetValue(id, out recipe))
            _recipes.Remove(id);
        else
            throw new IdNotFoundException();

        return recipe;
    }

    public Recipe Update(Recipe newRecipe)
    {
        Recipe recipe;
        if (_recipes.TryGetValue(newRecipe.Id, out recipe))
            _recipes.Remove(newRecipe.Id);
        else
            throw new IdNotFoundException();

        _recipes.Add(newRecipe.Id, newRecipe);

        return recipe;
    }
}
