using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;

namespace Discount.Application.Handlers
{
    public class DeleteCouponCommandHandler : IRequestHandler<DeleteCouponCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;

        public DeleteCouponCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }
        public async Task<bool> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
        {
            var deleted = await _discountRepository.DeleteCoupon(request.ProductName);
            return deleted;
        }
    }
}
