using FluentAssertions;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ResftulApiPlayground.Entities;
using RestfulApiPlayground.Infrastructure.Presentation;
using RestfulApiPlayground.src.Application.Contracts;
using RestfulApiPlayground.src.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RestfulApiPlaygroundTests
{
    public class RecipeEndpointTests
    {
        private RecipesController _repo;

        [Fact]
        public void GetRecipeById_WithExistingRecipe_ReturnsRecipe()
        {
            // Arrange
            Recipe recipe = new()
            {
                Id = 1,
                Name = "GetRecipeByIdReturnsRecipeWhenExists",
                Description = "Returns recipe when it exists",
                Ingredients = "One recipe",
                IncludedAt = DateTime.Now.AddDays(-10)
            };

            var hashids = new Hashids("testando");
            var repository = new Mock<IRecipeRepository>();
            repository.Setup(repo => repo.GetById(It.IsAny<int>())).Returns(recipe);

            _repo = new RecipesController(repository.Object);
            var hashid = IdScrambler.Encode(recipe.Id);

            OkObjectResult result = new(recipe);

            // Act
            var response = (OkObjectResult) _repo.GetRecipeById(hashid);

            // Assert
            response.StatusCode.Should().Be((int) HttpStatusCode.OK);
            response.Value.Should().Be(recipe);
        }
    }
}
