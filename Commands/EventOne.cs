using System;
using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using Paramore.Brighter.Logging.Attributes;

namespace BrighterTest2.Commands
{
    public class EventOne : EventWU
    {
        public string Event { get; set; }
    }

    public class EventHandlerOne : RequestHandlerAsync<EventOne>
    {
        [RequestLoggingAsync(1, HandlerTiming.Before)]
        [RequestUserInformation(2, HandlerTiming.Before)]
        public override async Task<EventOne> HandleAsync(EventOne command,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var started = DateTime.Now;
            Console.WriteLine(started + " " +  this.Name + "Started");   
            Thread.Sleep(1000);
            var ended = DateTime.Now;
            Console.WriteLine(ended + " " +  this.Name + "Ended");
            return await base.HandleAsync(command, cancellationToken);
        }
    }

    public class EventHandlerTwo : RequestHandlerAsync<EventOne>
    {
        private readonly IAmACommandProcessor _processor;

        public EventHandlerTwo(IAmACommandProcessor processor)
        {
            _processor = processor;
        }
        [RequestLoggingAsync(1, HandlerTiming.Before)]
        [RequestUserInformation(2, HandlerTiming.Before)]
        public override async Task<EventOne> HandleAsync(EventOne command,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var started = DateTime.Now;
            Console.WriteLine(started + " " +  this.Name + "Started");   
            await _processor.SendAsync(new CommandTwo {Command = "OneTwo"});
            await _processor.SendAsync(new CommandTwo {Command = "TwoTwo"});
            var ended = DateTime.Now;
            Console.WriteLine(ended + " " +  this.Name + "Ended");
            return await base.HandleAsync(command, cancellationToken);
        }
    }

    public class EventHandlerThree : RequestHandlerAsync<EventOne>
    {
        [RequestLoggingAsync(1, HandlerTiming.Before)]
        [RequestUserInformation(2, HandlerTiming.Before)]
        public override async Task<EventOne> HandleAsync(EventOne command,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var started = DateTime.Now;
            Console.WriteLine(started + " " +  this.Name + "Started");   
            var ended = DateTime.Now;
            Console.WriteLine(ended + " " +  this.Name + "Ended");
            return await base.HandleAsync(command, cancellationToken);
        }
    }
}