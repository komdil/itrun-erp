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
using AutoMapper;
using Warehouse.Contracts.ProductUOM;

namespace Application.Categories
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryRequest, SingleCategoryResponse>
    {
        IApplicationDbContext _context;
        IMapper _mapper;
        public CreateCategoryCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<SingleCategoryResponse> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<SingleCategoryResponse>(category);
        }
    }
}
