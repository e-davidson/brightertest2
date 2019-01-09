using System;
using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using Paramore.Brighter.Logging.Attributes;

namespace BrighterTest2.Commands
{
    public class CommandOne : CommandWU
    {
        public string Command { get; set; }
    }

    public class CommandOneHandler : RequestHandlerAsync<CommandOne>
    {
        private readonly IAmACommandProcessor _commandProcessor;

        public CommandOneHandler(IAmACommandProcessor commandProcessor)
        {
            _commandProcessor = commandProcessor;
        }

        [RequestLoggingAsync(1, HandlerTiming.Before)]
        public override async Task<CommandOne> HandleAsync(CommandOne command, CancellationToken cancellationToken = new CancellationToken())
        {
            Console.WriteLine(command.Command);

            await _commandProcessor.PublishAsync(new EventOne {Event = "I'm an event"});
            
            return await base.HandleAsync(command, cancellationToken);
        }
    }
}