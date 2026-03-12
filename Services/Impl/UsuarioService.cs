using System;
using System.Threading.Tasks;
using AutoMapper;
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

        public async Task<UsuarioResponse> AddUsuarioAsync(CreateUsuarioRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            // business rule: email must be unique
            // var exists = await _repository.EmailExistsAsync(request.Email);
            // if (exists)
            // {
            //     throw new InvalidOperationException("Email já está em uso");
            // }

            // map DTO to domain model
            var usuario = _mapper.Map<Usuario>(request);

            var added = await _repository.AddUsuarioAsync(usuario);

            // map back to response DTO
            var response = _mapper.Map<UsuarioResponse>(added);
            return response;
        }


        public async Task<UsuarioResponse> UpdateUsuarioAsync(int id, UpdateUsuarioRequest request)
        {   
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var usuarioAtual = await _repository.FindByIdAsync(id);
            if (usuarioAtual == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado");
            }

            _mapper.Map(request, usuarioAtual);

            var updated = await _repository.UpdateUsuarioAsync(usuarioAtual);
    

            // business rule: email must be unique
            // var exists = await _repository.EmailExistsAsync(request.Email);
            // if (exists)
            // {
            //     throw new InvalidOperationException("Email já está em uso");
            // }

            // map DTO to domain model

            // map back to response DTO
            var response = _mapper.Map<UsuarioResponse>(updated);
            return response;

        }

        public async Task<bool> DeleteUsuarioAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado");
            }
            return await _repository.DeleteUsuarioAsync(usuario);
        }

        public async Task<UsuarioResponse> FindByIdAsync(int id)
        {
            var usuario = await _repository.FindByIdAsync(id);
            if (usuario == null)
            {
                throw new KeyNotFoundException("Usuario não encontrado");
            }

            var response = _mapper.Map<UsuarioResponse>(usuario);
            return response;
        }

        public async Task<IEnumerable<UsuarioResponse>> GetUsuariosAsync()
        {
            var usuarios = await _repository.GetUsuariosAsync();
            var response = _mapper.Map<IEnumerable<UsuarioResponse>>(usuarios);
            return response;
        }
    }
}