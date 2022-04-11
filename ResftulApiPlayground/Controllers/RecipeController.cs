using HashidsNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResftulApiPlayground.Entities;
using ResftulApiPlayground.Exceptions;
using ResftulApiPlayground.Models.DTO;
using ResftulApiPlayground.Service;
using System;

namespace ResftulApiPlayground.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipeController : ControllerBase
{
    private readonly IHashids _hashids;
    private readonly RecipeRepository _repo;

    public RecipeController(IHashids hashids, RecipeRepository repo)
    {
        _hashids = hashids;
        _repo = repo;
    }

    [HttpGet]
    [Route("{hashid}")]
    public IActionResult GetRecipeById(string hashid)
    {
        try
        {
            int id = Decode(hashid);
                
            Recipe recipe = _repo.GetById(id);

            return Ok(recipe);

        } catch (InvalidArgumentException iaex)
        {
            return BadRequest("Argumento inválido, por favor forneça um id válido.");

            // TODO: Logar a exception
        } catch (ArgumentNullException anex)
        {
            return BadRequest("Argumento nulo, por favor forneça um id válido.");

            // TODO: Logar a exception
        } catch (IdNotFoundException infex)
        {
            return NotFound("Nenhuma receita foi encontrada com esse id.");
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
                DateIncluded = DateTime.Now
            };

            int id = _repo.InsertRecipe(recipe);
            string hashid = Encode(id);

            return Ok(hashid);
        }
        catch (InvalidArgumentException iaex)
        {
            return BadRequest("Argumento inválido, por favor forneça um id válido.");

            // TODO: Logar a exception
        }
        catch (ArgumentNullException anex)
        {
            return BadRequest("Argumento nulo, por favor forneça um id válido.");

            // TODO: Logar a exception
        }
        catch (IdNotFoundException infex)
        {
            return NotFound("Nenhuma receita foi encontrada com esse id.");
        }
    }

    [HttpDelete]
    public IActionResult DeleteById(string hashid)
    {
        try
        {
            int id = Decode(hashid);

            Recipe recipe = _repo.DeleteById(id);

            return Ok(recipe);

        }
        catch (InvalidArgumentException iaex)
        {
            return BadRequest("Argumento inválido, por favor forneça um id válido.");

            // TODO: Logar a exception
        }
        catch (ArgumentNullException anex)
        {
            return BadRequest("Argumento nulo, por favor forneça um id válido.");

            // TODO: Logar a exception
        }
        catch (IdNotFoundException infex)
        {
            return NotFound("Nenhuma receita foi encontrada com esse id.");
        }
    }


    private int Decode(string hashid)
    {
        if (string.IsNullOrEmpty(hashid))
            throw new ArgumentNullException();

        int id;
        if (_hashids.TryDecodeSingle(hashid, out id))
            return id;
        else
            throw new InvalidArgumentException();
    }

    private string Encode(int id)
    {
        if (id == 0)
            throw new InvalidArgumentException();

        return _hashids.Encode(id);
    }
}
