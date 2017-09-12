using Auth.Server.Entities;
using Auth.Server.Models;
using System.Web.Http;

namespace Auth.Server.Controllers
{
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceModel audienceModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Audience newAudience = AudienceStore.AddAudience(audienceModel.Name);

            return Ok<Audience>(newAudience);

        }
    }
}