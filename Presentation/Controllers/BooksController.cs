﻿using Entities.DTO_s;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BooksController : ControllerBase
    {
        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [HttpGet]
        public async Task<IActionResult>GetAllBooksAsync()
        {
                var books = await _manager.BookService.GetAllBooksAsync(false);
                return Ok(books);          
        }


        [HttpGet("{id:int}")]
        public async Task< IActionResult> GetOneBookAsync([FromRoute(Name = "id")] int id)
        {     
                var book = await _manager
                .BookService
                .GetBookByIdAsync(id, false);
                return Ok(book);          
        }

        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync([FromBody] BookDtoForInsertion bookDto)
        {           
                if (bookDto is null)
                    return BadRequest();// 400

            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

                return StatusCode(201, book);          
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateOneBookAsync([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
        {
            
                if (bookDto is null)
                    return BadRequest(); //400

                   await  _manager.BookService.UpdateOneBookAsync(id, bookDto, false);
                return NoContent();//204           
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteOneBookAsync([FromRoute] int id)
        {           
               await _manager.BookService.DeleteOneBookAsync(id, false);
                return NoContent();          
        }

    }
}
