﻿using AutoMapper;
using Entities.DTO_s;
using Entities.Exceptions;
using Entities.Models;
using Repositories.Contracts;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookManager : IBookService
    {
        private readonly IRepositoryManager _manager;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;

        public BookManager(IRepositoryManager manager, ILoggerService logger, IMapper mapper)
        {
            _manager = manager;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task < BookDto> CreateOneBookAsync(BookDtoForInsertion bookDto)
        {
            var entity= _mapper.Map<Book>(bookDto);
             _manager.Book.CreateOneBook(entity);
            await _manager.SaveAsync();
            return _mapper.Map<BookDto>(entity);
        }

        public async Task DeleteOneBookAsync(int id, bool trackChanges)
        {
            //check entity
            var entity = await _manager.Book.GetOneBookByIdAsync(id, trackChanges);

            if (entity is null)
            {
               throw new BookNotFoundException(id);
            }

             _manager.Book.DeleteOneBook(entity);
            await  _manager.SaveAsync();
        }

        public async Task <IEnumerable<BookDto>> GetAllBooksAsync(bool trackChanges)
        {
            var books = await _manager.Book.GetAllBooksAsync(trackChanges);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task<BookDto> GetBookByIdAsync(int id, bool trackChanges)
        {
           var book = await _manager.Book.GetOneBookByIdAsync(id,trackChanges);
            if (book is null)
                throw new BookNotFoundException(id);
            return _mapper.Map<BookDto>(book);
        }

        public async Task UpdateOneBookAsync(int id, BookDtoForUpdate bookDto,bool trackChanges)
        {
            //check entity
            var entity =await  _manager.Book.GetOneBookByIdAsync(id, trackChanges);
            if(entity is null)           
                throw new BookNotFoundException(id);
            
        //Mapping
            //entity.Title = book.Title;
            //entity.Price= book.Price;
            
            entity = _mapper.Map<Book>(bookDto);
            _manager.Book.Update(entity);
            _manager.SaveAsync();
        }

     
    }
}
