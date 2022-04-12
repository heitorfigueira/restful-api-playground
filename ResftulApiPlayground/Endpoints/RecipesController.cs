using HashidsNet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResftulApiPlayground.Entities;
using ResftulApiPlayground.Exceptions;
using ResftulApiPlayground.Models.DTO;
using ResftulApiPlayground.Service;
using RestfulApiPlayground.Models.DTO;
using System;

namespace ResftulApiPlayground.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    public readonly IHashids hashids;
    public readonly IRecipeRepository repo;

    public RecipesController(IHashids hashids, IRecipeRepository repo)
    {
        this.hashids = hashids;
        this.repo = repo;
    }

    [HttpGet]
    [Route("{hashid}")]
    public IActionResult GetRecipeById(string hashid)
    {
        try
        {
            // TODO: Retirar essa lógica de negócio da camada de apresentação da aplicação!!!!
            int id = Decode(hashid);
                
            Recipe recipe = repo.GetById(id);

            return Ok(recipe);
            
            // TODO: Também é lógica de negócio, mas é melhor encapsular essas mensagens nas próprias exceções
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
            // TODO: Retirar essa lógica de negócio da camada de apresentação da aplicação!!!!
            // TODO: Criar mapeador
            Recipe recipe = new()
            {
                Name = payload.Name,
                Description = payload.Description,
                Ingredients = payload.Ingredients,
                IncludedAt = DateTime.Now
            };

            int id = repo.InsertRecipe(recipe);
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

            Recipe recipe = repo.DeleteById(id);

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

    [HttpPut]
    public IActionResult UpdateRecipe([FromBody] UpdateRecipePayload payload)
    {
        try
        {



        } catch (Exception ex)
        {

        }
    }


    private int Decode(string hashid)
    {
        if (string.IsNullOrEmpty(hashid))
            throw new ArgumentNullException();

        int id;
        if (hashids.TryDecodeSingle(hashid, out id))
            return id;
        else
            throw new InvalidArgumentException();
    }

    private string Encode(int id)
    {
        if (id == 0)
            throw new InvalidArgumentException();

        return hashids.Encode(id);
    }
}
