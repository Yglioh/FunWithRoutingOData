using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.OData.Formatter;    Needed when making use of the attribute '[FromODataUri]'
using Microsoft.AspNetCore.OData.Query;
// using Microsoft.AspNetCore.OData.Routing.Attributes;       Needed when making use of the attributes 'ODataAttributeRouting' or 'Route'
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Models.V1;
using Repositories.V1;
using System.Linq;

namespace OData8x.Controllers.V1
{
    [ApiVersion("1.0")]
    // [ODataAttributeRouting] This isn't necessary because controller inherits from ODataController
    /* [Route("AddIns")] This will add a route prefix to all non-odata endpoints, i.e. where we add a Http<ACTION> attribute
                       * However, this will result in incorrect syntaxis: 
                       *     AddIns/({keyAddIn})/Formulas({keyFormula})    --> this should have been     AddIns({keyAddIn})/Formulas({keyFormula})
                       *     AddIns/{keyAddIn}/Formulas/{keyFormula}
                       */
    public class ProjectsController : ODataController
    {
        private readonly IProjectRepository _repository;
        public ProjectsController(IProjectRepository repo)
        {
            _repository = repo;
        }

        [EnableQuery]
        public IActionResult Get(ODataQueryOptions<Project> odataQueryOptions)
        {
            var projects = _repository.Get();
            return Ok(projects.AsQueryable());
        }

        // BUG @odata.editlink is broken: it doesn't show the entire link in response body when request contains accept-header: application/json;odata.metadata=full 
        [EnableQuery]
        public IActionResult Get(int key, ODataQueryOptions<Project> odataQueryOptions) // To be recognized automatically, the identifier parameter needs to be named 'key'
        {
            var project = _repository.Get().Where(proj => proj.Id.Equals(key));
            return Ok(project.AsQueryable());
        }

        [EnableQuery]
        public IActionResult GetProcessFlows(int key, ODataQueryOptions<Project> odataQueryOptions) // To be recognized automatically, the identifier parameter needs to be named 'key'
        {
            var processFlows = _repository.Get().FirstOrDefault(proj => proj.Id.Equals(key))?
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
            var processFlow = _repository.Get().FirstOrDefault(proj => proj.Id.Equals(ProjectId))?
                .ProcessFlows.Where(flow => flow.Id.Equals(keyProcessFlow));
            return Ok(processFlow?.AsQueryable());
        }
    }
}
