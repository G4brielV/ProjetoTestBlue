using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProjetoTestBlue.DTOs.Response;
using ProjetoTestBlue.DTOs.Request;
using ProjetoTestBlue.Services;

namespace ProjetoTestBlue.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public async Task<ActionResult<CardResponse>> CreateCard(CardRequest request)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _cardService.CriarCardAsync(request, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return CreatedAtAction(nameof(CreateCard), result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCard(int id, CardRequest request)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _cardService.UpdateCardAsync(id, request, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpPatch("{id}/toggle")]
        public async Task<IActionResult> ToggleCardStatus(int id)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _cardService.ToggleCardStatusAsync(id, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int id)
        {
            var userIdClaim = User.FindFirst("id");
            if (userIdClaim == null)
            {
                return Unauthorized();
            }
            int userId = int.Parse(userIdClaim.Value);
            var result = await _cardService.DeleteCardAsync(id, userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }
            return NoContent();
        }
    }
}