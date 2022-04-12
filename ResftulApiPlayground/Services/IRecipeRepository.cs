﻿using ResftulApiPlayground.Entities;

namespace ResftulApiPlayground.Service
{
    public interface IRecipeRepository
    {
        Recipe GetById(int id);
        int InsertRecipe(Recipe recipe);
        Recipe DeleteById(int id);
        Recipe Update(Recipe newRecipe);
    }
}