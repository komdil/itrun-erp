using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Product
{
	public class SellProductRequest : IRequest<bool>
	{
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
