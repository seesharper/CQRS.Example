using System.Threading;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;

namespace CQRS.Example.Server.Validation
{
    public class ValidatedCommandHandler<TCommand> : ICommandHandler<TCommand>
    {
        private ICommandHandler<TCommand> commandHandler;
        private readonly IValidator<TCommand> validator;

        public ValidatedCommandHandler(ICommandHandler<TCommand> commandHandler, IValidator<TCommand> validator)
        {
            this.commandHandler = commandHandler;
            this.validator = validator;
        }

        public async Task HandleAsync(TCommand command, CancellationToken cancellationToken = default)
        {
            validator.Validate(command);
            await commandHandler.HandleAsync(command).ConfigureAwait(false);
        }
    }
}