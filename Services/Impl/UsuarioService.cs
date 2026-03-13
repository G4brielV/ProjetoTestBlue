using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using ProjetoTestBlue.DTOs;
using ProjetoTestBlue.Models;
using ProjetoTestBlue.Repository;

namespace ProjetoTestBlue.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IMapper _mapper;

        public UsuarioService(IUsuarioRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<UsuarioResponse>> GetByIdAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);
            if (usuario != null)
            {
                var response = _mapper.Map<UsuarioResponse>(usuario);
                return Result<UsuarioResponse>.Success(response);
            }
            else
            {
                return Result<UsuarioResponse>.Failure("Usuario não encontrado."); 
            }
        }

        public async Task<Result<IEnumerable<UsuarioResponse>>> GetUsuariosAsync()
        {
            var usuarios = await _repository.GetUsuariosAsync();
            var response = _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
            return Result<IEnumerable<UsuarioResponse>>.Success(response);
        }

        public async Task<Result<UsuarioResponse>> AddUsuarioAsync(CreateUsuarioRequest request)
        {
            // business rule: email must be unique
            if (await _repository.EmailExistsAsync(request.Email))
            {
                return Result<UsuarioResponse>.Failure("Este e-mail já está em uso."); 
            }

            // map DTO to domain model
            var usuario = _mapper.Map<Usuario>(request);
            await _repository.AddUsuarioAsync(usuario);

            // map back to response DTO
            var response = _mapper.Map<UsuarioResponse>(usuario);
            return Result<UsuarioResponse>.Success(response);
        }


        public async Task<Result<UsuarioResponse>> UpdateUsuarioAsync(int id, UpdateUsuarioRequest request)
        {   
            Usuario usuarioAtual = await _repository.FindByIdAsync(id);
            if (usuarioAtual == null)
            {
                return Result<UsuarioResponse>.Failure("Usuario não encontrado"); 
            }

            _mapper.Map(request, usuarioAtual);

            var updated = await _repository.UpdateUsuarioAsync(usuarioAtual);

            // map back to response DTO
            var response = _mapper.Map<UsuarioResponse>(updated);
            return Result<UsuarioResponse>.Success(response);

        }

        public async Task<Result<bool>> DeleteUsuarioAsync(int id)
        {
            Usuario usuario = await _repository.FindByIdAsync(id);
            if (usuario == null)
            {
                return Result<bool>.Failure("Usuario não encontrado"); 
            }
            bool deleted = await _repository.DeleteUsuarioAsync(usuario);
            return Result<bool>.Success(true);
        }
    }
}