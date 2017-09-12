using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [RoutePrefix("api/Sensor")]
    public class SensorController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;
        public SensorController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("Ping")]
        public IHttpActionResult Get()
        {
            return Ok("OK");
        }
    }
}