using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Models;
using ProjetoTestBlue.Repository;
using AutoMapper;

namespace ProjetoTestBlue.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(IUsuarioRepository repository, ITokenService tokenService, IMapper mapper)
        {
            _repository = repository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<Result<CadastroResponse>> CadastroAsync(CadastroRequest request)
        {
            // business rule: email must be unique
            if (await _repository.EmailExistsAsync(request.Email))
            {
                return Result<CadastroResponse>.Failure("Este e-mail já está em uso."); 
            }

            // map DTO to domain model
            Usuario usuario = _mapper.Map<Usuario>(request);
            usuario.Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            await _repository.AddUsuarioAsync(usuario);

            // map back to response DTO
            var response = _mapper.Map<CadastroResponse>(usuario);
            return Result<CadastroResponse>.Success(response);
        }

        public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request)
        {   
            Usuario usuario = await _repository.FindByEmailAsync(request.Email);
            if (usuario == null) return Result<LoginResponse>.Failure("E-mail ou senha inválidos.");

            bool isValid = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha);
            if(!isValid) return Result<LoginResponse>.Failure("E-mail ou senha inválidos.");
            
            string token = _tokenService.GerarToken(usuario);
            return Result<LoginResponse>.Success(new LoginResponse(token));
        }
    }
}