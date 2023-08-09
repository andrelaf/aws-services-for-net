using Customers.Cosumer.Messages;
using MediatR;

namespace Customers.Cosumer.Handlers
{
    public class CustomerUpdatedHandler : IRequestHandler<CustomerUpdated>
    {
        private readonly ILogger<CustomerCreatedHandler> _logger;
        public CustomerUpdatedHandler(ILogger<CustomerCreatedHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(CustomerUpdated request, CancellationToken cancellationToken)
        {

            //_logger.LogInformation("Customer updated: {FullName}.", request.FullName);
            throw new Exception("Something broke oops");
            return Unit.Task;
        }
    }
}
