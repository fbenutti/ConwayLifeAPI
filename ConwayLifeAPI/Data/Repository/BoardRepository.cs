using ConwayLifeAPI.Data.Context;
using ConwayLifeAPI.Domain;
using ConwayLifeAPI.Models;
using Newtonsoft.Json;

namespace ConwayLifeAPI.Data.Repository
{
    public class BoardRepository : IBoardRepository
    {
        private GameOfLife _gameOfLife;

        private readonly AppDbContext _context;
        public BoardRepository(AppDbContext context)
        {
            _context = context;
            _gameOfLife = new GameOfLife();
        }
        public async Task<bool[][]> GetBoardByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync(id, cancellationToken);

            if (board == null)
                throw new Exception("Board not found");

            var jsonBoard = JsonConvert.DeserializeObject<bool[][]>(board.StateJson);

            return jsonBoard;
        }
        public async Task<Board> AddBoardAsync(Board board, CancellationToken cancellationToken)
        {
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return board;
        }
        public async Task<bool[][]> GetNextStateAsync(Guid boardId, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync(boardId, cancellationToken);

            if (board == null)
                throw new Exception("Board not found");
            

            var currentState = JsonConvert.DeserializeObject<bool[][]>(board.StateJson);
            var nextState = _gameOfLife.CalculateNextState(currentState);

            return nextState;
        }
        public async Task<bool[][]> GetStateAheadAsync(Guid boardId, int steps, CancellationToken cancellationToken)
        {
            if (steps < 1)
                throw new ArgumentException("Steps must be greater than 0");

            var board = await _context.Boards.FindAsync(boardId, cancellationToken);

            if (board == null)
            {
                throw new Exception("Board not found");
            }

            var currentState = JsonConvert.DeserializeObject<bool[][]>(board.StateJson);

            for (int i = 0; i < steps; i++)
            {
                var nextState = _gameOfLife.CalculateNextState(currentState!);

                if (_gameOfLife.AreBoardsEqual(currentState!, nextState))
                {
                    // Board stabilized, no need to continue
                    return nextState;
                }

                currentState = nextState;
            }

            return currentState!;
        }
        public async Task UpdateBoardAsync(Board board, CancellationToken cancellationToken)
        {
            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

    }
}
