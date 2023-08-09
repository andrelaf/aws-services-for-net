using Customers.Cosumer.Messages;
using MediatR;

namespace Customers.Cosumer.Handlers;

public class CustomerDeletedHandler : IRequestHandler<CustomerDeleted>
{
    private readonly ILogger<CustomerCreatedHandler> _logger;
    public CustomerDeletedHandler(ILogger<CustomerCreatedHandler> logger)
    {
        _logger = logger;
    }
    public Task Handle(CustomerDeleted request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Customer deleted id: {Id}.", request.Id.ToString());
        return Unit.Task;
    }
}
