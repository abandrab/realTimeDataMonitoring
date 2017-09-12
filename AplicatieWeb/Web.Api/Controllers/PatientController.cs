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
    [RoutePrefix("api/Patient")]
    public class PatientController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public PatientController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(Patient patient)
        {
            var command = new RegisterPatientCommand(patient);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();
            var query = new GetPatientQuery(email);

            var user = queryDispatcher.Dispatch<GetPatientQuery, GetPatientQueryResult>(query);
            return Ok(user);
        }

        [Authorize(Roles = Roles.Patient)]
        [HttpGet]
        [Route("GetSensors")]
        public IHttpActionResult GetSenors()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();
            var query = new GetPatientSensorsQuery(email);

            var sensors = queryDispatcher.Dispatch<GetPatientSensorsQuery, GetPatientSensorsQueryResult>(query).Sensors;
            return Ok(sensors);
        }
        [Authorize(Roles = Roles.Doctor)]
        [HttpGet]
        [Route("GetSensors")]
        public IHttpActionResult GetSenors([FromUri] string email)
        {
            var query = new GetPatientSensorsQuery(email);

            var sensors = queryDispatcher.Dispatch<GetPatientSensorsQuery, GetPatientSensorsQueryResult>(query).Sensors;
            return Ok(sensors);
        }
        [Authorize(Roles = Roles.Doctor)]
        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult GetPatient(string id)
        {
            var query = new GetPatientByIdQuery(id);

            var user = queryDispatcher.Dispatch<GetPatientByIdQuery, GetPatientByIdQueryResult>(query);
            return Ok(user);
        }


        [Authorize(Roles = Roles.Patient)]
        [HttpPost]
        [Route("Edit")]
        public IHttpActionResult Edit(Patient patient)
        {
            var command = new EditPatientCommand(patient);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GetCoordinator")]
        public IHttpActionResult GetCoordinatorIp()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();
            var query = new GetCoordinatorQuery(email);
            var result = queryDispatcher.Dispatch<GetCoordinatorQuery, GetCoordinatorQueryResult>(query).IP;
            return Ok(result);
        }

        [Authorize]
        [HttpGet]
        [Route("GetPatient")]
        public IHttpActionResult GetPatientInfos()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var query = new GetPatientInfosQuery(email);
            var result = queryDispatcher.Dispatch<GetPatientInfosQuery, GetPatientInfosQueryResult>(query);
            return Ok(result.Patient);
        }

        [Authorize]
        [HttpGet]
        [Route("GetDoctor")]
        public IHttpActionResult GetPatientDoctor()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                   .Select(c => c.Value).SingleOrDefault();

            var query = new GetPatientDoctorQuery(email);
            var result = queryDispatcher.Dispatch<GetPatientDoctorQuery, GetPatientDoctorQueryResult>(query);
            return Ok(result.Doctor);
        }
    }
}