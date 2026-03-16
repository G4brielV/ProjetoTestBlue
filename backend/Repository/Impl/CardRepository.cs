using Microsoft.EntityFrameworkCore;
using ProjetoTestBlue.Data;
using ProjetoTestBlue.Models;

namespace ProjetoTestBlue.Repository.Impl
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarCardAsync(Card card)
        {
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCardAsync(Card card)
        {
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Card?> FindByIdAsync(int id)
        {
            return await _context.Cards.Include(c => c.TodoList).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Card>> GetCardsFromListAsync(int listId)
        {
            return await _context.Cards.Where(c => c.ListId == listId).ToListAsync();
        }

        public async Task<Card> UpdateCardAsync(Card cardNovo)
        {
            _context.Cards.Update(cardNovo);
            await _context.SaveChangesAsync();
            return cardNovo;
        }
    }
}