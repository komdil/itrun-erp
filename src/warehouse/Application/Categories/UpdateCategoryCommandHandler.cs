using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Warehouse.Contracts.Categories;
using Warehouse.Contracts.Warehouse;

namespace Application.Categories
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryRequest, SingleCategoryResponse>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public UpdateCategoryCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleCategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (category == null)
                throw new NotFoundException();

            category.Name = request.Name;
            category.Description = request.Description;
            category.ParentCategoryId = request.ParentCategoryId;

            foreach(var item in request.SubCategories)
            {
                var subCategory = _context.Categories.Find(item);
                subCategory.ParentCategoryId = category.Id;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SingleCategoryResponse>(category);
        }
    }
}
