using Customers.Cosumer.Messages;
using MediatR;

namespace Customers.Cosumer.Handlers
{
    public class CustomerCreatedHandler : IRequestHandler<CustomerCreated>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;
        public CustomerCreatedHandler(ILogger<CustomerCreatedHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(CustomerCreated request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Customer created id: {id}.", request.Id.ToString());
            return Unit.Task;
        }
    }


}
