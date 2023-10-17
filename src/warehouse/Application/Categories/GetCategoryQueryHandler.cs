using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Warehouse.Contracts.Warehouse;
using Warehouse.Contracts.Categories;
using Microsoft.EntityFrameworkCore;

namespace Application.Categories
{
    internal class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, List<SingleCategoryResponse>>
        {
            IApplicationDbContext _dbContext;
            IMapper _mapper;

            public GetCategoryQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }

            public async Task<List<SingleCategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
            {
                IQueryable<Category> categoryQuery = _dbContext.Categories;

                if (!string.IsNullOrWhiteSpace(request.Name))
                categoryQuery = categoryQuery.Where(p => p.Name == request.Name);

                if (!string.IsNullOrWhiteSpace(request.Description))
                categoryQuery = categoryQuery.Where(p => p.Description == request.Description);            

                categoryQuery = categoryQuery.Skip(request.Skip).Take(request.PageSize);

                return await categoryQuery.ProjectTo<SingleCategoryResponse>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
    }
}

