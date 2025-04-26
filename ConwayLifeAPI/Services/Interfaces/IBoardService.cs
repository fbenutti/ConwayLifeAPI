using ConwayLifeAPI.DTOs;
using ConwayLifeAPI.Models;

namespace ConwayLifeAPI.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> CreateBoardAsync(CreateBoardDto dto);
    }
}
