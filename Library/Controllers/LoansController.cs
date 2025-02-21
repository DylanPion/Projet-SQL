using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Library.Service;
using Library.Models;
using System;
using System.Collections.Generic;

namespace Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly LoansService _loansService;
        private readonly ILogger<LoansController> _logger;

        public LoansController(LoansService loansService, ILogger<LoansController> logger)
        {
            _loansService = loansService;
            _logger = logger;
        }

        /// <summary>
        /// Enregistrer un emprunt sur /api/loans
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateLoan([FromBody] Loans loan)
        {
            try
            {
                if (loan == null)
                {
                    _logger.LogWarning("Null loan object received");
                    return BadRequest("Loan object is null");
                }

                _logger.LogInformation("Adding new loan");
                var addedLoan = _loansService.AddLoan(loan);
                return CreatedAtAction(nameof(GetLoans), new { id = addedLoan.Id }, addedLoan);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding loan: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Récupérer tous les emprunts sur /api/loans
        /// (possibilité de filtrer par livre, emprunter ou date)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetLoans([FromQuery] int? bookId, [FromQuery] int? userId, [FromQuery] DateTime? startDate)
        {
            try
            {
                _logger.LogInformation("Getting all loans with filters");
                var filters = new Dictionary<string, string>();

                if (bookId.HasValue)
                    filters.Add("book", bookId.ToString());
                if (userId.HasValue)
                    filters.Add("member", userId.ToString());
                if (startDate.HasValue)
                    filters.Add("loanDate", startDate.Value.ToString("yyyy-MM-dd"));

                var loans = _loansService.GetAllLoans(filters);
                return Ok(loans);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error getting loans: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Enregistrer la date de retour d'un emprunt 
        /// </summary>
        [HttpPost("return/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult ReturnLoan(int id)
        {
            try
            {
                _logger.LogInformation($"Saving return date for loan {id}");
                _loansService.SaveReturnDate(id);
                return Ok();
            }
            catch (Exception ex) when (ex.Message.Contains("not found"))
            {
                _logger.LogWarning($"Loan with id {id} not found");
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error saving return date: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
    }
}