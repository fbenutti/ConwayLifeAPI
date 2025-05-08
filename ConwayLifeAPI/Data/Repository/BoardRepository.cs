using ConwayLifeAPI.Data.Context;
using ConwayLifeAPI.Domain;
using ConwayLifeAPI.Models;
using Newtonsoft.Json;
using System.Threading;

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

            return jsonBoard!;
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
        public async Task<bool[][]> GetStateAheadAsync(Guid boardId, int steps, bool persistNewBoard, CancellationToken cancellationToken)
        {
            _ = _gameOfLife.checkSteps(steps);

            var board = await _context.Boards.FindAsync(boardId, cancellationToken);

            return await ReturnBoardStateAfterIteration(board!, steps, persistNewBoard, cancellationToken);

        }
        public async Task UpdateBoardAsync(Board board, CancellationToken cancellationToken)
        {
            _context.Boards.Update(board);
            await _context.SaveChangesAsync();
        }

        public async Task<bool[][]> GetFinalStateAsync(Guid boardId, bool persistNewBoard, CancellationToken cancellationToken)
        {
            var board = await _context.Boards.FindAsync(boardId, cancellationToken);

            return await ReturnBoardStateAfterIteration(board, null, persistNewBoard, cancellationToken);
        }

        private async Task<bool[][]> ReturnBoardStateAfterIteration(Board board, int? steps, bool persistNewBoard, CancellationToken cancellationToken)
        {
            if (board == null)
                throw new Exception("Board not found");

            int? maxSteps = steps;

            if(!steps.HasValue)
                maxSteps = GameOfLife.MAX_STEPS;

            var currentState = JsonConvert.DeserializeObject<bool[][]>(board.StateJson);

            for (int i = 0; i < maxSteps; i++)
            {
                var nextState = _gameOfLife.CalculateNextState(currentState!);

                if (_gameOfLife.AreBoardsEqual(currentState!, nextState))
                {
                    if (persistNewBoard)
                    {
                        board.StateJson = JsonConvert.SerializeObject(currentState!);
                        await UpdateBoardAsync(board, cancellationToken);
                    }
                    // Board stabilized, no need to continue
                    return nextState;
                }

                currentState = nextState;
            }

            if (!steps.HasValue)
                throw new Exception($"Board did not stabilize after {GameOfLife.MAX_STEPS} steps.");

            if (persistNewBoard)
            {
                board.StateJson = JsonConvert.SerializeObject(currentState!);
                await UpdateBoardAsync(board, cancellationToken);
            }

            return currentState!;
        }

    }
}
