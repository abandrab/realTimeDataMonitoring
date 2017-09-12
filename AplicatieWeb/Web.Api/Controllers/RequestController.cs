using Business.Commands;
using Business.Queries;
using Business.QueryResults;
using Common.Layer;
using Common.Layer.CqrsCore;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Request")]
    public class RequestController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public RequestController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult GetAll()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var query = new GetRequestsQuery(email);
            var result = queryDispatcher.Dispatch<GetRequestsQuery, GetRequestsQueryResult>(query).Requests;
            return Ok(result);
        }
        
        [Authorize(Roles = Roles.Patient)]
        [HttpPost]
        [Route("Add")]
        public IHttpActionResult Add([FromBody]string doctorId)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var command = new AddRequestCommand(doctorId, email);
            commandDispatcher.Dispatch(command);

            return Ok();
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet]
        [Route("Check")]
        public IHttpActionResult Check()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var query = new GetPatientRequestQuery(email);
            var result = queryDispatcher.Dispatch<GetPatientRequestQuery, GetPatientRequestQueryResult>(query).Request;
            return Ok(result);
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        [Route("Approve")]
        public IHttpActionResult ApproveRequest([FromBody] string id)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var command = new ApproveRequestCommand(email, id);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        [Route("Reject")]
        public IHttpActionResult RejectRequest([FromBody] string id)
        {
            var command = new RejectRequestCommand(id);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet]
        [Route("Delete")]
        public IHttpActionResult DeleteRequest()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();

            var command = new DeleteRequestCommand(email);
            commandDispatcher.Dispatch(command);
            return Ok();
        }
    }
}