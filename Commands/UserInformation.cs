using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Paramore.Brighter;

namespace BrighterTest2.Commands
{
    public interface IUser
    {
        string User { get; set; }
    }

    public interface IUserAccessor
    {
        string GetUser();
    }

    public class HttpUserAccessor : IUserAccessor
    {
        IHttpContextAccessor _accessor;

        public HttpUserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string GetUser()
        {
            return _accessor.HttpContext.User.Identity?.Name;
        }
    }

    public class CommandWU : Command, IUser
    {
        public CommandWU() : base(Guid.NewGuid()){}
        public string User { get; set; }
    }

    public class EventWU : Event, IUser
    {
        public EventWU() : base(Guid.NewGuid()) { }
        public string User { get; set; }
    }

    public class RequestUserInformationHandlerAsync<TRequest>
       : RequestHandlerAsync<TRequest> where TRequest : class, IRequest, IUser
    {      
        public override async Task<TRequest> HandleAsync(TRequest command, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (String.IsNullOrEmpty(command.User))
            {
                command.User = "anonymous";
            }

            return await base.HandleAsync(command, cancellationToken).ConfigureAwait(ContinueOnCapturedContext);
        }
    }

    public class RequestUserInformationAttribute : RequestHandlerAttribute
    {
        public RequestUserInformationAttribute(int step, HandlerTiming timing)
            : base(step, timing)
        { }

        public override object[] InitializerParams()
        {
            return new object[] { Timing };
        }

        public override Type GetHandlerType()
        {
            return typeof(RequestUserInformationHandlerAsync<>);
        }
    }
}