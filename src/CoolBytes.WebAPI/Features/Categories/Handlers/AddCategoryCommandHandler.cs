﻿using CoolBytes.Core.Utils;
using CoolBytes.Data;
using CoolBytes.WebAPI.Features.Categories.CQ;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CoolBytes.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace CoolBytes.WebAPI.Features.Categories.Handlers
{
    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, Result>
    {
        private readonly AppDbContext _context;

        public AddCategoryCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var highestSortOrder = await _context.Categories.MaxAsync(c => c.SortOrder);
            var sortOrder = ++highestSortOrder;

            var category = new Category(request.Name, sortOrder, request.Description, request.IsCourse);
            _context.Categories.Add(category);

            await _context.SaveChangesAsync(cancellationToken);

            return Result.SuccessResult();
        }
    }
}