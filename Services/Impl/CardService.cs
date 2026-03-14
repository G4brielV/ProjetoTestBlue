using AutoMapper;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.DTOs.Response;
using ProjetoTestBlue.Models;
using ProjetoTestBlue.Repository;

namespace ProjetoTestBlue.Services.Impl
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ITodoListRepository _todoListRepository;
        private readonly IMapper _mapper;

        public CardService(ICardRepository cardRepository, ITodoListRepository todoListRepository, IMapper mapper)
        {
            _cardRepository = cardRepository;
            _todoListRepository = todoListRepository;
            _mapper = mapper;
        }

        public async Task<Result<CardResponse>> CriarCardAsync(CardRequest request, int userId)
        {
            var todoList = await _todoListRepository.FindByIdAsync(request.ListId);
            if (todoList == null || todoList.UsuarioId != userId)
            {
                return Result<CardResponse>.Failure("List not found or access denied");
            }
            var card = _mapper.Map<Card>(request);
            await _cardRepository.CriarCardAsync(card);
            var response = _mapper.Map<CardResponse>(card);
            return Result<CardResponse>.Success(response);
        }

        public async Task<Result<bool>> DeleteCardAsync(int id, int userId)
        {
            var card = await _cardRepository.FindByIdAsync(id);
            if (card == null || card.TodoList.UsuarioId != userId)
            {
                return Result<bool>.Failure("Card not found or access denied");
            }
            await _cardRepository.DeleteCardAsync(card);
            return Result<bool>.Success(true);
        }

        public async Task<Result<CardResponse>> ToggleCardStatusAsync(int id, int userId)
        {
            var card = await _cardRepository.FindByIdAsync(id);
            if (card == null || card.TodoList.UsuarioId != userId)
            {
                return Result<CardResponse>.Failure("Card not found or access denied");
            }
            card.IsCompleted = !card.IsCompleted;
            await _cardRepository.UpdateCardAsync(card);
            var response = _mapper.Map<CardResponse>(card);
            return Result<CardResponse>.Success(response);
        }

        public async Task<Result<CardResponse>> UpdateCardAsync(int id, CardRequest request, int userId)
        {
            var card = await _cardRepository.FindByIdAsync(id);
            if (card == null || card.TodoList.UsuarioId != userId)
            {
                return Result<CardResponse>.Failure("Card not found or access denied");
            }
            _mapper.Map(request, card);
            await _cardRepository.UpdateCardAsync(card);
            var response = _mapper.Map<CardResponse>(card);
            return Result<CardResponse>.Success(response);
        }
    }
}