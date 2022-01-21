using Models.V1;
using System.Collections.Generic;

namespace Repositories.V1
{
    public interface IProcessFlowRepository : IRepository<ProcessFlow> { }

    public class ProcessFlowRepository : IProcessFlowRepository
    {
        private readonly IParameterRepository _processFlowParameterRepository;

        public ProcessFlowRepository(IParameterRepository processFlowParameterRepository)
        {
            _processFlowParameterRepository = processFlowParameterRepository;
        }

        public ICollection<ProcessFlow> Get()
        {
            var processFlows = new List<ProcessFlow>();
            for (int i = 1; i < 11; i++)
            {
                processFlows.Add(GetById(i));
            }

            return processFlows;
        }

        public ProcessFlow GetById(int id)
        {
            return new ProcessFlow
            {
                Id = id,
                Label = $"Flow {id}",
                Parameters = _processFlowParameterRepository.Get()
            };
        }
    }
}
