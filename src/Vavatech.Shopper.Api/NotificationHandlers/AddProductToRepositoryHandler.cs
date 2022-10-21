using MediatR;
using Vavatech.Shopper.Api.Controllers;

namespace Vavatech.Shopper.Api.NotificationHandlers
{
    public class AddProductToRepositoryHandler : INotificationHandler<AddedProduct>
    {
        private readonly IProductRepository _productRepository;

        public AddProductToRepositoryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task Handle(AddedProduct notification, CancellationToken cancellationToken)
        {
            _productRepository.Add(notification.Product);

            return Task.CompletedTask;
        }
    }
}
