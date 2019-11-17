using CQRS.Command.Abstractions;
using CQRS.Example.Server.Validation;
using CQRS.LightInject;
using LightInject;

namespace CQRS.Example.Server
{
    public class ServerCompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry
                .RegisterCommandHandlers()
                .RegisterQueryHandlers()
                .RegisterSingleton(typeof(IValidator<>), typeof(DefaultValidator<>))
                .Decorate(typeof(ICommandHandler<>), typeof(ValidatedCommandHandler<>));
        }
    }
}