
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Repository
{
    public interface ICardRepository
    {
        Task<IEnumerable<Card>> GetCardsFromListAsync(int listId);
        Task<Card?> FindByIdAsync(int id);
        Task CriarCardAsync(Card card);
        Task<Card> UpdateCardAsync(Card cardNovo);
        Task<bool> DeleteCardAsync(Card card);

        // Patch Status
        // Reorder
    }
}