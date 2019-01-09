using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrighterTest2.Commands;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;

namespace BrighterTest2.Controllers
{
    [Route("/")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAmACommandProcessor _processor;

        // GET api/values
        public ValuesController(IAmACommandProcessor processor)
        {
            _processor = processor;
        }

        [HttpGet("/")]
        public async Task<ActionResult> Get()
        {
            await _processor.SendAsync(new CommandOne {Command = "Hello I'm Command One"});
            return Ok( "Processor complete");
        }
    }
}
