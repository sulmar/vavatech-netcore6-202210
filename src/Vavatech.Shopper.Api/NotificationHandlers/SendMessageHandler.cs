using MediatR;
using Vavatech.Shopper.Api.Controllers;

namespace Vavatech.Shopper.Api.NotificationHandlers
{
    public class SendMessageHandler : INotificationHandler<AddedProduct>
    {
        private readonly IMessageSender messageSender;

        public SendMessageHandler(IMessageSender messageSender)
        {
            this.messageSender = messageSender;
        }

        public Task Handle(AddedProduct notification, CancellationToken cancellationToken)
        {
            messageSender.Send(notification.Product);

            return Task.CompletedTask;
        }
    }
}
