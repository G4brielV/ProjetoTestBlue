using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;

namespace ProjetoTestBlue.Services
{
    public interface ICardService
    {
        /* Paginação */
        Task<Result<CardResponse>> CriarCardAsync(CardRequest request, int userId);
        Task<Result<CardResponse>> UpdateCardAsync(int id, CardRequest request, int userId);
        Task<Result<CardResponse>> ToggleCardStatusAsync(int id, int userId);
        Task<Result<bool>> DeleteCardAsync(int id, int userId);

        /* TODO:
        Reorder 
        */
    }
}