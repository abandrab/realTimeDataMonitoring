using Business.Commands;
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
    [Authorize]
    [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        private readonly ICommandDispatcher commandDispatcher;
        private readonly IQueryDispatcher queryDispatcher;

        public AccountController(ICommandDispatcher commandDispatcher, IQueryDispatcher queryDispatcher)
        {
            this.commandDispatcher = commandDispatcher;
            this.queryDispatcher = queryDispatcher;
        } 
       
        [Route("GetRole")]
        public IHttpActionResult GetRole()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var role = identity.Claims.Where(c => c.Type == ClaimTypes.Role)
                               .Select(c => c.Value).SingleOrDefault();
            return Ok(role);
        }

        [Authorize]
        [Route("ChangePass")]
        [HttpPost]
        public IHttpActionResult ChangePassword(PasswordsWrapper passwords)
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();
            var query = new CheckPasswordQuery(email, passwords.OldPassword);
            var result = queryDispatcher.Dispatch<CheckPasswordQuery, CheckPasswordQueryResult>(query).CorrectPassword;
            if(result)
            {
                var command = new ChangePasswordCommand(email, passwords.NewPassword);
                commandDispatcher.Dispatch(command);
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
          
        }
        [AllowAnonymous]
        [Route("VerifyEmail")]
        [HttpGet]
        public IHttpActionResult VerifyEmail(string email)
        {
            var query = new GetEmailQuery(email);
            var result = this.queryDispatcher.Dispatch<GetEmailQuery, GetEmailQueryResult>(query);
            return Ok(result.Exists);
        }

        [Authorize]
        [Route("Delete")]
        [HttpGet]
        public IHttpActionResult DeleteAccount()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var email = identity.Claims.Where(c => c.Type == ClaimTypes.Email)
                               .Select(c => c.Value).SingleOrDefault();
            var command = new DeleteAccountCommand(email);
            commandDispatcher.Dispatch(command);
            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("GetCoordinator")]
        public IHttpActionResult GetCoordinatorIp(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Equals("undefined"))
            {
                return Ok("192.168.43.86");
            }
            else
            {

                var queryEmail = new GetPatientEmailQuery(id);
                var email = queryDispatcher.Dispatch<GetPatientEmailQuery, GetPatientEmailQueryResult>(queryEmail).Email;
                if (email != null)
                {
                    var query = new GetCoordinatorQuery(email);
                    var result = queryDispatcher.Dispatch<GetCoordinatorQuery, GetCoordinatorQueryResult>(query).IP;
                    return Ok(result);
                }
                else
                {
                    return Ok("Patient not found!");
                }
            }
        }
    }
}
