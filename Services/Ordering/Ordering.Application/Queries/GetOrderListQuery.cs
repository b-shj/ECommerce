using MediatR;
using Ordering.Application.Responses;

namespace Ordering.Application.Queries
{
    public class GetOrderListQuery : IRequest<List<OrderResponse>>
    {
        public string Username { get; }
        public GetOrderListQuery(string username)
        {
            Username = username;
        }

    }
}
