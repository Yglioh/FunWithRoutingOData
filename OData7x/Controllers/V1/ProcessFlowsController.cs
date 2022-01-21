using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Models.V1;
using Repositories.V1;
using System.Linq;

namespace OData8x.Controllers.V1
{
    [ApiVersion("1.0")]
    public class ProcessFlowsController : ODataController
    {
        private readonly IProcessFlowRepository _repository;
        public ProcessFlowsController(IProcessFlowRepository repo)
        {
            _repository = repo;
        }

        [EnableQuery]
        public IActionResult Get(ODataQueryOptions<Project> odataQueryOptions)
        {
            var projects = _repository.Get();
            return Ok(projects.AsQueryable());
        }

        [EnableQuery]
        public IActionResult Get(int key, ODataQueryOptions<Project> odataQueryOptions)
        {
            var project = _repository.Get().Where(proj => proj.Id.Equals(key));
            return Ok(project.AsQueryable());
        }

        [EnableQuery]
        public IActionResult GetParameters(int key, ODataQueryOptions<Project> odataQueryOptions)
        {
            var processFlows = _repository.Get().FirstOrDefault(proj => proj.Id.Equals(key))?
                .Parameters;
            return Ok(processFlows?.AsQueryable());
        }

        [HttpGet("odata/ProcessFlows({keyProcessFlow})/Parameters({keyParameter})")]
        [HttpGet("odata/ProcessFlows/{keyProcessFlow}/Parameters/{keyParameter}")]
        [EnableQuery]
        public IActionResult GetParameters(int keyProcessFlow, int keyParameter, ODataQueryOptions<Project> odataQueryOptions)
        {
            var parameter = _repository.Get().FirstOrDefault(proj => proj.Id.Equals(keyProcessFlow))?
                .Parameters.Where(flow => flow.Id.Equals(keyParameter));
            return Ok(parameter?.AsQueryable());
        }
    }
}

