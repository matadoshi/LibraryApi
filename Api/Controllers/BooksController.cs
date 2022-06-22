using AutoMapper;
using DomainModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Dtos;
using Repository.Repository.Abstraction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IRepository<Book> _repository;
        private readonly IMapper _mapper;
        public BooksController(IRepository<Book> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBook()
        {
            var book = await _repository.GetAllAsync();
            return Ok(_mapper.Map<List<BookDto>>(book));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            var book = await _repository.GetAsync(id);
            if (book == null) return NotFound("There is not book with this id");
            return Ok(_mapper.Map<BookDto>(book));
        }
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            var result = await _repository.AddAsync(book);
            if (!result) return BadRequest("Something bad happenes");
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook([FromRoute] int id)
        {
            var book = await _repository.GetAsync(id);
            if (book == null) return NotFound("This book doesnt exitst");
            bool result = await _repository.DeleteAsync(book);
            if (!result) return BadRequest("Something bad happenes");
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromRoute] int id, [FromBody] BookDto bookDto)
        {
            Book book = await _repository.GetAsync(id);
            if (book == null) return NotFound("Kitap Tapilmadi");
            book.Name = bookDto.Name;
            book.Genre = bookDto.Genre;
            book.Author = bookDto.Author;
            bool result = _repository.UpdateAsync(book);
            if (!result) return BadRequest("Something Bad Happend");
            return StatusCode(StatusCodes.Status200OK);
        }
        [Route("[action]/{author}")]
        [HttpGet]
        public async Task<IActionResult> GetByAuthor([FromRoute] string author)
        {
            var BookList = await _repository.GetByAuthorAsync(author);
            if (BookList.Count==0) return NotFound("There is not book with by author");
            return Ok(_mapper.Map<List<BookDto>>(BookList));
        }
        [Route("[action]/{genre}")]
        [HttpGet]
        public async Task<IActionResult> GetByGenre([FromRoute] string genre)
        {
            var BookList = await _repository.GetByGenreAsync(genre);
            if (BookList.Count==0) return NotFound("There is not book with by genre");
            return Ok(_mapper.Map<List<BookDto>>(BookList));

        }
    }
}
