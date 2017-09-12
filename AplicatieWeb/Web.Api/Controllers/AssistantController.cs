using Business.Commands;
using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Assistant")]
    public class AssistantController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        public AssistantController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(Assistant admin)
        {
            var command = new RegisterAssistantCommand(admin);
            commandDispatcher.Dispatch(command);
            return Ok();
        }
    }
}