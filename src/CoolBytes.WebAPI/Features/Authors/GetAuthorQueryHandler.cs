﻿using System.Threading.Tasks;
using AutoMapper;
using CoolBytes.Core.Interfaces;
using CoolBytes.Core.Models;
using CoolBytes.Data;
using CoolBytes.WebAPI.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CoolBytes.WebAPI.Features.Authors
{
    public class GetAuthorQueryHandler : IAsyncRequestHandler<GetAuthorQuery, AuthorViewModel>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAuthorService _authorService;

        public GetAuthorQueryHandler(AppDbContext appDbContext, IAuthorService authorService)
        {
            _appDbContext = appDbContext;
            _authorService = authorService;
        }

        public async Task<AuthorViewModel> Handle(GetAuthorQuery message)
        {
            var author = await _authorService.GetAuthorWithProfile();

            return CreateViewModel(author);
        }

        private AuthorViewModel CreateViewModel(Author author) => Mapper.Map<AuthorViewModel>(author);
    }
}