using DevLearning.AuthorAPI.Repositories;
using DevLearning.AuthorAPI.Repositories.Interfaces;
using DevLearning.AuthorAPI.Services.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Author;
using Domain.Models.Enums.Author;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DevLearning.AuthorAPI.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ILogger<AuthorService> _logger;
        private readonly AuthorRepository _authorRepository;
        public AuthorService(AuthorRepository authorRepository, ILogger<AuthorService> logger)
        {
            _authorRepository = authorRepository;
            _logger = logger;
        }
        public async Task<List<AuthorResponseDTO>> GetAllAuthorsAsync()
        {
            try
            {
                return await _authorRepository.GetAllAuthorsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<AuthorResponseDTO> GetAuthorByIdAsync(Guid id)
        {
            try
            {
                return await _authorRepository.GetAuthorByIdAsync(id);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreateAuthorAsync(AuthorRequestDTO author)
        {
            try
            {
                var findAuthor = await _authorRepository.GetAuthorByEmail(author.Email);
                if (findAuthor is null)
                {
                    var newAuthor = new Author(author.Name, author.Title, author.Image, author.Bio, author.Email);
                    await _authorRepository.CreateAuthorAsync(newAuthor);

                }
                else
                {
                    throw new Exception("O autor já existe.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task UpdatePatchAuthorAsync(Guid id, UpdateAuthorParcialDTO dto)
        {
            try
            {
                await _authorRepository.UpdatePatchAuthorAsync(dto, id);
            }
            catch( Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task UpdatePutAuthorAsync(Guid id, UpdateAuthorFullDTO dto)
        {
            try
            {
                await _authorRepository.UpdatePutAuthorAsync(dto, id);
            }
            catch( Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateAuthorTypeAsync(Guid id, AuthorType newType)
        {
            try
            {
                // valida enum
                if (!Enum.IsDefined(typeof(AuthorType), newType))
                    throw new ArgumentOutOfRangeException(nameof(newType), "Tipo inválido. Use 1 ou 2.");

                // regra: se inativo (2), precisa verificar cursos
                if (newType == AuthorType.Inativo)
                {
                    var count = await _authorRepository.CountCoursesAsync(id);
                    if (count > 0)
                        throw new InvalidOperationException("Não é possível inativar o autor pois ele possui cursos.");
                }

                await _authorRepository.UpdateAuthorTypeAsync(id, newType);
            }
            catch( Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }


        public async Task<AuthorWithCoursesDTO> GetAuthorCoursesAsync(Guid authorId)
        {
            try
            {
                var (authorName, courses) = await _authorRepository.GetAuthorCoursesAsync(authorId);

                if (authorName == null)
                    return null;

                return new AuthorWithCoursesDTO
                {
                    AuthorName = authorName,
                    Courses = courses
                };
            }
            catch( Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}