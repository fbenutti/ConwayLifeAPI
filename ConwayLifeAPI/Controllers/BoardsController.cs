using Asp.Versioning;
using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConwayLifeAPI.Controllers
{
    /// <summary>
    /// Controller for managing boards in the Conway's Game of Life API.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        public BoardsController(IBoardService boardService)
        {
            _boardService = boardService;
        }

        [HttpPost("/upload-board")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardDto dto, CancellationToken cancellationToken)
        {
            try
            {
                if (dto == null)
                    return BadRequest("Request body must not be empty.");

                if (dto.Cells == null || dto.Cells.Length == 0)
                    return BadRequest("Board must not be empty.");

                var board = await _boardService.CreateBoardAsync(dto, cancellationToken);

                return Ok(new { board.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("/next-state/{id}")]
        public async Task<IActionResult> GetNextState(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var nextState = await _boardService.GetNextStateAsync(id, cancellationToken);
                return Ok(nextState);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
