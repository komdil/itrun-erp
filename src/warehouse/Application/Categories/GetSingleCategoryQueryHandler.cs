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
    public class GetSingleCategoryQueryHandler : IRequestHandler<GetSingleCategoryQuery, SingleCategoryResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public GetSingleCategoryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<SingleCategoryResponse> Handle(GetSingleCategoryQuery request, CancellationToken cancellationToken)
        {
            Category category = await _dbContext.Categories.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (category == null)
                throw new NotFoundException();
            return _mapper.Map<SingleCategoryResponse>(category);
        }
    }
}
