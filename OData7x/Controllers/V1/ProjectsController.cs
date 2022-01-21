using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Models.V1;
using Repositories.V1;
using System.Linq;

namespace OData7x.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ProjectsController : ODataController
    {
        private IProjectRepository repository;
        public ProjectsController(IProjectRepository repo)
        {
            repository = repo;
        }

        [EnableQuery]
        public IActionResult Get(ODataQueryOptions<Project> odataQueryOptions)
        {
            var projects = repository.Get();
            return Ok(projects.AsQueryable());
        }

        [EnableQuery]
        public IActionResult Get(int key, ODataQueryOptions<Project> odataQueryOptions) // To be recognized automatically, the identifier parameter needs to be named 'key'
        {
            var project = repository.Get().Where(proj => proj.Id.Equals(key));
            return Ok(project.AsQueryable());
        }

        [EnableQuery]
        public IActionResult GetProcessFlows(int key, ODataQueryOptions<Project> odataQueryOptions) // To be recognized automatically, the identifier parameter needs to be named 'key'
        {
            var processFlows = repository.Get().FirstOrDefault(proj => proj.Id.Equals(key))?
                .ProcessFlows;
            return Ok(processFlows?.AsQueryable());
        }

        // Casing in Http<ACTION> attribute is ignored when receiving a request
        [HttpGet("odata/Projects({ProjectId})/ProcessFlows({keyProcessFlow})")] // prefix defined in AddRouteComponents() is not prepended automatically
        [HttpGet("odata/Projects/{ProjectId}/ProcessFlows/{keyProcessFlow}")] // prefix defined in AddRouteComponents() is not prepended automatically
        [EnableQuery]
        public IActionResult GetProcessFlows(int ProjectId, int keyProcessFlow, ODataQueryOptions<Project> odataQueryOptions)
        // no [FromODataUri] needed for input parameters
        // because of custom route, 'key' in parameter name is not needed
        {
            var processFlow = repository.Get().FirstOrDefault(proj => proj.Id.Equals(ProjectId))?
                .ProcessFlows.Where(flow => flow.Id.Equals(keyProcessFlow));
            return Ok(processFlow?.AsQueryable());
        }
    }
}
