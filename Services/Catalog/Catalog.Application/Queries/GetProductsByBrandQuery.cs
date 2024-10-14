using Amazon.Runtime.Internal;
using Catalog.Application.Responses;
using MediatR;

namespace Catalog.Application.Queries
{
    public class GetProductsByBrandQuery : IRequest<IList<ProductResponse>>
    {
        public string Brand { get; set; }
        public GetProductsByBrandQuery(string brand)
        {
            Brand = brand;
        }
    }
}
