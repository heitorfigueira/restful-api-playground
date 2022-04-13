using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using ResftulApiPlayground.Entities;
using ResftulApiPlayground.Exceptions;
using ResftulApiPlayground.Models.DTO;
using RestfulApiPlayground.Infrastructure.Presentation;
using RestfulApiPlayground.Infrastructure.Presentation.Errors;
using RestfulApiPlayground.Models.DTO;
using RestfulApiPlayground.src.Application.Contracts;
using System;

namespace RestfulApiPlayground.src.Presentation;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    public readonly IRecipeRepository repo;

    public RecipesController(IRecipeRepository repo)
    {
        this.repo = repo;
    }

    [HttpGet]
    [Route("{hashid}")]
    public IActionResult GetRecipeById(string hashid)
    {
        try
        {
            int id = IdScrambler.Decode(hashid);

            Recipe recipe = repo.GetById(id);

            return Ok(recipe);

        }
        catch (ErrorException ex)
        {
            return ex.Callback();
        }
    }

    [HttpPost]
    public IActionResult InsertRecipe([FromBody] InsertRecipePayload payload)
    {
        try
        {
            // TODO: Criar mapeador
            Recipe recipe = new()
            {
                Name = payload.Name,
                Description = payload.Description,
                Ingredients = payload.Ingredients,
                IncludedAt = DateTime.Now
            };

            int id = repo.Insert(recipe);
            string hashid = IdScrambler.Encode(id);

            return Ok(hashid);
        }
        catch (ErrorException ex)
        {
            return ex.Callback();
        }
    }

    [HttpDelete]
    public IActionResult DeleteById(string hashid)
    {
        try
        {
            int id = IdScrambler.Decode(hashid);

            Recipe recipe = repo.DeleteById(id);

            return Ok(recipe);

        }
        catch (ErrorException ex)
        {
            return ex.Callback();
        }
    }

    [HttpPut]
    public IActionResult UpdateRecipe([FromBody] UpdateRecipePayload payload)
    {
        try
        {
            Recipe recipe = new()
            {
                Id = IdScrambler.Decode(payload.Id),
                Name = payload.Name,
                Description = payload.Description,
                Ingredients = payload.Ingredients
            };

            Recipe oldRecipe = repo.Update(recipe);

            return Ok(oldRecipe);
        }
        catch (ErrorException ex)
        {
            return ex.Callback();
        }
    }
}
