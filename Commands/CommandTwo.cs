using System;
using System.Threading;
using System.Threading.Tasks;
using Paramore.Brighter;
using Paramore.Brighter.Logging.Attributes;

namespace BrighterTest2.Commands
{
    
    public class CommandTwo : CommandWU
    {
        public string Command { get; set; }
    }

    public class CommandTwoHandler : RequestHandlerAsync<CommandTwo>
    {
        public CommandTwoHandler()
        {
            
        }
        [RequestLoggingAsync(1, HandlerTiming.Before)]
        public override async Task<CommandTwo> HandleAsync(CommandTwo command, CancellationToken cancellationToken = new CancellationToken())
        {
            var started = DateTime.Now;
            Console.WriteLine(started + " " +  this.Name + "Started");      
            Thread.Sleep(1000);
            var ended = DateTime.Now;
            Console.WriteLine(ended + " " +  this.Name + "Ended");
            
            return await base.HandleAsync(command, cancellationToken);
        }
    }
}
