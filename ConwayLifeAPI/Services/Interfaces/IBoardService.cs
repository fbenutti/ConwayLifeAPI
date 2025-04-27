using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;

namespace ConwayLifeAPI.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoardAsync(CreateBoardDto dto, CancellationToken cancellationToken);
        Task<bool[][]> GetNextStateAsync(Guid boardId, CancellationToken cancellationToken);
    }
}
