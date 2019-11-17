using System;
using System.Threading.Tasks;
using CQRS.Command.Abstractions;
using CQRS.Example.Server.Customers;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Example.Application.Customers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ICommandExecutor commandExecutor;

        public CustomersController(ICommandExecutor commandExecutor)
        {
            this.commandExecutor = commandExecutor;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateCustomerCommand command)
        {
            await commandExecutor.ExecuteAsync(command);
            return CreatedAtAction(nameof(Post), new { Id = command.Id });
        }
    }
}