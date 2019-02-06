using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Lab3.API.Models;
using Lab3.Domain.Models.Entities;
using Lab3.Domain.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.API.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<BookViewModel>> Get()
        {
            var books = await _bookService.GetAllBooks();
            var booksView= _mapper.Map<IEnumerable<BookViewModel>>(books);

            return booksView;
        }

        [HttpGet("{id}")]
        public async Task<BookViewModel> Get(int id)
        {
            var book = await _bookService.GetBookById(id);
            var bookView = _mapper.Map<BookViewModel>(book);

            return bookView;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Post([FromBody]BookViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                var book = _mapper.Map<BookModel>(bookView);
                await _bookService.AddBook(book);

                return Ok(book);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody]BookViewModel bookView)
        {
            if (ModelState.IsValid)
            {
                var book = _mapper.Map<BookModel>(bookView);
                await _bookService.EditBook(book);

                return Ok(book);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteBook(id);

            return Ok();
        }
    }

}
