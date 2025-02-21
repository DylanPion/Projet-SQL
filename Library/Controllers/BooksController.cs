using Library.Data;
using Library.Models;
using Library.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BooksService _booksService;
        private readonly ILogger<BooksController> _logger;

        public BooksController(BooksService booksService, ILogger<BooksController> logger)
        {
            _booksService = booksService;
            _logger = logger;
        }

        /// <summary>
        /// Récupérer un livre par son id sur /api/books/{id}
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetBook(int id)
        {
            try
            {
                _logger.LogInformation($"Getting book with id {id}");
                var book = _booksService.GetBookById(id);
                return Ok(book);
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                _logger.LogWarning($"Book with id {id} not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting book with id {id}: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Ajouter un nouveau livre sur /api/books
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult AddBook([FromBody] Books book)
        {
            try
            {
                if (book == null)
                {
                    _logger.LogWarning("Null book object received");
                    return BadRequest("Book object is null");
                }

                _logger.LogInformation("Adding new book");
                var addedBook = _booksService.AddBook(book);
                return CreatedAtAction(nameof(GetBook), new { id = addedBook.Id }, addedBook);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding book: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Supprimer un livre sur /api/books/{id}
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteBook(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting book with id {id}");
                _booksService.DeleteBook(id);
                return NoContent();
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                _logger.LogWarning($"Book with id {id} not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting book with id {id}: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
