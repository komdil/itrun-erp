using Application.Common.Exceptions;
using Application.Common.Interfaces;
using AutoMapper;
using Warehouse.Contracts.Product;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using Warehouse.Contracts.ProductUOM;

namespace Application.ProductUom.Commands.UpdateProductUom
{
    public class UpdateProductUomCommandHandler : IRequestHandler<UpdateProductUomRequest, SingleProductUomResponse>
    {
        IApplicationDbContext _dbContext;
        IMapper _mapper;

        public UpdateProductUomCommandHandler(IApplicationDbContext dbcontext, IMapper mapper)
        {
            _dbContext = dbcontext;
            _mapper = mapper;
        }

        public async Task<SingleProductUomResponse> Handle(UpdateProductUomRequest request, CancellationToken cancellationToken)
        {
            var productuom = await _dbContext.ProductUOMs.FirstOrDefaultAsync(prod => prod.Id == request.Id, cancellationToken: cancellationToken);

            if (productuom == null)
                throw new NotFoundException();
            productuom.Name = request.Name;
            productuom.Details = request.Details;
            productuom.Abbreviation = request.Abbreviation;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return _mapper.Map<SingleProductUomResponse>(productuom);
        }
    }
}