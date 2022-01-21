using Models.V1;
using System.Collections.Generic;

namespace Repositories.V1
{
    public interface IProjectRepository : IRepository<Project> { }

    public class ProjectRepository : IProjectRepository
    {
        private readonly IProcessFlowRepository _processFlowRepository;

        public ProjectRepository(IProcessFlowRepository processFlowRepository)
        {
            _processFlowRepository = processFlowRepository;
        }

        public ICollection<Project> Get()
        {
            var projects = new List<Project>();
            for (int i = 1; i < 11; i++)
            {
                projects.Add(GetById(i));
            }

            return projects;
        }

        public Project GetById(int id)
        {
            return new Project
            {
                Id = id,
                Label = $"project {id}",
                ProcessFlows = _processFlowRepository.Get()
            };
        }
    }
}
