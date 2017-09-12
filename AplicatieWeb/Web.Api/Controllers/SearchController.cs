using Business.Queries;
using Business.QueryResults;
using Common.Layer.CqrsCore;
using System.Web.Http;

namespace Web.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        private readonly IQueryDispatcher queryDispatcher;

        public SearchController(IQueryDispatcher queryDispatcher)
        {
            this.queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Get(string searchString)
        {
            var query = new GetSearchQuery(searchString);
            var result = queryDispatcher.Dispatch<GetSearchQuery, GetSearchQueryResult>(query).Doctors;
            return Ok(result);
        }
    }
}