using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Service;
using Library.Models;
using System;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoriesService categoriesService, ILogger<CategoriesController> logger)
        {
            _categoriesService = categoriesService;
            _logger = logger;
        }

        /// <summary>
        /// Récupérer toutes les catégories sur /api/categories
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetCategories()
        {
            try
            {
                _logger.LogInformation("Getting all categories");
                var categories = _categoriesService.GetAllCategories();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting categories: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Ajouter une catégorie sur /api/categories
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddCategory([FromBody] Categories category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogWarning("Null category object received");
                    return BadRequest("Category object is null");
                }

                _logger.LogInformation("Adding new category");
                var addedCategory = _categoriesService.AddCategory(category);
                return CreatedAtAction(nameof(GetCategories), new { id = addedCategory.Id }, addedCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding category: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Supprimer une catégorie sur /api/categories/{id}
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting category with id {id}");
                _categoriesService.DeleteCategory(id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                _logger.LogWarning($"Category with id {id} not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting category with id {id}: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}