using MediatR;

namespace Discount.Application.Commands
{
    public class DeleteCouponCommand : IRequest<bool>
    {
        public string ProductName { get; set; }
        public DeleteCouponCommand(string productName) 
        {
            ProductName = productName;
        }

    }
}
