using Business.Commands;
using Common.Layer;
using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    [RoutePrefix("api/Administrator")]
    public class AdministratorController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        public AdministratorController(ICommandDispatcher commandDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(Administrator admin)
        {
            var command = new RegisterAdministratorCommand(admin);
            commandDispatcher.Dispatch(command);
            return Ok();
        }
    }
}