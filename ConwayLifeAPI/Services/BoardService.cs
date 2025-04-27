using ConwayLifeAPI.Data.Context;
using ConwayLifeAPI.Data.Repository;
using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;
using ConwayLifeAPI.Services.Interfaces;
using Newtonsoft.Json;

namespace ConwayLifeAPI.Services
{
    public class BoardService : IBoardService
    {
        private readonly IBoardRepository _boardRepository;

        public BoardService(IBoardRepository boardRepository)
        {
            _boardRepository = boardRepository;
        }

        public async Task<Board> CreateBoardAsync(CreateBoardDto dto, CancellationToken cancellationToken)
        {
            var board = new Board 
            { 
                StateJson = JsonConvert.SerializeObject(dto.Cells),
                Generation = 0,
                CreatedAt = DateTime.UtcNow
            };

            return await _boardRepository.AddBoardAsync(board, cancellationToken);
        }
        public async Task<bool[][]> GetBoardAsync(Guid boardId, CancellationToken cancellationToken)
        {
            return await _boardRepository.GetBoardByIdAsync(boardId, cancellationToken);
        }
        public async Task<bool[][]> GetNextStateAsync(Guid boardId, CancellationToken cancellationToken)
        {
            return await _boardRepository.GetNextStateAsync(boardId, cancellationToken);
        }
        public async Task<bool[][]> GetStateAheadAsync(Guid boardId, int steps, CancellationToken cancellationToken)
        {
            return await _boardRepository.GetStateAheadAsync(boardId, steps, cancellationToken);
        }

        
    }
}
