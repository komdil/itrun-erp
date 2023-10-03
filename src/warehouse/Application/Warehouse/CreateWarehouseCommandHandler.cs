using Application.Common.Interfaces;
using Warehouse.Contracts.Warehouse;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Contracts.Categories;

namespace Application.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryRequest, CreateCategoryResponse>
    {
        IApplicationDbContext _context;
        public CreateCategoryCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CreateCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = new Category()
            {
                
                Name = request.Name,
                Description = request.Description,
                ParentCategory = request.ParentCategory,
                SubCategories = request.SubCategories,
                Id = Guid.NewGuid(),
            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new CreateCategoryResponse
            {
                Description = category.Description,
                ParentCategory = category.ParentCategory,
                SubCategories = category.SubCategories,
                Name = category.Name,
                Slug = category.Name,
                //TODO: Use auto mapper
            };
        }
    }
}
