using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.Categories;
using Warehouse.Contracts.Warehouse;

namespace Application.Categories
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryRequest>
    {
        IApplicationDbContext _context;
        public DeleteCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.Name == request.Name);
            if (category == null)
                throw new NotFoundException();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
