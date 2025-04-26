using ConwayLifeAPI.Data;
using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;
using ConwayLifeAPI.Services.Interfaces;
using Newtonsoft.Json;

namespace ConwayLifeAPI.Services
{
    public class BoardService : IBoardService
    {
        private readonly AppDbContext _context;

        public BoardService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Board> CreateBoardAsync(CreateBoardDto dto)
        {
            var board = new Board 
            { 
                StateJson = JsonConvert.SerializeObject(dto.Cells),
                Generation = 0,
                CreatedAt = DateTime.UtcNow
            };

            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return board;
        }
    }
}
