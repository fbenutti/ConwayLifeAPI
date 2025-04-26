using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConwayLifeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService _boardService;

        [HttpPost]
        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardDto dto)
        {
            if (dto.)
            {
                
            }
        }
    }
}
