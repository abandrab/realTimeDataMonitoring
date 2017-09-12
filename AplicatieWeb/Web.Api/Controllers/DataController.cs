using Business.Commands;
using Common.Layer.CqrsCore;
using Common.Layer.Helpers;
using Domain.Model.Models.MongoDB;
using System.Collections.Generic;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [RoutePrefix("api/Data")]
    public class DataController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public DataController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        // [Authorize]
        [AllowAnonymous]
        [HttpPost]
        [Route("AddMany")]
        public IHttpActionResult AddMany(List<Data> datas)
        {
            var mapped = Mapper.Map(datas);
            var command = new AddDatasCommand(mapped);
            commandDispatcher.Dispatch(command);
            return Ok("Added to database!");
        }
    }
}