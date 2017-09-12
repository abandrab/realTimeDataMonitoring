using Business.Commands;
using Business.Queries;
using Business.QueryResults;
using Common.Layer;
using Common.Layer.CqrsCore;
using Domain.Model.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Doctor")]
    public class DoctorController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public DoctorController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(Doctor doctor)
        {
            var command = new RegisterDoctorCommand(doctor);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();
            var query = new GetDoctorQuery(email);
            
            var user = queryDispatcher.Dispatch<GetDoctorQuery, GetDoctorQueryResult>(query);
            return Ok(user);
        }

        [Authorize(Roles = Roles.Doctor)]
        [HttpPost]
        [Route("Edit")]
        public IHttpActionResult Edit(Doctor Doctor)
        {
            var command = new EditDoctorCommand(Doctor);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GetDoctor")]
        public IHttpActionResult GetDoctorInfos()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var query = new GetDoctorInfosQuery(email);
            var result = queryDispatcher.Dispatch<GetDoctorInfosQuery, GetDoctorInfosQueryResult>(query);
            return Ok(result.Id);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPatients")]
        public IHttpActionResult GetPatients()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var query = new GetPatientsQuery(email);
            var result = queryDispatcher.Dispatch<GetPatientsQuery, GetPatientsQueryResult>(query);
            return Ok(result.Patients);
        }
    }
}